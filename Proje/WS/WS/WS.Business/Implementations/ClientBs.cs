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
using WS.Model.Dtos.Client;
using WS.Model.Dtos.Departman;
using WS.Model.Entities;

namespace WS.Business.Implementations
{
    public class ClientBs : IClientBs
    {
        private readonly IClientRepository _repo;
        private readonly IMapper _mapper;
        
        public ClientBs (IClientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            {
                if (id <= 0)
                    throw new BadRequestException("id pozitif bir değer olmalıdır");
                var entity = await _repo.GetByIdAsync(id);
                entity.IsActive = false;
                await _repo.UpdateAsync(entity);

                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
        }

        public async Task<ApiResponse<ClientGetDto>> GetByIdAsync(int clientId, params string[] includeList)
        {
            if (clientId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır.");

            var departman = await _repo.GetByIdAsync(clientId, includeList);

            if (departman == null)
                throw new NotFoundException("bu id li ürün bulunamadı");

            var dto = _mapper.Map<ClientGetDto>(departman);

            return ApiResponse<ClientGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<ClientGetDto>>> GetClientsAsync(params string[] includeList)
        {

            var clients = await _repo.GetAllAsync(p => p.IsActive == true, includeList: includeList);

            if (clients.Count > 0)
            {
                var returnList = _mapper.Map<List<ClientGetDto>>(clients);

                return ApiResponse<List<ClientGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            return ApiResponse<List<ClientGetDto>>.Fail(StatusCodes.Status404NotFound, "kaynak bulunamadı");
        }

        public async Task<ApiResponse<Client>> InsertAsync(ClientPostDto dto)
        {
            if (dto.ClientName.Length < 1)
                throw new BadRequestException("Müşteri adı en az 1 karakterden oluşmalıdır");

            var entity = _mapper.Map<Client>(dto);
            entity.IsActive = true;

            var insertedDepartman = await _repo.InsertAsync(entity);

            return ApiResponse<Client>.Success(StatusCodes.Status201Created, insertedDepartman);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(ClientPutDto dto)
        {
            if (dto.ClientID <= 0)

                throw new BadRequestException("id pozitif bir değer olmalıdır");


            if (dto.ClientName.Length < 1)

                throw new BadRequestException("Müşteri Adı en az 1 karakterden oluşmalıdır");

            var entity = _mapper.Map<Client>(dto);
            entity.IsActive = true;

            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
