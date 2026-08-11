using Dapper;
using Microsoft.Data.SqlClient;
using SmartStore.API.Models.DTO;
using SmartStore.API.Repository.Interfaces;

namespace SmartStore.API.Repository.Implementation;

public class DapperProductRepository : IDapperProductRepository
{
    private readonly IConfiguration configuration;

    public DapperProductRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var connectionString =
            configuration.GetConnectionString("SmartStoreConnectionString");

        using var connection = new SqlConnection(connectionString);

        const string sql = """
            SELECT
                Id,
                Name,
                ProductCode,
                Price,
                Quantity,
                CategoryId,
                SupplierId
            FROM Products
            ORDER BY Name
            """;

        return await connection.QueryAsync<ProductDto>(sql);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var connectionString =
            configuration.GetConnectionString("SmartStoreConnectionString");

        using var connection = new SqlConnection(connectionString);

        const string sql = """
            SELECT
                Id,
                Name,
                ProductCode,
                Price,
                Quantity,
                CategoryId,
                SupplierId
            FROM Products
            WHERE Id = @Id
            """;

        return await connection.QueryFirstOrDefaultAsync<ProductDto>(
            sql,
            new { Id = id });
    }
}