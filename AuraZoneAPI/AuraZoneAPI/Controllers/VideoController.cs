using AuraZoneAPI.Services;
using AuraZoneAPI.DataAccess.Models;
using AuraZoneAPI.Streaming.Storage;
using Microsoft.AspNetCore.Mvc;

namespace AuraZoneAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IUserRepository _userRepository;
        private readonly StorageService _storageService = new();
        private readonly string _storagePath = "../../../../AuraZone-Data/Videos/";
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
            if (!_videoRepository.VideoExists(id))
            {
                return NotFound("Video not found.");
            }
            Video video = _videoRepository.GetVideoById(id);
            return Ok(video);
        }
        [HttpGet("{userId:guid}/videos")]
        public IActionResult GetAllUserVideos(Guid userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound("User not found.");
            }
            IQueryable<Video> videos = _videoRepository.GetAllUserVideos(userId);
            return Ok(videos);
        }
        [DisableRequestSizeLimit]
        [HttpPost("{userId}, {name}, {category}")]
        public async Task<IActionResult> AddVideo(Guid userId, string name, IFormFile videoFile, string category, string? thumbnailUrl, string? description)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound("User is not found");
            }

            User user = _userRepository.GetById(userId) ?? throw new ArgumentException("User not found", nameof(userId));

            if (videoFile == null || videoFile.Length == 0)
            {
                return BadRequest("No video file was uploaded");
            }

            string[] allowedExtensions = { ".mp4", ".avi", ".mkv" };
            string fileExtension = Path.GetExtension(videoFile.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Only .mp4, .avi, .mkv video files are allowed.");
            }

            Guid id = Guid.NewGuid();
            var fileName = Path.Combine(_storagePath, id.ToString());

            await _storageService.Upload(videoFile, fileName);

            Video newVideo = new()
            {
                Id = id,
                Name = name,
                Category = category,
                ThumbnailUrl = thumbnailUrl,
                Description = description,
                CreatedAt = DateTime.UtcNow,
                User = user
            };

            if (newVideo == null)
            {
                return BadRequest("Video is NULL");
            }
            
            _videoRepository.AddVideo(userId, newVideo);
            _userRepository.AddUserVideo(user, newVideo);
            return Ok(newVideo);
        }
        [HttpDelete()]
        public IActionResult DeleteVideo(Guid userId, Guid videoId)
        {
            if (_videoRepository.VideoExists(videoId))
            {
                _videoRepository.DeleteVideo(userId, videoId);
            }
            else
            {
                return NotFound("Video not found");
            }
            return Ok(null);
        }

    }
}
