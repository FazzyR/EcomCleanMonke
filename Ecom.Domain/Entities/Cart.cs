﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Domain.Entities
{
    public class Cart
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserEmail { get; set; } // This links the cart to a specific user

        public List<CartItem> Items { get; set; } = new List<CartItem>();


        [Required]
        public decimal Price { get; set; } = 0;


        public void AddItem(CartItem newItem)
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
