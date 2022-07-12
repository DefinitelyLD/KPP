namespace Messenger.BLL.UserAccounts
{
    public class UserAccountUpdateModel
    {
        public int Id { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}
