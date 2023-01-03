namespace FMedeirosAutoglassAPI.Application.DTO
{
    public class ReturnDTO
    {
        public ReturnDTO()
        { }

        public ReturnDTO(bool isSuccess, string deMessage, object deResult)
        {
            this.IsSuccess = isSuccess;
            this.DeMessage = deMessage;
            this.DeResult = deResult;
        }

        public bool IsSuccess { get; set; }

        public string DeMessage { get; set; }

        public object DeResult { get; set; }
    }
}