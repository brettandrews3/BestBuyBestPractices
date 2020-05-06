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
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Product").ToList();
        }
        public void CreateProduct(string newProduct)
        {
            _connection.Execute("INSERT INTO Products (Name) VALUES(@productName);", new { productName = newProduct });
        }

        public void CreateProduct(string name, int price, int CategoryID)
        {
            throw new NotImplementedException();
        }
    }
        
}
