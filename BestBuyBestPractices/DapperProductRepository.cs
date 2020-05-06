using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        //Special member method below/constructor; has same name as class, looks like a method
        public DapperProductRepository(IDbConnection connection) //Dapper extends IDbConnection
        {
            _connection = connection;
        }
        //can query or execute based off of this _connection
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Product");
        }
        public void CreateProduct(string name, int price, int categoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES(@productName, @price, @categoryID);",
                new { productName = name, price = price, categoryID = categoryID });
            //takes values of original parameters and stores them in the Query parameter
            //"name" in line 24 matches the one in line 21
        }
    }
        
}
