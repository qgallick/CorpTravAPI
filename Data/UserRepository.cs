using CorpTravAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CorpTravAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly QuibContext _dbContext;
        public UserRepository(QuibContext dbContext)
        {
            _dbContext = dbContext;
        }

        //find exact user by id
        public User GetUserById(string userId)
        {
            var user = _dbContext.User.FirstOrDefault(m => m.UserId == userId);

            return user;
        }

        //find exact user by firstname & lastname fields
        public User GetUserByName(string firstName, string lastName)
        {
            var user = _dbContext.User.FirstOrDefault(m => m.FirstName == firstName && m.LastName == lastName);

            return user;
        }

        //startswith search on lastname field
        public List<User> SearchUsersByLastName(string lastName)
        {
            var users = _dbContext.User.Where(m => m.LastName.StartsWith(lastName));

            if (users.Any())
            {
                return users.ToList();
            }

            return new List<User>();
        }

        //returns all users, for index view
        public List<User> GetAllUsers()
        {
            var users = _dbContext.User.OrderBy(m => m.LastName);

            return users.ToList();
        }

        //the api context may not have the user being passed in attached, this code ensures the source values are altered
        public void EditUser(User user)
        {
            var existingRec = _dbContext.User.First(m => m.UserId == user.UserId);
            _dbContext.Entry(existingRec).CurrentValues.SetValues(user);

            _dbContext.SaveChanges();
        }

        //assign a new role to a user
        public void AssignRoleById(string userId, int roleId)
        {
            //do nothing if the userrole record already exists
            if(_dbContext.UserRoles.Any(m => m.UserId == userId && m.RoleId == roleId))
            {
                return;
            }

            var userRoleRec = new UserRoles
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                RoleId = roleId
            };

            _dbContext.Add(userRoleRec);
            _dbContext.SaveChanges();
        }

        //assign a new role to a user using the role name
        public void AssignRoleByName(string userId, string role)
        {
            var roleId = _dbContext.Role.First(m => m.RoleName == role).Id;

            AssignRoleById(userId, roleId);
        }

        //delete userrole record
        public void RemoveRole(string userId, int roleId)
        {
            var rec = _dbContext.UserRoles.First(m => m.UserId == userId && m.RoleId == roleId);

            _dbContext.Remove(rec);
            _dbContext.SaveChanges();
        }

        //may be cases where we have the userrole record's guid and can remove the record by that
        public void RemoveRoleById(string userRoleId)
        {
            var rec = _dbContext.UserRoles.First(m => m.Id == userRoleId);

            _dbContext.Remove(rec);
            _dbContext.SaveChanges();
        }

        //if we want to find all users under one role
        public List<User> GetUsersForRole(int roleId)
        {
            var userRoles = _dbContext.UserRoles.Where(m => m.RoleId == roleId);

            var users = _dbContext.User.Where(m => m.UserRoles.Any(x => userRoles.Contains(x)));

            return users.ToList();
        }

        //for displaying all the role options
        public List<Role> GetAllRoles()
        {
            var roles = _dbContext.Role.ToList();

            return roles;
        }
    }
}
