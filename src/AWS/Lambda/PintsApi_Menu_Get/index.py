import boto3
from boto3.dynamodb.conditions import Key
import json
import re
from trp import Document

def get_textract_client(region):
   textract_client = boto3.client('textract', region_name=region)
   return textract_client
    
def clean_string(str):
   return re.sub(r'[^A-Za-z0-9 ]+', "", str)
   
   
def get_textract_document(region, bucket, document):
    # create textract client
    textract_client = get_textract_client(region)
    #get text from image
    textract_response = textract_client.detect_document_text(Document= {
        'S3Object': {
            'Bucket': bucket,
            'Name': document 
        }
    })
    return Document(textract_response)


def lambda_handler(event, context):

   bucket = 'pintmenus'
   document = event['resource']
   region = 'us-east-1'

   if len(document) <= 0:
      return {
         'statusCode': '400',
         'body': 'Bad Request. No resource specified'
      }
   
   doc = get_textract_document(region, bucket, document)
   lines = []
   for page in doc.pages:
      for line in page.lines:
         #not a line
         if(line.confidence < 94):
            continue
         # clean weird characters
         cleanLine = clean_string(line.text)
         if(len(cleanLine) <= 0):
            continue
         # no numbers only strings
         if(cleanLine.isdigit()):
            continue
         #remove duplicates
         if(cleanLine in lines):
            continue
         
         print(cleanLine)
         lines.append(cleanLine)
         
   pints = []
   
   client = boto3.resource('dynamodb')
   pintTable = client.Table('pints')
   ratingTable = client.Table('beer_ratings')
   for line in lines:
      
      # index queries
      # these can't be done on a batch-get because dynamodb does not support batch gets on indexes
      lineDbResult = pintTable.query(
         IndexName='name-index',
         KeyConditionExpression=Key('name').eq(line),
      )
      ratingsResult = ratingTable.query(
         IndexName='beer_name-index',
         KeyConditionExpression=Key('beer_name').eq(line),
      )

      if 'Items' in lineDbResult:
         for indexedItem in lineDbResult['Items']:
            
            #get the pint
            itemObjectResponse = pintTable.get_item(
               Key= {'id': indexedItem['id']}
            )
            retrievedPint = itemObjectResponse['Item']

            #get ratings
            if ratingsResult['Count'] > 0:
               batch_keys = {
                  ratingTable.name: {
                     'Keys': [{'PartitionKey': indexedRating['PartitionKey']} for indexedRating in ratingsResult['Items']]
                  }
               }
               ratingsResponse = client.batch_get_item(RequestItems=batch_keys)
               if 'Responses' in ratingsResponse:
                  ratings = ratingsResponse['Responses'][ratingTable.name]
                  retrievedPint['ratings'] = ratings

            pints.append(retrievedPint)

            # only process the first match of name
            # because we don't have information about brewery on pints
            # we cannot differentiate different beers with the same name from different breweries
            break
          
   return pints