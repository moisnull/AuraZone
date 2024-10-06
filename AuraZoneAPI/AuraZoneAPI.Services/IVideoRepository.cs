using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraZoneAPI.DataAccess.Models;

namespace AuraZoneAPI.Services
{
    public interface IVideoRepository
    {
        IQueryable<Video> GetAllVideos();
        Video GetVideoById(Guid id);
        IQueryable<Video> GetAllUserVideos(Guid userId);
        void AddVideo(User user, Video video);
        void UpdateVideo(Guid videoId);
        void DeleteVideo(Guid id);
    }
}
