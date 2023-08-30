using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
	public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecificaition<TEntity> specification)
	{
		var query = inputQuery;

		// Antes de aplicar paginación, aplicar el criterio de consulta y luego ls filtros.
		if (specification.Criteria != null)
		{
			query = query.Where(specification.Criteria);
		}

		if (specification.OrderBy != null)
		{
			query = query.OrderBy(specification.OrderBy);
		}

		if (specification.OrderByDescending != null)
		{
			query = query.OrderByDescending(specification.OrderByDescending);
		}

		if (specification.IsPagingEnabled)
		{
			query = query.Skip(specification.Skip).Take(specification.Take);
		}

		// Agregar las especificaciones a la consulta actual.
		query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

		return query;
	}
}
