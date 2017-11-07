using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.Pledges.AzureRepositories;
using Lykke.Service.Pledges.Core.Domain;
using Lykke.Service.Pledges.Core.Services;
using Lykke.Service.Pledges.Core.Settings.ServiceSettings;
using Lykke.Service.Pledges.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;
using System;
using Lykke.Service.ClientAccount.Client.AutorestClient;

namespace Lykke.Service.Pledges.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<PledgesSettings> _settings;
        private readonly ILog _log;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public ServiceModule(IReloadingManager<PledgesSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            // TODO: Do not register entire settings in container, pass necessary settings to services which requires them
            // ex:
            //  builder.RegisterType<QuotesPublisher>()
            //      .As<IQuotesPublisher>()
            //      .WithParameter(TypedParameter.From(_settings.CurrentValue.QuotesPublication))

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            builder.RegisterInstance<IPledgeRepository>(
                    AzureRepoFactories.CreatePledgeRepository(_settings.Nested(x => x.Db.PledgesConnString), _log))
                .SingleInstance();

            builder.RegisterType<ClientAccountService>()
                .As<IClientAccountService>()
                .WithParameter("baseUri", new Uri(_settings.CurrentValue.Services.ClientAccountServiceUrl));

            builder.RegisterType<ClientAccountClient>()
                .As<IClientAccountClient>()
                .WithParameter("serviceUrl", _settings.CurrentValue.Services.ClientAccountServiceUrl)
                .WithParameter("log", _log);

            builder.RegisterType<PledgesService>()
                .As<IPledgesService>()
                .SingleInstance();

            builder.Populate(_services);
        }
    }
}
