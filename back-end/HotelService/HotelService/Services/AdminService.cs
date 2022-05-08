using HotelService.Database;
using HotelService.Models;
using HotelService.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelService.Repository
{
    public class AdminService
    {

        private readonly ApplicationDbContext _context;
        private readonly CipherService _cipherService;
        private readonly JWT _jwtSecret;
        public AdminService(ApplicationDbContext context, CipherService cipherService, IOptions<JWT> jwtSecret)
        {
            _context = context;
            _cipherService = cipherService;
            _jwtSecret = jwtSecret.Value;
        }

        public async Task CreateAdminAsync(HotelAdmin admin)
        {
            admin.Password = _cipherService.Encrypt(admin.Password);
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Admins.SingleOrDefault(x => x.Username == model.Username);
           
            // return null if user not found
            if (user == null) return null;

            string decryptedPassword = _cipherService.Decrypt(user.Password);

            //return null if login password is incorrect
            if (model.Password != decryptedPassword) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private string generateJwtToken(HotelAdmin hotelAdmin)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", hotelAdmin.HotelId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
