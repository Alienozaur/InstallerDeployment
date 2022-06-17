using Azure.Storage.Blobs;

namespace InstallerDeployment.Services.FileStorage;

public class BlobStorage : IFileStorage
{
    private readonly BlobContainerClient _blobClient;

    private readonly string _connectionString = "";
    private readonly string _containerName = "installers";
    public BlobStorage()
    {
        _blobClient = new BlobContainerClient(_connectionString, _containerName);
        if (!_blobClient.Exists().Value)
            _blobClient.Create();
    }
    public async Task<IEnumerable<string>> List()
    {
        return _blobClient.GetBlobs().Select(blob => blob.Name).ToList();
    }

    public async Task Add(Stream fileStream, string name)
    {
        var response = await _blobClient.UploadBlobAsync(name, fileStream);
    }

    public async Task<Stream> Get(string name)
    {
        return await _blobClient.GetBlobClient(name).OpenReadAsync();
    }
}