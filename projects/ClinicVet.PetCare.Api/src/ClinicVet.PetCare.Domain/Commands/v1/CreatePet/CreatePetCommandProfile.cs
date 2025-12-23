using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreatePet;

public sealed class CreatePetCommandProfile : Profile
{
    public CreatePetCommandProfile()
    {
        CreateMap<CreatePetCommand, PetParameterDto>()
            .ForMember(dest => dest.PetOwnerDocument, src => src.MapFrom(opt => opt.PetOwner!.Document!.Code))
            .ForMember(dest => dest.DocumentType, src => src.MapFrom(opt => opt.PetOwner!.Document!.Type));
    }
}