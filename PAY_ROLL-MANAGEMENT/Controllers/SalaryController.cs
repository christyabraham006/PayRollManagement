using Pay_Roll_Management.dBHandler;
using Pay_Roll_Management.Models;
using PAY_ROLL_MANAGEMENT.dBHandler;
using PAY_ROLL_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAY_ROLL_MANAGEMENT.Controllers
{
    public class SalaryController : Controller
    {
        EmployeedBHandler em = new EmployeedBHandler(); 
        SalarydBHandler sd = new SalarydBHandler();
        LogindBHandler sbd = new LogindBHandler();
        [Authorize]
        // GET: EmployeeLeave
        public ActionResult Index()
        {
            
            List<Salary> employees = new List<Salary>();
            try
            {
                Employee emp = sbd.GetByUserName(HttpContext.User.Identity.Name);
                if (!emp.Admin)
                {
                    ViewBag.Employees = false;
                    List<Salary> employee = sd.UserSalaryReport(emp.EmployeeId);
                    return View(employee);
                }
                else
                {
                    ViewBag.Employees = true;
                    List<Salary> employee = sd.GetSalaryReport();
                    return View(employee);
                }

            }
            catch (Exception ex)
            {
                return View(employees);
            }
        }


        // GET: Salary/Create
        public ActionResult Create(string id,string name)
        {

            Salary employee = new Salary();
            employee.EmployeeId = id;
            employee.FullName=name;
            return View(employee);
        }

        // POST: Salary/Create
        [HttpPost]
        public ActionResult Create(Salary employee)
        {
            
            try
            {
                employee.Id = 0;
                ModelState.Clear();
                if (TryValidateModel(employee))
                {
                    if (sd.AddSalaryReport(employee))
                    {
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed");
                        return View();
                    }
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + " " + ex.StackTrace);
                return View(employee);
            }
        }

        // GET: Salary/Edit/5
        public ActionResult Edit(int Id)
        {
            Salary employee = sd.GetBySalaryId(Id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Salary employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sd.UpdateSalary(employee))
                    {
                        ModelState.Clear();
                        //ModelState.AddModelError("","Sucessfully added");
                        return RedirectToAction("Index");
                    }
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + " " + ex.StackTrace);
                return View(employee);
            }
        }

        // GET: Salary/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {

                if (sd.DeleteSalaryReport(id))
                {
                    ViewBag.AlertMsg = "Employee Leave Request Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult Generate(Employee employee)
        {
            List<Employee> employees = em.GetEmployees();
            return View(employees);
           
        }

    }
}