using Microsoft.EntityFrameworkCore;
using SmartStore.API.Data;
using SmartStore.API.Models.Domain;
using SmartStore.API.Repository.Interfaces;

namespace SmartStore.API.Repository.Implementation;

public class CategoryRepository : ICategoryRepository
{
    private readonly SmartStoreDbContext dbContext;

    public CategoryRepository(SmartStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await dbContext.Categories
                              .OrderBy(c => c.CategoryName)
                              .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await dbContext.Categories
                              .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);

        await dbContext.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var existingCategory = await dbContext.Categories
                                              .FirstOrDefaultAsync(c => c.Id == category.Id);

        if (existingCategory == null)
        {
            return null;
        }

        existingCategory.CategoryName = category.CategoryName;
        existingCategory.Description = category.Description;

        await dbContext.SaveChangesAsync();

        return existingCategory;
    }

    public async Task<Category?> DeleteAsync(int id)
    {
        var category = await dbContext.Categories
                                      .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return null;
        }

        dbContext.Categories.Remove(category);

        await dbContext.SaveChangesAsync();

        return category;
    }

}