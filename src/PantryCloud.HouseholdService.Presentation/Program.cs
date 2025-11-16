using PantryCloud.HouseholdService.Application;
using PantryCloud.HouseholdService.Infrastructure;
using PantryCloud.HouseholdService.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationLayerServices(builder.Configuration)
    .AddInfrastructureLayerServices(builder.Configuration)
    .AddApplicationLayerServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();