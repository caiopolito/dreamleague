namespace dreamleague.domain.Infrastructure
{
    public interface IAzureBlobStorageRepository
    {
        Task UploadJsonFileAsync(object obj, string fileName);
    }
}
