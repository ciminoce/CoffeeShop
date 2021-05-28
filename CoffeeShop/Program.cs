using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CoffeeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            //ListProductTypes();
            //ListProducts();
            //ListFilteredProducts();
            //AddingNewProductType();
            //UpdateProduct();
            //DeleteProductType();
            TrackingEntities();
            Console.ReadLine();

        }

        private static void DeleteProductType()
        {
            using (var dbContext = new CoffeeShopDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                var productTypeListBeforeDelete = dbContext.ProductTypes.ToList();
                DisplayProductTypeList(productTypeListBeforeDelete);
                var deleteProductType = dbContext.ProductTypes
                    .SingleOrDefault(p => p.Description == "Who Knocks");

                if (deleteProductType == null)
                {
                    return;
                }

                dbContext.ProductTypes.Remove(deleteProductType);
                dbContext.SaveChanges();
                var productTypeListAfterDelete = dbContext.ProductTypes.ToList();
                DisplayProductTypeList(productTypeListAfterDelete);
            }

        }

        private static void DisplayProductTypeList(List<ProductType> listType)
        {
            foreach (var productType in listType)
            {
               Console.WriteLine($"{productType.Description}");
            }
        }

        private static void UpdateProduct()
        {
            using (var dbContext=new CoffeeShopDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                var productListBeforeUpdate = dbContext.Products.ToList();
                DisplayProductList(productListBeforeUpdate);
                var modifiedProduct = dbContext.Products
                    .SingleOrDefault(p => p.Description == "Medium Coffee");
                if (modifiedProduct==null)
                {
                    return;
                }

                modifiedProduct.FullPrice += 1.20m;
                dbContext.SaveChanges();
                var productListAfterUpdate = dbContext.Products.ToList();
                Console.WriteLine("==================================");
                DisplayProductList(productListAfterUpdate);
            }
        }

        private static void DisplayProductList(List<Product> productListBeforeUpdate)
        {
            foreach (var product in productListBeforeUpdate)
            {
                Console.WriteLine($"{product.Description} - {product.FullPrice}");
            }
        }

        private static void ListProducts()
        {
            using (var dbContext=new CoffeeShopDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                var productList = dbContext.Products.Include(p=>p.ProductType).ToList();
                foreach (var product in productList)
                {
                    Console.WriteLine($"{product.ProductType.Description} {product.Description} {product.FullPrice}");
                }
            }
        }

        private static void TrackingEntities()
        {
            using (var dbContext = new CoffeeShopDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;

                var typeList = dbContext.ProductTypes.AsNoTracking().ToList();
                foreach (var productType in typeList)
                {
                    Console.WriteLine(productType.Description);
                }

                var productTypeModified = typeList[1];
                productTypeModified.Description = "XXX";

                dbContext.Entry(productTypeModified).State = EntityState.Modified;
                foreach (var entry in dbContext.ChangeTracker.Entries())
                {
                    Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().FullName);
                    Console.WriteLine("Status: {0}", entry.State);
                }

            }
        }

        private static void AddingNewProductType()
        {
            using (var dbContext = new CoffeeShopDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                var productType = new ProductType()
                {
                    Description = "Who Knocks"
                };

                dbContext.ProductTypes.Add(productType);
                dbContext.SaveChanges();

            }
        }

        private static void ListFilteredProducts()
        {
            using (var dbContext = new CoffeeShopDbContext())
            {
                //IQueryable<Product> listProducts = dbContext.Products;
                //listProducts = listProducts.Where(p => p.FullPrice > 150);
                dbContext.Database.Log = Console.WriteLine;
                var productList = dbContext.Products.Where(p => p.FullPrice > 150)
                    .OrderByDescending(p=>p.FullPrice)
                    .ToList();
                foreach (var product in productList)
                {
                    Console.WriteLine($"{product.Description} - {product.FullPrice}");
                }
            }
        }

        private static void ListProductTypes()
        {
            
            using (var dbContext = new CoffeeShopDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                var lista = dbContext.ProductTypes.ToList();
                foreach (var productType in lista)
                {
                    Console.WriteLine(productType.Description);
                }
            }
        }
    }
}
