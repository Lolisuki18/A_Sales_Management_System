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
    /// Interaction logic for ManagerProduct.xaml
    /// </summary>
    public partial class ManagerProduct : UserControl
    {
        public ManagerProduct()
        {
            InitializeComponent();
            LoadProductList();
        }
        private void LoadProductList()
        {
            var products = ProductDAO.Instance.GetAllProduct();
            dgProducts.ItemsSource = null;
            dgProducts.ItemsSource = products;
            //MessageBox.Show($"Đã load {products.Count} sản phẩm.");
        }
        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedItem is Product selected)
            {
                txtProductId.Text = selected.ProductId.ToString();
                txtProductName.Text = selected.ProductName;
                txtUnitPrice.Text = selected.UnitPrice?.ToString();
                txtStock.Text = selected.UnitsInStock?.ToString();
                txtOnOrder.Text = selected.UnitsOnOrder?.ToString();
                txtReorderLevel.Text = selected.ReorderLevel?.ToString();
                txtCategoryId.Text = selected.CategoryId?.ToString();
                txtSupplierId.Text = selected.SupplierId?.ToString();
                txtQuantityPerUnit.Text = selected.QuantityPerUnit;
                chkDiscontinued.IsChecked = selected.Discontinued;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productId = txtProductId.Text == "" ? 0 : int.Parse(txtProductId.Text);
                if(productId != 0)
                {
                    MessageBox.Show("ID đã được sử dụng. Vui lòng để trống hoặc chọn ID khác.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                string productName = txtProductName.Text;
                int? supplierId = txtSupplierId.Text == "" ? null : int.Parse(txtSupplierId.Text);
                int? categoryId = txtCategoryId.Text == "" ? null : int.Parse(txtCategoryId.Text);
                string? quantityPerUnit = txtQuantityPerUnit.Text == "" ? null :  txtQuantityPerUnit.Text;
                decimal? unitPrice = txtUnitPrice.Text == "" ? null :  decimal.Parse(txtUnitPrice.Text);
                int? unitsInStock = txtStock.Text == "" ? null : int.Parse(txtStock.Text);
                int? unitsOnOrder = txtOnOrder.Text == "" ? null : int.Parse(txtOnOrder.Text);
                int? reorderLevel = txtReorderLevel.Text == "" ? null : int.Parse(txtReorderLevel.Text);
                bool discontinued = chkDiscontinued.IsChecked == true;

                Product product = new Product
                {
                    ProductName = productName,
                    SupplierId = supplierId,
                    CategoryId = categoryId,
                    QuantityPerUnit = quantityPerUnit,
                    UnitPrice = unitPrice,
                    UnitsInStock = unitsInStock,
                    UnitsOnOrder = unitsOnOrder,
                    ReorderLevel = reorderLevel,
                    Discontinued = discontinued,
                };
               bool add = ProductDAO.Instance.AddProduct(product);
                if(!add)
                {
                    MessageBox.Show("Thêm sản phẩm thất bại. Vui lòng kiểm tra lại thông tin.Có thể là CategoryId hoặc SupplyId", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Thêm sản phẩm thành công.");
                LoadProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message);
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!int.TryParse(txtProductId.Text, out int productId))
                {
                    MessageBox.Show("ID không hợp lệ hoặc chưa chọn khách hàng.");
                    return;
                }
                string productName = txtProductName.Text;
                int? supplierId = txtSupplierId.Text == "" ? null : int.Parse(txtSupplierId.Text);
                int? categoryId = txtCategoryId.Text == "" ? null : int.Parse(txtCategoryId.Text);
                string? quantityPerUnit = txtQuantityPerUnit.Text == "" ? null : txtQuantityPerUnit.Text;
                decimal? unitPrice = txtUnitPrice.Text == "" ? null : decimal.Parse(txtUnitPrice.Text);
                int? unitsInStock = txtStock.Text == "" ? null : int.Parse(txtStock.Text);
                int? unitsOnOrder = txtOnOrder.Text == "" ? null : int.Parse(txtOnOrder.Text);
                int? reorderLevel = txtReorderLevel.Text == "" ? null : int.Parse(txtReorderLevel.Text);
                bool discontinued = chkDiscontinued.IsChecked == true;

                Product product = new Product
                {
                    ProductId = productId,
                    ProductName = productName,
                    SupplierId = supplierId,
                    CategoryId = categoryId,
                    QuantityPerUnit = quantityPerUnit,
                    UnitPrice = unitPrice,
                    UnitsInStock = unitsInStock,
                    UnitsOnOrder = unitsOnOrder,
                    ReorderLevel = reorderLevel,
                    Discontinued = discontinued,
                };
                product.ProductId = int.Parse(txtProductId.Text);
                bool update = ProductDAO.Instance.UpdateProduct(product);
                if (!update)
                {
                    MessageBox.Show("Cập nhập sản phẩm thất bại. Vui lòng kiểm tra lại thông tin.Có thể là CategoryId hoặc SupplyId", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Cập nhật sản phẩm thành công.");
                LoadProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật sản phẩm: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xoá sản phẩm này?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtProductId.Text);
                    var product = ProductDAO.Instance.GetProductById(id);
                    if (product != null)
                    {
                        bool delete = ProductDAO.Instance.DeleteProduct(product);
                        if (!delete)
                        {
                            MessageBox.Show("Xoá sản phẩm thất bại.");
                        }
                        LoadProductList();
                        MessageBox.Show("Xoá sản phẩm thành công.");
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xoá sản phẩm: " + ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                string keyword = txtSearchProduct.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    LoadProductList();
                    return;
                }

                List<Product> results = new List<Product>();

                if (int.TryParse(keyword, out int id))
                {
                    var product = ProductDAO.Instance.GetProductById(id);
                    if (product != null)
                        results.Add(product);
                }
                //else if (keyword.ToLower() == "discontinued")
                //{
                //    var discontinuedProducts = ProductDAO.Instance.GetDiscontinuedProducts();
                //    if (discontinuedProducts != null)
                //        results.AddRange(discontinuedProducts);
                //}
                else
                {
                    var nameMatches = ProductDAO.Instance.SearchByName(keyword);
                    if (nameMatches != null)
                        results.AddRange(nameMatches);
                }

                dgProducts.ItemsSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sản phẩm: " + ex.Message);
            }
        }
    }
}
