using System;
using DesignDocument.DAL;
using DesignDocument.Model;

namespace DesignDocument.BLL
{

	public class CollectDataManager
	{
		public CollectDataManager(Generation generation)
		{
			DataContext dataContext = new DataContext();
			try
			{
				GeneralManager<DataContext, Organization> generalManager = new GeneralManager<DataContext, Organization>(dataContext);
				generation.Organization = generalManager.ReplaceObjectIfExists(generation.Organization, (Organization p) => p.URL.ToLower() == generation.Organization.URL.ToLower());
				GeneralManager<DataContext, User> generalManager2 = new GeneralManager<DataContext, User>(dataContext);
				generation.User = generalManager2.ReplaceObjectIfExists(generation.User);
				GenericRepository<DataContext, Generation> genericRepository = new GenericRepository<DataContext, Generation>(dataContext);
				genericRepository.Add(generation);
				genericRepository.Save();
			}
			finally
			{
				((IDisposable)dataContext)?.Dispose();
			}
		}
	}
}