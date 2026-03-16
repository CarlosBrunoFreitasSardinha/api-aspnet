namespace CB.BackDefault.Domain.Exceptions
{
    public sealed class NotFoundDomainException : DomainException
    {
        public NotFoundDomainException(string entityName, object key) : base($"{entityName} com chave '{key}' não encontrado.", 404)
        {
        }
    }
}
