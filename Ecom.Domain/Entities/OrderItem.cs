using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Domain.Entities
{
    public class OrderItem
    {

        [BsonRepresentation(BsonType.ObjectId)]

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal productprice { get; set; }


    }
}
