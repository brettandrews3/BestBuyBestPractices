﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BestBuyBestPractices
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _connection; //Constructor; can't set the connection anywhere but this constructor because it's readonly
        public DepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments() //a collection of Departments
        {
            return _connection.Query<Department>("SELECT * FROM Departments");
            //Maps all results from Query to our Department objects, aligning all columns with the class properties
        }

        public void InsertDepartment(string newDepartmentName)
        {
           _connection.Execute("INSERT INTO Departments (Name) VALUES (@departmentName); ",
               new { departmentName = newDepartmentName });


        }
    }
}
/* Constructor:
Here, whenever we create a new instance of the DepartmentRepository, we will
pass in our connection string as a parameter and set that connection string in
our private readonly variable _connectionString.  

The benefit of having _connectionString private and readonly is that you can't
inadvertently change it from another part of the DepartmentRepository class after
it is initialized. The readonly modifier ensures the field can only be given a
value during its initialization or in its class constructor. */
