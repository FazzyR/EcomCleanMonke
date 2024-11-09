using Ecom.Application.Interfaces;
using Ecom.Domain.Dtos;
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
    public class ProductRepository:IProduct
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(string databaseName, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Ecom");
            _products = database.GetCollection<Product>("Products");

        }
        public List<Product> GetAllProducts() 
        {

            return _products.Find(product => true).ToList();


        }


        public Product CreateProduct(ProductDto product) 
        {
            var newProduct=new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = DateTime.Now,
                Category = product.Category,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,

            };

            _products.InsertOne(newProduct);

            return newProduct;
        
        }


        public Task EditProduct(string id,ProductDto product)
        {

            var filter = Builders<Product>.Filter.Eq(u => u.Id, id);

            var update = Builders<Product>.Update
                .Set(u => u.Name, product.Name)
                .Set(u => u.Description, product.Description)
                .Set(u => u.Price, product.Price)
                .Set(u=>u.Category,product.Category)
                .Set(u => u.StockQuantity, product.StockQuantity)
                .Set(u => u.UpdatedAt, DateTime.UtcNow)
                .Set(u => u.ImageUrl, product.ImageUrl)



                ;
            _products.UpdateOne(filter, update);


            return Task.CompletedTask;
        }

        public Task FindProductById(string id)
        {
            return Task.CompletedTask;
        }

        public Task DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(u => u.Name,  id);

            _products.DeleteOne(filter);

            return Task.CompletedTask;
        }

    }
}
