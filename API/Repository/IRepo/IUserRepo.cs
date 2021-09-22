using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Repository.IRepo
{
    public interface IUserRepo
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUserAsync();

        Task<AppUser> GetUsByIDAsync(int id);

        Task<AppUser> GetUserByNameAsync(string username);

        Task<MemberDto> GetUserDtoAsync(string username);

        Task<IEnumerable<MemberDto>> GetUsersDtoAsync();
    }
}