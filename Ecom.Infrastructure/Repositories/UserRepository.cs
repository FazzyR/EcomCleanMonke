using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class UserRepository :IUser
    {
        private readonly IMongoCollection<User> _users;
        private readonly string Key;

        public UserRepository(string databaseName, IMongoClient mongoClient, string key)
        { 
            var database=mongoClient.GetDatabase("Ecom");
            _users = database.GetCollection<User>("Users");
            Key = key;
        
        }


        public string Authenticate(string email, string password)
        {

                var user = _users.Find(x => x.Email == email).FirstOrDefault();

            if (user == null ||!VerifyPasswordHash(password,user.PasswordHash))
            {
                return null; 
            }   

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey=Encoding.ASCII.GetBytes(Key);


            var claims = new List<Claim>
                 {
                       new Claim(ClaimTypes.Email, email)
                     };

            if (user.Roles != null)
            {
                claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials
                (
                 new SymmetricSecurityKey(tokenKey),
                 SecurityAlgorithms.HmacSha256Signature
                    
                    )
            };

            var token =tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);


        }

        public List<User> GetUsers()=> _users.Find(User=>true).ToList();

        public User GetUserById(string id)=> _users.Find(User=>User.Id==id).FirstOrDefault();

        public User CreateUser(User user, List<string>? roles = null)
        {
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = HashPassword(user.PasswordHash),
                Roles = roles ?? new List<string>() // Assign roles if provided, otherwise keep it empty

            }; 
            _users.InsertOne(newUser);
            
            return user;
        }


        public void UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, user.Email);
            var update = Builders<User>.Update
                .Set(u => u.Username, user.Username)
                .Set(u => u.PasswordHash, user.PasswordHash)
                .Set(u => u.Roles, user.Roles);
            _users.UpdateOne(filter, update);
        }



        public string HashPassword(string password)
        {


            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        public bool VerifyPasswordHash(string inputpassword, string storedhash)
        {

            return BCrypt.Net.BCrypt.Verify(inputpassword, storedhash);

        }

      
    }
}
