using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
	/// <summary>
	/// Retornar entidad por Id.
	/// </summary>
	/// <param name="id">Id de la entidad</param>
	/// <returns>Entidad</returns>
	Task<TEntity> GetByIdAsync(int id);

	/// <summary>
	/// Obtener una lista de una entidad.
	/// </summary>
	/// <returns>Lista de una entidad.</returns>
	Task<IReadOnlyList<TEntity>> ListAllAsync();

	/// <summary>
	/// Obtener una entidad con especificaciones.
	/// </summary>
	/// <param name="specification">Especificación a aplicar a la consulta.</param>
	/// <returns>Entidad.</returns>
	Task<TEntity> GetEntityWithSpecifications(ISpecificaition<TEntity> specification);

	/// <summary>
	/// Obtener una lista de una entidad con espeficaciones.
	/// </summary>
	/// <param name="specification">Especificación a aplicar a la consulta.</param>
	/// <returns>Lista de una entidad.</returns>
	Task<IReadOnlyList<TEntity>> ListAsync(ISpecificaition<TEntity> specification);

	Task<int> CountAsync(ISpecificaition<TEntity> specificaition);

}
