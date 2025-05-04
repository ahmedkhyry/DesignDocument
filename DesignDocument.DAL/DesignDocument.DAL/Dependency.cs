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
[EntityLogicalName("dependency")]
public class Dependency : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum DependencyType
		{
			[EnumMember]
			None = 0,
			[EnumMember]
			SolutionInternal = 1,
			[EnumMember]
			Published = 2,
			[EnumMember]
			Unpublished = 4
		}

		[DataContract]
		public enum DependentComponentType
		{
			[EnumMember]
			Entity = 1,
			[EnumMember]
			Attribute = 2,
			[EnumMember]
			Relationship = 3,
			[EnumMember]
			AttributePicklistValue = 4,
			[EnumMember]
			AttributeLookupValue = 5,
			[EnumMember]
			ViewAttribute = 6,
			[EnumMember]
			LocalizedLabel = 7,
			[EnumMember]
			RelationshipExtraCondition = 8,
			[EnumMember]
			OptionSet = 9,
			[EnumMember]
			EntityRelationship = 10,
			[EnumMember]
			EntityRelationshipRole = 11,
			[EnumMember]
			EntityRelationshipRelationships = 12,
			[EnumMember]
			ManagedProperty = 13,
			[EnumMember]
			EntityKey = 14,
			[EnumMember]
			Role = 20,
			[EnumMember]
			RolePrivilege = 21,
			[EnumMember]
			DisplayString = 22,
			[EnumMember]
			DisplayStringMap = 23,
			[EnumMember]
			Form = 24,
			[EnumMember]
			Organization = 25,
			[EnumMember]
			SavedQuery = 26,
			[EnumMember]
			Workflow = 29,
			[EnumMember]
			Report = 31,
			[EnumMember]
			ReportEntity = 32,
			[EnumMember]
			ReportCategory = 33,
			[EnumMember]
			ReportVisibility = 34,
			[EnumMember]
			Attachment = 35,
			[EnumMember]
			EmailTemplate = 36,
			[EnumMember]
			ContractTemplate = 37,
			[EnumMember]
			KBArticleTemplate = 38,
			[EnumMember]
			MailMergeTemplate = 39,
			[EnumMember]
			DuplicateRule = 44,
			[EnumMember]
			DuplicateRuleCondition = 45,
			[EnumMember]
			EntityMap = 46,
			[EnumMember]
			AttributeMap = 47,
			[EnumMember]
			RibbonCommand = 48,
			[EnumMember]
			RibbonContextGroup = 49,
			[EnumMember]
			RibbonCustomization = 50,
			[EnumMember]
			RibbonRule = 52,
			[EnumMember]
			RibbonTabToCommandMap = 53,
			[EnumMember]
			RibbonDiff = 55,
			[EnumMember]
			SavedQueryVisualization = 59,
			[EnumMember]
			SystemForm = 60,
			[EnumMember]
			WebResource = 61,
			[EnumMember]
			SiteMap = 62,
			[EnumMember]
			ConnectionRole = 63,
			[EnumMember]
			FieldSecurityProfile = 70,
			[EnumMember]
			FieldPermission = 71,
			[EnumMember]
			PluginType = 90,
			[EnumMember]
			PluginAssembly = 91,
			[EnumMember]
			SDKMessageProcessingStep = 92,
			[EnumMember]
			SDKMessageProcessingStepImage = 93,
			[EnumMember]
			ServiceEndpoint = 95,
			[EnumMember]
			RoutingRule = 150,
			[EnumMember]
			RoutingRuleItem = 151,
			[EnumMember]
			SLA = 152,
			[EnumMember]
			SLAItem = 153,
			[EnumMember]
			ConvertRule = 154,
			[EnumMember]
			ConvertRuleItem = 155,
			[EnumMember]
			HierarchyRule = 65,
			[EnumMember]
			MobileOfflineProfile = 161,
			[EnumMember]
			MobileOfflineProfileItem = 162,
			[EnumMember]
			SimilarityRule = 165,
			[EnumMember]
			CustomControl = 66,
			[EnumMember]
			CustomControlDefaultConfig = 68
		}

		[DataContract]
		public enum RequiredComponentType
		{
			[EnumMember]
			Entity = 1,
			[EnumMember]
			Attribute = 2,
			[EnumMember]
			Relationship = 3,
			[EnumMember]
			AttributePicklistValue = 4,
			[EnumMember]
			AttributeLookupValue = 5,
			[EnumMember]
			ViewAttribute = 6,
			[EnumMember]
			LocalizedLabel = 7,
			[EnumMember]
			RelationshipExtraCondition = 8,
			[EnumMember]
			OptionSet = 9,
			[EnumMember]
			EntityRelationship = 10,
			[EnumMember]
			EntityRelationshipRole = 11,
			[EnumMember]
			EntityRelationshipRelationships = 12,
			[EnumMember]
			ManagedProperty = 13,
			[EnumMember]
			EntityKey = 14,
			[EnumMember]
			Role = 20,
			[EnumMember]
			RolePrivilege = 21,
			[EnumMember]
			DisplayString = 22,
			[EnumMember]
			DisplayStringMap = 23,
			[EnumMember]
			Form = 24,
			[EnumMember]
			Organization = 25,
			[EnumMember]
			SavedQuery = 26,
			[EnumMember]
			Workflow = 29,
			[EnumMember]
			Report = 31,
			[EnumMember]
			ReportEntity = 32,
			[EnumMember]
			ReportCategory = 33,
			[EnumMember]
			ReportVisibility = 34,
			[EnumMember]
			Attachment = 35,
			[EnumMember]
			EmailTemplate = 36,
			[EnumMember]
			ContractTemplate = 37,
			[EnumMember]
			KBArticleTemplate = 38,
			[EnumMember]
			MailMergeTemplate = 39,
			[EnumMember]
			DuplicateRule = 44,
			[EnumMember]
			DuplicateRuleCondition = 45,
			[EnumMember]
			EntityMap = 46,
			[EnumMember]
			AttributeMap = 47,
			[EnumMember]
			RibbonCommand = 48,
			[EnumMember]
			RibbonContextGroup = 49,
			[EnumMember]
			RibbonCustomization = 50,
			[EnumMember]
			RibbonRule = 52,
			[EnumMember]
			RibbonTabToCommandMap = 53,
			[EnumMember]
			RibbonDiff = 55,
			[EnumMember]
			SavedQueryVisualization = 59,
			[EnumMember]
			SystemForm = 60,
			[EnumMember]
			WebResource = 61,
			[EnumMember]
			SiteMap = 62,
			[EnumMember]
			ConnectionRole = 63,
			[EnumMember]
			FieldSecurityProfile = 70,
			[EnumMember]
			FieldPermission = 71,
			[EnumMember]
			PluginType = 90,
			[EnumMember]
			PluginAssembly = 91,
			[EnumMember]
			SDKMessageProcessingStep = 92,
			[EnumMember]
			SDKMessageProcessingStepImage = 93,
			[EnumMember]
			ServiceEndpoint = 95,
			[EnumMember]
			RoutingRule = 150,
			[EnumMember]
			RoutingRuleItem = 151,
			[EnumMember]
			SLA = 152,
			[EnumMember]
			SLAItem = 153,
			[EnumMember]
			ConvertRule = 154,
			[EnumMember]
			ConvertRuleItem = 155,
			[EnumMember]
			HierarchyRule = 65,
			[EnumMember]
			MobileOfflineProfile = 161,
			[EnumMember]
			MobileOfflineProfileItem = 162,
			[EnumMember]
			SimilarityRule = 165,
			[EnumMember]
			CustomControl = 66,
			[EnumMember]
			CustomControlDefaultConfig = 68
		}

		public static class Names
		{
			public const string DependencyType = "dependencytype";

			public const string DependentComponentType = "dependentcomponenttype";

			public const string RequiredComponentType = "requiredcomponenttype";
		}

		public static class Labels
		{
			public static class DependencyType
			{
				public const string None_1033 = "None";

				public const string None_1025 = "بلا";

				public const string SolutionInternal_1033 = "Solution Internal";

				public const string SolutionInternal_1025 = "حل داخلي";

				public const string Published_1033 = "Published";

				public const string Published_1025 = "تم نشره";

				public const string Unpublished_1033 = "Unpublished";

				public const string Unpublished_1025 = "غير منشور";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DependencyType), label, languageCode);
				}
			}

			public static class DependentComponentType
			{
				public const string Entity_1033 = "Entity";

				public const string Entity_1025 = "الكيان";

				public const string Attribute_1033 = "Attribute";

				public const string Attribute_1025 = "السمة";

				public const string Relationship_1033 = "Relationship";

				public const string Relationship_1025 = "العلاقة";

				public const string AttributePicklistValue_1033 = "Attribute Picklist Value";

				public const string AttributePicklistValue_1025 = "قيمة قائمة اختيار السمة";

				public const string AttributeLookupValue_1033 = "Attribute Lookup Value";

				public const string AttributeLookupValue_1025 = "قيمة البحث الخاصة بالسمة";

				public const string ViewAttribute_1033 = "View Attribute";

				public const string ViewAttribute_1025 = "سمة العرض";

				public const string LocalizedLabel_1033 = "Localized Label";

				public const string LocalizedLabel_1025 = "تسمية مترجمة";

				public const string RelationshipExtraCondition_1033 = "Relationship Extra Condition";

				public const string RelationshipExtraCondition_1025 = "شرط إضافي للعلاقة";

				public const string OptionSet_1033 = "Option Set";

				public const string OptionSet_1025 = "مجموعة الخيارات";

				public const string EntityRelationship_1033 = "Entity Relationship";

				public const string EntityRelationship_1025 = "علاقة الكيان";

				public const string EntityRelationshipRole_1033 = "Entity Relationship Role";

				public const string EntityRelationshipRole_1025 = "دور علاقة الكيان";

				public const string EntityRelationshipRelationships_1033 = "Entity Relationship Relationships";

				public const string EntityRelationshipRelationships_1025 = "\u200f\u200fالعلاقات الخاصة بعلاقة الكيان";

				public const string ManagedProperty_1033 = "Managed Property";

				public const string ManagedProperty_1025 = "خاصية مدارة";

				public const string EntityKey_1033 = "Entity Key";

				public const string EntityKey_1025 = "مفتاح الكيان";

				public const string Role_1033 = "Role";

				public const string Role_1025 = "الدور";

				public const string RolePrivilege_1033 = "Role Privilege";

				public const string RolePrivilege_1025 = "امتياز الدور";

				public const string DisplayString_1033 = "Display String";

				public const string DisplayString_1025 = "سلسلة العرض";

				public const string DisplayStringMap_1033 = "Display String Map";

				public const string DisplayStringMap_1025 = "تعيين سلسلة العرض";

				public const string Form_1033 = "Form";

				public const string Form_1025 = "نموذج";

				public const string Organization_1033 = "Organization";

				public const string Organization_1025 = "المؤسسة";

				public const string SavedQuery_1033 = "Saved Query";

				public const string SavedQuery_1025 = "استعلام محفوظ";

				public const string Workflow_1033 = "Workflow";

				public const string Workflow_1025 = "سير العمل";

				public const string Report_1033 = "Report";

				public const string Report_1025 = "\u200f\u200fتقرير";

				public const string ReportEntity_1033 = "Report Entity";

				public const string ReportEntity_1025 = "كيان التقرير";

				public const string ReportCategory_1033 = "Report Category";

				public const string ReportCategory_1025 = "\u200f\u200fفئة التقرير";

				public const string ReportVisibility_1033 = "Report Visibility";

				public const string ReportVisibility_1025 = "إمكانية رؤية التقرير";

				public const string Attachment_1033 = "Attachment";

				public const string Attachment_1025 = "\u200f\u200fمرفق";

				public const string EmailTemplate_1033 = "Email Template";

				public const string EmailTemplate_1025 = "قالب البريد الإلكتروني";

				public const string ContractTemplate_1033 = "Contract Template";

				public const string ContractTemplate_1025 = "قالب عقد";

				public const string KBArticleTemplate_1033 = "KB Article Template";

				public const string KBArticleTemplate_1025 = "\u200f\u200fقالب مقالة قاعدة المعارف";

				public const string MailMergeTemplate_1033 = "Mail Merge Template";

				public const string MailMergeTemplate_1025 = "قالب دمج البريد";

				public const string DuplicateRule_1033 = "Duplicate Rule";

				public const string DuplicateRule_1025 = "قاعدة التكرارات";

				public const string DuplicateRuleCondition_1033 = "Duplicate Rule Condition";

				public const string DuplicateRuleCondition_1025 = "شرط قاعدة التكرارات";

				public const string EntityMap_1033 = "Entity Map";

				public const string EntityMap_1025 = "تعيين الكيان";

				public const string AttributeMap_1033 = "Attribute Map";

				public const string AttributeMap_1025 = "تعيين السمة";

				public const string RibbonCommand_1033 = "Ribbon Command";

				public const string RibbonCommand_1025 = "أمر الشريط";

				public const string RibbonContextGroup_1033 = "Ribbon Context Group";

				public const string RibbonContextGroup_1025 = "مجموعة سياق الشريط";

				public const string RibbonCustomization_1033 = "Ribbon Customization";

				public const string RibbonCustomization_1025 = "تخصيص الشريط";

				public const string RibbonRule_1033 = "Ribbon Rule";

				public const string RibbonRule_1025 = "قاعدة الشريط";

				public const string RibbonTabToCommandMap_1033 = "Ribbon Tab To Command Map";

				public const string RibbonTabToCommandMap_1025 = "علامة تبويب الشريط إلى مخطط الأوامر";

				public const string RibbonDiff_1033 = "Ribbon Diff";

				public const string RibbonDiff_1025 = "اختلاف الشريط";

				public const string SavedQueryVisualization_1033 = "Saved Query Visualization";

				public const string SavedQueryVisualization_1025 = "مؤثرات عرض استعلام محفوظ";

				public const string SystemForm_1033 = "System Form";

				public const string SystemForm_1025 = "نموذج النظام";

				public const string WebResource_1033 = "Web Resource";

				public const string WebResource_1025 = "مورد ويب";

				public const string SiteMap_1033 = "Site Map";

				public const string SiteMap_1025 = "مخطط الموقع";

				public const string ConnectionRole_1033 = "Connection Role";

				public const string ConnectionRole_1025 = "دور الاتصال";

				public const string FieldSecurityProfile_1033 = "Field Security Profile";

				public const string FieldSecurityProfile_1025 = "ملف تعريف أمان الحقل";

				public const string FieldPermission_1033 = "Field Permission";

				public const string FieldPermission_1025 = "إذن الحقل";

				public const string PluginType_1033 = "Plugin Type";

				public const string PluginType_1025 = "نوع المكون الإضافي";

				public const string PluginAssembly_1033 = "Plugin Assembly";

				public const string PluginAssembly_1025 = "تجميع المكون الإضافي";

				public const string SDKMessageProcessingStep_1033 = "SDK Message Processing Step";

				public const string SDKMessageProcessingStep_1025 = "\u200f\u200fخطوة معالجة رسالة Sdk";

				public const string SDKMessageProcessingStepImage_1033 = "SDK Message Processing Step Image";

				public const string SDKMessageProcessingStepImage_1025 = "\u200f\u200fنسخة خطوة معالجة رسالة Sdk";

				public const string ServiceEndpoint_1033 = "Service Endpoint";

				public const string ServiceEndpoint_1025 = "نقطة نهاية الخدمة";

				public const string RoutingRule_1033 = "Routing Rule";

				public const string RoutingRule_1025 = "قاعدة التحويل";

				public const string RoutingRuleItem_1033 = "Routing Rule Item";

				public const string RoutingRuleItem_1025 = "عنصر قاعدة التحويل";

				public const string SLA_1033 = "SLA";

				public const string SLA_1025 = "SLA";

				public const string SLAItem_1033 = "SLA Item";

				public const string SLAItem_1025 = "بند SLA";

				public const string ConvertRule_1033 = "Convert Rule";

				public const string ConvertRule_1025 = "قاعدة التحويل";

				public const string ConvertRuleItem_1033 = "Convert Rule Item";

				public const string ConvertRuleItem_1025 = "عنصر قاعدة التحويل";

				public const string HierarchyRule_1033 = "Hierarchy Rule";

				public const string HierarchyRule_1025 = "قاعدة التدرج الهرمي";

				public const string MobileOfflineProfile_1033 = "Mobile Offline Profile";

				public const string MobileOfflineProfile_1025 = "ملف تعريف Mobile Offline";

				public const string MobileOfflineProfileItem_1033 = "Mobile Offline Profile Item";

				public const string MobileOfflineProfileItem_1025 = "عنصر ملف تعريف Mobile Offline";

				public const string SimilarityRule_1033 = "Similarity Rule";

				public const string SimilarityRule_1025 = "قاعدة التشابه";

				public const string CustomControl_1033 = "Custom Control";

				public const string CustomControl_1025 = "عنصر التحكم المخصص";

				public const string CustomControlDefaultConfig_1033 = "Custom Control Default Config";

				public const string CustomControlDefaultConfig_1025 = "التكوين الافتراضي لعنصر التحكم المخصص";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DependentComponentType), label, languageCode);
				}
			}

			public static class RequiredComponentType
			{
				public const string Entity_1033 = "Entity";

				public const string Entity_1025 = "الكيان";

				public const string Attribute_1033 = "Attribute";

				public const string Attribute_1025 = "السمة";

				public const string Relationship_1033 = "Relationship";

				public const string Relationship_1025 = "العلاقة";

				public const string AttributePicklistValue_1033 = "Attribute Picklist Value";

				public const string AttributePicklistValue_1025 = "قيمة قائمة اختيار السمة";

				public const string AttributeLookupValue_1033 = "Attribute Lookup Value";

				public const string AttributeLookupValue_1025 = "قيمة البحث الخاصة بالسمة";

				public const string ViewAttribute_1033 = "View Attribute";

				public const string ViewAttribute_1025 = "سمة العرض";

				public const string LocalizedLabel_1033 = "Localized Label";

				public const string LocalizedLabel_1025 = "تسمية مترجمة";

				public const string RelationshipExtraCondition_1033 = "Relationship Extra Condition";

				public const string RelationshipExtraCondition_1025 = "شرط إضافي للعلاقة";

				public const string OptionSet_1033 = "Option Set";

				public const string OptionSet_1025 = "مجموعة الخيارات";

				public const string EntityRelationship_1033 = "Entity Relationship";

				public const string EntityRelationship_1025 = "علاقة الكيان";

				public const string EntityRelationshipRole_1033 = "Entity Relationship Role";

				public const string EntityRelationshipRole_1025 = "دور علاقة الكيان";

				public const string EntityRelationshipRelationships_1033 = "Entity Relationship Relationships";

				public const string EntityRelationshipRelationships_1025 = "\u200f\u200fالعلاقات الخاصة بعلاقة الكيان";

				public const string ManagedProperty_1033 = "Managed Property";

				public const string ManagedProperty_1025 = "خاصية مدارة";

				public const string EntityKey_1033 = "Entity Key";

				public const string EntityKey_1025 = "مفتاح الكيان";

				public const string Role_1033 = "Role";

				public const string Role_1025 = "الدور";

				public const string RolePrivilege_1033 = "Role Privilege";

				public const string RolePrivilege_1025 = "امتياز الدور";

				public const string DisplayString_1033 = "Display String";

				public const string DisplayString_1025 = "سلسلة العرض";

				public const string DisplayStringMap_1033 = "Display String Map";

				public const string DisplayStringMap_1025 = "تعيين سلسلة العرض";

				public const string Form_1033 = "Form";

				public const string Form_1025 = "نموذج";

				public const string Organization_1033 = "Organization";

				public const string Organization_1025 = "المؤسسة";

				public const string SavedQuery_1033 = "Saved Query";

				public const string SavedQuery_1025 = "استعلام محفوظ";

				public const string Workflow_1033 = "Workflow";

				public const string Workflow_1025 = "سير العمل";

				public const string Report_1033 = "Report";

				public const string Report_1025 = "\u200f\u200fتقرير";

				public const string ReportEntity_1033 = "Report Entity";

				public const string ReportEntity_1025 = "كيان التقرير";

				public const string ReportCategory_1033 = "Report Category";

				public const string ReportCategory_1025 = "\u200f\u200fفئة التقرير";

				public const string ReportVisibility_1033 = "Report Visibility";

				public const string ReportVisibility_1025 = "إمكانية رؤية التقرير";

				public const string Attachment_1033 = "Attachment";

				public const string Attachment_1025 = "\u200f\u200fمرفق";

				public const string EmailTemplate_1033 = "Email Template";

				public const string EmailTemplate_1025 = "قالب البريد الإلكتروني";

				public const string ContractTemplate_1033 = "Contract Template";

				public const string ContractTemplate_1025 = "قالب عقد";

				public const string KBArticleTemplate_1033 = "KB Article Template";

				public const string KBArticleTemplate_1025 = "\u200f\u200fقالب مقالة قاعدة المعارف";

				public const string MailMergeTemplate_1033 = "Mail Merge Template";

				public const string MailMergeTemplate_1025 = "قالب دمج البريد";

				public const string DuplicateRule_1033 = "Duplicate Rule";

				public const string DuplicateRule_1025 = "قاعدة التكرارات";

				public const string DuplicateRuleCondition_1033 = "Duplicate Rule Condition";

				public const string DuplicateRuleCondition_1025 = "شرط قاعدة التكرارات";

				public const string EntityMap_1033 = "Entity Map";

				public const string EntityMap_1025 = "تعيين الكيان";

				public const string AttributeMap_1033 = "Attribute Map";

				public const string AttributeMap_1025 = "تعيين السمة";

				public const string RibbonCommand_1033 = "Ribbon Command";

				public const string RibbonCommand_1025 = "أمر الشريط";

				public const string RibbonContextGroup_1033 = "Ribbon Context Group";

				public const string RibbonContextGroup_1025 = "مجموعة سياق الشريط";

				public const string RibbonCustomization_1033 = "Ribbon Customization";

				public const string RibbonCustomization_1025 = "تخصيص الشريط";

				public const string RibbonRule_1033 = "Ribbon Rule";

				public const string RibbonRule_1025 = "قاعدة الشريط";

				public const string RibbonTabToCommandMap_1033 = "Ribbon Tab To Command Map";

				public const string RibbonTabToCommandMap_1025 = "علامة تبويب الشريط إلى مخطط الأوامر";

				public const string RibbonDiff_1033 = "Ribbon Diff";

				public const string RibbonDiff_1025 = "اختلاف الشريط";

				public const string SavedQueryVisualization_1033 = "Saved Query Visualization";

				public const string SavedQueryVisualization_1025 = "مؤثرات عرض استعلام محفوظ";

				public const string SystemForm_1033 = "System Form";

				public const string SystemForm_1025 = "نموذج النظام";

				public const string WebResource_1033 = "Web Resource";

				public const string WebResource_1025 = "مورد ويب";

				public const string SiteMap_1033 = "Site Map";

				public const string SiteMap_1025 = "مخطط الموقع";

				public const string ConnectionRole_1033 = "Connection Role";

				public const string ConnectionRole_1025 = "دور الاتصال";

				public const string FieldSecurityProfile_1033 = "Field Security Profile";

				public const string FieldSecurityProfile_1025 = "ملف تعريف أمان الحقل";

				public const string FieldPermission_1033 = "Field Permission";

				public const string FieldPermission_1025 = "إذن الحقل";

				public const string PluginType_1033 = "Plugin Type";

				public const string PluginType_1025 = "نوع المكون الإضافي";

				public const string PluginAssembly_1033 = "Plugin Assembly";

				public const string PluginAssembly_1025 = "تجميع المكون الإضافي";

				public const string SDKMessageProcessingStep_1033 = "SDK Message Processing Step";

				public const string SDKMessageProcessingStep_1025 = "\u200f\u200fخطوة معالجة رسالة Sdk";

				public const string SDKMessageProcessingStepImage_1033 = "SDK Message Processing Step Image";

				public const string SDKMessageProcessingStepImage_1025 = "\u200f\u200fنسخة خطوة معالجة رسالة Sdk";

				public const string ServiceEndpoint_1033 = "Service Endpoint";

				public const string ServiceEndpoint_1025 = "نقطة نهاية الخدمة";

				public const string RoutingRule_1033 = "Routing Rule";

				public const string RoutingRule_1025 = "قاعدة التحويل";

				public const string RoutingRuleItem_1033 = "Routing Rule Item";

				public const string RoutingRuleItem_1025 = "عنصر قاعدة التحويل";

				public const string SLA_1033 = "SLA";

				public const string SLA_1025 = "SLA";

				public const string SLAItem_1033 = "SLA Item";

				public const string SLAItem_1025 = "بند SLA";

				public const string ConvertRule_1033 = "Convert Rule";

				public const string ConvertRule_1025 = "قاعدة التحويل";

				public const string ConvertRuleItem_1033 = "Convert Rule Item";

				public const string ConvertRuleItem_1025 = "عنصر قاعدة التحويل";

				public const string HierarchyRule_1033 = "Hierarchy Rule";

				public const string HierarchyRule_1025 = "قاعدة التدرج الهرمي";

				public const string MobileOfflineProfile_1033 = "Mobile Offline Profile";

				public const string MobileOfflineProfile_1025 = "ملف تعريف Mobile Offline";

				public const string MobileOfflineProfileItem_1033 = "Mobile Offline Profile Item";

				public const string MobileOfflineProfileItem_1025 = "عنصر ملف تعريف Mobile Offline";

				public const string SimilarityRule_1033 = "Similarity Rule";

				public const string SimilarityRule_1025 = "قاعدة التشابه";

				public const string CustomControl_1033 = "Custom Control";

				public const string CustomControl_1025 = "عنصر التحكم المخصص";

				public const string CustomControlDefaultConfig_1033 = "Custom Control Default Config";

				public const string CustomControlDefaultConfig_1025 = "التكوين الافتراضي لعنصر التحكم المخصص";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(RequiredComponentType), label, languageCode);
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
			public const string DependencyId = "DependencyId";

			public const string DependencyType = "DependencyType";

			public const string DependentComponentBaseSolutionId = "DependentComponentBaseSolutionId";

			public const string DependentComponentNodeId = "DependentComponentNodeId";

			public const string DependentComponentNodeIdName = "DependentComponentNodeIdName";

			public const string DependentComponentObjectId = "DependentComponentObjectId";

			public const string DependentComponentParentId = "DependentComponentParentId";

			public const string DependentComponentType = "DependentComponentType";

			public const string RequiredComponentBaseSolutionId = "RequiredComponentBaseSolutionId";

			public const string RequiredComponentIntroducedVersion = "RequiredComponentIntroducedVersion";

			public const string RequiredComponentNodeId = "RequiredComponentNodeId";

			public const string RequiredComponentNodeIdName = "RequiredComponentNodeIdName";

			public const string RequiredComponentObjectId = "RequiredComponentObjectId";

			public const string RequiredComponentParentId = "RequiredComponentParentId";

			public const string RequiredComponentType = "RequiredComponentType";
		}

		public static class Labels
		{
			public static class DependencyId
			{
				public const string _1033 = "Dependency Identifier";

				public const string _1025 = "معر\u0651ف التبعية";
			}

			public static class DependencyType
			{
				public const string _1033 = "Dependency Type";

				public const string _1025 = "نوع التبعية";
			}

			public static class DependentComponentNodeId
			{
				public const string _1033 = "Dependent Component";

				public const string _1025 = "مكون تابع";
			}

			public static class RequiredComponentNodeId
			{
				public const string _1033 = "Required Component";

				public const string _1025 = "المكون المطلوب";
			}
		}

		public const string DependencyId = "dependencyid";

		public const string DependencyType = "dependencytype";

		public const string DependentComponentBaseSolutionId = "dependentcomponentbasesolutionid";

		public const string DependentComponentNodeId = "dependentcomponentnodeid";

		public const string DependentComponentNodeIdName = "dependentcomponentnodeidName";

		public const string DependentComponentObjectId = "dependentcomponentobjectid";

		public const string DependentComponentParentId = "dependentcomponentparentid";

		public const string DependentComponentType = "dependentcomponenttype";

		public const string RequiredComponentBaseSolutionId = "requiredcomponentbasesolutionid";

		public const string RequiredComponentIntroducedVersion = "requiredcomponentintroducedversion";

		public const string RequiredComponentNodeId = "requiredcomponentnodeid";

		public const string RequiredComponentNodeIdName = "requiredcomponentnodeidName";

		public const string RequiredComponentObjectId = "requiredcomponentobjectid";

		public const string RequiredComponentParentId = "requiredcomponentparentid";

		public const string RequiredComponentType = "requiredcomponenttype";
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
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Dependency";

	public const string SchemaName = "Dependency";

	public const string EntityLogicalName = "dependency";

	public const int EntityTypeCode = 7105;

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

	[AttributeLogicalName("dependencyid")]
	public override Guid Id
	{
		get
		{
			return ((Entity)this).Id;
		}
		set
		{
			((Entity)this).Id = value;
		}
	}

	[AttributeLogicalName("dependencyid")]
	public Guid? DependencyId => ((Entity)this).GetAttributeValue<Guid?>("dependencyid");

	[AttributeLogicalName("dependencytype")]
	public OptionSetValue DependencyType => ((Entity)this).GetAttributeValue<OptionSetValue>("dependencytype");

	[AttributeLogicalName("dependentcomponentbasesolutionid")]
	public Guid? DependentComponentBaseSolutionId => ((Entity)this).GetAttributeValue<Guid?>("dependentcomponentbasesolutionid");

	[AttributeLogicalName("dependentcomponentnodeid")]
	public EntityReference DependentComponentNodeId => ((Entity)this).GetAttributeValue<EntityReference>("dependentcomponentnodeid");

	[AttributeLogicalName("dependentcomponentobjectid")]
	public Guid? DependentComponentObjectId => ((Entity)this).GetAttributeValue<Guid?>("dependentcomponentobjectid");

	[AttributeLogicalName("dependentcomponentparentid")]
	public Guid? DependentComponentParentId => ((Entity)this).GetAttributeValue<Guid?>("dependentcomponentparentid");

	[AttributeLogicalName("dependentcomponenttype")]
	public OptionSetValue DependentComponentType => ((Entity)this).GetAttributeValue<OptionSetValue>("dependentcomponenttype");

	[AttributeLogicalName("requiredcomponentbasesolutionid")]
	public Guid? RequiredComponentBaseSolutionId => ((Entity)this).GetAttributeValue<Guid?>("requiredcomponentbasesolutionid");

	[AttributeLogicalName("requiredcomponentintroducedversion")]
	public double? RequiredComponentIntroducedVersion => ((Entity)this).GetAttributeValue<double?>("requiredcomponentintroducedversion");

	[AttributeLogicalName("requiredcomponentnodeid")]
	public EntityReference RequiredComponentNodeId => ((Entity)this).GetAttributeValue<EntityReference>("requiredcomponentnodeid");

	[AttributeLogicalName("requiredcomponentobjectid")]
	public Guid? RequiredComponentObjectId => ((Entity)this).GetAttributeValue<Guid?>("requiredcomponentobjectid");

	[AttributeLogicalName("requiredcomponentparentid")]
	public Guid? RequiredComponentParentId => ((Entity)this).GetAttributeValue<Guid?>("requiredcomponentparentid");

	[AttributeLogicalName("requiredcomponenttype")]
	public OptionSetValue RequiredComponentType => ((Entity)this).GetAttributeValue<OptionSetValue>("requiredcomponenttype");

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public Dependency()
		: base("dependency")
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

	public Dependency(object anonymousType)
		: this()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		PropertyInfo[] properties = anonymousType.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			object value = propertyInfo.GetValue(anonymousType, null);
			if (propertyInfo.PropertyType == typeof(Guid))
			{
				((Entity)this).Id = (Guid)value;
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
