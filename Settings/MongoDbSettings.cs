namespace PersoneApi.Settings;

public class MongoDbSettings
{
    private const string Host = "localhost";
    private const int Port = 27017;
    private const string User = "root";
    private const string Password = "root";
    private const string Database = "people";

    public const string SectionName = "MongoDb";

    public string ConnectionString { get; set; } =
        $"mongodb://{User}:{Password}@{Host}:{Port}";

    public string DatabaseName { get; set; } = Database;
}