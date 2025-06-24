using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    //Quản lý khách hàng: Xem, thêm, chỉnh sửa và xóa thông tin khách hàng.
    public class CustomerDAO
    {
        //chức năng xem danh sách khách hàng 
        public static List<Customer> GetCustomers()
        {
            var listCustomer = new List<Customer>();
            try
            {
                using var context = new LucyContext();
                listCustomer = context.Customers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách khách hàng: {ex.Message}");
            }
            return listCustomer;
        }
        //chức năng thêm khách hàng
        public static bool AddCustomer(Customer customer)
        {
            try
            {
                using var context = new LucyContext();
                context.Customers.Add(customer); // add thêm customer vào bảng Customers
                context.SaveChanges();// lưu thay đổi vào cơ sở dữ liệu
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine("Error in AddCustomer:" + ex.Message);
                return false;
            }

        }
        //chức năng chỉnh sửa thông tin của khách hàng
        public static bool UpdateCustomer(Customer customer)
        {
            try
            {
                using var context = new LucyContext();
                //context.Customers.Update(customer);//cập nhập thông tin khách hàng
                context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();// lưu thay đổi vào cơ sở dữ liệu
                return true;
            }catch(Exception ex){
                Console.WriteLine("Error in UpdateCustomer: " + ex.Message);
                return false;
            }
        }
        //chức năng xoá khách hàng 

        public static bool DeleteCustomer(Customer customer)
        {
            try
            {
                using var context = new LucyContext();
                //var customer = context.Customers.Find(customerId);// tìm khách hàng đó theo ID
                var customer1 = context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);
                //if(customer1 == null)
                //{
                //    Console.WriteLine("Customer not found.");
                //    return false;
                //} -> sẽ sử lý ở tầng service
                context.Customers.Remove(customer1); // xoá khách hàng đó
                context.SaveChanges(); // lưu thay đổi vào cơ sở dữ liệu 
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeleteCustomer: " + ex.Message);
                return false;
            }
        }
        //Các chức năng tìm kiếm khách hàng theo số điện thoại và mã khách hàng
        //tìm theo mã khách hàng
        public static Customer? GetCustomerById(int customerId)
        {
            try
            {
                using var context = new LucyContext();
                var customer = context.Customers.SingleOrDefault(c => c.CustomerId == customerId);
                //if(customer == null)
                //{
                //    Console.WriteLine("Customer not found.");
                //} -> cái này sẽ xử lý ở tầng service, không nên xử lý ở tầng DAO
                return customer;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in GetCustomerById: " + ex.Message);
                return null;
            }          
        }

        //tìm theo số điện thoại 
        public static Customer? GetCustomerByPhone(string phone)
        {
            try
            {
                using var context = new LucyContext();
                return context.Customers.SingleOrDefault(c => c.Phone == phone);
            }catch(Exception ex)
            {
                Console.WriteLine("Error in GetCustomerByPhone: " + ex.Message);
                return null;
            }
        }
    }
}
