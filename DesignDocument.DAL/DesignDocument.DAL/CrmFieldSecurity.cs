using System;

namespace DesignDocument.DAL;

[Serializable]
public class CrmFieldSecurity
{
	public string SecurityProfileName { get; set; }

	public int CanRead { get; set; }

	public int CanCreate { get; set; }

	public int CanUpdate { get; set; }
}
