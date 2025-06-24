using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository iproductRepository;
        public ProductService(IProductRepository productRepository)
        {
            iproductRepository = productRepository;
        }

        //thêm 1 sản phẩm
        public bool AddProduct(Product product) => iproductRepository.AddProduct(product);

        //xoá 1 sản phẩm 
        public bool DeleteProduct(Product product) => iproductRepository.DeleteProduct(product);    

        //Lấy tất cả sản phẩm
        public List<Product> GetAllProduct() => iproductRepository.GetAllProduct(); 

        //lấy sản phẩm giảm giá
        public List<Product>? GetDiscontinuedProducts() => iproductRepository.GetDiscontinuedProducts();

        // tìm kiếm sản phẩm theo id
        public Product? GetProductById(int productId) => iproductRepository.GetProductById(productId);

        // tìm kiếm sản phẩm theo tên gần đúng
        public List<Product>? SearchByName(string keyWords) => iproductRepository.SearchByName(keyWords);

        // Cập nhập thông tin sản phẩm
        public bool UpdateProduct(Product product) => iproductRepository.UpdateProduct(product);

    }
}
