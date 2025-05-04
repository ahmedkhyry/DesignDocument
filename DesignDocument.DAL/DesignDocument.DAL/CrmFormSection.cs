using System;
using System.Collections.Generic;

namespace DesignDocument.DAL;

[Serializable]
public class CrmFormSection
{
	public Guid Id { get; set; }

	public string Entity { get; set; }

	public string Form { get; set; }

	public string Tab { get; set; }

	public Dictionary<int, string> Names { get; set; }

	public Dictionary<int, string> Descriptions { get; set; }

	public Guid FormId { get; set; }

	public Guid FormUniqueId { get; set; }

	public bool IsVisible { get; set; }
}
