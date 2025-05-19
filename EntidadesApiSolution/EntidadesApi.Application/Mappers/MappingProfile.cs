using AutoMapper;
using EntidadesApi.Application.DTOs;
using EntidadesApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesApi.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // EntidadGubernamental mappings
            CreateMap<EntidadGubernamental, EntidadGubernamentalDto>();
                //.ForMember(dest => dest.TipoEntidadNombre, opt => opt.MapFrom(src => src.TipoEntidad != null ? src.TipoEntidad.Nombre : null))
                //.ForMember(dest => dest.SectorNombre, opt => opt.MapFrom(src => src.Sector != null ? src.Sector.Nombre : null));

            CreateMap<EntidadGubernamentalCreateDto, EntidadGubernamental>();
            CreateMap<EntidadGubernamentalUpdateDto, EntidadGubernamental>();

            // Sector mappings
            CreateMap<Sector, SectorDto>();

            // TipoEntidad mappings
            CreateMap<TipoEntidad, TipoEntidadDto>();
        }
    }
}
