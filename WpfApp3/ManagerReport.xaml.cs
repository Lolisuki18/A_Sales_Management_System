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
    /// Interaction logic for ManagerReport.xaml
    /// </summary>
    public partial class ManagerReport : UserControl
    {
        public ManagerReport()
        {
            InitializeComponent();
            LoadOrders(); 
        }
        private void LoadOrders()
        {
            var orders = OrderDAO.Instance.GetAllOrders();

            var displayList = orders.Select(o => new OrderDisplayModel
            {
                OrderId = o.OrderId,
                CustomerName = o.Customer.CompanyName,
                EmployeeName = o.Employee.Name,
                OrderDate = o.OrderDate,
                ProductNames = string.Join(", ", o.OrderDetails.Select(d => d.Product.ProductName)),
                Quantities = string.Join(", ", o.OrderDetails.Select(d => d.Quantity)),
                TotalAmount = o.OrderDetails.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount))
            }).ToList();

            dgReport.ItemsSource = displayList;
        }
        private void BtnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (dpFrom.SelectedDate == null || dpTo.SelectedDate == null)
            {
                MessageBox.Show("Please select both From and To dates.");
                return;
            }

            var fromDate = dpFrom.SelectedDate.Value;
            var toDate = dpTo.SelectedDate.Value;

            var orders = OrderDAO.Instance.GetAllOrders()
                .Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate)
                .OrderByDescending(o => o.OrderDate)
               .Select(o => new OrderDisplayModel
               {
                   OrderId = o.OrderId,
                   CustomerName = o.Customer.CompanyName,
                   EmployeeName = o.Employee.Name,
                   OrderDate = o.OrderDate,
                   ProductNames = string.Join(", ", o.OrderDetails.Select(d => d.Product.ProductName)),
                   Quantities = string.Join(", ", o.OrderDetails.Select(d => d.Quantity.ToString())),
                   TotalAmount = o.OrderDetails.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount)) 
               })
                .ToList();

            dgReport.ItemsSource = orders;
        }
    }
}
