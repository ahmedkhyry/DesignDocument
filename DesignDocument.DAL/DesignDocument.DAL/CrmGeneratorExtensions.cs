using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Xrm.Sdk;

namespace DesignDocument.DAL;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
public static class CrmGeneratorExtensions
{
	public static string GetLabel(this Enum enumMember, int languageCode = 1033)
	{
		Type type = enumMember.GetType();
		Type declaringType = type.DeclaringType;
		if (declaringType == null)
		{
			return "NO_LABEL";
		}
		Type nestedType = declaringType.GetNestedType("Labels");
		Type nestedType2 = nestedType.GetNestedType(type.Name);
		FieldInfo field = nestedType2.GetField(string.Concat(enumMember, "_", languageCode));
		return (field == null) ? "NO_LABEL" : field.GetValue(nestedType2).ToString();
	}

	public static OptionSetValue ToOptionSetValue(this Enum enumMember)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		return new OptionSetValue(int.Parse(enumMember.ToString("d")));
	}
}
