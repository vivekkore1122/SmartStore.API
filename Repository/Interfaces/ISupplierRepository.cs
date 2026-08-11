using SmartStore.API.Models.Domain;

namespace SmartStore.API.Repository.Interfaces;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAllAsync();

    Task<Supplier?> GetByIdAsync(int id);

    Task<Supplier> CreateAsync(Supplier supplier);

    Task<Supplier?> UpdateAsync(Supplier supplier);

    Task<Supplier?> DeleteAsync(int id);
}