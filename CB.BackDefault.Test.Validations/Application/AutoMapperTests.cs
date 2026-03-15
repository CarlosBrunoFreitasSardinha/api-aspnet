using AutoMapper;
using Microsoft.Extensions.Logging;
using CB.BackDefault.Application.AutoMapper;

namespace CB.BackDefault.UnitTests.Application
{
    public class AutoMapperTests
    {
        [Fact(DisplayName = "Deve Ter Configuracao Valida do AutoMAPPER")]
        [Trait("Core", "Auto Mapper Application")]
        public void Deve_Ter_ConfiguracaoValida_Do_AutoMapper()
        {
            var loggerFactory = LoggerFactory.Create(builder => { });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
            }, loggerFactory);

            config.AssertConfigurationIsValid();
        }
    }
}
