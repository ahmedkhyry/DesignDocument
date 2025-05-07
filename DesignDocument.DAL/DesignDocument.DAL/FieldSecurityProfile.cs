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
[EntityLogicalName("fieldsecurityprofile")]
public class FieldSecurityProfile : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
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
		public enum IsManaged
		{
			[EnumMember]
			Managed = 1,
			[EnumMember]
			Unmanaged = 0
		}

		public static class Names
		{
			public const string ComponentState = "componentstate";

			public const string IsManaged = "ismanaged";
		}

		public static class Labels
		{
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
			public const string ComponentState = "ComponentState";

			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string Description = "Description";

			public const string FieldSecurityProfileId = "FieldSecurityProfileId";

			public const string FieldSecurityProfileIdUnique = "FieldSecurityProfileIdUnique";

			public const string IsManaged = "IsManaged";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string Name = "Name";

			public const string OrganizationId = "OrganizationId";

			public const string OrganizationIdName = "OrganizationIdName";

			public const string OverwriteTime = "OverwriteTime";

			public const string SolutionId = "SolutionId";

			public const string SupportingSolutionId = "SupportingSolutionId";

			public const string VersionNumber = "VersionNumber";
		}

		public static class Labels
		{
			public static class ComponentState
			{
				public const string _1033 = "Component State";

				public const string _1025 = "حالة المكون";
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
				public const string _1033 = "Created By Impersonator";

				public const string _1025 = "قام بإنشائه المنتحل";
			}

			public static class Description
			{
				public const string _1033 = "Description";

				public const string _1025 = "الوصف";
			}

			public static class FieldSecurityProfileId
			{
				public const string _1033 = "Field Security Profile";

				public const string _1025 = "ملف تعريف أمان الحقل";
			}

			public static class FieldSecurityProfileIdUnique
			{
				public const string _1033 = "Field Security Profile";

				public const string _1025 = "ملف تعريف أمان الحقل";
			}

			public static class IsManaged
			{
				public const string _1033 = "Is Managed";

				public const string _1025 = "هو م\u064fدار";
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

			public static class OrganizationId
			{
				public const string _1033 = "Organization";

				public const string _1025 = "المؤسسة";
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

		public const string ComponentState = "componentstate";

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string Description = "description";

		public const string FieldSecurityProfileId = "fieldsecurityprofileid";

		public const string FieldSecurityProfileIdUnique = "fieldsecurityprofileidunique";

		public const string IsManaged = "ismanaged";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string Name = "name";

		public const string OrganizationId = "organizationid";

		public const string OrganizationIdName = "organizationidName";

		public const string OverwriteTime = "overwritetime";

		public const string SolutionId = "solutionid";

		public const string SupportingSolutionId = "supportingsolutionid";

		public const string VersionNumber = "versionnumber";
	}

	public static class Relations
	{
		public static class OneToN
		{
			public const string lk_fieldpermission_fieldsecurityprofileid = "lk_fieldpermission_fieldsecurityprofileid";
		}

		public static class NToOne
		{
			public static class Lookups
			{
				public const string lk_fieldsecurityprofile_createdby = "createdby";

				public const string lk_fieldsecurityprofile_createdonbehalfby = "createdonbehalfby";

				public const string lk_fieldsecurityprofile_modifiedby = "modifiedby";

				public const string lk_fieldsecurityprofile_modifiedonbehalfby = "modifiedonbehalfby";
			}

			public const string lk_fieldsecurityprofile_createdby = "lk_fieldsecurityprofile_createdby";

			public const string lk_fieldsecurityprofile_createdonbehalfby = "lk_fieldsecurityprofile_createdonbehalfby";

			public const string lk_fieldsecurityprofile_modifiedby = "lk_fieldsecurityprofile_modifiedby";

			public const string lk_fieldsecurityprofile_modifiedonbehalfby = "lk_fieldsecurityprofile_modifiedonbehalfby";
		}

		public static class NToN
		{
			public const string systemuserprofiles_association = "systemuserprofiles_association";
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Field Security Profile";

	public const string SchemaName = "FieldSecurityProfile";

	public const string EntityLogicalName = "fieldsecurityprofile";

	public const int EntityTypeCode = 1200;

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

	[AttributeLogicalName("fieldsecurityprofileid")]
	public override Guid Id
	{
		get
		{
			return Id;
		}
		set
		{
			FieldSecurityProfileId = value;
		}
	}

	[AttributeLogicalName("componentstate")]
	public OptionSetValue ComponentState => GetAttributeValue<OptionSetValue>("componentstate");

	[AttributeLogicalName("createdby")]
	public EntityReference CreatedBy => GetAttributeValue<EntityReference>("createdby");

	[AttributeLogicalName("createdon")]
	public DateTime? CreatedOn => GetAttributeValue<DateTime?>("createdon");

	[AttributeLogicalName("createdonbehalfby")]
	public EntityReference CreatedOnBehalfBy => GetAttributeValue<EntityReference>("createdonbehalfby");

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

	[AttributeLogicalName("fieldsecurityprofileid")]
	public Guid? FieldSecurityProfileId
	{
		get
		{
			return GetAttributeValue<Guid?>("fieldsecurityprofileid");
		}
		set
		{
			OnPropertyChanging("FieldSecurityProfileId");
			SetAttributeValue("fieldsecurityprofileid", (object)value);
			if (value.HasValue)
			{
				Id = value.Value;
			}
			else
			{
				Id = Guid.Empty;
			}
			OnPropertyChanged("FieldSecurityProfileId");
		}
	}

	[AttributeLogicalName("fieldsecurityprofileidunique")]
	public Guid? FieldSecurityProfileIdUnique => GetAttributeValue<Guid?>("fieldsecurityprofileidunique");

	[AttributeLogicalName("ismanaged")]
	public bool? IsManaged => GetAttributeValue<bool?>("ismanaged");

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

	[AttributeLogicalName("solutionid")]
	public Guid? SolutionId => GetAttributeValue<Guid?>("solutionid");

	[AttributeLogicalName("supportingsolutionid")]
	public Guid? SupportingSolutionId => GetAttributeValue<Guid?>("supportingsolutionid");

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => GetAttributeValue<long?>("versionnumber");

	[RelationshipSchemaName("lk_fieldpermission_fieldsecurityprofileid")]
	public IEnumerable<FieldPermission> lk_fieldpermission_fieldsecurityprofileid
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldpermission_fieldsecurityprofileid");
				}
				IEnumerable<FieldPermission> relatedEntities = GetRelatedEntities<FieldPermission>("lk_fieldpermission_fieldsecurityprofileid", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(FieldPermission element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<FieldPermission>("lk_fieldpermission_fieldsecurityprofileid", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_fieldpermission_fieldsecurityprofileid");
			SetRelatedEntities<FieldPermission>("lk_fieldpermission_fieldsecurityprofileid", (EntityRole?)null, value);
			OnPropertyChanged("lk_fieldpermission_fieldsecurityprofileid");
		}
	}

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("lk_fieldsecurityprofile_createdby")]
	public SystemUser lk_fieldsecurityprofile_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldsecurityprofile_createdby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_fieldsecurityprofile_createdby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_fieldsecurityprofile_createdby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("lk_fieldsecurityprofile_createdonbehalfby")]
	public SystemUser lk_fieldsecurityprofile_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldsecurityprofile_createdonbehalfby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_fieldsecurityprofile_createdonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_fieldsecurityprofile_createdonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("lk_fieldsecurityprofile_modifiedby")]
	public SystemUser lk_fieldsecurityprofile_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldsecurityprofile_modifiedby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_fieldsecurityprofile_modifiedby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_fieldsecurityprofile_modifiedby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("lk_fieldsecurityprofile_modifiedonbehalfby")]
	public SystemUser lk_fieldsecurityprofile_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldsecurityprofile_modifiedonbehalfby");
				}
				SystemUser relatedEntity = GetRelatedEntity<SystemUser>("lk_fieldsecurityprofile_modifiedonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemUser>("lk_fieldsecurityprofile_modifiedonbehalfby", (EntityRole?)null);
			}
		}
	}

	[RelationshipSchemaName("systemuserprofiles_association")]
	public IEnumerable<SystemUser> systemuserprofiles_association
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "systemuserprofiles_association");
				}
				IEnumerable<SystemUser> relatedEntities = GetRelatedEntities<SystemUser>("systemuserprofiles_association", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SystemUser element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<SystemUser>("systemuserprofiles_association", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("systemuserprofiles_association");
			SetRelatedEntities<SystemUser>("systemuserprofiles_association", (EntityRole?)null, value);
			OnPropertyChanged("systemuserprofiles_association");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public FieldSecurityProfile()
		: base("fieldsecurityprofile")
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

	public List<FieldPermission> Load_lk_fieldpermission_fieldsecurityprofileid(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<FieldPermission> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "fieldpermission", LogicalName, "fieldsecurityprofileid", "fieldsecurityprofileid", "fieldsecurityprofileid", "fieldsecurityprofileid", recordCountLimit, attributes)
			select entity.ToEntity<FieldPermission>();
		lk_fieldpermission_fieldsecurityprofileid = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public FieldSecurityProfile(object anonymousType)
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
				((DataCollection<string, object>)(object)Attributes)["fieldsecurityprofileid"] = Id;
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
