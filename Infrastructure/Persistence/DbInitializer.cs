
namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _context;

        public DbInitializer(StoreContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            try
            {
                //Create DB if it doesn't exist and apply any pending migrations 
                if (_context.Database.GetPendingMigrations().Any())
                    await _context.Database.MigrateAsync();

                if (!_context.ProductBrands.Any())
                {
                    //Read brands from file as string
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                    //transform from json into c# object
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    //check ,Add and savechanges
                    if (brands != null && brands.Any())
                    {
                        await _context.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!_context.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types != null && types.Any())
                    {
                        await _context.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!_context.Products.Any())
                {
                    var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    if (products != null && products.Any())
                    {
                        await _context.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}

