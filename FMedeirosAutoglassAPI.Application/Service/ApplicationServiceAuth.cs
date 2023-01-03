using FMedeirosAutoglassAPI.Application.DTO;
using FMedeirosAutoglassAPI.Application.Interface;
using FMedeirosAutoglassAPI.Domain.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FMedeirosAutoglassAPI.Application.Service
{
    public class ApplicationServiceAuth : IApplicationAuth
    {
        private readonly AppSettings _appSettings;

        public ApplicationServiceAuth(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public ReturnDTO GenerateToken(string secret)
        {
            ReturnDTO returnDTO = this.ValidateSecret(secret);

            if (returnDTO.IsSuccess)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                DateTime dtExpiration = DateTime.UtcNow.AddMinutes(60);
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim("Secret", secret)
                    });

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = dtExpiration,
                    NotBefore = DateTime.UtcNow,
                    Subject = claimsIdentity,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                Token token = new Token(tokenHandler.WriteToken(securityToken), dtExpiration);

                bool isSuccess = token != null;

                returnDTO.DeResult = token;
                returnDTO.DeMessage = isSuccess ? "Token gerado com sucesso." : "Falha ao gerar token.";
                returnDTO.IsSuccess = isSuccess;
            }

            return returnDTO;
        }

        private ReturnDTO ValidateSecret(string secret)
        {
            ReturnDTO returnDTO = new ReturnDTO();

            bool isSuccess = false;

            if (!string.IsNullOrEmpty(secret))
            {
                isSuccess = secret.Equals(_appSettings.Secret);
            }

            returnDTO.IsSuccess = isSuccess;
            returnDTO.DeMessage = isSuccess ? "Senha validada com sucesso." : "Falha ao validar senha.";

            return returnDTO;
        }
    }
}