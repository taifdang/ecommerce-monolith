using Api;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplication();
builder.AddInfrastructure();
builder.AddPresentation();

var app = builder.Build();

app.UseInfrastructure();
app.UsePresentation();

app.Run();

public partial class Program { }