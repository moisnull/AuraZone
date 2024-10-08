using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraZoneAPI.DataAccess.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Required]
        [ForeignKey("VideoId")]
        public Guid VideoId { get; set; }
        public Video Video { get; set; } = null!;
        [Required]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
