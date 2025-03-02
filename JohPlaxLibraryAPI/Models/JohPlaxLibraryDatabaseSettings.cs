namespace JohPlaxLibraryAPI.Models
{
    public class JohPlaxLibraryDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string OrderCollectionName { get; set; } = null!;
        public string BookCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
    }
}
