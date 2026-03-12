namespace CB.BackDefault.Application.Aggregates.AuthAggregate.ViewModels.Response
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public AuthResponse() { }
        public AuthResponse(string token, DateTime dateTime)
        {
            this.Token = token;
            this.Expiration = dateTime;
        }
    }
}
