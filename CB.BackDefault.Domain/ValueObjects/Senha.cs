using CB.BackDefault.Domain.Exceptions;
using CB.BackDefault.Domain.ValueObjects.Base;

namespace CB.BackDefault.Domain.ValueObjects;

public sealed class Senha : ValueObject
{
    public string HashedValue { get; }

    private Senha(string hashedValue)
    {
        HashedValue = hashedValue;
    }

    public static Senha Create(string rawSenha)
    {
        if (string.IsNullOrWhiteSpace(rawSenha))
            throw new ValidationException("Senha não pode ser vazia.");

        if (rawSenha.Length < 8)
            throw new ValidationException("Senha deve ter no mínimo 8 caracteres.");

        // Exemplo simples — em produção use BCrypt/Argon2
        var hashed = BCrypt.Net.BCrypt.HashPassword(rawSenha);

        return new Senha(hashed);
    }

    public bool Verify(string rawSenha)
        => BCrypt.Net.BCrypt.Verify(rawSenha, HashedValue);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return HashedValue;
    }
}