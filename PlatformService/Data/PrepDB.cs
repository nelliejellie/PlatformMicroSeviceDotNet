using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder builder)
        {
            using (var serviceSCope = builder.ApplicationServices.CreateScope())
            {
                SeedData(serviceSCope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        public static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> seeding data");
                var platforms = new List<Platform>
                {
                    new Platform
                    {
                        Id = 1,
                        Name = "Platform Alpha",
                        Publisher = "Alpha Studios",
                        Cost = "$100"
                    },
                    new Platform
                    {
                        Id = 2,
                        Name = "Platform Beta",
                        Publisher = "Beta Games",
                        Cost = "$200"
                    },
                    new Platform
                    {
                        Id = 3,
                        Name = "Platform Gamma",
                        Publisher = "Gamma Enterprises",
                        Cost = "$150"
                    },
                    new Platform
                    {
                        Id = 4,
                        Name = "Platform Delta",
                        Publisher = "Delta Software",
                        Cost = "$250"
                    },
                    new Platform
                    {
                        Id = 5,
                        Name = "Platform Epsilon",
                        Publisher = "Epsilon Interactive",
                        Cost = "$300"
                    }
                };
                context.Platforms.AddRange(platforms);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> we already have data");
            }
        }
    }
}
