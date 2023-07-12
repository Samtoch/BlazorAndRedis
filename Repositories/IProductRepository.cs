using BlazorAndRedis.Data.Model;

namespace BlazorAndRedis.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<bool> CreateProducts();
    }
}
