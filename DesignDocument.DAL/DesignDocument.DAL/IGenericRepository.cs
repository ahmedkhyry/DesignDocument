using System;
using System.Linq;
using System.Linq.Expressions;

namespace DesignDocument.DAL;

public interface IGenericRepository<T> where T : class
{
	IQueryable<T> GetAll();

	IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

	void Add(T entity);

	void Delete(T entity);

	void Edit(T entity);

	void Save();
}
