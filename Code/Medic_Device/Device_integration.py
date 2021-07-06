"""
Service Explorer
----------------

An example showing how to access and print out the services, characteristics and
descriptors of a connected GATT server.

Created on 2019-03-25 by hbldh <henrik.blidh@nedomkull.com>

"""
import platform
import asyncio
import logging
import schedule
import time
import os
import uuid
from azure.storage.blob import BlobServiceClient, BlobClient, ContainerClient
from bleak import BleakClient

Table_CRC8 = [ 0x00, 0x07, 0x0E, 0x09, 0x1C,
			0x1B, 0x12, 0x15, 0x38, 0x3F, 0x36, 0x31, 0x24, 0x23, 0x2A, 0x2D,
			0x70, 0x77, 0x7E, 0x79, 0x6C, 0x6B, 0x62, 0x65, 0x48, 0x4F, 0x46,
			0x41, 0x54, 0x53, 0x5A, 0x5D, 0xE0, 0xE7, 0xEE, 0xE9, 0xFC, 0xFB,
			0xF2, 0xF5, 0xD8, 0xDF, 0xD6, 0xD1, 0xC4, 0xC3, 0xCA, 0xCD, 0x90,
			0x97, 0x9E, 0x99, 0x8C, 0x8B, 0x82, 0x85, 0xA8, 0xAF, 0xA6, 0xA1,
			0xB4, 0xB3, 0xBA, 0xBD, 0xC7, 0xC0, 0xC9, 0xCE, 0xDB, 0xDC, 0xD5,
			0xD2, 0xFF, 0xF8, 0xF1, 0xF6, 0xE3, 0xE4, 0xED, 0xEA, 0xB7, 0xB0,
			0xB9, 0xBE, 0xAB, 0xAC, 0xA5, 0xA2, 0x8F, 0x88, 0x81, 0x86, 0x93,
			0x94, 0x9D, 0x9A, 0x27, 0x20, 0x29, 0x2E, 0x3B, 0x3C, 0x35, 0x32,
			0x1F, 0x18, 0x11, 0x16, 0x03, 0x04, 0x0D, 0x0A, 0x57, 0x50, 0x59,
			0x5E, 0x4B, 0x4C, 0x45, 0x42, 0x6F, 0x68, 0x61, 0x66, 0x73, 0x74,
			0x7D, 0x7A, 0x89, 0x8E, 0x87, 0x80, 0x95, 0x92, 0x9B, 0x9C, 0xB1,
			0xB6, 0xBF, 0xB8, 0xAD, 0xAA, 0xA3, 0xA4, 0xF9, 0xFE, 0xF7, 0xF0,
			0xE5, 0xE2, 0xEB, 0xEC, 0xC1, 0xC6, 0xCF, 0xC8, 0xDD, 0xDA, 0xD3,
			0xD4, 0x69, 0x6E, 0x67, 0x60, 0x75, 0x72, 0x7B, 0x7C, 0x51, 0x56,
			0x5F, 0x58, 0x4D, 0x4A, 0x43, 0x44, 0x19, 0x1E, 0x17, 0x10, 0x05,
			0x02, 0x0B, 0x0C, 0x21, 0x26, 0x2F, 0x28, 0x3D, 0x3A, 0x33, 0x34,
			0x4E, 0x49, 0x40, 0x47, 0x52, 0x55, 0x5C, 0x5B, 0x76, 0x71, 0x78,
			0x7F, 0x6A, 0x6D, 0x64, 0x63, 0x3E, 0x39, 0x30, 0x37, 0x22, 0x25,
			0x2C, 0x2B, 0x06, 0x01, 0x08, 0x0F, 0x1A, 0x1D, 0x14, 0x13, 0xAE,
			0xA9, 0xA0, 0xA7, 0xB2, 0xB5, 0xBC, 0xBB, 0x96, 0x91, 0x98, 0x9F,
			0x8A, 0x8D, 0x84, 0x83, 0xDE, 0xD9, 0xD0, 0xD7, 0xC2, 0xC5, 0xCC,
			0xCB, 0xE6, 0xE1, 0xE8, 0xEF, 0xFA, 0xFD, 0xF4, 0xF3 ]

def calCRC8(buf):
    crc=0
    input=bytearray(buf)
    print(input)
    print(input[0])
    for i in buf:
        # print(bytes([i]))
        x=crc ^ i
        # crc=Table_CRC8[x]
        # print(Table_CRC8[170])
        crc=Table_CRC8[0x00ff & (crc ^ i)]
    return crc



def notification_handler(sender, data):
    """Simple notification handler which prints the data received."""
    # print('data received')
    print("{0}: {1}".format(sender, data))
    # output=bytearray(data)
    # for byte in output:
    #     print(byte)

