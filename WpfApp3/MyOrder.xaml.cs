using BusinessObjects;
using DataAccessObjects;
using LeNguyenAnNinhWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeNguyenAnNinhWpfApp
{
    /// <summary>
    /// Interaction logic for MyOrder.xaml
    /// </summary>
    public partial class MyOrder : UserControl
    {
        public MyOrder( Customer customer)
        {
            InitializeComponent();
            LoadOrderHistory(customer);


        }
        private void LoadOrderHistory(Customer customer)
        {
            var orders = OrderDAO.Instance.GetOrderByCustomerId(customer.CustomerId);

            var displayList = orders.Select(o => new OrderDisplayModel
            {
                OrderId = o.OrderId,
                CustomerName = o.Customer.CompanyName,
                EmployeeName = o.Employee.Name,
                OrderDate = o.OrderDate,
                ProductNames = string.Join(", ", o.OrderDetails.Select(d => d.Product.ProductName)),
                Quantities = string.Join(", ", o.OrderDetails.Select(d => d.Quantity.ToString())),
                TotalAmount = o.OrderDetails.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount))
            }).ToList();

            dgOrderHistory.ItemsSource = displayList;
        }
    }
}
