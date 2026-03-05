namespace CB.BackDefault.Domain.Exceptions
{
    public sealed class NotFoundException : DomainException
    {
        public NotFoundException(string entityName, object key)
            : base($"{entityName} com chave '{key}' não encontrado.")
        {
        }
    }
}
