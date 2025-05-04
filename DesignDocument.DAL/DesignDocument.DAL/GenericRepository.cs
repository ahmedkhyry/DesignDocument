using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using DesignDocument.Model;

namespace DesignDocument.DAL;

public class GenericRepository<C, T> : IGenericRepository<T> where C : DbContext, new() where T : ModelBase
{
	private C _entities;

	public GenericRepository(C context)
	{
		_entities = context;
	}

	public virtual IQueryable<T> GetAll()
	{
		return (IQueryable<T>)((DbContext)_entities).Set<T>();
	}

	public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
	{
		return ((IQueryable<T>)((DbContext)_entities).Set<T>()).Where(predicate);
	}

	public T FindFirstBy(Expression<Func<T, bool>> predicate)
	{
		return ((IQueryable<T>)((DbContext)_entities).Set<T>()).FirstOrDefault(predicate);
	}

	public virtual void Add(T entity)
	{
		((DbSet<_003F>)(object)((DbContext)_entities).Set<T>()).Add(entity);
	}

	public virtual T AddIfNotExists<T>(T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
	{
		return (!((predicate != null) ? ((IQueryable<T>)((DbContext)_entities).Set<T>()).Any(predicate) : ((IQueryable<T>)((DbContext)_entities).Set<T>()).Any())) ? ((DbContext)_entities).Set<T>().Add(entity) : null;
	}

	public virtual T AddIfNotExistsUpdateIfExists(T entity, Expression<Func<T, bool>> predicate = null)
	{
		T val = ((IQueryable<T>)((DbContext)_entities).Set<T>()).FirstOrDefault(predicate);
		if (val == null)
		{
			entity = ((DbSet<_003F>)(object)((DbContext)_entities).Set<T>()).Add(entity);
		}
		else
		{
			entity.Id = val.Id;
			Edit(entity);
		}
		return entity;
	}

	public virtual void Delete(T entity)
	{
		((DbSet<_003F>)(object)((DbContext)_entities).Set<T>()).Remove(entity);
	}

	public virtual void Edit(T entity)
	{
		((DbEntityEntry<_003F>)(object)((DbContext)_entities).Entry<T>(entity)).State = (EntityState)16;
	}

	public virtual void Save()
	{
		((DbContext)_entities).SaveChanges();
	}
}
