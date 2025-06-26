using BusinessObjects;
using DataAccessObjects;
using System;
using System.Windows;

namespace LeNguyenAnNinhWpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                var customers = CustomerDAO.Instance.GetCustomers();
                MessageBox.Show($"Số lượng khách hàng: {customers.Count}");

                MessageBox.Show("kết nối được với database ");
        
                CustomerDataGrid.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}");
            }
        }

       
    }
}