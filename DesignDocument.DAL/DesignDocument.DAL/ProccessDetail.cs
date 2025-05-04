using System;
using System.Collections.Generic;

namespace DesignDocument.DAL;

[Serializable]
public class ProccessDetail
{
	public enum ProccessMessages
	{
		Create = 1,
		Update
	}

	public string ProccessName { get; set; }

	public ProccessMessages ProccessMessage { get; set; }

	public string ProccessDescription { get; set; }

	public string ProccessEntityLogicalName { get; set; }

	public string ProccessEntityDisplayName { get; set; }

	public bool IsSync { get; set; }

	public bool IsChild { get; set; }

	public List<string> ProccessSteps { get; set; }
}
