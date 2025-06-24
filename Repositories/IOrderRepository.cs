using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderRepository
    {
        //Lấy tất cả đơn hàng 
        List<Order> GetAllOrders();
        //thêm 1 đơn hàng 
        bool AddOrder(Order order);
        //Cập nhập thông tin của đơn hàng 
        bool UpdateOrder(Order order);

        //xoá 1 đơn hàng 
        bool DeleteOrder(Order order);

        //tìm kiếm đơn hàng theo orderId , mã đơn hàng 
        Order? GetOrderById(int orderId);

        //tìm kiếm đơn hàng theo mã khách hàng 
        List<Order> GetOrderByCustomerId(int customerId);

    }
}
