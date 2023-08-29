using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
	public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecificaition<TEntity> specificatiion)
	{
		var query = inputQuery;

		if (specificatiion.Criteria != null)
		{
			query.Where(specificatiion.Criteria);
		}

		// Agregar las especificaciones a la consulta actual.
		query = specificatiion.Includes.Aggregate(query, (current, include) => current.Include(include));

		return query;
	}
}
