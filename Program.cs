using Microsoft.EntityFrameworkCore;
using PermissionsAPI.Models;
using Elasticsearch.Net;
using Nest;
using System;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDbContext<PermisosContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PermisoConnection")));



var app = builder.Build();

app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();