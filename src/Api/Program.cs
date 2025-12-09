using Api;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();


var app = builder.Build();

app.MapDefaultEndpoints();

app.UseInfrastructureServices();
app.UseWebServices();

app.Run();

public partial class Program { }