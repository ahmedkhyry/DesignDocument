using System.Collections.Generic;

namespace DesignDocument.DAL;

public class FormField
{
	public string FieldDisplayName { get; set; }

	public string FieldDisplayNameArabic { get; set; }

	public string FieldLogicalName { get; set; }

	public string FieldDescription { get; set; }

	public List<string> ProccessDependencies { get; set; }
}
