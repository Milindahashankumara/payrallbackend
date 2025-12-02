using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class NoPayDay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeID { get; set; }

        [ForeignKey(nameof(EmployeID))]
        public Employe Employe { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Reason { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
