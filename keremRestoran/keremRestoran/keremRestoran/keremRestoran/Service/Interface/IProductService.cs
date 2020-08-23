using keremRestoran.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keremRestoran.Service.Interface
{
    public interface IProductService
    {
        List<RestoranÜrün> GetAllProducts();

        void Insert(RestoranÜrün product);

        RestoranÜrün GetProductById(string productId);

        List<RestoranÜrün> GetProductsById(List<string> selectedIds);

        List<RestoranÜrün> GetProductsByCategory(string categoryName);
        List<RestoranÜrün> GetProductsTanım(string tanım);
        List<RestoranÜrün> GetProductsTarih(DateTime tarih);
    }
}
