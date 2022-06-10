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
            IDbConnection connection = new MySqlConnection(connString);

            var repo = new DapperProductRepository(connection);

            repo.CreateProduct("New stuff", 20, 1);

            var products = repo.GetAllProducts();

            foreach(var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name}");
            }
            

        }
  
        
    }
}
