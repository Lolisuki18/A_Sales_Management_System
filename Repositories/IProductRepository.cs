using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        //Lấy tất cả sản phẩm 
        public List<Product> GetAllProduct();

        //thêm 1 sản phẩm
        public bool AddProduct(Product product);
        //Cập nhập thông tin sản phẩm 
        public bool UpdateProduct(Product product);

        //Xoá 1 sản phẩm
        public bool DeleteProduct(Product product);

        //Tìm kiếm sản phẩm id
        public Product? GetProductById(int productId);

        //Tìm kiếm sản phẩm theo tên gần đúng 
        public List<Product>? SearchByName(string keyWords);

        //Tìm kiếm các sản phẩm đã ngừng bán 
        public List<Product>? GetDiscontinuedProducts();
    }
}
