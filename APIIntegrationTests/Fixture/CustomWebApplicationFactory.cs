namespace APIIntegrationTests.Fixture
{
    //public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    //{
    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        builder.ConfigureServices(services =>
    //        {
    //            // Заменяем существующий контекст
    //            var descriptor = services.SingleOrDefault(
    //                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
    //            services.Remove(descriptor);

    //            // Используем реальное подключение к базе данных
    //            services.AddDbContext<ApplicationDbContext>(options =>
    //            {
    //                options.UseSqlServer("Server=localhost;Database=TestDatabase;User Id=test;Password=test;");
    //            });

    //            // Выполняем миграции
    //            using var scope = services.BuildServiceProvider().CreateScope();
    //            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //            context.Database.Migrate();
    //        });
    //    }
    //}
}
