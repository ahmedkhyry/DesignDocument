using System.Collections.Generic;

namespace DesignDocument.Model;

public class Generation : ModelBase
{
	public string IP { get; set; }

	public bool IsVisio { get; set; }

	public List<EntitySelect> EntitiesSelected { get; set; }

	public Organization Organization { get; set; }

	public User User { get; set; }

	public byte[] Document { get; set; }
}
