using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return Ok(await _productRepository.GetProductsListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return Ok(await _productRepository.GetProductByIdAsync(id));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypesAsync()
    {
        return Ok(await _productRepository.GetProductsTypesListAsync());
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsBrands()
    {
        return Ok(await _productRepository.GetProductsBrandsListAsync());
    }

    


}
