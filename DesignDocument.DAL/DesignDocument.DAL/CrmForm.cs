using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace DesignDocument.DAL;

[Serializable]
public class CrmForm
{
	public Guid FormUniqueId { get; set; }

	public Guid Id { get; set; }

	public string Entity { get; set; }

	public Dictionary<int, string> Names { get; set; }

	public Dictionary<int, string> Descriptions { get; set; }

	public List<Entity> WebResourceDependencies { get; set; }

	public List<CrmFieldEvent> Events { get; set; }
}
