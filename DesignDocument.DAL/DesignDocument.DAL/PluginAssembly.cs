using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace DesignDocument.DAL;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[DataContract]
[EntityLogicalName("pluginassembly")]
public class PluginAssembly : Entity, INotifyPropertyChanging, INotifyPropertyChanged
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

		[DataContract]
		public enum IsolationMode
		{
			[EnumMember]
			None = 1,
			[EnumMember]
			Sandbox
		}

		[DataContract]
		public enum SourceType
		{
			[EnumMember]
			Database,
			[EnumMember]
			Disk,
			[EnumMember]
			Normal
		}

		public static class Names
		{
			public const string ComponentState = "componentstate";

			public const string IsManaged = "ismanaged";

			public const string IsolationMode = "isolationmode";

			public const string SourceType = "sourcetype";
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

			public static class IsolationMode
			{
				public const string None_1033 = "None";

				public const string None_1025 = "بلا";

				public const string Sandbox_1033 = "Sandbox";

				public const string Sandbox_1025 = "\u200f\u200fتحديد آلية الوصول";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsolationMode), label, languageCode);
				}
			}

			public static class SourceType
			{
				public const string Database_1033 = "Database";

				public const string Database_1025 = "قاعدة بيانات";

				public const string Disk_1033 = "Disk";

				public const string Disk_1025 = "قرص";

				public const string Normal_1033 = "Normal";

				public const string Normal_1025 = "عادي";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(SourceType), label, languageCode);
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

			public const string Content = "Content";

			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string Culture = "Culture";

			public const string CustomizationLevel = "CustomizationLevel";

			public const string Description = "Description";

			public const string IntroducedVersion = "IntroducedVersion";

			public const string IsHidden = "IsHidden";

			public const string IsManaged = "IsManaged";

			public const string IsolationMode = "IsolationMode";

			public const string Major = "Major";

			public const string Minor = "Minor";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string Name = "Name";

			public const string OrganizationId = "OrganizationId";

			public const string OrganizationIdName = "OrganizationIdName";

			public const string OverwriteTime = "OverwriteTime";

			public const string Path = "Path";

			public const string PluginAssemblyId = "PluginAssemblyId";

			public const string PluginAssemblyIdUnique = "PluginAssemblyIdUnique";

			public const string PublicKeyToken = "PublicKeyToken";

			public const string SolutionId = "SolutionId";

			public const string SourceHash = "SourceHash";

			public const string SourceType = "SourceType";

			public const string SupportingSolutionId = "SupportingSolutionId";

			public const string Version = "Version";

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
				public const string _1033 = "Created By (Delegate)";

				public const string _1025 = "قام بإنشائه (المفو\u0651ض)";
			}

			public static class Culture
			{
				public const string _1033 = "Culture";

				public const string _1025 = "\u200f\u200fالبيئة";
			}

			public static class Description
			{
				public const string _1033 = "Description";

				public const string _1025 = "الوصف";
			}

			public static class IntroducedVersion
			{
				public const string _1033 = "Introduced Version";

				public const string _1025 = "إصدار م\u064fقد\u0651\u064eم";
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

			public static class IsolationMode
			{
				public const string _1033 = "Isolation Mode";

				public const string _1025 = "\u200f\u200fوضع العزل";
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

			public static class Path
			{
				public const string _1033 = "Path";

				public const string _1025 = "المسار";
			}

			public static class PublicKeyToken
			{
				public const string _1033 = "Public Key Token";

				public const string _1025 = "الرمز المميز للمفتاح العام";
			}

			public static class SolutionId
			{
				public const string _1033 = "Solution";

				public const string _1025 = "الحل";
			}

			public static class SourceType
			{
				public const string _1033 = "Source Type";

				public const string _1025 = "نوع المصدر";
			}

			public static class SupportingSolutionId
			{
				public const string _1033 = "Solution";

				public const string _1025 = "الحل";
			}

			public static class Version
			{
				public const string _1033 = "Version";

				public const string _1025 = "\u200f\u200fالإصدار";
			}
		}

		public const string ComponentState = "componentstate";

		public const string Content = "content";

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string Culture = "culture";

		public const string CustomizationLevel = "customizationlevel";

		public const string Description = "description";

		public const string IntroducedVersion = "introducedversion";

		public const string IsHidden = "ishidden";

		public const string IsManaged = "ismanaged";

		public const string IsolationMode = "isolationmode";

		public const string Major = "major";

		public const string Minor = "minor";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string Name = "name";

		public const string OrganizationId = "organizationid";

		public const string OrganizationIdName = "organizationidName";

		public const string OverwriteTime = "overwritetime";

		public const string Path = "path";

		public const string PluginAssemblyId = "pluginassemblyid";

		public const string PluginAssemblyIdUnique = "pluginassemblyidunique";

		public const string PublicKeyToken = "publickeytoken";

		public const string SolutionId = "solutionid";

		public const string SourceHash = "sourcehash";

		public const string SourceType = "sourcetype";

		public const string SupportingSolutionId = "supportingsolutionid";

		public const string Version = "version";

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
				public const string createdby_pluginassembly = "createdby";

				public const string lk_pluginassembly_createdonbehalfby = "createdonbehalfby";

				public const string lk_pluginassembly_modifiedonbehalfby = "modifiedonbehalfby";

				public const string modifiedby_pluginassembly = "modifiedby";
			}

			public const string createdby_pluginassembly = "createdby_pluginassembly";

			public const string lk_pluginassembly_createdonbehalfby = "lk_pluginassembly_createdonbehalfby";

			public const string lk_pluginassembly_modifiedonbehalfby = "lk_pluginassembly_modifiedonbehalfby";

			public const string modifiedby_pluginassembly = "modifiedby_pluginassembly";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Plug-in Assembly";

	public const string SchemaName = "PluginAssembly";

	public const string EntityLogicalName = "pluginassembly";

	public const int EntityTypeCode = 4605;

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

	[AttributeLogicalName("pluginassemblyid")]
	public override Guid Id
	{
		get
		{
			return ((Entity)this).Id;
		}
		set
		{
			PluginAssemblyId = value;
		}
	}

	[AttributeLogicalName("componentstate")]
	public OptionSetValue ComponentState => ((Entity)this).GetAttributeValue<OptionSetValue>("componentstate");

	[AttributeLogicalName("content")]
	public string Content
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("content");
		}
		set
		{
			OnPropertyChanging("Content");
			((Entity)this).SetAttributeValue("content", (object)value);
			OnPropertyChanged("Content");
		}
	}

	[AttributeLogicalName("createdby")]
	public EntityReference CreatedBy => ((Entity)this).GetAttributeValue<EntityReference>("createdby");

	[AttributeLogicalName("createdon")]
	public DateTime? CreatedOn => ((Entity)this).GetAttributeValue<DateTime?>("createdon");

	[AttributeLogicalName("createdonbehalfby")]
	public EntityReference CreatedOnBehalfBy => ((Entity)this).GetAttributeValue<EntityReference>("createdonbehalfby");

	[AttributeLogicalName("culture")]
	public string Culture
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("culture");
		}
		set
		{
			OnPropertyChanging("Culture");
			((Entity)this).SetAttributeValue("culture", (object)value);
			OnPropertyChanged("Culture");
		}
	}

	[AttributeLogicalName("customizationlevel")]
	public int? CustomizationLevel => ((Entity)this).GetAttributeValue<int?>("customizationlevel");

	[AttributeLogicalName("description")]
	public string Description
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("description");
		}
		set
		{
			OnPropertyChanging("Description");
			((Entity)this).SetAttributeValue("description", (object)value);
			OnPropertyChanged("Description");
		}
	}

	[AttributeLogicalName("introducedversion")]
	public string IntroducedVersion
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("introducedversion");
		}
		set
		{
			OnPropertyChanging("IntroducedVersion");
			((Entity)this).SetAttributeValue("introducedversion", (object)value);
			OnPropertyChanged("IntroducedVersion");
		}
	}

	[AttributeLogicalName("ishidden")]
	public BooleanManagedProperty IsHidden
	{
		get
		{
			return ((Entity)this).GetAttributeValue<BooleanManagedProperty>("ishidden");
		}
		set
		{
			OnPropertyChanging("IsHidden");
			((Entity)this).SetAttributeValue("ishidden", (object)value);
			OnPropertyChanged("IsHidden");
		}
	}

	[AttributeLogicalName("ismanaged")]
	public bool? IsManaged => ((Entity)this).GetAttributeValue<bool?>("ismanaged");

	[AttributeLogicalName("isolationmode")]
	public OptionSetValue IsolationMode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("isolationmode");
		}
		set
		{
			OnPropertyChanging("IsolationMode");
			((Entity)this).SetAttributeValue("isolationmode", (object)value);
			OnPropertyChanged("IsolationMode");
		}
	}

	[AttributeLogicalName("major")]
	public int? Major => ((Entity)this).GetAttributeValue<int?>("major");

	[AttributeLogicalName("minor")]
	public int? Minor => ((Entity)this).GetAttributeValue<int?>("minor");

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
	public EntityReference OrganizationId => ((Entity)this).GetAttributeValue<EntityReference>("organizationid");

	[AttributeLogicalName("overwritetime")]
	public DateTime? OverwriteTime => ((Entity)this).GetAttributeValue<DateTime?>("overwritetime");

	[AttributeLogicalName("path")]
	public string Path
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("path");
		}
		set
		{
			OnPropertyChanging("Path");
			((Entity)this).SetAttributeValue("path", (object)value);
			OnPropertyChanged("Path");
		}
	}

	[AttributeLogicalName("pluginassemblyid")]
	public Guid? PluginAssemblyId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("pluginassemblyid");
		}
		set
		{
			OnPropertyChanging("PluginAssemblyId");
			((Entity)this).SetAttributeValue("pluginassemblyid", (object)value);
			if (value.HasValue)
			{
				((Entity)this).Id = value.Value;
			}
			else
			{
				((Entity)this).Id = Guid.Empty;
			}
			OnPropertyChanged("PluginAssemblyId");
		}
	}

	[AttributeLogicalName("pluginassemblyidunique")]
	public Guid? PluginAssemblyIdUnique => ((Entity)this).GetAttributeValue<Guid?>("pluginassemblyidunique");

	[AttributeLogicalName("publickeytoken")]
	public string PublicKeyToken
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("publickeytoken");
		}
		set
		{
			OnPropertyChanging("PublicKeyToken");
			((Entity)this).SetAttributeValue("publickeytoken", (object)value);
			OnPropertyChanged("PublicKeyToken");
		}
	}

	[AttributeLogicalName("solutionid")]
	public Guid? SolutionId => ((Entity)this).GetAttributeValue<Guid?>("solutionid");

	[AttributeLogicalName("sourcehash")]
	public string SourceHash
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("sourcehash");
		}
		set
		{
			OnPropertyChanging("SourceHash");
			((Entity)this).SetAttributeValue("sourcehash", (object)value);
			OnPropertyChanged("SourceHash");
		}
	}

	[AttributeLogicalName("sourcetype")]
	public OptionSetValue SourceType
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("sourcetype");
		}
		set
		{
			OnPropertyChanging("SourceType");
			((Entity)this).SetAttributeValue("sourcetype", (object)value);
			OnPropertyChanged("SourceType");
		}
	}

	[AttributeLogicalName("supportingsolutionid")]
	public Guid? SupportingSolutionId => ((Entity)this).GetAttributeValue<Guid?>("supportingsolutionid");

	[AttributeLogicalName("version")]
	public string Version
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("version");
		}
		set
		{
			OnPropertyChanging("Version");
			((Entity)this).SetAttributeValue("version", (object)value);
			OnPropertyChanged("Version");
		}
	}

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => ((Entity)this).GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("createdby_pluginassembly")]
	public SystemUser createdby_pluginassembly
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "createdby_pluginassembly");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("createdby_pluginassembly", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("createdby_pluginassembly", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("lk_pluginassembly_createdonbehalfby")]
	public SystemUser lk_pluginassembly_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_pluginassembly_createdonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_pluginassembly_createdonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_pluginassembly_createdonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("lk_pluginassembly_modifiedonbehalfby")]
	public SystemUser lk_pluginassembly_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_pluginassembly_modifiedonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_pluginassembly_modifiedonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_pluginassembly_modifiedonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("modifiedby_pluginassembly")]
	public SystemUser modifiedby_pluginassembly
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "modifiedby_pluginassembly");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("modifiedby_pluginassembly", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("modifiedby_pluginassembly", (EntityRole?)null);
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public PluginAssembly()
		: base("pluginassembly")
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

	public PluginAssembly(object anonymousType)
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
				((DataCollection<string, object>)(object)((Entity)this).Attributes)["pluginassemblyid"] = ((Entity)this).Id;
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
