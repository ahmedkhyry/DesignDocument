using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace DesignDocument.DAL;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
public class XrmServiceContext : OrganizationServiceContext
{
	public IQueryable<ActivityParty> ActivityPartySet => ((OrganizationServiceContext)this).CreateQuery<ActivityParty>();

	public IQueryable<Dependency> DependencySet => ((OrganizationServiceContext)this).CreateQuery<Dependency>();

	public IQueryable<FieldPermission> FieldPermissionSet => ((OrganizationServiceContext)this).CreateQuery<FieldPermission>();

	public IQueryable<FieldSecurityProfile> FieldSecurityProfileSet => ((OrganizationServiceContext)this).CreateQuery<FieldSecurityProfile>();

	public IQueryable<PluginAssembly> PluginAssemblySet => ((OrganizationServiceContext)this).CreateQuery<PluginAssembly>();

	public IQueryable<Privilege> PrivilegeSet => ((OrganizationServiceContext)this).CreateQuery<Privilege>();

	public IQueryable<Role> RoleSet => ((OrganizationServiceContext)this).CreateQuery<Role>();

	public IQueryable<RolePrivileges> RolePrivilegesSet => ((OrganizationServiceContext)this).CreateQuery<RolePrivileges>();

	public IQueryable<SdkMessage> SdkMessageSet => ((OrganizationServiceContext)this).CreateQuery<SdkMessage>();

	public IQueryable<SdkMessageFilter> SdkMessageFilterSet => ((OrganizationServiceContext)this).CreateQuery<SdkMessageFilter>();

	public IQueryable<SdkMessageProcessingStep> SdkMessageProcessingStepSet => ((OrganizationServiceContext)this).CreateQuery<SdkMessageProcessingStep>();

	public IQueryable<SystemForm> SystemFormSet => ((OrganizationServiceContext)this).CreateQuery<SystemForm>();

	public IQueryable<SystemUser> SystemUserSet => ((OrganizationServiceContext)this).CreateQuery<SystemUser>();

	public IQueryable<Workflow> WorkflowSet => ((OrganizationServiceContext)this).CreateQuery<Workflow>();

	public IQueryable<WorkflowDependency> WorkflowDependencySet => ((OrganizationServiceContext)this).CreateQuery<WorkflowDependency>();

	public XrmServiceContext(IOrganizationService service)
		: base(service)
	{
	}
}
