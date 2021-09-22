using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if(await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserDummyData.json");
            
            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);
             foreach(var user in users)
             {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
                context.SaveChanges();
            }

        } 
    }
}