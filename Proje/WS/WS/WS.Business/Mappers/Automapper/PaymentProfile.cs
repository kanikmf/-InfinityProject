using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Payment;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile() 
        {
            CreateMap<Payment, PaymentGetDto>();
            CreateMap<PaymentPutDto,Payment>();
            CreateMap<PaymentPostDto, Payment>();

        }

    }
}
