using CB.BackDefault.Domain.Exceptions;
using CB.BackDefault.Domain.ValueObjects.Base;

namespace CB.BackDefault.Domain.ValueObjects;

public sealed class Endereco : ValueObject
{
    public string Rua { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public string CEP { get; }

    private Endereco(string rua, string cidade, string estado, string cep)
    {
        Rua = rua;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
    }

    public static Endereco Create(string rua, string cidade, string estado, string cep)
    {
        if (string.IsNullOrWhiteSpace(rua))
            throw new ValidationException("Rua obrigatória.");

        if (string.IsNullOrWhiteSpace(cidade))
            throw new ValidationException("Cidade obrigatória.");

        if (string.IsNullOrWhiteSpace(estado))
            throw new ValidationException("Estado obrigatório.");

        if (string.IsNullOrWhiteSpace(cep))
            throw new ValidationException("CEP obrigatório.");

        return new Endereco(rua, cidade, estado, cep);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Rua;
        yield return Cidade;
        yield return Estado;
        yield return CEP;
    }
}