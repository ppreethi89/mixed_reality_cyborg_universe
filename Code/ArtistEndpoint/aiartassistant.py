
import asyncio
import uuid
import os
from ImageCrawler import ImageCrawler
from azure.storage.blob import BlobServiceClient, BlobClient, ContainerClient
from enum import Enum
from datetime import date
from flask import Flask
from flask import request
from flask import jsonify
from flask import json
import io
import aiohttp
import ast
import asyncio
myapp = Flask(__name__)


class MediaType(Enum):
    TEXT_FILES = 1
    EMAIL = 2
    TWEETS = 3
    IMAGES = 4
    VIDEOS = 5
    FACEBOOK = 6
    WEB = 7
    AUDIO = 8


content_location = {
    'media_type': MediaType,
    'container_path': 'http://server/folder',
    'security_token': 'token1'
}
digital_footprint = [content_location, content_location]


# class ImageCrawler:
#   def __init__(self, theme, image_index_location):
#     #Jerry's theme matching code goes here
#     self.theme = theme
#     self.image_index_location = image_index_location
#     pass

#   def matching_image_locations(self, matching_threshold):
#     #convert the theme to topics
#     #find images that match those topics
#     #return the found image locations
#     imageList=[]
#     imageList={"https://cyborguniverse.blob.core.windows.net/thumbnails/Geneva_after_Megeve_039.JPG?sp=rwl&st=2020-07-21T17:47:07Z&se=2020-08-22T17:47:00Z&sv=2019-10-10&sr=b&sig=%2FICiV7sz5VGdHhO5DyrdKgVDlOelu61b3hGHehExM5Q%3D","https://cyborguniverse.blob.core.windows.net/thumbnails/Geneva_after_Megeve_045.JPG?sp=rcl&st=2020-07-21T17:50:28Z&se=2020-08-22T17:50:00Z&sv=2019-10-10&sr=b&sig=Lc%2BrXIkhsHzCVkQC65O%2FRr3X2bn6BVXS7hfXP%2F2YWfc%3D"}
#     return imageList
#     # ['https://cyborguniverse.blob.core.windows.net/thumbnails/Geneva%20after%20Megeve%20039.JPG?sv=2019-10-10&ss=bqtf&srt=sco&sp=rwdlacuptfx&se=2020-06-26T13:59:04Z', 'https://cyborguniverse.blob.core.windows.net/thumbnails/Megeve%20(2).JPG?sv=2019-10-10&ss=bqtf&srt=sco&sp=rwdlacuptfx&se=2020-06-26T13:39:37Z', 'https://cyborguniverse.blob.core.windows.net/thumbnails/Megeve(3).JPG?sv=2019-10-10&ss=bqtf&srt=sco&sp=rwdlacuptfx&se=2020-07-15T08:59:05Z&sig=92aKHBOVhtPAxqQHyc%2F1xe0BW%2BPZxTjUCU1GQzUQm50%3D&_=1594774827827']

class ArtCreator:
    # Jerry's art creation code goes here
    def __init__(self, art_creation_location, composition_image_location, color_image_location, sytle_image_location, transfer_people_from_composition_image, photomosaic):
        pass

    def create_art(self):
        # exec(open('applystyle.py').read())
        return 'https://cyborguniverse.blob.core.windows.net/thumbnails/Geneva_after_Megeve_039.JPG?sp=rwl&st=2020-07-21T17:47:07Z&se=2020-08-22T17:47:00Z&sv=2019-10-10&sr=b&sig=%2FICiV7sz5VGdHhO5DyrdKgVDlOelu61b3hGHehExM5Q%3D'


class AIArtistAssitant:
    def __init__(self, digital_footprint, working_repository, style_images_location, color_images_location):
        self.digital_footprint = digital_footprint
        self.__working_repository = working_repository
        self.__image_index_location = working_repository + '/image_index/'
        self.__topic_model_location = working_repository + '/topic_model/'

    def accept_theme(self, theme, matching_threshold=.5):
        # compare the image index to the theme
        image_crawler = ImageCrawler(theme, self.__image_index_location)

        # get a list of images that match
        image_locations = image_crawler.matching_image_locations(
            matching_threshold)

        return image_locations

    def create_art(self, composition_image_location, color_image_location, sytle_image_location, transfer_people_from_composition_image=False, photomosaic=False):
        art_creation_location = "???"  # TODO: this needs to be defined
        art = ArtCreator(art_creation_location, composition_image_location, color_image_location,
                         sytle_image_location, transfer_people_from_composition_image, photomosaic)
        art_location = art.create_art()
        return art_location


