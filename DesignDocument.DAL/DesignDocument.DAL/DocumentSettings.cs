using System;

namespace DesignDocument.DAL;

[Serializable]
public class DocumentSettings
{
	public bool EntityForms { get; set; }

	public bool SecurityProfiles { get; set; }

	public bool EntityScripts { get; set; }

	public bool Workflows { get; set; }

	public bool Plugins { get; set; }

	public bool EntityGlobalOptionSets { get; set; }

	public bool EntityLocalOptionSets { get; set; }

	public bool VisibleSectionsTabs { get; set; }

	public bool SecurityRoles { get; set; }
}
