namespace InstallerDeployment.Services.FileStorage;

public class LocalStorage : IFileStorage
{
    public async Task<IEnumerable<string>> List()
    {
        var files = new DirectoryInfo("./files").EnumerateFiles();
        return files.Select(x => x.Name).ToList();
    }

    public async Task Add(Stream fileStream, string name)
    {
        using var reader = new StreamReader(fileStream);
        await File.WriteAllTextAsync($"./files/{name}.msi", await reader.ReadToEndAsync());
    }

    public async Task<Stream> Get(string name)
    {
        return File.OpenRead($"./files/{name}.msi");
    }
}