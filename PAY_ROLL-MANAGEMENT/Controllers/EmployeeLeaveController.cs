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
    public class EmployeeLeaveController : Controller
    {
        EmployeeLeavedBHandler sd = new EmployeeLeavedBHandler();
        LogindBHandler sbd = new LogindBHandler();
        [Authorize]
        // GET: EmployeeLeave
        public ActionResult Index()
        {
            List<EmployeeLeave> employees = new List<EmployeeLeave>();
            try
            {
                Employee emp = sbd.GetByUserName(HttpContext.User.Identity.Name);
                if (!emp.Admin)
                {
                    List<EmployeeLeave> employee = sd.UserLeaveRequest(emp.EmployeeId);
                        return View(employee);
                }
                else
                {
                    List<EmployeeLeave> employee = sd.GetLeaveRequest();
                    return View(employee);
                }
                


                return View(employees);
            }
            catch (Exception ex)
            {
                return View(employees);
            }
        }

       
        // GET: EmployeeLeave/Create
        public ActionResult Create()
        {
            EmployeeLeave employeeLeave = new EmployeeLeave();
            Employee emp = sbd.GetByUserName(HttpContext.User.Identity.Name);
            employeeLeave.EmployeeId = emp.EmployeeId;
            employeeLeave.FullName = emp.FullName;
             return View(employeeLeave);
        }

        // POST: EmployeeLeave/Create
        [HttpPost]
        public ActionResult Create(EmployeeLeave employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sd.AddLeaveRequest(employee))
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

        // GET: EmployeeLeave/Edit/5
        public ActionResult Edit(int Id)
        {
            EmployeeLeave employee = sd.GetByLeaveId(Id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeLeave employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sd.UpdateLeave(employee))
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

        // GET: EmployeeLeave/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {

                if (sd.DeleteLeaveRequest(id))
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
    }
}
