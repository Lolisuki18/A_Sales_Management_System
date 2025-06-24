using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICustomerService
    {
        //Lấy tất cả danh sách khách hàng từ cơ sở dữ liệu
        List<Customer> GetCustomers();
        //Thêm một khách hàng mới
        bool AddCustomer(Customer customer);
        //Cập nhập thông tin của khách hàng 
        bool UpdateCustomer(Customer customer);
        //Xoá khách hàng 
        bool DeleteCustomer(Customer customer);
        //Tìm kiếm theo mã khách hàng 
        Customer? GetCustomerById(int customerId);

        //Tìm kiếm theo số điện thoại 
        Customer? GetCustomerByPhone(string phone);
    }
}
