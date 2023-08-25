using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Business.CustomExceptions;
using WS.Business.Interfaces;
using WS.DataAccess.Interfaces;
using WS.Model.Dtos.Departman;
using WS.Model.Dtos.Expense;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class ExpenseBs : IExpenseBs
    {
        private readonly IExpenseRepository _repo;
        private readonly IMapper _mapper;

        public ExpenseBs (IExpenseRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");
            var entity = await _repo.GetByIdAsync(id);
            entity.IsActive = false;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<ExpenseGetDto>> GetByIdAsync(int expenseId, params string[] includeList)
        {
            if (expenseId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır.");

            var expense = await _repo.GetByIdAsync(expenseId, includeList);

            if (expense == null)
                throw new NotFoundException("bu id ile ilgili bir harcama bulunamadı");

            var dto = _mapper.Map<ExpenseGetDto>(expense);

            return ApiResponse<ExpenseGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<ExpenseGetDto>>> GetExpenseesAsync(params string[] includeList)
        {
            var expenses = await _repo.GetAllAsync(p => p.IsActive == true, includeList: includeList);

            if (expenses.Count > 0)
            {
                var returnList = _mapper.Map<List<ExpenseGetDto>>(expenses);

                return ApiResponse<List<ExpenseGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            return ApiResponse<List<ExpenseGetDto>>.Fail(StatusCodes.Status404NotFound, "kaynak bulunamadı");
        }

        public async Task<ApiResponse<List<ExpenseGetDto>>> GetExpensesByAmountAsync(decimal min, decimal max, params string[] includeList)
        {
            if (min > max)
                throw new BadRequestException("min değer maxdan büyük olamaz");

            if (min < 0 || max < 0)
                throw new BadRequestException("Harcama miktarı pozitif değer olmalıdır");

            var expenses = await _repo.GetByAmountAsync(min, max, includeList);

            if (expenses.Count > 0)
            {
                var returnList = _mapper.Map<List<ExpenseGetDto>>(expenses);
                return ApiResponse<List<ExpenseGetDto>>.Success(200, returnList);
            }
            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<Expense>> InsertAsync(ExpensePostDto dto)
        {
            

            var entity = _mapper.Map<Expense>(dto);
            entity.IsActive = true;

            var insertedExpense = await _repo.InsertAsync(entity);

            return ApiResponse<Expense>.Success(StatusCodes.Status201Created, insertedExpense);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(ExpensePutDto dto)
        {

            if (dto.ExpenseID < 0)
            {
                throw new BadRequestException("Id pozitif bir değer olmalıdır");
            }

            if (dto.Amount <= 0)
            {
                throw new BadRequestException("Miktar pozitif bir değer olmalıdır");
            }
            var entity = _mapper.Map<Expense>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);
            entity.IsActive = true;

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
