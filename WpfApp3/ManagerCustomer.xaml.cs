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
    /// Interaction logic for ManagerCustomer.xaml
    /// </summary>
    public partial class ManagerCustomer : UserControl
    {
        public ManagerCustomer()
        {
            InitializeComponent();
            LoadCustomerList();
        }
        private void LoadCustomerList()
        {
            dgCustomers.ItemsSource = CustomerDAO.Instance.GetCustomers();
        }
        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = dgCustomers.SelectedItem as Customer;
            if (customer != null)
            {
                txtId.Text = customer.CustomerId.ToString();
                txtId.IsReadOnly = true;
                txtId.Background = new SolidColorBrush(Colors.LightGray);
                txtCompany.Text = customer.CompanyName;
                txtContact.Text = customer.ContactName;
                txtTitle.Text = customer.ContactTitle;
                txtPhone.Text = customer.Phone;
                txtAddress.Text = customer.Address;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               int customerId = txtId.Text == "" ? 0 : int.Parse(txtId.Text);
                if(customerId != 0)
                {
                  MessageBox.Show("ID đã được sử dụng. Vui lòng để trống hoặc chọn ID khác.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //  string CompanyName 
                string companyName = txtCompany.Text;
                // string? ContactName 
                string contactName = txtContact.Text;
                // string? ContactTitle 
                string contactTitle = txtTitle.Text;
                //string? Address 
                string address = txtAddress.Text;
                // string? Phone 
                string phone = txtPhone.Text;
                var customer = new Customer
                {
                    CompanyName = companyName,
                    ContactName = contactName,
                    ContactTitle = contactTitle,
                    Address = address,
                    Phone = phone
                };
                // Thêm khách hàng mới vào cơ sở dữ liệu
                CustomerDAO.Instance.AddCustomer(customer);
                MessageBox.Show($"Đã thêm khách hàng thành công:", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Tải lại danh sách khách hàng
                LoadCustomerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm khách hàng: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

      

        private void txtEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtId.Text, out int customerId))
                {
                    MessageBox.Show("ID không hợp lệ hoặc chưa chọn khách hàng.");
                    return;
                }
                //  string CompanyName 
                string companyName = txtCompany.Text;
                // string? ContactName 
                string? contactName = txtContact.Text;
                // string? ContactTitle 
                string? contactTitle = txtTitle.Text;
                //string? Address 
                string? address = txtAddress.Text;
                // string? Phone 
                string? phone = txtPhone.Text;
                var customer = new Customer
                {
                    CustomerId = customerId, 
                    CompanyName = companyName,
                    ContactName = contactName,
                    ContactTitle = contactTitle,
                    Address = address,
                    Phone = phone
                };
                // Thêm khách hàng mới vào cơ sở dữ liệu
                CustomerDAO.Instance.UpdateCustomer(customer);
                MessageBox.Show($"Đã chỉnh sửa khách hàng thành công:", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Tải lại danh sách khách hàng
                LoadCustomerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chỉnh sửa khách hàng: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int customerId = int.Parse(txtId.Text);
                Customer? customer = CustomerDAO.Instance.GetCustomerById(customerId);
                if (customer != null)
                {

                    //xác nhận trước khi xoá
                    var result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa khách hàng: {customer.CompanyName}?",
                        "Xác nhận xóa",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning
                    );

                    if (result == MessageBoxResult.Yes)
                    {
                        // Xóa khách hàng khỏi cơ sở dữ liệu
                        CustomerDAO.Instance.DeleteCustomer(customer);
                        MessageBox.Show("Đã xóa khách hàng thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCustomerList();
                    }
                    else
                    {
                        // Người dùng chọn Không
                        MessageBox.Show("Hủy thao tác xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Khách hàng không tồn tại.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa khách hàng: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSearch1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(searchText))
                {
                    MessageBox.Show("Vui lòng nhập thông tin tìm kiếm.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    LoadCustomerList();
                    return;
                }

                Customer? customer = null;

                // Nếu là số => tìm theo ID
                if (int.TryParse(searchText, out int customerId))
                {
                    customer = CustomerDAO.Instance.GetCustomerById(customerId);
                }
                else // Nếu không phải số => tìm theo số điện thoại
                {
                    customer = CustomerDAO.Instance.GetCustomerByPhone(searchText);
                }

                if (customer != null)
                {
                    dgCustomers.ItemsSource = new List<Customer> { customer };

                    // Optionally hiển thị dữ liệu ở form nhập
                    txtId.Text = customer.CustomerId.ToString();
                    txtCompany.Text = customer.CompanyName;
                    txtContact.Text = customer.ContactName;
                    txtTitle.Text = customer.ContactTitle;
                    txtAddress.Text = customer.Address;
                    txtPhone.Text = customer.Phone;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgCustomers.ItemsSource = null;
                    LoadCustomerList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm khách hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
