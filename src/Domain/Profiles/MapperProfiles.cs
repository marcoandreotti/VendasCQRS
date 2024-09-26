using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using Domain.Features.Commands.UpdateSale;
using MongoDB.Bson;

namespace Domain.Profiles;

public sealed class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<SaleContract, SaleEntity>()
            .ForMember(d => d.Id, o => o.MapFrom(s => ObjectId.GenerateNewId()))
            .ForMember(d => d.Status, o => o.MapFrom(s => (int)SaleStatusEnum.CompraCriada))
            .ForMember(d => d.TotalSalePrice, o =>
                o.MapFrom(s => s.Products != null ? s.Products.Sum(x => x.Quantity * (x.UnitPrice - x.Discount)) : 0))
            .ForMember(d => d.Customer, o => o.MapFrom(s => new CustomerContract(s.CustomerId)));

        CreateMap<UpdateSaleCommand, SaleEntity>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Status, o => o.MapFrom(s => (int)SaleStatusEnum.CompraCriada))
            .ForMember(d => d.TotalSalePrice, o =>
                o.MapFrom(s => s.Products != null ? s.Products.Sum(x => x.Quantity * (x.UnitPrice - x.Discount)) : 0))
            .ForMember(d => d.Customer, o => o.MapFrom(s => new CustomerContract(s.CustomerId)));

        CreateMap<SaleEntity, SaleQueryContract>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToEnum<SaleStatusEnum>()));

        CreateMap<ProductContract, ProductEntity>()
            .ForMember(d => d.Status, o => o.MapFrom(s => (int)SaleItemStatusEnum.ItemCriado));

        CreateMap<ProductQueryContract, ProductEntity>().ReverseMap()
            .ForMember(d => d.Status, o => o.MapFrom(s => (int)SaleItemStatusEnum.ItemCriado));

        CreateMap<CustomerContract, CustomerEntity>().ReverseMap();

    }
}
