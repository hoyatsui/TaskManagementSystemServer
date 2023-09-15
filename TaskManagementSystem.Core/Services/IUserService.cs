using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core.Services
{
    public interface IUserService
    {
        Task<UserDTO> RegisterAsync(UserDTO userDTO);
        Task<string> LoginAsync(UserDTO userDTO); //return jwt token if successful
    }
}
