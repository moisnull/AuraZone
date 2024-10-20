using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraZoneAPI.DataAccess;
using AuraZoneAPI.DataAccess.Models;

namespace AuraZoneAPI.Services
{
    public class VideoSqlRepository : IVideoRepository
    {
        private readonly DataContext _dataContext;
        private readonly IUserRepository _userRepository;
        public VideoSqlRepository(IUserRepository userRepository ,DataContext dataContext)
        {
            _userRepository = userRepository;
            _dataContext = dataContext;
        }
        public IQueryable<Video> GetVideosByCategory(string category)
        {
            return _dataContext.Videos.Where(d => d.Category == category);
        }
        public Video GetVideoById(Guid id)
        {
            return _dataContext.Videos.FirstOrDefault(d => d.Id == id);
        }
        public IQueryable<Video> GetAllUserVideos(Guid userId)
        {
            return _dataContext.Videos.Where(d => d.User.Id == userId);
        }
        public bool VideoExists(Guid id)
        {
            return _dataContext.Videos.Any(d => d.Id == id);
        }
        public void AddVideo(Guid userId, Video video)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
            {
                return;
            }
            user.Videos.Add(video);
            _dataContext.Videos.Add(video);
            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();
        }
        public void AddVideoComment(Video video, Comment comment)
        {
            video.Comments.Add(comment);
            _dataContext.Videos.Update(video);
            _dataContext.SaveChanges();
        }
        public void UpdateVideo(Guid id)
        {
            Video video = GetVideoById(id);
            _dataContext.Update(video);
            _dataContext.SaveChanges();
        }
        public void DeleteVideo(Guid userId, Guid id)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
            {
                return;
            }
            Video video = GetVideoById(id);
            user.Videos.Remove(video);
            _dataContext.Remove(video);
            _dataContext.SaveChanges();
        }
    }
}
