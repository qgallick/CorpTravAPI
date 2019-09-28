using CorpTravAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorpTravAPI.Data
{
    //repository for db operations for User object
    //argument can be made that Role operations should be moved to their own repository but kept bundled with User operations for purposes of this demo
    public interface IUserRepository
    {
        User GetUserById(string userId);
        User GetUserByName(string firstName, string lastName);
        List<User> SearchUsersByLastName(string lastName);
        List<User> GetAllUsers();
        void EditUser(User user);
        void AssignRoleById(string userId, int roleId);
        void AssignRoleByName(string userId, string role);
        void RemoveRole(string userId, int roleId);
        void RemoveRoleById(string userRoleId);
        List<User> GetUsersForRole(int roleId);
        List<Role> GetAllRoles();
    }
}
