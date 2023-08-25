using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Supplier;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class SupplierProfile : Profile
    {            
        public   SupplierProfile()
        {
            CreateMap<Supplier, SupplierGetDto>();

            CreateMap<SupplierPostDto, Supplier>();


            CreateMap<SupplierPutDto, Supplier>();
        }
    }
}