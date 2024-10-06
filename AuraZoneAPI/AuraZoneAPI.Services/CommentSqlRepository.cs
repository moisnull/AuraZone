using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraZoneAPI.DataAccess;
using AuraZoneAPI.DataAccess.Models;

namespace AuraZoneAPI.Services
{
    public class CommentSqlRepository : ICommentRepository
    {
        private readonly DataContext _dataContext;
        public CommentSqlRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IQueryable<Comment> GetVideoComments(Guid videoId)
        {
            return (IQueryable<Comment>)_dataContext.Comments.Single(b => b.Video.Id == videoId);
        }
        public void AddComment(Video video, User user, Comment comment)
        {
            comment.Video = video;
            comment.User = user;
            _dataContext.Comments.Add(comment);
            _dataContext.SaveChanges();
        }
        public void UpdateComment(Guid id)
        {
            Comment comment = _dataContext.Comments.Single(d => id == d.Id);
            _dataContext.Comments.Update(comment);
            _dataContext.SaveChanges();
        }
        public void DeleteComment(Guid id)
        {
            Comment comment = _dataContext.Comments.Single(d => id == d.Id);
            _dataContext.Comments.Remove(comment);
            _dataContext.SaveChanges();
        }
    }
}
