using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Utilities
{
    public class ValidDomainEmail : ValidationAttribute
    {
        private readonly string domailEmail;
        public ValidDomainEmail(string domailEmail)
        {
            this.domailEmail = domailEmail;
        }
        public override bool IsValid(object value)
        {
            string[] strings= value.ToString().Split('@');
            return strings[1].ToUpper() == domailEmail.ToUpper();
        }
    }
}
