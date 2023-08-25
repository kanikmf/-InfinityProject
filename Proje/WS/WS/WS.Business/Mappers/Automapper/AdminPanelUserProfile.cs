using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.AdminPanelUser;
using WS.Model.Entities;

namespace WS.Business.Mappers.AutoMapper
{
    public class AdminPanelUserProfile : Profile
    {
        public AdminPanelUserProfile()
        {


            CreateMap<AdminPanelUser, AdminPanelUserDto>();


        }

    }
}
