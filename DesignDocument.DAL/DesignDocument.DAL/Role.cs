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
[EntityLogicalName("role")]
public class Role : Entity, INotifyPropertyChanging, INotifyPropertyChanged
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
			public const string BusinessUnitId = "BusinessUnitId";

			public const string BusinessUnitIdName = "BusinessUnitIdName";

			public const string ComponentState = "ComponentState";

			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string ImportSequenceNumber = "ImportSequenceNumber";

			public const string IsCustomizable = "IsCustomizable";

			public const string IsManaged = "IsManaged";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string Name = "Name";

			public const string OrganizationId = "OrganizationId";

			public const string OverriddenCreatedOn = "OverriddenCreatedOn";

			public const string OverwriteTime = "OverwriteTime";

			public const string ParentRoleId = "ParentRoleId";

			public const string ParentRoleIdName = "ParentRoleIdName";

			public const string ParentRootRoleId = "ParentRootRoleId";

			public const string ParentRootRoleIdName = "ParentRootRoleIdName";

			public const string RoleId = "RoleId";

			public const string RoleIdUnique = "RoleIdUnique";

			public const string RoleTemplateId = "RoleTemplateId";

			public const string RoleTemplateIdName = "RoleTemplateIdName";

			public const string SolutionId = "SolutionId";

			public const string SupportingSolutionId = "SupportingSolutionId";

			public const string VersionNumber = "VersionNumber";
		}

		public static class Labels
		{
			public static class BusinessUnitId
			{
				public const string _1033 = "Business Unit";

				public const string _1025 = "وحدة أعمال";
			}

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

			public static class ImportSequenceNumber
			{
				public const string _1033 = "Import Sequence Number";

				public const string _1025 = "الرقم التسلسلي للاستيراد";
			}

			public static class IsCustomizable
			{
				public const string _1033 = "Customizable";

				public const string _1025 = "قابل للتخصيص";
			}

			public static class IsManaged
			{
				public const string _1033 = "State";

				public const string _1025 = "الحالة";
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

			public static class OverriddenCreatedOn
			{
				public const string _1033 = "Record Created On";

				public const string _1025 = "وقت إنشاء السجل";
			}

			public static class OverwriteTime
			{
				public const string _1033 = "Record Overwrite Time";

				public const string _1025 = "وقت الكتابة فوق السجل";
			}

			public static class ParentRoleId
			{
				public const string _1033 = "Parent Role";

				public const string _1025 = "الدور الأصل";
			}

			public static class ParentRootRoleId
			{
				public const string _1033 = "Parent Root Role";

				public const string _1025 = "دور الجذر الأصل";
			}

			public static class RoleId
			{
				public const string _1033 = "Role";

				public const string _1025 = "الدور";
			}

			public static class RoleIdUnique
			{
				public const string _1033 = "Unique Id";

				public const string _1025 = "المعر\u0651ف الفريد";
			}

			public static class RoleTemplateId
			{
				public const string _1033 = "Role Template";

				public const string _1025 = "قالب الدور";
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

			public static class VersionNumber
			{
				public const string _1033 = "Version number";

				public const string _1025 = "رقم الإصدار";
			}
		}

		public const string BusinessUnitId = "businessunitid";

		public const string BusinessUnitIdName = "businessunitidName";

		public const string ComponentState = "componentstate";

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string ImportSequenceNumber = "importsequencenumber";

		public const string IsCustomizable = "iscustomizable";

		public const string IsManaged = "ismanaged";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string Name = "name";

		public const string OrganizationId = "organizationid";

		public const string OverriddenCreatedOn = "overriddencreatedon";

		public const string OverwriteTime = "overwritetime";

		public const string ParentRoleId = "parentroleid";

		public const string ParentRoleIdName = "parentroleidName";

		public const string ParentRootRoleId = "parentrootroleid";

		public const string ParentRootRoleIdName = "parentrootroleidName";

		public const string RoleId = "roleid";

		public const string RoleIdUnique = "roleidunique";

		public const string RoleTemplateId = "roletemplateid";

		public const string RoleTemplateIdName = "roletemplateidName";

		public const string SolutionId = "solutionid";

		public const string SupportingSolutionId = "supportingsolutionid";

		public const string VersionNumber = "versionnumber";
	}

	public static class Relations
	{
		public static class OneToN
		{
			public const string role_parent_role = "role_parent_role";

			public const string role_parent_root_role = "role_parent_root_role";
		}

		public static class NToOne
		{
			public static class Lookups
			{
				public const string lk_role_createdonbehalfby = "createdonbehalfby";

				public const string lk_role_modifiedonbehalfby = "modifiedonbehalfby";

				public const string lk_rolebase_createdby = "createdby";

				public const string lk_rolebase_modifiedby = "modifiedby";

				public const string role_parent_role = "parentroleid";

				public const string role_parent_root_role = "parentrootroleid";
			}

			public const string lk_role_createdonbehalfby = "lk_role_createdonbehalfby";

			public const string lk_role_modifiedonbehalfby = "lk_role_modifiedonbehalfby";

			public const string lk_rolebase_createdby = "lk_rolebase_createdby";

			public const string lk_rolebase_modifiedby = "lk_rolebase_modifiedby";

			public const string role_parent_role = "role_parent_role";

			public const string role_parent_root_role = "role_parent_root_role";
		}

		public static class NToN
		{
			public const string roleprivileges_association = "roleprivileges_association";

			public const string systemuserroles_association = "systemuserroles_association";
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Security Role";

	public const string SchemaName = "Role";

	public const string EntityLogicalName = "role";

	public const int EntityTypeCode = 1036;

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

	[AttributeLogicalName("roleid")]
	public override Guid Id
	{
		get
		{
			return ((Entity)this).Id;
		}
		set
		{
			RoleId = value;
		}
	}

	[AttributeLogicalName("businessunitid")]
	public EntityReference BusinessUnitId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("businessunitid");
		}
		set
		{
			OnPropertyChanging("BusinessUnitId");
			((Entity)this).SetAttributeValue("businessunitid", (object)value);
			OnPropertyChanged("BusinessUnitId");
		}
	}

	[AttributeLogicalName("componentstate")]
	public OptionSetValue ComponentState => ((Entity)this).GetAttributeValue<OptionSetValue>("componentstate");

	[AttributeLogicalName("createdby")]
	public EntityReference CreatedBy => ((Entity)this).GetAttributeValue<EntityReference>("createdby");

	[AttributeLogicalName("createdon")]
	public DateTime? CreatedOn => ((Entity)this).GetAttributeValue<DateTime?>("createdon");

	[AttributeLogicalName("createdonbehalfby")]
	public EntityReference CreatedOnBehalfBy => ((Entity)this).GetAttributeValue<EntityReference>("createdonbehalfby");

	[AttributeLogicalName("importsequencenumber")]
	public int? ImportSequenceNumber
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("importsequencenumber");
		}
		set
		{
			OnPropertyChanging("ImportSequenceNumber");
			((Entity)this).SetAttributeValue("importsequencenumber", (object)value);
			OnPropertyChanged("ImportSequenceNumber");
		}
	}

	[AttributeLogicalName("iscustomizable")]
	public BooleanManagedProperty IsCustomizable
	{
		get
		{
			return ((Entity)this).GetAttributeValue<BooleanManagedProperty>("iscustomizable");
		}
		set
		{
			OnPropertyChanging("IsCustomizable");
			((Entity)this).SetAttributeValue("iscustomizable", (object)value);
			OnPropertyChanged("IsCustomizable");
		}
	}

	[AttributeLogicalName("ismanaged")]
	public bool? IsManaged => ((Entity)this).GetAttributeValue<bool?>("ismanaged");

	[AttributeLogicalName("modifiedby")]
	public EntityReference ModifiedBy => ((Entity)this).GetAttributeValue<EntityReference>("modifiedby");

	[AttributeLogicalName("modifiedon")]
	public DateTime? ModifiedOn => ((Entity)this).GetAttributeValue<DateTime?>("modifiedon");

	[AttributeLogicalName("modifiedonbehalfby")]
	public EntityReference ModifiedOnBehalfBy => ((Entity)this).GetAttributeValue<EntityReference>("modifiedonbehalfby");

	[AttributeLogicalName("name")]
	public string Name
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("name");
		}
		set
		{
			OnPropertyChanging("Name");
			((Entity)this).SetAttributeValue("name", (object)value);
			OnPropertyChanged("Name");
		}
	}

	[AttributeLogicalName("organizationid")]
	public Guid? OrganizationId => ((Entity)this).GetAttributeValue<Guid?>("organizationid");

	[AttributeLogicalName("overriddencreatedon")]
	public DateTime? OverriddenCreatedOn
	{
		get
		{
			return ((Entity)this).GetAttributeValue<DateTime?>("overriddencreatedon");
		}
		set
		{
			OnPropertyChanging("OverriddenCreatedOn");
			((Entity)this).SetAttributeValue("overriddencreatedon", (object)value);
			OnPropertyChanged("OverriddenCreatedOn");
		}
	}

	[AttributeLogicalName("overwritetime")]
	public DateTime? OverwriteTime => ((Entity)this).GetAttributeValue<DateTime?>("overwritetime");

	[AttributeLogicalName("parentroleid")]
	public EntityReference ParentRoleId => ((Entity)this).GetAttributeValue<EntityReference>("parentroleid");

	[AttributeLogicalName("parentrootroleid")]
	public EntityReference ParentRootRoleId => ((Entity)this).GetAttributeValue<EntityReference>("parentrootroleid");

	[AttributeLogicalName("roleid")]
	public Guid? RoleId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("roleid");
		}
		set
		{
			OnPropertyChanging("RoleId");
			((Entity)this).SetAttributeValue("roleid", (object)value);
			if (value.HasValue)
			{
				((Entity)this).Id = value.Value;
			}
			else
			{
				((Entity)this).Id = Guid.Empty;
			}
			OnPropertyChanged("RoleId");
		}
	}

	[AttributeLogicalName("roleidunique")]
	public Guid? RoleIdUnique => ((Entity)this).GetAttributeValue<Guid?>("roleidunique");

	[AttributeLogicalName("roletemplateid")]
	public EntityReference RoleTemplateId => ((Entity)this).GetAttributeValue<EntityReference>("roletemplateid");

	[AttributeLogicalName("solutionid")]
	public Guid? SolutionId => ((Entity)this).GetAttributeValue<Guid?>("solutionid");

	[AttributeLogicalName("supportingsolutionid")]
	public Guid? SupportingSolutionId => ((Entity)this).GetAttributeValue<Guid?>("supportingsolutionid");

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => ((Entity)this).GetAttributeValue<long?>("versionnumber");

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<Role> Referenced_role_parent_role
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "role_parent_role");
				}
				IEnumerable<Role> relatedEntities = ((Entity)this).GetRelatedEntities<Role>("role_parent_role", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(Role element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Role>("role_parent_role", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_role_parent_role");
			((Entity)this).SetRelatedEntities<Role>("role_parent_role", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_role_parent_role");
		}
	}

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<Role> Referenced_role_parent_root_role
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "role_parent_root_role");
				}
				IEnumerable<Role> relatedEntities = ((Entity)this).GetRelatedEntities<Role>("role_parent_root_role", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(Role element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Role>("role_parent_root_role", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_role_parent_root_role");
			((Entity)this).SetRelatedEntities<Role>("role_parent_root_role", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_role_parent_root_role");
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("lk_role_createdonbehalfby")]
	public SystemUser lk_role_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_role_createdonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_role_createdonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_role_createdonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("lk_role_modifiedonbehalfby")]
	public SystemUser lk_role_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_role_modifiedonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_role_modifiedonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_role_modifiedonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("lk_rolebase_createdby")]
	public SystemUser lk_rolebase_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_rolebase_createdby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_rolebase_createdby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_rolebase_createdby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("lk_rolebase_modifiedby")]
	public SystemUser lk_rolebase_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_rolebase_modifiedby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_rolebase_modifiedby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_rolebase_modifiedby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("parentroleid")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public Role Referencing_role_parent_role
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "role_parent_role");
				}
				Role relatedEntity = ((Entity)this).GetRelatedEntity<Role>("role_parent_role", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<Role>("role_parent_role", (EntityRole?)(EntityRole)0);
			}
		}
	}

	[AttributeLogicalName("parentrootroleid")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public Role Referencing_role_parent_root_role
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "role_parent_root_role");
				}
				Role relatedEntity = ((Entity)this).GetRelatedEntity<Role>("role_parent_root_role", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<Role>("role_parent_root_role", (EntityRole?)(EntityRole)0);
			}
		}
	}

	[RelationshipSchemaName("roleprivileges_association")]
	public IEnumerable<Privilege> roleprivileges_association
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "roleprivileges_association");
				}
				IEnumerable<Privilege> relatedEntities = ((Entity)this).GetRelatedEntities<Privilege>("roleprivileges_association", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Privilege element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Privilege>("roleprivileges_association", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("roleprivileges_association");
			((Entity)this).SetRelatedEntities<Privilege>("roleprivileges_association", (EntityRole?)null, value);
			OnPropertyChanged("roleprivileges_association");
		}
	}

	[RelationshipSchemaName("systemuserroles_association")]
	public IEnumerable<SystemUser> systemuserroles_association
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "systemuserroles_association");
				}
				IEnumerable<SystemUser> relatedEntities = ((Entity)this).GetRelatedEntities<SystemUser>("systemuserroles_association", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SystemUser element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SystemUser>("systemuserroles_association", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("systemuserroles_association");
			((Entity)this).SetRelatedEntities<SystemUser>("systemuserroles_association", (EntityRole?)null, value);
			OnPropertyChanged("systemuserroles_association");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public Role()
		: base("role")
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

	public List<Role> Load_Referenced_role_parent_role(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Role> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "role", ((Entity)this).LogicalName, "parentroleid", "roleid", "roleid", "roleid", recordCountLimit, attributes)
			select entity.ToEntity<Role>();
		Referenced_role_parent_role = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Role> Load_Referenced_role_parent_root_role(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Role> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "role", ((Entity)this).LogicalName, "parentrootroleid", "roleid", "roleid", "roleid", recordCountLimit, attributes)
			select entity.ToEntity<Role>();
		Referenced_role_parent_root_role = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Privilege> Load_roleprivileges_association(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Privilege> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "privilege", "roleprivileges", "privilegeid", "privilegeid", "roleid", "roleid", recordCountLimit, attributes)
			select entity.ToEntity<Privilege>();
		roleprivileges_association = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public Role(object anonymousType)
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
				((Entity)this).Id = (Guid)value;
				((DataCollection<string, object>)(object)((Entity)this).Attributes)["roleid"] = ((Entity)this).Id;
			}
			else if (propertyInfo.Name == "FormattedValues")
			{
				((DataCollection<string, string>)(object)((Entity)this).FormattedValues).AddRange((IEnumerable<KeyValuePair<string, string>>)(FormattedValueCollection)value);
			}
			else
			{
				((DataCollection<string, object>)(object)((Entity)this).Attributes)[propertyInfo.Name.ToLower()] = value;
			}
		}
	}
}
