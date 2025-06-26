using BusinessObjects;
using DataAccessObjects;
using LeNguyenAnNinhWpfApp.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ManagerOrder.xaml
    /// </summary>
    public partial class ManagerOrder : UserControl
    {
        private List<Product> allProducts;
        private List<Customer> allCustomers;
        private List<Employee> allEmployees;
        private List<OrderDetail> currentDetails;

        public ManagerOrder()
        {
            InitializeComponent();
            LoadData();
            LoadOrders();
        }

        private void LoadData()
        {
            allProducts = ProductDAO.Instance.GetAllProduct();
            allCustomers = CustomerDAO.Instance.GetCustomers();
            allEmployees = EmployeeDAO.Instance.GetAllEmployees();

            cbProducts.ItemsSource = allProducts;
            cbProducts.SelectedIndex = 0;
            cbCustomers.ItemsSource = allCustomers;
            cbCustomers.DisplayMemberPath = "CompanyName";
            cbCustomers.SelectedValuePath = "CustomerId";

            cbEmployees.ItemsSource = allEmployees;
            cbEmployees.DisplayMemberPath = "Name";
            cbEmployees.SelectedValuePath = "EmployeeId";

            dgOrders.ItemsSource = null;
            currentDetails = new List<OrderDetail>();
            dgOrderDetails.ItemsSource = currentDetails;

           
        }

        private void LoadOrders()
        {
            var orders = OrderDAO.Instance.GetAllOrders()
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

            dgOrders.ItemsSource = orders;
        }
        private void btnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            if (cbProducts.SelectedItem is Product selectedProduct &&
                int.TryParse(txtQuantity.Text, out int quantity) &&
                float.TryParse(txtDiscount.Text, out float discount))
            {
                if (selectedProduct.UnitPrice == null)
                {
                    MessageBox.Show("Sản phẩm không có giá.");
                    return;
                }
                discount = discount / 100;
                if (discount < 0 || discount > 1)
                {
                    MessageBox.Show("Giảm giá phải từ 0 đến 100 (%).");
                    return;
                }

                int orderId = 0;
                if (!string.IsNullOrEmpty(txtOrderId.Text) && int.TryParse(txtOrderId.Text, out orderId))
                {
                    currentDetails.Add(new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = selectedProduct.ProductId,
                        Product = selectedProduct,
                        UnitPrice = selectedProduct.UnitPrice.Value,
                        Quantity = (short)quantity,
                        Discount = discount
                    });
                }
                else
                {
                    currentDetails.Add(new OrderDetail
                    {
                        ProductId = selectedProduct.ProductId,
                        Product = selectedProduct,
                        UnitPrice = selectedProduct.UnitPrice.Value,
                        Quantity = (short)quantity,
                        Discount = discount
                    });
                }

                // Cập nhật DataGrid
                dgOrderDetails.ItemsSource = null;
                dgOrderDetails.ItemsSource = currentDetails;

                // Thông báo thành công
                MessageBox.Show("Đã thêm chi tiết sản phẩm thành công.");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng thông tin chi tiết đơn hàng.");
            }
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbCustomers.SelectedValue == null || cbEmployees.SelectedValue == null || dpOrderDate.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ Khách hàng, Nhân viên và Ngày tạo.");
                    return;
                }

                // Tạo đơn hàng mới
                var newOrder = new Order
                {
                    OrderId = txtOrderId.Text != "" ? int.Parse(txtOrderId.Text) : 0, // Nếu có OrderId thì dùng, nếu không thì để 0
                    CustomerId = (int)cbCustomers.SelectedValue,
                    EmployeeId = (int)cbEmployees.SelectedValue,
                    OrderDate = dpOrderDate.SelectedDate.Value,
                    OrderDetails = new List<OrderDetail>()
                };

                // Lấy danh sách chi tiết đơn hàng từ DataGrid
                foreach (OrderDetail detail in dgOrderDetails.Items)
                {
                    newOrder.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = detail.ProductId,
                        UnitPrice = detail.UnitPrice,
                        Quantity = detail.Quantity,
                        Discount = detail.Discount
                    });
                }

                // Kiểm tra nếu không có sản phẩm nào
                if (!newOrder.OrderDetails.Any())
                {
                    MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm vào đơn hàng.");
                    return;
                }

                // Thêm vào CSDL
                var success = OrderDAO.Instance.AddOrder(newOrder); // tùy bạn tổ chức repo, hoặc gọi trực tiếp context
                if (success)
                {
                    MessageBox.Show("Đã thêm thành công đơn hàng");
                    dgOrderDetails.ItemsSource = null;
                    dgOrderDetails.Items.Refresh();
                    LoadOrders(); // Làm mới danh sách đơn hàng
                   
                }
                else
                {
                    MessageBox.Show("Không thể tạo đơn hàng. Vui lòng kiểm tra lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                Debug.WriteLine("Chi tiết lỗi: " + ex.InnerException?.Message);
            }
        }
  
    }

}
