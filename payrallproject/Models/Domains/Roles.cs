using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Domains
{
    public class Roles
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
