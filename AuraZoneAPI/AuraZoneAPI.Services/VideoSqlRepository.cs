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
        public VideoSqlRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IQueryable<Video> GetAllVideos()
        {
            return _dataContext.Videos;
        }
        public Video GetVideoById(Guid id)
        {
            return _dataContext.Videos.FirstOrDefault(d => d.Id == id);
        }
        public IQueryable<Video> GetAllUserVideos(Guid userId)
        {
            return (IQueryable<Video>)_dataContext.Videos.Select(d => d.User.Id == userId);
        }
        public void AddVideo(User user, Video video)
        {
            video.User = user;
            _dataContext.Videos.Add(video);
            _dataContext.SaveChanges();
        }
        public void UpdateVideo(Guid id)
        {
            Video video = GetVideoById(id);
            _dataContext.Update(video);
            _dataContext.SaveChanges();
        }
        public void DeleteVideo(Guid id)
        {
            Video video = GetVideoById(id);
            _dataContext.Remove(video);
            _dataContext.SaveChanges();
        }
    }
}
