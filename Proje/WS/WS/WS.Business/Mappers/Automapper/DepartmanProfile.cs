using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Departman;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class DepartmanProfile : Profile
    {
        public DepartmanProfile()
        {
            CreateMap<Departman, DepartmanGetDto>().ForMember
             (dest => dest.DepartmanName, opt => opt.MapFrom(src => src.DepartmanName == null ? "" : src.DepartmanName.ToUpper()));

            CreateMap<DepartmanPostDto, Departman>();


            CreateMap<DepartmanPutDto, Departman>();
        }   
    }
}
