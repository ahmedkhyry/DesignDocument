using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DesignDocument.DAL;
using DesignDocument.Model;

namespace DesignDocument.BLL;

public class GeneralManager<C, T> where C : DbContext, new() where T : ModelBase
{
	private C _entities;

	public GeneralManager(C context)
	{
		_entities = context;
	}

	public T ReplaceObjectIfExists(T obj)
	{
		GenericRepository<C, T> genericRepository = new GenericRepository<C, T>(_entities);
		T val = genericRepository.FindBy((T u) => u.Name == obj.Name).FirstOrDefault();
		if (val == null)
		{
			return obj;
		}
		return val;
	}

	public T ReplaceObjectIfExists(T obj, Expression<Func<T, bool>> predicate)
	{
		GenericRepository<C, T> genericRepository = new GenericRepository<C, T>(_entities);
		T val = genericRepository.FindBy(predicate).FirstOrDefault();
		if (val == null)
		{
			return obj;
		}
		return val;
	}
}
