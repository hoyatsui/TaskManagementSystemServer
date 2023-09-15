using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Repositories;
using TaskManagementSystem.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaskManagement.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDTO> RegisterAsync(UserDTO userDTO)
        {
            string hasedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            User newUser = new User
            {
                Username = userDTO.Username,
                PasswordHash = hasedPassword
            };
            var addedUser = await _userRepository.AddUserAsync(newUser);
            if(addedUser == null) { return null; }
            var newUserDTO = _mapper.Map<UserDTO>(addedUser);
            return newUserDTO;
        }

        public async Task<string> LoginAsync(UserDTO userDTO)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userDTO.Username);
            if (user == null)
            {
                return null;
            }
            if (BCrypt.Net.BCrypt.Verify(userDTO.Password, user.PasswordHash))
            {
                return GenerateJwtToken(user);
            }
            return null;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("This is my custom Secret key for authentication");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                               SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}
