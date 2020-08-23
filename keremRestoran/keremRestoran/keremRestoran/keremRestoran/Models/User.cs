using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace keremRestoran.Models
{
    public class User : Entity
    {

        public User()
        {
            Cart = new List<ShoppingCartItem>();
        }


        [DisplayName("Username")]

        public string userName { get; set; }
        [DisplayName("Password")]

        public string password { get; set; }

        public List<ShoppingCartItem> Cart { get; set; }
        public DateTime tarih;
        public int CartItemCount
        {
            get
            {
                if (this.Cart == null)
                    return 0;

                return Cart.Sum(k => k.Quantity);
            }
        }

        public class ShoppingCartItem
        {
            public string Id { get; set; }
            public int Quantity { get; set; }

            public string Name { get; set; }
            public decimal Price { get; set; }
        }
        
    }
}