cyborg_artist = AIArtistAssitant


@myapp.route("/")
def aiartassistant():
    # create and host the cyborg artist assistant
    working_repository = "dummy_working_rep"
    style_images_repo_location = "dummy_style_images_repo_location"
    color_images_repo_location = "dummy_color_images_repo_location"
    cyborg_artist = AIArtistAssitant(
        digital_footprint, working_repository, style_images_repo_location, color_images_repo_location)
    return "Setup the initial variables"

# @myapp.route("/get_potential_composition_images")
# def potential_composition_images():
#     working_repository = "dummy_working_rep"
#     style_images_repo_location = "dummy_style_images_repo_location"
#     color_images_repo_location = "dummy_color_images_repo_location"
#     cyborg_artist = AIArtistAssitant(digital_footprint, working_repository, style_images_repo_location, color_images_repo_location)
#     theme = 'This is my theme'
#     threshold = .99
#     potential_composition_images = cyborg_artist.accept_theme(theme, threshold)
#     return str(potential_composition_images)
# connect to the assistant and give it a theme


@myapp.route("/get_final_art")
def final_art():
    composition_image_location = "dummy_img1_loc"
    color_image_location = "dummy_img2_loc"
    sytle_image_location = "dummy_img3_loc"
    transfer_people_from_composition_image = True
    photomosaic = True
    final_art = cyborg_artist.create_art(composition_image_location, color_image_location,
                                         sytle_image_location, transfer_people_from_composition_image, photomosaic)

    # print("Location of final art")
    # print(final_art)
    return final_art


@myapp.route("/create_final_art")
def test_final_art():
    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(connect_str)
    container_name = "create-art-requests"
    local_path = "./data"
    local_file_name = "request-" + str(uuid.uuid4()) + ".txt"
    upload_file_path = os.path.join(local_path, local_file_name)

    # Write text to the file
    file = open(upload_file_path, 'w')

    file.write(str(request.args))
    file.close()

    # Create a blob client using the local file name as the name for the blob
    blob_client = blob_service_client.get_blob_client(
        container=container_name, blob=local_file_name)

    print("\nUploading to Azure Storage as blob:\n\t" + local_file_name)

    # Upload the created file
    with open(upload_file_path, "rb") as data:
        blob_client.upload_blob(data)
    return "file uploaded"


@myapp.route('/get_potential_composition_images', methods=['GET'])
def composition_images():
    theme = str(request.args.get('theme'))
    if theme == "None":
        return "No theme entered"

    crawler = ImageCrawler(theme)
    image_locations = crawler.matching_image_locations(matching_threshold=0)
    return image_locations


@myapp.route('/push_new_notifications', methods=['POST'])
def new_notifications():
    new_request = request.get_json()
    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(connect_str)
    container_name = "notifications"
    local_path = "./notifications"
    local_file_name = "notification-" + str(uuid.uuid4()) + ".txt"
    upload_file_path = os.path.join(local_path, local_file_name)

    # Write text to the file
    file = open(upload_file_path, 'w')
    notific = {}
    # notific[str(uuid.uuid4())] = new_request
    notific['source'] = new_request['source']
    notific['subject'] = new_request['subject']
    notific['content'] = new_request['content']
    file.write(str(notific))
    file.close()

    # Create a blob client using the local file name as the name for the blob
    blob_client = blob_service_client.get_blob_client(
        container=container_name, blob=local_file_name)

    print("\nUploading to Azure Storage as blob:\n\t" + local_file_name)

    # Upload the created file
    with open(upload_file_path, "rb") as data:
        blob_client.upload_blob(data)
    os.remove(upload_file_path)
    return "notification uploaded"


