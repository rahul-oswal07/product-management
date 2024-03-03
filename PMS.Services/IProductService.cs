using PMS.DTOs;

namespace PMS.Services
{
    public interface IProductService
    {
        Task<ProductDTO> GetProductById(int id);

        Task<bool> ProductExists(int id);

        Task<IEnumerable<ProductDTO>> GetProducts();

        Task<ProductDTO> AddProduct(AddProductDTO product);

        Task<ProductDTO> UpdateProduct(ProductDTO product);

        Task<bool> DeleteProduct(int id);

        Task<IEnumerable<ProductDTO>> GetProductsByName(string name);

        Task<int> GetProductCount();

        Task<IEnumerable<ProductDTO>> GetProductByCategory(string category);

        Task<IEnumerable<ProductDTO>> SortProducts(string sortType, string sortOrder);

        Task<bool> DeleteAllProducts();
    }
}