namespace Messenger.WEB.Models
{
    public class MessengerDatabaseSettings : IMessengerDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
