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
   table = client.Table('pints')
   for line in lines:
      
      lineDbResult = table.query(
         IndexName='name-index',
         KeyConditionExpression=Key('name').eq(line),
      )

      if 'Items' in lineDbResult:
         for indexedItem in lineDbResult['Items']:
            
            itemObjectResponse = table.get_item(
               Key= {'id': indexedItem['id']}
            )
            retrievedPint = itemObjectResponse['Item']
            pints.append(retrievedPint)
            break
          
   return pints

   #return {
   #   'statusCode': '404',
   #   'body': 'Not found'
   #}
       
       