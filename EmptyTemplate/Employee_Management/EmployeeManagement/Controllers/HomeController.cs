using EmployeeManagement.Model;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class HomeController: Controller
    {
        private readonly IEmployeeRepository _IemployeeRepository;
        private readonly IHostingEnvironment environment;
        public HomeController(IEmployeeRepository IemployeeRepository,IHostingEnvironment environment )
        {
            _IemployeeRepository = IemployeeRepository;
            this.environment = environment;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            var EmployeeList = _IemployeeRepository.GetAllEmployee();
            return View(EmployeeList);
        }
        [AllowAnonymous]
        public ViewResult Details(int? Id)
        {
            Employee employee = _IemployeeRepository.GetEmpoyee(Id.Value);
            if(employee == null)
            {
                return View("EmployeeNotFound",Id.Value);
            }
            HomeEmployeeViewModel homeEmployeeViewModel = new HomeEmployeeViewModel()
            {
                Employee = employee,
                PageTitle="Employee Details"
              
            };
           
            return View(homeEmployeeViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateView employee)
        {
            if (ModelState.IsValid)
            {
                string unicFileName = null;
                if (employee.photoFile != null)
                {
                    string UpLoadFolder = Path.Combine(environment.WebRootPath, "images");
                    unicFileName = Guid.NewGuid().ToString() + "_" + employee.photoFile.FileName;
                    string filePath = Path.Combine(UpLoadFolder, unicFileName);
                    employee.photoFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee()
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    Photo = unicFileName
                };
                _IemployeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }

            return View();

        }


        [HttpGet]
        public ViewResult Edit(int Id)
        {
            Employee employee = _IemployeeRepository.GetEmpoyee(Id);
            
            if (employee == null)
            {
                return View("EmployeeNotFound", Id);
            }
            EmployeeCreateView editEmployee = new EmployeeCreateView()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                Photo = employee.Photo
            };
            return View(editEmployee);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeCreateView employee)
        {
            if (ModelState.IsValid)
            {

               
                string unicFileName = null;
                if (employee.photoFile != null)
                {
                    unicFileName = FileProcessing(employee);
                }
                if(employee.existingPhotoPath != null)
                {
                    string existngPath = Path.Combine(environment.WebRootPath, "images", employee.existingPhotoPath);
                    System.IO.File.Delete(existngPath);
                }

                Employee updateEmployee = _IemployeeRepository.GetEmpoyee(employee.Id);

                updateEmployee.Name = employee.Name;
                updateEmployee.Email = employee.Email;
                updateEmployee.Department = employee.Department;
                updateEmployee.Photo = unicFileName;
                _IemployeeRepository.Update(updateEmployee);
                
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Delete(int Id)
        {
           Employee employee = _IemployeeRepository.Delete(Id);
            return RedirectToAction("Index");
        }

        private string FileProcessing(EmployeeCreateView employee)
        {
            string unicFileName;
            string UpLoadFolder = Path.Combine(environment.WebRootPath, "images");
            unicFileName = Guid.NewGuid().ToString() + "_" + employee.photoFile.FileName;
            string filePath = Path.Combine(UpLoadFolder, unicFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                employee.photoFile.CopyTo(fileStream);
               
            }
            return unicFileName;

        }

        
    }
}
