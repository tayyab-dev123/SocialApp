using Microsoft.EntityFrameworkCore;
using SocialApp.Entities;
using SocialApp.Interfaces;

namespace SocialApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
         async Task<AppUser> IUserRepository.GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

         async Task<AppUser> IUserRepository.GetUserByNameAsync(string UserName)
        {
             return await _context.Users.Include(p=>p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == UserName);
        }

         async Task<IEnumerable<AppUser>> IUserRepository.GetUsersAsync()
        {
            return await _context.Users.Include(p => p.Photos).ToListAsync();
        }

         async Task<bool> IUserRepository.SaveChangesAsync()
        {
            // Value should be graeter than  0 
            return await _context.SaveChangesAsync()>0;
        }

         void IUserRepository.Update(AppUser User)
        {
             _context.Entry(User).State =EntityState.Modified;
        }
    }
}
