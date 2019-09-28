using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorpTravAPI.Data;
using CorpTravAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorpTravAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        
        [HttpGet]
        [Route("GetUserById")]
        public ActionResult<User> GetUserById(string userId)
        {
            var user = _userRepo.GetUserById(userId);

            //if the UserId is missing our repository didnt find the requested user
            if (string.IsNullOrEmpty(user.UserId)) 
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet]
        [Route("GetUserByName")]
        public ActionResult<User> GetUserByName(string firstName, string lastName)
        {
            var user = _userRepo.GetUserByName(firstName, lastName);

            //if the UserId is missing our repository didnt find the requested user
            if (string.IsNullOrEmpty(user.UserId))
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet]
        [Route("SearchUserByLastName")]
        public ActionResult<List<User>> SearchUserByLastName(string lastName)
        {
            var users = _userRepo.SearchUsersByLastName(lastName);

            //if we dont have any users, send notfound result
            if (!users.Any())
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();

            //if we dont have any users, send notfound result
            if (!users.Any())
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet]
        [Route("GetAllUsersForRole")]
        public ActionResult<List<User>> GetAllUsersForRole(int roleId)
        {
            var users = _userRepo.GetUsersForRole(roleId);

            //if we dont have any users, send notfound result
            if (!users.Any())
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public ActionResult<List<Role>> GetAllRoles()
        {
            var roles = _userRepo.GetAllRoles();

            //if we dont have any roles, send notfound result
            if (!roles.Any())
            {
                return NotFound();
            }

            return roles;
        }

        [HttpPost]
        [Route("PostUserRoleByName")]
        public ActionResult PostUserRoleByName([FromBody] UserRoleNameRequest request)
        {
            try
            {
                _userRepo.AssignRoleByName(request.UserId, request.RoleName);

                return Ok();
            }
            catch
            {
                return BadRequest(request.RoleName + " could not be assigned to User " + request.UserId);
            }
        }

        [HttpPost]
        [Route("PostUserRoleById")]
        public ActionResult PostUserRoleById([FromBody] UserRoleIdRequest request)
        {
            try
            {
                _userRepo.AssignRoleById(request.UserId, request.RoleId);

                return Ok();
            }
            catch
            {
                return BadRequest("Role with ID of " + request.RoleId + " could not be assigned to User " + request.UserId);
            }
        }
        
        [HttpPut("{id}")]
        [Route("PutUser")]
        public ActionResult PutUser([FromBody] User user)
        {
            try
            {
                _userRepo.EditUser(user);

                return Ok();
            }
            catch
            {
                return BadRequest("Error updating User " + user.UserId);
            }
        }
        
        [HttpDelete("{id}")]
        [Route("DeleteRole")]
        public ActionResult DeleteRole([FromBody] string id)
        {
            try
            {
                _userRepo.RemoveRoleById(id);

                return Ok();
            }
            catch
            {
                return BadRequest("Role could not be removed");
            }            
        }
    }
}
