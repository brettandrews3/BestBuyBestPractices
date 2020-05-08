using System;
using System.Data; //used for the IDbConnection
using System.IO; //input-output methods
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; //allows access to MySqlConnection class

namespace BestBuyBestPractices
{
    class Program
    {
        static IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        static string connString = config.GetConnectionString("DefaultConnection");

        //getting the value from DefaultConnection
        static IDbConnection conn = new MySqlConnection(connString);
        //Dapper is extending IDbConnection here and conforms to MySqlConnection

        static void Main(string[] args)
        {
            ListProducts();

            DeleteProduct();

            ListProducts();
        }

        //Create product
        public static void CreateProduct()
            //Put all these steps under 1 method here so we don't have to repeat
            //all that code. It's static, so I can just call the method--no instance req'd.
            {
                //created an instance so we can call methods that query the database
                var prodRepo = new ProductRepository(conn);

                Console.WriteLine($"What is the new product's name?");
                var prodName = Console.ReadLine();

                Console.WriteLine($"What is the new product's price?");
                var price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"What is the new product's category ID?");
                var categoryID = Convert.ToInt32(Console.ReadLine());

                //Method goes to database and inserts the new product
                prodRepo.CreateProduct(prodName, price, categoryID);

                ListProducts();
            }

        //Read product info:
        public static void ListProducts()
        {
            var prodRepo = new ProductRepository(conn);
            //copy over instance of repository to connect with database
            var products = prodRepo.GetAllProducts();

            //Print each product from the products collection to the console
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name}");
            }
        }

        //Update product name by its productID:
        public static void UpdateProductName()
        {
            //Use our connection to reach the database
            var prodRepo = new ProductRepository(conn);

            Console.WriteLine($"What is the productID of the product you would like to update?");
            var productID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"What is the new name you would like for the product with id {productID}?");
            var updatedName = Console.ReadLine();

            prodRepo.UpdateProductName(productID, updatedName); //method from the repository

            ListProducts();
        }

        //Delete product data by its productID
        public static void DeleteProduct()
        //No return type needed: go to database, do something, and leave
        {
            var prodRepo = new ProductRepository(conn);

            Console.WriteLine($"Please enter the productID of the product you want to delete:");
            var productID = Convert.ToInt32(Console.ReadLine());

            prodRepo.DeleteProduct(productID); //method from the repository

            ListProducts();
        }
    }
}
