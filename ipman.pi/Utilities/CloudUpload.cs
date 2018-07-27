using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ipman.pi.Utilities
{
    public static class CloudUpload
    {
        private const string _storageConnectionString = @"DefaultEndpointsProtocol=https;AccountName=robfunctionstorage;AccountKey=KrRmLOidXY7C8zMolkzBceJ4HFk2PTgOrMV1ug7s8Pwzyt8S2n6lfIBUwZc1UkLcqdgTT4WXtgsiCT62N2QGAg==;EndpointSuffix=core.windows.net";
        private const string _imageContainerName = "pi-images";
        public const string IMAGE_NAME = "image.jpeg";
        public static async Task<string> UploadPostImage(Guid postID, byte[] imageData)
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            string sourceFile = null;
            string destinationFile = null;

            if (CloudStorageAccount.TryParse(_storageConnectionString, out storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    cloudBlobContainer = cloudBlobClient.GetContainerReference(_imageContainerName);

                    await cloudBlobContainer.CreateIfNotExistsAsync();

                    // Set the permissions so the blobs are public. 
                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };

                    await cloudBlobContainer.SetPermissionsAsync(permissions);

                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference($"{postID}/{IMAGE_NAME}");

                    await cloudBlockBlob.UploadFromByteArrayAsync(imageData, 0, imageData.Length);

                    return cloudBlockBlob.Uri.ToString();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
            }
        }
    }
}
