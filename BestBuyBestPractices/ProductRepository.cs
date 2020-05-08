using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
namespace BestBuyBestPractices
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        //Special member method below/constructor; has same name as class, looks like a method
        public ProductRepository(IDbConnection connection) //Dapper extends IDbConnection
        {
            _connection = connection;
        }
        
        //Create the data
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES(@productName, @price, @categoryID);",
                new { productName = name, price = price, categoryID = categoryID });
            //takes values of original parameters and stores them in the Query parameter
            //"name" in line 24 matches the one in line 21
        }

        //Read the data
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products");
            //using our Dapper query to pull from SQL database
        }

        //Update the data
        public void UpdateProductName(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = 'whatever' WHERE ProductID = @ProductID;",
                new { updatedName, productID }); //simplify the names; no "name = name" needed
        }

        //Delete the data
        public void DeleteProduct(int productID)
        {
            //3 delete queries needed to scrub the database: reviews, sales, products
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID });
            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { productID });
            _connection.Execute(";DELETE FROM products WHERE ProductID = @productID;",
                new { productID });
        }
    }
        
}
