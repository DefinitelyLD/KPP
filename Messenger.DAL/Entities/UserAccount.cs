namespace Messenger.DAL.Entities
{
    public class UserAccount : BaseEntity<int>
    {
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
        public bool IsLeft { get; set; } = false;
    }
}
