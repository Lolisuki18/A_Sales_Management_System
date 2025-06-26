using DataAccessObjects;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tabLogin.SelectedIndex == 0) // Admin Login
            {
                string username = txtUsername.Text;
                string password = txtPassword.Password;

                var emp = EmployeeDAO.Instance.GetEmpByUsenameAndPassword(username, password);
                if (emp != null)
                {
                    RoleWindown roleWindow = new RoleWindown("Admin", emp);
                    roleWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid admin credentials");
                }
            }
            else // Customer Login
            {
                string phone = txtPhone.Text;

                var cus = CustomerDAO.Instance.GetCustomerByPhone(phone);
                   

                if (cus != null)
                {
                    RoleWindown roleWindow = new RoleWindown("Customer", cus);
                    roleWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Phone number not found");
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
