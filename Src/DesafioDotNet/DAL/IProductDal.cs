using DesafioDotNet.Models;
using System.Collections.Generic;

namespace DesafioDotNet.DAL
{
    public interface IProductDal
    {
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        Product GetProduct(int? id);
        void DeleteProduct(int? id);
    }
}
