using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;

namespace ClinicVet.PetCare.Domain.Commands.v1.UpdatePet;

public sealed class UpdatePetCommandProfile : Profile
{
    public UpdatePetCommandProfile()
    {
        CreateMap<UpdatePetCommand, PetParameterDto>()
            .ForMember(dest => dest.PetOwnerDocument, src => src.MapFrom(opt => opt.PetOwner!.Document!.Code))
            .ForMember(dest => dest.DocumentType, src => src.MapFrom(opt => opt.PetOwner!.Document!.Type));
    }
}