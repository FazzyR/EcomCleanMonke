using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }


        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; }
        public List<string> Roles { get; set; } = new List<string>();



    }
}
