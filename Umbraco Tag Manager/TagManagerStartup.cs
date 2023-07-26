

using Microsoft.Extensions.Logging;
using Our.Umbraco.TagManager.Migrations;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Umbraco_Tag_Manager
{
    public class TagManagerComponentComposer : ComponentComposer<TagManagerStartup>
    {

    }

    public class TagManagerStartup : IComponent
    {
		private readonly IScopeProvider _scopeProvider;
        private readonly IScopeAccessor _scopeAccessor;
        private readonly ILoggerFactory _logger;
        private readonly IMigrationBuilder _migrationBuilder;
        private readonly IKeyValueService _keyValueService;

        public TagManagerStartup(IScopeProvider scopeProvider,IScopeAccessor scopeAccessor, ILoggerFactory logger, IMigrationBuilder migrationBuilder,
            IKeyValueService keyValueService)
        {
            _scopeProvider = scopeProvider;
            _logger = logger;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
            _scopeAccessor = scopeAccessor;
        }

        public void Initialize()
        {
            //var migrationPlan = new MigrationPlan("UsomeTagManagerMigrationv1");
            //migrationPlan.From(string.Empty).To<InstallHelper>("UsomeTagManagerMigrationv1-db");
            //var upgrader = new Upgrader(migrationPlan);
            //upgrader.Execute(new MigrationPlanExecutor(_scopeProvider, _scopeAccessor, _logger, _migrationBuilder), _scopeProvider, _keyValueService);

        }

        public void Terminate()
        {

        }
	}
}