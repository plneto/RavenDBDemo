namespace RavenDbDemo.Api.AutofacModules
{
    using Autofac;
    using Raven.Client.Documents;
    using Raven.Client.Documents.Session;
    using Raven.Client.ServerWide;
    using Raven.Client.ServerWide.Operations;
    using Settings;

    public class RavenModule : Module
    {
        private readonly RavenDbSettings _setting;

        public RavenModule(RavenDbSettings setting)
        {
            _setting = setting;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx =>
                {
                    var store = new DocumentStore
                    {
                        Urls = new[]
                        {
                            _setting.Endpoint
                        },
                        Database = _setting.Database
                    };

                    store.Initialize();

                    var database = store.Maintenance.Server.Send(new GetDatabaseRecordOperation(_setting.Database));

                    if (database is null)
                    {
                        store.Maintenance.Server.Send(
                            new CreateDatabaseOperation(new DatabaseRecord(_setting.Database)));
                    }

                    return store.OpenAsyncSession();
                })
                .As<IAsyncDocumentSession>();
        }
    }
}