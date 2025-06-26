using BusinessObjects;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeNguyenAnNinhWpfApp
{
    /// <summary>
    /// Interaction logic for CustomerProfile.xaml
    /// </summary>
    public partial class CustomerProfile : UserControl
    {
        public Customer CurrentCustomer { get; private set; }
        public CustomerProfile(Customer customer)
        {
            InitializeComponent();
            InitializeComponent();
            CurrentCustomer = customer;

            // Load dữ liệu vào textbox
            txtCustomerId.Text = customer.CustomerId.ToString();
            txtCompanyName.Text = customer.CompanyName;
            txtContactName.Text = customer.ContactName;
            txtContactTitle.Text = customer.ContactTitle;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật thông tin mới
            CurrentCustomer.CompanyName = txtCompanyName.Text;
            CurrentCustomer.ContactName = txtContactName.Text;
            CurrentCustomer.ContactTitle = txtContactTitle.Text;
            CurrentCustomer.Phone = txtPhone.Text;
            CurrentCustomer.Address = txtAddress.Text;

            // Gọi DAO để cập nhật vào DB
            bool result = CustomerDAO.Instance.UpdateCustomer(CurrentCustomer);

            if (result)
            {
                MessageBox.Show("Profile updated successfully.");
                //DialogResult = true;
            }
            else
            {
                MessageBox.Show("Failed to update profile.");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = false;
            
        }
    }
}
