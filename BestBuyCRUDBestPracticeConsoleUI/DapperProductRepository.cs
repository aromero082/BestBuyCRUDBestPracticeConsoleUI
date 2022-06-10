using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BestBuyCRUDBestPracticeConsoleUI 
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
                new {name = name, price = price, categoryID = categoryID});
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM PRODUCTS;");
        }

        //Bonus
        public void UpdateProductName(int ProductID, string UpdateName)
        {
            _connection.Execute("UPDATE products Set Name = @updateName Where productID = @productID;",
                new { UpdateName = UpdateName, ProductID = ProductID });
        }

        //Bonus Delete Data
        public void DeleteProduct(int productID)
        {
             _connection.Execute("DELETE FROM reviews WHERE ProductId = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM products WHERE productID = @productID;",
                new { productID = productID });
        }
    }
}
