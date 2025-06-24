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
        //lấy ra tất cả sản phẩm từ cơ sở dữ liệu
        public static List<Product> GetAllProduct()
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
        public static bool AddProduct(Product product)
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
        public static bool UpdateProduct(Product product)
        {
            try
            {
                using var context = new LucyContext();
                //context.Products.Update(product);
                context.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                //context.Entry<Product>(product):
                    //Đây là cách truy cập vào entry tracking(bộ theo dõi thay đổi) của EF cho entity product.

                    //EF theo dõi mọi object đã attach vào context, và ghi nhận trạng thái(thêm mới, sửa, xoá...).

                //.State = EntityState.Modified:

                    //Bạn đang gắn cờ cho EF biết rằng object product này đã bị chỉnh sửa(modified).

                    //EF sẽ không cần so sánh từng property, nó sẽ coi như tất cả đều thay đổi — và khi gọi SaveChanges(), nó sẽ generate câu UPDATE cho toàn bộ field.
                context.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine("Error in UpdateProduct: " + ex.Message);
                return false;
            }
        }

        //Xoá 1 sản phẩm khởi cơ sở dữ liệu 
        public static bool DeleteProduct(Product product)
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
        public static Product? GetProductById(int productId)
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
        public static List<Product>? SearchByName(string keyWords)
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
        public static List<Product>? GetDiscontinuedProducts()
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
