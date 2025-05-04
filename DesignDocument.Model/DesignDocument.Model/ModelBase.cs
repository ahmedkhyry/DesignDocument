using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignDocument.Model;

public class ModelBase
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	public string Name { get; set; }

	public string Notes { get; set; }

	public DateTime ModifiedOn { get; private set; }

	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	public DateTime CreatedOn { get; set; }

	public ModelBase()
	{
		ModifiedOn = DateTime.Now;
	}
}
