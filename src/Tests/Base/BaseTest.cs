using AutoMapper;
using Domain.Profiles;

namespace Tests.Base;

public abstract class BaseTest
{
    protected readonly IMapper _mapper;

    protected BaseTest()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapperProfiles());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        _mapper = mapper;
    }
}