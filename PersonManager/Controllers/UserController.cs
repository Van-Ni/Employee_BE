using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonManager.Controllers
{
    public class UserController : ApiController
    {
        private HRMEntities db = new HRMEntities();
        [HttpGet]
        [Route("api/User/GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            var users = db.users.Select(u => new { id = u.id, username = u.username,password = u.password, role_id = u.role_id }).ToList();

            return Ok(users);
        }

        // POST api/users/CreateUserForEmployee
        [HttpPost]
        [Route("api/User/CreateUserForEmployee", Name = "CreateUserForEmployee")]
        public IHttpActionResult CreateUserForEmployee(int employee_id, user user)
        {

            var employee = db.employees.Find(employee_id);
            if (employee == null)
            {
                return NotFound();
            }

           
            var existingUser = db.users.FirstOrDefault(u => u.username == user.username);
            if (existingUser != null)
            {
                return BadRequest("Tài khoản cho nhân viên này đã tồn tại.");
            }

            
            var newUser = new user
            {
                username = user.username,
                password = user.password,
                role_id = user.role_id
            };

            db.users.Add(newUser);
            db.SaveChanges();

            
            employee.user_id = newUser.id;
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();

            
            var createdUser = db.users.Find(newUser.id);

            
            return Ok(new { id = createdUser.id, username = createdUser.username, role_id = createdUser.role_id });
        }



        [HttpDelete]
        [Route("api/User/DeleteUser/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            var userToDelete = db.users.Find(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            db.users.Remove(userToDelete);
            db.SaveChanges();
            return Ok();
        }

        // POST api/users/Login
        [HttpPost]
        [Route("api/User/Login")]
        public IHttpActionResult Login(user loginUser)
        {
            var user = db.users.FirstOrDefault(u => u.username == loginUser.username && u.password == loginUser.password);
            if (user == null)
            {
                return BadRequest("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            return Ok(new { id = user.id, username = user.username, role_id = user.role_id });
        }

        [HttpDelete]
        [Route("api/User/Logout/{id}")]
        public IHttpActionResult Logout(int id)
        {
            var user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
