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
[EntityLogicalName("roleprivileges")]
public class RolePrivileges : Entity, INotifyPropertyChanging, INotifyPropertyChanged
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

			public const string IsManaged = "IsManaged";

			public const string OverwriteTime = "OverwriteTime";

			public const string PrivilegeDepthMask = "PrivilegeDepthMask";

			public const string PrivilegeId = "PrivilegeId";

			public const string RoleId = "RoleId";

			public const string RolePrivilegeId = "RolePrivilegeId";

			public const string RolePrivilegeIdUnique = "RolePrivilegeIdUnique";

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

		public const string IsManaged = "ismanaged";

		public const string OverwriteTime = "overwritetime";

		public const string PrivilegeDepthMask = "privilegedepthmask";

		public const string PrivilegeId = "privilegeid";

		public const string RoleId = "roleid";

		public const string RolePrivilegeId = "roleprivilegeid";

		public const string RolePrivilegeIdUnique = "roleprivilegeidunique";

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
			}
		}

		public static class NToN
		{
			public const string roleprivileges_association = "roleprivileges_association";
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = null;

	public const string SchemaName = "RolePrivileges";

	public const string EntityLogicalName = "roleprivileges";

	public const int EntityTypeCode = 12;

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

	[AttributeLogicalName("roleprivilegeid")]
	public override Guid Id
	{
		get
		{
			return Id;
		}
		set
		{
			RolePrivilegeId = value;
		}
	}

	[AttributeLogicalName("componentstate")]
	public OptionSetValue ComponentState => GetAttributeValue<OptionSetValue>("componentstate");

	[AttributeLogicalName("ismanaged")]
	public bool? IsManaged => GetAttributeValue<bool?>("ismanaged");

	[AttributeLogicalName("overwritetime")]
	public DateTime? OverwriteTime => GetAttributeValue<DateTime?>("overwritetime");

	[AttributeLogicalName("privilegedepthmask")]
	public int? PrivilegeDepthMask
	{
		get
		{
			return GetAttributeValue<int?>("privilegedepthmask");
		}
		set
		{
			OnPropertyChanging("PrivilegeDepthMask");
			SetAttributeValue("privilegedepthmask", (object)value);
			OnPropertyChanged("PrivilegeDepthMask");
		}
	}

	[AttributeLogicalName("privilegeid")]
	public Guid? PrivilegeId => GetAttributeValue<Guid?>("privilegeid");

	[AttributeLogicalName("roleid")]
	public Guid? RoleId => GetAttributeValue<Guid?>("roleid");

	[AttributeLogicalName("roleprivilegeid")]
	public Guid? RolePrivilegeId
	{
		get
		{
			return GetAttributeValue<Guid?>("roleprivilegeid");
		}
		set
		{
			OnPropertyChanging("RolePrivilegeId");
			SetAttributeValue("roleprivilegeid", (object)value);
			if (value.HasValue)
			{
				Id = value.Value;
			}
			else
			{
				Id = Guid.Empty;
			}
			OnPropertyChanged("RolePrivilegeId");
		}
	}

	[AttributeLogicalName("roleprivilegeidunique")]
	public Guid? RolePrivilegeIdUnique => GetAttributeValue<Guid?>("roleprivilegeidunique");

	[AttributeLogicalName("solutionid")]
	public Guid? SolutionId => GetAttributeValue<Guid?>("solutionid");

	[AttributeLogicalName("supportingsolutionid")]
	public Guid? SupportingSolutionId => GetAttributeValue<Guid?>("supportingsolutionid");

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => GetAttributeValue<long?>("versionnumber");

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
				IEnumerable<Privilege> relatedEntities = GetRelatedEntities<Privilege>("roleprivileges_association", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Privilege element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<Privilege>("roleprivileges_association", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("roleprivileges_association");
			SetRelatedEntities<Privilege>("roleprivileges_association", (EntityRole?)null, value);
			OnPropertyChanged("roleprivileges_association");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public RolePrivileges()
		: base("roleprivileges")
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

	public List<Privilege> Load_roleprivileges_association(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Privilege> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "privilege", "roleprivileges", "privilegeid", "privilegeid", "roleprivilegeid", "roleid", recordCountLimit, attributes)
			select entity.ToEntity<Privilege>();
		roleprivileges_association = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public RolePrivileges(object anonymousType)
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
				((DataCollection<string, object>)(object)Attributes)["roleprivilegeid"] = Id;
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
