namespace Messenger.BLL.Users
{
    public class UserChangePasswordModel
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
