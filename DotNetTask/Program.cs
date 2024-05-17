using DotNetTask.Extensions;
using Microsoft.Azure.Cosmos;
using static DotNetTask.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterRepositories();

builder.Services.AddSingleton<CosmosClient>(_ =>
{
    var connectionString = Environment.GetEnvironmentVariable("COSMOS_DB_CONNECTION_STRING");
    return new CosmosClient(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using var serviceScope = app.Services.CreateScope();
var cosmosClient = serviceScope.ServiceProvider.GetRequiredService<CosmosClient>();
await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);

app.Run();
