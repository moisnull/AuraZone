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
        IQueryable<Video> GetVideosByCategory(string category);
        Video GetVideoById(Guid id);
        IQueryable<Video> GetAllUserVideos(Guid userId);
        bool VideoExists(Guid id);
        void AddVideo(Guid userId, Video video);
        void AddVideoComment(Video video, Comment comment);
        void UpdateVideo(Guid id);
        void DeleteVideo(Guid userId, Guid id);
    }
}
