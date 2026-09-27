using Microsoft.EntityFrameworkCore;
using ReceptionLogistique.Application;
using ReceptionLogistique.Infrastructure;
using ReceptionLogistique.Infrastructure.Persistence;
using ReceptionLogistique.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReceptionLogistiqueDbContext>();
    await db.Database.MigrateAsync();
    DeliverySeeder.Seed(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ReceptionLogistique.WebApi.Middleware.ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
