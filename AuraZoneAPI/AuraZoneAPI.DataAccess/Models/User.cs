using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraZoneAPI.DataAccess.Models
{
    [Table("User")]
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHashed { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Video> Videos { get; set; } = new List<Video>();
        public ICollection<Comment> Comments { get; set; } = null!;
    }
}
