using FMedeirosAutoglassAPI.Domain.Core.Interface.Repository;
using FMedeirosAutoglassAPI.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMedeirosAutoglassAPI.Infrastructure.Data.Repositorys
{
    public class RepositoryProduct : IRepositoryProduct
    {
        private readonly SqlContext _sqlContext;

        public RepositoryProduct(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public Product GetProductById(int idProduct)
        {
            return _sqlContext.Set<Product>().Find(idProduct);
        }

        public List<Product> GetProduct()
        {
            return _sqlContext.Set<Product>().ToList();
        }

        public void InsertProduct(Product product)
        {
            _sqlContext.Set<Product>().Add(product);
            _sqlContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _sqlContext.Entry(product).State = EntityState.Modified;
            _sqlContext.SaveChanges();
        }

        public void RemoveProduct(int idProduct)
        {
            _sqlContext.Update(_sqlContext.Product.Find(idProduct)).State = EntityState.Unchanged;
            _sqlContext.SaveChanges();
        }
    }
}