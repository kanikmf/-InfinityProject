using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Product;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductPutDto, Product>();
            CreateMap<ProductPostDto, Product>();

        }
    }
}
