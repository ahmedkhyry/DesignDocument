using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Xrm.Sdk.Metadata;

namespace DesignDocument.DAL;

[Serializable]
public class CrmConfiguration
{
	public string OrganizationURL { get; set; }

	public string BaseURL { get; set; }

	public string UserName { get; set; }

	public string Password { get; set; }

	public List<EntityMetadata> Entities { get; set; }

	public List<int> Lcids { get; set; }

	public bool IsIfd { get; set; }

	public DateTime ModifiedOn { get; set; }

	public Color MainColor1 { get; set; }

	public Color MainColor2 { get; set; }

	public Color MainColor3 { get; set; }

	public bool IsVisio { get; set; }

	public Font Font { get; set; }
}
