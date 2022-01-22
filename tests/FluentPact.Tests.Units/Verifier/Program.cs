using System.Net;
using FluentPact.Tests.Units;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/users/8BB7865C-E9E4-4634-AF9D-CDA7EFDBFD0B", async httpContext =>
{
    var user = new User("Dave", "Pumpkin", 25);
    await httpContext.Response.WriteAsJsonAsync(user);
});

app.MapPost("/users", async httpContext =>
{
    httpContext.Response.StatusCode = (int)HttpStatusCode.Created;
});

app.MapRazorPages();

app.Run(); // calls Host.StartAsync()

public partial class Program { }
