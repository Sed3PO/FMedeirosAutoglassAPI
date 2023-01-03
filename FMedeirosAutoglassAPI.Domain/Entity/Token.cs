using System;

namespace FMedeirosAutoglassAPI.Domain.Entity
{
    public class Token
    {
        public Token(string deToken, DateTime dtExpiretation)
        {
            this.DeToken = deToken;
            this.DtExpiretation = dtExpiretation;
        }

        public string DeToken { get; set; }
        public DateTime DtExpiretation { get; set; }
    }
}