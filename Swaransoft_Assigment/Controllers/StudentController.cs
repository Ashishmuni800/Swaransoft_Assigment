using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Swaransoft_Assigment.Data;
using Swaransoft_Assigment.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swaransoft_Assigment.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public StudentController(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var userDetails = _dbContext.UserDetails.ToList();
            var states = _dbContext.State.ToList();
            ViewBag.States = states;
            return View(userDetails);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(int stateId)
        {
            var cities = _dbContext.StateCity.Where(x => x.StateId == stateId).ToList();
            return Json(cities);
        }

        [HttpPost]
        public IActionResult SaveEmployee(UserDetails model)
        {
            if (ModelState.IsValid)
            {
                UserDetails user = new UserDetails();
                user.Name = model.Name;
                user.Remarks = model.Remarks;
                user.MobileNumber = model.MobileNumber;
                user.Email = model.Email;
                user.City = model.City;
                user.State = model.State;

                // Extract the file name from the uploaded photo
                string fileName = model.uploadphoto;

                // Store the file name in the UserDetails object
                user.uploadphoto = fileName;

                _dbContext.Add(user);
                _dbContext.SaveChanges();

               
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqueFileName);

                Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

               
                return Json(new { success = true, message = "Employee details saved successfully." });
            }
            else
            {
                // Return validation errors
                return Json(new { success = false, errors = ModelState.Values });
            }
        }


        [HttpGet]
        public JsonResult GetEmployeeDetails(int id)
        {
            var employee = _dbContext.UserDetails.Find(id);

            if (employee != null)
            {
                var state = _dbContext.State.FirstOrDefault(s => s.StateId == Convert.ToInt32(employee.State));
                var city = _dbContext.StateCity.FirstOrDefault(c => c.CityId == Convert.ToInt32(employee.City));
                ViewBag.States = new
                {
                    id = employee.Id,
                    name = employee.Name,
                    remarks = employee.Remarks,
                    mobilenumber = employee.MobileNumber,
                    email = employee.Email,
                    state = state.StateName,
                    city = city.City,
                    stateId = employee.State,
                    cityId = employee.City,
                    uploadimage = employee.uploadphoto,
                    success = true,
                    message = "Employee detail Saved Successfully"
                };   
                if (state != null && city != null)
                {
                    return Json(new
                    {
                        id = employee.Id,
                        name = employee.Name,
                        remarks = employee.Remarks,
                        mobilenumber = employee.MobileNumber,
                        email = employee.Email,
                        state = state.StateName,
                        city = city.City,
                        stateId = employee.State,
                        cityId = employee.City,
                        uploadimage = employee.uploadphoto,
                        success = true,
                        message = "Employee detail Saved Successfully"
                    });
                }
                else
                {
                    return Json(new { success = false, message = "State or city details not found" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Employee detail not found" });
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var employee = _dbContext.UserDetails.Find(id);
            if (employee == null)
            {
                return Json(new { success = false, message = "Employee not found" });
            }
            _dbContext.UserDetails.Remove(employee);
            _dbContext.SaveChanges();
            return Json(new { success = true, message = "Employee deleted successfully" });
        }
  
        [HttpPost]
        public JsonResult UpdateEmployeeDetails(UserDetails model)
        {
            try
            {
                var employee = _dbContext.UserDetails.Find(model.Id);
                if (employee != null)
                {
                    // Update employee details
                    employee.Name = model.Name;
                    employee.Email = model.Email;
                    employee.MobileNumber = model.MobileNumber;
                    employee.Remarks = model.Remarks;
                    employee.State = model.State;
                    employee.City = model.City;
                    employee.uploadphoto = employee.uploadphoto;
                    // Save changes to database
                    _dbContext.SaveChanges();

                    return Json(new { success = true, message = "Employee details updated successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Employee not found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating employee details: " + ex.Message });
            }
        }

    }
}
