using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class NoPayEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime NoPayDate { get; set; }

        [StringLength(500)]
        public string? Reason { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("EmployeeId")]
        public virtual Employe Employee { get; set; } = null!;
    }
}
