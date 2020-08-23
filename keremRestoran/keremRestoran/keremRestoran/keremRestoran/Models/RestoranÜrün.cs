using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keremRestoran.Models
{
    public class RestoranÜrün : Entity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string GetPath()
        {
            return "/images/Products/" + this.Id + ".jpg";

        }

        public string Category { get; set; }

        public string tanım { get; set; }
        public DateTime tarih { get; set; }
    }
}
