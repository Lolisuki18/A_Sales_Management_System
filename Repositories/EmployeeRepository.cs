using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> GetAllEmployees() => EmployeeDAO.Instance.GetAllEmployees();


        public Employee? GetEmpByUsenameAndPassword(string useName, string password) => EmployeeDAO.Instance.GetEmpByUsenameAndPassword(useName, password);

    }
}
