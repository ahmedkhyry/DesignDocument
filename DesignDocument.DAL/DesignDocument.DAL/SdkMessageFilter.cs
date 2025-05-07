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
[EntityLogicalName("sdkmessagefilter")]
public class SdkMessageFilter : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum IsCustomProcessingStepAllowed
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsVisible
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum PrimaryObjectTypeCode
		{

		}

		[DataContract]
		public enum SecondaryObjectTypeCode
		{

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
			public const string IsCustomProcessingStepAllowed = "iscustomprocessingstepallowed";

			public const string IsVisible = "isvisible";

			public const string PrimaryObjectTypeCode = "primaryobjecttypecode";

			public const string SecondaryObjectTypeCode = "secondaryobjecttypecode";

			public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
		}

		public static class Labels
		{
			public static class IsCustomProcessingStepAllowed
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsCustomProcessingStepAllowed), label, languageCode);
				}
			}

			public static class IsVisible
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsVisible), label, languageCode);
				}
			}

			public static class PrimaryObjectTypeCode
			{
				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(PrimaryObjectTypeCode), label, languageCode);
				}
			}

			public static class SecondaryObjectTypeCode
			{
				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(SecondaryObjectTypeCode), label, languageCode);
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
			public const string Availability = "Availability";

			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string CustomizationLevel = "CustomizationLevel";

			public const string IsCustomProcessingStepAllowed = "IsCustomProcessingStepAllowed";

			public const string IsVisible = "IsVisible";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string OrganizationId = "OrganizationId";

			public const string OrganizationIdName = "OrganizationIdName";

			public const string PrimaryObjectTypeCode = "PrimaryObjectTypeCode";

			public const string SdkMessageFilterId = "SdkMessageFilterId";

			public const string SdkMessageFilterIdUnique = "SdkMessageFilterIdUnique";

			public const string SdkMessageId = "SdkMessageId";

			public const string SdkMessageIdName = "SdkMessageIdName";

			public const string SecondaryObjectTypeCode = "SecondaryObjectTypeCode";

			public const string VersionNumber = "VersionNumber";

			public const string WorkflowSdkStepEnabled = "WorkflowSdkStepEnabled";
		}

		public static class Labels
		{
			public static class Availability
			{
				public const string _1033 = "Availability";

				public const string _1025 = "\u200f\u200fالتوفر";
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

			public static class IsCustomProcessingStepAllowed
			{
				public const string _1033 = "Custom Processing Step Allowed";

				public const string _1025 = "خطوة المعالجة الخاصة المسموح بها";
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

			public static class PrimaryObjectTypeCode
			{
				public const string _1033 = "Primary Object Type Code";

				public const string _1025 = "رمز نوع الكائن الأساسي";
			}

			public static class SdkMessageId
			{
				public const string _1033 = "SDK Message ID";

				public const string _1025 = "معرف رسالة SDK";
			}

			public static class SecondaryObjectTypeCode
			{
				public const string _1033 = "Secondary Object Type Code";

				public const string _1025 = "رمز نوع الكائن الثانوي";
			}

			public static class WorkflowSdkStepEnabled
			{
				public const string _1033 = "WorkflowSdkStepEnabled";

				public const string _1025 = "WorkflowSdkStepEnabled";
			}
		}

		public const string Availability = "availability";

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string CustomizationLevel = "customizationlevel";

		public const string IsCustomProcessingStepAllowed = "iscustomprocessingstepallowed";

		public const string IsVisible = "isvisible";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string OrganizationId = "organizationid";

		public const string OrganizationIdName = "organizationidName";

		public const string PrimaryObjectTypeCode = "primaryobjecttypecode";

		public const string SdkMessageFilterId = "sdkmessagefilterid";

		public const string SdkMessageFilterIdUnique = "sdkmessagefilteridunique";

		public const string SdkMessageId = "sdkmessageid";

		public const string SdkMessageIdName = "sdkmessageidName";

		public const string SecondaryObjectTypeCode = "secondaryobjecttypecode";

		public const string VersionNumber = "versionnumber";

		public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
	}

	public static class Relations
	{
		public static class OneToN
		{
			public const string sdkmessagefilterid_sdkmessageprocessingstep = "sdkmessagefilterid_sdkmessageprocessingstep";
		}

		public static class NToOne
		{
			public static class Lookups
			{
				public const string createdby_sdkmessagefilter = "createdby";

				public const string lk_sdkmessagefilter_createdonbehalfby = "createdonbehalfby";

				public const string lk_sdkmessagefilter_modifiedonbehalfby = "modifiedonbehalfby";

				public const string modifiedby_sdkmessagefilter = "modifiedby";

				public const string sdkmessageid_sdkmessagefilter = "sdkmessageid";
			}

			public const string createdby_sdkmessagefilter = "createdby_sdkmessagefilter";

			public const string lk_sdkmessagefilter_createdonbehalfby = "lk_sdkmessagefilter_createdonbehalfby";

			public const string lk_sdkmessagefilter_modifiedonbehalfby = "lk_sdkmessagefilter_modifiedonbehalfby";

			public const string modifiedby_sdkmessagefilter = "modifiedby_sdkmessagefilter";

			public const string sdkmessageid_sdkmessagefilter = "sdkmessageid_sdkmessagefilter";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Sdk Message Filter";

	public const string SchemaName = "SdkMessageFilter";

	public const string EntityLogicalName = "sdkmessagefilter";

	public const int EntityTypeCode = 4607;

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

	[AttributeLogicalName("sdkmessagefilterid")]
	public override Guid Id
	{
		get
		{
			return Id;
		}
		set
		{
			SdkMessageFilterId = value;
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

	[AttributeLogicalName("createdby")]
	public EntityReference CreatedBy => GetAttributeValue<EntityReference>("createdby");

	[AttributeLogicalName("createdon")]
	public DateTime? CreatedOn => GetAttributeValue<DateTime?>("createdon");

	[AttributeLogicalName("createdonbehalfby")]
	public EntityReference CreatedOnBehalfBy => GetAttributeValue<EntityReference>("createdonbehalfby");

	[AttributeLogicalName("customizationlevel")]
	public int? CustomizationLevel => GetAttributeValue<int?>("customizationlevel");

	[AttributeLogicalName("iscustomprocessingstepallowed")]
	public bool? IsCustomProcessingStepAllowed
	{
		get
		{
			return GetAttributeValue<bool?>("iscustomprocessingstepallowed");
		}
		set
		{
			OnPropertyChanging("IsCustomProcessingStepAllowed");
			SetAttributeValue("iscustomprocessingstepallowed", (object)value);
			OnPropertyChanged("IsCustomProcessingStepAllowed");
		}
	}

	[AttributeLogicalName("isvisible")]
	public bool? IsVisible => GetAttributeValue<bool?>("isvisible");

	[AttributeLogicalName("modifiedby")]
	public EntityReference ModifiedBy => GetAttributeValue<EntityReference>("modifiedby");

	[AttributeLogicalName("modifiedon")]
	public DateTime? ModifiedOn => GetAttributeValue<DateTime?>("modifiedon");

	[AttributeLogicalName("modifiedonbehalfby")]
	public EntityReference ModifiedOnBehalfBy => GetAttributeValue<EntityReference>("modifiedonbehalfby");

	[AttributeLogicalName("organizationid")]
	public EntityReference OrganizationId => GetAttributeValue<EntityReference>("organizationid");

	[AttributeLogicalName("primaryobjecttypecode")]
	public string PrimaryObjectTypeCode => GetAttributeValue<string>("primaryobjecttypecode");

	[AttributeLogicalName("sdkmessagefilterid")]
	public Guid? SdkMessageFilterId
	{
		get
		{
			return GetAttributeValue<Guid?>("sdkmessagefilterid");
		}
		set
		{
			OnPropertyChanging("SdkMessageFilterId");
			SetAttributeValue("sdkmessagefilterid", (object)value);
			if (value.HasValue)
			{
				Id = value.Value;
			}
			else
			{
				Id = Guid.Empty;
			}
			OnPropertyChanged("SdkMessageFilterId");
		}
	}

	[AttributeLogicalName("sdkmessagefilteridunique")]
	public Guid? SdkMessageFilterIdUnique => GetAttributeValue<Guid?>("sdkmessagefilteridunique");

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

	[AttributeLogicalName("secondaryobjecttypecode")]
	public string SecondaryObjectTypeCode => GetAttributeValue<string>("secondaryobjecttypecode");

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("workflowsdkstepenabled")]
	public bool? WorkflowSdkStepEnabled => GetAttributeValue<bool?>("workflowsdkstepenabled");

	[RelationshipSchemaName("sdkmessagefilterid_sdkmessageprocessingstep")]
	public IEnumerable<SdkMessageProcessingStep> sdkmessagefilterid_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "sdkmessagefilterid_sdkmessageprocessingstep");
				}
				IEnumerable<SdkMessageProcessingStep> relatedEntities = GetRelatedEntities<SdkMessageProcessingStep>("sdkmessagefilterid_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageProcessingStep element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<SdkMessageProcessingStep>("sdkmessagefilterid_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("sdkmessagefilterid_sdkmessageprocessingstep");
			SetRelatedEntities<SdkMessageProcessingStep>("sdkmessagefilterid_sdkmessageprocessingstep", (EntityRole?)null, value);
			OnPropertyChanged("sdkmessagefilterid_sdkmessageprocessingstep");
		}
	}

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("createdby_sdkmessagefilter")]
	public SystemUser createdby_sdkmessagefilter
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "createdby_sdkmessagefilter");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("createdby_sdkmessagefilter", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("createdby_sdkmessagefilter", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("lk_sdkmessagefilter_createdonbehalfby")]
	public SystemUser lk_sdkmessagefilter_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessagefilter_createdonbehalfby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_sdkmessagefilter_createdonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_sdkmessagefilter_createdonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("lk_sdkmessagefilter_modifiedonbehalfby")]
	public SystemUser lk_sdkmessagefilter_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessagefilter_modifiedonbehalfby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_sdkmessagefilter_modifiedonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_sdkmessagefilter_modifiedonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("modifiedby_sdkmessagefilter")]
	public SystemUser modifiedby_sdkmessagefilter
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "modifiedby_sdkmessagefilter");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("modifiedby_sdkmessagefilter", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("modifiedby_sdkmessagefilter", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("sdkmessageid")]
	[RelationshipSchemaName("sdkmessageid_sdkmessagefilter")]
	public SdkMessage sdkmessageid_sdkmessagefilter
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "sdkmessageid_sdkmessagefilter");
				}
				SdkMessage relatedEntity = GetRelatedEntity<SdkMessage>("sdkmessageid_sdkmessagefilter", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SdkMessage>("sdkmessageid_sdkmessagefilter", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("sdkmessageid_sdkmessagefilter");
			SetRelatedEntity<SdkMessage>("sdkmessageid_sdkmessagefilter", (EntityRole?)null, value);
			OnPropertyChanged("sdkmessageid_sdkmessagefilter");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public SdkMessageFilter()
		: base("sdkmessagefilter")
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

	public List<SdkMessageProcessingStep> Load_sdkmessagefilterid_sdkmessageprocessingstep(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageProcessingStep> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessageprocessingstep", LogicalName, "sdkmessagefilterid", "sdkmessagefilterid", "sdkmessagefilterid", "sdkmessagefilterid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageProcessingStep>();
		sdkmessagefilterid_sdkmessageprocessingstep = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public SdkMessage Load_sdkmessageid_sdkmessagefilter(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessage", LogicalName, "sdkmessageid", "sdkmessageid", "sdkmessagefilterid", "sdkmessagefilterid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return sdkmessageid_sdkmessagefilter = val.ToEntity<SdkMessage>();
		}
		return sdkmessageid_sdkmessagefilter = null;
	}

	public SdkMessageFilter(object anonymousType)
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
				((DataCollection<string, object>)(object)Attributes)["sdkmessagefilterid"] = Id;
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
