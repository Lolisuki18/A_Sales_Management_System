using BusinessObjects;
using DataAccessObjects;
using LeNguyenAnNinhWpfApp.Models;
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

            dgOrders.ItemsSource = null;
            currentDetails = new List<OrderDetail>();
            dgOrderDetails.ItemsSource = currentDetails;

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

                    // Tạo bản sao của currentDetails, không gán OrderId
                    var safeDetails = currentDetails.Select(d => new OrderDetail
                    {
                        ProductId = d.ProductId,
                        UnitPrice = d.UnitPrice,
                        Quantity = d.Quantity,
                        Discount = d.Discount,
                        Product = d.Product // nếu bạn dùng navigation property
                    }).ToList();

                    Order newOrder = new Order
                    {
                        OrderId = orderId,
                        CustomerId = customerId,
                        EmployeeId = employeeId,
                        OrderDate = orderDate,
                        OrderDetails = safeDetails
                    };

                    bool add = OrderDAO.Instance.AddOrder(newOrder);
                    if (!add)
                    {
                        MessageBox.Show("Đơn hàng không được tạo thành công. Vui lòng kiểm tra chi tiết.");
                        return;
                    }

                    MessageBox.Show("Đơn hàng đã được tạo thành công.");
                    LoadData();
                    ClearInputFields(); // nên có hàm để reset textbox sau khi thêm xong
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin đơn hàng.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in btnNewOrder_Click: " + ex.Message);
                MessageBox.Show("Có lỗi xảy ra khi tạo đơn hàng:\n" + ex.Message);
            }
        }
        private void ClearInputFields()
        {
            txtOrderId.Clear();
            txtQuantity.Clear();
            txtDiscount.Clear();
            dpOrderDate.SelectedDate = null;
            currentDetails.Clear();
            dgOrderDetails.ItemsSource = null;
            dgOrderDetails.ItemsSource = currentDetails;
        }
        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Bạn có thể mở rộng để xem chi tiết đơn hàng nếu muốn
        }
    }

}
