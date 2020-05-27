using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public interface IEmployeeRepository
    {
        Employee GetEmpoyee(int Id);
        IEnumerable<Employee> GetAllEmployee();
        Employee Add(Employee employee);
        Employee Delete(int Id);
        Employee Update(Employee changeEmployee);
    }
}
