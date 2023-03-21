import boto3
from botocore import client
import requests
import uuid

def lambda_handler(event, context):

   bucket = 'pintmenus'
   region = 'us-east-1'

   extension = event['extension']
   fileId = str(uuid.uuid4())
   key = fileId + extension

   s3Client = boto3.client('s3', config=client.Config(signature_version='s3v4'))
   url = s3Client.generate_presigned_url(
      ClientMethod='put_object',
      Params={
         'Bucket': bucket,
         'Key': key
      },
      ExpiresIn=3600 # in seconds
   )

   return {
      'url': url,
      'key': key
   }