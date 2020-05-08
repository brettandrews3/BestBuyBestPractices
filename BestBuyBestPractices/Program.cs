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

        static void Main(string[] args)
        {
            

            //var repo = new DapperDepartmentRepository(conn);



            {
                ListDepartments();
                DepartmentUpdate();
            }

            public static void ListDepartments() //Updating from Michael's video
            {
                var repo = new DapperDepartmentRepository(conn)

                var departments = repo.GetDepartments();
                foreach(var item in departments)
                {
                    Console.WriteLine($"{item.DepartmentID} {item.Name}");
                }
            }

            public static void DepartmentUpdate()
            {
                var repo = new DapperDepartmentRepository(conn);

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