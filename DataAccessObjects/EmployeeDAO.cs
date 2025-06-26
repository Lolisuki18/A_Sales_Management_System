using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class EmployeeDAO
    {
        public static EmployeeDAO? instance = null;
        private static readonly object lockObj = new();
        private EmployeeDAO() { }

        public static EmployeeDAO Instance
        {
            get
            {
                lock (lockObj)
                {
                    return instance ??= new EmployeeDAO();
                }
            }
        }
        
        public Employee? GetEmpByUsenameAndPassword(string useName, string password)
        {
            try
            {
                using var context = new LucyContext();
                //Tìm kiếm nhân viên theo tên đăng nhập và mật khẩu
                var employee = context.Employees.SingleOrDefault(e => e.UserName == useName && e.Password == password);
                return employee;
            }catch(Exception ex)
            {
                Console.WriteLine("Error in GetEmpByUsenameAndPassword " + ex.Message);
                return null;
            }
        }
    }
}
