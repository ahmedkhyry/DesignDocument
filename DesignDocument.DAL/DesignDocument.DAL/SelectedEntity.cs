using System;

namespace DesignDocument.DAL;

[Serializable]
public class SelectedEntity
{
	public string LogicalName { get; set; }

	public bool IsConfigurationEntity { get; set; }
}
