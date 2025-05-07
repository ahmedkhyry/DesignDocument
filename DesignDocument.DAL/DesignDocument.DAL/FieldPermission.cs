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
[EntityLogicalName("fieldpermission")]
public class FieldPermission : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum CanCreate
		{
			[EnumMember]
			NotAllowed = 0,
			[EnumMember]
			Allowed = 4
		}

		[DataContract]
		public enum CanRead
		{
			[EnumMember]
			NotAllowed = 0,
			[EnumMember]
			Allowed = 4
		}

		[DataContract]
		public enum CanUpdate
		{
			[EnumMember]
			NotAllowed = 0,
			[EnumMember]
			Allowed = 4
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
		public enum EntityName
		{

		}

		[DataContract]
		public enum IsManaged
		{
			[EnumMember]
			Managed = 1,
			[EnumMember]
			Unmanaged = 0
		}

		public static class Names
		{
			public const string CanCreate = "cancreate";

			public const string CanRead = "canread";

			public const string CanUpdate = "canupdate";

			public const string ComponentState = "componentstate";

			public const string EntityName = "entityname";

			public const string IsManaged = "ismanaged";
		}

		public static class Labels
		{
			public static class CanCreate
			{
				public const string NotAllowed_1033 = "Not Allowed";

				public const string NotAllowed_1025 = "\u200f\u200fغير مسموح به";

				public const string Allowed_1033 = "Allowed";

				public const string Allowed_1025 = "\u200f\u200fمسموح به";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanCreate), label, languageCode);
				}
			}

			public static class CanRead
			{
				public const string NotAllowed_1033 = "Not Allowed";

				public const string NotAllowed_1025 = "\u200f\u200fغير مسموح به";

				public const string Allowed_1033 = "Allowed";

				public const string Allowed_1025 = "\u200f\u200fمسموح به";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanRead), label, languageCode);
				}
			}

			public static class CanUpdate
			{
				public const string NotAllowed_1033 = "Not Allowed";

				public const string NotAllowed_1025 = "\u200f\u200fغير مسموح به";

				public const string Allowed_1033 = "Allowed";

				public const string Allowed_1025 = "\u200f\u200fمسموح به";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanUpdate), label, languageCode);
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

			public static class EntityName
			{
				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(EntityName), label, languageCode);
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
			public const string AttributeLogicalName = "AttributeLogicalName";

			public const string CanCreate = "CanCreate";

			public const string CanRead = "CanRead";

			public const string CanUpdate = "CanUpdate";

			public const string ComponentState = "ComponentState";

			public const string EntityName = "EntityName";

			public const string FieldPermissionId = "FieldPermissionId";

			public const string FieldPermissionIdUnique = "FieldPermissionIdUnique";

			public const string FieldSecurityProfileId = "FieldSecurityProfileId";

			public const string FieldSecurityProfileIdName = "FieldSecurityProfileIdName";

			public const string IsManaged = "IsManaged";

			public const string OrganizationId = "OrganizationId";

			public const string OrganizationIdName = "OrganizationIdName";

			public const string OrganizationIdType = "OrganizationIdType";

			public const string OverwriteTime = "OverwriteTime";

			public const string SolutionId = "SolutionId";

			public const string SupportingSolutionId = "SupportingSolutionId";

			public const string VersionNumber = "VersionNumber";
		}

		public static class Labels
		{
			public static class AttributeLogicalName
			{
				public const string _1033 = "Name of the attribute for which this privilege is defined";

				public const string _1025 = "اسم السمة التي يتم تحديد هذا الامتياز لها";
			}

			public static class CanCreate
			{
				public const string _1033 = "Can create the attribute";

				public const string _1025 = "يمكنه إنشاء السمة";
			}

			public static class CanRead
			{
				public const string _1033 = "Can Read the attribute";

				public const string _1025 = "يمكنه قراءة السمة";
			}

			public static class CanUpdate
			{
				public const string _1033 = "Can Update the attribute";

				public const string _1025 = "يمكنه تحديث السمة";
			}

			public static class ComponentState
			{
				public const string _1033 = "Component State";

				public const string _1025 = "حالة المكون";
			}

			public static class EntityName
			{
				public const string _1033 = "Name of the Entity for which this privilege is defined";

				public const string _1025 = "اسم الكيان الذي تم تحديد هذا الامتياز له";
			}

			public static class FieldPermissionId
			{
				public const string _1033 = "Field Permission";

				public const string _1025 = "إذن الحقل";
			}

			public static class FieldPermissionIdUnique
			{
				public const string _1033 = "Field Permission";

				public const string _1025 = "إذن الحقل";
			}

			public static class FieldSecurityProfileId
			{
				public const string _1033 = "Profile";

				public const string _1025 = "ملف التعريف";
			}

			public static class IsManaged
			{
				public const string _1033 = "Is Managed";

				public const string _1025 = "هو م\u064fدار";
			}

			public static class OrganizationId
			{
				public const string _1033 = "Organization Id";

				public const string _1025 = "معرف المؤسسة";
			}

			public static class OverwriteTime
			{
				public const string _1033 = "Record Overwrite Time";

				public const string _1025 = "وقت الكتابة فوق السجل";
			}

			public static class SolutionId
			{
				public const string _1033 = "Solution";

				public const string _1025 = "الحل";
			}

			public static class SupportingSolutionId
			{
				public const string _1033 = "Solution";

				public const string _1025 = "الحل";
			}
		}

		public const string AttributeLogicalName = "attributelogicalname";

		public const string CanCreate = "cancreate";

		public const string CanRead = "canread";

		public const string CanUpdate = "canupdate";

		public const string ComponentState = "componentstate";

		public const string EntityName = "entityname";

		public const string FieldPermissionId = "fieldpermissionid";

		public const string FieldPermissionIdUnique = "fieldpermissionidunique";

		public const string FieldSecurityProfileId = "fieldsecurityprofileid";

		public const string FieldSecurityProfileIdName = "fieldsecurityprofileidName";

		public const string IsManaged = "ismanaged";

		public const string OrganizationId = "organizationid";

		public const string OrganizationIdName = "organizationidName";

		public const string OrganizationIdType = "organizationidType";

		public const string OverwriteTime = "overwritetime";

		public const string SolutionId = "solutionid";

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
				public const string lk_fieldpermission_fieldsecurityprofileid = "fieldsecurityprofileid";
			}

			public const string lk_fieldpermission_fieldsecurityprofileid = "lk_fieldpermission_fieldsecurityprofileid";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Field Permission";

	public const string SchemaName = "FieldPermission";

	public const string EntityLogicalName = "fieldpermission";

	public const int EntityTypeCode = 1201;

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

	[AttributeLogicalName("fieldpermissionid")]
	public override Guid Id
	{
		get
		{
			return Id;
		}
		set
		{
			FieldPermissionId = value;
		}
	}

	[AttributeLogicalName("attributelogicalname")]
	public string AttributeLogicalName
	{
		get
		{
			return GetAttributeValue<string>("attributelogicalname");
		}
		set
		{
			OnPropertyChanging("AttributeLogicalName");
			SetAttributeValue("attributelogicalname", (object)value);
			OnPropertyChanged("AttributeLogicalName");
		}
	}

	[AttributeLogicalName("cancreate")]
	public OptionSetValue CanCreate
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("cancreate");
		}
		set
		{
			OnPropertyChanging("CanCreate");
			SetAttributeValue("cancreate", (object)value);
			OnPropertyChanged("CanCreate");
		}
	}

	[AttributeLogicalName("canread")]
	public OptionSetValue CanRead
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("canread");
		}
		set
		{
			OnPropertyChanging("CanRead");
			SetAttributeValue("canread", (object)value);
			OnPropertyChanged("CanRead");
		}
	}

	[AttributeLogicalName("canupdate")]
	public OptionSetValue CanUpdate
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("canupdate");
		}
		set
		{
			OnPropertyChanging("CanUpdate");
			SetAttributeValue("canupdate", (object)value);
			OnPropertyChanged("CanUpdate");
		}
	}

	[AttributeLogicalName("componentstate")]
	public OptionSetValue ComponentState => GetAttributeValue<OptionSetValue>("componentstate");

	[AttributeLogicalName("entityname")]
	public string EntityName
	{
		get
		{
			return GetAttributeValue<string>("entityname");
		}
		set
		{
			OnPropertyChanging("EntityName");
			SetAttributeValue("entityname", (object)value);
			OnPropertyChanged("EntityName");
		}
	}

	[AttributeLogicalName("fieldpermissionid")]
	public Guid? FieldPermissionId
	{
		get
		{
			return GetAttributeValue<Guid?>("fieldpermissionid");
		}
		set
		{
			OnPropertyChanging("FieldPermissionId");
			SetAttributeValue("fieldpermissionid", (object)value);
			if (value.HasValue)
			{
				Id = value.Value;
			}
			else
			{
				Id = Guid.Empty;
			}
			OnPropertyChanged("FieldPermissionId");
		}
	}

	[AttributeLogicalName("fieldpermissionidunique")]
	public Guid? FieldPermissionIdUnique => GetAttributeValue<Guid?>("fieldpermissionidunique");

	[AttributeLogicalName("fieldsecurityprofileid")]
	public EntityReference FieldSecurityProfileId
	{
		get
		{
			return GetAttributeValue<EntityReference>("fieldsecurityprofileid");
		}
		set
		{
			OnPropertyChanging("FieldSecurityProfileId");
			SetAttributeValue("fieldsecurityprofileid", (object)value);
			OnPropertyChanged("FieldSecurityProfileId");
		}
	}

	[AttributeLogicalName("ismanaged")]
	public bool? IsManaged => GetAttributeValue<bool?>("ismanaged");

	[AttributeLogicalName("organizationid")]
	public EntityReference OrganizationId => GetAttributeValue<EntityReference>("organizationid");

	[AttributeLogicalName("overwritetime")]
	public DateTime? OverwriteTime => GetAttributeValue<DateTime?>("overwritetime");

	[AttributeLogicalName("solutionid")]
	public Guid? SolutionId => GetAttributeValue<Guid?>("solutionid");

	[AttributeLogicalName("supportingsolutionid")]
	public Guid? SupportingSolutionId => GetAttributeValue<Guid?>("supportingsolutionid");

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("fieldsecurityprofileid")]
	[RelationshipSchemaName("lk_fieldpermission_fieldsecurityprofileid")]
	public FieldSecurityProfile lk_fieldpermission_fieldsecurityprofileid
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldpermission_fieldsecurityprofileid");
				}
				FieldSecurityProfile relatedEntity = GetRelatedEntity<FieldSecurityProfile>("lk_fieldpermission_fieldsecurityprofileid", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<FieldSecurityProfile>("lk_fieldpermission_fieldsecurityprofileid", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_fieldpermission_fieldsecurityprofileid");
			SetRelatedEntity<FieldSecurityProfile>("lk_fieldpermission_fieldsecurityprofileid", (EntityRole?)null, value);
			OnPropertyChanged("lk_fieldpermission_fieldsecurityprofileid");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public FieldPermission()
		: base("fieldpermission")
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

	public FieldSecurityProfile Load_lk_fieldpermission_fieldsecurityprofileid(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "fieldsecurityprofile", LogicalName, "fieldsecurityprofileid", "fieldsecurityprofileid", "fieldpermissionid", "fieldpermissionid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return lk_fieldpermission_fieldsecurityprofileid = val.ToEntity<FieldSecurityProfile>();
		}
		return lk_fieldpermission_fieldsecurityprofileid = null;
	}

	public FieldPermission(object anonymousType)
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
				((DataCollection<string, object>)(object)Attributes)["fieldpermissionid"] = Id;
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
