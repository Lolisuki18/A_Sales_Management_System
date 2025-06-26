using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository iemployeeRepository;

        public EmployeeService(IEmployeeRepository iemployeeRepository)
        {
            this.iemployeeRepository = iemployeeRepository;
        }

        public List<Employee> GetAllEmployees() => iemployeeRepository.GetAllEmployees();


        public Employee? GetEmpByUsenameAndPassword(string useName, string password) => iemployeeRepository.GetEmpByUsenameAndPassword(useName, password);
       
    }
}
