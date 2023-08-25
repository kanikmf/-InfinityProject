using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Departman;
using WS.Model.Dtos.Employee;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, EmployeeGetDto>();

            CreateMap<EmployeePostDto, Employee>();


            CreateMap<EmployeePutDto, Employee>();
        }
    }
}
