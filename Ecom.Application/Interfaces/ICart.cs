using Ecom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Application.Interfaces
{
    public interface ICart
    {
        Cart GetCartByUserEmailAsync(string userEmail);
        Task CreateCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);

        Task DeleteCartItemAsync(string useremail,string ItemId);
        Task AddItemCart(string useremail, CartItem cartItem);
    }
}
