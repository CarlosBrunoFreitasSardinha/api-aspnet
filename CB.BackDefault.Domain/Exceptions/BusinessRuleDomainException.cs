namespace CB.BackDefault.Domain.Exceptions
{
    public sealed class BusinessRuleDomainException : DomainException
    {
        public BusinessRuleDomainException(string message) : base(message, 400)
        {
        }
    }
}
