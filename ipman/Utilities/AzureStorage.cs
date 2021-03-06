using IPMan.Utilities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ipman.Utilities
{
    public static class AzureStorage
    {
        private const string AZURE_STORAGE_CONNECTION_STRING = "AzureStorageConnectionString";
        private const string _imageContainerName = "pi-images";
        public const string IMAGE_NAME = "image.jpeg";
        public static async Task<bool> DeletePostImage(Guid postID)
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            string sourceFile = null;
            string destinationFile = null;
            string storageConnectionString = ConfigurationService.Configuration[AZURE_STORAGE_CONNECTION_STRING];
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    cloudBlobContainer = cloudBlobClient.GetContainerReference(_imageContainerName);

                    bool exists = await cloudBlobContainer.ExistsAsync();
                    if (!exists)
                        return false;

                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference($"{postID}/{IMAGE_NAME}");

                    return await cloudBlockBlob.DeleteIfExistsAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                    return false;
                }
            }
            else
                throw new Exception("Storage Account Does not exist.");
        }
    }
}
