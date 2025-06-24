using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductService
    {
        //Lấy tất cả sản phẩm 
        List<Product> GetAllProduct();

        //thêm 1 sản phẩm
        bool AddProduct(Product product);
        //Cập nhập thông tin sản phẩm 
        bool UpdateProduct(Product product);

        //Xoá 1 sản phẩm
        bool DeleteProduct(Product product);

        //Tìm kiếm sản phẩm id
        Product? GetProductById(int productId);

        //Tìm kiếm sản phẩm theo tên gần đúng 
        List<Product>? SearchByName(string keyWords);

        //Tìm kiếm các sản phẩm đã ngừng bán 
        List<Product>? GetDiscontinuedProducts();
    }
}
