using AutoMapper;
using StoreManager.BLL.Models;
using StoreManager.DAL.Entities;

namespace StoreManager.PL.API.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemModel>().ReverseMap();
            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Review, ReviewModel>().ReverseMap();
        }
    }
}
