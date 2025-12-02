using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class EmployeeOvertime
    {
        [Key]
        public int? Id { get; set; }
        public int? EmployeId { get; set; }
        [ForeignKey(nameof(EmployeId))]
        public Employe Employe { get; set; }
        public int? OtId { get; set; }
        [ForeignKey(nameof(OtId))]
        public OT OT { get; set; }
        public DateTime? DateWorked { get; set; }
        public int? HoursWorked { get; set; }
        public string? Remarks { get; set; }
        public decimal? Amount { get; set; }
    }
}
