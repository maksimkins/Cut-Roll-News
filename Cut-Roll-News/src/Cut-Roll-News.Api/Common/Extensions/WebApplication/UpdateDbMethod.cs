namespace Cut_Roll_News.Api.Common.Extensions.WebApplication;

using Cut_Roll_News.Infrastructure.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

public static class UpdateDbMethod
{
    public static void UpdateDb(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<NewsDbContext>();

            dbContext.Database.Migrate();
        }
    }
}