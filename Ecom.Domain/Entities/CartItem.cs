using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Domain.Entities
{
    public class CartItem
    {


        [BsonRepresentation(BsonType.ObjectId)]

        public string ProductId { get; set; }

        public int Quantity { get; set; }




    }
}
