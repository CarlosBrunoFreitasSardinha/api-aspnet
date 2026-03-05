namespace CB.BackDefault.Domain.Exceptions
{
    public sealed class ValidationException : DomainException
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationException(
            string message,
            Dictionary<string, string[]>? errors = null)
            : base(message)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
}
