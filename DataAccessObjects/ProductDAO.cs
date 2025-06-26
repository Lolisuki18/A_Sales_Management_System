using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccessObjects
{
    
    //Quản lý sản phẩm: Quản lý thông tin sản phẩm, bao gồm hàng tồn kho và giá cả.
    public class ProductDAO
    {
        //Singleton Instance 
        private static ProductDAO? instance = null;
        //tạo khoá để đảm bảo thread safety khi truy cập vào instance
        private static readonly object lockObj = new();

        //Constructor private : để đảm bảo chỉ có 1 instance của ProductDAO
        private ProductDAO() { }
        //Tạo public ProperTy để lấy instance của ProductDAO duy nhất
        public static ProductDAO Instance
        {
            get
            {
                //Kiểm, ta xem instance đã được khởi tạo hay chưa ?
                lock (lockObj)
                {
                    return instance ??= new ProductDAO();
                }
            }
        }
        //lấy ra tất cả sản phẩm từ cơ sở dữ liệu
        public  List<Product> GetAllProduct()
        {
            var listProduct = new List<Product>();
            try
            {
                using var context = new LucyContext();
                listProduct = context.Products.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAllProduct: " + ex.Message);
            }
            return listProduct;
        }
        
        //thêm 1 sản phầm mới vào cơ sở dữ liệu
        public  bool AddProduct(Product product)
        {
            try
            {
                using var context = new LucyContext();
                context.Products.Add(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AddProduct: " + ex.Message);
                return false;
            }
        }

        //Cập nhập thông tin của 1 sản phẩm
        public bool UpdateProduct(Product product)
        {
            try
            {
                using var context = new LucyContext();

                // Tìm Product gốc từ DB
                var existing = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existing == null)
                {
                    Console.WriteLine("Product not found.");
                    return false;
                }

                // Cập nhật từng field cơ bản, không đụng navigation
                existing.ProductName = product.ProductName;
                existing.SupplierId = product.SupplierId;
                existing.CategoryId = product.CategoryId;
                existing.QuantityPerUnit = product.QuantityPerUnit;
                existing.UnitPrice = product.UnitPrice;
                existing.UnitsInStock = product.UnitsInStock;
                existing.UnitsOnOrder = product.UnitsOnOrder;
                existing.ReorderLevel = product.ReorderLevel;
                existing.Discontinued = product.Discontinued;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UpdateProduct: " + ex.Message);
                return false;
            }
        }

        //Xoá 1 sản phẩm khởi cơ sở dữ liệu 
        public  bool DeleteProduct(Product product)
        {
            try
            {
                using var context = new LucyContext();
                //tìm sản phẩm đó theo productId của product được truyền vào
                var product1 = context.Products.SingleOrDefault(p => p.ProductId == product.ProductId);
                //if(product1 == null)
                //{
                //    Console.WriteLine("Product not found.");
                //    return false;
                //}-> sẽ sử lý ở tầng service
                context.Products.Remove(product1);//xoá sản phầm đó 
                context.SaveChanges();// lưu thay đổi vào cơ sở dữ liệu
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine("Error in DeleteProduct: " + ex.Message);
                return false;
            }
        }

        //Viết hàm để tìm kiếm 
        
        //tìm kiếm theo ID
        public  Product? GetProductById(int productId)
        {
            try
            {
                using var context = new LucyContext();
                return context.Products.SingleOrDefault(c => c.ProductId == productId);
            }catch(Exception ex)
            {
                Console.WriteLine("Error in GetProductById : " + ex.Message);
                return null;
            }
        }
        //Tìm kiếm theo tên gần đúng 
        public  List<Product>? SearchByName(string keyWords)
        {
            try
            {
                using var context = new LucyContext();
                return context.Products.Where(p => p.ProductName.Contains(keyWords)).ToList();
            }catch(Exception ex)
            {
                Console.WriteLine("Error is SearchByName : " + ex.Message);
                return null;
            }
        }

        //tìm theo sản phẩm đã ngừng bán 
        public  List<Product>? GetDiscontinuedProducts()
        {
            try
            {
                using var context = new LucyContext();
                return context.Products
                              .Where(p => p.Discontinued == true)
                              .ToList();
            }catch(Exception ex)
            {
                Console.WriteLine("Error in GetDiscontinuedProducts" + ex.Message);
                return null;
            }
        }
    }
}
