using Ecom.Domain.Dtos;
using Ecom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Application.Interfaces
{
    public interface IProduct
    {



        public List<Product>GetAllProducts();

        public Product CreateProduct(ProductDto product);


        public Task EditProduct(string id ,ProductDto product);

        public Task FindProductById(string id);

        public Task DeleteProduct(string id);



    }
}
