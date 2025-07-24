using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class UserRoles
    {
        [Key]
        public int? Id { get; set; }
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int? RolesId { get; set; }

        [ForeignKey(nameof(RolesId))]
        public Roles Roles { get; set; }
    }
}
