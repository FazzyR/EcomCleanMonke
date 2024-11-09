using Ecom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Application.Interfaces
{
    public interface IUser
    {

        public string Authenticate (string username, string password);  

        public List<User> GetUsers();



        public User GetUserById(string id);


        public User CreateUser(User user, List<string>? roles = null);

        public void UpdateUser(User user);




    }
}
