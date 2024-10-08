using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraZoneAPI.DataAccess.Models
{
    [Table("Video")]
    public class Video
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Url]
        public string Url { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Category { get; set; } = string.Empty;
        [Url]
        public string? ThumbnailUrl { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [Required]
        public User User { get; set; } = null!;
        public ICollection<Comment>? Comments { get; set; } = null!;

    }
}
