using AutoMapper;
using PMS.DTOs;
using PMS.Entities;
using PMS.Entities.Repositories;

namespace PMS.Services;
public class ProductService : IProductService
{
    private IProductRepository _productRepository;
    private IMapper _mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDTO> AddProduct(AddProductDTO product)
    {
        var productEntity = _mapper.Map<Product>(product);
        var result = await _productRepository.AddProductAsync(productEntity);
        return _mapper.Map<ProductDTO>(result);
    }

    public Task<bool> DeleteAllProducts()
    {
        return _productRepository.DeleteAllProducts();
    }

    public async Task<bool> DeleteProduct(int id)
    {
        return await _productRepository.DeleteProductAsync(id);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductByCategory(string category)
    {
        var result = await _productRepository.GetProductsByCategory(category);
        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<ProductDTO> GetProductById(int id)
    {
        var result = await _productRepository.GetProductAsync(id);
        return _mapper.Map<ProductDTO>(result);
    }

    public async Task<int> GetProductCount()
    {
        return await _productRepository.GetTotalProductCount();
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var result = await _productRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsByName(string name)
    {
        var result = await _productRepository.GetProductsByName(name);
        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<bool> ProductExists(int id)
    {
        return await _productRepository.ProductExists(id);
    }

    public async Task<IEnumerable<ProductDTO>> SortProducts(string sortType, string sortOrder)
    {
        return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.SortProducts(sortType, sortOrder));
    }

    public async Task<ProductDTO> UpdateProduct(ProductDTO product)
    {
        var productEntity = _mapper.Map<Product>(product);
        var result = await _productRepository.UpdateProductAsync(productEntity);
        return _mapper.Map<ProductDTO>(result);
    }
}