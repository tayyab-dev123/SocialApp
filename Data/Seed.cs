using System.Text;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SocialApp.Entities;

namespace SocialApp.Data
{
    public class Seed
    {
        //To update database from seed class
        //1) create static method and give parameter of DbContext Class
        //2) inside method first check if user exists
        //3) Read UserData the json file using system.io.file.readalltextasync function   
        //4) Get users using jsonserializer.deserialize<List<AppUser>>(UserData)
        //   Create foreach loop inside it do as follows
        //5) get hmac instance in variable
        //6) get user in lowercase
        //7) Add hard coded password and compute hash and create password salt
        //8) Add user in context 
        //9) SaveChanges

        public static async Task SeedUsers(DataContext context)
        {
            if(await context.Users.AnyAsync()) return;

            var UserData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(UserData);
            foreach (var user in users)
            {
                var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("tayyab"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}