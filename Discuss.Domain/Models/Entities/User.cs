namespace Discuss.Domain.Models.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}