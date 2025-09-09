using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class EmployeeOvertime
    {
        [Key]
        public int? Id { get; set; }
        public string? EmployeId { get; set; }
        [ForeignKey(nameof(EmployeId))]
        public User Employe { get; set; }
        public string? OTId { get; set; }
        [ForeignKey(nameof(OTId))]
        public User OT { get; set; }
        public DateTime? DateWorked { get; set; }
        public int? HoursWorked { get; set; }
    }
}
