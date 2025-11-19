using Microsoft.Extensions.DependencyInjection;
using JobTracker.Api.Data;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Api.AcceptanceTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<JobTrackerDbContext>));

            
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<JobTrackerDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryJobTrackerTestDb");
            });
        });
    }
}