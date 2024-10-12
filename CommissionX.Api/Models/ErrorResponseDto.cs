namespace CommissionX.Api.Models
{
    public class ErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Detailed { get; set; }
    }
}