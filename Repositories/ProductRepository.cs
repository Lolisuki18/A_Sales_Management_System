using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        //thêm 1 sản phẩm
        public bool AddProduct(Product product) => ProductDAO.AddProduct(product);

        //xoá 1 sản phẩm
        public bool DeleteProduct(Product product) => ProductDAO.DeleteProduct(product);

        //Lấy tất cả sản phẩm
        public List<Product> GetAllProduct() => ProductDAO.GetAllProduct();

        //Lấy danh sách cảc sản phẩm đã ngừng bán
        public List<Product>? GetDiscontinuedProducts() => ProductDAO.GetDiscontinuedProducts();

        //tìm kiếm sản phẩm theo id
        public Product? GetProductById(int productId) => ProductDAO.GetProductById(productId);

        //Tìm kiếm sản phẩm theo tên gần đúng
        public List<Product>? SearchByName(string keyWords) => ProductDAO.SearchByName(keyWords);


        public bool UpdateProduct(Product product) => ProductDAO.UpdateProduct(product);
        
    }
}
