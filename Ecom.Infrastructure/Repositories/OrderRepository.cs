using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class OrderRepository:IOrder
    {

        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(string databaseName, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Ecom");
            _orders = database.GetCollection<Order>("Orders");

        }

        public  List<Order> GetAllOrders()
        {




          return  _orders.Find(product => true).ToList();
        }

        public Order GetOrderById(string id) {


            Order p = (Order)_orders.Find(p => p.Id == id);
             
            return p;

        }


        public Task CreateOrder() { 
            
            
            return Task.FromResult(0); }

        public Task CancelOrder() { 
            
            
            return Task.FromResult(0); }



    }
}
