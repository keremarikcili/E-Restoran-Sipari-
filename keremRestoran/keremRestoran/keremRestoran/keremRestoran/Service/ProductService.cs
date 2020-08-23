using keremRestoran.Models;
using keremRestoran.Service.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keremRestoran.Service
{
    public class ProductService : IProductService
    {
        public List<RestoranÜrün> GetAllProducts()
        {
            MongoHelper context = new MongoHelper();
            var products = context.restoranürün.Find(new BsonDocument()).ToList();
            return products;
        }

        public RestoranÜrün GetProductById(string Id)
        {
            MongoHelper context = new MongoHelper();
            var product = context.restoranürün.Find(x => x.Id == Id).FirstOrDefault();
            return product;
        }

        public List<RestoranÜrün> GetProductsByCategory(string categoryName)
        {
            MongoHelper context = new MongoHelper();
            var products = context.restoranürün.Find(x => x.Category == categoryName).ToList();
            return products;

        }

        public List<RestoranÜrün> GetProductsById(List<string> selectedIds)
        {
            MongoHelper context = new MongoHelper();
            var builder = Builders<RestoranÜrün>.Filter;
            FilterDefinition<RestoranÜrün> filter;
            filter = builder.Eq("_id", ObjectId.Parse(selectedIds[0]));
            for (int i = 1; i < selectedIds.Count(); i++)
            {
                filter = filter | builder.Eq("_id", ObjectId.Parse(selectedIds[i]));
            }
            var products = context.restoranürün.Find<RestoranÜrün>(filter);
            return products.ToList();
        }
        public List<RestoranÜrün> GetProductsTarih(DateTime tarih)
        {
            MongoHelper context = new MongoHelper();
            var products = context.restoranürün.Find(x => x.tarih == tarih).ToList();
            return products;

        }
        public List<RestoranÜrün> GetProductsTanım(string tanım)
        {
            MongoHelper context = new MongoHelper();
            var products = context.restoranürün.Find(x => x.tanım == tanım).ToList();
            return products;

        }

        public void Insert(RestoranÜrün product)
        {
            MongoHelper context = new MongoHelper();
            context.restoranürün.InsertOne(product);
        }
    }
}
