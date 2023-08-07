using Azure.Storage.Blobs;
using dreamleague.domain.Infrastructure;
using dreamleague.shared.Configurations;

namespace dreamleague.infrastructure.Repositories.Azure
{
    public class AzureBlobStorageRepository : IAzureBlobStorageRepository
    {
        private readonly BlobContainerClient blobContainerClient;
        public AzureBlobStorageRepository
            (
                ApplicationConfig appConfig
            )
        {
            blobContainerClient = new BlobContainerClient(appConfig.ConnectionStrings.BlobStorageConnection, "matches");
        }
        public async Task UploadJsonFileAsync(object obj, string fileName)
        {
            await blobContainerClient.UploadBlobAsync(fileName + ".json", new BinaryData(obj));
        }
    }
}
