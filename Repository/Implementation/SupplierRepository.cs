using Microsoft.EntityFrameworkCore;
using SmartStore.API.Data;
using SmartStore.API.Models.Domain;
using SmartStore.API.Repository.Interfaces;

namespace SmartStore.API.Repository.Implementation;

public class SupplierRepository : ISupplierRepository
{
    private readonly SmartStoreDbContext dbContext;

    public SupplierRepository(SmartStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await dbContext.Suppliers
            .ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await dbContext.Suppliers
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Supplier> CreateAsync(Supplier supplier)
    {
        await dbContext.Suppliers.AddAsync(supplier);
        await dbContext.SaveChangesAsync();

        return supplier;
    }

    public async Task<Supplier?> UpdateAsync(Supplier supplier)
    {
        var existingSupplier = await dbContext.Suppliers
            .FirstOrDefaultAsync(s => s.Id == supplier.Id);

        if (existingSupplier == null)
        {
            return null;
        }

        existingSupplier.SupplierName = supplier.SupplierName;
        existingSupplier.Phone = supplier.Phone;
        existingSupplier.Email = supplier.Email;

        await dbContext.SaveChangesAsync();

        return existingSupplier;
    }

    public async Task<Supplier?> DeleteAsync(int id)
    {
        var supplier = await dbContext.Suppliers
            .FirstOrDefaultAsync(s => s.Id == id);

        if (supplier == null)
        {
            return null;
        }

        dbContext.Suppliers.Remove(supplier);
        await dbContext.SaveChangesAsync();

        return supplier;
    }
}