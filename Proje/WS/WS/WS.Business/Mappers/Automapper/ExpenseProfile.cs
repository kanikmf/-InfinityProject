using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Expense;
using WS.Model.Entities;

namespace WS.Business.Mappers.Automapper
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile() 
        {
            CreateMap<Expense, ExpenseGetDto>();
            CreateMap<ExpensePostDto, Expense>();
            CreateMap<ExpensePutDto, Expense>();
        }
    }
}
