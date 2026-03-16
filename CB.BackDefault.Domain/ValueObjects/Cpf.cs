using CB.BackDefault.Domain.Exceptions;
using CB.BackDefault.Domain.ValueObjects.Base;

namespace CB.BackDefault.Domain.ValueObjects;

public sealed class Cpf : ValueObject
{
    public string Numero { get; }

    private Cpf(string numero)
    {
        Numero = numero;
    }

    public static Cpf Create(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ValidationDomainException("Documento inválido.");

        var cleaned = new string(numero.Where(char.IsDigit).ToArray());

        if (cleaned.Length != 11)
            throw new ValidationDomainException("CPF deve conter 11 dígitos.");

        return new Cpf(cleaned);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Numero;
    }

    public override string ToString() => Numero;
}