using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;

namespace ClinicVet.PetCare.Domain.Commands.v1.UpdatePetOwner;

public sealed class UpdatePetOwnerCommandProfile : Profile
{
    public UpdatePetOwnerCommandProfile()
    {
         CreateMap<UpdatePetOwnerCommand, PetOwnerParametersDto>()
             .ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.Name))
             .ForMember(dest => dest.OwnerType, src => src.MapFrom(opt => opt.OwnerType))
             .ForMember(dest => dest.Document, src => src.MapFrom(opt => opt.Document!.Code))
             .ForMember(dest => dest.DocumentType, src => src.MapFrom(opt => opt.Document!.Type))
             .ForMember(dest => dest.Phone, src => src.MapFrom(opt => opt.Contact!.Phone))
             .ForMember(dest => dest.Street, src => src.MapFrom(opt => opt.Address.Street))
             .ForMember(dest => dest.Number, src => src.MapFrom(opt => opt.Address.Number))
             .ForMember(dest => dest.City, src => src.MapFrom(opt => opt.Address.City))
             .ForMember(dest => dest.State, src => src.MapFrom(opt => opt.Address.State))
             .ForMember(dest => dest.ZipCode, src => src.MapFrom(opt => opt.Address.ZipCode));
    }
}