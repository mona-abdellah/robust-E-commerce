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

namespace Robust.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddScoped<ICategoryService, CategoryService>();
            //builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
              builder.Services.AddScoped<IProductService, ProductService>();
              builder.Services.AddScoped<IProductRepo, ProductRepo>();
            //builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            //builder.Services.AddScoped<IOrederItemService, OrderItemService>();
            //builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            //builder.Services.AddScoped<IOrderService, OrderService>();
              builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddIdentity<User,IdentityRole>(
                  options =>
                  {
                      options.SignIn.RequireConfirmedAccount = false;
                  }).AddEntityFrameworkStores<RobustContext>()
                  .AddDefaultTokenProviders();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<RobustContext>(options =>
               options.UseSqlServer(connectionString));
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
