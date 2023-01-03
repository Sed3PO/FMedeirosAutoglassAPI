using FMedeirosAutoglassAPI.Application.DTO;
using FMedeirosAutoglassAPI.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMedeirosAutoglassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IApplicationProduct _applicationProduct;

        public ProductController(IApplicationProduct applicationProduct)
        {
            _applicationProduct = applicationProduct;
        }

        /// <summary>
        /// Método para retornar apenas um produto de acordo com o seu código.
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        [Route("GetProductById")]
        [Authorize]
        [HttpGet]
        public ActionResult<ReturnDTO> GetProductById(int idProduct)
        {
            try
            {
                ReturnDTO returnDTO = _applicationProduct.GetProductById(idProduct);

                if (returnDTO != null)
                {
                    return new OkObjectResult(returnDTO);
                }

                return new NotFoundObjectResult(new ReturnDTO(false, "Falha ao buscar produto.", null));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ReturnDTO(false, "Falha ao buscar produto.", ex));
            }
        }

        /// <summary>
        /// Lista os produtos disponíveis de acordo com os filtros enviados.
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        [Route("GetProduct")]
        [Authorize]
        [HttpGet]
        public ActionResult<ReturnDTO> GetProduct(int pIdProduct, string pNmProduct, int? pNuRecordsPerPage)
        {
            try
            {
                ReturnDTO returnDTO = _applicationProduct.GetProduct(pIdProduct, pNmProduct, pNuRecordsPerPage);

                if (returnDTO != null)
                {
                    return new OkObjectResult(returnDTO);
                }

                return new NotFoundObjectResult(new ReturnDTO(false, "Falha ao buscar produto.", null));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ReturnDTO(false, "Falha ao buscar produto.", ex));
            }
        }

        /// <summary>
        /// Método para inserir o Produto
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        [Route("InsertProduct")]
        [Authorize]
        [HttpPost]
        public ActionResult<ReturnDTO> InsertProduct([FromBody] ProductDTO product)
        {
            try
            {
                ReturnDTO returnDTO = _applicationProduct.InsertProduct(product);

                if (returnDTO != null)
                {
                    return new OkObjectResult(returnDTO);
                }

                return new NotFoundObjectResult(new ReturnDTO(false, "Falha ao inserir produto.", null));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ReturnDTO(false, "Falha ao inserir produto.", ex));
            }
        }

        /// <summary>
        /// Método para atualizar algum produto existente
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        [Route("UpdateProduct")]
        [Authorize]
        [HttpPost]
        public ActionResult<ReturnDTO> UpdateProduct([FromBody] ProductDTO product)
        {
            try
            {
                ReturnDTO returnDTO = _applicationProduct.UpdateProduct(product);

                if (returnDTO != null)
                {
                    return new OkObjectResult(returnDTO);
                }

                return new NotFoundObjectResult(new ReturnDTO(false, "Falha ao atualizar produto.", null));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ReturnDTO(false, "Falha ao atualizar produto.", ex));
            }
        }

        /// <summary>
        /// Método para remover algum produto existente
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        [Route("RemoveProduct")]
        [Authorize]
        [HttpPost]
        public ActionResult<ReturnDTO> RemoveProduct(int idProduct)
        {
            try
            {
                ReturnDTO returnDTO = _applicationProduct.RemoveProduct(idProduct);

                if (returnDTO != null)
                {
                    return new OkObjectResult(returnDTO);
                }

                return new NotFoundObjectResult(new ReturnDTO(false, "Falha ao remover produto.", null));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ReturnDTO(false, "Falha ao remover produto.", ex));
            }
        }
    }
}