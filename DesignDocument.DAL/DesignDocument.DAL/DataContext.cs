using System.Data.Entity;
using DesignDocument.Model;

namespace DesignDocument.DAL;

public class DataContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public DbSet<Organization> Organizations { get; set; }

	public DbSet<Generation> Generations { get; set; }

	public DbSet<EntitySelect> EntitySelects { get; set; }

	public DataContext()
		: base("DesignDocument")
	{
	}

	protected override void OnModelCreating(DbModelBuilder modelBuilder)
	{
	}
}
