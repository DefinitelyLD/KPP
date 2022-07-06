namespace Messenger.BLL.UserAccounts
{
    public class UserAccountCreateModel
    {
        public int ChatId { get; set; } 
        public string UserId { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}
