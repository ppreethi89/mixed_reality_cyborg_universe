
from flask import Flask
from flask import request
from flask_mail import Mail
from flask_mail import Message
from flask import jsonify
import os
import uuid
from azure.storage.blob import BlobServiceClient, BlobClient, ContainerClient
import pandas as pd
from io import StringIO
import requests
import string
import datetime
import time


app = Flask(__name__)


# Azure storage account details
storageAccountName = "cyborguniverse"
storageAccountKey = "Bsu0QC1pGIdHIG6OJ9W4vHz3oN68M9L8QI4crjGuz6sBOJZ5KoAp8UEE/XM86Ot7T62e3vSJCGGmHxzFFxTlpQ=="

containerName = "medic"
blob = "oximeter-readings.csv"


# confiure email server and network credentials

app.config['DEBUG'] = True
app.config['MAIL_SERVER'] = 'smtp.office365.com'
app.config['MAIL_PORT'] = 587
app.config['MAIL_USERNAME'] = "cyborg.psm@outlook.com"
app.config['MAIL_PASSWORD'] = "aaaaaaaaaa"
app.config['MAIL_USE_TLS'] = True
app.config['MAIL_USE_SSL'] = False


mail = Mail(app)


# Make the WSGI interface available at the top level so wfastcgi can get it.
wsgi_app = app.wsgi_app


@app.route('/')
def home():

    print('Medic end point')

    return "This is home page"


@app.route('/uploadReadings', methods=['GET','POST'])
def uploadReadings():

    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(connect_str)
    container_name = "medic"
    local_path = "./data"
    local_file_name = "oximeter-readings.txt"
    upload_file_path = os.path.join(local_path, local_file_name)

    blob_client = blob_service_client.get_blob_client(
        container=container_name, blob=local_file_name)

    print("\nUploading to Azure Storage as blob:\n\t" + local_file_name)

    # Upload the created file
    with open(upload_file_path, "rb") as data:
        blob_client.upload_blob(data, overwrite=True)
    os.remove(upload_file_path)
    return "file uploaded"


@app.route('/getReadings', methods=['GET'])
def get_readings():

    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')

    blob_service_client = BlobServiceClient.from_connection_string(
        connect_str)

    local_path = "./get_readings"

    token_container_client = blob_service_client.get_container_client(
        "medic-o2-readings")

    blob_list = token_container_client.list_blobs()

    #blob_service = BlockBlobService(account_name=storageAccountName, account_key=storageAccountKey)

    # streaming the blob into a pandas dataframe
    #blobstring = blob_service_client.get_blob_to_text(containerName, blob).content

    token_items = {}

    for blob in blob_list:

        blob_data = token_container_client.download_blob(
            blob)
        with open("oximeter-readings.txt", "wb") as token:
            blob_data.readinto(token)

    with open("oximeter-readings.txt", "r+") as o2reading:

        lines = o2reading.readlines()

        for line in lines:
            
            token_items = line
            

    return jsonify(token_items)


@app.route('/saveToBlob', methods=['GET','POST'])

def save_to_blob():

    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(connect_str)
    container_name = "medic-o2-readings"
    local_path = "./data"

    date_time = time.strftime("%Y%m%d-%H%M%S")

    file_name = date_time + "oximeter-readings" + ".txt"

    upload_file_path = os.path.join(local_path, file_name)

    blob_client = blob_service_client.get_blob_client(
        container=container_name, blob=file_name)

    print("\nUploading to Azure Storage as blob:\n\t" + file_name)

    # save the file

    with open(upload_file_path, "rb") as data:
        blob_client.upload_blob(data, overwrite=True)
    os.remove(upload_file_path)

    return "file saved successfully!"


@app.route('/getLastValue', methods=['GET'])
def get_last_value():
    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(
        connect_str)

    local_path = "./get_last_value"
    token_container_client = blob_service_client.get_container_client(
        "medic-last-value")
    blob_list = token_container_client.list_blobs()
    token_items = {}
    total_read = 0
    for blob in blob_list:

        blob_data = token_container_client.download_blob(
            blob)
        with open("last_value.txt", "wb") as token:
            blob_data.readinto(token)

    with open("last_value.txt", "r+") as lastvalue:
        lines = lastvalue.readlines()
        for line in lines:
            # json_token = json.loads(line)
            token_items = line
            # print(line[1])

    return jsonify(token_items)


@app.route('/send_email', methods=['GET', 'POST'])
def send_email():

    mail_From = "cyborg.psm@outlook.com"

    mail_To = "cyborg.psm@outlook.com"

    Subject = "Test Email for Medic"

    Body = "Testing email for Medic"

    message = Message(Subject, sender=mail_From, recipients=[mail_To])
    message.body = Body
    mail.send(message)

    return "Email triggered successfully!"


if __name__ == '__main__':

    #HOST = os.environ.get('SERVER_HOST', 'localhost')
    # try:
    #PORT = int(os.environ.get('SERVER_PORT', '5555'))
    # except ValueError:
    # PORT = 5555
    app.run()
