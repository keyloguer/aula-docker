using AutoMapper;

namespace HackatonBtp.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DtoToModelMappingProfile());
            });
        }
    }
}
