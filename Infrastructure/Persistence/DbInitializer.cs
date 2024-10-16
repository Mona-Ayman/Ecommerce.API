
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task InitializeIdentityAsync()
        {
            //seed default roles
            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //seed default users
            if(!_userManager.Users.Any())
            {
                var superAdmin = new User
                {
                    DisplayName = "Super Admin User",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "1234567890",
                    UserName = "superAdmin"
                };
                var admin = new User
                {
                    DisplayName = "Admin User",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "1234567890",
                    UserName = "Admin"
                };
                await _userManager.CreateAsync(superAdmin, "Passw0rd");
                await _userManager.CreateAsync(admin, "Passw0rd");

                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

        }
    }
}

