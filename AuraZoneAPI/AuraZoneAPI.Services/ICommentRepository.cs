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
        Comment GetCommentById(Guid commentId);
        IQueryable<Comment> GetVideoComments(Guid videoId);
        bool CommentExists(Guid commentId);
        void AddComment(Video video, User user, Comment comment);
        void UpdateComment(Guid id);
        void DeleteComment(Guid id);
    }
}
