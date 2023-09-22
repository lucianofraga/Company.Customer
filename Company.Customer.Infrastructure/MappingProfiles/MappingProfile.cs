using AutoMapper;

namespace Company.Customer.Persistence.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Customer, Entities.Customer>();
            CreateMap<Entities.Customer, Domain.Customer>();
        }
    }
}
