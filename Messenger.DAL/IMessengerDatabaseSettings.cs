namespace Messenger.DAL
{
    public interface IMessengerDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
