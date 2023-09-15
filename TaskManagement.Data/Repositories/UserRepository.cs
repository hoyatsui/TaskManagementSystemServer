using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagementDbContext _context;

        public UserRepository(TaskManagementDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddUserAsync(User user)
        {
            var existingUser = await _context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();
            if (existingUser != null) return null;
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user =  await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            return user;
        }
    }
}
