using AutoMapper;
using Webjet.Backend.Common.Mappings;

namespace Webjet.Backend.UnitTests;

public class MappingTestsFixture
{
    public MappingTestsFixture()
    {
        ConfigurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = ConfigurationProvider.CreateMapper();
    }

    public IConfigurationProvider ConfigurationProvider { get; }

    public IMapper Mapper { get; }
}
