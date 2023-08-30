using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
	private readonly StoreContext _context;
	public GenericRepository(StoreContext context)
	{
		_context = context;

	}
	public async Task<TEntity> GetByIdAsync(int id)
	{
		return await _context.Set<TEntity>().FindAsync(id);
	}

	public async Task<IReadOnlyList<TEntity>> ListAllAsync()
	{
		return await _context.Set<TEntity>().ToListAsync();
	}

	public async Task<TEntity> GetEntityWithSpecifications(ISpecificaition<TEntity> specification)
	{
		return await ApplySpecification(specification)
					.AsQueryable()
					.FirstOrDefaultAsync();
	}

	public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecificaition<TEntity> specification)
	{
		return await ApplySpecification(specification)
					.ToListAsync();
	}

	private IQueryable<TEntity> ApplySpecification(ISpecificaition<TEntity> specification)
	{
		return SpecificationEvaluator<TEntity>
				.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
	}

	public async Task<int> CountAsync(ISpecificaition<TEntity> specificaition)
	{
		return await ApplySpecification(specificaition).CountAsync();
	}
}