def real_time_handler(sender, data):
    """Simple notification handler which prints the data received."""
    print('\nReal time data {0}', data)
    output=bytearray(data)
    # save_to_db(output[7])
    try:
        print("ACK -->{0}", output[0])
        print("SP02 Level -->{0}", output[7])
        save_o2_to_db(output[7])
        print("Pulse Rate -->{0}", output[8])
        save_bpm_to_db(output[8])
    
    except:
        error="error"
    # for byte in output:
    #     print(byte)


async def print_services(mac_addr: str):
    log = logging.getLogger(__name__)
    async with BleakClient(mac_addr) as client:
        svcs = await client.get_services()
        print("Services:", svcs)
        for svc in svcs:
            if "14839ac4-7d7e-415c-9a42-167340cf2339" in str(svc):
                print('service is',svc)
                # buf[0] = 0xAA
                # buf[1] = 0X003
                # buf[2] = ~0x003
                # buf[3] = 0
                # buf[4] = 0
               

                await client.start_notify('0734594a-a8e7-4b1a-a6b1-cd5243059a57',notification_handler)
                value=await client.write_gatt_char('8B00ACE7-EB0B-49B0-BBE9-9AEE0A26E1A3',bytearray([0xAA,0x14,0Xeb,0x00,0x00,0x00,0x00,0xc6]))
                # print("I/O Data Post-Write Value: {0}".format(value))
                # value=await client.write_gatt_char('8B00ACE7-EB0B-49B0-BBE9-9AEE0A26E1A3',bytearray([0xAA,0X04,0xfb,0x00,0xc6,0xc6,0xc6]),True)
                # print("I/O Data Post-Write Value: {0}".format(value))
                # await client.start_notify('0734594a-a8e7-4b1a-a6b1-cd5243059a57',notification_handler)
                # log.info("\n response is".format(response))
                # crc_output=calCRC8(bytearray([0xAA,0x03,0Xfc,0x00,0x00,0x00,0x00]))
                # print("printed output",hex(crc_output))
                # crc_output=calCRC8(bytearray([0xAA,0x17,0Xe8,0x00,0x00,0x00,0x00]))
                # print("printed output",hex(crc_output))
                # crc_output=calCRC8(bytearray([0xAA,0x15,0XEA,0x00,0x00,0x00,0x00]))
                # print("printed output",hex(crc_output))
               
                # await asyncio.sleep(10.0)
                # value=await client.write_gatt_char('8B00ACE7-EB0B-49B0-BBE9-9AEE0A26E1A3',bytearray([0xAA,0x03,0Xfc,0x00,0x00,0x00,0x00,0x9c]))
                # await asyncio.sleep(20.0)
                # value=await client.write_gatt_char('8B00ACE7-EB0B-49B0-BBE9-9AEE0A26E1A3',bytearray([0xAA,0x15,0XEA,0x00,0x00,0x00,0x00,0x8D]))
                # await asyncio.sleep(10.0)
                
                await asyncio.sleep(10.0)
                await client.stop_notify('0734594a-a8e7-4b1a-a6b1-cd5243059a57')
                await client.start_notify('0734594a-a8e7-4b1a-a6b1-cd5243059a57',real_time_handler)
                value=await client.write_gatt_char('8B00ACE7-EB0B-49B0-BBE9-9AEE0A26E1A3',bytearray([0xAA,0x17,0Xe8,0x00,0x00,0x00,0x00,0x1b]))
               
                # print("I/O Data Post-Write Value: {0}".format(value))
                # value=await client.write_gatt_char('8B00ACE7-EB0B-49B0-BBE9-9AEE0A26E1A3',bytearray([0xAA,0X04,0xfb,0x00,0xc6,0xc6,0xc6]),True)
                # print("I/O Data Post-Write Value: {0}".format(value))
                await asyncio.sleep(10.0)
                await client.stop_notify('0734594a-a8e7-4b1a-a6b1-cd5243059a57')
            # else:
            #     print(str(svc))

def save_o2_to_db(oxygen_value):
    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(connect_str)
    container_name = "medic-o2-readings"
    local_path = "./o2_data"
    local_file_name = "readings-" + str(uuid.uuid4()) + ".txt"
    upload_file_path = os.path.join(local_path, local_file_name)

    # Write text to the file
    file = open(upload_file_path, 'w')

    file.write("O2:["+str(oxygen_value)+"]")
    file.close()

    # Create a blob client using the local file name as the name for the blob
    blob_client = blob_service_client.get_blob_client(
        container=container_name, blob=local_file_name)

    print("\nUploading to Azure Storage as blob:\n\t" + local_file_name)

    # Upload the created file
    with open(upload_file_path, "rb") as data:
        blob_client.upload_blob(data)
    return "file uploaded"

