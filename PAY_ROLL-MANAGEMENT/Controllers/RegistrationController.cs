using Pay_Roll_Management.dBHandler;
using Pay_Roll_Management.Models;
using PAY_ROLL_MANAGEMENT.dBHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pay_Roll_Management.Controllers
{
    public class RegistrationController : Controller
    {
        EmployeedBHandler sbd = new EmployeedBHandler();
        LogindBHandler sd = new LogindBHandler();
        // GET: Employee
        [Authorize]
        public ActionResult Index()
        {
            try
            {
              
                Employee emp = sd.GetByUserName(HttpContext.User.Identity.Name);
                if (!emp.Admin)
                {
                    List<Employee> employees = sbd.UserEmployees(emp.EmployeeId);
                    return View(employees);
                }
                else
                {
                    List<Employee> employees = sbd.GetEmployees();
                    return View(employees);
                }
                    
                
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (sbd.AddNewEmployee(employee))
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

        public ActionResult Delete(int id)
        {
            try
            {

                if (sbd.DeleteEmployee(id))
                {
                    ViewBag.AlertMsg = "Employee Details Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int Id)
        {
            Employee employee = sbd.GetById(Id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sbd.UpdateDetails(employee))
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







    }
}