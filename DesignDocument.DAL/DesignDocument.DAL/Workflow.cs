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
[EntityLogicalName("workflow")]
public class Workflow : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum AsyncAutoDelete
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum BusinessProcessType
		{
			[EnumMember]
			BusinessFlow,
			[EnumMember]
			TaskFlow
		}

		[DataContract]
		public enum Category
		{
			[EnumMember]
			Workflow,
			[EnumMember]
			Dialog,
			[EnumMember]
			BusinessRule,
			[EnumMember]
			Action,
			[EnumMember]
			BusinessProcessFlow
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
		public enum CreateStage
		{
			[EnumMember]
			Preoperation = 20,
			[EnumMember]
			Postoperation = 40
		}

		[DataContract]
		public enum DeleteStage
		{
			[EnumMember]
			Preoperation = 20,
			[EnumMember]
			Postoperation = 40
		}

		[DataContract]
		public enum IsCrmUIWorkflow
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
		public enum IsTransacted
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum Mode
		{
			[EnumMember]
			Background,
			[EnumMember]
			Realtime
		}

		[DataContract]
		public enum OnDemand
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum PrimaryEntity
		{

		}

		[DataContract]
		public enum RendererObjectTypeCode
		{

		}

		[DataContract]
		public enum RunAs
		{
			[EnumMember]
			Owner,
			[EnumMember]
			CallingUser
		}

		[DataContract]
		public enum Scope
		{
			[EnumMember]
			User = 1,
			[EnumMember]
			BusinessUnit,
			[EnumMember]
			ParentChildBusinessUnits,
			[EnumMember]
			Organization
		}

		[DataContract]
		public enum StateCode
		{
			[EnumMember]
			Draft,
			[EnumMember]
			Activated
		}

		[DataContract]
		public enum StatusCode
		{
			[EnumMember]
			Draft = 1,
			[EnumMember]
			Activated
		}

		[DataContract]
		public enum Subprocess
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum SyncWorkflowLogOnFailure
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum TriggerOnCreate
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum TriggerOnDelete
		{
			[EnumMember]
			Yes = 1,
			[EnumMember]
			No = 0
		}

		[DataContract]
		public enum Type
		{
			[EnumMember]
			Definition = 1,
			[EnumMember]
			Activation,
			[EnumMember]
			Template
		}

		[DataContract]
		public enum UpdateStage
		{
			[EnumMember]
			Preoperation = 20,
			[EnumMember]
			Postoperation = 40
		}

		public static class Names
		{
			public const string AsyncAutoDelete = "asyncautodelete";

			public const string BusinessProcessType = "businessprocesstype";

			public const string Category = "category";

			public const string ComponentState = "componentstate";

			public const string CreateStage = "createstage";

			public const string DeleteStage = "deletestage";

			public const string IsCrmUIWorkflow = "iscrmuiworkflow";

			public const string IsManaged = "ismanaged";

			public const string IsTransacted = "istransacted";

			public const string Mode = "mode";

			public const string OnDemand = "ondemand";

			public const string PrimaryEntity = "primaryentity";

			public const string RendererObjectTypeCode = "rendererobjecttypecode";

			public const string RunAs = "runas";

			public const string Scope = "scope";

			public const string StateCode = "statecode";

			public const string StatusCode = "statuscode";

			public const string Subprocess = "subprocess";

			public const string SyncWorkflowLogOnFailure = "syncworkflowlogonfailure";

			public const string TriggerOnCreate = "triggeroncreate";

			public const string TriggerOnDelete = "triggerondelete";

			public const string Type = "type";

			public const string UpdateStage = "updatestage";
		}

		public static class Labels
		{
			public static class AsyncAutoDelete
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "\u200f\u200fنعم";

				public const string No_1033 = "No";

				public const string No_1025 = "\u200f\u200fلا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(AsyncAutoDelete), label, languageCode);
				}
			}

			public static class BusinessProcessType
			{
				public const string BusinessFlow_1033 = "Business Flow";

				public const string BusinessFlow_1025 = "سير العمل";

				public const string TaskFlow_1033 = "Task Flow";

				public const string TaskFlow_1025 = "سير المهام";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(BusinessProcessType), label, languageCode);
				}
			}

			public static class Category
			{
				public const string Workflow_1033 = "Workflow";

				public const string Workflow_1025 = "\u200f\u200fسير العمل";

				public const string Dialog_1033 = "Dialog";

				public const string Dialog_1025 = "مربع الحوار";

				public const string BusinessRule_1033 = "Business Rule";

				public const string BusinessRule_1025 = "قاعدة العمل";

				public const string Action_1033 = "Action";

				public const string Action_1025 = "الإجراء";

				public const string BusinessProcessFlow_1033 = "Business Process Flow";

				public const string BusinessProcessFlow_1025 = "سير إجراءات العمل";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Category), label, languageCode);
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

			public static class CreateStage
			{
				public const string Preoperation_1033 = "Pre-operation";

				public const string Preoperation_1025 = "عملية مسبقة";

				public const string Postoperation_1033 = "Post-operation";

				public const string Postoperation_1025 = "ما بعد التشغيل";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(CreateStage), label, languageCode);
				}
			}

			public static class DeleteStage
			{
				public const string Preoperation_1033 = "Pre-operation";

				public const string Preoperation_1025 = "عملية مسبقة";

				public const string Postoperation_1033 = "Post-operation";

				public const string Postoperation_1025 = "ما بعد التشغيل";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(DeleteStage), label, languageCode);
				}
			}

			public static class IsCrmUIWorkflow
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsCrmUIWorkflow), label, languageCode);
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

			public static class IsTransacted
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(IsTransacted), label, languageCode);
				}
			}

			public static class Mode
			{
				public const string Background_1033 = "Background";

				public const string Background_1025 = "خلفية";

				public const string Realtime_1033 = "Real-time";

				public const string Realtime_1025 = "الوقت الحقيقي";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Mode), label, languageCode);
				}
			}

			public static class OnDemand
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(OnDemand), label, languageCode);
				}
			}

			public static class PrimaryEntity
			{
				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(PrimaryEntity), label, languageCode);
				}
			}

			public static class RendererObjectTypeCode
			{
				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(RendererObjectTypeCode), label, languageCode);
				}
			}

			public static class RunAs
			{
				public const string Owner_1033 = "Owner";

				public const string Owner_1025 = "المالك";

				public const string CallingUser_1033 = "Calling User";

				public const string CallingUser_1025 = "المستخدم المستدعي";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(RunAs), label, languageCode);
				}
			}

			public static class Scope
			{
				public const string User_1033 = "User";

				public const string User_1025 = "المستخدم";

				public const string BusinessUnit_1033 = "Business Unit";

				public const string BusinessUnit_1025 = "وحدة أعمال";

				public const string ParentChildBusinessUnits_1033 = "Parent: Child Business Units";

				public const string ParentChildBusinessUnits_1025 = "الأصل: وحدات أعمال تابعة";

				public const string Organization_1033 = "Organization";

				public const string Organization_1025 = "المؤسسة";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Scope), label, languageCode);
				}
			}

			public static class StateCode
			{
				public const string Draft_1033 = "Draft";

				public const string Draft_1025 = "مسودة";

				public const string Activated_1033 = "Activated";

				public const string Activated_1025 = "منش\u0651\u064eط";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(StateCode), label, languageCode);
				}
			}

			public static class StatusCode
			{
				public const string Draft_1033 = "Draft";

				public const string Draft_1025 = "مسودة";

				public const string Activated_1033 = "Activated";

				public const string Activated_1025 = "منش\u0651\u064eط";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(StatusCode), label, languageCode);
				}
			}

			public static class Subprocess
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Subprocess), label, languageCode);
				}
			}

			public static class SyncWorkflowLogOnFailure
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(SyncWorkflowLogOnFailure), label, languageCode);
				}
			}

			public static class TriggerOnCreate
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(TriggerOnCreate), label, languageCode);
				}
			}

			public static class TriggerOnDelete
			{
				public const string Yes_1033 = "Yes";

				public const string Yes_1025 = "نعم";

				public const string No_1033 = "No";

				public const string No_1025 = "لا";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(TriggerOnDelete), label, languageCode);
				}
			}

			public static class Type
			{
				public const string Definition_1033 = "Definition";

				public const string Definition_1025 = "تعريف";

				public const string Activation_1033 = "Activation";

				public const string Activation_1025 = "تنشيط";

				public const string Template_1033 = "Template";

				public const string Template_1025 = "القالب";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(Type), label, languageCode);
				}
			}

			public static class UpdateStage
			{
				public const string Preoperation_1033 = "Pre-operation";

				public const string Preoperation_1025 = "عملية مسبقة";

				public const string Postoperation_1033 = "Post-operation";

				public const string Postoperation_1025 = "ما بعد التشغيل";

				public static int GetValue(string label, int languageCode = 1033)
				{
					return CrmHelpers.GetValue(typeof(UpdateStage), label, languageCode);
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
			public const string ActiveWorkflowId = "ActiveWorkflowId";

			public const string ActiveWorkflowIdName = "ActiveWorkflowIdName";

			public const string AsyncAutoDelete = "AsyncAutoDelete";

			public const string BusinessProcessType = "BusinessProcessType";

			public const string Category = "Category";

			public const string ClientData = "ClientData";

			public const string ComponentState = "ComponentState";

			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string CreateStage = "CreateStage";

			public const string DeleteStage = "DeleteStage";

			public const string Description = "Description";

			public const string EntityImage = "EntityImage";

			public const string EntityImage_Timestamp = "EntityImage_Timestamp";

			public const string EntityImage_URL = "EntityImage_URL";

			public const string EntityImageId = "EntityImageId";

			public const string FormId = "FormId";

			public const string InputParameters = "InputParameters";

			public const string IntroducedVersion = "IntroducedVersion";

			public const string IsCrmUIWorkflow = "IsCrmUIWorkflow";

			public const string IsCustomizable = "IsCustomizable";

			public const string IsManaged = "IsManaged";

			public const string IsTransacted = "IsTransacted";

			public const string LanguageCode = "LanguageCode";

			public const string Mode = "Mode";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string Name = "Name";

			public const string OnDemand = "OnDemand";

			public const string OverwriteTime = "OverwriteTime";

			public const string OwnerId = "OwnerId";

			public const string OwningBusinessUnit = "OwningBusinessUnit";

			public const string OwningBusinessUnitName = "OwningBusinessUnitName";

			public const string OwningTeam = "OwningTeam";

			public const string OwningTeamName = "OwningTeamName";

			public const string OwningUser = "OwningUser";

			public const string OwningUserName = "OwningUserName";

			public const string ParentWorkflowId = "ParentWorkflowId";

			public const string ParentWorkflowIdName = "ParentWorkflowIdName";

			public const string PluginTypeId = "PluginTypeId";

			public const string PluginTypeIdName = "PluginTypeIdName";

			public const string PrimaryEntity = "PrimaryEntity";

			public const string ProcessOrder = "ProcessOrder";

			public const string ProcessRoleAssignment = "ProcessRoleAssignment";

			public const string Rank = "Rank";

			public const string RendererObjectTypeCode = "RendererObjectTypeCode";

			public const string RunAs = "RunAs";

			public const string Scope = "Scope";

			public const string SdkMessageId = "SdkMessageId";

			public const string SdkMessageIdName = "SdkMessageIdName";

			public const string SolutionId = "SolutionId";

			public const string StateCode = "StateCode";

			public const string StatusCode = "StatusCode";

			public const string Subprocess = "Subprocess";

			public const string SupportingSolutionId = "SupportingSolutionId";

			public const string SyncWorkflowLogOnFailure = "SyncWorkflowLogOnFailure";

			public const string TriggerOnCreate = "TriggerOnCreate";

			public const string TriggerOnDelete = "TriggerOnDelete";

			public const string TriggerOnUpdateAttributeList = "TriggerOnUpdateAttributeList";

			public const string Type = "Type";

			public const string UniqueName = "UniqueName";

			public const string UpdateStage = "UpdateStage";

			public const string VersionNumber = "VersionNumber";

			public const string WorkflowId = "WorkflowId";

			public const string WorkflowIdUnique = "WorkflowIdUnique";

			public const string Xaml = "Xaml";
		}

		public static class Labels
		{
			public static class ActiveWorkflowId
			{
				public const string _1033 = "Active Process ID";

				public const string _1025 = "معر\u0651ف العملية النشطة";
			}

			public static class AsyncAutoDelete
			{
				public const string _1033 = "Delete Job On Completion";

				public const string _1025 = "حذف المهمة عند اكتمالها";
			}

			public static class BusinessProcessType
			{
				public const string _1033 = "Business Process Type";

				public const string _1025 = "نوع إجراءات العمل";
			}

			public static class Category
			{
				public const string _1033 = "Category";

				public const string _1025 = "التصنيف";
			}

			public static class ClientData
			{
				public const string _1033 = "Client Data";

				public const string _1025 = "بيانات العميل";
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
				public const string _1033 = "Created By (Delegate)";

				public const string _1025 = "قام بإنشائه (المفو\u0651ض)";
			}

			public static class CreateStage
			{
				public const string _1033 = "Create Stage";

				public const string _1025 = "مرحلة إنشاء";
			}

			public static class DeleteStage
			{
				public const string _1033 = "Delete stage";

				public const string _1025 = "مرحلة حذف";
			}

			public static class Description
			{
				public const string _1033 = "Description";

				public const string _1025 = "الوصف";
			}

			public static class EntityImageId
			{
				public const string _1033 = "Entity Image Id";

				public const string _1025 = "معر\u0651\u0650ف صورة الكيان";
			}

			public static class FormId
			{
				public const string _1033 = "Form ID";

				public const string _1025 = "معرف النموذج";
			}

			public static class InputParameters
			{
				public const string _1033 = "Input Parameters";

				public const string _1025 = "معلمات الإدخال";
			}

			public static class IntroducedVersion
			{
				public const string _1033 = "Introduced Version";

				public const string _1025 = "إصدار م\u064fقد\u0651\u064eم";
			}

			public static class IsCrmUIWorkflow
			{
				public const string _1033 = "Is CRM Process";

				public const string _1025 = "عملية CRM";
			}

			public static class IsCustomizable
			{
				public const string _1033 = "Customizable";

				public const string _1025 = "قابل للتخصيص";
			}

			public static class IsManaged
			{
				public const string _1033 = "Is Managed";

				public const string _1025 = "هو م\u064fدار";
			}

			public static class IsTransacted
			{
				public const string _1033 = "Is Transacted";

				public const string _1025 = "تمت معالجتها";
			}

			public static class LanguageCode
			{
				public const string _1033 = "Language";

				public const string _1025 = "اللغة";
			}

			public static class Mode
			{
				public const string _1033 = "Mode";

				public const string _1025 = "وضع";
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
				public const string _1033 = "Process Name";

				public const string _1025 = "اسم العملية";
			}

			public static class OnDemand
			{
				public const string _1033 = "Run as On Demand";

				public const string _1025 = "تشغيل بأسلوب عند الطلب";
			}

			public static class OverwriteTime
			{
				public const string _1033 = "Record Overwrite Time";

				public const string _1025 = "وقت الكتابة فوق السجل";
			}

			public static class OwnerId
			{
				public const string _1033 = "Owner";

				public const string _1025 = "المالك";
			}

			public static class OwningBusinessUnit
			{
				public const string _1033 = "Owning Business Unit";

				public const string _1025 = "وحدة الأعمال المالكة";
			}

			public static class OwningTeam
			{
				public const string _1033 = "Owning Team";

				public const string _1025 = "الفريق المالك";
			}

			public static class OwningUser
			{
				public const string _1033 = "Owning User";

				public const string _1025 = "المستخدم المالك";
			}

			public static class ParentWorkflowId
			{
				public const string _1033 = "Parent Process ID";

				public const string _1025 = "معر\u0651ف العملية الأصل";
			}

			public static class PrimaryEntity
			{
				public const string _1033 = "Primary Entity";

				public const string _1025 = "الكيان الأساسي";
			}

			public static class ProcessOrder
			{
				public const string _1033 = "Process Order";

				public const string _1025 = "أمر العملية";
			}

			public static class ProcessRoleAssignment
			{
				public const string _1033 = "Role assignment for Process";

				public const string _1025 = "تعيين دور للعملية";
			}

			public static class Rank
			{
				public const string _1033 = "Rank";

				public const string _1025 = "رتبة";
			}

			public static class RendererObjectTypeCode
			{
				public const string _1033 = "Renderer Type";

				public const string _1025 = "نوع المعالج";
			}

			public static class RunAs
			{
				public const string _1033 = "Run As User";

				public const string _1025 = "التشغيل كمستخدم";
			}

			public static class Scope
			{
				public const string _1033 = "Scope";

				public const string _1025 = "النطاق";
			}

			public static class SdkMessageId
			{
				public const string _1033 = "SDK Message";

				public const string _1025 = "رسالة Sdk";
			}

			public static class SolutionId
			{
				public const string _1033 = "Solution";

				public const string _1025 = "الحل";
			}

			public static class StateCode
			{
				public const string _1033 = "Status";

				public const string _1025 = "الحالة";
			}

			public static class StatusCode
			{
				public const string _1033 = "Status Reason";

				public const string _1025 = "سبب الحالة";
			}

			public static class Subprocess
			{
				public const string _1033 = "Is Child Process";

				public const string _1025 = "عملية فرعية";
			}

			public static class SupportingSolutionId
			{
				public const string _1033 = "Solution";

				public const string _1025 = "الحل";
			}

			public static class SyncWorkflowLogOnFailure
			{
				public const string _1033 = "Log upon Failure";

				public const string _1025 = "التسجيل في حالة الفشل";
			}

			public static class TriggerOnCreate
			{
				public const string _1033 = "Trigger On Create";

				public const string _1025 = "تشغيل عند الإنشاء";
			}

			public static class TriggerOnDelete
			{
				public const string _1033 = "Trigger On Delete";

				public const string _1025 = "تشغيل عند الحذف";
			}

			public static class TriggerOnUpdateAttributeList
			{
				public const string _1033 = "Trigger On Update Attribute List";

				public const string _1025 = "تشغيل عند تحديث قائمة السمات";
			}

			public static class Type
			{
				public const string _1033 = "Type";

				public const string _1025 = "النوع";
			}

			public static class UniqueName
			{
				public const string _1033 = "Unique Name";

				public const string _1025 = "الاسم الفريد";
			}

			public static class UpdateStage
			{
				public const string _1033 = "Update Stage";

				public const string _1025 = "مرحلة تحديث";
			}

			public static class WorkflowId
			{
				public const string _1033 = "Process";

				public const string _1025 = "\u200f\u200fالعملية";
			}
		}

		public const string ActiveWorkflowId = "activeworkflowid";

		public const string ActiveWorkflowIdName = "activeworkflowidName";

		public const string AsyncAutoDelete = "asyncautodelete";

		public const string BusinessProcessType = "businessprocesstype";

		public const string Category = "category";

		public const string ClientData = "clientdata";

		public const string ComponentState = "componentstate";

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string CreateStage = "createstage";

		public const string DeleteStage = "deletestage";

		public const string Description = "description";

		public const string EntityImage = "entityimage";

		public const string EntityImage_Timestamp = "entityimage_timestamp";

		public const string EntityImage_URL = "entityimage_url";

		public const string EntityImageId = "entityimageid";

		public const string FormId = "formid";

		public const string InputParameters = "inputparameters";

		public const string IntroducedVersion = "introducedversion";

		public const string IsCrmUIWorkflow = "iscrmuiworkflow";

		public const string IsCustomizable = "iscustomizable";

		public const string IsManaged = "ismanaged";

		public const string IsTransacted = "istransacted";

		public const string LanguageCode = "languagecode";

		public const string Mode = "mode";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string Name = "name";

		public const string OnDemand = "ondemand";

		public const string OverwriteTime = "overwritetime";

		public const string OwnerId = "ownerid";

		public const string OwningBusinessUnit = "owningbusinessunit";

		public const string OwningBusinessUnitName = "owningbusinessunitName";

		public const string OwningTeam = "owningteam";

		public const string OwningTeamName = "owningteamName";

		public const string OwningUser = "owninguser";

		public const string OwningUserName = "owninguserName";

		public const string ParentWorkflowId = "parentworkflowid";

		public const string ParentWorkflowIdName = "parentworkflowidName";

		public const string PluginTypeId = "plugintypeid";

		public const string PluginTypeIdName = "plugintypeidName";

		public const string PrimaryEntity = "primaryentity";

		public const string ProcessOrder = "processorder";

		public const string ProcessRoleAssignment = "processroleassignment";

		public const string Rank = "rank";

		public const string RendererObjectTypeCode = "rendererobjecttypecode";

		public const string RunAs = "runas";

		public const string Scope = "scope";

		public const string SdkMessageId = "sdkmessageid";

		public const string SdkMessageIdName = "sdkmessageidName";

		public const string SolutionId = "solutionid";

		public const string StateCode = "statecode";

		public const string StatusCode = "statuscode";

		public const string Subprocess = "subprocess";

		public const string SupportingSolutionId = "supportingsolutionid";

		public const string SyncWorkflowLogOnFailure = "syncworkflowlogonfailure";

		public const string TriggerOnCreate = "triggeroncreate";

		public const string TriggerOnDelete = "triggerondelete";

		public const string TriggerOnUpdateAttributeList = "triggeronupdateattributelist";

		public const string Type = "type";

		public const string UniqueName = "uniquename";

		public const string UpdateStage = "updatestage";

		public const string VersionNumber = "versionnumber";

		public const string WorkflowId = "workflowid";

		public const string WorkflowIdUnique = "workflowidunique";

		public const string Xaml = "xaml";
	}

	public static class Relations
	{
		public static class OneToN
		{
			public const string workflow_active_workflow = "workflow_active_workflow";

			public const string workflow_dependencies = "workflow_dependencies";

			public const string workflow_parent_workflow = "workflow_parent_workflow";
		}

		public static class NToOne
		{
			public static class Lookups
			{
				public const string system_user_workflow = "owninguser";

				public const string workflow_active_workflow = "activeworkflowid";

				public const string workflow_createdby = "createdby";

				public const string workflow_createdonbehalfby = "createdonbehalfby";

				public const string workflow_modifiedby = "modifiedby";

				public const string workflow_modifiedonbehalfby = "modifiedonbehalfby";

				public const string workflow_parent_workflow = "parentworkflowid";
			}

			public const string system_user_workflow = "system_user_workflow";

			public const string workflow_active_workflow = "workflow_active_workflow";

			public const string workflow_createdby = "workflow_createdby";

			public const string workflow_createdonbehalfby = "workflow_createdonbehalfby";

			public const string workflow_modifiedby = "workflow_modifiedby";

			public const string workflow_modifiedonbehalfby = "workflow_modifiedonbehalfby";

			public const string workflow_parent_workflow = "workflow_parent_workflow";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public enum WorkflowCategory
	{
		Workflow,
		Dialog,
		BusinessRule,
		Action,
		BusinessProcessFlow
	}

	public const string DisplayName = "Process";

	public const string SchemaName = "Workflow";

	public const string EntityLogicalName = "workflow";

	public const int EntityTypeCode = 4703;

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

	[AttributeLogicalName("workflowid")]
	public override Guid Id
	{
		get
		{
			return ((Entity)this).Id;
		}
		set
		{
			WorkflowId = value;
		}
	}

	[AttributeLogicalName("activeworkflowid")]
	public EntityReference ActiveWorkflowId => ((Entity)this).GetAttributeValue<EntityReference>("activeworkflowid");

	[AttributeLogicalName("asyncautodelete")]
	public bool? AsyncAutoDelete
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("asyncautodelete");
		}
		set
		{
			OnPropertyChanging("AsyncAutoDelete");
			((Entity)this).SetAttributeValue("asyncautodelete", (object)value);
			OnPropertyChanged("AsyncAutoDelete");
		}
	}

	[AttributeLogicalName("businessprocesstype")]
	public OptionSetValue BusinessProcessType
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("businessprocesstype");
		}
		set
		{
			OnPropertyChanging("BusinessProcessType");
			((Entity)this).SetAttributeValue("businessprocesstype", (object)value);
			OnPropertyChanged("BusinessProcessType");
		}
	}

	[AttributeLogicalName("category")]
	public OptionSetValue Category
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("category");
		}
		set
		{
			OnPropertyChanging("Category");
			((Entity)this).SetAttributeValue("category", (object)value);
			OnPropertyChanged("Category");
		}
	}

	[AttributeLogicalName("clientdata")]
	public string ClientData => ((Entity)this).GetAttributeValue<string>("clientdata");

	[AttributeLogicalName("componentstate")]
	public OptionSetValue ComponentState => ((Entity)this).GetAttributeValue<OptionSetValue>("componentstate");

	[AttributeLogicalName("createdby")]
	public EntityReference CreatedBy => ((Entity)this).GetAttributeValue<EntityReference>("createdby");

	[AttributeLogicalName("createdon")]
	public DateTime? CreatedOn => ((Entity)this).GetAttributeValue<DateTime?>("createdon");

	[AttributeLogicalName("createdonbehalfby")]
	public EntityReference CreatedOnBehalfBy => ((Entity)this).GetAttributeValue<EntityReference>("createdonbehalfby");

	[AttributeLogicalName("createstage")]
	public OptionSetValue CreateStage
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("createstage");
		}
		set
		{
			OnPropertyChanging("CreateStage");
			((Entity)this).SetAttributeValue("createstage", (object)value);
			OnPropertyChanged("CreateStage");
		}
	}

	[AttributeLogicalName("deletestage")]
	public OptionSetValue DeleteStage
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("deletestage");
		}
		set
		{
			OnPropertyChanging("DeleteStage");
			((Entity)this).SetAttributeValue("deletestage", (object)value);
			OnPropertyChanged("DeleteStage");
		}
	}

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

	[AttributeLogicalName("formid")]
	public Guid? FormId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("formid");
		}
		set
		{
			OnPropertyChanging("FormId");
			((Entity)this).SetAttributeValue("formid", (object)value);
			OnPropertyChanged("FormId");
		}
	}

	[AttributeLogicalName("inputparameters")]
	public string InputParameters
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("inputparameters");
		}
		set
		{
			OnPropertyChanging("InputParameters");
			((Entity)this).SetAttributeValue("inputparameters", (object)value);
			OnPropertyChanged("InputParameters");
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

	[AttributeLogicalName("iscrmuiworkflow")]
	public bool? IsCrmUIWorkflow => ((Entity)this).GetAttributeValue<bool?>("iscrmuiworkflow");

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

	[AttributeLogicalName("istransacted")]
	public bool? IsTransacted
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("istransacted");
		}
		set
		{
			OnPropertyChanging("IsTransacted");
			((Entity)this).SetAttributeValue("istransacted", (object)value);
			OnPropertyChanged("IsTransacted");
		}
	}

	[AttributeLogicalName("languagecode")]
	public int? LanguageCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("languagecode");
		}
		set
		{
			OnPropertyChanging("LanguageCode");
			((Entity)this).SetAttributeValue("languagecode", (object)value);
			OnPropertyChanged("LanguageCode");
		}
	}

	[AttributeLogicalName("mode")]
	public OptionSetValue Mode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("mode");
		}
		set
		{
			OnPropertyChanging("Mode");
			((Entity)this).SetAttributeValue("mode", (object)value);
			OnPropertyChanged("Mode");
		}
	}

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

	[AttributeLogicalName("ondemand")]
	public bool? OnDemand
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("ondemand");
		}
		set
		{
			OnPropertyChanging("OnDemand");
			((Entity)this).SetAttributeValue("ondemand", (object)value);
			OnPropertyChanged("OnDemand");
		}
	}

	[AttributeLogicalName("overwritetime")]
	public DateTime? OverwriteTime => ((Entity)this).GetAttributeValue<DateTime?>("overwritetime");

	[AttributeLogicalName("ownerid")]
	public EntityReference OwnerId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("ownerid");
		}
		set
		{
			OnPropertyChanging("OwnerId");
			((Entity)this).SetAttributeValue("ownerid", (object)value);
			OnPropertyChanged("OwnerId");
		}
	}

	[AttributeLogicalName("owningbusinessunit")]
	public EntityReference OwningBusinessUnit => ((Entity)this).GetAttributeValue<EntityReference>("owningbusinessunit");

	[AttributeLogicalName("owningteam")]
	public EntityReference OwningTeam => ((Entity)this).GetAttributeValue<EntityReference>("owningteam");

	[AttributeLogicalName("owninguser")]
	public EntityReference OwningUser => ((Entity)this).GetAttributeValue<EntityReference>("owninguser");

	[AttributeLogicalName("parentworkflowid")]
	public EntityReference ParentWorkflowId => ((Entity)this).GetAttributeValue<EntityReference>("parentworkflowid");

	[AttributeLogicalName("plugintypeid")]
	public EntityReference PluginTypeId => ((Entity)this).GetAttributeValue<EntityReference>("plugintypeid");

	[AttributeLogicalName("primaryentity")]
	public string PrimaryEntity
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("primaryentity");
		}
		set
		{
			OnPropertyChanging("PrimaryEntity");
			((Entity)this).SetAttributeValue("primaryentity", (object)value);
			OnPropertyChanged("PrimaryEntity");
		}
	}

	[AttributeLogicalName("processorder")]
	public int? ProcessOrder
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("processorder");
		}
		set
		{
			OnPropertyChanging("ProcessOrder");
			((Entity)this).SetAttributeValue("processorder", (object)value);
			OnPropertyChanged("ProcessOrder");
		}
	}

	[AttributeLogicalName("processroleassignment")]
	public string ProcessRoleAssignment
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("processroleassignment");
		}
		set
		{
			OnPropertyChanging("ProcessRoleAssignment");
			((Entity)this).SetAttributeValue("processroleassignment", (object)value);
			OnPropertyChanged("ProcessRoleAssignment");
		}
	}

	[AttributeLogicalName("rank")]
	public int? Rank
	{
		get
		{
			return ((Entity)this).GetAttributeValue<int?>("rank");
		}
		set
		{
			OnPropertyChanging("Rank");
			((Entity)this).SetAttributeValue("rank", (object)value);
			OnPropertyChanged("Rank");
		}
	}

	[AttributeLogicalName("rendererobjecttypecode")]
	public string RendererObjectTypeCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("rendererobjecttypecode");
		}
		set
		{
			OnPropertyChanging("RendererObjectTypeCode");
			((Entity)this).SetAttributeValue("rendererobjecttypecode", (object)value);
			OnPropertyChanged("RendererObjectTypeCode");
		}
	}

	[AttributeLogicalName("runas")]
	public OptionSetValue RunAs
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("runas");
		}
		set
		{
			OnPropertyChanging("RunAs");
			((Entity)this).SetAttributeValue("runas", (object)value);
			OnPropertyChanged("RunAs");
		}
	}

	[AttributeLogicalName("scope")]
	public OptionSetValue Scope
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("scope");
		}
		set
		{
			OnPropertyChanging("Scope");
			((Entity)this).SetAttributeValue("scope", (object)value);
			OnPropertyChanged("Scope");
		}
	}

	[AttributeLogicalName("sdkmessageid")]
	public EntityReference SdkMessageId => ((Entity)this).GetAttributeValue<EntityReference>("sdkmessageid");

	[AttributeLogicalName("solutionid")]
	public Guid? SolutionId => ((Entity)this).GetAttributeValue<Guid?>("solutionid");

	[AttributeLogicalName("statecode")]
	public WorkflowState? StateCode
	{
		get
		{
			OptionSetValue attributeValue = ((Entity)this).GetAttributeValue<OptionSetValue>("statecode");
			if (attributeValue != null)
			{
				return (WorkflowState)Enum.ToObject(typeof(WorkflowState), attributeValue.Value);
			}
			return null;
		}
		set
		{
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Expected O, but got Unknown
			OnPropertyChanging("StateCode");
			if (!value.HasValue)
			{
				((Entity)this).SetAttributeValue("statecode", (object)null);
			}
			else
			{
				((Entity)this).SetAttributeValue("statecode", (object)new OptionSetValue((int)value.Value));
			}
			OnPropertyChanged("StateCode");
		}
	}

	[AttributeLogicalName("statuscode")]
	public OptionSetValue StatusCode
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("statuscode");
		}
		set
		{
			OnPropertyChanging("StatusCode");
			((Entity)this).SetAttributeValue("statuscode", (object)value);
			OnPropertyChanged("StatusCode");
		}
	}

	[AttributeLogicalName("subprocess")]
	public bool? Subprocess
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("subprocess");
		}
		set
		{
			OnPropertyChanging("Subprocess");
			((Entity)this).SetAttributeValue("subprocess", (object)value);
			OnPropertyChanged("Subprocess");
		}
	}

	[AttributeLogicalName("supportingsolutionid")]
	public Guid? SupportingSolutionId => ((Entity)this).GetAttributeValue<Guid?>("supportingsolutionid");

	[AttributeLogicalName("syncworkflowlogonfailure")]
	public bool? SyncWorkflowLogOnFailure
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("syncworkflowlogonfailure");
		}
		set
		{
			OnPropertyChanging("SyncWorkflowLogOnFailure");
			((Entity)this).SetAttributeValue("syncworkflowlogonfailure", (object)value);
			OnPropertyChanged("SyncWorkflowLogOnFailure");
		}
	}

	[AttributeLogicalName("triggeroncreate")]
	public bool? TriggerOnCreate
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("triggeroncreate");
		}
		set
		{
			OnPropertyChanging("TriggerOnCreate");
			((Entity)this).SetAttributeValue("triggeroncreate", (object)value);
			OnPropertyChanged("TriggerOnCreate");
		}
	}

	[AttributeLogicalName("triggerondelete")]
	public bool? TriggerOnDelete
	{
		get
		{
			return ((Entity)this).GetAttributeValue<bool?>("triggerondelete");
		}
		set
		{
			OnPropertyChanging("TriggerOnDelete");
			((Entity)this).SetAttributeValue("triggerondelete", (object)value);
			OnPropertyChanged("TriggerOnDelete");
		}
	}

	[AttributeLogicalName("triggeronupdateattributelist")]
	public string TriggerOnUpdateAttributeList
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("triggeronupdateattributelist");
		}
		set
		{
			OnPropertyChanging("TriggerOnUpdateAttributeList");
			((Entity)this).SetAttributeValue("triggeronupdateattributelist", (object)value);
			OnPropertyChanged("TriggerOnUpdateAttributeList");
		}
	}

	[AttributeLogicalName("type")]
	public OptionSetValue Type
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("type");
		}
		set
		{
			OnPropertyChanging("Type");
			((Entity)this).SetAttributeValue("type", (object)value);
			OnPropertyChanged("Type");
		}
	}

	[AttributeLogicalName("uniquename")]
	public string UniqueName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("uniquename");
		}
		set
		{
			OnPropertyChanging("UniqueName");
			((Entity)this).SetAttributeValue("uniquename", (object)value);
			OnPropertyChanged("UniqueName");
		}
	}

	[AttributeLogicalName("updatestage")]
	public OptionSetValue UpdateStage
	{
		get
		{
			return ((Entity)this).GetAttributeValue<OptionSetValue>("updatestage");
		}
		set
		{
			OnPropertyChanging("UpdateStage");
			((Entity)this).SetAttributeValue("updatestage", (object)value);
			OnPropertyChanged("UpdateStage");
		}
	}

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => ((Entity)this).GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("workflowid")]
	public Guid? WorkflowId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("workflowid");
		}
		set
		{
			OnPropertyChanging("WorkflowId");
			((Entity)this).SetAttributeValue("workflowid", (object)value);
			if (value.HasValue)
			{
				((Entity)this).Id = value.Value;
			}
			else
			{
				((Entity)this).Id = Guid.Empty;
			}
			OnPropertyChanged("WorkflowId");
		}
	}

	[AttributeLogicalName("workflowidunique")]
	public Guid? WorkflowIdUnique => ((Entity)this).GetAttributeValue<Guid?>("workflowidunique");

	[AttributeLogicalName("xaml")]
	public string Xaml
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("xaml");
		}
		set
		{
			OnPropertyChanging("Xaml");
			((Entity)this).SetAttributeValue("xaml", (object)value);
			OnPropertyChanged("Xaml");
		}
	}

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<Workflow> Referenced_workflow_active_workflow
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_active_workflow");
				}
				IEnumerable<Workflow> relatedEntities = ((Entity)this).GetRelatedEntities<Workflow>("workflow_active_workflow", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(Workflow element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Workflow>("workflow_active_workflow", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_workflow_active_workflow");
			((Entity)this).SetRelatedEntities<Workflow>("workflow_active_workflow", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_workflow_active_workflow");
		}
	}

	[RelationshipSchemaName("workflow_dependencies")]
	public IEnumerable<WorkflowDependency> workflow_dependencies
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependencies");
				}
				IEnumerable<WorkflowDependency> relatedEntities = ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependencies", (EntityRole?)null);
				relatedEntities.ToList().ForEach(delegate(WorkflowDependency element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<WorkflowDependency>("workflow_dependencies", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_dependencies");
			((Entity)this).SetRelatedEntities<WorkflowDependency>("workflow_dependencies", (EntityRole?)null, value);
			OnPropertyChanged("workflow_dependencies");
		}
	}

	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public IEnumerable<Workflow> Referenced_workflow_parent_workflow
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_parent_workflow");
				}
				IEnumerable<Workflow> relatedEntities = ((Entity)this).GetRelatedEntities<Workflow>("workflow_parent_workflow", (EntityRole?)(EntityRole)1);
				relatedEntities.ToList().ForEach(delegate(Workflow element)
				{
					element.ServiceContext = ServiceContext;
				});
				return relatedEntities;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntities<Workflow>("workflow_parent_workflow", (EntityRole?)(EntityRole)1);
			}
		}
		set
		{
			OnPropertyChanging("Referenced_workflow_parent_workflow");
			((Entity)this).SetRelatedEntities<Workflow>("workflow_parent_workflow", (EntityRole?)(EntityRole)1, value);
			OnPropertyChanged("Referenced_workflow_parent_workflow");
		}
	}

	[AttributeLogicalName("owninguser")]
	[RelationshipSchemaName("system_user_workflow")]
	public SystemUser system_user_workflow
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "system_user_workflow");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("system_user_workflow", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("system_user_workflow", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("activeworkflowid")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public Workflow Referencing_workflow_active_workflow
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_active_workflow");
				}
				Workflow relatedEntity = ((Entity)this).GetRelatedEntity<Workflow>("workflow_active_workflow", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<Workflow>("workflow_active_workflow", (EntityRole?)(EntityRole)0);
			}
		}
	}

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("workflow_createdby")]
	public SystemUser workflow_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_createdby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("workflow_createdby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("workflow_createdby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("workflow_createdonbehalfby")]
	public SystemUser workflow_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_createdonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("workflow_createdonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("workflow_createdonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("workflow_modifiedby")]
	public SystemUser workflow_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_modifiedby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("workflow_modifiedby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("workflow_modifiedby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("workflow_modifiedonbehalfby")]
	public SystemUser workflow_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_modifiedonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("workflow_modifiedonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("workflow_modifiedonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("parentworkflowid")]
	[RelationshipSchemaName(/*Could not decode attribute arguments.*/)]
	public Workflow Referencing_workflow_parent_workflow
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_parent_workflow");
				}
				Workflow relatedEntity = ((Entity)this).GetRelatedEntity<Workflow>("workflow_parent_workflow", (EntityRole?)(EntityRole)0);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<Workflow>("workflow_parent_workflow", (EntityRole?)(EntityRole)0);
			}
		}
	}

	public List<Workflow> ChildWorkflows { get; set; }

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public Workflow()
		: base("workflow")
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

	public List<Workflow> Load_Referenced_workflow_active_workflow(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Workflow> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflow", ((Entity)this).LogicalName, "activeworkflowid", "workflowid", "workflowid", "workflowid", recordCountLimit, attributes)
			select entity.ToEntity<Workflow>();
		Referenced_workflow_active_workflow = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<WorkflowDependency> Load_workflow_dependencies(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<WorkflowDependency> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflowdependency", ((Entity)this).LogicalName, "workflowid", "workflowid", "workflowid", "workflowid", recordCountLimit, attributes)
			select entity.ToEntity<WorkflowDependency>();
		workflow_dependencies = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public List<Workflow> Load_Referenced_workflow_parent_workflow(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		IEnumerable<Workflow> source = from entity in CrmHelpers.LoadRelation((Entity)(object)this, service, "workflow", ((Entity)this).LogicalName, "parentworkflowid", "workflowid", "workflowid", "workflowid", recordCountLimit, attributes)
			select entity.ToEntity<Workflow>();
		Referenced_workflow_parent_workflow = ((source.Count() > 0) ? source.ToArray() : null);
		return source.ToList();
	}

	public Workflow(object anonymousType)
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
				((DataCollection<string, object>)(object)((Entity)this).Attributes)["workflowid"] = ((Entity)this).Id;
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
