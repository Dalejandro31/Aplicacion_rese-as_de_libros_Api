namespace API.BackEnd.Models
{
    public class JwtSettings
    {
        public string Key { get; set; }    // tu secreto HMAC
        public string Issuer { get; set; }    // p.ej. "ReseniasLibrosAPI"
        public string Audience { get; set; }    // p.ej. "ReseniasLibrosClient"
        public int ExpirationMinutes { get; set; }    // duración del token
    }
}
