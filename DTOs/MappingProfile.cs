using ActividadPractica3.DTOs;
using ActividadPractica3.Models;
using AutoMapper;

namespace ActividadPractica3.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Factura, FacturaDTO>()
                .ForMember(dest => dest.FormaPago, opt => opt.MapFrom(src => src.IdFormaPagoNavigation)).ReverseMap();

            CreateMap<DetalleFactura, DetalleFacturaDTO>()
                .ForMember(dest => dest.Articulo, opt => opt.MapFrom(src => src.IdArticuloNavigation)).ReverseMap();

            CreateMap<Articulo, ArticuloDTO>().ReverseMap();
            CreateMap<FormaPago, FormaPagoDTO>().ReverseMap();

            CreateMap<FacturaCreateDTO, Factura>()
                .ForMember(dest => dest.IdFormaPagoNavigation, opt => opt.Ignore());
            CreateMap<DetalleFacturaCreateDTO, DetalleFactura>()
                .ForMember(dest => dest.IdArticuloNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.NroFacturaNavigation, opt => opt.Ignore());
        }
    }
}
