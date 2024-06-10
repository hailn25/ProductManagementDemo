using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static List<Product> listProducts = GetProducts();
        public ProductDAO()
        {
            Product chai = new Product(1, "Chai", 3, 12, 18);
            Product chang = new Product(2, "chang", 1, 23, 19);
            Product aniseed = new Product(3, "Aniseed Syrup", 2, 23, 10);
            listProducts = new List<Product> { chai, chang, aniseed };
        }
        public List<Product> GetProduct()
        {
            return listProducts;
        }

        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using var db = new MyStoreContextContext();
                listProducts = db.Products.ToList();
           
            }
            catch (Exception ex) { }
            return listProducts;
        }

        public static void SaveProduct(Product p)
        {
            try
            {
                using var db = new MyStoreContextContext();
                db.Products.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void UpdateProduct(Product product)
        {
            using var db = new MyStoreContextContext();
            foreach (var p in listProducts.ToList())
            {
                if (p.ProductId == product.ProductId)
                {
                    p.ProductId = product.ProductId;
                    p.ProductName = product.ProductName;
                    p.UnitPrice = product.UnitPrice;
                    p.UnitsInStock = product.UnitsInStock;
                    p.CategoryId = product.CategoryId;
                    db.Products.Update(p);
                    db.SaveChanges();
                }
                

            }
            

        }

        public static void DeleteProduct(Product product)
        {
            using var db = new MyStoreContextContext();
            foreach (Product p in listProducts.ToList())
            {
                if (p.ProductId == product.ProductId)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
               
            }
           


        }

        public static Product GetProductById(int id)
        {
            try
            {
                using var db = new MyStoreContextContext();
                return db.Products.FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception e)
            {

                return null;
            }
        }

    }
}
