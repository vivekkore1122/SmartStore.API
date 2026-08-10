using SmartStore.API.Models.Domain;
using SmartStore.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SmartStore.API.Data;


namespace SmartStore.API.Repository.Implementation;

public class ProductRepository : IProductRepository
{
    private readonly SmartStoreDbContext dbContext;

    public ProductRepository(SmartStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await dbContext.Products
                              .OrderBy(p => p.Name)
                              .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await dbContext.Products
                              .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);

        await dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existingProduct = await dbContext.Products
                                             .FirstOrDefaultAsync(p => p.Id == product.Id);

        if (existingProduct == null)
        {
            return null;
        }

        existingProduct.Name = product.Name;
        existingProduct.ProductCode = product.ProductCode;
        existingProduct.Category = product.Category;
        existingProduct.Supplier = product.Supplier;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;

        await dbContext.SaveChangesAsync();

        return existingProduct;
    }
    public async Task<Product?> DeleteAsync(int id)
    {
        var product = await dbContext.Products
                                     .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return null;
        }

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync();

        return product;
    }
}