using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class LeaveBalance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(20)]
        public string LeaveType { get; set; } = "Casual"; // "Annual", "Casual"

        [Required]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal EntitledDays { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal UsedDays { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal BalanceDays { get; set; } = 0;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        [ForeignKey("EmployeeId")]
        public virtual Employe Employee { get; set; } = null!;
    }
}
