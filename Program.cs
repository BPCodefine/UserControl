using Microsoft.Data.SqlClient;
using System.Data;
using UserControl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("OpenPolicy");

app.MapUserEndpoints();
app.MapRoleEndpoints();
app.MapUserRolesEndpoints();
app.MapAppPageEndpoints();
app.MapPermissionEndpoints();

app.UseDefaultFiles(); // Serve index.html from wwwroot
app.UseStaticFiles();  // Serve Angular files
app.MapFallbackToFile("index.html");
app.UseHttpsRedirection();

app.Run();