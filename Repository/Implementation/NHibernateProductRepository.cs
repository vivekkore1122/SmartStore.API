using NHibernate.Linq;
using SmartStore.API.Models.Domain;
using SmartStore.API.Models.DTO;
using SmartStore.API.NHibernate;
using SmartStore.API.Repository.Interfaces;

namespace SmartStore.API.Repository.Implementation;

public class NHibernateProductRepository : INHibernateProductRepository
{
    private readonly NHibernateSessionFactory sessionFactory;

    public NHibernateProductRepository(
        NHibernateSessionFactory sessionFactory)
    {
        this.sessionFactory = sessionFactory;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        using var session = sessionFactory.OpenSession();

        var products = await session.Query<Product>()
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ProductCode = p.ProductCode,
                Price = p.Price,
                Quantity = p.Quantity,
                CategoryId = p.Category.Id,
                SupplierId = p.Supplier.Id
            })
            .ToListAsync();

        return products;
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        using var session = sessionFactory.OpenSession();

        var product = await session.Query<Product>()
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ProductCode = p.ProductCode,
                Price = p.Price,
                Quantity = p.Quantity,
                CategoryId = p.Category.Id,
                SupplierId = p.Supplier.Id
            })
            .FirstOrDefaultAsync();

        return product;
    }
}