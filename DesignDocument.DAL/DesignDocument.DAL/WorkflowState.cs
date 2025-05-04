using System.Runtime.Serialization;

namespace DesignDocument.DAL;

[DataContract]
public enum WorkflowState
{
	[EnumMember]
	Draft,
	[EnumMember]
	Activated
}
