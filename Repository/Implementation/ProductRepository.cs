using SmartStore.API.Models.Domain;
using SmartStore.API.Repository.Interfaces;

namespace SmartStore.API.Repository.Implementation;

public class ProductRepository : IProductRepository
{
    private static readonly List<Product> products = new()
    {
        new Product
        {
            Id = 1,
            Name = "Laptop",
            ProductCode = "PRD001",
            Category = "Electronics",
            Supplier = "Dell",
            Price = 65000,
            Quantity = 12
        },

        new Product
        {
            Id = 2,
            Name = "Mouse",
            ProductCode = "PRD002",
            Category = "Electronics",
            Supplier = "Logitech",
            Price = 900,
            Quantity = 40
        }
    };

    public IEnumerable<Product> GetAll()
    {
        return products.OrderBy(p => p.Name);
    }

    public Product? GetById(int id)
    {
        return products.FirstOrDefault(p => p.Id == id);
    }

    public Product Add(Product product)
    {
        product.Id = products.Max(p => p.Id) + 1;

        products.Add(product);

        return product;
    }

    public Product? Update(Product product)
    {
        var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);

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

        return existingProduct;
    }

    public bool Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return false;
        }

        products.Remove(product);

        return true;
    }
}