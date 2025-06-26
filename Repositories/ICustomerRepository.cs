using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICustomerRepository
    {
       //Lấy tất cả danh sách khách hàng từ cơ sở dữ liệu
        public List<Customer> GetCustomers();
        //Thêm một khách hàng mới
        public bool AddCustomer(Customer customer);
        //Cập nhập thông tin của khách hàng 
        public bool UpdateCustomer(Customer customer);
        //Xoá khách hàng 
        public bool DeleteCustomer(Customer customer);
        //Tìm kiếm theo mã khách hàng 
        public Customer? GetCustomerById(int customerId);

        //Tìm kiếm theo số điện thoại 
        public Customer? GetCustomerByPhone(string phone);

    }
}
