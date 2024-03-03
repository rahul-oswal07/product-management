using Microsoft.AspNetCore.Mvc;
using PMS.DTOs;
using PMS.Services;

namespace PMS.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ProductDTO>> Post(AddProductDTO productDTO)
    {
        var result = await _productService.AddProduct(productDTO);
        if (result != null)
        {
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        else return BadRequest();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var result = await _productService.GetProducts();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var result = await _productService.GetProductById(id);
        if (result != null)
        {
            return Ok(result);
        }
        else return NotFound();
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsbyName(string name)
    {
        var result = await _productService.GetProductsByName(name);
        return Ok(result);
    }

    [HttpGet("total-count")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetTotalCount()
    {
        try
        {
            return Ok(await _productService.GetProductCount());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetTotalCount");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("category/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(string category)
    {
        var result = await _productService.GetProductByCategory(category);
        if (result.Any())
            return Ok(result);
        else
            return NotFound();
    }

    [HttpGet("sort")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> SortProduct(string sortType, string sortOrder)
    {
        var result = await _productService.SortProducts(sortType, sortOrder);
        return Ok(result);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(ProductDTO productDTO)
    {
        var product = await _productService.GetProductById(productDTO.Id);
        if (product == null)
        {
            return NotFound();
        }
        else
        {
            await _productService.UpdateProduct(productDTO);
            return NoContent();
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _productService.ProductExists(id);
        if (!result)
        {
            return NotFound();
        }
        await _productService.DeleteProduct(id);
        return NoContent();
    }
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete()
    {
        await _productService.DeleteAllProducts();

        return NoContent();

    }
}
