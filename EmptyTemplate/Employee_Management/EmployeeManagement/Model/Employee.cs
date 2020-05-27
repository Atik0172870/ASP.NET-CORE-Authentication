using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="maximum length exceeds, you should less than 50 character")]
        public string Name { get; set; }
        [Required]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage ="Invalid email address")]
        [EmailAddress]
        [Display(Name ="Office Email")]
        public string Email { get; set; }
        [Required]
        public Department? Department { get; set; } 
        public string Photo { get; set; }
    }
}
