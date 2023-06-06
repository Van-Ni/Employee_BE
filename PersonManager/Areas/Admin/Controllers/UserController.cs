using PersonManager.Areas.Admin.Models;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PersonManager.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        private HRMEntities db = new HRMEntities();

        public ActionResult Index()
        {
            var users = db.users.Select(u => new UserViewModel { Id = u.id, Username = u.username, Password = u.password, RoleId = u.role_id }).ToList();

            return View(users);
        }
        public ActionResult Create(int id) //CreateUserByEmployee_id
        {

            var employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            // Tạo đối tượng UserViewModel với RoleId mặc định là 1
            var model = new UserViewModel
            {
                RoleId = 1
            };
            ViewBag.EmployeeName = employee.fullname;
            ViewBag.Roles = db.roles.Select(r => new SelectListItem
            {
                Value = r.id.ToString(),
                Text = r.name
            }).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Create(int id, UserViewModel model)
        {
            //Tìm employee cần tạo user trong database
            var employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                // Tạo đối tượng user từ dữ liệu nhập vào từ form
                var user = new user
                {
                    username = model.Username,
                    password = model.Password,
                    role_id = model.RoleId
                };

                var existingUser = db.users.FirstOrDefault(u => u.username == user.username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Tài khoản này đã tồn tại.");
                }
                else
                {
                    // Thêm user mới vào database
                    db.users.Add(user);
                    db.SaveChanges();

                    // Cập nhật user_id cho employee
                    employee.user_id = user.id;
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();

                    // Chuyển hướng đến trang EmployeeDetail của EmployeeController
                    return RedirectToAction("Index", "Employee");
                }
            }

            // Nếu model không hợp lệ, hiển thị lại view Create với thông tin đã nhập và danh sách các role
            ViewBag.Roles = db.roles.Select(r => new SelectListItem
            {
                Value = r.id.ToString(),
                Text = r.name
            }).ToList();
            ViewBag.EmployeeName = employee.fullname;
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            // Tìm user cần cập nhật trong database
            var user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Tạo đối tượng UserViewModel từ user và lấy danh sách các role từ database
            var model = new UserViewModel
            {
                Id = user.id,
                Username = user.username,
                Password = user.password,
                RoleId = (int)user.role_id
            };
            ViewBag.Roles = db.roles.Select(r => new SelectListItem
            {
                Value = r.id.ToString(),
                Text = r.name
            }).ToList();

            // Hiển thị view Update với đối tượng UserViewModel và danh sách các role
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                // Tìm user cần cập nhật trong database
                var user = db.users.Find(model.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                // Cập nhật thông tin user từ đối tượng UserViewModel
                user.username = model.Username;
                user.password = model.Password;
                user.role_id = model.RoleId;

                // Lưu thay đổi vào database
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Roles = db.roles.Select(r => new SelectListItem
            {
                Value = r.id.ToString(),
                Text = r.name
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var userToDelete = db.users.Find(id);
            if (userToDelete == null)
            {
                return HttpNotFound();
            }

            db.users.Remove(userToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}