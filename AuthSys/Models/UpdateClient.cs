namespace AuthSys.Models
{
    public class UpdateClient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime DateOfBirdth { get; set; }
        public string Statut { get; set; }
    }
}
