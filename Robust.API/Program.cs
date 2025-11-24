using Robust.App.Mapper;
using Robust.Infrastructure;
using Robust.App.Contracts;
using Robust.App.Services.Abstrctions;
using Robust.App.Services.Implementation;
using Robust.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Robust.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Robust.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
              builder.Services.AddScoped<ICategoryService, CategoryService>();
              builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
              builder.Services.AddScoped<IProductService, ProductService>();
              builder.Services.AddScoped<IProductRepo, ProductRepo>();
              builder.Services.AddScoped<ITokenService, TokenService>();
              builder.Services.AddScoped<IAuthService, AuthService>();
              builder.Services.AddScoped<IUserRepo, UserRepo>();
            //builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            //builder.Services.AddScoped<IOrederItemService, OrderItemService>();
            //builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            //builder.Services.AddScoped<IOrderService, OrderService>();
              builder.Services.AddAutoMapper(typeof(AutoMapperProfile));            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<RobustContext>(options =>
               options.UseSqlServer(connectionString));
            var jwtKey = builder.Configuration["jwt:Key"];
            var jwtIssuer = builder.Configuration["jwt:issuer"];
            var jwtAudience = builder.Configuration["jwt:audience"];

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
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
