using Microsoft.EntityFrameworkCore;
using ProduzirAPI.Data;
using ProduzirAPI.Repositories;
using AutoMapper;

using ProduzirAPI.Models;

// Services Container
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecficOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
});

// Register repository here, before building the app
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

var connString = "";
//if (builder.Environment.IsDevelopment())
connString = builder.Configuration.GetConnectionString("DefaultConnection");
//else
{
    // Use connection string provided at runtime by Heroku.
    //var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    // Parse connection URL to connection string for Npgsql
    //connUrl = connUrl.Replace("postgres://", string.Empty);
    //var pgUserPass = connUrl.Split("@")[0];
    //var pgHostPortDb = connUrl.Split("@")[1];
    //var pgHostPort = pgHostPortDb.Split("/")[0];
    //var pgDb = pgHostPortDb.Split("/")[1];
    //var pgUser = pgUserPass.Split(":")[0];
    //var pgPass = pgUserPass.Split(":")[1];
    //var pgHost = pgHostPort.Split(":")[0];
    //var pgPort = pgHostPort.Split(":")[1];

    //connString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
}
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(connString);
});

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowSpecficOrigins");
}

app.UseHttpsRedirection();

app.UseAuthorization();


// Map controllers so the endpoints match what I call them when establishing the Controller Classes ("api/[controller]")
app.MapControllers();

// Seed Data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        await context.Database.MigrateAsync();
    }
    if (!context.Products.Any())  // Only seed if no products exist
    {
        await Seed.SeedData(context);
    }
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during database seeding");
}

app.Run();

