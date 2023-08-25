using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Project;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class ProjectProfile : Profile
    {            
        public   ProjectProfile()
        {
            CreateMap<Project, ProjectGetDto>();

            CreateMap<ProjectPostDto, Project>();


            CreateMap<ProjectPutDto, Project>();
        }
    }
}
