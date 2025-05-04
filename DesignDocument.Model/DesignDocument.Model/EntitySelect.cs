namespace DesignDocument.Model;

public class EntitySelect : ModelBase
{
	public string DisplayName { get; set; }

	public string LogicalName { get; set; }

	public bool IsConfigurationEntity { get; set; }

	public Generation Generation { get; set; }
}
