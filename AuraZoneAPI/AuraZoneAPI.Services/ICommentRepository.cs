using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraZoneAPI.DataAccess.Models;

namespace AuraZoneAPI.Services
{
    public interface ICommentRepository
    {
        IQueryable<Comment> GetVideoComments(Guid videoId);
        void AddComment(Video video, User user, Comment comment);
        void UpdateComment(Guid id);
        void DeleteComment(Guid id);
    }
}
