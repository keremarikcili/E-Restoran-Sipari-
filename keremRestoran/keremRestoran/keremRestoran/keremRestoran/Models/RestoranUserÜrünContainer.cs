using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keremRestoran.Models
{
    public class RestoranUserÜrünContainer
    {
        public User user { get; set; }
        public List<RestoranÜrün> RestoranUserÜrünList{ get; set; }
    }
}
