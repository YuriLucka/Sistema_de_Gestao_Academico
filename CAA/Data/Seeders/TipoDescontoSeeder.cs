using CAA.Models;
using Microsoft.EntityFrameworkCore;

namespace CAA.Data.Seeders
{
    public static class TipoDescontoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!await context.TipoDesconto.AnyAsync())
            {
                var tipos = new List<TipoDesconto>
                {
                    new TipoDesconto { Nome = "Pontualidade" },
                    new TipoDesconto { Nome = "Outros" }
                };
                context.TipoDesconto.AddRange(tipos);
                await context.SaveChangesAsync();
            }
        }
    }
}
