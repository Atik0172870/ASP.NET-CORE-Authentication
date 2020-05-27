using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Utilities;

namespace EmployeeManagement.Model
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailUsed", controller: "Account")]
        [ValidDomainEmail(domailEmail:"atik.com",ErrorMessage ="Email domail must be atik.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Pasword")]
        [Compare("Password",ErrorMessage ="Password and Confirm Pasword You Provide Don't Match ")]
        public string ConfirmPassword { get; set; }

    }
}
