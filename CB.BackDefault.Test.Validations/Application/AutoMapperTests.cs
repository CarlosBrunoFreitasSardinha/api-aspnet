using AutoMapper;
using CB.BackDefault.Application.AutoMapper;

namespace CB.BackDefault.UnitTests.Application
{
    public class AutoMapperTests
    {
        [Fact(DisplayName = "Deve Ter Configuracao Valida do AutoMAPPER")]
        [Trait("Core", "Auto Mapper Application")]
        public void Deve_Ter_ConfiguracaoValida_Do_AutoMapper()
        {
            //arrange
            var config = new MapperConfiguration(cfg => {
                //act
                cfg.AddProfile<AutoMapperProfile>();
            });

            //assert

            config.AssertConfigurationIsValid();
        }
    }
}
