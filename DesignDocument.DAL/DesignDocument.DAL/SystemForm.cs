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
[EntityLogicalName("systemform")]
public class SystemForm : Entity, INotifyPropertyChanging, INotifyPropertyChanged
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
		public enum FormActivationState
		{
			[EnumMember]
			Inactive,
			[EnumMember]
			Active
		}

		[DataContract]
		public enum FormPresentation
		{
			[EnumMember]
			ClassicForm,
			[EnumMember]
			AirForm
		}

		[DataContract]
		public enum IsAIRMerged
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsDefault
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsDesktopEnabled
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
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
		public enum IsTabletEnabled
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum ObjectTypeCode
		{

		}

		[DataContract]
		public enum Type
		{
			[EnumMember]
			Dashboard = 0,
			[EnumMember]
			AppointmentBook = 1,
			[EnumMember]
			Main = 2,
			[EnumMember]
			MiniCampaignBO = 3,
			[EnumMember]
			Preview = 4,
			[EnumMember]
			MobileExpress = 5,
			[EnumMember]
			QuickViewForm = 6,
			[EnumMember]
			QuickCreate = 7,
			[EnumMember]
			Dialog = 8,
			[EnumMember]
			TaskFlowForm = 9,
			[EnumMember]
			InteractionCentricDashboard = 10,
			[EnumMember]
			Card = 11,
			[EnumMember]
			MainInteractiveexperience = 12,
			[EnumMember]
			Other = 100,
			[EnumMember]
			MainBackup = 101,
			[EnumMember]
			AppointmentBookBackup = 102
		}

		public static class Names
		{
			public const string ComponentState = "componentstate";

			public const string FormActivationState = "formactivationstate";

			public const string FormPresentation = "formpresentation";

			public const string IsAIRMerged = "isairmerged";

			public const string IsDefault = "isdefault";

			public const string IsDesktopEnabled = "isdesktopenabled";

			public const string IsManaged = "ismanaged";

			public const string IsTabletEnabled = "istabletenabled";

			public const string ObjectTypeCode = "objecttypecode";

			public const string Type = "type";
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

			public static class FormActivationState
			{
				public const string Inactive_1033 = "Inactive";

				public const string Inactive_1025 = "غير نشط";

				public const string Active_1033 = "Active";

				public const string Active_1025 = "نشط";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(FormActivationState), label, languageCode);
				}
			}

			public static class FormPresentation
			{
				public const string ClassicForm_1033 = "ClassicForm";

				public const string ClassicForm_1025 = "ClassicForm";

				public const string AirForm_1033 = "AirForm";

				public const string AirForm_1025 = "AirForm";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(FormPresentation), label, languageCode);
				}
			}

			public static class IsAIRMerged
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsAIRMerged), label, languageCode);
				}
			}

			public static class IsDefault
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsDefault), label, languageCode);
				}
			}

			public static class IsDesktopEnabled
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "\u200f\u200fنعم";

				public const string No_1033 = "No";

				public const string No_1025 = "\u200f\u200fلا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsDesktopEnabled), label, languageCode);
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

			public static class IsTabletEnabled
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "\u200f\u200fنعم";

				public const string No_1033 = "No";

				public const string No_1025 = "\u200f\u200fلا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsTabletEnabled), label, languageCode);
				}
			}

			public static class ObjectTypeCode
			{
				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(ObjectTypeCode), label, languageCode);
				}
			}

			public static class Type
			{
				public const string Dashboard_1033 = "Dashboard";

				public const string Dashboard_1025 = "لوحة المعلومات";

				public const string AppointmentBook_1033 = "AppointmentBook";

				public const string AppointmentBook_1025 = "\u200f\u200fدفتر المواعيد";

				public const string Main_1033 = "Main";

				public const string Main_1025 = "رئيسي";

				public const string MiniCampaignBO_1033 = "MiniCampaignBO";

				public const string MiniCampaignBO_1025 = "\u200f\u200fMiniCampaignBO";

				public const string Preview_1033 = "Preview";

				public const string Preview_1025 = "\u200f\u200fمعاينة";

				public const string MobileExpress_1033 = "Mobile - Express";

				public const string MobileExpress_1025 = "Mobile - Express";

				public const string QuickViewForm_1033 = "Quick View Form";

				public const string QuickViewForm_1025 = "نموذج العرض السريع";

				public const string QuickCreate_1033 = "Quick Create";

				public const string QuickCreate_1025 = "إنشاء سريع";

				public const string Dialog_1033 = "Dialog";

				public const string Dialog_1025 = "حوار";

				public const string TaskFlowForm_1033 = "Task Flow Form";

				public const string TaskFlowForm_1025 = "النموذج المستند إلى المهمة";

				public const string InteractionCentricDashboard_1033 = "InteractionCentricDashboard";

				public const string InteractionCentricDashboard_1025 = "InteractionCentricDashboard";

				public const string Card_1033 = "Card";

				public const string Card_1025 = "البطاقة";

				public const string MainInteractiveexperience_1033 = "Main - Interactive experience";

				public const string MainInteractiveexperience_1025 = "رئيسي - تجربة تفاعلية";

				public const string Other_1033 = "Other";

				public const string Other_1025 = "\u200f\u200fأخرى";

				public const string MainBackup_1033 = "MainBackup";

				public const string MainBackup_1025 = "MainBackup";

				public const string AppointmentBookBackup_1033 = "AppointmentBookBackup";

				public const string AppointmentBookBackup_1025 = "AppointmentBookBackup";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Type), label, languageCode);
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
			public const string AncestorFormId = "AncestorFormId";

			public const string AncestorFormIdName = "AncestorFormIdName";

			public const string CanBeDeleted = "CanBeDeleted";

			public const string ComponentState = "ComponentState";

			public const string Description = "Description";

			public const string FormActivationState = "FormActivationState";

			public const string FormId = "FormId";

			public const string FormIdUnique = "FormIdUnique";

			public const string FormPresentation = "FormPresentation";

			public const string FormXml = "FormXml";

			public const string FormXmlManaged = "FormXmlManaged";

			public const string IntroducedVersion = "IntroducedVersion";

			public const string IsAIRMerged = "IsAIRMerged";

			public const string IsCustomizable = "IsCustomizable";

			public const string IsDefault = "IsDefault";

			public const string IsDesktopEnabled = "IsDesktopEnabled";

			public const string IsManaged = "IsManaged";

			public const string IsTabletEnabled = "IsTabletEnabled";

			public const string Name = "Name";

			public const string ObjectTypeCode = "ObjectTypeCode";

			public const string OrganizationId = "OrganizationId";

			public const string OrganizationIdName = "OrganizationIdName";

			public const string OverwriteTime = "OverwriteTime";

			public const string PublishedOn = "PublishedOn";

			public const string SolutionId = "SolutionId";

			public const string SupportingSolutionId = "SupportingSolutionId";

			public const string Type = "Type";

			public const string UniqueName = "UniqueName";

			public const string Version = "Version";

			public const string VersionNumber = "VersionNumber";
		}

		public static class Labels
		{
			public static class AncestorFormId
			{
				public const string _1033 = "Parent Form";

				public const string _1025 = "النموذج الأصل";
			}

			public static class CanBeDeleted
			{
				public const string _1033 = "Can Be Deleted";

				public const string _1025 = "\u200f\u200fيمكن حذفه";
			}

			public static class ComponentState
			{
				public const string _1033 = "Component State";

				public const string _1025 = "حالة المكون";
			}

			public static class Description
			{
				public const string _1033 = "Description";

				public const string _1025 = "الوصف";
			}

			public static class FormActivationState
			{
				public const string _1033 = "Form State";

				public const string _1025 = "حالة النموذج";
			}

			public static class FormPresentation
			{
				public const string _1033 = "AIR Refreshed";

				public const string _1025 = "تم تحديث AIR";
			}

			public static class IntroducedVersion
			{
				public const string _1033 = "Introduced Version";

				public const string _1025 = "إصدار م\u064fقد\u0651\u064eم";
			}

			public static class IsAIRMerged
			{
				public const string _1033 = "Refreshed";

				public const string _1025 = "تم التحديث";
			}

			public static class IsCustomizable
			{
				public const string _1033 = "Customizable";

				public const string _1025 = "قابل للتخصيص";
			}

			public static class IsDefault
			{
				public const string _1033 = "Default Form";

				public const string _1025 = "نموذج افتراضي";
			}

			public static class IsDesktopEnabled
			{
				public const string _1033 = "Is Desktop Enabled";

				public const string _1025 = "تمكين سطح المكتب";
			}

			public static class IsManaged
			{
				public const string _1033 = "State";

				public const string _1025 = "المحافظة";
			}

			public static class IsTabletEnabled
			{
				public const string _1033 = "Is Tablet Enabled";

				public const string _1025 = "هل الكمبيوتر اللوحي ممك\u0651ن";
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

			public static class Type
			{
				public const string _1033 = "Form Type";

				public const string _1025 = "نوع النموذج";
			}

			public static class UniqueName
			{
				public const string _1033 = "Unique Name";

				public const string _1025 = "الاسم الفريد";
			}
		}

		public const string AncestorFormId = "ancestorformid";

		public const string AncestorFormIdName = "ancestorformidName";

		public const string CanBeDeleted = "canbedeleted";

		public const string ComponentState = "componentstate";

		public const string Description = "description";

		public const string FormActivationState = "formactivationstate";

		public const string FormId = "formid";

		public const string FormIdUnique = "formidunique";

		public const string FormPresentation = "formpresentation";

		public const string FormXml = "formxml";

		public const string FormXmlManaged = "formxmlmanaged";

		public const string IntroducedVersion = "introducedversion";

		public const string IsAIRMerged = "isairmerged";

		public const string IsCustomizable = "iscustomizable";

		public const string IsDefault = "isdefault";

		public const string IsDesktopEnabled = "isdesktopenabled";

		public const string IsManaged = "ismanaged";

		public const string IsTabletEnabled = "istabletenabled";

		public const string Name = "name";

		public const string ObjectTypeCode = "objecttypecode";

		public const string OrganizationId = "organizationid";

		public const string OrganizationIdName = "organizationidName";

		public const string OverwriteTime = "overwritetime";

		public const string PublishedOn = "publishedon";

		public const string SolutionId = "solutionid";

		public const string SupportingSolutionId = "supportingsolutionid";

		public const string Type = "type";

		public const string UniqueName = "uniquename";

		public const string Version = "version";

		public const string VersionNumber = "versionnumber";
	}

	public static class Relations
	{
		public static class OneToN
		{
			public const string form_ancestor_form = "form_ancestor_form";
		}

		public static class NToOne
		{
			public static class Lookups
			{
				public const string form_ancestor_form = "ancestorformid";
			}

			public const string form_ancestor_form = "form_ancestor_form";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "System Form";

	public const string SchemaName = "SystemForm";

	public const string EntityLogicalName = "systemform";

	public const int EntityTypeCode = 1030;

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

	[AttributeLogicalName("formid")]
	public override Guid Id
	{
		get
		{
			return Id;
		}
		set
		{
			FormId = value;
		}
	}

	[AttributeLogicalName("ancestorformid")]
	public EntityReference AncestorFormId
	{
		get
		{
			return GetAttributeValue<EntityReference>("ancestorformid");
		}
		set
		{
			OnPropertyChanging("AncestorFormId");
			SetAttributeValue("ancestorformid", (object)value);
			OnPropertyChanged("AncestorFormId");
		}
	}

	[AttributeLogicalName("canbedeleted")]
	public BooleanManagedProperty CanBeDeleted
	{
		get
		{
			return GetAttributeValue<BooleanManagedProperty>("canbedeleted");
		}
		set
		{
			OnPropertyChanging("CanBeDeleted");
			SetAttributeValue("canbedeleted", (object)value);
			OnPropertyChanged("CanBeDeleted");
		}
	}

	[AttributeLogicalName("componentstate")]
	public OptionSetValue ComponentState => GetAttributeValue<OptionSetValue>("componentstate");

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

	[AttributeLogicalName("formactivationstate")]
	public OptionSetValue FormActivationState
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("formactivationstate");
		}
		set
		{
			OnPropertyChanging("FormActivationState");
			SetAttributeValue("formactivationstate", (object)value);
			OnPropertyChanged("FormActivationState");
		}
	}

	[AttributeLogicalName("formid")]
	public Guid? FormId
	{
		get
		{
			return GetAttributeValue<Guid?>("formid");
		}
		set
		{
			OnPropertyChanging("FormId");
			SetAttributeValue("formid", (object)value);
			if (value.HasValue)
			{
				Id = value.Value;
			}
			else
			{
				Id = Guid.Empty;
			}
			OnPropertyChanged("FormId");
		}
	}

	[AttributeLogicalName("formidunique")]
	public Guid? FormIdUnique => GetAttributeValue<Guid?>("formidunique");

	[AttributeLogicalName("formpresentation")]
	public OptionSetValue FormPresentation
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("formpresentation");
		}
		set
		{
			OnPropertyChanging("FormPresentation");
			SetAttributeValue("formpresentation", (object)value);
			OnPropertyChanged("FormPresentation");
		}
	}

	[AttributeLogicalName("formxml")]
	public string FormXml
	{
		get
		{
			return GetAttributeValue<string>("formxml");
		}
		set
		{
			OnPropertyChanging("FormXml");
			SetAttributeValue("formxml", (object)value);
			OnPropertyChanged("FormXml");
		}
	}

	[AttributeLogicalName("formxmlmanaged")]
	public string FormXmlManaged => GetAttributeValue<string>("formxmlmanaged");

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

	[AttributeLogicalName("isairmerged")]
	public bool? IsAIRMerged
	{
		get
		{
			return GetAttributeValue<bool?>("isairmerged");
		}
		set
		{
			OnPropertyChanging("IsAIRMerged");
			SetAttributeValue("isairmerged", (object)value);
			OnPropertyChanged("IsAIRMerged");
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

	[AttributeLogicalName("isdefault")]
	public bool? IsDefault
	{
		get
		{
			return GetAttributeValue<bool?>("isdefault");
		}
		set
		{
			OnPropertyChanging("IsDefault");
			SetAttributeValue("isdefault", (object)value);
			OnPropertyChanged("IsDefault");
		}
	}

	[AttributeLogicalName("isdesktopenabled")]
	public bool? IsDesktopEnabled
	{
		get
		{
			return GetAttributeValue<bool?>("isdesktopenabled");
		}
		set
		{
			OnPropertyChanging("IsDesktopEnabled");
			SetAttributeValue("isdesktopenabled", (object)value);
			OnPropertyChanged("IsDesktopEnabled");
		}
	}

	[AttributeLogicalName("ismanaged")]
	public bool? IsManaged => GetAttributeValue<bool?>("ismanaged");

	[AttributeLogicalName("istabletenabled")]
	public bool? IsTabletEnabled
	{
		get
		{
			return GetAttributeValue<bool?>("istabletenabled");
		}
		set
		{
			OnPropertyChanging("IsTabletEnabled");
			SetAttributeValue("istabletenabled", (object)value);
			OnPropertyChanged("IsTabletEnabled");
		}
	}

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

	[AttributeLogicalName("objecttypecode")]
	public string ObjectTypeCode
	{
		get
		{
			return GetAttributeValue<string>("objecttypecode");
		}
		set
		{
			OnPropertyChanging("ObjectTypeCode");
			SetAttributeValue("objecttypecode", (object)value);
			OnPropertyChanged("ObjectTypeCode");
		}
	}

	[AttributeLogicalName("organizationid")]
	public EntityReference OrganizationId => GetAttributeValue<EntityReference>("organizationid");

	[AttributeLogicalName("overwritetime")]
	public DateTime? OverwriteTime => GetAttributeValue<DateTime?>("overwritetime");

	[AttributeLogicalName("publishedon")]
	public DateTime? PublishedOn => GetAttributeValue<DateTime?>("publishedon");

	[AttributeLogicalName("solutionid")]
	public Guid? SolutionId => GetAttributeValue<Guid?>("solutionid");

	[AttributeLogicalName("supportingsolutionid")]
	public Guid? SupportingSolutionId => GetAttributeValue<Guid?>("supportingsolutionid");

	[AttributeLogicalName("type")]
	public OptionSetValue Type
	{
		get
		{
			return GetAttributeValue<OptionSetValue>("type");
		}
		set
		{
			OnPropertyChanging("Type");
			SetAttributeValue("type", (object)value);
			OnPropertyChanged("Type");
		}
	}

	[AttributeLogicalName("uniquename")]
	public string UniqueName
	{
		get
		{
			return GetAttributeValue<string>("uniquename");
		}
		set
		{
			OnPropertyChanging("UniqueName");
			SetAttributeValue("uniquename", (object)value);
			OnPropertyChanged("UniqueName");
		}
	}

	[AttributeLogicalName("version")]
	public int? Version
	{
		get
		{
			return GetAttributeValue<int?>("version");
		}
		set
		{
			OnPropertyChanging("Version");
			SetAttributeValue("version", (object)value);
			OnPropertyChanged("Version");
		}
	}

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => GetAttributeValue<long?>("versionnumber");

	//[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<SystemForm> Referenced_form_ancestor_form
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "form_ancestor_form");
				}
				IEnumerable<SystemForm> relatedEntities = GetRelatedEntities<SystemForm>("form_ancestor_form", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(SystemForm element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return GetRelatedEntities<SystemForm>("form_ancestor_form", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_form_ancestor_form");
			SetRelatedEntities<SystemForm>("form_ancestor_form", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_form_ancestor_form");
		}
	}

	[AttributeLogicalName("ancestorformid")]
	//[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public SystemForm Referencing_form_ancestor_form
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "form_ancestor_form");
				}
				SystemForm relatedEntity = GetRelatedEntity<SystemForm>("form_ancestor_form", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return GetRelatedEntity<SystemForm>("form_ancestor_form", (EntityRole?)(EntityRole)0);
			}
		}
		set
		{
			OnPropertyChanging("Referencing_form_ancestor_form");
			SetRelatedEntity<SystemForm>("form_ancestor_form", (EntityRole?)(EntityRole)0, value);
			OnPropertyChanged("Referencing_form_ancestor_form");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public SystemForm()
		: base("systemform")
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

	public List<SystemForm> Load_Referenced_form_ancestor_form(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SystemForm> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "systemform", LogicalName, "ancestorformid", "formid", "formid", "formid", recordCountLimit, attributes)
			select entity.ToEntity<SystemForm>();
		Referenced_form_ancestor_form = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public SystemForm Load_Referencing_form_ancestor_form(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "systemform", LogicalName, "formid", "ancestorformid", "formid", "formid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return Referencing_form_ancestor_form = val.ToEntity<SystemForm>();
		}
		return Referencing_form_ancestor_form = null;
	}

	public SystemForm(object anonymousType)
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
				((DataCollection<string, object>)(object)Attributes)["formid"] = Id;
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
