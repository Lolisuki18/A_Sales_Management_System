using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        public IOrderRepository iOrderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            iOrderRepository = orderRepository;
        }
        //thêm 1 đơn hàng
        public bool AddOrder(Order order) => iOrderRepository.AddOrder(order);

        //xoá 1 đơn hàng
        public bool DeleteOrder(Order order) => iOrderRepository.DeleteOrder(order);

        //lấy tất cả đơn hàng
        public List<Order> GetAllOrders() => iOrderRepository.GetAllOrders();

        //Tìm kiếm đơn hàng theo mã khách hàng
        public List<Order> GetOrderByCustomerId(int customerId) => iOrderRepository.GetOrderByCustomerId(customerId);

        //Tìm kiếm đơn hàng theo orderId, mã đơn hàng
        public Order? GetOrderById(int orderId) => iOrderRepository.GetOrderById(orderId);

        //Cập nhập thông tin của đơn hàng
        public bool UpdateOrder(Order order) => iOrderRepository.UpdateOrder(order);

    }
}
