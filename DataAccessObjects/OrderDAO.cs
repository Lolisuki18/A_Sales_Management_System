using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class OrderDAO
    {
        //SingleTon instance
        private static OrderDAO? instance = null;
        //tạo khoá để đảm bảo thread safety khi truy cập vào instance
        private static readonly object lockObj = new();

        //Constructor private : để đảm bảo chỉ có 1 instacne của OrderDAO mà thôi
        private OrderDAO() { }

        //tạo public Property để lấy instance của OrderDAO duy nhất
        public static OrderDAO Instance
        {
            get
            {
                lock (lockObj)
                {
                    return instance ??= new OrderDAO();
                }
            }
        }
        //Chức năng CRUD cho đơn hàng

        //Lấy tất cả đơn hàng 
        public List<Order> GetAllOrders()
        {
            var listOrders = new List<Order>();
            try
            {
                using var context = new LucyContext();
                listOrders = context.Orders
                                    .Include(o => o.Customer)
                                    .Include(o => o.Employee)
                                      .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetAllOrders : " + ex.Message);
            }
            return listOrders;
        }
        //thêm đơn hàng 
        public bool AddOrder(Order order)
        {
            try
            {
                using var context = new LucyContext();
                context.Orders.Add(order);
                //context.OrderDetails.AddRange(order.OrderDetails); // Thêm chi tiết đơn hàng -> không cần nữa vì EF sẽ tự động ánh xạ đế và gán OderDetails cho Order
                context.SaveChanges();//Lưu thay đổi vào cơ sở dữ liệu
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in AddOrder : " + ex.Message);
                return false;
            }
        }
        //Cập nhập thông tin đơn hàng
        public bool UpdateOrder(Order order)
        {
            try
            {
                using var context = new LucyContext();
                var existing = context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);

                if (existing == null)
                {
                    Debug.WriteLine("Order not found.");
                    return false;
                }

                // Cập nhật từng field đơn giản, không đụng vào navigation để tránh lỗi
                existing.CustomerId = order.CustomerId;
                existing.EmployeeId = order.EmployeeId;
                existing.OrderDate = order.OrderDate;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in UpdateOrder: " + ex.Message);
                return false;
            }
        }

        //xoá 1 đơn hàng 
        public bool DeleteOrder(Order order)
        {
            try
            {
                using var context = new LucyContext();
                var order1 = context.Orders.SingleOrDefault(o => o.OrderId == order.OrderId);
                //if (order1 == null)
                //{
                //    Console.WriteLine("Order not found.");
                //    return false;
                //}-> sẽ sử lý ở tầng service
                context.Orders.Remove(order1);//Xoá đơn hàng
                context.SaveChanges();// lưu thay đổi vào cơ sở dữ liệu
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in DeleteOrrder : " + ex.Message);
                return false;
            }
        }
        //Một số hàm tìm kiếm 
        //Tìm kiếm đơn hàng theo OrderId
        public Order? GetOrderById(int orderId)
        {
            try
            {
                using var context = new LucyContext();
                return context.Orders.SingleOrDefault(o => o.OrderId == orderId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetOrderById : " + ex.Message);
                return null;
            }
        }
        //tìm kiếm đơn hàng theo customerId 
        public List<Order> GetOrderByCustomerId(int customerId)
        {
            var listOrders = new List<Order>();
            try
            {
                using var context = new LucyContext();
                listOrders = context.Orders.Where(o => o.CustomerId == customerId)
                    .Include(o => o.Customer)//Bao gồm thông tin của khách hàng đó
                    .Include(o => o.OrderDetails)//Bao gồm thông tin chi tiết đơn hàng
                    .Include(o => o.Employee)//Bao gồm thông tin nhân viên nếu cần
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetOrderByCustomerId : " + ex.Message);
            }
            return listOrders;
        }

    }
}
