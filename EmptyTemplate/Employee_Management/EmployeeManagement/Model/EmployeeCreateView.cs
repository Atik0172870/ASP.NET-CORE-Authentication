using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class EmployeeCreateView:Employee
    {
        public IFormFile photoFile { get; set; }
        public string existingPhotoPath { get; set; }
    }
}
