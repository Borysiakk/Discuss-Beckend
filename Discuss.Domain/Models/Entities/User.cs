namespace Discuss.Domain.Models.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string ClientCommunicationHubId { get; set; }
        public bool Status { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}