namespace API.BackEnd.DTOS.Auth
{
    public class AuthResultDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
