using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            {
                new Employee(){Id=1,Name="Atik",Email="atik@123",Department=Department.IT},
                new Employee(){Id=2,Name="Limu",Email="Limu@123",Department=Department.HR},
                new Employee(){Id=3,Name="Shunchi",Email="Shunchi@123",Department=Department.ADMIN},
                new Employee(){Id=4,Name="Kona",Email="Kona@123",Department=Department.Accounts},
                new Employee(){Id=5,Name="Mahi",Email="Mahi@123",Department=Department.SupplyChain},
                new Employee(){Id=6,Name="Jeci",Email="Jeci@123",Department=Department.Inventory},
                new Employee(){Id=7,Name="Mou",Email="Mou@123",Department=Department.PayRoll}

            };

        }

        public Employee Add(Employee employee)
        {
               var id= _employeeList.Max(x => x.Id) + 1;
                employee.Id = id;
               _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.Id == Id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmpoyee(int Id)
        {
            return _employeeList.FirstOrDefault(x => x.Id == Id);
        }

        public Employee Update(Employee changeEmployee)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.Id == changeEmployee.Id);
            if(employee != null)
            {
                employee.Name = changeEmployee.Name;
                employee.Email = changeEmployee.Email;
                employee.Department = changeEmployee.Department;
                
            }
            return employee;

        }
    }
}
