using Ecom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Application.Interfaces
{
    public interface IOrder
    {

        public List<Order> GetAllOrders();


        public Order GetOrderById();


        public Task CreateOrder();

        public Task CancelOrder();



    }
}
