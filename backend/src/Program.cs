using FluentValidation;
using FluentValidation.AspNetCore;
using HouseholdExpenseManager.Api.Data;
using HouseholdExpenseManager.Api.Middlewares;
using HouseholdExpenseManager.Api.Services;
using HouseholdExpenseManager.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "A string de conexão 'DefaultConnection' não foi configurada."
    );

// MVC
builder.Services.AddControllers();

// Validação automática dos DTOs
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// Documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    string xmlFile =
        $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";

    string xmlPath = Path.Combine(
        AppContext.BaseDirectory,
        xmlFile
    );

    options.IncludeXmlComments(xmlPath);
});

// Persistência
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Serviços da aplicação
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// CORS liberado para o frontend durante desenvolvimento.
// Como a aplicação não possui autenticação, uma política restritiva não é necessária neste momento.
// Em um ambiente fora de desenvolvimento, restringir para domínios conhecidos.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Monitoramento
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using IServiceScope scope = app.Services.CreateScope();

    AppDbContext database = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    await database.Database.MigrateAsync();
    await DevelopmentDatabaseSeeder.SeedAsync(database);
}

// Tratamento global de exceções
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Frontend");

app.MapControllers();

app.MapHealthChecks("/health");

await app.RunAsync();
