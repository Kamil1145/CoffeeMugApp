﻿using CoffeeMugApp.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMugApp.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        // dependency injection
        public ProductService(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>(settings.ProductCollectionName);
        }

        public List<Product> Get()
        {
            return _products.Find(product => true).ToList();
        }

        public Product Get(string id)
        {
            return _products.Find<Product>(product => product.Id == id).FirstOrDefault();
        }

        public Product Create(Product product)
        {
            _products.InsertOne(product);
            return product;
        }

        public void Update(string id, Product ProductIn)
        {
            _products.ReplaceOne(product => product.Id == id, ProductIn);
        }

        public void Remove(Product productIn)
        {
            _products.DeleteOne(product => product.Id == productIn.Id);
        }

        public void Remove(string id)
        {
            _products.DeleteOne(product => product.Id == id);
        }
    }
}