using FluentNHibernate.Mapping;
using SmartStore.API.Models.Domain;

namespace SmartStore.API.NHibernate.Mappings;

public class SupplierMap : ClassMap<Supplier>
{
    public SupplierMap()
    {
        Table("Suppliers");

        Id(x => x.Id)
            .Column("Id")
            .GeneratedBy.Identity();

        Map(x => x.SupplierName)
            .Column("SupplierName")
            .Not.Nullable();

        Map(x => x.Phone)
            .Column("Phone");

        Map(x => x.Email)
            .Column("Email");
    }
}