namespace Messenger.WEB.Models
{
    public interface IMessengerDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