def save_bpm_to_db(oxygen_value):
    connect_str = os.getenv('AZURE_STORAGE_CONNECTION_STRING')
    blob_service_client = BlobServiceClient.from_connection_string(connect_str)
    container_name = "medic-bpm-readings"
    local_path = "./bpm_data"
    local_file_name = "readings-" + str(uuid.uuid4()) + ".txt"
    upload_file_path = os.path.join(local_path, local_file_name)

    # Write text to the file
    file = open(upload_file_path, 'w')

    file.write("BPM:["+str(oxygen_value)+"]")
    file.close()

    # Create a blob client using the local file name as the name for the blob
    blob_client = blob_service_client.get_blob_client(
        container=container_name, blob=local_file_name)

    print("\nUploading to Azure Storage as blob:\n\t" + local_file_name)

    # Upload the created file
    with open(upload_file_path, "rb") as data:
        blob_client.upload_blob(data)
    return "file uploaded"

async def run(address, debug=False):
    log = logging.getLogger(__name__)
    if debug:
        import sys

        log.setLevel(logging.DEBUG)
        h = logging.StreamHandler(sys.stdout)
        h.setLevel(logging.DEBUG)
        log.addHandler(h)

    async with BleakClient(address) as client:
        x = await client.is_connected()
        log.info("Connected: {0}".format(x))


        
        for service in client.services:
            if "14839ac4-7d7e-415c-9a42-167340cf2339" in str(service):
                log.info("[Service] {0}: {1}".format(service.uuid, service.description))


                log.info("\t inside ")
                value=await client.write_gatt_char('8B00ACE7-EB0B-49B0-BBE9-9AEE0A26E1A3',bytearray([0xaa,0x03,0xfc,0x00,0x00,0x00,0x00,0xc6]))
                    

                        # value = await client.read_gatt_char(char.uuid)
                print("I/O Data Post-Write Value: {0}".format(value))
                response = await client.start_notify('0734594a-a8e7-4b1a-a6b1-cd5243059a57',notification_handler)
                log.info("\n response is".format(response))


                for char in service.characteristics:
                    if "read" in char.properties:
                        try:
                            value = bytes(await client.read_gatt_char(char.uuid))
                        except Exception as e:
                            value = str(e).encode()
                    else:
                        value = None
                    log.info(
                        "\t[Characteristic] {0}: (Handle: {1}) ({2}) | Name: {3}, Value: {4} ".format(
                            char.uuid,
                            char.handle,
                            ",".join(char.properties),
                            char.description,
                            value,
                        )
                    )
                    for descriptor in char.descriptors:
                        value = await client.read_gatt_descriptor(descriptor.handle)
                        log.info(
                            "\t\t[Descriptor] {0}: (Handle: {1}) | Value: {2} ".format(
                                descriptor.uuid, descriptor.handle, bytes(value)
                            )
                        )
                
                #     if str(char.uuid).upper()=="8B00ACE7-EB0B-49B0-BBE9-9AEE0A26E1A3":
                #         # await client.start_notify(char.uuid, notification_handler)
                #         log.info("\t inside ")
                #         value=await client.write_gatt_char(char.uuid,bytearray([0xaa,0x03,0xfc,0x00,0x00,0x00,0x00,0xc6]))
                    

                #         # value = await client.read_gatt_char(char.uuid)
                #         print("I/O Data Post-Write Value: {0}".format(value))
                #         response = await client.start_notify('0734594a-a8e7-4b1a-a6b1-cd5243059a57',notification_handler)
                #         log.info("\n response is".format(response))

def job():
    print('am printing')
    address = (
        "D9:42:B8:25:91:BC"
        if platform.system() != "Darwin"
        else "189FD214-F4A5-42CC-8FA4-79CBCC572BD5"
    )
    loop = asyncio.get_event_loop()
    loop.set_debug(True)
    loop.run_until_complete(print_services(address))

#idle scenario is to check every 2 hours if unstable. every 4 hours for stable.

if __name__ == "__main__":
    # address = (
    #     "24:71:89:cc:09:05"
    #     if platform.system() != "Darwin"
    #     else "189FD214-F4A5-42CC-8FA4-79CBCC572BD5"
    # )
    # # x=bytearray(b'U\x00\xff\x00\x00\r\x00bE\x00\x00\x00\x00\x00`\x00\x00\x04\x01\x00')

    # # x=bytearray(b'U\x00\xff\x00\x00\r\x00`H\x00\x00\x00\x00\x00_\x00\x02\x04\x01\x00')

    # # x=bytearray(b'U\x00\xff\x00\x00\r\x00ZS\x00\x00\x00\x00\x00_\x00\x01\x04\x01\x00')
    # # for xy in x:

    # #     print(xy)
    # # b'U\x00\xff\x00\x00\x04\x00\x00\x00\x00\x00\xea'.decode("utf-8")
    # loop = asyncio.get_event_loop()
    # loop.set_debug(True)
    # # loop.run_until_complete(run(address, True))
    
    # loop.run_until_complete(print_services(address))

    schedule.every(10).seconds.do(job)


    while 1:
        schedule.run_pending()
        time.sleep(1)




