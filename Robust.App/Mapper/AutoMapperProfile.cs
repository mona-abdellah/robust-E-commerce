using AutoMapper;
using Robust.DTO.Category;
using Robust.DTO.Order;
using Robust.DTO.OrderItem;
using Robust.DTO.Products;
using Robust.DTO.User;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<GetProductDTO, Product>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<GetCategoryDTO, Category>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<GetOrderDTO, Order>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<GetOrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<RegisterDTO,User>().ReverseMap();
            CreateMap<LoginDTO,User>().ReverseMap();
            CreateMap<AuthResponceDTO, User>().ReverseMap();
        }
    }
}
