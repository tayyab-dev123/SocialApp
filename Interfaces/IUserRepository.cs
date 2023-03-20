using SocialApp.DTOs;
using SocialApp.Entities;

namespace SocialApp.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser User);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByNameAsync(string UserName);
        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<MemberDTO> GetMemberByUsernameAsync(string username);
    }
}