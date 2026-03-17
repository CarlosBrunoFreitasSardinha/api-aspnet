namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels.Response
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }

        public AuthResponse() { }
        public AuthResponse(string token, string refreshToken, DateTime dateTime)
        {
            this.Token = token;
            this.RefreshToken = refreshToken;
            this.Expiration = dateTime;
        }
    }
}
