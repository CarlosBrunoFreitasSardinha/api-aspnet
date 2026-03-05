using CB.BackDefault.IntegrationTests.Base;
using Xunit;

namespace CB.BackDefault.IntegrationTests.Collections
{
    [CollectionDefinition(nameof(IntegrationApiCollection))]
    public class IntegrationApiCollection : ICollectionFixture<IntegrationTestsFixture<Program>>
    {
    }
}
