using BusinessObjects;
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
using System.Windows.Shapes;

namespace LeNguyenAnNinhWpfApp
{
    /// <summary>
    /// Interaction logic for RoleWindown.xaml
    /// </summary>
    public partial class RoleWindown : Window
    {
        private string _role;
        private Customer _loggedCustomer;
        private Employee _loggedEmployee;
        public RoleWindown(string role, object user)
        {
            InitializeComponent();
            _role = role;

            if (role == "Admin")
            {
                _loggedEmployee = user as Employee;
            }
            else
            {
                _loggedCustomer = user as Customer;
            }

            ConfigureUIByRole();
        }
        private void ConfigureUIByRole()
        {
            if (_role == "Admin")
            {
                btnCustomerMng.Visibility = Visibility.Visible;
                btnProductMng.Visibility = Visibility.Visible;
                btnOrderMng.Visibility = Visibility.Visible;
                btnReportMng.Visibility = Visibility.Visible;

                btnMyOrders.Visibility = Visibility.Collapsed;
                btnEditProfile.Visibility = Visibility.Collapsed;
               
            }
            else // Customer
            {
                btnCustomerMng.Visibility = Visibility.Collapsed;
                btnProductMng.Visibility = Visibility.Collapsed;
                btnOrderMng.Visibility = Visibility.Collapsed;
                btnReportMng.Visibility = Visibility.Collapsed;

                btnMyOrders.Visibility = Visibility.Visible;
                btnEditProfile.Visibility = Visibility.Visible;
             
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void btnCustomerMng_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo customer view và hiển thị nó trong ContentControl
            var customerView = new ManagerCustomer();
            MainContent.Content = customerView;
        }

        private void btnProductMng_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo product view và hiển thị nó trong ContentControl
            var productView = new ManagerProduct();
            MainContent.Content = productView;
        }

        private void btnOrderMng_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo product view và hiển thị nó trong ContentControl
            var orderView = new ManagerOrder();
            MainContent.Content = orderView;
        }

        private void btnReportMng_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo product view và hiển thị nó trong ContentControl
            var reportView = new ManagerReport();
            MainContent.Content = reportView;
        }
    }
}
