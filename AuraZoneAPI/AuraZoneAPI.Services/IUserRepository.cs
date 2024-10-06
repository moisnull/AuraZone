using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraZoneAPI.DataAccess.Models;

namespace AuraZoneAPI.Services
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User GetById(Guid id);
        User GetByUsername(string username);
        bool UserExists(Guid id);
        bool UserExists(string username);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);

    }
}
