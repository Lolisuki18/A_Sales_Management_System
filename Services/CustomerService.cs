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
           this.icustomerRepository = customerRepository;
        }
        //Thêm 1 khách hàng
        public bool AddCustomer(Customer customer)
        {
            //Kiểm tra xem có nhập liệu companyName và phone hay không ?
            if (string.IsNullOrWhiteSpace(customer.CompanyName) ||
                string.IsNullOrWhiteSpace(customer.Phone))
            {
                Console.WriteLine("CompanyName and Phone is requird");
                return false;
            }

            return icustomerRepository.AddCustomer(customer);
        }

        //xoá 1 khách hàng
        public bool DeleteCustomer(Customer customer)
        {
            //Kiểm tra xem khách hàng muốn xoá có tồn tại hay không ?
            var existing = icustomerRepository.GetCustomerById(customer.CustomerId);
            if(existing == null)
            {
                Console.WriteLine("Không tìm thấy khách hàng để xoá");
                return false;
            }
            return icustomerRepository.DeleteCustomer(customer);
        }

        // Lấy khách hàng theo mã
        public Customer? GetCustomerById(int customerId)
        {
            if(customerId <= 0)
            {
                Console.WriteLine("Mã khách hàng không hợp lệ");
                return null;
            }
            return icustomerRepository.GetCustomerById(customerId);
        }


        // Lấy khách hàng theo số điện thoại
        public Customer? GetCustomerByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Số điện thoại không hợp lệ.");
                return null;
            }
            return icustomerRepository.GetCustomerByPhone(phone);
        }

        //Lấy toàn danh sách khách hàng từ cơ sở dữ liệu
        public List<Customer> GetCustomers() => icustomerRepository.GetCustomers();

        // Cập nhập thông tin của khách hàng
        public bool UpdateCustomer(Customer customer)
        {
            var existing = icustomerRepository.GetCustomerById(customer.CustomerId);
            if(existing == null)
            {
                Console.WriteLine("Không tìm thấy khách hàng để cập nhập");
                return false;
            }
            if (string.IsNullOrWhiteSpace(customer.CompanyName) ||
              string.IsNullOrWhiteSpace(customer.Phone))
            {
                Console.WriteLine("Tên công ty và số điện thoại là bắt buộc.");
                return false;
            }
            return icustomerRepository.UpdateCustomer(customer);
        }

    }
}
