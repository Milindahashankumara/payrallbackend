using payrallproject.Models.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Dtos
{
    public class EmployeeOvertimeDto
    {
        public int? Id { get; set; }
        public int? EmployeId { get; set; }
        public int? OtId { get; set; }
        public DateTime? DateWorked { get; set; }
        public int? HoursWorked { get; set; }
        public string? EmployeeName { get; set; }
        public string? OTType { get; set; }
        public string? Remarks { get; set; }
        public decimal? Amount { get; set; }
    }
}
