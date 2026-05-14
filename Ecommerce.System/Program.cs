using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Infrastructure.Persistence.PostgreSQL;
using Ecommerce.System.Infrastructure.Persistence.MongoData;
using Ecommerce.System.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using Ecommerce.System.Infrastructure.Persistence.MongoData;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Konfiguracja Globalna ---
// To ustawienie dba, aby GUID-y w Mongo by³y czytelne, a nie binarne
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 2. Rejestracja AppDbContext ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

// --- 3. Dynamiczny wybór repozytoriów (SERCE TWOJEJ ARCHITEKTURY) ---
var dbType = builder.Configuration["DatabaseSettings:Type"];

if (dbType == "PostgreSQL")
{
    // Jeœli w appsettings wybierzesz PostgreSQL:
    builder.Services.AddScoped<IOrderRepository, SqlOrderRepository>();
    builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
}
else if (dbType == "MongoDB")
{
    // Jeœli w appsettings wybierzesz MongoDB:
    builder.Services.AddSingleton<MongoDbContext>();
    builder.Services.AddScoped<IOrderRepository, MongoOrderRepository>();

    // NAPRAWIONE: Teraz podpinamy PRAWDZIWE repozytorium Mongo
    builder.Services.AddScoped<IProductRepository, MongoProductRepository>();
}

// --- 4. Rejestracja us³ug biznesowych ---
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// --- 5. Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();