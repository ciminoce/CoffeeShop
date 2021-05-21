using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var dbContext=new CoffeeShopDbContext())
            //{
            //    var lista = dbContext.ProductTypes.ToList();
            //    foreach (var productType in lista)
            //    {
            //        Console.WriteLine(productType.Description);
            //    }
            //}

            //using (var dbcontext=new CoffeeShopDbContext())
            //{
            //    IQueryable<Product> listaProducts = dbcontext.Products;
            //    listaProducts = listaProducts.Where(p => p.FullPrice > 150);
            //    foreach (var product in listaProducts)
            //    {
            //        Console.WriteLine(product.Description);
            //    }
            //}
            //using (var dbcontext = new CoffeeShopDbContext())
            //{
            //    var productType = new ProductType()
            //    {
            //        Description = "Hot Drinks"
            //    };

            //    dbcontext.ProductTypes.Add(productType);
            //    dbcontext.SaveChanges();

            //}


            using (var dbContext = new CoffeeShopDbContext())
            {
                var lista = dbContext.ProductTypes.ToList();
                foreach (var productType in lista)
                {
                    Console.WriteLine(productType.Description);
                }

                var productTypeModified = lista[1];
                productTypeModified.Description = "XXX";

                foreach (var entry in dbContext.ChangeTracker.Entries())
                {
                    Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().FullName);
                    Console.WriteLine("Status: {0}", entry.State);


                }

                Console.ReadLine();
            }
        }
    }
}
