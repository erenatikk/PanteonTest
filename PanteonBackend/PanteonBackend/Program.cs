using Microsoft.EntityFrameworkCore;
using Panteon_Backend.Data;
using MediatR;
using MongoDB.Driver;
using Microsoft.AspNetCore.Identity;
using Panteon_Backend.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Reflection;
using Panteon_Backend.Controllers.Data;
using Panteon_Backend.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3001", "https://localhost:5100")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateUserCommandHandler).Assembly,
    typeof(GetAllUsersQueryHandler).Assembly
));

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var connectionString = builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString");
    var databaseName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");
    return new MongoDbContext(connectionString, databaseName);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000); // HTTP
    serverOptions.ListenAnyIP(5100, listenOptions =>
    {
        listenOptions.UseHttps("/etc/ssl/certs/aspnetcore-selfsigned.crt", "/etc/ssl/private/aspnetcore-selfsigned.key");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapConfigEndpoints();
app.MapIdentityEndpoints();

app.Run();
