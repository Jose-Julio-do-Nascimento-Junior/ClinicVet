using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters.Responses;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters;

public sealed class GetTemporaryPetByFiltersQueryProfile : Profile
{
    public GetTemporaryPetByFiltersQueryProfile()
    {
        CreateMap<GetTemporaryPetByFiltersQuery, PetByFiltersDto>();

        CreateMap<TemporaryPetDto, PetDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.PetName))
            .ForMember(dest => dest.Specie, src => src.MapFrom(opt => opt.PetSpecie))
            .ForMember(dest => dest.Breed, src => src.MapFrom(opt => opt.PetBreed))
            .ForMember(dest => dest.BirthDate, src => src.MapFrom(opt => opt.PetBirthDate))
            .ForPath(dest => dest.PetOwner!.Document!.Code, src => src.MapFrom(opt => opt.OwnerDocument))
            .ForPath(dest => dest.PetOwner!.Document!.Type, src => src.MapFrom(opt => opt.DocType));

        CreateMap<PetDto, GetTemporaryPetByFiltersQueryResponsesDetail>();
    }
}