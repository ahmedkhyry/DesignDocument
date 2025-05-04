using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word;

[ComImport]
[CompilerGenerated]
[DefaultMember("Item")]
[Guid("000209A9-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface InlineShapes : IEnumerable
{
	void _VtblGap1_7();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(24)]
	[return: MarshalAs(UnmanagedType.Interface)]
	InlineShape AddOLEObject([Optional][In][MarshalAs(UnmanagedType.Struct)] ref object ClassType, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object FileName, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object LinkToFile, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object DisplayAsIcon, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object IconFileName, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object IconIndex, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object IconLabel, [Optional][In][MarshalAs(UnmanagedType.Struct)] ref object Range);
}
