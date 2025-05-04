using System;
using System.Windows.Forms;

namespace DesignDocument.UI;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.Run(new Technical_Document_Generator());
	}
}
