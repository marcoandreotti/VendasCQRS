using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using MongoDB.Bson;

namespace Domain.Profiles;

public sealed class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<SaleContract, SaleEntity>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => ObjectId.GenerateNewId()))
            .ForMember(d => d.Status, o => o.Ignore())
            .ForMember(d => d.TotalSalePrice, o => 
                o.MapFrom(s => s.Products != null ? s.Products.Where(x => x.Status != 2).Sum(x => x.Quantity * (x.UnitPrice - x.Discount)) : 0));

        CreateMap<ProductContract, ProductEntity>()
            .ForMember(d => d.Status, o => o.Ignore());

        CreateMap<CustomerContract, CustomerEntity>();

    }
}
