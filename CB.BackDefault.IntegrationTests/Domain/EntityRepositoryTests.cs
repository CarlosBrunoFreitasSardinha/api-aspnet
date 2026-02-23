using CB.BackDefault.Domain.Aggregates.AggregatesTest.Models;
using CB.BackDefault.Domain.Shared.Interfaces;
using CB.BackDefault.IntegrationTests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace CB.BackDefault.IntegrationTests.Domain
{
    public class EntityRepositoryTests : IClassFixture<IntegrationTestBase>
    {
        private readonly IntegrationTestBase _factory;

        public EntityRepositoryTests(IntegrationTestBase factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Deve_Adicionar_E_Recuperar_Entidade_Do_Banco()
        {
            using var scope = _factory.Services.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository<PersonModel>>();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var novaPessoa = new PersonModel 
            {
                Name = "Carlos Bruno"
            };

            await repository.AdicionarAsync(novaPessoa);
            await uow.CommitAsync();

            var resultado = await repository.ObterAsync(x => x.Id == novaPessoa.Id);

            Assert.NotNull(resultado);
            Assert.Equal(novaPessoa.Id, resultado.Id);
        }
    }
}
