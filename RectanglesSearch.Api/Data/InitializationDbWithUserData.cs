using RectanglesSearch.Api.Models;

namespace RectanglesSearch.Api.Data
{
    public class InitializationDbWithUserData
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<RectangleDbContext>());
        }

        private static void SeedData(RectangleDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    UserName = "admin",
                    Password = "AP6U+t+HhjtHr2fCg0OxVTr3jOSnEI6Q/0dK7K9O5sZ1ToHOiSqDifAzCCkZsRTt4A=="
                });

                context.Users.Add(new User
                {
                    UserName = "customer",
                    Password = "APD7M0atYz6QgwGXwZ/CaMJjffveM8y58gQzA44uYP3szno9BOLeyfR0eCJL6SRv9g=="
                });

                context.SaveChanges();
            }
        }
    }
}
