using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class Loans
    {
        [Key]
        public int? Id { get; set; }
        public int? EmployeID { get; set; }
        public Employe Employe { get; set; }
        [ForeignKey(nameof(EmployeID))]
        public decimal PrincipalAmount { get; set; }
        public int TermMonths { get; set; }

        public decimal MonthlyInstallment { get; set; }

        public decimal RemainingBalance { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Settled { get; set; } = true;
    }
}
