using FMedeirosAutoglassAPI.Domain.Entity;
using System.Collections.Generic;

namespace FMedeirosAutoglassAPI.Domain.Core.Interface.Repository
{
    public interface IRepositoryProduct
    {
        Product GetProductById(int idProduct);

        List<Product> GetProduct();

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        void RemoveProduct(int idProduct);
    }
}