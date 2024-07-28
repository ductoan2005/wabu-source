using AutoMapper;

namespace WABU.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingModelToViewModel>();
                x.AddProfile<MappingViewModelToModel>();
                x.AddProfile<MappingCustomModel>();
            });
        }
    }
}