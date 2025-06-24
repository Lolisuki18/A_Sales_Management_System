using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        public readonly ICustomerRepository icustomerRepository;
        
        public CustomerService(ICustomerRepository customerRepository)
        {
            icustomerRepository = customerRepository;
        }
        //Thêm 1 khách hàng
        public bool AddCustomer(Customer customer) => icustomerRepository.AddCustomer(customer);

        //xoá 1 khách hàng
        public bool DeleteCustomer(Customer customer) => icustomerRepository.DeleteCustomer(customer);

        // Lấy khách hàng theo mã
        public Customer? GetCustomerById(int customerId) => icustomerRepository.GetCustomerById(customerId);


        // Lấy khách hàng theo số điện thoại
        public Customer? GetCustomerByPhone(string phone) => icustomerRepository.GetCustomerByPhone(phone);

        //Lấy taộ danh sách khách hàng từ cơ sở dữ liệu
        public List<Customer> GetCustomers() => icustomerRepository.GetCustomers();

        // Cập nhập thông tin của khách hàng
        public bool UpdateCustomer(Customer customer) => icustomerRepository.UpdateCustomer(customer);

    }
}
