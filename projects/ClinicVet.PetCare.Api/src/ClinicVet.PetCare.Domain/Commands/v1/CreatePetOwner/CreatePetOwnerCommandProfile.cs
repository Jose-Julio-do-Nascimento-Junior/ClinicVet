using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;
using ClinicVet.PetCare.Domain.Fixeds.v1;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreatePetOwner;

public sealed class CreatePetOwnerCommandProfile : Profile
{
    public CreatePetOwnerCommandProfile()
    {
        CreateMap<CreatePetOwnerCommand, PetOwnerParametersDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.Name))
            .ForMember(dest => dest.OwnerType, src => src.MapFrom(opt => opt.OwnerType ?? PetOwnerType.Permanent))
            .ForMember(dest => dest.Document, src => src.MapFrom(opt => opt.Document!.Code))
            .ForMember(dest => dest.DocumentType, src => src.MapFrom(opt => opt.Document!.Type ?? DocumentType.CPF))
            .ForMember(dest => dest.Phone, src => src.MapFrom(opt => opt.Contact!.Phone))
            .ForMember(dest => dest.Street, src => src.MapFrom(opt => opt.Address.Street))
            .ForMember(dest => dest.Number, src => src.MapFrom(opt => opt.Address.Number))
            .ForMember(dest => dest.City, src => src.MapFrom(opt => opt.Address.City))
            .ForMember(dest => dest.State, src => src.MapFrom(opt => opt.Address.State))
            .ForMember(dest => dest.ZipCode, src => src.MapFrom(opt => opt.Address.ZipCode));
    }
}