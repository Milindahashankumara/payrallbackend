using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Domains
{
    public class Loans
    {
        [Key]
        public int? Id { get; set; }
        public int? EmployeID { get; set; }
        public int? get { get; set; }
    }
}
