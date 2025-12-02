using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Dtos
{
    public class NoPayDayDto
    {
        public int? Id { get; set; }

        [Required]
        public int EmployeID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Reason { get; set; }
    }
}
