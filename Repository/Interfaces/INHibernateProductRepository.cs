using SmartStore.API.Models.DTO;

namespace SmartStore.API.Repository.Interfaces;

public interface INHibernateProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(int id);
}