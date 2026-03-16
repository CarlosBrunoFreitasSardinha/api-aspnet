namespace CB.BackDefault.Domain.Exceptions
{
    public sealed class ValidationDomainException : DomainException
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationDomainException( string message, Dictionary<string, string[]>? errors = null) : base(message, 400)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
}
