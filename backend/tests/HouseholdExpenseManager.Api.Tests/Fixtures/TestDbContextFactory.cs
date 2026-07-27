using HouseholdExpenseManager.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseholdExpenseManager.Api.Tests.Fixtures;

/// <summary>
/// Cria instâncias isoladas do contexto de banco para testes.
/// </summary>
public static class TestDbContextFactory
{
    /// <summary>
    /// Cria um contexto usando banco em memória isolado.
    /// </summary>
    public static AppDbContext Create()
    {
        DbContextOptions<AppDbContext> options =
            new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        return new AppDbContext(options);
    }
}
