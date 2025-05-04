using System.Runtime.Serialization;

namespace DesignDocument.DAL;

[DataContract]
public enum SdkMessageProcessingStepState
{
	[EnumMember]
	Enabled,
	[EnumMember]
	Disabled
}
