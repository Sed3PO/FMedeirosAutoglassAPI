using FMedeirosAutoglassAPI.Application.DTO;

namespace FMedeirosAutoglassAPI.Application.Interface
{
    public interface IApplicationAuth
    {
        ReturnDTO GenerateToken(string secret);
    }
}