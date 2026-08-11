using FluentNHibernate.Mapping;
using SmartStore.API.Models.Domain;

namespace SmartStore.API.NHibernate.Mappings;

public class CategoryMap : ClassMap<Category>
{
    public CategoryMap()
    {
        Table("Categories");

        Id(x => x.Id)
            .Column("Id")
            .GeneratedBy.Identity();

        Map(x => x.CategoryName)
            .Column("CategoryName")
            .Not.Nullable();

        Map(x => x.Description)
            .Column("Description");
    }
}