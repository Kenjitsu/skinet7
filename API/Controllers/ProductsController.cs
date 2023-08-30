using API.Controllers;
using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class ProductsController : BaseApiController
{
	private readonly IGenericRepository<Product> _productRepository;
	private readonly IGenericRepository<ProductBrand> _productBrandRepository;
	private readonly IGenericRepository<ProductType> _productTypeRepository;
	private readonly IMapper _mapper;

	public ProductsController(IGenericRepository<Product> productRepository, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
	{
		this._mapper = mapper;
		_productRepository = productRepository;
		_productBrandRepository = productBrandRepository;
		_productTypeRepository = productTypeRepository;

	}

	[HttpGet]
	public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
	{
		var specification = new ProductsWithTypesAndBrandsSpecification();
		var products = await _productRepository.ListAsync(specification);

		return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products));
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
	{
		var specification = new ProductsWithTypesAndBrandsSpecification(id);
		var product = await _productRepository.GetEntityWithSpecifications(specification);

		if (product == null) return NotFound(new ApiResponse(404));

		return _mapper.Map<Product, ProductToReturnDTO>(product);
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
