using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Metadata;

namespace DesignDocument.DAL;

[Serializable]
public class CrmFormLabel
{
	public Guid Id { get; set; }

	public string Entity { get; set; }

	public int? EntityObjectTypeCode { get; set; }

	public Guid EntityId { get; set; }

	public List<KeyValuePair<int, string>> EntityDisplayName { get; set; }

	public Guid AttributeId { get; set; }

	public string Attribute { get; set; }

	public string AttributeType { get; set; }

	public string Form { get; set; }

	public CrmForm LabelForm { get; set; }

	public Guid FormId { get; set; }

	public Guid FormUniqueId { get; set; }

	public CrmFormTab Tab { get; set; }

	public CrmFormSection Section { get; set; }

	public string Value { get; set; }

	public string RelatedEntityLogicalName { get; set; }

	public Guid RelatedEntityGuid { get; set; }

	public List<Workflow> RelatedWorkflows { get; set; }

	public Dictionary<int, string> Names { get; set; }

	public Dictionary<int, string> Descriptions { get; set; }

	public Dictionary<int, string> Values { get; set; }

	public List<Workflow> WorkflowDependencies { get; set; }

	public bool IsRequired { get; set; }

	public bool IsRecommended { get; set; }

	public bool IsAudit { get; set; }

	public bool IsSecured { get; set; }

	public Guid ControlUniqueId { get; set; }

	public DateTime ModifiedOn { get; set; }

	public DateTime CreatedOn { get; set; }

	public List<CrmFieldEvent> Events { get; set; }

	public List<CrmFieldSecurity> SecurityProfilePermissions { get; set; }

	public AttributeMetadata MetaData { get; set; }

	public bool IsRollup { get; set; }

	public bool IsCalculated { get; set; }

	public bool IsReadOnly { get; set; }

	public bool IsConfigurationEntity { get; set; }

	public int? MaxLength { get; set; }
}
