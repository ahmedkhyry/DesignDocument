using System.Collections.Generic;

namespace DesignDocument.DAL;

public class FormDetail
{
	public string FormDisplayName { get; set; }

	public string EntityLogicalName { get; set; }

	public List<FormField> FormFields { get; set; }
}
