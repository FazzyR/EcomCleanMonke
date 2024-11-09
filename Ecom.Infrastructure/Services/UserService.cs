using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
namespace Ecom.Infrastructure.Services
{
    public class UserService
    {


        public UserService()
        {
            
        }



        public string HashPassword(string password)
        {
        
        
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        public bool VerifyPasswordHas(string inputpassword,string storedhash)
        {

            return BCrypt.Net.BCrypt.Verify(inputpassword, storedhash);
        
        }


    }
}
