using System;

namespace DesignDocument.BLL;

public class RolePrivilegesViewModel
{
	public string EntityLogicalName { get; set; }

	public Guid RoleId { get; set; }

	public string RoleName { get; set; }

	public int CreateAccessRight { get; set; }

	public int ReadAccessRight { get; set; }

	public int WriteAccessRight { get; set; }

	public int DeleteAccessRight { get; set; }

	public int AppendAccessRight { get; set; }

	public int AppendToAccessRight { get; set; }

	public int AssignAccessRight { get; set; }

	public int ShareAccessRight { get; set; }

	public int ReparentAccessRight { get; set; }
}
