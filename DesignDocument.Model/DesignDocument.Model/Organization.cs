using System.Collections.Generic;

namespace DesignDocument.Model;

public class Organization : ModelBase
{
	public string URL { get; set; }

	public List<User> Users { get; set; }
}
