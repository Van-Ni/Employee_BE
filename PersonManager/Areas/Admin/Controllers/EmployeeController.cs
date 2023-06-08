using PersonManager.Areas.Admin.Models;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonManager.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Admin/Employee
        private HRMEntities db = new HRMEntities();
        public ActionResult Index()
        {
            var employees = db.employees
                .Include("user")
                .Include("department")
                .OrderByDescending(e => e.id)
                .Select(e => new EmployeeViewModel
                {
                    Id = e.id,
                    Fullname = e.fullname,
                    Gender = e.gender,
                    Email = e.email,
                    Phone = e.phone,
                    Address = e.address,
                    Birthday = e.birthday,
                    JoinDate = e.joindate,
                    Avatar = e.avatar,
                    Status = e.status,
                    DepartmentId = e.department_id,
                    DepartmentName = e.department != null ? e.department.name : null,
                    PositionId = e.position_id,
                    PositionName = e.position != null ? e.position.name : null,
                    ContractId = e.contract_Id,
                    ContractName = e.contract != null ? e.contract.type : null,
                    UserId = e.user_id,
                    UserName = e.user != null ? e.user.username : null
                })
                .ToList();
          // return Json(employees, JsonRequestBehavior.AllowGet);
           return View(employees);
        }
        public ActionResult GetEmployee(int id)
        {
           // return Json(id, JsonRequestBehavior.AllowGet);
            var emp = db.employees.Where(e => e.id == id)
                .Select(e => new EmployeeViewModel
                {
                    Id = e.id,
                    Fullname = e.fullname,
                    Gender = e.gender,
                    Email = e.email,
                    Phone = e.phone,
                    Address = e.address,
                    Birthday = e.birthday,
                    JoinDate = e.joindate,
                    Avatar = e.avatar,
                    Status = e.status,
                    DepartmentId = e.department_id,
                    DepartmentName = e.department != null ? e.department.name : null,
                    PositionId = e.position_id,
                    PositionName = e.position != null ? e.position.name : null,
                    ContractId = e.contract_Id,
                    ContractName = e.contract != null ? e.contract.type : null,
                    UserId = e.user_id,
                    UserName = e.user != null ? e.user.username : null
                })
                .SingleOrDefault();

            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        public ActionResult Create()
        {
            ViewBag.Departments = db.departments.Select(d => new SelectListItem
            {
                Value = d.id.ToString(),
                Text = d.name
            }).ToList();
            ViewBag.Positions = db.positions.Select(p => new SelectListItem
            {
                Value = p.id.ToString(),
                Text = p.name
            }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index"); // chuyển đến controller Index()
            }
            else
            {
                return View(employee); // nếu có lỗi chuyển lại trang Create
            }
        }
        public ActionResult Edit(int id)
        {
            var employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var employeeViewModel = new EmployeeViewModel
            {
                Id = employee.id,
                Fullname = employee.fullname,
                Gender = employee.gender,
                Email = employee.email,
                Phone = employee.phone,
                Address = employee.address,
                Birthday = employee.birthday,
                JoinDate = employee.joindate,
                Status = employee.status,
                DepartmentId = employee.department_id,
                PositionId = employee.position_id,
                ContractId = employee.contract_Id
            };

            return View(employeeViewModel);
        }
        [HttpPost]
        public ActionResult Edit(int id, employee employee)
        {
            var empToUpdate = db.employees.Find(id);
            if (empToUpdate == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                empToUpdate.fullname = employee.fullname;
                empToUpdate.gender = employee.gender;
                empToUpdate.email = employee.email;
                empToUpdate.phone = employee.phone;
                empToUpdate.address = employee.address;
                empToUpdate.birthday = employee.birthday;
                empToUpdate.joindate = employee.joindate;
                empToUpdate.status = employee.status;
                empToUpdate.department_id = employee.department_id;
                empToUpdate.position_id = employee.position_id;
                empToUpdate.contract_Id = employee.contract_Id;
                db.SaveChanges();
                
                return RedirectToAction("Index"); 

            }
            else
            {
                return View(employee);
            }
        }
        public ActionResult DeleteEmployee(int id)
        {
            var empToDelete = db.employees.Find(id);
            if (empToDelete == null)
            {
                return HttpNotFound();
            }

            db.employees.Remove(empToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}