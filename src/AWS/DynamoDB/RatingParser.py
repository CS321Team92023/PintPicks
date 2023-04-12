#This file will be used to parse the csv file with all the beer rating data and will upload it to DynamoDB
import pandas as pd
import boto3
import hashlib
import time
from decimal import *

#Takes in a csv file and a line number to start reading from
def exportToDynamoDB(csv_file, start_line):
    #Set up DynamoDB client
    dynamodb = boto3.resource('dynamodb')
    #Set up table
    table = dynamodb.Table('beer_ratings')
    print("Now exporting the data to the database!")

    #Read CSV file into Pandas DataFrame, Skips to specified start line
    df = pd.read_csv(csv_file, skiprows=start_line, names=['brewery_id', 'brewery_name', 'review_time', 'review_overall',
                                      'review_aroma', 'review_appearance', 'review_profilename', 'beer_style',
                                      'review_palate', 'review_taste', 'beer_name', 'beer_abv', 'beer_beerid'],
                     header=0)

    #Convert specified columns to string type
    
    current_line = start_line   

    #Use batch writer for more efficient writes
    with table.batch_writer() as batch:
        #Iterate over rows in DataFrame
        for index, row in df.iloc[current_line:].iterrows():
            print('processing line: ', current_line)
            #Sleep for 0.5 seconds to ensure we do not blow our throuput allowance
            time.sleep(100/1000) 
            #Concatenate beer_name and review_time columns
            string_to_hash = str(row['beer_name']) + str(row['review_time'])
            #Hash the string
            hash_value = hashlib.sha256(string_to_hash.encode('utf-8')).hexdigest()
            #Convert row to dictionary
            item = row.to_dict()

            if pd.isnull(row['review_overall']):
                del item['review_overall']
            else:
                item['review_overall'] = Decimal(str(row['review_overall']))
                
            if pd.isnull(row['review_aroma']):
                del item['review_aroma']
            else:
                item['review_aroma'] = Decimal(str(row['review_aroma']))
                
            if pd.isnull(row['review_appearance']):
                del item['review_appearance']
            else:
                item['review_appearance'] = Decimal(str(row['review_appearance']))
                
            if pd.isnull(row['review_palate']):
                del item['review_palate']
            else:
                item['review_palate'] = Decimal(str(row['review_palate']))
                
            if pd.isnull(row['review_taste']):
                del item['review_taste']
            else:
                item['review_taste'] = Decimal(str(row['review_taste']))

            if pd.isnull(row['beer_abv']):
                del item['beer_abv']
            else:
                item['beer_abv'] = Decimal(str(row['beer_abv']))

            # if any of these values is null then we will have an exception
            # so remove them
            if pd.isnull(row['brewery_id']):
                del item['brewery_id']
            if pd.isnull(row['brewery_name']):
                del item['brewery_name']
            if pd.isnull(row['beer_style']):
                del item['beer_style']
            if pd.isnull(row['beer_name']):
                del item['beer_name']
            if pd.isnull(row['review_profilename']):
                del item['review_profilename']

            #Add partition key to item
            item['PartitionKey'] = hash_value
            #Add item to batch writer
            batch.put_item(Item=item)
            #Updates current line and writes it to a file
            current_line += 1
            with open('line_count.txt', 'w') as line_file:
                    line_file.write(str(current_line))

    print("Done!")


exportToDynamoDB('beer_reviews.csv', 819)

        
