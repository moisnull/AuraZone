using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraZoneAPI.DataAccess;
using AuraZoneAPI.DataAccess.Models;

namespace AuraZoneAPI.Services
{
    public class UserSqlRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserSqlRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IQueryable<User> GetAll()
        {
            return _dataContext.Users;
        }
        public User GetById(Guid id)
        {
            return _dataContext.Users.FirstOrDefault(d => d.Id == id);
        }
        public User GetByUsername(string username)
        {
            return _dataContext.Users.FirstOrDefault(d => d.Username == username);
        }
        public bool UserExists(Guid id)
        {
            return _dataContext.Users.Any(d => d.Id == id);
        }
        public bool UserExists(string username) 
        {
            return _dataContext.Users.Any(d => d.Username == username);
        }
        public void AddUser(User user)
        {
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
        }
        public void AddUserVideo(User user, Video video)
        {
            user.Videos.Add(video);
            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();
        }
        public void AddUserComment(User user, Comment comment)
        {
            user.Comments.Add(comment);
            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();
        }
        public void DeleteUser(Guid id)
        {
            User user = GetById(id);
            if (user != null)
            {
                List<Video> videos = _dataContext.Videos.Where(v => v.UserId == id).ToList();
                List<Comment> comments = _dataContext.Comments.Where(c => c.UserId == id).ToList();
                _dataContext.Videos.RemoveRange(videos);
                _dataContext.Comments.RemoveRange(comments);
                _dataContext.Users.Remove(user);
                _dataContext.SaveChanges();
            }
        }
        public void DeleteUserVideo(User user, Video video)
        {
            user?.Videos.Remove(video);
        }
        public void DeleteUserComment(User user, Comment comment)
        {
            user?.Comments.Remove(comment);
        }
    }
}
