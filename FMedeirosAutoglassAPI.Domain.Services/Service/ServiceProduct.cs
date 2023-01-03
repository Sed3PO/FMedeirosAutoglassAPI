using FMedeirosAutoglassAPI.Domain.Core.Interface.Repository;
using FMedeirosAutoglassAPI.Domain.Core.Interface.Service;
using FMedeirosAutoglassAPI.Domain.Entity;
using System.Collections.Generic;

namespace FMedeirosAutoglassAPI.Domain.Services.Service
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IRepositoryProduct _repositoryProduct;

        public ServiceProduct(IRepositoryProduct repository)
        {
            _repositoryProduct = repository;
        }

        public Product GetProductById(int idProduct)
        {
            return _repositoryProduct.GetProductById(idProduct);
        }

        public List<Product> GetProduct()
        {
            return _repositoryProduct.GetProduct();
        }

        public void InsertProduct(Product product)
        {
            _repositoryProduct.InsertProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _repositoryProduct.UpdateProduct(product);
        }

        public void RemoveProduct(int idProduct)
        {
            _repositoryProduct.RemoveProduct(idProduct);
        }
    }
}