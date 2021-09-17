using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register( [FromForm] RegisterDto registerDto)
        {
            if(await UserExists(registerDto.UserName))
            {
                return BadRequest("User Name is taken");
            }
            // for disposing
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new ObjectResult(new
            {
                message = "Account  was created successful",
                Id = user.Id,
                userName = user.UserName
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult>Login([FromForm] LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
            if(user == null){
                return Unauthorized("Invalid User Name");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            
            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]){
                    return Unauthorized("Invalid Password");
                }
            }
            return new ObjectResult(new
            {
                message = "Login was successful",
                UserId = user.Id,
                userName = user.UserName
                

                // access_token = token.AccessToken,
                // token_type = token.TokenType,
                // expires_in = token.ExpiresIn,


                /* expires_in = token.ExpiresIn,
                 token_type = token.TokenType,
                 creation_Time = token.ValidFrom,
                 expiration_Time = token.ValidTo,*/
            });

        }

        private async Task<bool>UserExists(string username){
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}