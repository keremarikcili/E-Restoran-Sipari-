using keremRestoran.Models;
using keremRestoran.Service.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keremRestoran.Service
{
    public class UserService : IUserService
    {
        public User GetUserById(string Id)
        {
            MongoHelper context = new MongoHelper();
            var user = context.kullanicilar.Find(x => x.Id == Id).FirstOrDefault();
            return user;
        }

        public User GetUserByUsername(string userName)
        {
            MongoHelper context = new MongoHelper();
            var user = context.kullanicilar.Find(x => x.userName == userName).FirstOrDefault();
            return user;
        }

        public void Insert(User user)
        {
            MongoHelper context = new MongoHelper();
            context.kullanicilar.InsertOne(user);
        }

        public Entity LoginUser(User model)
        {
            var user = GetUserByUsername(model.userName.ToLower());

            if (user == null)
            {
                model.Error = "Kullanıcı bulunamadı!";
                return model;
            }
            if(user.password != model.password)
            {
                model.Error = "Hatalı parola!";
                return model;
            }

            model.Id = user.Id;


            return model;
        }

        public Entity RegisterUser(User model)
        {
           

            if (model.password.Length < 4)
            {
                model.Error = "Şifre en az 4 karakter olmalıdır";
                return model;
            }

           

            var user = GetUserByUsername(model.userName.ToLower());
            if (user != null)
            {
                model.Error = "Kullanıcı zaten var";
                return model;
            }

            user = new User();
            user.userName = model.userName;
            user.password = model.password;
            Insert(user);


            return new Entity();
        }

        public void Update(User user)
        {
            MongoHelper context = new MongoHelper();
            context.kullanicilar.ReplaceOne<User>(k => k.Id == user.Id, user);
        }
        public Entity Delete(User user)
        {
            MongoHelper context = new MongoHelper();
            context.kullanicilar.DeleteOne<User>(k => k.userName == user.userName);
            return new Entity();
        }






    }
}
