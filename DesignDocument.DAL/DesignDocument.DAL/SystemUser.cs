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
[EntityLogicalName("systemuser")]
public class SystemUser : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum AccessMode
		{
			[EnumMember]
			ReadWrite,
			[EnumMember]
			Administrative,
			[EnumMember]
			Read,
			[EnumMember]
			SupportUser,
			[EnumMember]
			Noninteractive,
			[EnumMember]
			DelegatedAdmin
		}

		[DataContract]
		public enum Address1_AddressTypeCode
		{
			[EnumMember]
			DefaultValue = 1
		}

		[DataContract]
		public enum Address1_ShippingMethodCode
		{
			[EnumMember]
			DefaultValue = 1
		}

		[DataContract]
		public enum Address2_AddressTypeCode
		{
			[EnumMember]
			DefaultValue = 1
		}

		[DataContract]
		public enum Address2_ShippingMethodCode
		{
			[EnumMember]
			DefaultValue = 1
		}

		[DataContract]
		public enum CALType
		{
			[EnumMember]
			Professional,
			[EnumMember]
			Administrative,
			[EnumMember]
			Basic,
			[EnumMember]
			DeviceProfessional,
			[EnumMember]
			DeviceBasic,
			[EnumMember]
			Essential,
			[EnumMember]
			DeviceEssential,
			[EnumMember]
			Enterprise,
			[EnumMember]
			DeviceEnterprise
		}

		[DataContract]
		public enum DefaultFiltersPopulated
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum DisplayInServiceViews
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum EmailRouterAccessApproval
		{
			[EnumMember]
			Empty,
			[EnumMember]
			Approved,
			[EnumMember]
			PendingApproval,
			[EnumMember]
			Rejected
		}

		[DataContract]
		public enum IncomingEmailDeliveryMethod
		{
			[EnumMember]
			None,
			[EnumMember]
			MicrosoftDynamicsCRMforOutlook,
			[EnumMember]
			ServerSideSynchronizationorEmailRouter,
			[EnumMember]
			ForwardMailbox
		}

		[DataContract]
		public enum InviteStatusCode
		{
			[EnumMember]
			InvitationNotSent,
			[EnumMember]
			Invited,
			[EnumMember]
			InvitationNearExpired,
			[EnumMember]
			InvitationExpired,
			[EnumMember]
			InvitationAccepted,
			[EnumMember]
			InvitationRejected,
			[EnumMember]
			InvitationRevoked
		}

		[DataContract]
		public enum IsActiveDirectoryUser
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsDisabled
		{
			[EnumMember]
			Disabled = 1,
			[EnumMember]
			Enabled = 0
		}

		[DataContract]
		public enum IsEmailAddressApprovedByO365Admin
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsIntegrationUser
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsLicensed
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum IsSyncWithDirectory
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum ldv_isonleave
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum ldv_PreferredCommunicationLanguage
		{
			[EnumMember]
			ArabicSaudiArabia = 1025,
			[EnumMember]
			EnglishUnitedStates = 1033
		}

		[DataContract]
		public enum ldv_userrole
		{
			[EnumMember]
			SocialResearcher = 1
		}

		[DataContract]
		public enum OutgoingEmailDeliveryMethod
		{
			[EnumMember]
			None,
			[EnumMember]
			MicrosoftDynamicsCRMforOutlook,
			[EnumMember]
			ServerSideSynchronizationorEmailRouter
		}

		[DataContract]
		public enum PreferredAddressCode
		{
			[EnumMember]
			MailingAddress = 1,
			[EnumMember]
			OtherAddress
		}

		[DataContract]
		public enum PreferredEmailCode
		{
			[EnumMember]
			DefaultValue = 1
		}

		[DataContract]
		public enum PreferredPhoneCode
		{
			[EnumMember]
			MainPhone = 1,
			[EnumMember]
			OtherPhone,
			[EnumMember]
			HomePhone,
			[EnumMember]
			MobilePhone
		}

		[DataContract]
		public enum SetupUser
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		public static class Names
		{
			public const string AccessMode = "accessmode";

			public const string Address1_AddressTypeCode = "address1_addresstypecode";

			public const string Address1_ShippingMethodCode = "address1_shippingmethodcode";

			public const string Address2_AddressTypeCode = "address2_addresstypecode";

			public const string Address2_ShippingMethodCode = "address2_shippingmethodcode";

			public const string CALType = "caltype";

			public const string DefaultFiltersPopulated = "defaultfilterspopulated";

			public const string DisplayInServiceViews = "displayinserviceviews";

			public const string EmailRouterAccessApproval = "emailrouteraccessapproval";

			public const string IncomingEmailDeliveryMethod = "incomingemaildeliverymethod";

			public const string InviteStatusCode = "invitestatuscode";

			public const string IsActiveDirectoryUser = "isactivedirectoryuser";

			public const string IsDisabled = "isdisabled";

			public const string IsEmailAddressApprovedByO365Admin = "isemailaddressapprovedbyo365admin";

			public const string IsIntegrationUser = "isintegrationuser";

			public const string IsLicensed = "islicensed";

			public const string IsSyncWithDirectory = "issyncwithdirectory";

			public const string ldv_isonleave = "ldv_isonleave";

			public const string ldv_PreferredCommunicationLanguage = "ldv_preferredcommunicationlanguage";

			public const string ldv_userrole = "ldv_userrole";

			public const string OutgoingEmailDeliveryMethod = "outgoingemaildeliverymethod";

			public const string PreferredAddressCode = "preferredaddresscode";

			public const string PreferredEmailCode = "preferredemailcode";

			public const string PreferredPhoneCode = "preferredphonecode";

			public const string SetupUser = "setupuser";
		}

		public static class Labels
		{
			public static class AccessMode
			{
				public const string ReadWrite_1033 = "Read-Write";

				public const string ReadWrite_1025 = "للقراءة والكتابة";

				public const string Administrative_1033 = "Administrative";

				public const string Administrative_1025 = "إداري";

				public const string Read_1033 = "Read";

				public const string Read_1025 = "قراءة";

				public const string SupportUser_1033 = "Support User";

				public const string SupportUser_1025 = "مستخدم دعم";

				public const string Noninteractive_1033 = "Non-interactive";

				public const string Noninteractive_1025 = "غير تفاعلي";

				public const string DelegatedAdmin_1033 = "Delegated Admin";

				public const string DelegatedAdmin_1025 = "المسؤول المفو\u0651\u064eض";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(AccessMode), label, languageCode);
				}
			}

			public static class Address1_AddressTypeCode
			{
				public const string DefaultValue_1033 = "Default Value";

				public const string DefaultValue_1025 = "القيمة الافتراضية";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Address1_AddressTypeCode), label, languageCode);
				}
			}

			public static class Address1_ShippingMethodCode
			{
				public const string DefaultValue_1033 = "Default Value";

				public const string DefaultValue_1025 = "القيمة الافتراضية";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Address1_ShippingMethodCode), label, languageCode);
				}
			}

			public static class Address2_AddressTypeCode
			{
				public const string DefaultValue_1033 = "Default Value";

				public const string DefaultValue_1025 = "القيمة الافتراضية";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Address2_AddressTypeCode), label, languageCode);
				}
			}

			public static class Address2_ShippingMethodCode
			{
				public const string DefaultValue_1033 = "Default Value";

				public const string DefaultValue_1025 = "القيمة الافتراضية";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Address2_ShippingMethodCode), label, languageCode);
				}
			}

			public static class CALType
			{
				public const string Professional_1033 = "Professional";

				public const string Professional_1025 = "محترف";

				public const string Administrative_1033 = "Administrative";

				public const string Administrative_1025 = "\u200f\u200fإداري";

				public const string Basic_1033 = "Basic";

				public const string Basic_1025 = "أساسي";

				public const string DeviceProfessional_1033 = "Device Professional";

				public const string DeviceProfessional_1025 = "أخصائي أجهزة";

				public const string DeviceBasic_1033 = "Device Basic";

				public const string DeviceBasic_1025 = "Device Basic";

				public const string Essential_1033 = "Essential";

				public const string Essential_1025 = "أساسي";

				public const string DeviceEssential_1033 = "Device Essential";

				public const string DeviceEssential_1025 = "Device Essential";

				public const string Enterprise_1033 = "Enterprise";

				public const string Enterprise_1025 = "مؤسسة";

				public const string DeviceEnterprise_1033 = "Device Enterprise";

				public const string DeviceEnterprise_1025 = "ترخيص على مستوى المؤسسات للأجهزة";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CALType), label, languageCode);
				}
			}

			public static class DefaultFiltersPopulated
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DefaultFiltersPopulated), label, languageCode);
				}
			}

			public static class DisplayInServiceViews
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DisplayInServiceViews), label, languageCode);
				}
			}

			public static class EmailRouterAccessApproval
			{
				public const string Empty_1033 = "Empty";

				public const string Empty_1025 = "فارغ";

				public const string Approved_1033 = "Approved";

				public const string Approved_1025 = "\u200f\u200fتمت الموافقة";

				public const string PendingApproval_1033 = "Pending Approval";

				public const string PendingApproval_1025 = "موافقة معلقة";

				public const string Rejected_1033 = "Rejected";

				public const string Rejected_1025 = "مرفوض";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(EmailRouterAccessApproval), label, languageCode);
				}
			}

			public static class IncomingEmailDeliveryMethod
			{
				public const string None_1033 = "None";

				public const string None_1025 = "بلا";

				public const string MicrosoftDynamicsCRMforOutlook_1033 = "Microsoft Dynamics CRM for Outlook";

				public const string MicrosoftDynamicsCRMforOutlook_1025 = "Microsoft Dynamics CRM for Outlook";

				public const string ServerSideSynchronizationorEmailRouter_1033 = "Server-Side Synchronization or Email Router";

				public const string ServerSideSynchronizationorEmailRouter_1025 = "المزامنة من جانب الخادم أو جهاز توجيه البريد الإلكتروني";

				public const string ForwardMailbox_1033 = "Forward Mailbox";

				public const string ForwardMailbox_1025 = "صندوق بريد إعادة التوجيه";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IncomingEmailDeliveryMethod), label, languageCode);
				}
			}

			public static class InviteStatusCode
			{
				public const string InvitationNotSent_1033 = "Invitation Not Sent";

				public const string InvitationNotSent_1025 = "لم يتم إرسال الدعوة";

				public const string Invited_1033 = "Invited";

				public const string Invited_1025 = "تمت الدعوة";

				public const string InvitationNearExpired_1033 = "Invitation Near Expired";

				public const string InvitationNearExpired_1025 = "فترة الدعوة على وشك الانتهاء";

				public const string InvitationExpired_1033 = "Invitation Expired";

				public const string InvitationExpired_1025 = "انتهت فترة الدعوة";

				public const string InvitationAccepted_1033 = "Invitation Accepted";

				public const string InvitationAccepted_1025 = "تم قبول الدعوة";

				public const string InvitationRejected_1033 = "Invitation Rejected";

				public const string InvitationRejected_1025 = "تم رفض الدعوة";

				public const string InvitationRevoked_1033 = "Invitation Revoked";

				public const string InvitationRevoked_1025 = "تم إبطال الدعوة";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(InviteStatusCode), label, languageCode);
				}
			}

			public static class IsActiveDirectoryUser
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsActiveDirectoryUser), label, languageCode);
				}
			}

			public static class IsDisabled
			{
				public const string Disabled_1033 = "Disabled";

				public const string Disabled_1025 = "قيد التعطيل";

				public const string Enabled_1033 = "Enabled";

				public const string Enabled_1025 = "قيد التمكين";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsDisabled), label, languageCode);
				}
			}

			public static class IsEmailAddressApprovedByO365Admin
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsEmailAddressApprovedByO365Admin), label, languageCode);
				}
			}

			public static class IsIntegrationUser
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsIntegrationUser), label, languageCode);
				}
			}

			public static class IsLicensed
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsLicensed), label, languageCode);
				}
			}

			public static class IsSyncWithDirectory
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsSyncWithDirectory), label, languageCode);
				}
			}

			public static class ldv_isonleave
			{
				public const string Yes_1033 = "Yes";

				public const string No_1033 = "No";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(ldv_isonleave), label, languageCode);
				}
			}

			public static class ldv_PreferredCommunicationLanguage
			{
				public const string ArabicSaudiArabia_1033 = "Arabic - Saudi Arabia";

				public const string EnglishUnitedStates_1033 = "English - United States";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(ldv_PreferredCommunicationLanguage), label, languageCode);
				}
			}

			public static class ldv_userrole
			{
				public const string SocialResearcher_1033 = "Social Researcher";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(ldv_userrole), label, languageCode);
				}
			}

			public static class OutgoingEmailDeliveryMethod
			{
				public const string None_1033 = "None";

				public const string None_1025 = "بلا";

				public const string MicrosoftDynamicsCRMforOutlook_1033 = "Microsoft Dynamics CRM for Outlook";

				public const string MicrosoftDynamicsCRMforOutlook_1025 = "Microsoft Dynamics CRM for Outlook";

				public const string ServerSideSynchronizationorEmailRouter_1033 = "Server-Side Synchronization or Email Router";

				public const string ServerSideSynchronizationorEmailRouter_1025 = "المزامنة من جانب الخادم أو جهاز توجيه البريد الإلكتروني";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(OutgoingEmailDeliveryMethod), label, languageCode);
				}
			}

			public static class PreferredAddressCode
			{
				public const string MailingAddress_1033 = "Mailing Address";

				public const string MailingAddress_1025 = "عنوان المراسلة";

				public const string OtherAddress_1033 = "Other Address";

				public const string OtherAddress_1025 = "عنوان آخر";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(PreferredAddressCode), label, languageCode);
				}
			}

			public static class PreferredEmailCode
			{
				public const string DefaultValue_1033 = "Default Value";

				public const string DefaultValue_1025 = "القيمة الافتراضية";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(PreferredEmailCode), label, languageCode);
				}
			}

			public static class PreferredPhoneCode
			{
				public const string MainPhone_1033 = "Main Phone";

				public const string MainPhone_1025 = "الهاتف الرئيسي";

				public const string OtherPhone_1033 = "Other Phone";

				public const string OtherPhone_1025 = "هاتف آخر";

				public const string HomePhone_1033 = "Home Phone";

				public const string HomePhone_1025 = "هاتف المنزل";

				public const string MobilePhone_1033 = "Mobile Phone";

				public const string MobilePhone_1025 = "الهاتف الجوال";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(PreferredPhoneCode), label, languageCode);
				}
			}

			public static class SetupUser
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(SetupUser), label, languageCode);
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
			public const string AccessMode = "AccessMode";

			public const string ActiveDirectoryGuid = "ActiveDirectoryGuid";

			public const string Address1_AddressId = "Address1_AddressId";

			public const string Address1_AddressTypeCode = "Address1_AddressTypeCode";

			public const string Address1_City = "Address1_City";

			public const string Address1_Composite = "Address1_Composite";

			public const string Address1_Country = "Address1_Country";

			public const string Address1_County = "Address1_County";

			public const string Address1_Fax = "Address1_Fax";

			public const string Address1_Latitude = "Address1_Latitude";

			public const string Address1_Line1 = "Address1_Line1";

			public const string Address1_Line2 = "Address1_Line2";

			public const string Address1_Line3 = "Address1_Line3";

			public const string Address1_Longitude = "Address1_Longitude";

			public const string Address1_Name = "Address1_Name";

			public const string Address1_PostalCode = "Address1_PostalCode";

			public const string Address1_PostOfficeBox = "Address1_PostOfficeBox";

			public const string Address1_ShippingMethodCode = "Address1_ShippingMethodCode";

			public const string Address1_StateOrProvince = "Address1_StateOrProvince";

			public const string Address1_Telephone1 = "Address1_Telephone1";

			public const string Address1_Telephone2 = "Address1_Telephone2";

			public const string Address1_Telephone3 = "Address1_Telephone3";

			public const string Address1_UPSZone = "Address1_UPSZone";

			public const string Address1_UTCOffset = "Address1_UTCOffset";

			public const string Address2_AddressId = "Address2_AddressId";

			public const string Address2_AddressTypeCode = "Address2_AddressTypeCode";

			public const string Address2_City = "Address2_City";

			public const string Address2_Composite = "Address2_Composite";

			public const string Address2_Country = "Address2_Country";

			public const string Address2_County = "Address2_County";

			public const string Address2_Fax = "Address2_Fax";

			public const string Address2_Latitude = "Address2_Latitude";

			public const string Address2_Line1 = "Address2_Line1";

			public const string Address2_Line2 = "Address2_Line2";

			public const string Address2_Line3 = "Address2_Line3";

			public const string Address2_Longitude = "Address2_Longitude";

			public const string Address2_Name = "Address2_Name";

			public const string Address2_PostalCode = "Address2_PostalCode";

			public const string Address2_PostOfficeBox = "Address2_PostOfficeBox";

			public const string Address2_ShippingMethodCode = "Address2_ShippingMethodCode";

			public const string Address2_StateOrProvince = "Address2_StateOrProvince";

			public const string Address2_Telephone1 = "Address2_Telephone1";

			public const string Address2_Telephone2 = "Address2_Telephone2";

			public const string Address2_Telephone3 = "Address2_Telephone3";

			public const string Address2_UPSZone = "Address2_UPSZone";

			public const string Address2_UTCOffset = "Address2_UTCOffset";

			public const string BusinessUnitId = "BusinessUnitId";

			public const string BusinessUnitIdName = "BusinessUnitIdName";

			public const string CalendarId = "CalendarId";

			public const string CalendarIdName = "CalendarIdName";

			public const string CALType = "CALType";

			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string DefaultFiltersPopulated = "DefaultFiltersPopulated";

			public const string DefaultMailbox = "DefaultMailbox";

			public const string DefaultMailboxName = "DefaultMailboxName";

			public const string DefaultOdbFolderName = "DefaultOdbFolderName";

			public const string DisabledReason = "DisabledReason";

			public const string DisplayInServiceViews = "DisplayInServiceViews";

			public const string DomainName = "DomainName";

			public const string EmailRouterAccessApproval = "EmailRouterAccessApproval";

			public const string EmployeeId = "EmployeeId";

			public const string EntityImage = "EntityImage";

			public const string EntityImage_Timestamp = "EntityImage_Timestamp";

			public const string EntityImage_URL = "EntityImage_URL";

			public const string EntityImageId = "EntityImageId";

			public const string ExchangeRate = "ExchangeRate";

			public const string FirstName = "FirstName";

			public const string FullName = "FullName";

			public const string GovernmentId = "GovernmentId";

			public const string HomePhone = "HomePhone";

			public const string ImportSequenceNumber = "ImportSequenceNumber";

			public const string IncomingEmailDeliveryMethod = "IncomingEmailDeliveryMethod";

			public const string InternalEMailAddress = "InternalEMailAddress";

			public const string InviteStatusCode = "InviteStatusCode";

			public const string IsActiveDirectoryUser = "IsActiveDirectoryUser";

			public const string IsDisabled = "IsDisabled";

			public const string IsEmailAddressApprovedByO365Admin = "IsEmailAddressApprovedByO365Admin";

			public const string IsIntegrationUser = "IsIntegrationUser";

			public const string IsLicensed = "IsLicensed";

			public const string IsSyncWithDirectory = "IsSyncWithDirectory";

			public const string JobTitle = "JobTitle";

			public const string LastName = "LastName";

			public const string ldv_isonleave = "ldv_isonleave";

			public const string ldv_PreferredCommunicationLanguage = "ldv_PreferredCommunicationLanguage";

			public const string ldv_userrole = "ldv_userrole";

			public const string MiddleName = "MiddleName";

			public const string mlsd_branch = "mlsd_branch";

			public const string mlsd_branchName = "mlsd_branchName";

			public const string mlsd_Order = "mlsd_Order";

			public const string mlsd_region = "mlsd_region";

			public const string mlsd_regionName = "mlsd_regionName";

			public const string MobileAlertEMail = "MobileAlertEMail";

			public const string MobileOfflineProfileId = "MobileOfflineProfileId";

			public const string MobileOfflineProfileIdName = "MobileOfflineProfileIdName";

			public const string MobilePhone = "MobilePhone";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string NickName = "NickName";

			public const string OrganizationId = "OrganizationId";

			public const string OutgoingEmailDeliveryMethod = "OutgoingEmailDeliveryMethod";

			public const string OverriddenCreatedOn = "OverriddenCreatedOn";

			public const string ParentSystemUserId = "ParentSystemUserId";

			public const string ParentSystemUserIdName = "ParentSystemUserIdName";

			public const string PassportHi = "PassportHi";

			public const string PassportLo = "PassportLo";

			public const string PersonalEMailAddress = "PersonalEMailAddress";

			public const string PhotoUrl = "PhotoUrl";

			public const string PositionId = "PositionId";

			public const string PositionIdName = "PositionIdName";

			public const string PreferredAddressCode = "PreferredAddressCode";

			public const string PreferredEmailCode = "PreferredEmailCode";

			public const string PreferredPhoneCode = "PreferredPhoneCode";

			public const string ProcessId = "ProcessId";

			public const string QueueId = "QueueId";

			public const string QueueIdName = "QueueIdName";

			public const string Salutation = "Salutation";

			public const string SetupUser = "SetupUser";

			public const string SharePointEmailAddress = "SharePointEmailAddress";

			public const string SiteId = "SiteId";

			public const string SiteIdName = "SiteIdName";

			public const string Skills = "Skills";

			public const string StageId = "StageId";

			public const string SystemUserId = "SystemUserId";

			public const string TerritoryId = "TerritoryId";

			public const string TerritoryIdName = "TerritoryIdName";

			public const string TimeZoneRuleVersionNumber = "TimeZoneRuleVersionNumber";

			public const string Title = "Title";

			public const string TransactionCurrencyId = "TransactionCurrencyId";

			public const string TransactionCurrencyIdName = "TransactionCurrencyIdName";

			public const string TraversedPath = "TraversedPath";

			public const string UserLicenseType = "UserLicenseType";

			public const string UTCConversionTimeZoneCode = "UTCConversionTimeZoneCode";

			public const string VersionNumber = "VersionNumber";

			public const string WindowsLiveID = "WindowsLiveID";

			public const string YammerEmailAddress = "YammerEmailAddress";

			public const string YammerUserId = "YammerUserId";

			public const string YomiFirstName = "YomiFirstName";

			public const string YomiFullName = "YomiFullName";

			public const string YomiLastName = "YomiLastName";

			public const string YomiMiddleName = "YomiMiddleName";
		}

		public static class Labels
		{
			public static class AccessMode
			{
				public const string _1033 = "Access Mode";

				public const string _1025 = "وضع الوصول";
			}

			public static class ActiveDirectoryGuid
			{
				public const string _1033 = "Active Directory Guid";

				public const string _1025 = "Active Directory Guid";
			}

			public static class Address1_AddressId
			{
				public const string _1033 = "Address 1: ID";

				public const string _1025 = "العنوان 1: المعرف";
			}

			public static class Address1_AddressTypeCode
			{
				public const string _1033 = "Address 1: Address Type";

				public const string _1025 = "العنوان 1: نوع العنوان";
			}

			public static class Address1_City
			{
				public const string _1033 = "City";

				public const string _1025 = "المدينة";
			}

			public static class Address1_Composite
			{
				public const string _1033 = "Address";

				public const string _1025 = "العنوان";
			}

			public static class Address1_Country
			{
				public const string _1033 = "Country/Region";

				public const string _1025 = "الدولة/المنطقة";
			}

			public static class Address1_County
			{
				public const string _1033 = "Address 1: County";

				public const string _1025 = "العنوان 1: المقاطعة";
			}

			public static class Address1_Fax
			{
				public const string _1033 = "Address 1: Fax";

				public const string _1025 = "العنوان 1: الفاكس";
			}

			public static class Address1_Latitude
			{
				public const string _1033 = "Address 1: Latitude";

				public const string _1025 = "العنوان 1: خط العرض";
			}

			public static class Address1_Line1
			{
				public const string _1033 = "Street 1";

				public const string _1025 = "الشارع 1";
			}

			public static class Address1_Line2
			{
				public const string _1033 = "Street 2";

				public const string _1025 = "الشارع 2";
			}

			public static class Address1_Line3
			{
				public const string _1033 = "Street 3";

				public const string _1025 = "الشارع 3";
			}

			public static class Address1_Longitude
			{
				public const string _1033 = "Address 1: Longitude";

				public const string _1025 = "العنوان 1: خط الطول";
			}

			public static class Address1_Name
			{
				public const string _1033 = "Address 1: Name";

				public const string _1025 = "العنوان 1: الاسم";
			}

			public static class Address1_PostalCode
			{
				public const string _1033 = "ZIP/Postal Code";

				public const string _1025 = "الرمز البريدي";
			}

			public static class Address1_PostOfficeBox
			{
				public const string _1033 = "Address 1: Post Office Box";

				public const string _1025 = "العنوان 1: صندوق البريد";
			}

			public static class Address1_ShippingMethodCode
			{
				public const string _1033 = "Address 1: Shipping Method";

				public const string _1025 = "العنوان 1: أسلوب الشحن";
			}

			public static class Address1_StateOrProvince
			{
				public const string _1033 = "State/Province";

				public const string _1025 = "المحافظة/المنطقة";
			}

			public static class Address1_Telephone1
			{
				public const string _1033 = "Main Phone";

				public const string _1025 = "الهاتف الرئيسي";
			}

			public static class Address1_Telephone2
			{
				public const string _1033 = "Other Phone";

				public const string _1025 = "هاتف آخر";
			}

			public static class Address1_Telephone3
			{
				public const string _1033 = "Pager";

				public const string _1025 = "النداء";
			}

			public static class Address1_UPSZone
			{
				public const string _1033 = "Address 1: UPS Zone";

				public const string _1025 = "العنوان 1: منطقة UPS";
			}

			public static class Address1_UTCOffset
			{
				public const string _1033 = "Address 1: UTC Offset";

				public const string _1025 = "العنوان 1: إزاحة UTC";
			}

			public static class Address2_AddressId
			{
				public const string _1033 = "Address 2: ID";

				public const string _1025 = "العنوان 2: المعرف";
			}

			public static class Address2_AddressTypeCode
			{
				public const string _1033 = "Address 2: Address Type";

				public const string _1025 = "العنوان 2: نوع العنوان";
			}

			public static class Address2_City
			{
				public const string _1033 = "Other City";

				public const string _1025 = "مدينة أخرى";
			}

			public static class Address2_Composite
			{
				public const string _1033 = "Other Address";

				public const string _1025 = "عنوان آخر";
			}

			public static class Address2_Country
			{
				public const string _1033 = "Other Country/Region";

				public const string _1025 = "بلد/منطقة أخرى";
			}

			public static class Address2_County
			{
				public const string _1033 = "Address 2: County";

				public const string _1025 = "العنوان 2: المقاطعة";
			}

			public static class Address2_Fax
			{
				public const string _1033 = "Address 2: Fax";

				public const string _1025 = "العنوان 2: الفاكس";
			}

			public static class Address2_Latitude
			{
				public const string _1033 = "Address 2: Latitude";

				public const string _1025 = "العنوان 2: خط العرض";
			}

			public static class Address2_Line1
			{
				public const string _1033 = "Other Street 1";

				public const string _1025 = "شارع 1 آخر";
			}

			public static class Address2_Line2
			{
				public const string _1033 = "Other Street 2";

				public const string _1025 = "شارع 2 آخر";
			}

			public static class Address2_Line3
			{
				public const string _1033 = "Other Street 3";

				public const string _1025 = "شارع 3 آخر";
			}

			public static class Address2_Longitude
			{
				public const string _1033 = "Address 2: Longitude";

				public const string _1025 = "العنوان 2: خط الطول";
			}

			public static class Address2_Name
			{
				public const string _1033 = "Address 2: Name";

				public const string _1025 = "العنوان 2: الاسم";
			}

			public static class Address2_PostalCode
			{
				public const string _1033 = "Other ZIP/Postal Code";

				public const string _1025 = "رمز بريدي آخر";
			}

			public static class Address2_PostOfficeBox
			{
				public const string _1033 = "Address 2: Post Office Box";

				public const string _1025 = "العنوان 2: صندوق البريد";
			}

			public static class Address2_ShippingMethodCode
			{
				public const string _1033 = "Address 2: Shipping Method";

				public const string _1025 = "العنوان 2: أسلوب الشحن";
			}

			public static class Address2_StateOrProvince
			{
				public const string _1033 = "Other State/Province";

				public const string _1025 = "محافظة/منطقة أخرى";
			}

			public static class Address2_Telephone1
			{
				public const string _1033 = "Address 2: Telephone 1";

				public const string _1025 = "العنوان 2: الهاتف 1";
			}

			public static class Address2_Telephone2
			{
				public const string _1033 = "Address 2: Telephone 2";

				public const string _1025 = "العنوان 2: الهاتف 2";
			}

			public static class Address2_Telephone3
			{
				public const string _1033 = "Address 2: Telephone 3";

				public const string _1025 = "العنوان 2: الهاتف 3";
			}

			public static class Address2_UPSZone
			{
				public const string _1033 = "Address 2: UPS Zone";

				public const string _1025 = "العنوان 2: منطقة UPS";
			}

			public static class Address2_UTCOffset
			{
				public const string _1033 = "Address 2: UTC Offset";

				public const string _1025 = "العنوان 2: إزاحة UTC";
			}

			public static class BusinessUnitId
			{
				public const string _1033 = "Business Unit";

				public const string _1025 = "وحدة أعمال";
			}

			public static class CalendarId
			{
				public const string _1033 = "Calendar";

				public const string _1025 = "التقويم";
			}

			public static class CALType
			{
				public const string _1033 = "License Type";

				public const string _1025 = "نوع الترخيص";
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

			public static class DefaultFiltersPopulated
			{
				public const string _1033 = "Default Filters Populated";

				public const string _1025 = "عوامل التصفية الافتراضية المعممة";
			}

			public static class DefaultMailbox
			{
				public const string _1033 = "Mailbox";

				public const string _1025 = "صندوق بريد";
			}

			public static class DefaultOdbFolderName
			{
				public const string _1033 = "Default OneDrive for Business Folder Name";

				public const string _1025 = "اسم المجلد الافتراضي لـ OneDrive for Business";
			}

			public static class DisabledReason
			{
				public const string _1033 = "Disabled Reason";

				public const string _1025 = "سبب التعطيل";
			}

			public static class DisplayInServiceViews
			{
				public const string _1033 = "Display in Service Views";

				public const string _1025 = "العرض في طرق عرض الخدمات";
			}

			public static class DomainName
			{
				public const string _1033 = "User Name";

				public const string _1025 = "اسم المستخدم";
			}

			public static class EmailRouterAccessApproval
			{
				public const string _1033 = "Primary Email Status";

				public const string _1025 = "حالة البريد الإلكتروني الأساسي";
			}

			public static class EmployeeId
			{
				public const string _1033 = "Employee";

				public const string _1025 = "الموظف";
			}

			public static class EntityImageId
			{
				public const string _1033 = "Entity Image Id";

				public const string _1025 = "معر\u0651ف صورة الكيان";
			}

			public static class ExchangeRate
			{
				public const string _1033 = "Exchange Rate";

				public const string _1025 = "سعر الصرف";
			}

			public static class FirstName
			{
				public const string _1033 = "First Name";

				public const string _1025 = "الاسم الأول";
			}

			public static class FullName
			{
				public const string _1033 = "Full Name";

				public const string _1025 = "الاسم بالكامل";
			}

			public static class GovernmentId
			{
				public const string _1033 = "Government";

				public const string _1025 = "الحكومة";
			}

			public static class HomePhone
			{
				public const string _1033 = "Home Phone";

				public const string _1025 = "هاتف المنزل";
			}

			public static class ImportSequenceNumber
			{
				public const string _1033 = "Import Sequence Number";

				public const string _1025 = "الرقم التسلسلي للاستيراد";
			}

			public static class IncomingEmailDeliveryMethod
			{
				public const string _1033 = "Incoming Email Delivery Method";

				public const string _1025 = "أسلوب تسليم البريد الإلكتروني الوارد";
			}

			public static class InternalEMailAddress
			{
				public const string _1033 = "Primary Email";

				public const string _1025 = "البريد الإلكتروني الرئيسي";
			}

			public static class InviteStatusCode
			{
				public const string _1033 = "Invitation Status";

				public const string _1025 = "حالة الدعوة";
			}

			public static class IsActiveDirectoryUser
			{
				public const string _1033 = "Is Active Directory User";

				public const string _1025 = "مستخدم Active Directory";
			}

			public static class IsDisabled
			{
				public const string _1033 = "Status";

				public const string _1025 = "الحالة";
			}

			public static class IsEmailAddressApprovedByO365Admin
			{
				public const string _1033 = "Email Address O365 Admin Approval Status";

				public const string _1025 = "حالة اعتماد عنوان البريد الإلكتروني لمسؤول O365.";
			}

			public static class IsIntegrationUser
			{
				public const string _1033 = "Integration user mode";

				public const string _1025 = "وضع مستخدم التكامل";
			}

			public static class IsLicensed
			{
				public const string _1033 = "User Licensed";

				public const string _1025 = "المستخدم لديه ترخيص";
			}

			public static class IsSyncWithDirectory
			{
				public const string _1033 = "User Synced";

				public const string _1025 = "تمت مزامنة المستخدم";
			}

			public static class JobTitle
			{
				public const string _1033 = "Job Title";

				public const string _1025 = "المسمى الوظيفي";
			}

			public static class LastName
			{
				public const string _1033 = "Last Name";

				public const string _1025 = "الاسم الأخير";
			}

			public static class ldv_isonleave
			{
				public const string _1033 = "Is On Leave";
			}

			public static class ldv_PreferredCommunicationLanguage
			{
				public const string _1033 = "Preferred Communication Language";
			}

			public static class ldv_userrole
			{
				public const string _1033 = "User Role";
			}

			public static class MiddleName
			{
				public const string _1033 = "Middle Name";

				public const string _1025 = "الاسم الأوسط";
			}

			public static class mlsd_branch
			{
				public const string _1033 = "Branch";

				public const string _1025 = "فرع";
			}

			public static class mlsd_Order
			{
				public const string _1033 = "Order";

				public const string _1025 = "طلب";
			}

			public static class mlsd_region
			{
				public const string _1033 = "Region";

				public const string _1025 = "منطقة";
			}

			public static class MobileAlertEMail
			{
				public const string _1033 = "Mobile Alert Email";

				public const string _1025 = "بريد إلكتروني إعلامي على الهاتف الجوال";
			}

			public static class MobileOfflineProfileId
			{
				public const string _1033 = "Mobile Offline Profile";

				public const string _1025 = "ملف تعريف Mobile Offline";
			}

			public static class MobilePhone
			{
				public const string _1033 = "Mobile Phone";

				public const string _1025 = "الهاتف الجوال";
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

			public static class NickName
			{
				public const string _1033 = "Nickname";

				public const string _1025 = "اسم الشهرة";
			}

			public static class OrganizationId
			{
				public const string _1033 = "Organization ";

				public const string _1025 = "المؤسسة ";
			}

			public static class OutgoingEmailDeliveryMethod
			{
				public const string _1033 = "Outgoing Email Delivery Method";

				public const string _1025 = "أسلوب تسليم البريد الإلكتروني الصادر";
			}

			public static class OverriddenCreatedOn
			{
				public const string _1033 = "Record Created On";

				public const string _1025 = "وقت إنشاء السجل";
			}

			public static class ParentSystemUserId
			{
				public const string _1033 = "Manager";

				public const string _1025 = "المدير";
			}

			public static class PassportHi
			{
				public const string _1033 = "Passport Hi";

				public const string _1025 = "جواز المرور عالي الدقة";
			}

			public static class PassportLo
			{
				public const string _1033 = "Passport Lo";

				public const string _1025 = "جواز المرور منخفض الدقة";
			}

			public static class PersonalEMailAddress
			{
				public const string _1033 = "Email 2";

				public const string _1025 = "البريد الإلكتروني 2";
			}

			public static class PhotoUrl
			{
				public const string _1033 = "Photo URL";

				public const string _1025 = "محدد موقع معلومات الصورة";
			}

			public static class PositionId
			{
				public const string _1033 = "Position";

				public const string _1025 = "الموضع";
			}

			public static class PreferredAddressCode
			{
				public const string _1033 = "Preferred Address";

				public const string _1025 = "العنوان المفضل";
			}

			public static class PreferredEmailCode
			{
				public const string _1033 = "Preferred Email";

				public const string _1025 = "البريد الإلكتروني المفضل";
			}

			public static class PreferredPhoneCode
			{
				public const string _1033 = "Preferred Phone";

				public const string _1025 = "الهاتف المفضل";
			}

			public static class ProcessId
			{
				public const string _1033 = "Process";

				public const string _1025 = "العملية";
			}

			public static class QueueId
			{
				public const string _1033 = "Default Queue";

				public const string _1025 = "قائمة الانتظار الافتراضية";
			}

			public static class Salutation
			{
				public const string _1033 = "Salutation";

				public const string _1025 = "التحية";
			}

			public static class SetupUser
			{
				public const string _1033 = "Restricted Access Mode";

				public const string _1025 = "وضع الوصول المقيد";
			}

			public static class SharePointEmailAddress
			{
				public const string _1033 = "SharePoint Email Address";

				public const string _1025 = "عنوان البريد الإلكتروني الخاص بـSharePoint";
			}

			public static class SiteId
			{
				public const string _1033 = "Site";

				public const string _1025 = "الموقع";
			}

			public static class Skills
			{
				public const string _1033 = "Skills";

				public const string _1025 = "المهارات";
			}

			public static class StageId
			{
				public const string _1033 = "Process Stage";

				public const string _1025 = "مرحلة العملية";
			}

			public static class SystemUserId
			{
				public const string _1033 = "User";

				public const string _1025 = "المستخدم";
			}

			public static class TerritoryId
			{
				public const string _1033 = "Territory";

				public const string _1025 = "المنطقة";
			}

			public static class TimeZoneRuleVersionNumber
			{
				public const string _1033 = "Time Zone Rule Version Number";

				public const string _1025 = "رقم إصدار قاعدة المنطقة الزمنية";
			}

			public static class Title
			{
				public const string _1033 = "Title";

				public const string _1025 = "\u200f\u202bاللقب";
			}

			public static class TransactionCurrencyId
			{
				public const string _1033 = "Currency";

				public const string _1025 = "عملة";
			}

			public static class TraversedPath
			{
				public const string _1033 = "Traversed Path";

				public const string _1025 = "مسار تم اجتيازه";
			}

			public static class UserLicenseType
			{
				public const string _1033 = "User License Type";

				public const string _1025 = "نوع الترخيص الخاص بالمستخدم";
			}

			public static class UTCConversionTimeZoneCode
			{
				public const string _1033 = "UTC Conversion Time Zone Code";

				public const string _1025 = "رمز المنطقة الزمنية لتحويل UTC";
			}

			public static class VersionNumber
			{
				public const string _1033 = "Version number";

				public const string _1025 = "رقم الإصدار";
			}

			public static class WindowsLiveID
			{
				public const string _1033 = "Windows Live ID";

				public const string _1025 = "معرف Windows Live";
			}

			public static class YammerEmailAddress
			{
				public const string _1033 = "Yammer Email";

				public const string _1025 = "البريد الإلكتروني لخدمة Yammer";
			}

			public static class YammerUserId
			{
				public const string _1033 = "Yammer User ID";

				public const string _1025 = "معرف مستخدم Yammer";
			}

			public static class YomiFirstName
			{
				public const string _1033 = "Yomi First Name";

				public const string _1025 = "الاسم الأول Yomi";
			}

			public static class YomiFullName
			{
				public const string _1033 = "Yomi Full Name";

				public const string _1025 = "Yomi الاسم بالكامل";
			}

			public static class YomiLastName
			{
				public const string _1033 = "Yomi Last Name";

				public const string _1025 = "Yomi اسم العائلة";
			}

			public static class YomiMiddleName
			{
				public const string _1033 = "Yomi Middle Name";

				public const string _1025 = "Yomi الاسم الأوسط";
			}
		}

		public const string AccessMode = "accessmode";

		public const string ActiveDirectoryGuid = "activedirectoryguid";

		public const string Address1_AddressId = "address1_addressid";

		public const string Address1_AddressTypeCode = "address1_addresstypecode";

		public const string Address1_City = "address1_city";

		public const string Address1_Composite = "address1_composite";

		public const string Address1_Country = "address1_country";

		public const string Address1_County = "address1_county";

		public const string Address1_Fax = "address1_fax";

		public const string Address1_Latitude = "address1_latitude";

		public const string Address1_Line1 = "address1_line1";

		public const string Address1_Line2 = "address1_line2";

		public const string Address1_Line3 = "address1_line3";

		public const string Address1_Longitude = "address1_longitude";

		public const string Address1_Name = "address1_name";

		public const string Address1_PostalCode = "address1_postalcode";

		public const string Address1_PostOfficeBox = "address1_postofficebox";

		public const string Address1_ShippingMethodCode = "address1_shippingmethodcode";

		public const string Address1_StateOrProvince = "address1_stateorprovince";

		public const string Address1_Telephone1 = "address1_telephone1";

		public const string Address1_Telephone2 = "address1_telephone2";

		public const string Address1_Telephone3 = "address1_telephone3";

		public const string Address1_UPSZone = "address1_upszone";

		public const string Address1_UTCOffset = "address1_utcoffset";

		public const string Address2_AddressId = "address2_addressid";

		public const string Address2_AddressTypeCode = "address2_addresstypecode";

		public const string Address2_City = "address2_city";

		public const string Address2_Composite = "address2_composite";

		public const string Address2_Country = "address2_country";

		public const string Address2_County = "address2_county";

		public const string Address2_Fax = "address2_fax";

		public const string Address2_Latitude = "address2_latitude";

		public const string Address2_Line1 = "address2_line1";

		public const string Address2_Line2 = "address2_line2";

		public const string Address2_Line3 = "address2_line3";

		public const string Address2_Longitude = "address2_longitude";

		public const string Address2_Name = "address2_name";

		public const string Address2_PostalCode = "address2_postalcode";

		public const string Address2_PostOfficeBox = "address2_postofficebox";

		public const string Address2_ShippingMethodCode = "address2_shippingmethodcode";

		public const string Address2_StateOrProvince = "address2_stateorprovince";

		public const string Address2_Telephone1 = "address2_telephone1";

		public const string Address2_Telephone2 = "address2_telephone2";

		public const string Address2_Telephone3 = "address2_telephone3";

		public const string Address2_UPSZone = "address2_upszone";

		public const string Address2_UTCOffset = "address2_utcoffset";

		public const string BusinessUnitId = "businessunitid";

		public const string BusinessUnitIdName = "businessunitidName";

		public const string CalendarId = "calendarid";

		public const string CalendarIdName = "calendaridName";

		public const string CALType = "caltype";

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string DefaultFiltersPopulated = "defaultfilterspopulated";

		public const string DefaultMailbox = "defaultmailbox";

		public const string DefaultMailboxName = "defaultmailboxName";

		public const string DefaultOdbFolderName = "defaultodbfoldername";

		public const string DisabledReason = "disabledreason";

		public const string DisplayInServiceViews = "displayinserviceviews";

		public const string DomainName = "domainname";

		public const string EmailRouterAccessApproval = "emailrouteraccessapproval";

		public const string EmployeeId = "employeeid";

		public const string EntityImage = "entityimage";

		public const string EntityImage_Timestamp = "entityimage_timestamp";

		public const string EntityImage_URL = "entityimage_url";

		public const string EntityImageId = "entityimageid";

		public const string ExchangeRate = "exchangerate";

		public const string FirstName = "firstname";

		public const string FullName = "fullname";

		public const string GovernmentId = "governmentid";

		public const string HomePhone = "homephone";

		public const string ImportSequenceNumber = "importsequencenumber";

		public const string IncomingEmailDeliveryMethod = "incomingemaildeliverymethod";

		public const string InternalEMailAddress = "internalemailaddress";

		public const string InviteStatusCode = "invitestatuscode";

		public const string IsActiveDirectoryUser = "isactivedirectoryuser";

		public const string IsDisabled = "isdisabled";

		public const string IsEmailAddressApprovedByO365Admin = "isemailaddressapprovedbyo365admin";

		public const string IsIntegrationUser = "isintegrationuser";

		public const string IsLicensed = "islicensed";

		public const string IsSyncWithDirectory = "issyncwithdirectory";

		public const string JobTitle = "jobtitle";

		public const string LastName = "lastname";

		public const string ldv_isonleave = "ldv_isonleave";

		public const string ldv_PreferredCommunicationLanguage = "ldv_preferredcommunicationlanguage";

		public const string ldv_userrole = "ldv_userrole";

		public const string MiddleName = "middlename";

		public const string mlsd_branch = "mlsd_branch";

		public const string mlsd_branchName = "mlsd_branchName";

		public const string mlsd_Order = "mlsd_order";

		public const string mlsd_region = "mlsd_region";

		public const string mlsd_regionName = "mlsd_regionName";

		public const string MobileAlertEMail = "mobilealertemail";

		public const string MobileOfflineProfileId = "mobileofflineprofileid";

		public const string MobileOfflineProfileIdName = "mobileofflineprofileidName";

		public const string MobilePhone = "mobilephone";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string NickName = "nickname";

		public const string OrganizationId = "organizationid";

		public const string OutgoingEmailDeliveryMethod = "outgoingemaildeliverymethod";

		public const string OverriddenCreatedOn = "overriddencreatedon";

		public const string ParentSystemUserId = "parentsystemuserid";

		public const string ParentSystemUserIdName = "parentsystemuseridName";

		public const string PassportHi = "passporthi";

		public const string PassportLo = "passportlo";

		public const string PersonalEMailAddress = "personalemailaddress";

		public const string PhotoUrl = "photourl";

		public const string PositionId = "positionid";

		public const string PositionIdName = "positionidName";

		public const string PreferredAddressCode = "preferredaddresscode";

		public const string PreferredEmailCode = "preferredemailcode";

		public const string PreferredPhoneCode = "preferredphonecode";

		public const string ProcessId = "processid";

		public const string QueueId = "queueid";

		public const string QueueIdName = "queueidName";

		public const string Salutation = "salutation";

		public const string SetupUser = "setupuser";

		public const string SharePointEmailAddress = "sharepointemailaddress";

		public const string SiteId = "siteid";

		public const string SiteIdName = "siteidName";

		public const string Skills = "skills";

		public const string StageId = "stageid";

		public const string SystemUserId = "systemuserid";

		public const string TerritoryId = "territoryid";

		public const string TerritoryIdName = "territoryidName";

		public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";

		public const string Title = "title";

		public const string TransactionCurrencyId = "transactioncurrencyid";

		public const string TransactionCurrencyIdName = "transactioncurrencyidName";

		public const string TraversedPath = "traversedpath";

		public const string UserLicenseType = "userlicensetype";

		public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";

		public const string VersionNumber = "versionnumber";

		public const string WindowsLiveID = "windowsliveid";

		public const string YammerEmailAddress = "yammeremailaddress";

		public const string YammerUserId = "yammeruserid";

		public const string YomiFirstName = "yomifirstname";

		public const string YomiFullName = "yomifullname";

		public const string YomiLastName = "yomilastname";

		public const string YomiMiddleName = "yomimiddlename";
	}

	public static class Relations
	{
		public static class OneToN
		{
			public const string createdby_pluginassembly = "createdby_pluginassembly";

			public const string createdby_sdkmessage = "createdby_sdkmessage";

			public const string createdby_sdkmessagefilter = "createdby_sdkmessagefilter";

			public const string createdby_sdkmessageprocessingstep = "createdby_sdkmessageprocessingstep";

			public const string impersonatinguserid_sdkmessageprocessingstep = "impersonatinguserid_sdkmessageprocessingstep";

			public const string lk_fieldsecurityprofile_createdby = "lk_fieldsecurityprofile_createdby";

			public const string lk_fieldsecurityprofile_createdonbehalfby = "lk_fieldsecurityprofile_createdonbehalfby";

			public const string lk_fieldsecurityprofile_modifiedby = "lk_fieldsecurityprofile_modifiedby";

			public const string lk_fieldsecurityprofile_modifiedonbehalfby = "lk_fieldsecurityprofile_modifiedonbehalfby";

			public const string lk_pluginassembly_createdonbehalfby = "lk_pluginassembly_createdonbehalfby";

			public const string lk_pluginassembly_modifiedonbehalfby = "lk_pluginassembly_modifiedonbehalfby";

			public const string lk_role_createdonbehalfby = "lk_role_createdonbehalfby";

			public const string lk_role_modifiedonbehalfby = "lk_role_modifiedonbehalfby";

			public const string lk_rolebase_createdby = "lk_rolebase_createdby";

			public const string lk_rolebase_modifiedby = "lk_rolebase_modifiedby";

			public const string lk_sdkmessage_createdonbehalfby = "lk_sdkmessage_createdonbehalfby";

			public const string lk_sdkmessage_modifiedonbehalfby = "lk_sdkmessage_modifiedonbehalfby";

			public const string lk_sdkmessagefilter_createdonbehalfby = "lk_sdkmessagefilter_createdonbehalfby";

			public const string lk_sdkmessagefilter_modifiedonbehalfby = "lk_sdkmessagefilter_modifiedonbehalfby";

			public const string lk_sdkmessageprocessingstep_createdonbehalfby = "lk_sdkmessageprocessingstep_createdonbehalfby";

			public const string lk_sdkmessageprocessingstep_modifiedonbehalfby = "lk_sdkmessageprocessingstep_modifiedonbehalfby";

			public const string lk_systemuser_createdonbehalfby = "lk_systemuser_createdonbehalfby";

			public const string lk_systemuser_modifiedonbehalfby = "lk_systemuser_modifiedonbehalfby";

			public const string lk_systemuserbase_createdby = "lk_systemuserbase_createdby";

			public const string lk_systemuserbase_modifiedby = "lk_systemuserbase_modifiedby";

			public const string modifiedby_pluginassembly = "modifiedby_pluginassembly";

			public const string modifiedby_sdkmessage = "modifiedby_sdkmessage";

			public const string modifiedby_sdkmessagefilter = "modifiedby_sdkmessagefilter";

			public const string modifiedby_sdkmessageprocessingstep = "modifiedby_sdkmessageprocessingstep";

			public const string system_user_activity_parties = "system_user_activity_parties";

			public const string system_user_workflow = "system_user_workflow";

			public const string user_parent_user = "user_parent_user";

			public const string workflow_createdby = "workflow_createdby";

			public const string workflow_createdonbehalfby = "workflow_createdonbehalfby";

			public const string workflow_dependency_createdby = "workflow_dependency_createdby";

			public const string workflow_dependency_createdonbehalfby = "workflow_dependency_createdonbehalfby";

			public const string workflow_dependency_modifiedby = "workflow_dependency_modifiedby";

			public const string workflow_dependency_modifiedonbehalfby = "workflow_dependency_modifiedonbehalfby";

			public const string workflow_modifiedby = "workflow_modifiedby";

			public const string workflow_modifiedonbehalfby = "workflow_modifiedonbehalfby";
		}

		public static class NToOne
		{
			public static class Lookups
			{
				public const string lk_systemuser_createdonbehalfby = "createdonbehalfby";

				public const string lk_systemuser_modifiedonbehalfby = "modifiedonbehalfby";

				public const string lk_systemuserbase_createdby = "createdby";

				public const string lk_systemuserbase_modifiedby = "modifiedby";

				public const string user_parent_user = "parentsystemuserid";
			}

			public const string lk_systemuser_createdonbehalfby = "lk_systemuser_createdonbehalfby";

			public const string lk_systemuser_modifiedonbehalfby = "lk_systemuser_modifiedonbehalfby";

			public const string lk_systemuserbase_createdby = "lk_systemuserbase_createdby";

			public const string lk_systemuserbase_modifiedby = "lk_systemuserbase_modifiedby";

			public const string user_parent_user = "user_parent_user";
		}

		public static class NToN
		{
			public const string systemuserprofiles_association = "systemuserprofiles_association";

			public const string systemuserroles_association = "systemuserroles_association";
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "User";

	public const string SchemaName = "SystemUser";

	public const string EntityLogicalName = "systemuser";

	public const int EntityTypeCode = 8;

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

	[AttributeLogicalName("systemuserid")]
	public override Guid Id
	{
		get
		{
			return ((Entity)this).Id;
		}
		set
		{
			SystemUserId = value;
		}
	}

	[AttributeLogicalName("accessmode")]
	public OptionSetValue AccessMode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("accessmode");
		}
		set
		{
			OnPropertyChanging("AccessMode");
			((Entity)this).SetAttributeValue("accessmode", (object)value);
			OnPropertyChanged("AccessMode");
		}
	}

	[AttributeLogicalName("activedirectoryguid")]
	public Guid? ActiveDirectoryGuid => ((Entity)this).GetAttributeValue<Guid?>("activedirectoryguid");

	[AttributeLogicalName("address1_addressid")]
	public Guid? Address1_AddressId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("address1_addressid");
		}
		set
		{
			OnPropertyChanging("Address1_AddressId");
			((Entity)this).SetAttributeValue("address1_addressid", (object)value);
			OnPropertyChanged("Address1_AddressId");
		}
	}

	[AttributeLogicalName("address1_addresstypecode")]
	public OptionSetValue Address1_AddressTypeCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("address1_addresstypecode");
		}
		set
		{
			OnPropertyChanging("Address1_AddressTypeCode");
			((Entity)this).SetAttributeValue("address1_addresstypecode", (object)value);
			OnPropertyChanged("Address1_AddressTypeCode");
		}
	}

	[AttributeLogicalName("address1_city")]
	public string Address1_City
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_city");
		}
		set
		{
			OnPropertyChanging("Address1_City");
			((Entity)this).SetAttributeValue("address1_city", (object)value);
			OnPropertyChanged("Address1_City");
		}
	}

	[AttributeLogicalName("address1_composite")]
	public string Address1_Composite => ((Entity)this).GetAttributeValue<string>("address1_composite");

	[AttributeLogicalName("address1_country")]
	public string Address1_Country
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_country");
		}
		set
		{
			OnPropertyChanging("Address1_Country");
			((Entity)this).SetAttributeValue("address1_country", (object)value);
			OnPropertyChanged("Address1_Country");
		}
	}

	[AttributeLogicalName("address1_county")]
	public string Address1_County
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_county");
		}
		set
		{
			OnPropertyChanging("Address1_County");
			((Entity)this).SetAttributeValue("address1_county", (object)value);
			OnPropertyChanged("Address1_County");
		}
	}

	[AttributeLogicalName("address1_fax")]
	public string Address1_Fax
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_fax");
		}
		set
		{
			OnPropertyChanging("Address1_Fax");
			((Entity)this).SetAttributeValue("address1_fax", (object)value);
			OnPropertyChanged("Address1_Fax");
		}
	}

	[AttributeLogicalName("address1_latitude")]
	public double? Address1_Latitude
	{
		get
		{
			return ((Entity)this).GetAttributeValue<double?>("address1_latitude");
		}
		set
		{
			OnPropertyChanging("Address1_Latitude");
			((Entity)this).SetAttributeValue("address1_latitude", (object)value);
			OnPropertyChanged("Address1_Latitude");
		}
	}

	[AttributeLogicalName("address1_line1")]
	public string Address1_Line1
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_line1");
		}
		set
		{
			OnPropertyChanging("Address1_Line1");
			((Entity)this).SetAttributeValue("address1_line1", (object)value);
			OnPropertyChanged("Address1_Line1");
		}
	}

	[AttributeLogicalName("address1_line2")]
	public string Address1_Line2
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_line2");
		}
		set
		{
			OnPropertyChanging("Address1_Line2");
			((Entity)this).SetAttributeValue("address1_line2", (object)value);
			OnPropertyChanged("Address1_Line2");
		}
	}

	[AttributeLogicalName("address1_line3")]
	public string Address1_Line3
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_line3");
		}
		set
		{
			OnPropertyChanging("Address1_Line3");
			((Entity)this).SetAttributeValue("address1_line3", (object)value);
			OnPropertyChanged("Address1_Line3");
		}
	}

	[AttributeLogicalName("address1_longitude")]
	public double? Address1_Longitude
	{
		get
		{
			return ((Entity)this).GetAttributeValue<double?>("address1_longitude");
		}
		set
		{
			OnPropertyChanging("Address1_Longitude");
			((Entity)this).SetAttributeValue("address1_longitude", (object)value);
			OnPropertyChanged("Address1_Longitude");
		}
	}

	[AttributeLogicalName("address1_name")]
	public string Address1_Name
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_name");
		}
		set
		{
			OnPropertyChanging("Address1_Name");
			((Entity)this).SetAttributeValue("address1_name", (object)value);
			OnPropertyChanged("Address1_Name");
		}
	}

	[AttributeLogicalName("address1_postalcode")]
	public string Address1_PostalCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_postalcode");
		}
		set
		{
			OnPropertyChanging("Address1_PostalCode");
			((Entity)this).SetAttributeValue("address1_postalcode", (object)value);
			OnPropertyChanged("Address1_PostalCode");
		}
	}

	[AttributeLogicalName("address1_postofficebox")]
	public string Address1_PostOfficeBox
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_postofficebox");
		}
		set
		{
			OnPropertyChanging("Address1_PostOfficeBox");
			((Entity)this).SetAttributeValue("address1_postofficebox", (object)value);
			OnPropertyChanged("Address1_PostOfficeBox");
		}
	}

	[AttributeLogicalName("address1_shippingmethodcode")]
	public OptionSetValue Address1_ShippingMethodCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("address1_shippingmethodcode");
		}
		set
		{
			OnPropertyChanging("Address1_ShippingMethodCode");
			((Entity)this).SetAttributeValue("address1_shippingmethodcode", (object)value);
			OnPropertyChanged("Address1_ShippingMethodCode");
		}
	}

	[AttributeLogicalName("address1_stateorprovince")]
	public string Address1_StateOrProvince
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_stateorprovince");
		}
		set
		{
			OnPropertyChanging("Address1_StateOrProvince");
			((Entity)this).SetAttributeValue("address1_stateorprovince", (object)value);
			OnPropertyChanged("Address1_StateOrProvince");
		}
	}

	[AttributeLogicalName("address1_telephone1")]
	public string Address1_Telephone1
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_telephone1");
		}
		set
		{
			OnPropertyChanging("Address1_Telephone1");
			((Entity)this).SetAttributeValue("address1_telephone1", (object)value);
			OnPropertyChanged("Address1_Telephone1");
		}
	}

	[AttributeLogicalName("address1_telephone2")]
	public string Address1_Telephone2
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_telephone2");
		}
		set
		{
			OnPropertyChanging("Address1_Telephone2");
			((Entity)this).SetAttributeValue("address1_telephone2", (object)value);
			OnPropertyChanged("Address1_Telephone2");
		}
	}

	[AttributeLogicalName("address1_telephone3")]
	public string Address1_Telephone3
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_telephone3");
		}
		set
		{
			OnPropertyChanging("Address1_Telephone3");
			((Entity)this).SetAttributeValue("address1_telephone3", (object)value);
			OnPropertyChanged("Address1_Telephone3");
		}
	}

	[AttributeLogicalName("address1_upszone")]
	public string Address1_UPSZone
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address1_upszone");
		}
		set
		{
			OnPropertyChanging("Address1_UPSZone");
			((Entity)this).SetAttributeValue("address1_upszone", (object)value);
			OnPropertyChanged("Address1_UPSZone");
		}
	}

	[AttributeLogicalName("address1_utcoffset")]
	public int? Address1_UTCOffset
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("address1_utcoffset");
		}
		set
		{
			OnPropertyChanging("Address1_UTCOffset");
			((Entity)this).SetAttributeValue("address1_utcoffset", (object)value);
			OnPropertyChanged("Address1_UTCOffset");
		}
	}

	[AttributeLogicalName("address2_addressid")]
	public Guid? Address2_AddressId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("address2_addressid");
		}
		set
		{
			OnPropertyChanging("Address2_AddressId");
			((Entity)this).SetAttributeValue("address2_addressid", (object)value);
			OnPropertyChanged("Address2_AddressId");
		}
	}

	[AttributeLogicalName("address2_addresstypecode")]
	public OptionSetValue Address2_AddressTypeCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("address2_addresstypecode");
		}
		set
		{
			OnPropertyChanging("Address2_AddressTypeCode");
			((Entity)this).SetAttributeValue("address2_addresstypecode", (object)value);
			OnPropertyChanged("Address2_AddressTypeCode");
		}
	}

	[AttributeLogicalName("address2_city")]
	public string Address2_City
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_city");
		}
		set
		{
			OnPropertyChanging("Address2_City");
			((Entity)this).SetAttributeValue("address2_city", (object)value);
			OnPropertyChanged("Address2_City");
		}
	}

	[AttributeLogicalName("address2_composite")]
	public string Address2_Composite => ((Entity)this).GetAttributeValue<string>("address2_composite");

	[AttributeLogicalName("address2_country")]
	public string Address2_Country
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_country");
		}
		set
		{
			OnPropertyChanging("Address2_Country");
			((Entity)this).SetAttributeValue("address2_country", (object)value);
			OnPropertyChanged("Address2_Country");
		}
	}

	[AttributeLogicalName("address2_county")]
	public string Address2_County
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_county");
		}
		set
		{
			OnPropertyChanging("Address2_County");
			((Entity)this).SetAttributeValue("address2_county", (object)value);
			OnPropertyChanged("Address2_County");
		}
	}

	[AttributeLogicalName("address2_fax")]
	public string Address2_Fax
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_fax");
		}
		set
		{
			OnPropertyChanging("Address2_Fax");
			((Entity)this).SetAttributeValue("address2_fax", (object)value);
			OnPropertyChanged("Address2_Fax");
		}
	}

	[AttributeLogicalName("address2_latitude")]
	public double? Address2_Latitude
	{
		get
		{
			return ((Entity)this).GetAttributeValue<double?>("address2_latitude");
		}
		set
		{
			OnPropertyChanging("Address2_Latitude");
			((Entity)this).SetAttributeValue("address2_latitude", (object)value);
			OnPropertyChanged("Address2_Latitude");
		}
	}

	[AttributeLogicalName("address2_line1")]
	public string Address2_Line1
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_line1");
		}
		set
		{
			OnPropertyChanging("Address2_Line1");
			((Entity)this).SetAttributeValue("address2_line1", (object)value);
			OnPropertyChanged("Address2_Line1");
		}
	}

	[AttributeLogicalName("address2_line2")]
	public string Address2_Line2
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_line2");
		}
		set
		{
			OnPropertyChanging("Address2_Line2");
			((Entity)this).SetAttributeValue("address2_line2", (object)value);
			OnPropertyChanged("Address2_Line2");
		}
	}

	[AttributeLogicalName("address2_line3")]
	public string Address2_Line3
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_line3");
		}
		set
		{
			OnPropertyChanging("Address2_Line3");
			((Entity)this).SetAttributeValue("address2_line3", (object)value);
			OnPropertyChanged("Address2_Line3");
		}
	}

	[AttributeLogicalName("address2_longitude")]
	public double? Address2_Longitude
	{
		get
		{
			return ((Entity)this).GetAttributeValue<double?>("address2_longitude");
		}
		set
		{
			OnPropertyChanging("Address2_Longitude");
			((Entity)this).SetAttributeValue("address2_longitude", (object)value);
			OnPropertyChanged("Address2_Longitude");
		}
	}

	[AttributeLogicalName("address2_name")]
	public string Address2_Name
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_name");
		}
		set
		{
			OnPropertyChanging("Address2_Name");
			((Entity)this).SetAttributeValue("address2_name", (object)value);
			OnPropertyChanged("Address2_Name");
		}
	}

	[AttributeLogicalName("address2_postalcode")]
	public string Address2_PostalCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_postalcode");
		}
		set
		{
			OnPropertyChanging("Address2_PostalCode");
			((Entity)this).SetAttributeValue("address2_postalcode", (object)value);
			OnPropertyChanged("Address2_PostalCode");
		}
	}

	[AttributeLogicalName("address2_postofficebox")]
	public string Address2_PostOfficeBox
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_postofficebox");
		}
		set
		{
			OnPropertyChanging("Address2_PostOfficeBox");
			((Entity)this).SetAttributeValue("address2_postofficebox", (object)value);
			OnPropertyChanged("Address2_PostOfficeBox");
		}
	}

	[AttributeLogicalName("address2_shippingmethodcode")]
	public OptionSetValue Address2_ShippingMethodCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("address2_shippingmethodcode");
		}
		set
		{
			OnPropertyChanging("Address2_ShippingMethodCode");
			((Entity)this).SetAttributeValue("address2_shippingmethodcode", (object)value);
			OnPropertyChanged("Address2_ShippingMethodCode");
		}
	}

	[AttributeLogicalName("address2_stateorprovince")]
	public string Address2_StateOrProvince
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_stateorprovince");
		}
		set
		{
			OnPropertyChanging("Address2_StateOrProvince");
			((Entity)this).SetAttributeValue("address2_stateorprovince", (object)value);
			OnPropertyChanged("Address2_StateOrProvince");
		}
	}

	[AttributeLogicalName("address2_telephone1")]
	public string Address2_Telephone1
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_telephone1");
		}
		set
		{
			OnPropertyChanging("Address2_Telephone1");
			((Entity)this).SetAttributeValue("address2_telephone1", (object)value);
			OnPropertyChanged("Address2_Telephone1");
		}
	}

	[AttributeLogicalName("address2_telephone2")]
	public string Address2_Telephone2
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_telephone2");
		}
		set
		{
			OnPropertyChanging("Address2_Telephone2");
			((Entity)this).SetAttributeValue("address2_telephone2", (object)value);
			OnPropertyChanged("Address2_Telephone2");
		}
	}

	[AttributeLogicalName("address2_telephone3")]
	public string Address2_Telephone3
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_telephone3");
		}
		set
		{
			OnPropertyChanging("Address2_Telephone3");
			((Entity)this).SetAttributeValue("address2_telephone3", (object)value);
			OnPropertyChanged("Address2_Telephone3");
		}
	}

	[AttributeLogicalName("address2_upszone")]
	public string Address2_UPSZone
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("address2_upszone");
		}
		set
		{
			OnPropertyChanging("Address2_UPSZone");
			((Entity)this).SetAttributeValue("address2_upszone", (object)value);
			OnPropertyChanged("Address2_UPSZone");
		}
	}

	[AttributeLogicalName("address2_utcoffset")]
	public int? Address2_UTCOffset
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("address2_utcoffset");
		}
		set
		{
			OnPropertyChanging("Address2_UTCOffset");
			((Entity)this).SetAttributeValue("address2_utcoffset", (object)value);
			OnPropertyChanged("Address2_UTCOffset");
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

	[AttributeLogicalName("calendarid")]
	public EntityReference CalendarId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("calendarid");
		}
		set
		{
			OnPropertyChanging("CalendarId");
			((Entity)this).SetAttributeValue("calendarid", (object)value);
			OnPropertyChanged("CalendarId");
		}
	}

	[AttributeLogicalName("caltype")]
	public OptionSetValue CALType
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("caltype");
		}
		set
		{
			OnPropertyChanging("CALType");
			((Entity)this).SetAttributeValue("caltype", (object)value);
			OnPropertyChanged("CALType");
		}
	}

	[AttributeLogicalName("createdby")]
	public EntityReference CreatedBy => ((Entity)this).GetAttributeValue<EntityReference>("createdby");

	[AttributeLogicalName("createdon")]
	public DateTime? CreatedOn => ((Entity)this).GetAttributeValue<DateTime?>("createdon");

	[AttributeLogicalName("createdonbehalfby")]
	public EntityReference CreatedOnBehalfBy => ((Entity)this).GetAttributeValue<EntityReference>("createdonbehalfby");

	[AttributeLogicalName("defaultfilterspopulated")]
	public bool? DefaultFiltersPopulated => ((Entity)this).GetAttributeValue<bool?>("defaultfilterspopulated");

	[AttributeLogicalName("defaultmailbox")]
	public EntityReference DefaultMailbox => ((Entity)this).GetAttributeValue<EntityReference>("defaultmailbox");

	[AttributeLogicalName("defaultodbfoldername")]
	public string DefaultOdbFolderName => ((Entity)this).GetAttributeValue<string>("defaultodbfoldername");

	[AttributeLogicalName("disabledreason")]
	public string DisabledReason => ((Entity)this).GetAttributeValue<string>("disabledreason");

	[AttributeLogicalName("displayinserviceviews")]
	public bool? DisplayInServiceViews
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("displayinserviceviews");
		}
		set
		{
			OnPropertyChanging("DisplayInServiceViews");
			((Entity)this).SetAttributeValue("displayinserviceviews", (object)value);
			OnPropertyChanged("DisplayInServiceViews");
		}
	}

	[AttributeLogicalName("domainname")]
	public string DomainName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("domainname");
		}
		set
		{
			OnPropertyChanging("DomainName");
			((Entity)this).SetAttributeValue("domainname", (object)value);
			OnPropertyChanged("DomainName");
		}
	}

	[AttributeLogicalName("emailrouteraccessapproval")]
	public OptionSetValue EmailRouterAccessApproval
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("emailrouteraccessapproval");
		}
		set
		{
			OnPropertyChanging("EmailRouterAccessApproval");
			((Entity)this).SetAttributeValue("emailrouteraccessapproval", (object)value);
			OnPropertyChanged("EmailRouterAccessApproval");
		}
	}

	[AttributeLogicalName("employeeid")]
	public string EmployeeId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("employeeid");
		}
		set
		{
			OnPropertyChanging("EmployeeId");
			((Entity)this).SetAttributeValue("employeeid", (object)value);
			OnPropertyChanged("EmployeeId");
		}
	}

	[AttributeLogicalName("entityimage")]
	public byte[] EntityImage
	{
		get
		{
			return ((Entity)this).GetAttributeValue<byte[]>("entityimage");
		}
		set
		{
			OnPropertyChanging("EntityImage");
			((Entity)this).SetAttributeValue("entityimage", (object)value);
			OnPropertyChanged("EntityImage");
		}
	}

	[AttributeLogicalName("entityimage_timestamp")]
	public long? EntityImage_Timestamp => ((Entity)this).GetAttributeValue<long?>("entityimage_timestamp");

	[AttributeLogicalName("entityimage_url")]
	public string EntityImage_URL => ((Entity)this).GetAttributeValue<string>("entityimage_url");

	[AttributeLogicalName("entityimageid")]
	public Guid? EntityImageId => ((Entity)this).GetAttributeValue<Guid?>("entityimageid");

	[AttributeLogicalName("exchangerate")]
	public decimal? ExchangeRate => ((Entity)this).GetAttributeValue<decimal?>("exchangerate");

	[AttributeLogicalName("firstname")]
	public string FirstName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("firstname");
		}
		set
		{
			OnPropertyChanging("FirstName");
			((Entity)this).SetAttributeValue("firstname", (object)value);
			OnPropertyChanged("FirstName");
		}
	}

	[AttributeLogicalName("fullname")]
	public string FullName => ((Entity)this).GetAttributeValue<string>("fullname");

	[AttributeLogicalName("governmentid")]
	public string GovernmentId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("governmentid");
		}
		set
		{
			OnPropertyChanging("GovernmentId");
			((Entity)this).SetAttributeValue("governmentid", (object)value);
			OnPropertyChanged("GovernmentId");
		}
	}

	[AttributeLogicalName("homephone")]
	public string HomePhone
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("homephone");
		}
		set
		{
			OnPropertyChanging("HomePhone");
			((Entity)this).SetAttributeValue("homephone", (object)value);
			OnPropertyChanged("HomePhone");
		}
	}

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

	[AttributeLogicalName("incomingemaildeliverymethod")]
	public OptionSetValue IncomingEmailDeliveryMethod
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("incomingemaildeliverymethod");
		}
		set
		{
			OnPropertyChanging("IncomingEmailDeliveryMethod");
			((Entity)this).SetAttributeValue("incomingemaildeliverymethod", (object)value);
			OnPropertyChanged("IncomingEmailDeliveryMethod");
		}
	}

	[AttributeLogicalName("internalemailaddress")]
	public string InternalEMailAddress
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("internalemailaddress");
		}
		set
		{
			OnPropertyChanging("InternalEMailAddress");
			((Entity)this).SetAttributeValue("internalemailaddress", (object)value);
			OnPropertyChanged("InternalEMailAddress");
		}
	}

	[AttributeLogicalName("invitestatuscode")]
	public OptionSetValue InviteStatusCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("invitestatuscode");
		}
		set
		{
			OnPropertyChanging("InviteStatusCode");
			((Entity)this).SetAttributeValue("invitestatuscode", (object)value);
			OnPropertyChanged("InviteStatusCode");
		}
	}

	[AttributeLogicalName("isactivedirectoryuser")]
	public bool? IsActiveDirectoryUser => ((Entity)this).GetAttributeValue<bool?>("isactivedirectoryuser");

	[AttributeLogicalName("isdisabled")]
	public bool? IsDisabled => ((Entity)this).GetAttributeValue<bool?>("isdisabled");

	[AttributeLogicalName("isemailaddressapprovedbyo365admin")]
	public bool? IsEmailAddressApprovedByO365Admin => ((Entity)this).GetAttributeValue<bool?>("isemailaddressapprovedbyo365admin");

	[AttributeLogicalName("isintegrationuser")]
	public bool? IsIntegrationUser
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("isintegrationuser");
		}
		set
		{
			OnPropertyChanging("IsIntegrationUser");
			((Entity)this).SetAttributeValue("isintegrationuser", (object)value);
			OnPropertyChanged("IsIntegrationUser");
		}
	}

	[AttributeLogicalName("islicensed")]
	public bool? IsLicensed
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("islicensed");
		}
		set
		{
			OnPropertyChanging("IsLicensed");
			((Entity)this).SetAttributeValue("islicensed", (object)value);
			OnPropertyChanged("IsLicensed");
		}
	}

	[AttributeLogicalName("issyncwithdirectory")]
	public bool? IsSyncWithDirectory
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("issyncwithdirectory");
		}
		set
		{
			OnPropertyChanging("IsSyncWithDirectory");
			((Entity)this).SetAttributeValue("issyncwithdirectory", (object)value);
			OnPropertyChanged("IsSyncWithDirectory");
		}
	}

	[AttributeLogicalName("jobtitle")]
	public string JobTitle
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("jobtitle");
		}
		set
		{
			OnPropertyChanging("JobTitle");
			((Entity)this).SetAttributeValue("jobtitle", (object)value);
			OnPropertyChanged("JobTitle");
		}
	}

	[AttributeLogicalName("lastname")]
	public string LastName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("lastname");
		}
		set
		{
			OnPropertyChanging("LastName");
			((Entity)this).SetAttributeValue("lastname", (object)value);
			OnPropertyChanged("LastName");
		}
	}

	[AttributeLogicalName("ldv_isonleave")]
	public bool? ldv_isonleave
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("ldv_isonleave");
		}
		set
		{
			OnPropertyChanging("ldv_isonleave");
			((Entity)this).SetAttributeValue("ldv_isonleave", (object)value);
			OnPropertyChanged("ldv_isonleave");
		}
	}

	[AttributeLogicalName("ldv_preferredcommunicationlanguage")]
	public OptionSetValue ldv_PreferredCommunicationLanguage
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("ldv_preferredcommunicationlanguage");
		}
		set
		{
			OnPropertyChanging("ldv_PreferredCommunicationLanguage");
			((Entity)this).SetAttributeValue("ldv_preferredcommunicationlanguage", (object)value);
			OnPropertyChanged("ldv_PreferredCommunicationLanguage");
		}
	}

	[AttributeLogicalName("ldv_userrole")]
	public OptionSetValue ldv_userrole
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("ldv_userrole");
		}
		set
		{
			OnPropertyChanging("ldv_userrole");
			((Entity)this).SetAttributeValue("ldv_userrole", (object)value);
			OnPropertyChanged("ldv_userrole");
		}
	}

	[AttributeLogicalName("middlename")]
	public string MiddleName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("middlename");
		}
		set
		{
			OnPropertyChanging("MiddleName");
			((Entity)this).SetAttributeValue("middlename", (object)value);
			OnPropertyChanged("MiddleName");
		}
	}

	[AttributeLogicalName("mlsd_branch")]
	public EntityReference mlsd_branch
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("mlsd_branch");
		}
		set
		{
			OnPropertyChanging("mlsd_branch");
			((Entity)this).SetAttributeValue("mlsd_branch", (object)value);
			OnPropertyChanged("mlsd_branch");
		}
	}

	[AttributeLogicalName("mlsd_order")]
	public int? mlsd_Order
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("mlsd_order");
		}
		set
		{
			OnPropertyChanging("mlsd_Order");
			((Entity)this).SetAttributeValue("mlsd_order", (object)value);
			OnPropertyChanged("mlsd_Order");
		}
	}

	[AttributeLogicalName("mlsd_region")]
	public EntityReference mlsd_region
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("mlsd_region");
		}
		set
		{
			OnPropertyChanging("mlsd_region");
			((Entity)this).SetAttributeValue("mlsd_region", (object)value);
			OnPropertyChanged("mlsd_region");
		}
	}

	[AttributeLogicalName("mobilealertemail")]
	public string MobileAlertEMail
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("mobilealertemail");
		}
		set
		{
			OnPropertyChanging("MobileAlertEMail");
			((Entity)this).SetAttributeValue("mobilealertemail", (object)value);
			OnPropertyChanged("MobileAlertEMail");
		}
	}

	[AttributeLogicalName("mobileofflineprofileid")]
	public EntityReference MobileOfflineProfileId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("mobileofflineprofileid");
		}
		set
		{
			OnPropertyChanging("MobileOfflineProfileId");
			((Entity)this).SetAttributeValue("mobileofflineprofileid", (object)value);
			OnPropertyChanged("MobileOfflineProfileId");
		}
	}

	[AttributeLogicalName("mobilephone")]
	public string MobilePhone
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("mobilephone");
		}
		set
		{
			OnPropertyChanging("MobilePhone");
			((Entity)this).SetAttributeValue("mobilephone", (object)value);
			OnPropertyChanged("MobilePhone");
		}
	}

	[AttributeLogicalName("modifiedby")]
	public EntityReference ModifiedBy => ((Entity)this).GetAttributeValue<EntityReference>("modifiedby");

	[AttributeLogicalName("modifiedon")]
	public DateTime? ModifiedOn => ((Entity)this).GetAttributeValue<DateTime?>("modifiedon");

	[AttributeLogicalName("modifiedonbehalfby")]
	public EntityReference ModifiedOnBehalfBy => ((Entity)this).GetAttributeValue<EntityReference>("modifiedonbehalfby");

	[AttributeLogicalName("nickname")]
	public string NickName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("nickname");
		}
		set
		{
			OnPropertyChanging("NickName");
			((Entity)this).SetAttributeValue("nickname", (object)value);
			OnPropertyChanged("NickName");
		}
	}

	[AttributeLogicalName("organizationid")]
	public Guid? OrganizationId => ((Entity)this).GetAttributeValue<Guid?>("organizationid");

	[AttributeLogicalName("outgoingemaildeliverymethod")]
	public OptionSetValue OutgoingEmailDeliveryMethod
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("outgoingemaildeliverymethod");
		}
		set
		{
			OnPropertyChanging("OutgoingEmailDeliveryMethod");
			((Entity)this).SetAttributeValue("outgoingemaildeliverymethod", (object)value);
			OnPropertyChanged("OutgoingEmailDeliveryMethod");
		}
	}

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

	[AttributeLogicalName("parentsystemuserid")]
	public EntityReference ParentSystemUserId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("parentsystemuserid");
		}
		set
		{
			OnPropertyChanging("ParentSystemUserId");
			((Entity)this).SetAttributeValue("parentsystemuserid", (object)value);
			OnPropertyChanged("ParentSystemUserId");
		}
	}

	[AttributeLogicalName("passporthi")]
	public int? PassportHi
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("passporthi");
		}
		set
		{
			OnPropertyChanging("PassportHi");
			((Entity)this).SetAttributeValue("passporthi", (object)value);
			OnPropertyChanged("PassportHi");
		}
	}

	[AttributeLogicalName("passportlo")]
	public int? PassportLo
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("passportlo");
		}
		set
		{
			OnPropertyChanging("PassportLo");
			((Entity)this).SetAttributeValue("passportlo", (object)value);
			OnPropertyChanged("PassportLo");
		}
	}

	[AttributeLogicalName("personalemailaddress")]
	public string PersonalEMailAddress
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("personalemailaddress");
		}
		set
		{
			OnPropertyChanging("PersonalEMailAddress");
			((Entity)this).SetAttributeValue("personalemailaddress", (object)value);
			OnPropertyChanged("PersonalEMailAddress");
		}
	}

	[AttributeLogicalName("photourl")]
	public string PhotoUrl
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("photourl");
		}
		set
		{
			OnPropertyChanging("PhotoUrl");
			((Entity)this).SetAttributeValue("photourl", (object)value);
			OnPropertyChanged("PhotoUrl");
		}
	}

	[AttributeLogicalName("positionid")]
	public EntityReference PositionId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("positionid");
		}
		set
		{
			OnPropertyChanging("PositionId");
			((Entity)this).SetAttributeValue("positionid", (object)value);
			OnPropertyChanged("PositionId");
		}
	}

	[AttributeLogicalName("preferredaddresscode")]
	public OptionSetValue PreferredAddressCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("preferredaddresscode");
		}
		set
		{
			OnPropertyChanging("PreferredAddressCode");
			((Entity)this).SetAttributeValue("preferredaddresscode", (object)value);
			OnPropertyChanged("PreferredAddressCode");
		}
	}

	[AttributeLogicalName("preferredemailcode")]
	public OptionSetValue PreferredEmailCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("preferredemailcode");
		}
		set
		{
			OnPropertyChanging("PreferredEmailCode");
			((Entity)this).SetAttributeValue("preferredemailcode", (object)value);
			OnPropertyChanged("PreferredEmailCode");
		}
	}

	[AttributeLogicalName("preferredphonecode")]
	public OptionSetValue PreferredPhoneCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("preferredphonecode");
		}
		set
		{
			OnPropertyChanging("PreferredPhoneCode");
			((Entity)this).SetAttributeValue("preferredphonecode", (object)value);
			OnPropertyChanged("PreferredPhoneCode");
		}
	}

	[AttributeLogicalName("processid")]
	public Guid? ProcessId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("processid");
		}
		set
		{
			OnPropertyChanging("ProcessId");
			((Entity)this).SetAttributeValue("processid", (object)value);
			OnPropertyChanged("ProcessId");
		}
	}

	[AttributeLogicalName("queueid")]
	public EntityReference QueueId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("queueid");
		}
		set
		{
			OnPropertyChanging("QueueId");
			((Entity)this).SetAttributeValue("queueid", (object)value);
			OnPropertyChanged("QueueId");
		}
	}

	[AttributeLogicalName("salutation")]
	public string Salutation
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("salutation");
		}
		set
		{
			OnPropertyChanging("Salutation");
			((Entity)this).SetAttributeValue("salutation", (object)value);
			OnPropertyChanged("Salutation");
		}
	}

	[AttributeLogicalName("setupuser")]
	public bool? SetupUser
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("setupuser");
		}
		set
		{
			OnPropertyChanging("SetupUser");
			((Entity)this).SetAttributeValue("setupuser", (object)value);
			OnPropertyChanged("SetupUser");
		}
	}

	[AttributeLogicalName("sharepointemailaddress")]
	public string SharePointEmailAddress
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("sharepointemailaddress");
		}
		set
		{
			OnPropertyChanging("SharePointEmailAddress");
			((Entity)this).SetAttributeValue("sharepointemailaddress", (object)value);
			OnPropertyChanged("SharePointEmailAddress");
		}
	}

	[AttributeLogicalName("siteid")]
	public EntityReference SiteId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("siteid");
		}
		set
		{
			OnPropertyChanging("SiteId");
			((Entity)this).SetAttributeValue("siteid", (object)value);
			OnPropertyChanged("SiteId");
		}
	}

	[AttributeLogicalName("skills")]
	public string Skills
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("skills");
		}
		set
		{
			OnPropertyChanging("Skills");
			((Entity)this).SetAttributeValue("skills", (object)value);
			OnPropertyChanged("Skills");
		}
	}

	[AttributeLogicalName("stageid")]
	public Guid? StageId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("stageid");
		}
		set
		{
			OnPropertyChanging("StageId");
			((Entity)this).SetAttributeValue("stageid", (object)value);
			OnPropertyChanged("StageId");
		}
	}

	[AttributeLogicalName("systemuserid")]
	public Guid? SystemUserId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("systemuserid");
		}
		set
		{
			OnPropertyChanging("SystemUserId");
			((Entity)this).SetAttributeValue("systemuserid", (object)value);
			if (value.HasValue)
			{
				((Entity)this).Id = value.Value;
			}
			else
			{
				((Entity)this).Id = Guid.Empty;
			}
			OnPropertyChanged("SystemUserId");
		}
	}

	[AttributeLogicalName("territoryid")]
	public EntityReference TerritoryId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("territoryid");
		}
		set
		{
			OnPropertyChanging("TerritoryId");
			((Entity)this).SetAttributeValue("territoryid", (object)value);
			OnPropertyChanged("TerritoryId");
		}
	}

	[AttributeLogicalName("timezoneruleversionnumber")]
	public int? TimeZoneRuleVersionNumber
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("timezoneruleversionnumber");
		}
		set
		{
			OnPropertyChanging("TimeZoneRuleVersionNumber");
			((Entity)this).SetAttributeValue("timezoneruleversionnumber", (object)value);
			OnPropertyChanged("TimeZoneRuleVersionNumber");
		}
	}

	[AttributeLogicalName("title")]
	public string Title
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("title");
		}
		set
		{
			OnPropertyChanging("Title");
			((Entity)this).SetAttributeValue("title", (object)value);
			OnPropertyChanged("Title");
		}
	}

	[AttributeLogicalName("transactioncurrencyid")]
	public EntityReference TransactionCurrencyId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("transactioncurrencyid");
		}
		set
		{
			OnPropertyChanging("TransactionCurrencyId");
			((Entity)this).SetAttributeValue("transactioncurrencyid", (object)value);
			OnPropertyChanged("TransactionCurrencyId");
		}
	}

	[AttributeLogicalName("traversedpath")]
	public string TraversedPath
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("traversedpath");
		}
		set
		{
			OnPropertyChanging("TraversedPath");
			((Entity)this).SetAttributeValue("traversedpath", (object)value);
			OnPropertyChanged("TraversedPath");
		}
	}

	[AttributeLogicalName("userlicensetype")]
	public int? UserLicenseType
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("userlicensetype");
		}
		set
		{
			OnPropertyChanging("UserLicenseType");
			((Entity)this).SetAttributeValue("userlicensetype", (object)value);
			OnPropertyChanged("UserLicenseType");
		}
	}

	[AttributeLogicalName("utcconversiontimezonecode")]
	public int? UTCConversionTimeZoneCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("utcconversiontimezonecode");
		}
		set
		{
			OnPropertyChanging("UTCConversionTimeZoneCode");
			((Entity)this).SetAttributeValue("utcconversiontimezonecode", (object)value);
			OnPropertyChanged("UTCConversionTimeZoneCode");
		}
	}

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => ((Entity)this).GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("windowsliveid")]
	public string WindowsLiveID
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("windowsliveid");
		}
		set
		{
			OnPropertyChanging("WindowsLiveID");
			((Entity)this).SetAttributeValue("windowsliveid", (object)value);
			OnPropertyChanged("WindowsLiveID");
		}
	}

	[AttributeLogicalName("yammeremailaddress")]
	public string YammerEmailAddress
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("yammeremailaddress");
		}
		set
		{
			OnPropertyChanging("YammerEmailAddress");
			((Entity)this).SetAttributeValue("yammeremailaddress", (object)value);
			OnPropertyChanged("YammerEmailAddress");
		}
	}

	[AttributeLogicalName("yammeruserid")]
	public string YammerUserId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("yammeruserid");
		}
		set
		{
			OnPropertyChanging("YammerUserId");
			((Entity)this).SetAttributeValue("yammeruserid", (object)value);
			OnPropertyChanged("YammerUserId");
		}
	}

	[AttributeLogicalName("yomifirstname")]
	public string YomiFirstName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("yomifirstname");
		}
		set
		{
			OnPropertyChanging("YomiFirstName");
			((Entity)this).SetAttributeValue("yomifirstname", (object)value);
			OnPropertyChanged("YomiFirstName");
		}
	}

	[AttributeLogicalName("yomifullname")]
	public string YomiFullName => ((Entity)this).GetAttributeValue<string>("yomifullname");

	[AttributeLogicalName("yomilastname")]
	public string YomiLastName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("yomilastname");
		}
		set
		{
			OnPropertyChanging("YomiLastName");
			((Entity)this).SetAttributeValue("yomilastname", (object)value);
			OnPropertyChanged("YomiLastName");
		}
	}

	[AttributeLogicalName("yomimiddlename")]
	public string YomiMiddleName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("yomimiddlename");
		}
		set
		{
			OnPropertyChanging("YomiMiddleName");
			((Entity)this).SetAttributeValue("yomimiddlename", (object)value);
			OnPropertyChanged("YomiMiddleName");
		}
	}

	[RelationshipSchemaName("createdby_pluginassembly")]
	public IEnumerable<PluginAssembly> createdby_pluginassembly
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "createdby_pluginassembly");
				}
				IEnumerable<PluginAssembly> relatedEntities = ((Entity)this).GetRelatedEntities<PluginAssembly>("createdby_pluginassembly", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(PluginAssembly element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<PluginAssembly>("createdby_pluginassembly", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("createdby_pluginassembly");
			((Entity)this).SetRelatedEntities<PluginAssembly>("createdby_pluginassembly", (EntityRole?)null, value);
			OnPropertyChanged("createdby_pluginassembly");
		}
	}

	[RelationshipSchemaName("createdby_sdkmessage")]
	public IEnumerable<SdkMessage> createdby_sdkmessage
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "createdby_sdkmessage");
				}
				IEnumerable<SdkMessage> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessage>("createdby_sdkmessage", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessage element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessage>("createdby_sdkmessage", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("createdby_sdkmessage");
			((Entity)this).SetRelatedEntities<SdkMessage>("createdby_sdkmessage", (EntityRole?)null, value);
			OnPropertyChanged("createdby_sdkmessage");
		}
	}

	[RelationshipSchemaName("createdby_sdkmessagefilter")]
	public IEnumerable<SdkMessageFilter> createdby_sdkmessagefilter
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "createdby_sdkmessagefilter");
				}
				IEnumerable<SdkMessageFilter> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageFilter>("createdby_sdkmessagefilter", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageFilter element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageFilter>("createdby_sdkmessagefilter", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("createdby_sdkmessagefilter");
			((Entity)this).SetRelatedEntities<SdkMessageFilter>("createdby_sdkmessagefilter", (EntityRole?)null, value);
			OnPropertyChanged("createdby_sdkmessagefilter");
		}
	}

	[RelationshipSchemaName("createdby_sdkmessageprocessingstep")]
	public IEnumerable<SdkMessageProcessingStep> createdby_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "createdby_sdkmessageprocessingstep");
				}
				IEnumerable<SdkMessageProcessingStep> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("createdby_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageProcessingStep element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("createdby_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("createdby_sdkmessageprocessingstep");
			((Entity)this).SetRelatedEntities<SdkMessageProcessingStep>("createdby_sdkmessageprocessingstep", (EntityRole?)null, value);
			OnPropertyChanged("createdby_sdkmessageprocessingstep");
		}
	}

	[RelationshipSchemaName("impersonatinguserid_sdkmessageprocessingstep")]
	public IEnumerable<SdkMessageProcessingStep> impersonatinguserid_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "impersonatinguserid_sdkmessageprocessingstep");
				}
				IEnumerable<SdkMessageProcessingStep> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("impersonatinguserid_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageProcessingStep element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("impersonatinguserid_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("impersonatinguserid_sdkmessageprocessingstep");
			((Entity)this).SetRelatedEntities<SdkMessageProcessingStep>("impersonatinguserid_sdkmessageprocessingstep", (EntityRole?)null, value);
			OnPropertyChanged("impersonatinguserid_sdkmessageprocessingstep");
		}
	}

	[RelationshipSchemaName("lk_fieldsecurityprofile_createdby")]
	public IEnumerable<FieldSecurityProfile> lk_fieldsecurityprofile_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldsecurityprofile_createdby");
				}
				IEnumerable<FieldSecurityProfile> relatedEntities = ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_createdby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(FieldSecurityProfile element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_createdby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_fieldsecurityprofile_createdby");
			((Entity)this).SetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_createdby", (EntityRole?)null, value);
			OnPropertyChanged("lk_fieldsecurityprofile_createdby");
		}
	}

	[RelationshipSchemaName("lk_fieldsecurityprofile_createdonbehalfby")]
	public IEnumerable<FieldSecurityProfile> lk_fieldsecurityprofile_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldsecurityprofile_createdonbehalfby");
				}
				IEnumerable<FieldSecurityProfile> relatedEntities = ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_createdonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(FieldSecurityProfile element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_createdonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_fieldsecurityprofile_createdonbehalfby");
			((Entity)this).SetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_createdonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_fieldsecurityprofile_createdonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_fieldsecurityprofile_modifiedby")]
	public IEnumerable<FieldSecurityProfile> lk_fieldsecurityprofile_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldsecurityprofile_modifiedby");
				}
				IEnumerable<FieldSecurityProfile> relatedEntities = ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_modifiedby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(FieldSecurityProfile element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_modifiedby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_fieldsecurityprofile_modifiedby");
			((Entity)this).SetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_modifiedby", (EntityRole?)null, value);
			OnPropertyChanged("lk_fieldsecurityprofile_modifiedby");
		}
	}

	[RelationshipSchemaName("lk_fieldsecurityprofile_modifiedonbehalfby")]
	public IEnumerable<FieldSecurityProfile> lk_fieldsecurityprofile_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_fieldsecurityprofile_modifiedonbehalfby");
				}
				IEnumerable<FieldSecurityProfile> relatedEntities = ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_modifiedonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(FieldSecurityProfile element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_modifiedonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_fieldsecurityprofile_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<FieldSecurityProfile>("lk_fieldsecurityprofile_modifiedonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_fieldsecurityprofile_modifiedonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_pluginassembly_createdonbehalfby")]
	public IEnumerable<PluginAssembly> lk_pluginassembly_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_pluginassembly_createdonbehalfby");
				}
				IEnumerable<PluginAssembly> relatedEntities = ((Entity)this).GetRelatedEntities<PluginAssembly>("lk_pluginassembly_createdonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(PluginAssembly element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<PluginAssembly>("lk_pluginassembly_createdonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_pluginassembly_createdonbehalfby");
			((Entity)this).SetRelatedEntities<PluginAssembly>("lk_pluginassembly_createdonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_pluginassembly_createdonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_pluginassembly_modifiedonbehalfby")]
	public IEnumerable<PluginAssembly> lk_pluginassembly_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_pluginassembly_modifiedonbehalfby");
				}
				IEnumerable<PluginAssembly> relatedEntities = ((Entity)this).GetRelatedEntities<PluginAssembly>("lk_pluginassembly_modifiedonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(PluginAssembly element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<PluginAssembly>("lk_pluginassembly_modifiedonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_pluginassembly_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<PluginAssembly>("lk_pluginassembly_modifiedonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_pluginassembly_modifiedonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_role_createdonbehalfby")]
	public IEnumerable<Role> lk_role_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_role_createdonbehalfby");
				}
				IEnumerable<Role> relatedEntities = ((Entity)this).GetRelatedEntities<Role>("lk_role_createdonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Role element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Role>("lk_role_createdonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_role_createdonbehalfby");
			((Entity)this).SetRelatedEntities<Role>("lk_role_createdonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_role_createdonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_role_modifiedonbehalfby")]
	public IEnumerable<Role> lk_role_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_role_modifiedonbehalfby");
				}
				IEnumerable<Role> relatedEntities = ((Entity)this).GetRelatedEntities<Role>("lk_role_modifiedonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Role element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Role>("lk_role_modifiedonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_role_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<Role>("lk_role_modifiedonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_role_modifiedonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_rolebase_createdby")]
	public IEnumerable<Role> lk_rolebase_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_rolebase_createdby");
				}
				IEnumerable<Role> relatedEntities = ((Entity)this).GetRelatedEntities<Role>("lk_rolebase_createdby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Role element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Role>("lk_rolebase_createdby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_rolebase_createdby");
			((Entity)this).SetRelatedEntities<Role>("lk_rolebase_createdby", (EntityRole?)null, value);
			OnPropertyChanged("lk_rolebase_createdby");
		}
	}

	[RelationshipSchemaName("lk_rolebase_modifiedby")]
	public IEnumerable<Role> lk_rolebase_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_rolebase_modifiedby");
				}
				IEnumerable<Role> relatedEntities = ((Entity)this).GetRelatedEntities<Role>("lk_rolebase_modifiedby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Role element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Role>("lk_rolebase_modifiedby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_rolebase_modifiedby");
			((Entity)this).SetRelatedEntities<Role>("lk_rolebase_modifiedby", (EntityRole?)null, value);
			OnPropertyChanged("lk_rolebase_modifiedby");
		}
	}

	[RelationshipSchemaName("lk_sdkmessage_createdonbehalfby")]
	public IEnumerable<SdkMessage> lk_sdkmessage_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessage_createdonbehalfby");
				}
				IEnumerable<SdkMessage> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessage>("lk_sdkmessage_createdonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessage element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessage>("lk_sdkmessage_createdonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_sdkmessage_createdonbehalfby");
			((Entity)this).SetRelatedEntities<SdkMessage>("lk_sdkmessage_createdonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_sdkmessage_createdonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_sdkmessage_modifiedonbehalfby")]
	public IEnumerable<SdkMessage> lk_sdkmessage_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessage_modifiedonbehalfby");
				}
				IEnumerable<SdkMessage> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessage>("lk_sdkmessage_modifiedonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessage element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessage>("lk_sdkmessage_modifiedonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_sdkmessage_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<SdkMessage>("lk_sdkmessage_modifiedonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_sdkmessage_modifiedonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_sdkmessagefilter_createdonbehalfby")]
	public IEnumerable<SdkMessageFilter> lk_sdkmessagefilter_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessagefilter_createdonbehalfby");
				}
				IEnumerable<SdkMessageFilter> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_createdonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageFilter element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_createdonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_sdkmessagefilter_createdonbehalfby");
			((Entity)this).SetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_createdonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_sdkmessagefilter_createdonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_sdkmessagefilter_modifiedonbehalfby")]
	public IEnumerable<SdkMessageFilter> lk_sdkmessagefilter_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessagefilter_modifiedonbehalfby");
				}
				IEnumerable<SdkMessageFilter> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_modifiedonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageFilter element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_modifiedonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_sdkmessagefilter_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_modifiedonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_sdkmessagefilter_modifiedonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_sdkmessageprocessingstep_createdonbehalfby")]
	public IEnumerable<SdkMessageProcessingStep> lk_sdkmessageprocessingstep_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessageprocessingstep_createdonbehalfby");
				}
				IEnumerable<SdkMessageProcessingStep> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_createdonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageProcessingStep element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_createdonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_sdkmessageprocessingstep_createdonbehalfby");
			((Entity)this).SetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_createdonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_sdkmessageprocessingstep_createdonbehalfby");
		}
	}

	[RelationshipSchemaName("lk_sdkmessageprocessingstep_modifiedonbehalfby")]
	public IEnumerable<SdkMessageProcessingStep> lk_sdkmessageprocessingstep_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_sdkmessageprocessingstep_modifiedonbehalfby");
				}
				IEnumerable<SdkMessageProcessingStep> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_modifiedonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageProcessingStep element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_modifiedonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("lk_sdkmessageprocessingstep_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_modifiedonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("lk_sdkmessageprocessingstep_modifiedonbehalfby");
		}
	}

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<SystemUser> Referenced_lk_systemuser_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_systemuser_createdonbehalfby");
				}
				IEnumerable<SystemUser> relatedEntities = ((Entity)this).GetRelatedEntities<SystemUser>("lk_systemuser_createdonbehalfby", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(SystemUser element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SystemUser>("lk_systemuser_createdonbehalfby", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_lk_systemuser_createdonbehalfby");
			((Entity)this).SetRelatedEntities<SystemUser>("lk_systemuser_createdonbehalfby", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_lk_systemuser_createdonbehalfby");
		}
	}

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<SystemUser> Referenced_lk_systemuser_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_systemuser_modifiedonbehalfby");
				}
				IEnumerable<SystemUser> relatedEntities = ((Entity)this).GetRelatedEntities<SystemUser>("lk_systemuser_modifiedonbehalfby", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(SystemUser element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SystemUser>("lk_systemuser_modifiedonbehalfby", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_lk_systemuser_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<SystemUser>("lk_systemuser_modifiedonbehalfby", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_lk_systemuser_modifiedonbehalfby");
		}
	}

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<SystemUser> Referenced_lk_systemuserbase_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_systemuserbase_createdby");
				}
				IEnumerable<SystemUser> relatedEntities = ((Entity)this).GetRelatedEntities<SystemUser>("lk_systemuserbase_createdby", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(SystemUser element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SystemUser>("lk_systemuserbase_createdby", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_lk_systemuserbase_createdby");
			((Entity)this).SetRelatedEntities<SystemUser>("lk_systemuserbase_createdby", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_lk_systemuserbase_createdby");
		}
	}

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<SystemUser> Referenced_lk_systemuserbase_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_systemuserbase_modifiedby");
				}
				IEnumerable<SystemUser> relatedEntities = ((Entity)this).GetRelatedEntities<SystemUser>("lk_systemuserbase_modifiedby", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(SystemUser element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SystemUser>("lk_systemuserbase_modifiedby", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_lk_systemuserbase_modifiedby");
			((Entity)this).SetRelatedEntities<SystemUser>("lk_systemuserbase_modifiedby", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_lk_systemuserbase_modifiedby");
		}
	}

	[RelationshipSchemaName("modifiedby_pluginassembly")]
	public IEnumerable<PluginAssembly> modifiedby_pluginassembly
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "modifiedby_pluginassembly");
				}
				IEnumerable<PluginAssembly> relatedEntities = ((Entity)this).GetRelatedEntities<PluginAssembly>("modifiedby_pluginassembly", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(PluginAssembly element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<PluginAssembly>("modifiedby_pluginassembly", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("modifiedby_pluginassembly");
			((Entity)this).SetRelatedEntities<PluginAssembly>("modifiedby_pluginassembly", (EntityRole?)null, value);
			OnPropertyChanged("modifiedby_pluginassembly");
		}
	}

	[RelationshipSchemaName("modifiedby_sdkmessage")]
	public IEnumerable<SdkMessage> modifiedby_sdkmessage
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "modifiedby_sdkmessage");
				}
				IEnumerable<SdkMessage> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessage>("modifiedby_sdkmessage", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessage element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessage>("modifiedby_sdkmessage", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("modifiedby_sdkmessage");
			((Entity)this).SetRelatedEntities<SdkMessage>("modifiedby_sdkmessage", (EntityRole?)null, value);
			OnPropertyChanged("modifiedby_sdkmessage");
		}
	}

	[RelationshipSchemaName("modifiedby_sdkmessagefilter")]
	public IEnumerable<SdkMessageFilter> modifiedby_sdkmessagefilter
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "modifiedby_sdkmessagefilter");
				}
				IEnumerable<SdkMessageFilter> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageFilter>("modifiedby_sdkmessagefilter", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageFilter element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageFilter>("modifiedby_sdkmessagefilter", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("modifiedby_sdkmessagefilter");
			((Entity)this).SetRelatedEntities<SdkMessageFilter>("modifiedby_sdkmessagefilter", (EntityRole?)null, value);
			OnPropertyChanged("modifiedby_sdkmessagefilter");
		}
	}

	[RelationshipSchemaName("modifiedby_sdkmessageprocessingstep")]
	public IEnumerable<SdkMessageProcessingStep> modifiedby_sdkmessageprocessingstep
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "modifiedby_sdkmessageprocessingstep");
				}
				IEnumerable<SdkMessageProcessingStep> relatedEntities = ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("modifiedby_sdkmessageprocessingstep", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(SdkMessageProcessingStep element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SdkMessageProcessingStep>("modifiedby_sdkmessageprocessingstep", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("modifiedby_sdkmessageprocessingstep");
			((Entity)this).SetRelatedEntities<SdkMessageProcessingStep>("modifiedby_sdkmessageprocessingstep", (EntityRole?)null, value);
			OnPropertyChanged("modifiedby_sdkmessageprocessingstep");
		}
	}

	[RelationshipSchemaName("system_user_activity_parties")]
	public IEnumerable<ActivityParty> system_user_activity_parties
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "system_user_activity_parties");
				}
				IEnumerable<ActivityParty> relatedEntities = ((Entity)this).GetRelatedEntities<ActivityParty>("system_user_activity_parties", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(ActivityParty element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<ActivityParty>("system_user_activity_parties", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("system_user_activity_parties");
			((Entity)this).SetRelatedEntities<ActivityParty>("system_user_activity_parties", (EntityRole?)null, value);
			OnPropertyChanged("system_user_activity_parties");
		}
	}

	[RelationshipSchemaName("system_user_workflow")]
	public IEnumerable<Workflow> system_user_workflow
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "system_user_workflow");
				}
				IEnumerable<Workflow> relatedEntities = ((Entity)this).GetRelatedEntities<Workflow>("system_user_workflow", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Workflow element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Workflow>("system_user_workflow", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("system_user_workflow");
			((Entity)this).SetRelatedEntities<Workflow>("system_user_workflow", (EntityRole?)null, value);
			OnPropertyChanged("system_user_workflow");
		}
	}

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<SystemUser> Referenced_user_parent_user
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "user_parent_user");
				}
				IEnumerable<SystemUser> relatedEntities = ((Entity)this).GetRelatedEntities<SystemUser>("user_parent_user", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(SystemUser element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<SystemUser>("user_parent_user", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_user_parent_user");
			((Entity)this).SetRelatedEntities<SystemUser>("user_parent_user", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_user_parent_user");
		}
	}

	[RelationshipSchemaName("workflow_createdby")]
	public IEnumerable<Workflow> workflow_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_createdby");
				}
				IEnumerable<Workflow> relatedEntities = ((Entity)this).GetRelatedEntities<Workflow>("workflow_createdby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Workflow element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Workflow>("workflow_createdby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_createdby");
			((Entity)this).SetRelatedEntities<Workflow>("workflow_createdby", (EntityRole?)null, value);
			OnPropertyChanged("workflow_createdby");
		}
	}

	[RelationshipSchemaName("workflow_createdonbehalfby")]
	public IEnumerable<Workflow> workflow_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_createdonbehalfby");
				}
				IEnumerable<Workflow> relatedEntities = ((Entity)this).GetRelatedEntities<Workflow>("workflow_createdonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Workflow element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Workflow>("workflow_createdonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_createdonbehalfby");
			((Entity)this).SetRelatedEntities<Workflow>("workflow_createdonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("workflow_createdonbehalfby");
		}
	}

	[RelationshipSchemaName("workflow_dependency_createdby")]
	public IEnumerable<WorkflowDependency> workflow_dependency_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependency_createdby");
				}
				IEnumerable<WorkflowDependency> relatedEntities = ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependency_createdby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(WorkflowDependency element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependency_createdby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_dependency_createdby");
			((Entity)this).SetRelatedEntities<WorkflowDependency>("workflow_dependency_createdby", (EntityRole?)null, value);
			OnPropertyChanged("workflow_dependency_createdby");
		}
	}

	[RelationshipSchemaName("workflow_dependency_createdonbehalfby")]
	public IEnumerable<WorkflowDependency> workflow_dependency_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependency_createdonbehalfby");
				}
				IEnumerable<WorkflowDependency> relatedEntities = ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependency_createdonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(WorkflowDependency element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependency_createdonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_dependency_createdonbehalfby");
			((Entity)this).SetRelatedEntities<WorkflowDependency>("workflow_dependency_createdonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("workflow_dependency_createdonbehalfby");
		}
	}

	[RelationshipSchemaName("workflow_dependency_modifiedby")]
	public IEnumerable<WorkflowDependency> workflow_dependency_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependency_modifiedby");
				}
				IEnumerable<WorkflowDependency> relatedEntities = ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependency_modifiedby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(WorkflowDependency element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependency_modifiedby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_dependency_modifiedby");
			((Entity)this).SetRelatedEntities<WorkflowDependency>("workflow_dependency_modifiedby", (EntityRole?)null, value);
			OnPropertyChanged("workflow_dependency_modifiedby");
		}
	}

	[RelationshipSchemaName("workflow_dependency_modifiedonbehalfby")]
	public IEnumerable<WorkflowDependency> workflow_dependency_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependency_modifiedonbehalfby");
				}
				IEnumerable<WorkflowDependency> relatedEntities = ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependency_modifiedonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(WorkflowDependency element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependency_modifiedonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_dependency_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<WorkflowDependency>("workflow_dependency_modifiedonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("workflow_dependency_modifiedonbehalfby");
		}
	}

	[RelationshipSchemaName("workflow_modifiedby")]
	public IEnumerable<Workflow> workflow_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_modifiedby");
				}
				IEnumerable<Workflow> relatedEntities = ((Entity)this).GetRelatedEntities<Workflow>("workflow_modifiedby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Workflow element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Workflow>("workflow_modifiedby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_modifiedby");
			((Entity)this).SetRelatedEntities<Workflow>("workflow_modifiedby", (EntityRole?)null, value);
			OnPropertyChanged("workflow_modifiedby");
		}
	}

	[RelationshipSchemaName("workflow_modifiedonbehalfby")]
	public IEnumerable<Workflow> workflow_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_modifiedonbehalfby");
				}
				IEnumerable<Workflow> relatedEntities = ((Entity)this).GetRelatedEntities<Workflow>("workflow_modifiedonbehalfby", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Workflow element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Workflow>("workflow_modifiedonbehalfby", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_modifiedonbehalfby");
			((Entity)this).SetRelatedEntities<Workflow>("workflow_modifiedonbehalfby", (EntityRole?)null, value);
			OnPropertyChanged("workflow_modifiedonbehalfby");
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public SystemUser Referencing_lk_systemuser_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_systemuser_createdonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_systemuser_createdonbehalfby", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_systemuser_createdonbehalfby", (EntityRole?)(EntityRole)0);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public SystemUser Referencing_lk_systemuser_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_systemuser_modifiedonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_systemuser_modifiedonbehalfby", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_systemuser_modifiedonbehalfby", (EntityRole?)(EntityRole)0);
			}
		}
	}

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public SystemUser Referencing_lk_systemuserbase_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_systemuserbase_createdby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_systemuserbase_createdby", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_systemuserbase_createdby", (EntityRole?)(EntityRole)0);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public SystemUser Referencing_lk_systemuserbase_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "lk_systemuserbase_modifiedby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("lk_systemuserbase_modifiedby", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("lk_systemuserbase_modifiedby", (EntityRole?)(EntityRole)0);
			}
		}
	}

	[AttributeLogicalName("parentsystemuserid")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public SystemUser Referencing_user_parent_user
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "user_parent_user");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("user_parent_user", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("user_parent_user", (EntityRole?)(EntityRole)0);
			}
		}
		set
		{
			OnPropertyChanging("Referencing_user_parent_user");
			((Entity)this).SetRelatedEntity<SystemUser>("user_parent_user", (EntityRole?)(EntityRole)0, value);
			OnPropertyChanged("Referencing_user_parent_user");
		}
	}

	[RelationshipSchemaName("systemuserprofiles_association")]
	public IEnumerable<FieldSecurityProfile> systemuserprofiles_association
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "systemuserprofiles_association");
				}
				IEnumerable<FieldSecurityProfile> relatedEntities = ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("systemuserprofiles_association", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(FieldSecurityProfile element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<FieldSecurityProfile>("systemuserprofiles_association", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("systemuserprofiles_association");
			((Entity)this).SetRelatedEntities<FieldSecurityProfile>("systemuserprofiles_association", (EntityRole?)null, value);
			OnPropertyChanged("systemuserprofiles_association");
		}
	}

	[RelationshipSchemaName("systemuserroles_association")]
	public IEnumerable<Role> systemuserroles_association
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "systemuserroles_association");
				}
				IEnumerable<Role> relatedEntities = ((Entity)this).GetRelatedEntities<Role>("systemuserroles_association", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(Role element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Role>("systemuserroles_association", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("systemuserroles_association");
			((Entity)this).SetRelatedEntities<Role>("systemuserroles_association", (EntityRole?)null, value);
			OnPropertyChanged("systemuserroles_association");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public SystemUser()
		: base("systemuser")
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

	public List<PluginAssembly> Load_createdby_pluginassembly(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<PluginAssembly> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "pluginassembly", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<PluginAssembly>();
		createdby_pluginassembly = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessage> Load_createdby_sdkmessage(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessage> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessage", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessage>();
		createdby_sdkmessage = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageFilter> Load_createdby_sdkmessagefilter(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageFilter> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessagefilter", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageFilter>();
		createdby_sdkmessagefilter = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageProcessingStep> Load_createdby_sdkmessageprocessingstep(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageProcessingStep> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessageprocessingstep", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageProcessingStep>();
		createdby_sdkmessageprocessingstep = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageProcessingStep> Load_impersonatinguserid_sdkmessageprocessingstep(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageProcessingStep> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessageprocessingstep", ((Entity)this).LogicalName, "impersonatinguserid", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageProcessingStep>();
		impersonatinguserid_sdkmessageprocessingstep = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<FieldSecurityProfile> Load_lk_fieldsecurityprofile_createdby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<FieldSecurityProfile> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "fieldsecurityprofile", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<FieldSecurityProfile>();
		lk_fieldsecurityprofile_createdby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<FieldSecurityProfile> Load_lk_fieldsecurityprofile_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<FieldSecurityProfile> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "fieldsecurityprofile", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<FieldSecurityProfile>();
		lk_fieldsecurityprofile_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<FieldSecurityProfile> Load_lk_fieldsecurityprofile_modifiedby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<FieldSecurityProfile> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "fieldsecurityprofile", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<FieldSecurityProfile>();
		lk_fieldsecurityprofile_modifiedby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<FieldSecurityProfile> Load_lk_fieldsecurityprofile_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<FieldSecurityProfile> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "fieldsecurityprofile", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<FieldSecurityProfile>();
		lk_fieldsecurityprofile_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<PluginAssembly> Load_lk_pluginassembly_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<PluginAssembly> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "pluginassembly", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<PluginAssembly>();
		lk_pluginassembly_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<PluginAssembly> Load_lk_pluginassembly_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<PluginAssembly> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "pluginassembly", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<PluginAssembly>();
		lk_pluginassembly_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Role> Load_lk_role_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Role> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "role", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Role>();
		lk_role_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Role> Load_lk_role_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Role> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "role", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Role>();
		lk_role_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Role> Load_lk_rolebase_createdby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Role> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "role", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Role>();
		lk_rolebase_createdby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Role> Load_lk_rolebase_modifiedby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Role> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "role", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Role>();
		lk_rolebase_modifiedby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessage> Load_lk_sdkmessage_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessage> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessage", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessage>();
		lk_sdkmessage_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessage> Load_lk_sdkmessage_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessage> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessage", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessage>();
		lk_sdkmessage_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageFilter> Load_lk_sdkmessagefilter_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageFilter> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessagefilter", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageFilter>();
		lk_sdkmessagefilter_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageFilter> Load_lk_sdkmessagefilter_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageFilter> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessagefilter", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageFilter>();
		lk_sdkmessagefilter_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageProcessingStep> Load_lk_sdkmessageprocessingstep_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageProcessingStep> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessageprocessingstep", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageProcessingStep>();
		lk_sdkmessageprocessingstep_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageProcessingStep> Load_lk_sdkmessageprocessingstep_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageProcessingStep> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessageprocessingstep", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageProcessingStep>();
		lk_sdkmessageprocessingstep_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SystemUser> Load_Referenced_lk_systemuser_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SystemUser> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "systemuser", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SystemUser>();
		Referenced_lk_systemuser_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SystemUser> Load_Referenced_lk_systemuser_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SystemUser> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "systemuser", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SystemUser>();
		Referenced_lk_systemuser_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SystemUser> Load_Referenced_lk_systemuserbase_createdby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SystemUser> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "systemuser", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SystemUser>();
		Referenced_lk_systemuserbase_createdby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SystemUser> Load_Referenced_lk_systemuserbase_modifiedby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SystemUser> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "systemuser", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SystemUser>();
		Referenced_lk_systemuserbase_modifiedby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<PluginAssembly> Load_modifiedby_pluginassembly(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<PluginAssembly> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "pluginassembly", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<PluginAssembly>();
		modifiedby_pluginassembly = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessage> Load_modifiedby_sdkmessage(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessage> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessage", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessage>();
		modifiedby_sdkmessage = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageFilter> Load_modifiedby_sdkmessagefilter(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageFilter> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessagefilter", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageFilter>();
		modifiedby_sdkmessagefilter = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SdkMessageProcessingStep> Load_modifiedby_sdkmessageprocessingstep(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SdkMessageProcessingStep> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessageprocessingstep", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SdkMessageProcessingStep>();
		modifiedby_sdkmessageprocessingstep = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<ActivityParty> Load_system_user_activity_parties(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<ActivityParty> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "activityparty", ((Entity)this).LogicalName, "partyid", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<ActivityParty>();
		system_user_activity_parties = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Workflow> Load_system_user_workflow(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Workflow> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflow", ((Entity)this).LogicalName, "owninguser", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Workflow>();
		system_user_workflow = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<SystemUser> Load_Referenced_user_parent_user(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<SystemUser> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "systemuser", ((Entity)this).LogicalName, "parentsystemuserid", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<SystemUser>();
		Referenced_user_parent_user = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Workflow> Load_workflow_createdby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Workflow> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflow", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Workflow>();
		workflow_createdby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Workflow> Load_workflow_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Workflow> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflow", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Workflow>();
		workflow_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<WorkflowDependency> Load_workflow_dependency_createdby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<WorkflowDependency> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflowdependency", ((Entity)this).LogicalName, "createdby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<WorkflowDependency>();
		workflow_dependency_createdby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<WorkflowDependency> Load_workflow_dependency_createdonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<WorkflowDependency> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflowdependency", ((Entity)this).LogicalName, "createdonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<WorkflowDependency>();
		workflow_dependency_createdonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<WorkflowDependency> Load_workflow_dependency_modifiedby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<WorkflowDependency> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflowdependency", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<WorkflowDependency>();
		workflow_dependency_modifiedby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<WorkflowDependency> Load_workflow_dependency_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<WorkflowDependency> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflowdependency", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<WorkflowDependency>();
		workflow_dependency_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Workflow> Load_workflow_modifiedby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Workflow> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflow", ((Entity)this).LogicalName, "modifiedby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Workflow>();
		workflow_modifiedby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Workflow> Load_workflow_modifiedonbehalfby(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Workflow> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflow", ((Entity)this).LogicalName, "modifiedonbehalfby", "systemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes)
			select entity.ToEntity<Workflow>();
		workflow_modifiedonbehalfby = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public SystemUser Load_Referencing_user_parent_user(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "systemuser", ((Entity)this).LogicalName, "systemuserid", "parentsystemuserid", "systemuserid", "systemuserid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return Referencing_user_parent_user = val.ToEntity<SystemUser>();
		}
		return Referencing_user_parent_user = null;
	}

	public SystemUser(object anonymousType)
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
				((DataCollection<string, object>)(object)((Entity)this).Attributes)["systemuserid"] = ((Entity)this).Id;
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
