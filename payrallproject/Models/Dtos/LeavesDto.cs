using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Dtos
{
    public class LeavesDto
    {
        public int? Id { get; set; }

        [Required]
        public int EmployeID { get; set; }

        [Required]
        public int Year { get; set; }
        public double AnnualLeavesAllocated { get; set; }
        public double AnnualLeavesUsed { get; set; }

        public double CasualLeavesAllocated { get; set; }
        public double CasualLeavesUsed { get; set; }

        // Read-only properties for frontend
        public double AnnualLeavesRemaining => AnnualLeavesAllocated - AnnualLeavesUsed;
        public double CasualLeavesRemaining => CasualLeavesAllocated - CasualLeavesUsed;
        public double TotalLeavesRemaining => AnnualLeavesRemaining + CasualLeavesRemaining;
    }

    public class LeaveRequestDto
    {
        [Required]
        public int EmployeID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsHalfDay { get; set; }

        public string Reason { get; set; }

        [Required]
        public string LeaveType { get; set; } // "Annual" or "Casual"
    }

    public class LeaveBalanceDto1
    {
        public int EmployeID { get; set; }
        public int Year { get; set; }
        public double AnnualLeavesAllocated { get; set; }
        public double AnnualLeavesUsed { get; set; }
        public double AnnualLeavesRemaining { get; set; }
        public double CasualLeavesAllocated { get; set; }
        public double CasualLeavesUsed { get; set; }
        public double CasualLeavesRemaining { get; set; }
        public double TotalLeavesRemaining { get; set; }
    }
}