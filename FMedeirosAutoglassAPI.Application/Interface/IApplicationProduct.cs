using FMedeirosAutoglassAPI.Application.DTO;

namespace FMedeirosAutoglassAPI.Application.Interface
{
    public interface IApplicationProduct
    {
        ReturnDTO GetProductById(int idProduct);

        ReturnDTO GetProduct(int idProduct, string nmProduct, int? nuRecordsPerPage);

        ReturnDTO InsertProduct(ProductDTO product);

        ReturnDTO UpdateProduct(ProductDTO product);

        ReturnDTO RemoveProduct(int idProduct);
    }
}