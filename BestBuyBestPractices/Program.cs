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
        IDbConnection conn = new MySqlConnection(connString);
        //Dapper is extending IDbConnection here and conforms to MySqlConnection

        static void Main(string[] args)
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

            //Call the GetAllProducts() using that instance, storing result in
            //the products variable
            var products = prodRepo.GetAllProducts();

            //Print each product from the products collection to the console
            foreach(var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name}");
            }
            

            public static void ListDepartments() //Updating from Michael's video
            {
                var repo = new DepartmentRepository(conn)

                var departments = repo.GetDepartments();
                foreach(var item in departments)
                {
                    Console.WriteLine($"{item.DepartmentID} {item.Name}");
                }
            }

            public static void DepartmentUpdate()
            {
                var repo = new DepartmentRepository(conn);

                Console.WriteLine($"Would you like to update a department? Yes or no:");
                if(Console.ReadLine().ToUpper() == "YES")
                {
                    Console.WriteLine($"What is the ID of the Department you would like to update?");
                    var id = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine($"What would you like to change the name of the department to?");

                    var newName = Console.ReadLine();

                    repo.UpdateDepartment(id, newName);
                }
            }
        }
    }
}


/* Console.WriteLine("Type a new Department name:");

            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);

            var departments = repo.GetAllDepartments();

            foreach(var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }
*/