using System;

namespace DesignDocument.DAL;

[Serializable]
public class CrmFieldEvent
{
	public string LibraryName { get; set; }

	public string EventName { get; set; }

	public string FunctionName { get; set; }

	public string AttributeName { get; set; }

	public string AttributeDisplayName { get; set; }

	public bool IsEnabled { get; set; }

	public bool IsExecutionContext { get; set; }

	public string Parameters { get; set; }

	public DateTime ModifiedOn { get; set; }

	public DateTime CreatedOn { get; set; }
}
