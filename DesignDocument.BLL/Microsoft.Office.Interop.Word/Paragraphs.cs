using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Item")]
[Guid("00020958-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Paragraphs : IEnumerable
{
	void _VtblGap1_2();

	[DispId(3)]
	Paragraph First
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(3)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}
}
