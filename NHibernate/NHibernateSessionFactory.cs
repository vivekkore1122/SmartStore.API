using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SmartStore.API.NHibernate.Mappings;

using NHibernateSession = global::NHibernate.ISession;
using MicrosoftDataSqlClientDriver =
    global::NHibernate.Driver.MicrosoftDataSqlClientDriver;

namespace SmartStore.API.NHibernate;

public class NHibernateSessionFactory
{
    private readonly ISessionFactory sessionFactory;

    public NHibernateSessionFactory(IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("SmartStoreConnectionString");

        sessionFactory = Fluently.Configure()
            .Database(
                MsSqlConfiguration.MsSql2012
                    .Driver<MicrosoftDataSqlClientDriver>()
                    .ConnectionString(connectionString)
                    .ShowSql()
            )
            .Mappings(mappings =>
            {
                mappings.FluentMappings
                    .Add<ProductMap>()
                    .Add<CategoryMap>()
                    .Add<SupplierMap>();
            })
            .BuildSessionFactory();
    }

    public NHibernateSession OpenSession()
    {
        return sessionFactory.OpenSession();
    }
}