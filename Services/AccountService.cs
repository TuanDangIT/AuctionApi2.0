using AuctionApi.MappingServices;
using AuctionApi.Models;
using AuctionApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuctionApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly AuctionDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(AuctionDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context= context;
            _passwordHasher= passwordHasher;
            _authenticationSettings= authenticationSettings;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = UserServiceMapper.MapToUser(dto);
            newUser.Password = _passwordHasher.HashPassword(newUser, dto.Password);
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public string GetJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer, 
                _authenticationSettings.JwtIssuer, 
                claims: claims, 
                expires: expires, 
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
        public List<UserDto> GetAll()
        {
            var users = _context.Users
                .Include(u => u.Auctions)
                .ToList();
            List<UserDto> usersDto = new List<UserDto>();
            users.ForEach(user =>
            {
                usersDto.Add(UserServiceMapper.MapToUserDto(user));
            });
            return usersDto;
        }
        public UserDto GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.Auctions)
                .FirstOrDefault(u => u.Id == id);
            if (user == null) throw new NotFoundException("User not found");
            var userDto = UserServiceMapper.MapToUserDto(user);
            return userDto;
        }

         

    }
}