using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Client;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class ClientProfile : Profile
    {
        public  ClientProfile()
        {
            CreateMap<Client, ClientGetDto>();

            CreateMap<ClientPostDto, Client>();
               
                                 
            CreateMap<ClientPutDto, Client>();
        }
    }
}
