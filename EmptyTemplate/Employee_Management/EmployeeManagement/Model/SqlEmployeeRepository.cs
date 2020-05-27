using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SqlEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = context.Employees.Find(Id);
            context.Remove(employee);
            context.SaveChanges();
            return employee;

        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmpoyee(int Id)
        {
            Employee employee = context.Employees.Find(Id);
            return employee;
        }

        public Employee Update(Employee changeEmployee)
        {
            var employee = context.Employees.Attach(changeEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changeEmployee;

        }
    }
}
