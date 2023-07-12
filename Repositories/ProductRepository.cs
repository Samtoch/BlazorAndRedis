using BlazorAndRedis.Data;
using BlazorAndRedis.Data.Model;
using BlazorAndRedis.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace BlazorAndRedis.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext _dbContext;
        private IDistributedCache _cache;
        public ProductRepository(ProductDbContext dbContext, IDistributedCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<List<Product>> GetProducts()
        {
            var cacheData = new List<Product>();
            string recordKey = $"Products_{DateTime.Now:yyyyMMdd_hhmm}";
            cacheData = await _cache.GetRecord<List<Product>>(recordKey);
            if (cacheData == null)
            {
                cacheData = await _dbContext.Products.ToListAsync();
                await _cache.SetRecord<List<Product>>(recordKey, cacheData);
            }
            return cacheData;
        }

        public async Task<bool> CreateProducts()
        {
            bool isCreated = false;
            for (int i = 0; i < 10000; i++)
            {
                var prod = new Product() { Name = "Television " + i, Description = "Communication Tool", Stock = i };
                await _dbContext.AddAsync(prod);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
