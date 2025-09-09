using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Domains
{
    public class OT
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Rate { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
