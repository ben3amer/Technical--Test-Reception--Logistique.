using Microsoft.EntityFrameworkCore;
using ReceptionLogistique.Application;
using ReceptionLogistique.Infrastructure;
using ReceptionLogistique.Infrastructure.Persistence;
using ReceptionLogistique.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Réception Logistique API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReceptionLogistiqueDbContext>();
    await db.Database.MigrateAsync();
    DeliverySeeder.Seed(db);
}

app.UseMiddleware<ReceptionLogistique.WebApi.Middleware.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Réception Logistique API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
