using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Item")]
[Guid("0002095B-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Sentences : IEnumerable
{
	void _VtblGap1_2();

	[DispId(3)]
	Range First
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(3)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}
}
