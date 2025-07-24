using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class PasswordResetTokens
    {
        [Key]
        public int? Id { get; set; }
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
