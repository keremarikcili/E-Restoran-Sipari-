using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace keremRestoran.Models
{
    public class MongoHelper
    {
        private IMongoClient client;
        private IMongoDatabase database;
        public MongoHelper()
        {
            var connectionString = "mongodb+srv://kerem:555444333@cluster0-he1eq.mongodb.net/test?retryWrites=true&w=majority";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("restaurantDB");
        }
        

        public IMongoCollection<User> kullanicilar
        {
            get { return database.GetCollection<User>("kullanicilar"); }
        }

        public IMongoCollection<RestoranÜrün> restoranürün
        {
            get { return database.GetCollection<RestoranÜrün>("restoranürün"); }
        }
    }
}
