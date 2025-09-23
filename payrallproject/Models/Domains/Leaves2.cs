using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class Leaves2
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EmployeID { get; set; }
        [ForeignKey(nameof(EmployeID))]
        public Employe Employe { get; set; }
        [Required]
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
