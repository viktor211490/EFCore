using System;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
namespace WorkingWithEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //QeryCategories();
            //QueryingProducts();
            QueryingWithLike();
        }
        static void QeryCategories()
        {
            using(var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                WriteLine("Categories and how many products they have:");
                // запрос для всех категорий, включающих связанные товары
                IQueryable<Category> cats = db.Categories.Include(c => c.Products);
                foreach (Category c in cats)
                {
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }
            }
        }
        static void QueryingProducts()
        {
            using(var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());
                WriteLine("Products that costs more than price, and sorted");
                string input;
                decimal price;
                do
                {
                    WriteLine("Enter a product price: ");
                    input = ReadLine();
                } while (!decimal.TryParse(input, out price));
                IQueryable<Product> prods = db.Products
                    .Where(product => product.Cost > price)
                    .OrderByDescending(products => products.Cost);
                foreach (Product item in prods)
                {
                    WriteLine($"{item.ProductID} : {item.ProductName} costs {item.Cost : $#,##0.00} and has {item.Stock} unit in stock");
                }
            }
        }
        static void QueryingWithLike()
        {
            using (var dataBase = new Northwind())

            {
                var loggerFactory = dataBase.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                Write($"Enter a part og product name: ");
                string input = ReadLine();

                IQueryable<Product> products = dataBase.Products
                    .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));

                foreach (Product item in products)
                {
                    WriteLine($"{item.ProductName} has {item.Stock} units in stock. Discontinued? {item.Discontinued}");
                }
            }
        }
    }
}
