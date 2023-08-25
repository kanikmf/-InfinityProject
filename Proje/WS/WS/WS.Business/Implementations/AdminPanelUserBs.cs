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
using WS.Model.Dtos.AdminPanelUser;

namespace WS.Business.Implementations
{
    public class AdminPanelUserBs : IAdminPanelUserBs
    {
        private readonly IAdminPanelUserRepository _repo;
        private readonly IMapper _mapper;
        public AdminPanelUserBs(IAdminPanelUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        public async Task<ApiResponse<AdminPanelUserDto>> LogInAsync(string userName, string password, params string[] includeList)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new BadRequestException("Kullanıcı Ve Password Kısmı Boş Bırakılamaz");

            }
            if (userName.Length <= 2)
            {
                throw new BadRequestException("Kullanıcı adı 2 karakterden büyük olmalıdır.");

            }

            var adminUser = await
                 _repo.GetByUserNameAndPasswordAsync(userName, password, includeList);

            if (adminUser == null)
            {

                throw new BadRequestException("Kullanıcı bulunamadı");
            }
            var list = _mapper.Map<AdminPanelUserDto>(adminUser);
            return ApiResponse<AdminPanelUserDto>.Success(StatusCodes.Status200OK, list);


        }
    }
}
