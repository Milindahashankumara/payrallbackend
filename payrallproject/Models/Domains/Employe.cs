using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Domains
{
    public class Employe
    {
        [Key]
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nic { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
