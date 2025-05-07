using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace DesignDocument.DAL;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[DataContract]
[EntityLogicalName("sdkmessageprocessingstep")]
public class SdkMessageProcessingStep : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum AsyncAutoDelete
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum CanUseReadOnlyConnection
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum ComponentState
		{
			[EnumMember]
			Published,
			[EnumMember]
			Unpublished,
			[EnumMember]
			Deleted,
			[EnumMember]
			DeletedUnpublished
		}

		[DataContract]
		public enum InvocationSource
		{
			[EnumMember]
			Internal = -1,
			[EnumMember]
			Parent,
			[EnumMember]
			Child
		}

		[DataContract]
		public enum IsManaged
		{
			[EnumMember]
			Managed = 1,
			[EnumMember]
			Unmanaged = 0
		}

		[DataContract]
		public enum Mode
		{
			[EnumMember]
			Synchronous,
			[EnumMember]
			Asynchronous
		}

		[DataContract]
		public enum Stage
		{
			[EnumMember]
			InitialPreoperationForinternaluseonly = 5,
			[EnumMember]
			Prevalidation = 10,
			[EnumMember]
			InternalPreoperationBeforeExternalPluginsForinternaluseonly = 15,
			[EnumMember]
			Preoperation = 20,
			[EnumMember]
			InternalPreoperationAfterExternalPluginsForinternaluseonly = 25,
			[EnumMember]
			MainOperationForinternaluseonly = 30,
			[EnumMember]
			InternalPostoperationBeforeExternalPluginsForinternaluseonly = 35,
			[EnumMember]
			Postoperation = 40,
			[EnumMember]
			InternalPostoperationAfterExternalPluginsForinternaluseonly = 45,
			[EnumMember]
			PostoperationDeprecated = 50,
			[EnumMember]
			FinalPostoperationForinternaluseonly = 55
		}

		[DataContract]
		public enum StateCode
		{
			[EnumMember]
			Enabled,
			[EnumMember]
			Disabled
		}

		[DataContract]
		public enum StatusCode
		{
			[EnumMember]
			Enabled = 1,
			[EnumMember]
			Disabled
		}

		[DataContract]
		public enum SupportedDeployment
		{
			[EnumMember]
			ServerOnly,
			[EnumMember]
			MicrosoftDynamicsCRMClientforOutlookOnly,
			[EnumMember]
			Both
		}

		public static class Names
		{
			public const string AsyncAutoDelete = "asyncautodelete";

			public const string CanUseReadOnlyConnection = "canusereadonlyconnection";

			public const string ComponentState = "componentstate";

			public const string InvocationSource = "invocationsource";

			public const string IsManaged = "ismanaged";

			public const string Mode = "mode";

			public const string Stage = "stage";

			public const string StateCode = "statecode";

			public const string StatusCode = "statuscode";

			public const string SupportedDeployment = "supporteddeployment";
		}

		public static class Labels
		{
			public static class AsyncAutoDelete
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(AsyncAutoDelete), label, languageCode);
				}
			}

			public static class CanUseReadOnlyConnection
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "\u200f\u200fنعم";

				public const string No_1033 = "No";

				public const string No_1025 = "\u200f\u200fلا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanUseReadOnlyConnection), label, languageCode);
				}
			}

			public static class ComponentState
			{
				public const string Published_1033 = "Published";

				public const string Published_1025 = "تم نشره";

				public const string Unpublished_1033 = "Unpublished";

				public const string Unpublished_1025 = "غير منشور";

				public const string Deleted_1033 = "Deleted";

				public const string Deleted_1025 = "محذوف";

				public const string DeletedUnpublished_1033 = "Deleted Unpublished";

				public const string DeletedUnpublished_1025 = "غير منشور وتم حذفه";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(ComponentState), label, languageCode);
				}
			}

			public static class InvocationSource
			{
				public const string Internal_1033 = "Internal";

				public const string Internal_1025 = "داخلي";

				public const string Parent_1033 = "Parent";

				public const string Parent_1025 = "الأصل";

				public const string Child_1033 = "Child";

				public const string Child_1025 = "تابع";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(InvocationSource), label, languageCode);
				}
			}

			public static class IsManaged
			{
				public const string Managed_1033 = "Managed";

				public const string Managed_1025 = "مدار";

				public const string Unmanaged_1033 = "Unmanaged";

				public const string Unmanaged_1025 = "غير مدار";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsManaged), label, languageCode);
				}
			}

			public static class Mode
			{
				public const string Synchronous_1033 = "Synchronous";

				public const string Synchronous_1025 = "متزامن";

				public const string Asynchronous_1033 = "Asynchronous";

				public const string Asynchronous_1025 = "غير متزامن";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Mode), label, languageCode);
				}
			}

			public static class Stage
			{
				public const string InitialPreoperationForinternaluseonly_1033 = "Initial Pre-operation (For internal use only)";

				public const string InitialPreoperationForinternaluseonly_1025 = "مرحلة ما قبل التشغيل الأولية (للاستخدام الداخلي فقط)";

				public const string Prevalidation_1033 = "Pre-validation";

				public const string Prevalidation_1025 = "تحقق مسبق من الصحة";

				public const string InternalPreoperationBeforeExternalPluginsForinternaluseonly_1033 = "Internal Pre-operation Before External Plugins (For internal use only)";

				public const string InternalPreoperationBeforeExternalPluginsForinternaluseonly_1025 = "مرحلة ما قبل التشغيل الداخلية قبل المكونات الإضافية الخارجية (للاستخدام الداخلي فقط)";

				public const string Preoperation_1033 = "Pre-operation";

				public const string Preoperation_1025 = "عملية مسبقة";

				public const string InternalPreoperationAfterExternalPluginsForinternaluseonly_1033 = "Internal Pre-operation After External Plugins (For internal use only)";

				public const string InternalPreoperationAfterExternalPluginsForinternaluseonly_1025 = "مرحلة ما قبل التشغيل الداخلية بعد المكونات الإضافية الخارجية (للاستخدام الداخلي فقط)";

				public const string MainOperationForinternaluseonly_1033 = "Main Operation (For internal use only)";

				public const string MainOperationForinternaluseonly_1025 = "عملية التشغيل الأساسية (للاستخدام الداخلي فقط)";

				public const string InternalPostoperationBeforeExternalPluginsForinternaluseonly_1033 = "Internal Post-operation Before External Plugins (For internal use only)";

				public const string InternalPostoperationBeforeExternalPluginsForinternaluseonly_1025 = "مرحلة ما بعد التشغيل الداخلية قبل المكونات الإضافية الخارجية (للاستخدام الداخلي فقط)";

				public const string Postoperation_1033 = "Post-operation";

				public const string Postoperation_1025 = "ما بعد التشغيل";

				public const string InternalPostoperationAfterExternalPluginsForinternaluseonly_1033 = "Internal Post-operation After External Plugins (For internal use only)";

				public const string InternalPostoperationAfterExternalPluginsForinternaluseonly_1025 = "مرحلة ما بعد التشغيل الداخلية بعد المكونات الإضافية الخارجية (للاستخدام الداخلي فقط)";

				public const string PostoperationDeprecated_1033 = "Post-operation (Deprecated)";

				public const string PostoperationDeprecated_1025 = "ما بعد التشغيل (مهملة)";

				public const string FinalPostoperationForinternaluseonly_1033 = "Final Post-operation (For internal use only)";

				public const string FinalPostoperationForinternaluseonly_1025 = "مرحلة ما بعد التشغيل النهائية (للاستخدام الداخلي فقط)";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Stage), label, languageCode);
				}
			}

			public static class StateCode
			{
				public const string Enabled_1033 = "Enabled";

				public const string Enabled_1025 = "قيد التمكين";

				public const string Disabled_1033 = "Disabled";

				public const string Disabled_1025 = "قيد التعطيل";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(StateCode), label, languageCode);
				}
			}

			public static class StatusCode
			{
				public const string Enabled_1033 = "Enabled";

				public const string Enabled_1025 = "قيد التمكين";

				public const string Disabled_1033 = "Disabled";

				public const string Disabled_1025 = "قيد التعطيل";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(StatusCode), label, languageCode);
				}
			}

			public static class SupportedDeployment
			{
				public const string ServerOnly_1033 = "Server Only";

				public const string ServerOnly_1025 = "خادم فقط";

				public const string MicrosoftDynamicsCRMClientforOutlookOnly_1033 = "Microsoft Dynamics CRM Client for Outlook Only";

				public const string MicrosoftDynamicsCRMClientforOutlookOnly_1025 = "Microsoft Dynamics CRM Client for Outlook فقط";

				public const string Both_1033 = "Both";

				public const string Both_1025 = "الاثنان معا\u064b";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(SupportedDeployment), label, languageCode);
				}
			}
		}

		public static string GetLabel(string logicalName, int constant, int languageCode = 1033)
		{
			return CrmHelpers.GetLabel(logicalName, constant, typeof(Enums), languageCode);
		}

		public static int GetValue(string logicalName, string label, int languageCode = 1033)
		{
			return CrmHelpers.GetValue(logicalName, label, typeof(Enums), languageCode);
		}
	}

	public static class Fields
	{
		public static class Schema
		{
			public const string AsyncAutoDelete = "AsyncAutoDelete";

			public const string CanUseReadOnlyConnection = "CanUseReadOnlyConnection";

			public const string ComponentState = "ComponentState";

			public const string Configuration = "Configuration";

			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string CustomizationLevel = "CustomizationLevel";

			public const string Description = "Description";

			public const string EventHandler = "EventHandler";

			public const string EventHandlerName = "EventHandlerName";

			public const string EventHandlerType = "EventHandlerType";

			public const string FilteringAttributes = "FilteringAttributes";

			public const string ImpersonatingUserId = "ImpersonatingUserId";

			public const string ImpersonatingUserIdName = "ImpersonatingUserIdName";

			public const string IntroducedVersion = "IntroducedVersion";

			public const string InvocationSource = "InvocationSource";

			public const string IsCustomizable = "IsCustomizable";

			public const string IsHidden = "IsHidden";

			public const string IsManaged = "IsManaged";

			public const string Mode = "Mode";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string Name = "Name";

			public const string OrganizationId = "OrganizationId";

			public const string OrganizationIdName = "OrganizationIdName";

			public const string OverwriteTime = "OverwriteTime";

			public const string PluginTypeId = "PluginTypeId";

			public const string PluginTypeIdName = "PluginTypeIdName";

			public const string Rank = "Rank";

			public const string SdkMessageFilterId = "SdkMessageFilterId";

			public const string SdkMessageFilterIdName = "SdkMessageFilterIdName";

			public const string SdkMessageId = "SdkMessageId";

			public const string SdkMessageIdName = "SdkMessageIdName";

			public const string SdkMessageProcessingStepId = "SdkMessageProcessingStepId";

			public const string SdkMessageProcessingStepIdUnique = "SdkMessageProcessingStepIdUnique";

			public const string SdkMessageProcessingStepSecureConfigId = "SdkMessageProcessingStepSecureConfigId";

			public const string SdkMessageProcessingStepSecureConfigIdName = "SdkMessageProcessingStepSecureConfigIdName";

			public const string SolutionId = "SolutionId";

			public const string Stage = "Stage";

			public const string StateCode = "StateCode";

			public const string StatusCode = "StatusCode";

			public const string SupportedDeployment = "SupportedDeployment";

			public const string SupportingSolutionId = "SupportingSolutionId";

			public const string VersionNumber = "VersionNumber";
		}

		public static class Labels
		{
			public static class AsyncAutoDelete
			{
				public const string _1033 = "Asynchronous Automatic Delete";

				public const string _1025 = "الحذف التلقائي غير المتزامن";
			}

			public static class CanUseReadOnlyConnection
			{
				public const string _1033 = "Intent";

				public const string _1025 = "هدف";
			}

			public static class ComponentState
			{
				public const string _1033 = "Component State";

				public const string _1025 = "حالة المكون";
			}

			public static class Configuration
			{
				public const string _1033 = "Configuration";

				public const string _1025 = "التكوين";
			}

			public static class CreatedBy
			{
				public const string _1033 = "Created By";

				public const string _1025 = "منشأ بواسطة";
			}

			public static class CreatedOn
			{
				public const string _1033 = "Created On";

				public const string _1025 = "تاريخ الإنشاء";
			}

			public static class CreatedOnBehalfBy
			{
				public const string _1033 = "Created By (Delegate)";

				public const string _1025 = "قام بإنشائه (المفو\u0651ض)";
			}

			public static class Description
			{
				public const string _1033 = "Description";

				public const string _1025 = "الوصف";
			}

			public static class EventHandler
			{
				public const string _1033 = "Event Handler";

				public const string _1025 = "معالج الأحداث";
			}

			public static class FilteringAttributes
			{
				public const string _1033 = "Filtering Attributes";

				public const string _1025 = "سمات التصفية";
			}

			public static class ImpersonatingUserId
			{
				public const string _1033 = "Impersonating User";

				public const string _1025 = "مستخدم منتحل";
			}

			public static class IntroducedVersion
			{
				public const string _1033 = "Introduced Version";

				public const string _1025 = "إصدار م\u064fقد\u0651\u064eم";
			}

			public static class InvocationSource
			{
				public const string _1033 = "Invocation Source";

				public const string _1025 = "مصدر الاستدعاء";
			}

			public static class IsCustomizable
			{
				public const string _1033 = "Customizable";

				public const string _1025 = "قابل للتخصيص";
			}

			public static class IsHidden
			{
				public const string _1033 = "Hidden";

				public const string _1025 = "\u200f\u200fمخفية";
			}

			public static class IsManaged
			{
				public const string _1033 = "State";

				public const string _1025 = "المحافظة";
			}

			public static class Mode
			{
				public const string _1033 = "Execution Mode";

				public const string _1025 = "\u200f\u200fوضع التنفيذ";
			}

			public static class ModifiedBy
			{
				public const string _1033 = "Modified By";

				public const string _1025 = "تعديل بواسطة";
			}

			public static class ModifiedOn
			{
				public const string _1033 = "Modified On";

				public const string _1025 = "تاريخ التعديل";
			}

			public static class ModifiedOnBehalfBy
			{
				public const string _1033 = "Modified By (Delegate)";

				public const string _1025 = "قام بالتعديل (المفو\u0651ض)";
			}

			public static class Name
			{
				public const string _1033 = "Name";

				public const string _1025 = "الاسم";
			}

			public static class OverwriteTime
			{
				public const string _1033 = "Record Overwrite Time";

				public const string _1025 = "وقت الكتابة فوق السجل";
			}

			public static class PluginTypeId
			{
				public const string _1033 = "Plug-In Type";

				public const string _1025 = "نوع المكو\u0651\u0650ن الإضافي";
			}

			public static class Rank
			{
				public const string _1033 = "Execution Order";

				public const string _1025 = "أمر التنفيذ";
			}

			public static class SdkMessageFilterId
			{
				public const string _1033 = "SdkMessage Filter";

				public const string _1025 = "تصفية SdkMessage";
			}

			public static class SdkMessageId
			{
				public const string _1033 = "SDK Message";

				public const string _1025 = "رسالة Sdk";
			}

			public static class SdkMessageProcessingStepSecureConfigId
			{
				public const string _1033 = "SDK Message Processing Step Secure Configuration";

				public const string _1025 = "التكوين الآمن لخطوة معالجة رسالة Sdk";
			}

			public static class SolutionId
			{
				public const string _1033 = "Solution";

				public const string _1025 = "الحل";
			}

			public static class Stage
			{
				public const string _1033 = "Execution Stage";

				public const string _1025 = "مرحلة التنفيذ";
			}

			public static class StateCode
			{
				public const string _1033 = "Status";

				public const string _1025 = "الموقف";
			}

			public static class StatusCode
			{
				public const string _1033 = "Status Reason";

				public const string _1025 = "سبب الموقف";
			}

			public static class SupportedDeployment
			{
				public const string _1033 = "Deployment";

				public const string _1025 = "النشر";
			}

			public static class SupportingSolutionId
			{
				public const string _1033 = "Solution";

				public const string _1025 = "الحل";
			}
		}

		public const string AsyncAutoDelete = "asyncautodelete";

		public const string CanUseReadOnlyConnection = "canusereadonlyconnection";

		public const string ComponentState = "componentstate";

		public const string Configuration = "configuration";

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string CustomizationLevel = "customizationlevel";

		public const string Description = "description";

		public const string EventHandler = "eventhandler";

		public const string EventHandlerName = "eventhandlerName";

		public const string EventHandlerType = "eventhandlerType";

		public const string FilteringAttributes = "filteringattributes";

		public const string ImpersonatingUserId = "impersonatinguserid";

		public const string ImpersonatingUserIdName = "impersonatinguseridName";

		public const string IntroducedVersion = "introducedversion";

		public const string InvocationSource = "invocationsource";

		public const string IsCustomizable = "iscustomizable";

		public const string IsHidden = "ishidden";

		public const string IsManaged = "ismanaged";

		public const string Mode = "mode";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string Name = "name";

		public const string OrganizationId = "organizationid";

		public const string OrganizationIdName = "organizationidName";

		public const string OverwriteTime = "overwritetime";

		public const string PluginTypeId = "plugintypeid";

		public const string PluginTypeIdName = "plugintypeidName";

		public const string Rank = "rank";

		public const string SdkMessageFilterId = "sdkmessagefilterid";

		public const string SdkMessageFilterIdName = "sdkmessagefilteridName";

		public const string SdkMessageId = "sdkmessageid";

		public const string SdkMessageIdName = "sdkmessageidName";

		public const string SdkMessageProcessingStepId = "sdkmessageprocessingstepid";

		public const string SdkMessageProcessingStepIdUnique = "sdkmessageprocessingstepidunique";

		public const string SdkMessageProcessingStepSecureConfigId = "sdkmessageprocessingstepsecureconfigid";

		public const string SdkMessageProcessingStepSecureConfigIdName = "sdkmessageprocessingstepsecureconfigidName";

		public const string SolutionId = "solutionid";

		public const string Stage = "stage";

		public const string StateCode = "statecode";

		public const string StatusCode = "statuscode";

		public const string SupportedDeployment = "supporteddeployment";

		public const string SupportingSolutionId = "supportingsolutionid";

		public const string VersionNumber = "versionnumber";
	}

	public static class Relations
	{
		public static class OneToN
		{
		}

		public static class NToOne
		{
			public static class Lookups
			{
				public const string createdby_sdkmessageprocessingstep = "createdby";

				public const string impersonatinguserid_sdkmessageprocessingstep = "impersonatinguserid";

				public const string lk_sdkmessageprocessingstep_createdonbehalfby = "createdonbehalfby";

				public const string lk_sdkmessageprocessingstep_modifiedonbehalfby = "modifiedonbehalfby";

				public const string modifiedby_sdkmessageprocessingstep = "modifiedby";

				public const string sdkmessagefilterid_sdkmessageprocessingstep = "sdkmessagefilterid";

				public const string sdkmessageid_sdkmessageprocessingstep = "sdkmessageid";
			}

			public const string createdby_sdkmessageprocessingstep = "createdby_sdkmessageprocessingstep";

			public const string impersonatinguserid_sdkmessageprocessingstep = "impersonatinguserid_sdkmessageprocessingstep";

			public const string lk_sdkmessageprocessingstep_createdonbehalfby = "lk_sdkmessageprocessingstep_createdonbehalfby";

			public const string lk_sdkmessageprocessingstep_modifiedonbehalfby = "lk_sdkmessageprocessingstep_modifiedonbehalfby";

			public const string modifiedby_sdkmessageprocessingstep = "modifiedby_sdkmessageprocessingstep";

			public const string sdkmessagefilterid_sdkmessageprocessingstep = "sdkmessagefilterid_sdkmessageprocessingstep";

			public const string sdkmessageid_sdkmessageprocessingstep = "sdkmessageid_sdkmessageprocessingstep";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Sdk Message Processing Step";

	public const string SchemaName = "SdkMessageProcessingStep";

	public const string EntityLogicalName = "sdkmessageprocessingstep";

	public const int EntityTypeCode = 4608;

	private XrmServiceContext serviceContext;

	public XrmServiceContext ServiceContext
	{
		get
		{
			return serviceContext;
		}
		set
		{
			try
			{
				serviceContext = value;
				((OrganizationServiceContext)serviceContext).Attach((Entity)(object)this);
			}
			catch
			{
			}
		}
	}

	[AttributeLogicalName("sdkmessageprocessingstepid")]
	public override Guid Id
	{
		get
		{
			return Id;
		}
		set
		{
			SdkMessageProcessingStepId = value;
		}
	}

	[AttributeLogicalName("asyncautodelete")]
	public bool? AsyncAutoDelete
	{
		get
		{
			return GetAttributeValue<bool?>("asyncautodelete");
		}
		set
		{
			OnPropertyChanging("AsyncAutoDelete");
			SetAttributeValue("asyncautodelete", (object)value);
			OnPropertyChanged("AsyncAutoDelete");
		}
	}

	[AttributeLogicalName("canusereadonlyconnection")]
	public bool? CanUseReadOnlyConnection
	{
		get
		{
			return GetAttributeValue<bool?>("canusereadonlyconnection");
		}
		set
		{
			OnPropertyChanging("CanUseReadOnlyConnection");
			SetAttributeValue("canusereadonlyconnection", (object)value);
			OnPropertyChanged("CanUseReadOnlyConnection");
		}
	}

	[AttributeLogicalName("componentstate")]
	public OptionSetValue ComponentState => GetAttributeValue<OptionSetValue>("componentstate");

	[AttributeLogicalName("configuration")]
	public string Configuration
	{
		get
		{
			return GetAttributeValue<string>("configuration");
		}
		set
		{
			OnPropertyChanging("Configuration");
			SetAttributeValue("configuration", (object)value);
			OnPropertyChanged("Configuration");
		}
	}

	[AttributeLogicalName("createdby")]
	public EntityReference CreatedBy => GetAttributeValue<EntityReference>("createdby");

	[AttributeLogicalName("createdon")]
	public DateTime? CreatedOn => GetAttributeValue<DateTime?>("createdon");

	[AttributeLogicalName("createdonbehalfby")]
	public EntityReference CreatedOnBehalfBy => GetAttributeValue<EntityReference>("createdonbehalfby");

	[AttributeLogicalName("customizationlevel")]
	public int? CustomizationLevel => GetAttributeValue<int?>("customizationlevel");

	[AttributeLogicalName("description")]
	public string Description
	{
		get
		{
			return GetAttributeValue<string>("description");
		}
		set
		{
			OnPropertyChanging("Description");
			SetAttributeValue("description", (object)value);
			OnPropertyChanged("Description");
		}
	}

	[AttributeLogicalName("eventhandler")]
	public EntityReference EventHandler
	{
		get
		{
			return GetAttributeValue<EntityReference>("eventhandler");
		}
		set
		{
			OnPropertyChanging("EventHandler");
			SetAttributeValue("eventhandler", (object)value);
			OnPropertyChanged("EventHandler");
		}
	}

	[AttributeLogicalName("filteringattributes")]
	public string FilteringAttributes
	{
		get
		{
			return GetAttributeValue<string>("filteringattributes");
		}
		set
		{
			OnPropertyChanging("FilteringAttributes");
			SetAttributeValue("filteringattributes", (object)value);
			OnPropertyChanged("FilteringAttributes");
		}
	}

	[AttributeLogicalName("impersonatinguserid")]
	public EntityReference ImpersonatingUserId
	{
		get
		{
			return GetAttributeValue<EntityReference>("impersonatinguserid");
		}
		set
		{
			OnPropertyChanging("ImpersonatingUserId");
			SetAttributeValue("impersonatinguserid", (object)value);
			OnPropertyChanged("ImpersonatingUserId");
		}
	}

	[AttributeLogicalName("introducedversion")]
	public string IntroducedVersion
	{
		get
		{
			return GetAttributeValue<string>("introducedversion");
		}
		set
		{
			OnPropertyChanging("IntroducedVersion");
			SetAttributeValue("introducedversion", (object)value);
			OnPropertyChanged("IntroducedVersion");
		}
	}

	[AttributeLogicalName("invocationsource")]
	[Obsolete]
	public OptionSetValue InvocationSource
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("invocationsource");
		}
		set
		{
			OnPropertyChanging("InvocationSource");
			SetAttributeValue("invocationsource", (object)value);
			OnPropertyChanged("InvocationSource");
		}
	}

	[AttributeLogicalName("iscustomizable")]
	public BooleanManagedProperty IsCustomizable
	{
		get
		{
			return GetAttributeValue<BooleanManagedProperty>("iscustomizable");
		}
		set
		{
			OnPropertyChanging("IsCustomizable");
			SetAttributeValue("iscustomizable", (object)value);
			OnPropertyChanged("IsCustomizable");
		}
	}

	[AttributeLogicalName("ishidden")]
	public BooleanManagedProperty IsHidden
	{
		get
		{
			return GetAttributeValue<BooleanManagedProperty>("ishidden");
		}
		set
		{
			OnPropertyChanging("IsHidden");
			SetAttributeValue("ishidden", (object)value);
			OnPropertyChanged("IsHidden");
		}
	}

	[AttributeLogicalName("ismanaged")]
	public bool? IsManaged => GetAttributeValue<bool?>("ismanaged");

	[AttributeLogicalName("mode")]
	public OptionSetValue Mode
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("mode");
		}
		set
		{
			OnPropertyChanging("Mode");
			SetAttributeValue("mode", (object)value);
			OnPropertyChanged("Mode");
		}
	}

	[AttributeLogicalName("modifiedby")]
	public EntityReference ModifiedBy => GetAttributeValue<EntityReference>("modifiedby");

	[AttributeLogicalName("modifiedon")]
	public DateTime? ModifiedOn => GetAttributeValue<DateTime?>("modifiedon");

	[AttributeLogicalName("modifiedonbehalfby")]
	public EntityReference ModifiedOnBehalfBy => GetAttributeValue<EntityReference>("modifiedonbehalfby");

	[AttributeLogicalName("name")]
	public string Name
	{
		get
		{
			return GetAttributeValue<string>("name");
		}
		set
		{
			OnPropertyChanging("Name");
			SetAttributeValue("name", (object)value);
			OnPropertyChanged("Name");
		}
	}

	[AttributeLogicalName("organizationid")]
	public EntityReference OrganizationId => GetAttributeValue<EntityReference>("organizationid");

	[AttributeLogicalName("overwritetime")]
	public DateTime? OverwriteTime => GetAttributeValue<DateTime?>("overwritetime");

	[AttributeLogicalName("plugintypeid")]
	[Obsolete]
	public EntityReference PluginTypeId
	{
		get
		{
			return GetAttributeValue<EntityReference>("plugintypeid");
		}
		set
		{
			OnPropertyChanging("PluginTypeId");
			SetAttributeValue("plugintypeid", (object)value);
			OnPropertyChanged("PluginTypeId");
		}
	}

	[AttributeLogicalName("rank")]
	public int? Rank
	{
		get
		{
			return GetAttributeValue<int?>("rank");
		}
		set
		{
			OnPropertyChanging("Rank");
			SetAttributeValue("rank", (object)value);
			OnPropertyChanged("Rank");
		}
	}

	[AttributeLogicalName("sdkmessagefilterid")]
	public EntityReference SdkMessageFilterId
	{
		get
		{
			return GetAttributeValue<EntityReference>("sdkmessagefilterid");
		}
		set
		{
			OnPropertyChanging("SdkMessageFilterId");
			SetAttributeValue("sdkmessagefilterid", (object)value);
			OnPropertyChanged("SdkMessageFilterId");
		}
	}

	[AttributeLogicalName("sdkmessageid")]
	public EntityReference SdkMessageId
	{
		get
		{
			return GetAttributeValue<EntityReference>("sdkmessageid");
		}
		set
		{
			OnPropertyChanging("SdkMessageId");
			SetAttributeValue("sdkmessageid", (object)value);
			OnPropertyChanged("SdkMessageId");
		}
	}

	[AttributeLogicalName("sdkmessageprocessingstepid")]
	public Guid? SdkMessageProcessingStepId
	{
		get
		{
			return GetAttributeValue<Guid?>("sdkmessageprocessingstepid");
		}
		set
		{
			OnPropertyChanging("SdkMessageProcessingStepId");
			SetAttributeValue("sdkmessageprocessingstepid", (object)value);
			if (value.HasValue)
			{
				Id = value.Value;
			}
			else
			{
				Id = Guid.Empty;
			}
			OnPropertyChanged("SdkMessageProcessingStepId");
		}
	}

	[AttributeLogicalName("sdkmessageprocessingstepidunique")]
	public Guid? SdkMessageProcessingStepIdUnique => GetAttributeValue<Guid?>("sdkmessageprocessingstepidunique");

	[AttributeLogicalName("sdkmessageprocessingstepsecureconfigid")]
	public EntityReference SdkMessageProcessingStepSecureConfigId
	{
		get
		{
			return GetAttributeValue<EntityReference>("sdkmessageprocessingstepsecureconfigid");
		}
		set
		{
			OnPropertyChanging("SdkMessageProcessingStepSecureConfigId");
			SetAttributeValue("sdkmessageprocessingstepsecureconfigid", (object)value);
			OnPropertyChanged("SdkMessageProcessingStepSecureConfigId");
		}
	}

	[AttributeLogicalName("solutionid")]
	public Guid? SolutionId => GetAttributeValue<Guid?>("solutionid");

	[AttributeLogicalName("stage")]
	public OptionSetValue Stage
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("stage");
		}
		set
		{
			OnPropertyChanging("Stage");
			SetAttributeValue("stage", (object)value);
			OnPropertyChanged("Stage");
		}
	}

	[AttributeLogicalName("statecode")]
	public SdkMessageProcessingStepState? StateCode
	{
		get
		{
			OptionSetValue attributeValue = GetAttributeValue<OptionSetValue>("statecode");
			if (attributeValue != null)
			{
				return (SdkMessageProcessingStepState)Enum.ToObject(typeof(SdkMessageProcessingStepState), attributeValue.Value);
			}
			return null;
		}
		set
		{
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Expected O, but got Unknown
			OnPropertyChanging("StateCode");
			if (!value.HasValue)
			{
				SetAttributeValue("statecode", (object)null);
			}
			else
			{
				SetAttributeValue("statecode", (object)new OptionSetValue((int)value.Value));
			}
			OnPropertyChanged("StateCode");
		}
	}

	[AttributeLogicalName("statuscode")]
	public OptionSetValue StatusCode
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("statuscode");
		}
		set
		{
			OnPropertyChanging("StatusCode");
			SetAttributeValue("statuscode", (object)value);
			OnPropertyChanged("StatusCode");
		}
	}

	[AttributeLogicalName("supporteddeployment")]
	public OptionSetValue SupportedDeployment
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("supporteddeployment");
		}
		set
		{
			OnPropertyChanging("SupportedDeployment");
			SetAttributeValue("supporteddeployment", (object)value);
			OnPropertyChanged("SupportedDeployment");
		}
	}

	[AttributeLogicalName("supportingsolutionid")]
	public Guid? SupportingSolutionId => GetAttributeValue<Guid?>("supportingsolutionid");

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("createdby_sdkmessageprocessingstep")]
	public SystemUser createdby_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "createdby_sdkmessageprocessingstep");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("createdby_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("createdby_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("impersonatinguserid")]
	[RelationshipSchemaName("impersonatinguserid_sdkmessageprocessingstep")]
	public SystemUser impersonatinguserid_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "impersonatinguserid_sdkmessageprocessingstep");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("impersonatinguserid_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("impersonatinguserid_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("impersonatinguserid_sdkmessageprocessingstep");
			SetRelatedEntity<SystemUser>("impersonatinguserid_sdkmessageprocessingstep", (EntityRole?)null, value);
			OnPropertyChanged("impersonatinguserid_sdkmessageprocessingstep");
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("lk_sdkmessageprocessingstep_createdonbehalfby")]
	public SystemUser lk_sdkmessageprocessingstep_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessageprocessingstep_createdonbehalfby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_sdkmessageprocessingstep_createdonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_sdkmessageprocessingstep_createdonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("lk_sdkmessageprocessingstep_modifiedonbehalfby")]
	public SystemUser lk_sdkmessageprocessingstep_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessageprocessingstep_modifiedonbehalfby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_sdkmessageprocessingstep_modifiedonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_sdkmessageprocessingstep_modifiedonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("modifiedby_sdkmessageprocessingstep")]
	public SystemUser modifiedby_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "modifiedby_sdkmessageprocessingstep");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("modifiedby_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("modifiedby_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("sdkmessagefilterid")]
	[RelationshipSchemaName("sdkmessagefilterid_sdkmessageprocessingstep")]
	public SdkMessageFilter sdkmessagefilterid_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "sdkmessagefilterid_sdkmessageprocessingstep");
				}
				SdkMessageFilter relatedEntity = GetRelatedEntity<SdkMessageFilter>("sdkmessagefilterid_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SdkMessageFilter>("sdkmessagefilterid_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("sdkmessagefilterid_sdkmessageprocessingstep");
			SetRelatedEntity<SdkMessageFilter>("sdkmessagefilterid_sdkmessageprocessingstep", (EntityRole?)null, value);
			OnPropertyChanged("sdkmessagefilterid_sdkmessageprocessingstep");
		}
	}

	[AttributeLogicalName("sdkmessageid")]
	[RelationshipSchemaName("sdkmessageid_sdkmessageprocessingstep")]
	public SdkMessage sdkmessageid_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "sdkmessageid_sdkmessageprocessingstep");
				}
				SdkMessage relatedEntity = GetRelatedEntity<SdkMessage>("sdkmessageid_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SdkMessage>("sdkmessageid_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("sdkmessageid_sdkmessageprocessingstep");
			SetRelatedEntity<SdkMessage>("sdkmessageid_sdkmessageprocessingstep", (EntityRole?)null, value);
			OnPropertyChanged("sdkmessageid_sdkmessageprocessingstep");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public SdkMessageProcessingStep()
		: base("sdkmessageprocessingstep")
	{
	}

	private void OnPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	private void OnPropertyChanging(string propertyName)
	{
		if (this.PropertyChanging != null)
		{
			this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
		}
	}

	public SystemUser Load_impersonatinguserid_sdkmessageprocessingstep(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "systemuser", LogicalName, "systemuserid", "impersonatinguserid", "sdkmessageprocessingstepid", "sdkmessageprocessingstepid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return impersonatinguserid_sdkmessageprocessingstep = val.ToEntity<SystemUser>();
		}
		return impersonatinguserid_sdkmessageprocessingstep = null;
	}

	public SdkMessageFilter Load_sdkmessagefilterid_sdkmessageprocessingstep(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessagefilter", LogicalName, "sdkmessagefilterid", "sdkmessagefilterid", "sdkmessageprocessingstepid", "sdkmessageprocessingstepid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return sdkmessagefilterid_sdkmessageprocessingstep = val.ToEntity<SdkMessageFilter>();
		}
		return sdkmessagefilterid_sdkmessageprocessingstep = null;
	}

	public SdkMessage Load_sdkmessageid_sdkmessageprocessingstep(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessage", LogicalName, "sdkmessageid", "sdkmessageid", "sdkmessageprocessingstepid", "sdkmessageprocessingstepid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return sdkmessageid_sdkmessageprocessingstep = val.ToEntity<SdkMessage>();
		}
		return sdkmessageid_sdkmessageprocessingstep = null;
	}

	public SdkMessageProcessingStep(object anonymousType)
		: this()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		PropertyInfo[] properties = anonymousType.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			object value = propertyInfo.GetValue(anonymousType, null);
			if (propertyInfo.PropertyType == typeof(Guid))
			{
				Id = (Guid)value;
				((DataCollection<string, object>)(object)Attributes)["sdkmessageprocessingstepid"] = Id;
			}
			else if (propertyInfo.Name == "FormattedValues")
			{
				((DataCollection<string, string>)(object)FormattedValues).AddRange((IEnumerable<KeyValuePair<string, string>>)(FormattedValueCollection)value);
			}
			else
			{
				((DataCollection<string, object>)(object)Attributes)[propertyInfo.Name.ToLower()] = value;
			}
		}
	}
}
