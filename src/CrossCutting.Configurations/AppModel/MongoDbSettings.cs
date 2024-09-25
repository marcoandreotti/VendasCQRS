using Domain.Intefaces;

namespace CrossCutting.Configurations.AppModel;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}
