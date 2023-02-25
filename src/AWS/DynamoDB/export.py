
import boto3
import sys
import os, json 
import glob
import time
import argparse

def export(table, data_folder):
    dynamodb = boto3.resource('dynamodb')

    print('now exporting data to table: ' + table)
    
    table = dynamodb.Table(table)


    for file in glob.glob(data_folder + "*.json"):
        print('processing file: ' + file)
        f = open(file, encoding="utf8")
        data = json.load(f)
        pints = []
        for pint in data['data']:
            pints.append(pint)
        f.close()
        
        with table.batch_writer() as batch:
            for pint in pints:
                time.sleep(500/1000) #ensure we don't blow our throuput allowance
                print('adding pint ' + pint["name"])
                batch.put_item(Item=pint)
        os.remove(file)
        print('')


    print('done')


if __name__ == '__main__':

    parser = argparse.ArgumentParser(description='Dynamo Pint Exporter')
    parser.add_argument("--table",nargs="?",default='pints',help="string, the table to target for the export",type=str)
    parser.add_argument("--data_folder",nargs="?",default='',help="string, the folder where the data json files are located",type=str)
    args=parser.parse_args()
    export(args.table, args.data_folder)
