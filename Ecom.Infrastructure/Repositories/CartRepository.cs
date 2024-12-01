using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class CartRepository:ICart
    {
            private readonly IMongoCollection<Cart> _carts;
        
        public CartRepository(string databaseName, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Ecom");
            _carts = database.GetCollection<Cart>("Carts");

        }




         public Cart GetCartByUserEmailAsync(string userEmail)
        {
            return  _carts.Find(c => c.UserEmail == userEmail).FirstOrDefault();
        }

        public Task CreateCartAsync(Cart cart) 
        {
            _carts.InsertOne(cart);

            return Task.CompletedTask;

        }

        public Task AddItemCart(string useremail, CartItem cartItem)
        {
            var Cart = GetCartByUserEmailAsync(useremail);
           

             Cart.AddItem(cartItem);

            UpdateCartAsync(Cart);



            return Task.CompletedTask;

        }


        public  Task UpdateCartAsync(Cart cart) 
        {
            var filter = Builders<Cart>.Filter.Eq(u => u.Id, cart.Id);
            var update = Builders<Cart>.Update
                .Set(u => u.Items, cart.Items)
                .Set(u=>u.Price,cart.Price);

         

           
            _carts.UpdateOne(filter, update);


            return Task.CompletedTask;
         }

        public Task DeleteCartItemAsync(string useremail,string ItemId)
        {



            var Cart=_carts.Find(c => c.UserEmail ==useremail).FirstOrDefault();

           
               if(Cart != null)
            {
                Cart.RemoveItem(ItemId);

                UpdateCartAsync(Cart);
            }
           
            
          
            return Task.CompletedTask;


        }


    }
}
