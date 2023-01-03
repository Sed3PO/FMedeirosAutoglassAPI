using FMedeirosAutoglassAPI.Application.DTO;
using FMedeirosAutoglassAPI.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMedeirosAutoglassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationAuth _applicationAuth;

        public AuthController(IApplicationAuth applicationAuth)
        {
            _applicationAuth = applicationAuth;
        }

        [AllowAnonymous]
        [HttpPost("GenerateToken")]
        public ActionResult<ReturnDTO> GenerateToken(string secret)
        {
            try
            {
                ReturnDTO returnDTO = _applicationAuth.GenerateToken(secret);

                if (returnDTO != null)
                {
                    return new OkObjectResult(returnDTO);
                }

                return new NotFoundObjectResult(new ReturnDTO(false, "Falha ao autenticar.", null));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ReturnDTO(false, "Falha ao autenticar.", ex));
            }
        }
    }
}