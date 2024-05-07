namespace AuthSys.Models.Domain
{
    public class Responsables
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime DateOfBirdth { get; set; }
        public string NameEntreprise { get; set; }
    }
}
