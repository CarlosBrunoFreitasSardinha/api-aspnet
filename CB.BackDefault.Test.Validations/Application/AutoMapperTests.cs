using AutoMapper;
using CB.BackDefault.Application.AutoMapper;

namespace CB.BackDefault.UnitTests.Base
{
    public class AutoMapperTests
    {
        [Fact]
        public void Deve_Ter_ConfiguracaoValida_Do_AutoMapper()
        {

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
