using CB.BackDefault.IntegrationTests.Base;
using CB.BackDefault.IntegrationTests.Collections;

namespace CB.BackDefault.IntegrationTests.Persistence
{
    [Collection(nameof(IntegrationApiCollection))]
    public class EntityRepositoryTests
    {
        private readonly IntegrationTestsFixture<Program> _fixture;

        public EntityRepositoryTests(IntegrationTestsFixture<Program> fixture)
        {
            _fixture = fixture;
        }

        //[Fact(DisplayName = "Deve registrar e Recuperar Usuario do Banco com Sucesso")]
        //[Trait("Categoria", "Testar Persistencia")]
        //public async Task Deve_Adicionar_E_Recuperar_Entidade_Do_Banco()
        //{
        //    await _fixture.ExecuteInTransactionAsync(async sp =>
        //    {
        //        var repository = sp.GetRequiredService<IRepository<RefreshTokenModel>>();
        //        var uow = sp.GetRequiredService<IUnitOfWork>();

        //        var novaPessoa = new RefreshTokenModel
        //        {
        //            Name = "Carlos Bruno"
        //        };

        //        await repository.AdicionarAsync(novaPessoa);
        //        await uow.CommitAsync();

        //        var resultado = await repository
        //            .ObterAsync(x => x.Id == novaPessoa.Id);

        //        Assert.NotNull(resultado);
        //        Assert.Equal(novaPessoa.Id, resultado.Id);
        //    });
        //}
    }
}
