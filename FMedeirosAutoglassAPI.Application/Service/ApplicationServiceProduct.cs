using AutoMapper;
using FMedeirosAutoglassAPI.Application.DTO;
using FMedeirosAutoglassAPI.Application.Interface;
using FMedeirosAutoglassAPI.Domain.Core.Interface.Service;
using FMedeirosAutoglassAPI.Domain.Entity;
using System;
using System.Collections.Generic;

namespace FMedeirosAutoglassAPI.Application.Service
{
    public class ApplicationServiceProduct : IApplicationProduct
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IMapper _mapper;

        public ApplicationServiceProduct(IServiceProduct serviceProduct, IMapper mapper)
        {
            _serviceProduct = serviceProduct;
            _mapper = mapper;
        }

        public ReturnDTO GetProductById(int idProduct)
        {
            ReturnDTO returnDTO = this.ValidateProduct(idProduct);

            if (returnDTO.IsSuccess)
            {
                var product = _serviceProduct.GetProductById(idProduct);
                ProductDTO productDTO = _mapper.Map<ProductDTO>(product);

                bool isSuccess = productDTO != null;

                returnDTO.DeResult = productDTO;
                returnDTO.DeMessage = isSuccess ? "Produto resgatado com sucesso" : "Falha ou não encontrado Produto.";
                returnDTO.IsSuccess = isSuccess;
            }

            return returnDTO;
        }

        public ReturnDTO GetProduct(int idProduct, string nmProduct, int? nuRecordsPerPage)
        {
            ReturnDTO returnDTO = new ReturnDTO();

            var product = _serviceProduct.GetProduct();
            List<ProductDTO> lstProduct = _mapper.Map<List<ProductDTO>>(product);

            if (lstProduct != null && lstProduct.Count > 0)
            {
                lstProduct.RemoveAll(p => p.IsActive == false);

                if (idProduct > 0)
                {
                    lstProduct.RemoveAll(p => p.Id != idProduct);
                }
                if (!string.IsNullOrEmpty(nmProduct))
                {
                    lstProduct.RemoveAll(p => p.NmProduct != nmProduct);
                }
                if (nuRecordsPerPage != null && nuRecordsPerPage > 0)
                {
                    for (int i = 0; i < lstProduct.Count && lstProduct.Count > nuRecordsPerPage; i++)
                    {
                        lstProduct.RemoveRange(lstProduct.Count - 1, 1);
                    }
                }
            }

            bool isSuccess = lstProduct != null;

            returnDTO.DeResult = lstProduct;
            returnDTO.DeMessage = isSuccess ? "Produto resgatado com sucesso" : "Falha ou não encontrado nenhum Produto.";
            returnDTO.IsSuccess = isSuccess;

            return returnDTO;
        }

        public ReturnDTO InsertProduct(ProductDTO productDTO)
        {
            ReturnDTO returnDTO = this.ValidateDtManufactureProduct(productDTO);

            if (returnDTO.IsSuccess)
            {
                Product product = _mapper.Map<Product>(productDTO);

                if (product != null)
                {
                    try
                    {
                        _serviceProduct.InsertProduct(product);
                    }
                    catch (Exception ex)
                    {
                        return new ReturnDTO(false, ex.Message, ex);
                    }
                }

                returnDTO.DeResult = product;
                returnDTO.DeMessage = product != null ? "Produto inserido com sucesso" : "Falha ao inserir Produto.";
                returnDTO.IsSuccess = true;
            }

            return returnDTO;
        }

        public ReturnDTO UpdateProduct(ProductDTO productDTO)
        {
            ReturnDTO returnDTO = this.ValidateDtManufactureProduct(productDTO);

            if (returnDTO.IsSuccess)
            {
                Product product = _mapper.Map<Product>(productDTO);

                if (product != null)
                {
                    try
                    {
                        _serviceProduct.UpdateProduct(product);
                    }
                    catch (Exception ex)
                    {
                        return new ReturnDTO(false, ex.Message, ex);
                    }
                }

                returnDTO.DeResult = product;
                returnDTO.DeMessage = product != null ? "Produto atualizado com sucesso" : "Falha ao atualizar Produto.";
                returnDTO.IsSuccess = true;
            }

            return returnDTO;
        }

        public ReturnDTO RemoveProduct(int idProduct)
        {
            ReturnDTO returnDTO = new ReturnDTO();

            try
            {
                _serviceProduct.RemoveProduct(idProduct);
            }
            catch (Exception ex)
            {
                return new ReturnDTO(false, ex.Message, ex);
            }

            returnDTO.DeResult = null;
            returnDTO.DeMessage = "Produto removido com sucesso";
            returnDTO.IsSuccess = true;

            return returnDTO;
        }

        /// <summary>
        /// Necessário passar o código do Produto.
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        private ReturnDTO ValidateProduct(int idProduct)
        {
            ReturnDTO returnDTO = new ReturnDTO();

            bool isSuccess = idProduct > 0;

            returnDTO.IsSuccess = isSuccess;
            returnDTO.DeMessage = isSuccess ? "Produto validado com sucesso" : "Falha ao validar Produto.";

            return returnDTO;
        }

        /// <summary>
        /// Data de Fabricação deve ser menor que a Data de Validade.
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        private ReturnDTO ValidateDtManufactureProduct(ProductDTO productDTO)
        {
            ReturnDTO returnDTO = new ReturnDTO();
            bool isSuccess = false;

            if (productDTO != null)
            {
                isSuccess = productDTO.DtManufacture < productDTO.DtExpiration;
            }

            returnDTO.IsSuccess = isSuccess;
            returnDTO.DeMessage = isSuccess ? "Produto a ser inserido validado com sucesso." : "Falha ao validar Produto a ser inserido.";

            return returnDTO;
        }
    }
}