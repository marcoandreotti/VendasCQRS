using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using Domain.Features.Commands.UpdateBuy;
using MongoDB.Bson;

namespace Domain.Profiles;

public sealed class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<BuyContract, BuyEntity>()
            .ForMember(d => d.Id, o => o.MapFrom(s => ObjectId.GenerateNewId()))
            .ForMember(d => d.Status, o => o.MapFrom(s => (int)BuyStatusEnum.CompraCriada))
            .ForMember(d => d.TotalBuyPrice, o =>
                o.MapFrom(s => s.Products != null ? s.Products.Sum(x => x.Quantity * (x.UnitPrice - x.Discount)) : 0))
            .ForMember(d => d.Customer, o => o.MapFrom(s => new CustomerContract(s.CustomerId)));

        CreateMap<UpdateBuyCommand, BuyEntity>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Status, o => o.MapFrom(s => (int)BuyStatusEnum.CompraCriada))
            .ForMember(d => d.TotalBuyPrice, o =>
                o.MapFrom(s => s.Products != null ? s.Products.Sum(x => x.Quantity * (x.UnitPrice - x.Discount)) : 0))
            .ForMember(d => d.Customer, o => o.MapFrom(s => new CustomerContract(s.CustomerId)));

        CreateMap<BuyEntity, BuyQueryContract>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToEnum<BuyStatusEnum>()));

        CreateMap<ProductContract, ProductEntity>()
            .ForMember(d => d.Status, o => o.MapFrom(s => (int)BuyItemStatusEnum.ItemCriado));

        CreateMap<ProductQueryContract, ProductEntity>().ReverseMap()
            .ForMember(d => d.Status, o => o.MapFrom(s => (int)BuyItemStatusEnum.ItemCriado));

        CreateMap<CustomerContract, CustomerEntity>().ReverseMap();

    }
}
