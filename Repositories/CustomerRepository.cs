using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        //Thêm 1 customer
        public bool AddCustomer(Customer customer) => CustomerDAO.AddCustomer(customer);

        //Xoá 1 customer
        public bool DeleteCustomer(Customer customer) => CustomerDAO.DeleteCustomer(customer);
        
        //Lấy customer theo mã
        public Customer? GetCustomerById(int customerId) => CustomerDAO.GetCustomerById(customerId);

        //Lấy customer theo số điện thoại
        public Customer? GetCustomerByPhone(string phone) => CustomerDAO.GetCustomerByPhone(phone);

        //Lấy tất cả danh sách khách hàng từ cơ sở dữ liệu
        public List<Customer> GetCustomers() => CustomerDAO.GetCustomers();

        //Cập nhập thông tin của khách hàng
        public bool UpdateCustomer(Customer customer) => CustomerDAO.UpdateCustomer(customer);
        
    }
}
