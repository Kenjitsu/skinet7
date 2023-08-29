using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly IGenericRepository<Product> _productRepository;
	private readonly IGenericRepository<ProductBrand> _productBrandRepository;
	private readonly IGenericRepository<ProductType> _productTypeRepository;

	public ProductsController(IGenericRepository<Product> productRepository, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository)
	{
		_productRepository = productRepository;
		_productBrandRepository = productBrandRepository;
		_productTypeRepository = productTypeRepository;

	}

	[HttpGet]
	public async Task<ActionResult<List<Product>>> GetProducts()
	{
		var specification = new ProductsWithTypesAndBrandsSpecification();
		var products = await _productRepository.ListAsync(specification);

		return Ok(products);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Product>> GetProduct(int id)
	{
		var specification = new ProductsWithTypesAndBrandsSpecification(id);
		var product = await _productRepository.GetEntityWithSpecifications(specification);

		return Ok(product);
	}

	[HttpGet("types")]
	public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypesAsync()
	{
		return Ok(await _productTypeRepository.ListAllAsync());
	}

	[HttpGet("brands")]
	public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrands()
	{
		return Ok(await _productBrandRepository.ListAllAsync());
	}




}
