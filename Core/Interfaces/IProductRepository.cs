using Core.Entities;

namespace Core.Interfaces;
public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Product>> GetProductsListAsync();
    Task<IReadOnlyList<ProductBrand>> GetProductsBrandsListAsync();
    Task<IReadOnlyList<ProductType>> GetProductsTypesListAsync();

}
