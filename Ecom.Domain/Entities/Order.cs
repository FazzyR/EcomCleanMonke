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
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserEmail { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();


        [Required]
        public decimal Price { get; set; } = 0;


        public void AddItem(OrderItem newItem)
        {
            Items.Add(newItem);
            Price += newItem.productprice * newItem.Quantity;
        }

        public void RemoveItem(string productId)
        {
            var itemToRemove = Items.Find(item => item.ProductId == productId);
            if (itemToRemove != null)
            {
                Price -= itemToRemove.productprice * itemToRemove.Quantity;
                Items.Remove(itemToRemove);
            }
        }
    }
}
