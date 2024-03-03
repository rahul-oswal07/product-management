
using Microsoft.EntityFrameworkCore;

namespace PMS.Entities.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationContext _applicationContext;
    public ProductRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _applicationContext.Products.Add(product);
        int result = await _applicationContext.SaveChangesAsync();
        if (result > 0)
        {
            return product;
        }
        return product;
    }

    public async Task<bool> DeleteAllProducts()
    {
        return await _applicationContext.Database.ExecuteSqlRawAsync("DELETE FROM Products") > 0;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        _applicationContext.Products.Remove(new Product { Id = id });
        int result = await _applicationContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Product> GetProductAsync(int id)
    {
        Product product = await _applicationContext.Products.FindAsync(id);
        return product;

    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return _applicationContext.Products.AsNoTracking();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        return await _applicationContext.Products.Where(product => product.Category == category).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        return await _applicationContext.Products.Where(product => product.Name.Contains(name)).ToListAsync();
    }

    public async Task<int> GetTotalProductCount()
    {
        return await _applicationContext.Products.CountAsync();
    }

    public async Task<bool> ProductExists(int id)
    {
        return await _applicationContext.Products.AnyAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> SortProducts(string sortType, string sortOrder)
    {
        IQueryable<Product> query = _applicationContext.Products;

        // Apply sorting based on the provided parameters
        switch (sortType)
        {
            case "name":
                query = sortOrder == "asc" ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                break;
            case "category":
                query = sortOrder == "asc" ? query.OrderBy(p => p.Category) : query.OrderByDescending(p => p.Category);
                break;
            case "price":
                query = sortOrder == "asc" ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                break;
            default:
                return query.OrderBy(p => p.Name); //TODO: Add default sorting
        }

        var sortedProducts = await query.ToListAsync();
        return sortedProducts;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var result = _applicationContext.Products.SingleOrDefault(p => p.Id == product.Id);
        if (result != null)
        {
            _applicationContext.Entry(result).CurrentValues.SetValues(product);
            await _applicationContext.SaveChangesAsync();
        }
        return result;
    }
}
