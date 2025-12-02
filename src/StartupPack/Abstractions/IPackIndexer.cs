namespace StartupPack;

public interface IPackIndexer
{
    int? GetApplicationIndex(string? key);
    int? GetServicesIndex(string? key);
}
