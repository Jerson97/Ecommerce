using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ecommerce.Infrastructure.Persistence
{
    public class EcommerceDbContextData
    {
        public static async Task LoadDataAsync(EcommerceDbContext contex, UserManager<User> usuarioManager, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
        {
			try
			{
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(Role.USER));
                }

                if (!usuarioManager.Users.Any())
                {
                    var usuarioAdmin = new User
                    {
                        Name = "Jerson",
                        LastName = "Ramirez",
                        Email = "jersonramsoto@gmail.com",
                        UserName = "jerson.rs",
                        Phone = "960315728",
                        AvatarUrl = "https://www.lavanguardia.com/andro4all/hero/2023/04/6d8841b6-9d24-457a-95c5-0d3de1d7bf5f.png?width=768&aspect_ratio=16:9&format=nowebp"
                    };
                    await usuarioManager.CreateAsync(usuarioAdmin, "Jerson123456789$");

                    await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.ADMIN);

                    var usuario = new User
                    {
                        Name = "Luis",
                        LastName = "Soto",
                        Email = "luisoto@gmail.com",
                        UserName = "luis.rs",
                        Phone = "960212328",
                        AvatarUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTexyst2oQDHyHz9RlXtuIS6Hok2qtndKfCH1QtYw0sJ3KluyvnA_nqeC8ci1PJDOu3Ct4&usqp=CAU"
                    };
                    await usuarioManager.CreateAsync(usuario, "Luis123456789$");
                    await usuarioManager.AddToRoleAsync(usuario, Role.USER);
                }

                if (!contex.Categories!.Any())
                {
                    var categoryData = File.ReadAllText("../Infrastructure/Data/category.json");
                    var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                    await contex.Categories!.AddRangeAsync(categories!);
                    await contex.SaveChangesAsync();
                }

                if (!contex.Products!.Any())
                {
                    var productData = File.ReadAllText("../Infrastructure/Data/product.json");
                    var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                    await contex.Products!.AddRangeAsync(products!);
                    await contex.SaveChangesAsync();
                }

                if (!contex.Images!.Any())
                {
                    var imageData = File.ReadAllText("../Infrastructure/Data/image.json");
                    var images = JsonConvert.DeserializeObject<List<Image>>(imageData);
                    await contex.Images!.AddRangeAsync(images!);
                    await contex.SaveChangesAsync();
                }

                if (!contex.Reviews!.Any())
                {
                    var reviewData = File.ReadAllText("../Infrastructure/Data/review.json");
                    var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewData);
                    await contex.Reviews!.AddRangeAsync(reviews!);
                    await contex.SaveChangesAsync();
                }

                if (!contex.Countries!.Any())
                {
                    var countryData = File.ReadAllText("../Infrastructure/Data/countries.json");
                    var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                    await contex.Countries!.AddRangeAsync(countries!);
                    await contex.SaveChangesAsync();
                }

            }
			catch (Exception e)
			{

				var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
				logger.LogError(e.Message);
			}
        }
    }
}
