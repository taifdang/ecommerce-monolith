using Api;
using Api.Endpoints;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

builder.Services.AddCors();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(static builder =>
    builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());

app.UseInfrastructureServices();
app.UseWebServices();

app.MapCatalogApi();
app.MapBasketApi();
app.MapOrderApi();
app.MapCustomerApi();
app.MapIdentityApi();

app.Run();

public partial class Program { }