@myapp.route('/accessToken', methods=['POST'])
def accessToken():
    new_request = request.get_json()
    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(connect_str)
    container_name = "tokenstore"
    local_path = "./tokenstore"
    local_file_name = "token.txt"
    upload_file_path = os.path.join(local_path, local_file_name)

    # Write text to the file
    file = open(upload_file_path, 'w')
    token = {}
    # notific[str(uuid.uuid4())] = new_request
    token['refreshToken'] = new_request['accessToken']

    file.write(str(token))
    file.close()

    # Create a blob client using the local file name as the name for the blob
    blob_client = blob_service_client.get_blob_client(
        container=container_name, blob=local_file_name)

    print("\nUploading to Azure Storage as blob:\n\t" + local_file_name)

    # Upload the created file
    with open(upload_file_path, "rb") as data:
        blob_client.upload_blob(data, overwrite=True)
    os.remove(upload_file_path)
    return "token uploaded"


@myapp.route('/refreshToken', methods=['GET'])
def get_token():

    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(
        connect_str)

    local_path = "./get_token"
    token_container_client = blob_service_client.get_container_client(
        "tokenstore")
    blob_list = token_container_client.list_blobs()
    token_items = {}
    total_read = 0
    for blob in blob_list:

        blob_data = token_container_client.download_blob(
            blob)
        with open("get_token.txt", "wb") as token:
            blob_data.readinto(token)

    with open("get_token.txt", "r+") as notification_list:
        lines = notification_list.readlines()
        for line in lines:
            # json_token = json.loads(line)
            token_items = line
            # print(line[1])

    return jsonify(token_items)


@myapp.route('/get_recent_notification', methods=['GET'])
def get_notifications():
    new_request = request.get_json()
    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(
        connect_str)
    source = "all"

    local_path = "./get_notifications"
    try:
        source = request.args.get('source')
    except:
        source = "all"
    notifiction_container_client = blob_service_client.get_container_client(
        "notifications")
    blob_list = notifiction_container_client.list_blobs()
    notification_items = []
    total_read = 0
    for blob in blob_list:
        if total_read <= 10:
            print("\t" + blob.name)
            blob_data = notifiction_container_client.download_blob(
                blob)
            with open("get_notifications.txt", "ab") as notification_list:
                blob_data.readinto(notification_list)
                notification_list.write('\n'.encode('utf-8'))
            total_read += 1
            notifiction_container_client.delete_blob(blob)

    with open("get_notifications.txt", "r+") as notification_list:
        lines = notification_list.readlines()
        for line in lines:
            # if json(lines)['source']
            source = ast.literal_eval(line)['source']
            notification_items.append(line)
        notification_list.truncate(0)

    return jsonify(notification_items)


@myapp.route('/update_index')
def update_index():
    # Using connection string for blob service client
    blob_service_client = BlobServiceClient.from_connection_string(
        "DefaultEndpointsProtocol=https;AccountName=cyborguniverse;AccountKey=Bsu0QC1pGIdHIG6OJ9W4vHz3oN68M9L8QI4crjGuz6sBOJZ5KoAp8UEE/XM86Ot7T62e3vSJCGGmHxzFFxTlpQ==;EndpointSuffix=core.windows.net")

    # Instantiate a ContainerClient for each folder
    container_client_topic_model = blob_service_client.get_container_client(
        "ai-artist-assistant-obj/topic_model")
    container_client_image_index = blob_service_client.get_container_client(
        "ai-artist-assistant-obj/image_index")

    # Downloads the image indexer, topic model and dictionary and writes them to local files
    with open("image_index.pkl", "wb") as my_imagindex, open("lda_model.pkl", "wb") as my_ldamodel, open("dictionary.pkl", "wb") as my_dictionary:
        blob_data = container_client_image_index.download_blob(
            "image_index.pkl")
        blob_data_2 = container_client_topic_model.download_blob(
            "lda_model.pkl")
        blob_data_3 = container_client_topic_model.download_blob(
            "dictionary.pkl")
        blob_data.readinto(my_imagindex)
        blob_data_2.readinto(my_ldamodel)
        blob_data_3.readinto(my_dictionary)
    return "pass"
# give the assistant the selections made in the UI and create art
