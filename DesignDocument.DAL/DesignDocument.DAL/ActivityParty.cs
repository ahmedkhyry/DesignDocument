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
[EntityLogicalName("activityparty")]
public class ActivityParty : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum DoNotEmail
		{
			[EnumMember]
			DoNotAllow = 1,
			[EnumMember]
			Allow = 0
		}

		[DataContract]
		public enum DoNotFax
		{
			[EnumMember]
			DoNotAllow = 1,
			[EnumMember]
			Allow = 0
		}

		[DataContract]
		public enum DoNotPhone
		{
			[EnumMember]
			DoNotAllow = 1,
			[EnumMember]
			Allow = 0
		}

		[DataContract]
		public enum DoNotPostalMail
		{
			[EnumMember]
			DoNotAllow = 1,
			[EnumMember]
			Allow = 0
		}

		[DataContract]
		public enum InstanceTypeCode
		{
			[EnumMember]
			NotRecurring,
			[EnumMember]
			RecurringMaster,
			[EnumMember]
			RecurringInstance,
			[EnumMember]
			RecurringException,
			[EnumMember]
			RecurringFutureException
		}

		[DataContract]
		public enum IsPartyDeleted
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum ParticipationTypeMask
		{
			[EnumMember]
			Sender = 1,
			[EnumMember]
			ToRecipient,
			[EnumMember]
			CCRecipient,
			[EnumMember]
			BCCRecipient,
			[EnumMember]
			Requiredattendee,
			[EnumMember]
			Optionalattendee,
			[EnumMember]
			Organizer,
			[EnumMember]
			Regarding,
			[EnumMember]
			Owner,
			[EnumMember]
			Resource,
			[EnumMember]
			Customer
		}

		public static class Names
		{
			public const string DoNotEmail = "donotemail";

			public const string DoNotFax = "donotfax";

			public const string DoNotPhone = "donotphone";

			public const string DoNotPostalMail = "donotpostalmail";

			public const string InstanceTypeCode = "instancetypecode";

			public const string IsPartyDeleted = "ispartydeleted";

			public const string ParticipationTypeMask = "participationtypemask";
		}

		public static class Labels
		{
			public static class DoNotEmail
			{
				public const string DoNotAllow_1033 = "Do Not Allow";

				public const string DoNotAllow_1025 = "عدم السماح";

				public const string Allow_1033 = "Allow";

				public const string Allow_1025 = "السماح";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DoNotEmail), label, languageCode);
				}
			}

			public static class DoNotFax
			{
				public const string DoNotAllow_1033 = "Do Not Allow";

				public const string DoNotAllow_1025 = "عدم السماح";

				public const string Allow_1033 = "Allow";

				public const string Allow_1025 = "السماح";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DoNotFax), label, languageCode);
				}
			}

			public static class DoNotPhone
			{
				public const string DoNotAllow_1033 = "Do Not Allow";

				public const string DoNotAllow_1025 = "عدم السماح";

				public const string Allow_1033 = "Allow";

				public const string Allow_1025 = "السماح";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DoNotPhone), label, languageCode);
				}
			}

			public static class DoNotPostalMail
			{
				public const string DoNotAllow_1033 = "Do Not Allow";

				public const string DoNotAllow_1025 = "عدم السماح";

				public const string Allow_1033 = "Allow";

				public const string Allow_1025 = "السماح";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DoNotPostalMail), label, languageCode);
				}
			}

			public static class InstanceTypeCode
			{
				public const string NotRecurring_1033 = "Not Recurring";

				public const string NotRecurring_1025 = "\u200f\u200fغير متكرر";

				public const string RecurringMaster_1033 = "Recurring Master";

				public const string RecurringMaster_1025 = "\u200f\u200fالتكرار الرئيسي";

				public const string RecurringInstance_1033 = "Recurring Instance";

				public const string RecurringInstance_1025 = "\u200f\u200fمثيل متكرر";

				public const string RecurringException_1033 = "Recurring Exception";

				public const string RecurringException_1025 = "\u200f\u200fاستثناء التكرار";

				public const string RecurringFutureException_1033 = "Recurring Future Exception";

				public const string RecurringFutureException_1025 = "\u200f\u200fاستثناء تكرار مستقبلي";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(InstanceTypeCode), label, languageCode);
				}
			}

			public static class IsPartyDeleted
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsPartyDeleted), label, languageCode);
				}
			}

			public static class ParticipationTypeMask
			{
				public const string Sender_1033 = "Sender";

				public const string Sender_1025 = "المرسل";

				public const string ToRecipient_1033 = "To Recipient";

				public const string ToRecipient_1025 = "إلى المتلقي";

				public const string CCRecipient_1033 = "CC Recipient";

				public const string CCRecipient_1025 = "نسخة للمتلقي";

				public const string BCCRecipient_1033 = "BCC Recipient";

				public const string BCCRecipient_1025 = "نسخة مخفية للمتلقي";

				public const string Requiredattendee_1033 = "Required attendee";

				public const string Requiredattendee_1025 = "الحضور المطلوب";

				public const string Optionalattendee_1033 = "Optional attendee";

				public const string Optionalattendee_1025 = "الحضور الاختياري";

				public const string Organizer_1033 = "Organizer";

				public const string Organizer_1025 = "المنظم";

				public const string Regarding_1033 = "Regarding";

				public const string Regarding_1025 = "بخصوص";

				public const string Owner_1033 = "Owner";

				public const string Owner_1025 = "المالك";

				public const string Resource_1033 = "Resource";

				public const string Resource_1025 = "مورد";

				public const string Customer_1033 = "Customer";

				public const string Customer_1025 = "العميل";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(ParticipationTypeMask), label, languageCode);
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
			public const string ActivityId = "ActivityId";

			public const string ActivityIdName = "ActivityIdName";

			public const string ActivityPartyId = "ActivityPartyId";

			public const string AddressUsed = "AddressUsed";

			public const string AddressUsedEmailColumnNumber = "AddressUsedEmailColumnNumber";

			public const string DoNotEmail = "DoNotEmail";

			public const string DoNotFax = "DoNotFax";

			public const string DoNotPhone = "DoNotPhone";

			public const string DoNotPostalMail = "DoNotPostalMail";

			public const string Effort = "Effort";

			public const string ExchangeEntryId = "ExchangeEntryId";

			public const string InstanceTypeCode = "InstanceTypeCode";

			public const string IsPartyDeleted = "IsPartyDeleted";

			public const string OwnerId = "OwnerId";

			public const string OwningBusinessUnit = "OwningBusinessUnit";

			public const string OwningUser = "OwningUser";

			public const string ParticipationTypeMask = "ParticipationTypeMask";

			public const string PartyId = "PartyId";

			public const string PartyIdName = "PartyIdName";

			public const string PartyIdType = "PartyIdType";

			public const string ResourceSpecId = "ResourceSpecId";

			public const string ResourceSpecIdName = "ResourceSpecIdName";

			public const string ScheduledEnd = "ScheduledEnd";

			public const string ScheduledStart = "ScheduledStart";

			public const string VersionNumber = "VersionNumber";
		}

		public static class Labels
		{
			public static class ActivityId
			{
				public const string _1033 = "Activity";

				public const string _1025 = "النشاط";
			}

			public static class ActivityPartyId
			{
				public const string _1033 = "Activity Party";

				public const string _1025 = "طرف النشاط";
			}

			public static class AddressUsed
			{
				public const string _1033 = "Address ";

				public const string _1025 = "العنوان ";
			}

			public static class AddressUsedEmailColumnNumber
			{
				public const string _1033 = "Email column number of party";

				public const string _1025 = "رقم عمود البريد الإلكتروني للطرف";
			}

			public static class DoNotEmail
			{
				public const string _1033 = "Do not allow Emails";

				public const string _1025 = "عدم السماح برسائل البريد الإلكتروني";
			}

			public static class DoNotFax
			{
				public const string _1033 = "Do not allow Faxes";

				public const string _1025 = "عدم السماح بالفاكسات";
			}

			public static class DoNotPhone
			{
				public const string _1033 = "Do not allow Phone Calls";

				public const string _1025 = "عدم السماح بالمكالمات الهاتفية";
			}

			public static class DoNotPostalMail
			{
				public const string _1033 = "Do not allow Postal Mails";

				public const string _1025 = "عدم السماح برسائل البريد";
			}

			public static class Effort
			{
				public const string _1033 = "Effort";

				public const string _1025 = "المجهود";
			}

			public static class ExchangeEntryId
			{
				public const string _1033 = "Exchange Entry";

				public const string _1025 = "إدخال التبديل";
			}

			public static class InstanceTypeCode
			{
				public const string _1033 = "Appointment Type";

				public const string _1025 = "نوع المواعيد";
			}

			public static class IsPartyDeleted
			{
				public const string _1033 = "Is Party Deleted";

				public const string _1025 = "هل تم حذف الطرف";
			}

			public static class OwnerId
			{
				public const string _1033 = "Owner";

				public const string _1025 = "المالك";
			}

			public static class ParticipationTypeMask
			{
				public const string _1033 = "Participation Type";

				public const string _1025 = "نوع المشاركة";
			}

			public static class PartyId
			{
				public const string _1033 = "Party";

				public const string _1025 = "المجموعة";
			}

			public static class ResourceSpecId
			{
				public const string _1033 = "Resource Specification";

				public const string _1025 = "مواصفات المورد";
			}

			public static class ScheduledEnd
			{
				public const string _1033 = "Scheduled End";

				public const string _1025 = "الانتهاء المجدول";
			}

			public static class ScheduledStart
			{
				public const string _1033 = "Scheduled Start";

				public const string _1025 = "البدء المجدول";
			}
		}

		public const string ActivityId = "activityid";

		public const string ActivityIdName = "activityidName";

		public const string ActivityPartyId = "activitypartyid";

		public const string AddressUsed = "addressused";

		public const string AddressUsedEmailColumnNumber = "addressusedemailcolumnnumber";

		public const string DoNotEmail = "donotemail";

		public const string DoNotFax = "donotfax";

		public const string DoNotPhone = "donotphone";

		public const string DoNotPostalMail = "donotpostalmail";

		public const string Effort = "effort";

		public const string ExchangeEntryId = "exchangeentryid";

		public const string InstanceTypeCode = "instancetypecode";

		public const string IsPartyDeleted = "ispartydeleted";

		public const string OwnerId = "ownerid";

		public const string OwningBusinessUnit = "owningbusinessunit";

		public const string OwningUser = "owninguser";

		public const string ParticipationTypeMask = "participationtypemask";

		public const string PartyId = "partyid";

		public const string PartyIdName = "partyidName";

		public const string PartyIdType = "partyidType";

		public const string ResourceSpecId = "resourcespecid";

		public const string ResourceSpecIdName = "resourcespecidName";

		public const string ScheduledEnd = "scheduledend";

		public const string ScheduledStart = "scheduledstart";

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
				public const string system_user_activity_parties = "partyid";
			}

			public const string system_user_activity_parties = "system_user_activity_parties";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Activity Party";

	public const string SchemaName = "ActivityParty";

	public const string EntityLogicalName = "activityparty";

	public const int EntityTypeCode = 135;

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

	[AttributeLogicalName("activitypartyid")]
	public override Guid Id
	{
		get
		{
			return ((Entity)this).Id;
		}
		set
		{
			ActivityPartyId = value;
		}
	}

	[AttributeLogicalName("activityid")]
	public EntityReference ActivityId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("activityid");
		}
		set
		{
			OnPropertyChanging("ActivityId");
			((Entity)this).SetAttributeValue("activityid", (object)value);
			OnPropertyChanged("ActivityId");
		}
	}

	[AttributeLogicalName("activitypartyid")]
	public Guid? ActivityPartyId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("activitypartyid");
		}
		set
		{
			OnPropertyChanging("ActivityPartyId");
			((Entity)this).SetAttributeValue("activitypartyid", (object)value);
			if (value.HasValue)
			{
				((Entity)this).Id = value.Value;
			}
			else
			{
				((Entity)this).Id = Guid.Empty;
			}
			OnPropertyChanged("ActivityPartyId");
		}
	}

	[AttributeLogicalName("addressused")]
	public string AddressUsed
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("addressused");
		}
		set
		{
			OnPropertyChanging("AddressUsed");
			((Entity)this).SetAttributeValue("addressused", (object)value);
			OnPropertyChanged("AddressUsed");
		}
	}

	[AttributeLogicalName("addressusedemailcolumnnumber")]
	public int? AddressUsedEmailColumnNumber => ((Entity)this).GetAttributeValue<int?>("addressusedemailcolumnnumber");

	[AttributeLogicalName("donotemail")]
	public bool? DoNotEmail => ((Entity)this).GetAttributeValue<bool?>("donotemail");

	[AttributeLogicalName("donotfax")]
	public bool? DoNotFax => ((Entity)this).GetAttributeValue<bool?>("donotfax");

	[AttributeLogicalName("donotphone")]
	public bool? DoNotPhone => ((Entity)this).GetAttributeValue<bool?>("donotphone");

	[AttributeLogicalName("donotpostalmail")]
	public bool? DoNotPostalMail => ((Entity)this).GetAttributeValue<bool?>("donotpostalmail");

	[AttributeLogicalName("effort")]
	public double? Effort
	{
		get
		{
			return ((Entity)this).GetAttributeValue<double?>("effort");
		}
		set
		{
			OnPropertyChanging("Effort");
			((Entity)this).SetAttributeValue("effort", (object)value);
			OnPropertyChanged("Effort");
		}
	}

	[AttributeLogicalName("exchangeentryid")]
	public string ExchangeEntryId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("exchangeentryid");
		}
		set
		{
			OnPropertyChanging("ExchangeEntryId");
			((Entity)this).SetAttributeValue("exchangeentryid", (object)value);
			OnPropertyChanged("ExchangeEntryId");
		}
	}

	[AttributeLogicalName("instancetypecode")]
	public OptionSetValue InstanceTypeCode => ((Entity)this).GetAttributeValue<OptionSetValue>("instancetypecode");

	[AttributeLogicalName("ispartydeleted")]
	public bool? IsPartyDeleted => ((Entity)this).GetAttributeValue<bool?>("ispartydeleted");

	[AttributeLogicalName("ownerid")]
	public EntityReference OwnerId => ((Entity)this).GetAttributeValue<EntityReference>("ownerid");

	[AttributeLogicalName("owningbusinessunit")]
	public Guid? OwningBusinessUnit => ((Entity)this).GetAttributeValue<Guid?>("owningbusinessunit");

	[AttributeLogicalName("owninguser")]
	public Guid? OwningUser => ((Entity)this).GetAttributeValue<Guid?>("owninguser");

	[AttributeLogicalName("participationtypemask")]
	public OptionSetValue ParticipationTypeMask
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("participationtypemask");
		}
		set
		{
			OnPropertyChanging("ParticipationTypeMask");
			((Entity)this).SetAttributeValue("participationtypemask", (object)value);
			OnPropertyChanged("ParticipationTypeMask");
		}
	}

	[AttributeLogicalName("partyid")]
	public EntityReference PartyId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("partyid");
		}
		set
		{
			OnPropertyChanging("PartyId");
			((Entity)this).SetAttributeValue("partyid", (object)value);
			OnPropertyChanged("PartyId");
		}
	}

	[AttributeLogicalName("resourcespecid")]
	public EntityReference ResourceSpecId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("resourcespecid");
		}
		set
		{
			OnPropertyChanging("ResourceSpecId");
			((Entity)this).SetAttributeValue("resourcespecid", (object)value);
			OnPropertyChanged("ResourceSpecId");
		}
	}

	[AttributeLogicalName("scheduledend")]
	public DateTime? ScheduledEnd => ((Entity)this).GetAttributeValue<DateTime?>("scheduledend");

	[AttributeLogicalName("scheduledstart")]
	public DateTime? ScheduledStart => ((Entity)this).GetAttributeValue<DateTime?>("scheduledstart");

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => ((Entity)this).GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("partyid")]
	[RelationshipSchemaName("system_user_activity_parties")]
	public SystemUser system_user_activity_parties
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "system_user_activity_parties");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("system_user_activity_parties", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("system_user_activity_parties", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("system_user_activity_parties");
			((Entity)this).SetRelatedEntity<SystemUser>("system_user_activity_parties", (EntityRole?)null, value);
			OnPropertyChanged("system_user_activity_parties");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public ActivityParty()
		: base("activityparty")
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

	public SystemUser Load_system_user_activity_parties(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "systemuser", ((Entity)this).LogicalName, "systemuserid", "partyid", "activitypartyid", "activitypartyid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return system_user_activity_parties = val.ToEntity<SystemUser>();
		}
		return system_user_activity_parties = null;
	}

	public ActivityParty(object anonymousType)
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
				((DataCollection<string, object>)(object)((Entity)this).Attributes)["activitypartyid"] = ((Entity)this).Id;
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
