using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Order;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class OrderProfile: Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderGetDto>();
            CreateMap<OrderPostDto, Order>();
            CreateMap<OrderPutDto, Order>();

        }
    }
}
