using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data.SeedData;

public class StoreContextSeed
{
    /// <summary>
    /// Ingresar datos de prueba a la base de datos en caso de que no haya nada.
    /// </summary>
    /// <param name="context">Contexto de la base de datos.</param>
    /// <returns></returns>
    public static async Task SeedAsync(StoreContext context)
    {
        if(!context.ProductBrands.Any())
        {
            var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            context.ProductBrands.AddRange(brands);
        }

        if(!context.ProductTypes.Any())
        {
            var produtcTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
            var productTypes = JsonSerializer.Deserialize<List<ProductType>>(produtcTypesData);
            context.ProductTypes.AddRange(productTypes);
        }

        if(!context.Products.Any())
        {
            var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            context.Products.AddRange(products);
        }

        if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}
