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
[EntityLogicalName("privilege")]
public class Privilege : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum CanBeBasic
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum CanBeDeep
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum CanBeEntityReference
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum CanBeGlobal
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum CanBeLocal
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum CanBeParentEntityReference
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsDisabledWhenIntegrated
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		public static class Names
		{
			public const string CanBeBasic = "canbebasic";

			public const string CanBeDeep = "canbedeep";

			public const string CanBeEntityReference = "canbeentityreference";

			public const string CanBeGlobal = "canbeglobal";

			public const string CanBeLocal = "canbelocal";

			public const string CanBeParentEntityReference = "canbeparententityreference";

			public const string IsDisabledWhenIntegrated = "isdisabledwhenintegrated";
		}

		public static class Labels
		{
			public static class CanBeBasic
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanBeBasic), label, languageCode);
				}
			}

			public static class CanBeDeep
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanBeDeep), label, languageCode);
				}
			}

			public static class CanBeEntityReference
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "\u200f\u200fنعم";

				public const string No_1033 = "No";

				public const string No_1025 = "\u200f\u200fلا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanBeEntityReference), label, languageCode);
				}
			}

			public static class CanBeGlobal
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanBeGlobal), label, languageCode);
				}
			}

			public static class CanBeLocal
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanBeLocal), label, languageCode);
				}
			}

			public static class CanBeParentEntityReference
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "\u200f\u200fنعم";

				public const string No_1033 = "No";

				public const string No_1025 = "\u200f\u200fلا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CanBeParentEntityReference), label, languageCode);
				}
			}

			public static class IsDisabledWhenIntegrated
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsDisabledWhenIntegrated), label, languageCode);
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
			public const string AccessRight = "AccessRight";

			public const string CanBeBasic = "CanBeBasic";

			public const string CanBeDeep = "CanBeDeep";

			public const string CanBeEntityReference = "CanBeEntityReference";

			public const string CanBeGlobal = "CanBeGlobal";

			public const string CanBeLocal = "CanBeLocal";

			public const string CanBeParentEntityReference = "CanBeParentEntityReference";

			public const string IsDisabledWhenIntegrated = "IsDisabledWhenIntegrated";

			public const string Name = "Name";

			public const string PrivilegeId = "PrivilegeId";

			public const string VersionNumber = "VersionNumber";
		}

		public static class Labels
		{
		}

		public const string AccessRight = "accessright";

		public const string CanBeBasic = "canbebasic";

		public const string CanBeDeep = "canbedeep";

		public const string CanBeEntityReference = "canbeentityreference";

		public const string CanBeGlobal = "canbeglobal";

		public const string CanBeLocal = "canbelocal";

		public const string CanBeParentEntityReference = "canbeparententityreference";

		public const string IsDisabledWhenIntegrated = "isdisabledwhenintegrated";

		public const string Name = "name";

		public const string PrivilegeId = "privilegeid";

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

	public const string DisplayName = "Privilege";

	public const string SchemaName = "Privilege";

	public const string EntityLogicalName = "privilege";

	public const int EntityTypeCode = 1023;

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

	[AttributeLogicalName("privilegeid")]
	public override Guid Id
	{
		get
		{
			return Id;
		}
		set
		{
			PrivilegeId = value;
		}
	}

	[AttributeLogicalName("accessright")]
	public int? AccessRight
	{
		get
		{
			return GetAttributeValue<int?>("accessright");
		}
		set
		{
			OnPropertyChanging("AccessRight");
			SetAttributeValue("accessright", (object)value);
			OnPropertyChanged("AccessRight");
		}
	}

	[AttributeLogicalName("canbebasic")]
	public bool? CanBeBasic
	{
		get
		{
			return GetAttributeValue<bool?>("canbebasic");
		}
		set
		{
			OnPropertyChanging("CanBeBasic");
			SetAttributeValue("canbebasic", (object)value);
			OnPropertyChanged("CanBeBasic");
		}
	}

	[AttributeLogicalName("canbedeep")]
	public bool? CanBeDeep
	{
		get
		{
			return GetAttributeValue<bool?>("canbedeep");
		}
		set
		{
			OnPropertyChanging("CanBeDeep");
			SetAttributeValue("canbedeep", (object)value);
			OnPropertyChanged("CanBeDeep");
		}
	}

	[AttributeLogicalName("canbeentityreference")]
	public bool? CanBeEntityReference
	{
		get
		{
			return GetAttributeValue<bool?>("canbeentityreference");
		}
		set
		{
			OnPropertyChanging("CanBeEntityReference");
			SetAttributeValue("canbeentityreference", (object)value);
			OnPropertyChanged("CanBeEntityReference");
		}
	}

	[AttributeLogicalName("canbeglobal")]
	public bool? CanBeGlobal
	{
		get
		{
			return GetAttributeValue<bool?>("canbeglobal");
		}
		set
		{
			OnPropertyChanging("CanBeGlobal");
			SetAttributeValue("canbeglobal", (object)value);
			OnPropertyChanged("CanBeGlobal");
		}
	}

	[AttributeLogicalName("canbelocal")]
	public bool? CanBeLocal
	{
		get
		{
			return GetAttributeValue<bool?>("canbelocal");
		}
		set
		{
			OnPropertyChanging("CanBeLocal");
			SetAttributeValue("canbelocal", (object)value);
			OnPropertyChanged("CanBeLocal");
		}
	}

	[AttributeLogicalName("canbeparententityreference")]
	public bool? CanBeParentEntityReference
	{
		get
		{
			return GetAttributeValue<bool?>("canbeparententityreference");
		}
		set
		{
			OnPropertyChanging("CanBeParentEntityReference");
			SetAttributeValue("canbeparententityreference", (object)value);
			OnPropertyChanged("CanBeParentEntityReference");
		}
	}

	[AttributeLogicalName("isdisabledwhenintegrated")]
	public bool? IsDisabledWhenIntegrated => GetAttributeValue<bool?>("isdisabledwhenintegrated");

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

	[AttributeLogicalName("privilegeid")]
	public Guid? PrivilegeId
	{
		get
		{
			return GetAttributeValue<Guid?>("privilegeid");
		}
		set
		{
			OnPropertyChanging("PrivilegeId");
			SetAttributeValue("privilegeid", (object)value);
			if (value.HasValue)
			{
				Id = value.Value;
			}
			else
			{
				Id = Guid.Empty;
			}
			OnPropertyChanged("PrivilegeId");
		}
	}

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => GetAttributeValue<long?>("versionnumber");

	[RelationshipSchemaName("roleprivileges_association")]
	public IEnumerable<Role> roleprivileges_association
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "roleprivileges_association");
				}
				IEnumerable<Role> relatedEntities = GetRelatedEntities<Role>("roleprivileges_association", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Role element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<Role>("roleprivileges_association", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("roleprivileges_association");
			SetRelatedEntities<Role>("roleprivileges_association", (EntityRole?)null, value);
			OnPropertyChanged("roleprivileges_association");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public Privilege()
		: base("privilege")
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

	public List<Role> Load_roleprivileges_association(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Role> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "role", "roleprivileges", "roleid", "roleid", "privilegeid", "privilegeid", recordCountLimit, attributes)
			select entity.ToEntity<Role>();
		roleprivileges_association = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public Privilege(object anonymousType)
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
				((DataCollection<string, object>)(object)Attributes)["privilegeid"] = Id;
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
