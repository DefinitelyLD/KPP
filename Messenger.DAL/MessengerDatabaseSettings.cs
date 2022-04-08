namespace Messenger.DAL
{
    public class MessengerDatabaseSettings : IMessengerDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
