﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        public Employee? GetEmpByUsenameAndPassword(string useName, string password);
        public List<Employee> GetAllEmployees();
    }

}
