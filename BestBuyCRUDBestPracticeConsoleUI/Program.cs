using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion
            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Plese press enter...");
            Console.ReadLine();

            var depos = repo.GetAllDepartments();
            print(depos);


            Console.WriteLine("Do you want to add a department???");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "Yes")
            {
                Console.WriteLine("What is the nname of your new Department???");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                print(repo.GetAllDepartments());
            }

            Console.WriteLine("Have a great day");

        }
        private static void print(IEnumerable<Department> depos)
        {
            foreach(var depo in depos)
            {
                Console.WriteLine($"ID: {depo.DepartmentID} Name: {depo.Name}");
            }
        }
    }
}
