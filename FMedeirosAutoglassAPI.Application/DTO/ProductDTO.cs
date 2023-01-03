using System;

namespace FMedeirosAutoglassAPI.Application.DTO
{
    public class ProductDTO
    {
        public int? Id { get; set; }

        public string NmProduct { get; set; }

        public bool IsActive { get; set; }

        public DateTime DtManufacture { get; set; }

        public DateTime DtExpiration { get; set; }

        public string CodeProvider { get; set; }

        public string NmProvider { get; set; }

        public string NuRegistrationProvider { get; set; }
    }
}