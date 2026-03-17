namespace CB.BackDefault.Api.Controllers.IdentityAggregate.Request
{
    public class UpdateClaimRequest
    {
        public string Type { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
