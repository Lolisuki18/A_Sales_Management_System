using BusinessObjects;
using DataAccessObjects;
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

            currentDetails = new List<OrderDetail>();
            dgOrderDetails.ItemsSource = currentDetails;

            dgOrders.ItemsSource = OrderDAO.Instance.GetAllOrders();
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
                if (int.TryParse(txtOrderId.Text, out int orderId) &&
                    cbCustomers.SelectedValue is int customerId &&
                    cbEmployees.SelectedValue is int employeeId &&
                    dpOrderDate.SelectedDate is DateTime orderDate &&
                    currentDetails.Any())
                {
                    var existingOrder = OrderDAO.Instance.GetOrderById(orderId);
                    if (existingOrder != null)
                    {
                        MessageBox.Show("Đơn hàng đã tồn tại. Vui lòng dùng mã khác.");
                        return;
                    }

                    // Giả sử có phương thức thêm đơn hàng ở đây
                    Order newOrder = new Order
                    {
                        OrderId = orderId,
                        CustomerId = customerId,
                        EmployeeId = employeeId,
                        OrderDate = orderDate,
                        OrderDetails = currentDetails
                    };

                    OrderDAO.Instance.AddOrder(newOrder);

                    MessageBox.Show("Đơn hàng đã được tạo thành công.");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin đơn hàng.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in btnNewOrder_Click: " + ex.Message);
                MessageBox.Show("Có lỗi xảy ra khi tạo đơn hàng.");
            }
        }
        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Bạn có thể mở rộng để xem chi tiết đơn hàng nếu muốn
        }
    }

}
