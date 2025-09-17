using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class Leaves
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeID { get; set; }

        [ForeignKey(nameof(EmployeID))]
        public Employe Employe { get; set; }

        [Required]
        public int Year { get; set; }

        public double AnnualLeavesAllocated { get; set; }
        public double AnnualLeavesUsed { get; set; }

        public double CasualLeavesAllocated { get; set; }
        public double CasualLeavesUsed { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public double AnnualLeavesRemaining => AnnualLeavesAllocated - AnnualLeavesUsed;

        [NotMapped]
        public double CasualLeavesRemaining => CasualLeavesAllocated - CasualLeavesUsed;

        [NotMapped]
        public double TotalLeavesRemaining => AnnualLeavesRemaining + CasualLeavesRemaining;
    }
}