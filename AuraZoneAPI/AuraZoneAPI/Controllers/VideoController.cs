using AuraZoneAPI.Services;
using AuraZoneAPI.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuraZoneAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IUserRepository _userRepository;
        public VideoController(IVideoRepository videoRepository, IUserRepository userRepository)
        {
            _videoRepository = videoRepository;
            _userRepository = userRepository;
        }
        [HttpGet("{category}")]
        public IActionResult GetByCategory(string category)
        {
            var videos = _videoRepository.GetVideosByCategory(category);
            if (videos == null)
            {
                return NotFound();
            }
            return Ok(videos);
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var video = _videoRepository.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }
        [HttpGet("{userId:guid}/videos")]
        public IActionResult GetAllUserVideos(Guid userId)
        {
            var videos = _videoRepository.GetAllUserVideos(userId);
            if (videos == null)
            {
                return BadRequest();
            }
            return Ok(videos);
        }
        [HttpPost]
        public IActionResult AddVideo(Guid userId, string name, string url, string category, string? thumbnailUrl, string? description)
        {
            if (!_userRepository.UserExists(userId))
            {
                return BadRequest("User is not found");
            }
            Video newVideo = new Video
            {
                Id = Guid.NewGuid(),
                Name = name,
                Url = url,
                Category = category,
                ThumbnailUrl = thumbnailUrl,
                Description = description,
                CreatedAt = DateTime.UtcNow,
                User = _userRepository.GetById(userId) ?? throw new ArgumentException("User not found", nameof(userId))
            };
            if (newVideo == null)
            {
                return NotFound("Video is NULL");
            }
            _videoRepository.AddVideo(userId, newVideo);
            return Ok(newVideo);
        }

    }
}
