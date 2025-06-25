using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        //thêm 1 đơn hàng
        public bool AddOrder(Order order) => OrderDAO.Instance.AddOrder(order);

        //xoá 1 đơn hàng
        public bool DeleteOrder(Order order) => OrderDAO.Instance.DeleteOrder(order);

        //Lấy tất cả đơn hàng 
        public List<Order> GetAllOrders() => OrderDAO.Instance.GetAllOrders();

        //Lấy các đơn hàng theo mã khách hàng
        public List<Order> GetOrderByCustomerId(int customerId) => OrderDAO.Instance.GetOrderByCustomerId(customerId);

        //Lấy đơn hàng theo mã đơn hàng
        public Order? GetOrderById(int orderId) => OrderDAO.Instance.GetOrderById(orderId);

        //Cập nhập thông tin của đơn hàng
        public bool UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);     
    }
}
