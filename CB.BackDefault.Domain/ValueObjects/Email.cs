using CB.BackDefault.Domain.Exceptions;
using CB.BackDefault.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;

namespace CB.BackDefault.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Address { get; }

    private Email(string address)
    {
        Address = address;
    }

    public static Email Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ValidationDomainException("Email não pode ser vazio.");

        if (!IsValid(address))
            throw new ValidationDomainException("Email inválido.");

        return new Email(address.Trim().ToLower());
    }

    private static bool IsValid(string email)
    {
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Address;
    }

    public override string ToString() => Address;
}