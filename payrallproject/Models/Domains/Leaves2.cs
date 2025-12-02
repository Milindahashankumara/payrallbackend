using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class Leaves2
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsHalfDay { get; set; } = false;

        public bool? IsFirstHalfDay { get; set; } // true: First half, false: Second half, null: Full day

        [Required]
        [Column(TypeName = "decimal(3,1)")]
        public decimal NumberOfDays { get; set; } // 1.0, 0.5, 2.0, etc.

        [Required]
        [StringLength(20)]
        public string LeaveType { get; set; } = "Casual"; // "Annual", "Casual"

        [StringLength(500)]
        public string? Reason { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        [Required]
        public int Year { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        [ForeignKey("EmployeeId")]
        public virtual Employe Employee { get; set; } = null!;
    }
}
