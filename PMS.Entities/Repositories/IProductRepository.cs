namespace PMS.Entities.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();

    Task<Product> GetProductAsync(int id);
    
    Task<Product> AddProductAsync(Product product);
    
    Task<Product> UpdateProductAsync(Product product);
    
    Task<bool> DeleteProductAsync(int id);
    
    Task<IEnumerable<Product>> GetProductsByName(string name);
    
    Task<IEnumerable<Product>> GetProductsByCategory(string category);
    
    Task<int> GetTotalProductCount();
    
    Task<IEnumerable<Product>> SortProducts(string sortType, string sortOrder);
    
    Task<bool> DeleteAllProducts();

    Task<bool> ProductExists(int id);
}