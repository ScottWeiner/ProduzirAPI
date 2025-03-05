using ProduzirAPI.Models.Domain;

namespace ProduzirAPI.Data;

public class Seed
{
    public static async Task SeedData(AppDbContext context)
    {
        if (context.ProductClasses.Any() || context.Products.Any()) return;

        var productClasses = new List<ProductClass>
        {
            new ProductClass { Name = "Raw Materials", Description = "Basic input materials" },
            new ProductClass { Name = "Components", Description = "Manufactured parts" },
            new ProductClass { Name = "Finished Goods", Description = "Complete products" },
            new ProductClass { Name = "Packaging", Description = "Packaging materials" }
        };

        await context.ProductClasses.AddRangeAsync(productClasses);
        await context.SaveChangesAsync();

        var products = new List<Product>
        {
            new Product { Number = 1001, Description = "Steel Sheet 2mm", Weight = 25.5M, ProductClass = productClasses[0] },
            new Product { Number = 1002, Description = "Aluminum Rod 10mm", Weight = 12.3M, ProductClass = productClasses[0] },
            new Product { Number = 2001, Description = "Bearing Assembly", Weight = 0.5M, ProductClass = productClasses[1] },
            new Product { Number = 2002, Description = "Hydraulic Pump", Weight = 5.7M, ProductClass = productClasses[1]},
            new Product { Number = 3001, Description = "Electric Motor", Weight = 15.8M, ProductClass = productClasses[2]},
            new Product { Number = 3002, Description = "Control Panel", Weight = 8.2M, ProductClass = productClasses[2] },
            new Product { Number = 4001, Description = "Cardboard Box Large", Weight = 0.8M, ProductClass = productClasses[3]},
            new Product { Number = 4002, Description = "Plastic Wrap Roll", Weight = 2.1M, ProductClass = productClasses[3] },
            new Product { Number = 1003, Description = "Copper Wire 1mm", Weight = 1.2M, ProductClass = productClasses[0]},
            new Product { Number = 2003, Description = "Gear Assembly", Weight = 3.4M, ProductClass = productClasses[1]}
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}