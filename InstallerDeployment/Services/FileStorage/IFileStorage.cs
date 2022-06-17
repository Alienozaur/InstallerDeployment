namespace InstallerDeployment.Services.FileStorage;

public interface IFileStorage
{
    Task<IEnumerable<string>> List();
    Task Add(Stream fileStream, string name);
    Task<Stream> Get(string name);
}