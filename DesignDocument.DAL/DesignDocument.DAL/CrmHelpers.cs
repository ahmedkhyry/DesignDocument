using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace DesignDocument.DAL;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
public static class CrmHelpers
{
	public static int GetValue(Type labelType, string label, int languageCode = 1033)
	{
		Type declaringType = labelType.DeclaringType;
		if (declaringType == null)
		{
			return -1;
		}
		Type declaringType2 = declaringType.DeclaringType;
		if (declaringType2 == null)
		{
			return -1;
		}
		List<FieldInfo> source = (from fieldQ in labelType.GetFields()
			where fieldQ.Name.Contains(languageCode.ToString()) && (string)fieldQ.GetValue(labelType) == label
			select fieldQ).ToList();
		if (!source.Any())
		{
			return -1;
		}
		FieldInfo fieldInfo = source.First();
		Type nestedType = declaringType2.GetNestedType(labelType.Name);
		if (nestedType == null)
		{
			return -1;
		}
		object obj = Enum.Parse(nestedType, fieldInfo.Name.Replace("_" + languageCode, ""));
		return (int)obj;
	}

	public static int GetValue(string logicalName, string label, Type enumsType, int languageCode = 1033)
	{
		Type labelType = GetLabelType(enumsType, logicalName);
		return GetValue(labelType, label, languageCode);
	}

	private static Type GetEnumType(Type enumsType, string logicalName)
	{
		FieldInfo logicalNameField = GetLogicalNameField(enumsType, logicalName);
		return (logicalNameField == null) ? null : enumsType.GetNestedType(logicalNameField.Name);
	}

	public static string GetLabel(string logicalName, int constant, Type enumsType, int languageCode = 1033)
	{
		Type enumType = GetEnumType(enumsType, logicalName);
		string name = enumType.Name;
		string enumName = enumType.GetEnumName(constant);
		Type nestedType = enumsType.GetNestedType("Labels").GetNestedType(name);
		if (nestedType == null)
		{
			return "NO_LABEL";
		}
		FieldInfo field = nestedType.GetField(enumName + "_" + languageCode);
		return (field == null) ? "NO_LABEL" : field.GetValue(nestedType).ToString();
	}

	private static Type GetLabelType(Type enumsType, string logicalName)
	{
		FieldInfo logicalNameField = GetLogicalNameField(enumsType, logicalName);
		return enumsType.GetNestedType("Labels").GetNestedType(logicalNameField.Name);
	}

	private static FieldInfo GetLogicalNameField(Type enumsType, string logicalName)
	{
		Type namesType = enumsType.GetNestedType("Names");
		List<FieldInfo> source = (from fieldQ in namesType.GetFields()
			where (string)fieldQ.GetValue(namesType) == logicalName
			select fieldQ).ToList();
		return source.FirstOrDefault();
	}

	internal static List<Entity> LoadRelation(Entity entity, IOrganizationService service, string fromEntityName, string toEntityName, string fromFieldName, string toFieldName, string idFieldName, string intersectIdFieldName, int limit = -1, params string[] attributes)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		limit = ((limit < 0) ? int.MaxValue : limit);
		QueryExpression val = new QueryExpression(fromEntityName);
		((Collection<LinkEntity>)(object)val.LinkEntities).Add(new LinkEntity(fromEntityName, toEntityName, fromFieldName, toFieldName, (JoinOperator)0));
		((Collection<LinkEntity>)(object)val.LinkEntities)[0].EntityAlias = "linkedEntityAlias";
		val.Criteria.AddCondition("linkedEntityAlias", intersectIdFieldName, (ConditionOperator)0, new object[1] { entity[idFieldName] });
		if (attributes.Length == 1 && attributes[0] == "*")
		{
			val.ColumnSet = new ColumnSet(true);
		}
		else if (attributes.Length != 0)
		{
			val.ColumnSet = new ColumnSet(attributes);
		}
		else
		{
			val.ColumnSet = new ColumnSet(false);
		}
		val.PageInfo = new PagingInfo
		{
			PageNumber = 1,
			Count = 1000
		};
		List<Entity> list = new List<Entity>();
		EntityCollection val2;
		do
		{
			val2 = service.RetrieveMultiple((QueryBase)(object)val);
			val.PageInfo.PagingCookie = val2.PagingCookie;
			PagingInfo pageInfo = val.PageInfo;
			int pageNumber = pageInfo.PageNumber;
			pageInfo.PageNumber = pageNumber + 1;
			list.AddRange((IEnumerable<Entity>)val2.Entities);
		}
		while (val2.MoreRecords && list.Count + 500 <= limit);
		return list.ToList();
	}
}
