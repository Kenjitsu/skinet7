using System.Linq.Expressions;

namespace Core.Specifications;

public class BaseSpecification<T> : ISpecificaition<T>
{
	public BaseSpecification() { }
	public BaseSpecification(Expression<Func<T, bool>> criteria)
	{
		Criteria = criteria;
	}

	public Expression<Func<T, bool>> Criteria { get; }

	public List<Expression<Func<T, object>>> Includes { get; } =
		new List<Expression<Func<T, object>>>();

	/// <summary>
	/// Añadir expresiones a la lista de Includes para una consulta.
	/// </summary>
	/// <param name="includeExpression">Expresión a añadir a la consulta</param>
	protected void AddInclude(Expression<Func<T, object>> includeExpression)
	{
		Includes.Add(includeExpression);
	}
}
