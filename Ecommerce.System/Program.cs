using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Infrastructure.Persistence.PostgreSQL;
using Ecommerce.System.Infrastructure.Persistence.MongoData;
using Ecommerce.System.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

if (BsonSerializer.LookupSerializer<GuidSerializer>() == null)
{
    BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbType = builder.Configuration["DatabaseSettings:Type"];

if (dbType == "PostgreSQL")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

    builder.Services.AddScoped<IClientRepository, SqlClientRepository>();
    builder.Services.AddScoped<IOrderRepository, SqlOrderRepository>();
    builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
}
else if (dbType == "MongoDB")
{
    builder.Services.AddSingleton<MongoDbContext>();
    builder.Services.AddScoped<IClientRepository, MongoClientRepository>();
    builder.Services.AddScoped<IOrderRepository, MongoOrderRepository>();
    builder.Services.AddScoped<IProductRepository, MongoProductRepository>();
}

builder.Services.AddScoped<OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();