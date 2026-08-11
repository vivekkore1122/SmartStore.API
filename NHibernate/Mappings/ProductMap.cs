using FluentNHibernate.Mapping;
using SmartStore.API.Models.Domain;

namespace SmartStore.API.NHibernate.Mappings;

public class ProductMap : ClassMap<Product>
{
    public ProductMap()
    {
        Table("Products");

        Id(x => x.Id)
            .Column("Id")
            .GeneratedBy.Identity();

        Map(x => x.Name)
            .Column("Name")
            .Not.Nullable();

        Map(x => x.ProductCode)
            .Column("ProductCode")
            .Not.Nullable();

        Map(x => x.Price)
            .Column("Price")
            .Not.Nullable();

        Map(x => x.Quantity)
            .Column("Quantity")
            .Not.Nullable();

        References(x => x.Category)
            .Column("CategoryId")
            .Not.Nullable();

        References(x => x.Supplier)
            .Column("SupplierId")
            .Not.Nullable();
    }
}