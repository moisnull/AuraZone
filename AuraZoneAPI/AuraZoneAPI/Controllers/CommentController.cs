using Microsoft.AspNetCore.Mvc;
using AuraZoneAPI.DataAccess.Models;
using AuraZoneAPI.Services;

namespace AuraZoneAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;
        public CommentController(ICommentRepository commentRepository, IUserRepository userRepository, IVideoRepository videoRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _videoRepository = videoRepository;
        }
        [HttpGet("{videoId:guid}")]
        public IActionResult GetAllVideoComments(Guid videoId) 
        {
            IQueryable<Comment> comments = _commentRepository.GetVideoComments(videoId);
            if (comments == null)
            {
                return BadRequest("No comments on video or video's id is incorrect.");
            }
            return Ok(comments);
        }
        [HttpPost("{videoId}, {userId}, {commentContent}")]
        public IActionResult AddComment(Guid videoId, Guid userId, string commentContent)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound("User not found.");
            }
            if (!_videoRepository.VideoExists(videoId))
            {
                return NotFound("Video not found.");
            }
            if (string.IsNullOrEmpty(commentContent))
            {
                return BadRequest("Comment content is null or empty.");
            }
            Video video = _videoRepository.GetVideoById(videoId) ?? throw new ArgumentException("Video not found", nameof(videoId));
            User user = _userRepository.GetById(userId) ?? throw new ArgumentException("User not found", nameof(userId));
            Comment comment = new()
            {
                Id = Guid.NewGuid(),
                Content = commentContent,
                CreatedDate = DateTime.UtcNow,
                VideoId = videoId,
                Video = video,
                UserId = userId,
                User = user
            };
            if (user == null)
            {
                return NotFound("User not found.");
            }
            else if (video == null)
            {
                return NotFound("Video not found.");
            }
            _commentRepository.AddComment(video, user, comment);
            _videoRepository.AddVideoComment(video, comment);
            _userRepository.AddUserComment(user, comment);
            return Ok(comment);
        }
        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(Guid commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
            {
                return NotFound("Comment not found.");
            }
            _commentRepository.DeleteComment(commentId);
            return Ok();
        }
    }
}
