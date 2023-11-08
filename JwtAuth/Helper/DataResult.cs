namespace JwtAuth.Helper
{
    public class DataResult
    {
        public string? StatusCode { get; set; }
        public string? Message { get; set; }
        public string? ExceptionErrorMessage { get; set; }
        public Object? Data { get; set; }
    }
}
