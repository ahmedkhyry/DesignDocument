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
[EntityLogicalName("workflowdependency")]
public class WorkflowDependency : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
	public static class Enums
	{
		[DataContract]
		public enum Type
		{
			[EnumMember]
			Sdkassociation = 1,
			[EnumMember]
			Localparameter,
			[EnumMember]
			Primaryentity,
			[EnumMember]
			PrimaryentitybeforeSDKoperation,
			[EnumMember]
			PrimaryentityafterSDKoperation,
			[EnumMember]
			Relatedentity,
			[EnumMember]
			Customentitydefinitionthatworkflowdependson,
			[EnumMember]
			Attributedefinitionthatworkflowdependson,
			[EnumMember]
			ArgumentEntitythatworkflowdependson
		}

		public static class Names
		{
			public const string Type = "type";
		}

		public static class Labels
		{
			public static class Type
			{
				public const string Sdkassociation_1033 = "Sdk association";

				public const string Sdkassociation_1025 = "اقتران Sdk";

				public const string Localparameter_1033 = "Local parameter";

				public const string Localparameter_1025 = "معلمة محلية";

				public const string Primaryentity_1033 = "Primary entity";

				public const string Primaryentity_1025 = "الكيان الأساسي";

				public const string PrimaryentitybeforeSDKoperation_1033 = "Primary entity - before SDK operation";

				public const string PrimaryentitybeforeSDKoperation_1025 = "الكيان الأساسي – قبل عملية SDK";

				public const string PrimaryentityafterSDKoperation_1033 = "Primary entity - after SDK operation";

				public const string PrimaryentityafterSDKoperation_1025 = "الكيان الأساسي – بعد عملية SDK";

				public const string Relatedentity_1033 = "Related entity";

				public const string Relatedentity_1025 = "الكيان ذو الصلة";

				public const string Customentitydefinitionthatworkflowdependson_1033 = "Custom entity definition that workflow depends on";

				public const string Customentitydefinitionthatworkflowdependson_1025 = "تعريف الكيان المخصص الذي يعتمد عليه سير العمل";

				public const string Attributedefinitionthatworkflowdependson_1033 = "Attribute definition that workflow depends on";

				public const string Attributedefinitionthatworkflowdependson_1025 = "تعريف السمة التي يعتمد عليها سير العمل";

				public const string ArgumentEntitythatworkflowdependson_1033 = "Argument Entity that workflow depends on";

				public const string ArgumentEntitythatworkflowdependson_1025 = "كيان الوسيطة الذي يعتمد عليه سير العمل";

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
			public const string CreatedBy = "CreatedBy";

			public const string CreatedByName = "CreatedByName";

			public const string CreatedOn = "CreatedOn";

			public const string CreatedOnBehalfBy = "CreatedOnBehalfBy";

			public const string CreatedOnBehalfByName = "CreatedOnBehalfByName";

			public const string CustomEntityName = "CustomEntityName";

			public const string DependentAttributeName = "DependentAttributeName";

			public const string DependentEntityName = "DependentEntityName";

			public const string EntityAttributes = "EntityAttributes";

			public const string ModifiedBy = "ModifiedBy";

			public const string ModifiedByName = "ModifiedByName";

			public const string ModifiedOn = "ModifiedOn";

			public const string ModifiedOnBehalfBy = "ModifiedOnBehalfBy";

			public const string ModifiedOnBehalfByName = "ModifiedOnBehalfByName";

			public const string OwnerId = "OwnerId";

			public const string OwningBusinessUnit = "OwningBusinessUnit";

			public const string OwningUser = "OwningUser";

			public const string ParameterName = "ParameterName";

			public const string ParameterType = "ParameterType";

			public const string RelatedAttributeName = "RelatedAttributeName";

			public const string RelatedEntityName = "RelatedEntityName";

			public const string SdkMessageId = "SdkMessageId";

			public const string SdkMessageIdName = "SdkMessageIdName";

			public const string Type = "Type";

			public const string VersionNumber = "VersionNumber";

			public const string WorkflowDependencyId = "WorkflowDependencyId";

			public const string WorkflowId = "WorkflowId";

			public const string WorkflowIdName = "WorkflowIdName";
		}

		public static class Labels
		{
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

			public static class CustomEntityName
			{
				public const string _1033 = "Custom Entity";

				public const string _1025 = "كيان مخصص";
			}

			public static class DependentAttributeName
			{
				public const string _1033 = "Dependent Attribute Name";

				public const string _1025 = "اسم السمة التابعة";
			}

			public static class DependentEntityName
			{
				public const string _1033 = "Dependent Entity Name";

				public const string _1025 = "اسم الكيان التابع";
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

			public static class OwnerId
			{
				public const string _1033 = "Owner";

				public const string _1025 = "المالك";
			}

			public static class RelatedEntityName
			{
				public const string _1033 = "Related Entity";

				public const string _1025 = "الكيان ذو الصلة";
			}

			public static class Type
			{
				public const string _1033 = "Type";

				public const string _1025 = "النوع";
			}

			public static class WorkflowDependencyId
			{
				public const string _1033 = "Process Dependency";

				public const string _1025 = "تبعية العملية";
			}

			public static class WorkflowId
			{
				public const string _1033 = "Process";

				public const string _1025 = "العملية";
			}
		}

		public const string CreatedBy = "createdby";

		public const string CreatedByName = "createdbyName";

		public const string CreatedOn = "createdon";

		public const string CreatedOnBehalfBy = "createdonbehalfby";

		public const string CreatedOnBehalfByName = "createdonbehalfbyName";

		public const string CustomEntityName = "customentityname";

		public const string DependentAttributeName = "dependentattributename";

		public const string DependentEntityName = "dependententityname";

		public const string EntityAttributes = "entityattributes";

		public const string ModifiedBy = "modifiedby";

		public const string ModifiedByName = "modifiedbyName";

		public const string ModifiedOn = "modifiedon";

		public const string ModifiedOnBehalfBy = "modifiedonbehalfby";

		public const string ModifiedOnBehalfByName = "modifiedonbehalfbyName";

		public const string OwnerId = "ownerid";

		public const string OwningBusinessUnit = "owningbusinessunit";

		public const string OwningUser = "owninguser";

		public const string ParameterName = "parametername";

		public const string ParameterType = "parametertype";

		public const string RelatedAttributeName = "relatedattributename";

		public const string RelatedEntityName = "relatedentityname";

		public const string SdkMessageId = "sdkmessageid";

		public const string SdkMessageIdName = "sdkmessageidName";

		public const string Type = "type";

		public const string VersionNumber = "versionnumber";

		public const string WorkflowDependencyId = "workflowdependencyid";

		public const string WorkflowId = "workflowid";

		public const string WorkflowIdName = "workflowidName";
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
				public const string sdkmessageid_workflow_dependency = "sdkmessageid";

				public const string workflow_dependencies = "workflowid";

				public const string workflow_dependency_createdby = "createdby";

				public const string workflow_dependency_createdonbehalfby = "createdonbehalfby";

				public const string workflow_dependency_modifiedby = "modifiedby";

				public const string workflow_dependency_modifiedonbehalfby = "modifiedonbehalfby";
			}

			public const string sdkmessageid_workflow_dependency = "sdkmessageid_workflow_dependency";

			public const string workflow_dependencies = "workflow_dependencies";

			public const string workflow_dependency_createdby = "workflow_dependency_createdby";

			public const string workflow_dependency_createdonbehalfby = "workflow_dependency_createdonbehalfby";

			public const string workflow_dependency_modifiedby = "workflow_dependency_modifiedby";

			public const string workflow_dependency_modifiedonbehalfby = "workflow_dependency_modifiedonbehalfby";
		}

		public static class NToN
		{
		}
	}

	public static class Actions
	{
	}

	public const string DisplayName = "Process Dependency";

	public const string SchemaName = "WorkflowDependency";

	public const string EntityLogicalName = "workflowdependency";

	public const int EntityTypeCode = 4704;

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

	[AttributeLogicalName("workflowdependencyid")]
	public override Guid Id
	{
		get
		{
			return ((Entity)this).Id;
		}
		set
		{
			WorkflowDependencyId = value;
		}
	}

	[AttributeLogicalName("createdby")]
	public EntityReference CreatedBy => ((Entity)this).GetAttributeValue<EntityReference>("createdby");

	[AttributeLogicalName("createdon")]
	public DateTime? CreatedOn => ((Entity)this).GetAttributeValue<DateTime?>("createdon");

	[AttributeLogicalName("createdonbehalfby")]
	public EntityReference CreatedOnBehalfBy => ((Entity)this).GetAttributeValue<EntityReference>("createdonbehalfby");

	[AttributeLogicalName("customentityname")]
	public string CustomEntityName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("customentityname");
		}
		set
		{
			OnPropertyChanging("CustomEntityName");
			((Entity)this).SetAttributeValue("customentityname", (object)value);
			OnPropertyChanged("CustomEntityName");
		}
	}

	[AttributeLogicalName("dependentattributename")]
	public string DependentAttributeName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("dependentattributename");
		}
		set
		{
			OnPropertyChanging("DependentAttributeName");
			((Entity)this).SetAttributeValue("dependentattributename", (object)value);
			OnPropertyChanged("DependentAttributeName");
		}
	}

	[AttributeLogicalName("dependententityname")]
	public string DependentEntityName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("dependententityname");
		}
		set
		{
			OnPropertyChanging("DependentEntityName");
			((Entity)this).SetAttributeValue("dependententityname", (object)value);
			OnPropertyChanged("DependentEntityName");
		}
	}

	[AttributeLogicalName("entityattributes")]
	public string EntityAttributes
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("entityattributes");
		}
		set
		{
			OnPropertyChanging("EntityAttributes");
			((Entity)this).SetAttributeValue("entityattributes", (object)value);
			OnPropertyChanged("EntityAttributes");
		}
	}

	[AttributeLogicalName("modifiedby")]
	public EntityReference ModifiedBy => ((Entity)this).GetAttributeValue<EntityReference>("modifiedby");

	[AttributeLogicalName("modifiedon")]
	public DateTime? ModifiedOn => ((Entity)this).GetAttributeValue<DateTime?>("modifiedon");

	[AttributeLogicalName("modifiedonbehalfby")]
	public EntityReference ModifiedOnBehalfBy => ((Entity)this).GetAttributeValue<EntityReference>("modifiedonbehalfby");

	[AttributeLogicalName("ownerid")]
	public EntityReference OwnerId => ((Entity)this).GetAttributeValue<EntityReference>("ownerid");

	[AttributeLogicalName("owningbusinessunit")]
	public Guid? OwningBusinessUnit => ((Entity)this).GetAttributeValue<Guid?>("owningbusinessunit");

	[AttributeLogicalName("owninguser")]
	public Guid? OwningUser => ((Entity)this).GetAttributeValue<Guid?>("owninguser");

	[AttributeLogicalName("parametername")]
	public string ParameterName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("parametername");
		}
		set
		{
			OnPropertyChanging("ParameterName");
			((Entity)this).SetAttributeValue("parametername", (object)value);
			OnPropertyChanged("ParameterName");
		}
	}

	[AttributeLogicalName("parametertype")]
	public string ParameterType
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("parametertype");
		}
		set
		{
			OnPropertyChanging("ParameterType");
			((Entity)this).SetAttributeValue("parametertype", (object)value);
			OnPropertyChanged("ParameterType");
		}
	}

	[AttributeLogicalName("relatedattributename")]
	public string RelatedAttributeName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("relatedattributename");
		}
		set
		{
			OnPropertyChanging("RelatedAttributeName");
			((Entity)this).SetAttributeValue("relatedattributename", (object)value);
			OnPropertyChanged("RelatedAttributeName");
		}
	}

	[AttributeLogicalName("relatedentityname")]
	public string RelatedEntityName
	{
		get
		{
			return ((Entity)this).GetAttributeValue<string>("relatedentityname");
		}
		set
		{
			OnPropertyChanging("RelatedEntityName");
			((Entity)this).SetAttributeValue("relatedentityname", (object)value);
			OnPropertyChanged("RelatedEntityName");
		}
	}

	[AttributeLogicalName("sdkmessageid")]
	public EntityReference SdkMessageId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("sdkmessageid");
		}
		set
		{
			OnPropertyChanging("SdkMessageId");
			((Entity)this).SetAttributeValue("sdkmessageid", (object)value);
			OnPropertyChanged("SdkMessageId");
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

	[AttributeLogicalName("versionnumber")]
	public long? VersionNumber => ((Entity)this).GetAttributeValue<long?>("versionnumber");

	[AttributeLogicalName("workflowdependencyid")]
	public Guid? WorkflowDependencyId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<Guid?>("workflowdependencyid");
		}
		set
		{
			OnPropertyChanging("WorkflowDependencyId");
			((Entity)this).SetAttributeValue("workflowdependencyid", (object)value);
			if (value.HasValue)
			{
				((Entity)this).Id = value.Value;
			}
			else
			{
				((Entity)this).Id = Guid.Empty;
			}
			OnPropertyChanged("WorkflowDependencyId");
		}
	}

	[AttributeLogicalName("workflowid")]
	public EntityReference WorkflowId
	{
		get
		{
			return ((Entity)this).GetAttributeValue<EntityReference>("workflowid");
		}
		set
		{
			OnPropertyChanging("WorkflowId");
			((Entity)this).SetAttributeValue("workflowid", (object)value);
			OnPropertyChanged("WorkflowId");
		}
	}

	[AttributeLogicalName("sdkmessageid")]
	[RelationshipSchemaName("sdkmessageid_workflow_dependency")]
	public SdkMessage sdkmessageid_workflow_dependency
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "sdkmessageid_workflow_dependency");
				}
				SdkMessage relatedEntity = ((Entity)this).GetRelatedEntity<SdkMessage>("sdkmessageid_workflow_dependency", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SdkMessage>("sdkmessageid_workflow_dependency", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("sdkmessageid_workflow_dependency");
			((Entity)this).SetRelatedEntity<SdkMessage>("sdkmessageid_workflow_dependency", (EntityRole?)null, value);
			OnPropertyChanged("sdkmessageid_workflow_dependency");
		}
	}

	[AttributeLogicalName("workflowid")]
	[RelationshipSchemaName("workflow_dependencies")]
	public Workflow workflow_dependencies
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependencies");
				}
				Workflow relatedEntity = ((Entity)this).GetRelatedEntity<Workflow>("workflow_dependencies", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<Workflow>("workflow_dependencies", (EntityRole?)null);
			}
		}
		set
		{
			OnPropertyChanging("workflow_dependencies");
			((Entity)this).SetRelatedEntity<Workflow>("workflow_dependencies", (EntityRole?)null, value);
			OnPropertyChanged("workflow_dependencies");
		}
	}

	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("workflow_dependency_createdby")]
	public SystemUser workflow_dependency_createdby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependency_createdby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("workflow_dependency_createdby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("workflow_dependency_createdby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("workflow_dependency_createdonbehalfby")]
	public SystemUser workflow_dependency_createdonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependency_createdonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("workflow_dependency_createdonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("workflow_dependency_createdonbehalfby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("workflow_dependency_modifiedby")]
	public SystemUser workflow_dependency_modifiedby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependency_modifiedby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("workflow_dependency_modifiedby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("workflow_dependency_modifiedby", (EntityRole?)null);
			}
		}
	}

	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("workflow_dependency_modifiedonbehalfby")]
	public SystemUser workflow_dependency_modifiedonbehalfby
	{
		get
		{
			try
			{
				if (serviceContext != null)
				{
					((OrganizationServiceContext)serviceContext).LoadProperty((Entity)(object)this, "workflow_dependency_modifiedonbehalfby");
				}
				SystemUser relatedEntity = ((Entity)this).GetRelatedEntity<SystemUser>("workflow_dependency_modifiedonbehalfby", (EntityRole?)null);
				relatedEntity.ServiceContext = ServiceContext;
				return relatedEntity;
			}
			catch
			{
				return ((Entity)this).GetRelatedEntity<SystemUser>("workflow_dependency_modifiedonbehalfby", (EntityRole?)null);
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PropertyChangingEventHandler PropertyChanging;

	public WorkflowDependency()
		: base("workflowdependency")
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

	public SdkMessage Load_sdkmessageid_workflow_dependency(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "sdkmessage", ((Entity)this).LogicalName, "sdkmessageid", "sdkmessageid", "workflowdependencyid", "workflowdependencyid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return sdkmessageid_workflow_dependency = val.ToEntity<SdkMessage>();
		}
		return sdkmessageid_workflow_dependency = null;
	}

	public Workflow Load_workflow_dependencies(IOrganizationService service, int recordCountLimit = -1, params string[] attributes)
	{
		Entity val = CrmHelpers.LoadRelation((Entity)(object)this, service, "workflow", ((Entity)this).LogicalName, "workflowid", "workflowid", "workflowdependencyid", "workflowdependencyid", recordCountLimit, attributes).FirstOrDefault();
		if (val != null)
		{
			return workflow_dependencies = val.ToEntity<Workflow>();
		}
		return workflow_dependencies = null;
	}

	public WorkflowDependency(object anonymousType)
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
				((DataCollection<string, object>)(object)((Entity)this).Attributes)["workflowdependencyid"] = ((Entity)this).Id;
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
