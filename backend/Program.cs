using HouseholdExpenseManager.Api.Data;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "A string de conexão 'DefaultConnection' não foi configurada."
    );

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using IServiceScope scope = app.Services.CreateScope();
    AppDbContext database = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await database.Database.MigrateAsync();
    await DevelopmentDatabaseSeeder.SeedAsync(database);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthChecks("/health");

await app.RunAsync();
