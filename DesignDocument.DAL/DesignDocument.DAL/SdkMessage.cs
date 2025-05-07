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
[EntityLogicalName("sdkmessage")]
public class SdkMessage : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum AutoTransact
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum Expand
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsActive
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsPrivate
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsReadOnly
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsValidForExecuteAsync
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum Template
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum WorkflowSdkStepEnabled
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		public static class Names
		{
			public const string AutoTransact = "autotransact";

			public const string Expand = "expand";

			public const string IsActive = "isactive";

			public const string IsPrivate = "isprivate";

			public const string IsReadOnly = "isreadonly";

			public const string IsValidForExecuteAsync = "isvalidforexecuteasync";

			public const string Template = "template";

			public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
		}

		public static class Labels
		{
			public static class AutoTransact
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(AutoTransact), label, languageCode);
				}
			}

			public static class Expand
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Expand), label, languageCode);
				}
			}

			public static class IsActive
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsActive), label, languageCode);
				}
			}

			public static class IsPrivate
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsPrivate), label, languageCode);
				}
			}

			public static class IsReadOnly
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "\u200f\u200fنعم";

				public const string No_1033 = "No";

				public const string No_1025 = "\u200f\u200fلا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsReadOnly), label, languageCode);
				}
			}

			public static class IsValidForExecuteAsync
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsValidForExecuteAsync), label, languageCode);
				}
			}

			public static class Template
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Template), label, languageCode);
				}
			}

			public static class WorkflowSdkStepEnabled
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "\u200f\u200fنعم";

				public const string No_1033 = "No";

				public const string No_1025 = "\u200f\u200fلا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(WorkflowSdkStepEnabled), label, languageCode);
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
			public const string AutoTransact = "AutoTransact";

			public const string Availability = "Availability";

			public const string CategoryName = "CategoryName";

			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string CustomizationLevel = "CustomizationLevel";

			public const string Expand = "Expand";

			public const string IsActive = "IsActive";

			public const string IsPrivate = "IsPrivate";

			public const string IsReadOnly = "IsReadOnly";

			public const string IsValidForExecuteAsync = "IsValidForExecuteAsync";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string Name = "Name";

			public const string OrganizationId = "OrganizationId";

			public const string OrganizationIdName = "OrganizationIdName";

			public const string SdkMessageId = "SdkMessageId";

			public const string SdkMessageIdUnique = "SdkMessageIdUnique";

			public const string Template = "Template";

			public const string ThrottleSettings = "ThrottleSettings";

			public const string VersionNumber = "VersionNumber";

			public const string WorkflowSdkStepEnabled = "WorkflowSdkStepEnabled";
		}

		public static class Labels
		{
			public static class AutoTransact
			{
				public const string _1033 = "Auto Transact";

				public const string _1025 = "عمليات تلقائية";
			}

			public static class Availability
			{
				public const string _1033 = "Availability";

				public const string _1025 = "\u200f\u200fالتوفر";
			}

			public static class CategoryName
			{
				public const string _1033 = "Category Name";

				public const string _1025 = "اسم الفئة";
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

			public static class Expand
			{
				public const string _1033 = "Expand";

				public const string _1025 = "توسيع";
			}

			public static class IsActive
			{
				public const string _1033 = "Is Active";

				public const string _1025 = "نشط";
			}

			public static class IsPrivate
			{
				public const string _1033 = "Is Private";

				public const string _1025 = "هو خاص";
			}

			public static class IsReadOnly
			{
				public const string _1033 = "Intent";

				public const string _1025 = "هدف";
			}

			public static class IsValidForExecuteAsync
			{
				public const string _1033 = "Is Valid for Execute Async";

				public const string _1025 = "صالح لتنفيذ غير متزامن";
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

			public static class Template
			{
				public const string _1033 = "Template";

				public const string _1025 = "\u200f\u200fالقالب";
			}

			public static class ThrottleSettings
			{
				public const string _1033 = "Throttle Settings";

				public const string _1025 = "إعدادات التحكم";
			}

			public static class WorkflowSdkStepEnabled
			{
				public const string _1033 = "WorkflowSdkStepEnabled";

				public const string _1025 = "WorkflowSdkStepEnabled";
			}
		}

		public const string AutoTransact = "autotransact";

		public const string Availability = "availability";

		public const string CategoryName = "categoryname";

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string CustomizationLevel = "customizationlevel";

		public const string Expand = "expand";

		public const string IsActive = "isactive";

		public const string IsPrivate = "isprivate";

		public const string IsReadOnly = "isreadonly";

		public const string IsValidForExecuteAsync = "isvalidforexecuteasync";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string Name = "name";

		public const string OrganizationId = "organizationid";

		public const string OrganizationIdName = "organizationidName";

		public const string SdkMessageId = "sdkmessageid";

		public const string SdkMessageIdUnique = "sdkmessageidunique";

		public const string Template = "template";

		public const string ThrottleSettings = "throttlesettings";

		public const string VersionNumber = "versionnumber";

		public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
	}

	public static class Relations
	{
		public static class OneToN
		{
			public const string sdkmessageid_sdkmessagefilter = "sdkmessageid_sdkmessagefilter";

			public const string sdkmessageid_sdkmessageprocessingstep = "sdkmessageid_sdkmessageprocessingstep";

			public const string sdkmessageid_workflow_dependency = "sdkmessageid_workflow_dependency";
		}

		public static class NToOne
		{
			public static class Lookups
			{
				public const string createdby_sdkmessage = "createdby";

				public const string lk_sdkmessage_createdonbehalfby = "createdonbehalfby";

				public const string lk_sdkmessage_modifiedonbehalfby = "modifiedonbehalfby";

				public const string modifiedby_sdkmessage = "modifiedby";
			}

			public const string createdby_sdkmessage = "createdby_sdkmessage";

			public const string lk_sdkmessage_createdonbehalfby = "lk_sdkmessage_createdonbehalfby";

			public const string lk_sdkmessage_modifiedonbehalfby = "lk_sdkmessage_modifiedonbehalfby";

			public const string modifiedby_sdkmessage = "modifiedby_sdkmessage";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Sdk Message";

	public const string SchemaName = "SdkMessage";

	public const string EntityLogicalName = "sdkmessage";

	public const int EntityTypeCode = 4606;

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

	[AttributeLogicalName("sdkmessageid")]
	public override Guid Id
	{
		get
		{
			return Id;
		}
		set
		{
			SdkMessageId = value;
		}
	}

	[AttributeLogicalName("autotransact")]
	public bool? AutoTransact
	{
		get
		{
			return GetAttributeValue<bool?>("autotransact");
		}
		set
		{
			OnPropertyChanging("AutoTransact");
			SetAttributeValue("autotransact", (object)value);
			OnPropertyChanged("AutoTransact");
		}
	}

	[AttributeLogicalName("availability")]
	public int? Availability
	{
		get
		{
			return GetAttributeValue<int?>("availability");
		}
		set
		{
			OnPropertyChanging("Availability");
			SetAttributeValue("availability", (object)value);
			OnPropertyChanged("Availability");
		}
	}

	[AttributeLogicalName("categoryname")]
	public string CategoryName
	{
		get
		{
			return GetAttributeValue<string>("categoryname");
		}
		set
		{
			OnPropertyChanging("CategoryName");
			SetAttributeValue("categoryname", (object)value);
			OnPropertyChanged("CategoryName");
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

	[AttributeLogicalName("expand")]
	public bool? Expand
	{
		get
		{
			return GetAttributeValue<bool?>("expand");
		}
		set
		{
			OnPropertyChanging("Expand");
			SetAttributeValue("expand", (object)value);
			OnPropertyChanged("Expand");
		}
	}

	[AttributeLogicalName("isactive")]
	public bool? IsActive
	{
		get
		{
			return GetAttributeValue<bool?>("isactive");
		}
		set
		{
			OnPropertyChanging("IsActive");
			SetAttributeValue("isactive", (object)value);
			OnPropertyChanged("IsActive");
		}
	}

	[AttributeLogicalName("isprivate")]
	public bool? IsPrivate
	{
		get
		{
			return GetAttributeValue<bool?>("isprivate");
		}
		set
		{
			OnPropertyChanging("IsPrivate");
			SetAttributeValue("isprivate", (object)value);
			OnPropertyChanged("IsPrivate");
		}
	}

	[AttributeLogicalName("isreadonly")]
	public bool? IsReadOnly
	{
		get
		{
			return GetAttributeValue<bool?>("isreadonly");
		}
		set
		{
			OnPropertyChanging("IsReadOnly");
			SetAttributeValue("isreadonly", (object)value);
			OnPropertyChanged("IsReadOnly");
		}
	}

	[AttributeLogicalName("isvalidforexecuteasync")]
	public bool? IsValidForExecuteAsync => GetAttributeValue<bool?>("isvalidforexecuteasync");

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

	[AttributeLogicalName("sdkmessageid")]
	public Guid? SdkMessageId
	{
		get
		{
			return GetAttributeValue<Guid?>("sdkmessageid");
		}
		set
		{
			OnPropertyChanging("SdkMessageId");
			SetAttributeValue("sdkmessageid", (object)value);
			if (value.HasValue)
			{
				Id = value.Value;
			}
			else
			{
				Id = Guid.Empty;
			}
			OnPropertyChanged("SdkMessageId");
		}
	}

	[AttributeLogicalName("sdkmessageidunique")]
	public Guid? SdkMessageIdUnique => GetAttributeValue<Guid?>("sdkmessageidunique");

	[AttributeLogicalName("template")]
	public bool? Template
	{
		get
		{
			return GetAttributeValue<bool?>("template");
		}
		set
		{
			OnPropertyChanging("Template");
			SetAttributeValue("template", (object)value);
			OnPropertyChanged("Template");
		}
	}

	[AttributeLogicalName("throttlesettings")]
	public string ThrottleSettings => GetAttributeValue<string>("throttlesettings");

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("workflowsdkstepenabled")]
	public bool? WorkflowSdkStepEnabled => GetAttributeValue<bool?>("workflowsdkstepenabled");

	[RelationshipSchemaName("sdkmessageid_sdkmessagefilter")]
	public IEnumerable<SdkMessageFilter> sdkmessageid_sdkmessagefilter
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "sdkmessageid_sdkmessagefilter");
				}
				IEnumerable<SdkMessageFilter> relatedEntities = GetRelatedEntities<SdkMessageFilter>("sdkmessageid_sdkmessagefilter", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageFilter element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<SdkMessageFilter>("sdkmessageid_sdkmessagefilter", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("sdkmessageid_sdkmessagefilter");
			SetRelatedEntities<SdkMessageFilter>("sdkmessageid_sdkmessagefilter", (EntityRole?)null, value);
			OnPropertyChanged("sdkmessageid_sdkmessagefilter");
		}
	}

	[RelationshipSchemaName("sdkmessageid_sdkmessageprocessingstep")]
	public IEnumerable<SdkMessageProcessingStep> sdkmessageid_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "sdkmessageid_sdkmessageprocessingstep");
				}
				IEnumerable<SdkMessageProcessingStep> relatedEntities = GetRelatedEntities<SdkMessageProcessingStep>("sdkmessageid_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageProcessingStep element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<SdkMessageProcessingStep>("sdkmessageid_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("sdkmessageid_sdkmessageprocessingstep");
			SetRelatedEntities<SdkMessageProcessingStep>("sdkmessageid_sdkmessageprocessingstep", (EntityRole?)null, value);
			OnPropertyChanged("sdkmessageid_sdkmessageprocessingstep");
		}
	}

	[RelationshipSchemaName("sdkmessageid_workflow_dependency")]
	public IEnumerable<WorkflowDependency> sdkmessageid_workflow_dependency
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "sdkmessageid_workflow_dependency");
				}
				IEnumerable<WorkflowDependency> relatedEntities = GetRelatedEntities<WorkflowDependency>("sdkmessageid_workflow_dependency", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(WorkflowDependency element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<WorkflowDependency>("sdkmessageid_workflow_dependency", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("sdkmessageid_workflow_dependency");
			SetRelatedEntities<WorkflowDependency>("sdkmessageid_workflow_dependency", (EntityRole?)null, value);
			OnPropertyChanged("sdkmessageid_workflow_dependency");
		}
	}

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("createdby_sdkmessage")]
	public SystemUser createdby_sdkmessage
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "createdby_sdkmessage");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("createdby_sdkmessage", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("createdby_sdkmessage", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("lk_sdkmessage_createdonbehalfby")]
	public SystemUser lk_sdkmessage_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessage_createdonbehalfby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_sdkmessage_createdonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_sdkmessage_createdonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("lk_sdkmessage_modifiedonbehalfby")]
	public SystemUser lk_sdkmessage_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessage_modifiedonbehalfby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_sdkmessage_modifiedonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_sdkmessage_modifiedonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("modifiedby_sdkmessage")]
	public SystemUser modifiedby_sdkmessage
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "modifiedby_sdkmessage");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("modifiedby_sdkmessage", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("modifiedby_sdkmessage", (EntityRole?)null);
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public SdkMessage()
		: base("sdkmessage")
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

	public List<SdkMessageFilter> Load_sdkmessageid_sdkmessagefilter(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageFilter> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessagefilter", LogicalName, "sdkmessageid", "sdkmessageid", "sdkmessageid", "sdkmessageid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageFilter>();
		sdkmessageid_sdkmessagefilter = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageProcessingStep> Load_sdkmessageid_sdkmessageprocessingstep(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageProcessingStep> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessageprocessingstep", LogicalName, "sdkmessageid", "sdkmessageid", "sdkmessageid", "sdkmessageid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageProcessingStep>();
		sdkmessageid_sdkmessageprocessingstep = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<WorkflowDependency> Load_sdkmessageid_workflow_dependency(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<WorkflowDependency> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflowdependency", LogicalName, "sdkmessageid", "sdkmessageid", "sdkmessageid", "sdkmessageid", recordCountLimit, attributes)
			select entity.ToEntity<WorkflowDependency>();
		sdkmessageid_workflow_dependency = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public SdkMessage(object anonymousType)
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
				((DataCollection<string, object>)(object)Attributes)["sdkmessageid"] = Id;
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
