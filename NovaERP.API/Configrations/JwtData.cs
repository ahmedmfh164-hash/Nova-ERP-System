namespace ERP.API.Configrations
{
    public class JwtData
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SecretKey { get; set; }
    }
}
