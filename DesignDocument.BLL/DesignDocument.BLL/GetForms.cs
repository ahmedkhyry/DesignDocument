using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DesignDocument.DAL;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace DesignDocument.BLL;

public static class GetForms
{
	public static IEnumerable<SystemForm> RetrieveEntityFormList(string logicalNames, IOrganizationService oService)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		QueryByAttribute val = new QueryByAttribute("systemform");
		val.Attributes.AddRange(new string[3] { "objecttypecode", "type", "formactivationstate" });
		val.Values.AddRange(new object[3] { logicalNames, 2, 1 });
		val.ColumnSet = new ColumnSet(true);
		EntityCollection val2 = oService.RetrieveMultiple((QueryBase)(object)val);
		return ((IEnumerable<Entity>)val2.Entities).Select((Entity s) => s.ToEntity<SystemForm>());
	}

	public static void ExtractField(XmlNode cellNode, List<CrmFormLabel> crmFormLabels, Entity form, CrmFormTab tabName, CrmFormSection sectionName, string entityLogicalName, int lcid)
	{
		if (cellNode.Attributes == null)
		{
			return;
		}
		XmlAttribute cellIdAttr = cellNode.Attributes["id"];
		if (cellIdAttr == null || cellNode.ChildNodes.Count == 0)
		{
			return;
		}
		XmlNode xmlNode = cellNode.SelectSingleNode("control");
		if (xmlNode != null && xmlNode.Attributes != null)
		{
			CrmFormLabel crmFormLabel = crmFormLabels.FirstOrDefault((CrmFormLabel f) => f.Id == new Guid(cellIdAttr.Value) && f.FormUniqueId == form.GetAttributeValue<Guid>("formidunique"));
			if (crmFormLabel == null)
			{
				crmFormLabel = new CrmFormLabel
				{
					Id = new Guid(cellIdAttr.Value),
					Form = form.GetAttributeValue<string>("name"),
					FormUniqueId = form.GetAttributeValue<Guid>("formidunique"),
					FormId = form.GetAttributeValue<Guid>("formid"),
					Tab = tabName,
					Section = sectionName,
					Entity = entityLogicalName,
					Attribute = xmlNode.Attributes["id"].Value,
					Names = new Dictionary<int, string>()
				};
				crmFormLabels.Add(crmFormLabel);
			}
			XmlAttribute xmlAttribute = (cellNode.SelectSingleNode("labels/label[@languagecode='" + lcid + "']")?.Attributes)?["description"];
			if (!crmFormLabel.Names.ContainsKey(lcid))
			{
				crmFormLabel.Names.Add(lcid, (xmlAttribute == null) ? string.Empty : xmlAttribute.Value);
			}
		}
	}

	public static string ExtractSection(XmlNode sectionNode, int lcid, List<CrmFormSection> crmFormSections, Entity form, string tabName, string entityLogicalName)
	{
		if (sectionNode.Attributes == null || sectionNode.Attributes["id"] == null)
		{
			return string.Empty;
		}
		string sectionId = sectionNode.Attributes["id"].Value;
		XmlNode xmlNode = sectionNode.SelectSingleNode("labels/label[@languagecode='" + lcid + "']");
		if (xmlNode == null || xmlNode.Attributes == null)
		{
			return string.Empty;
		}
		XmlAttribute xmlAttribute = xmlNode.Attributes["description"];
		if (xmlAttribute == null)
		{
			return string.Empty;
		}
		string value = xmlAttribute.Value;
		CrmFormSection crmFormSection = crmFormSections.FirstOrDefault((CrmFormSection f) => f.Id == new Guid(sectionId) && f.FormUniqueId == form.GetAttributeValue<Guid>("formidunique"));
		if (crmFormSection == null)
		{
			crmFormSection = new CrmFormSection
			{
				Id = new Guid(sectionId),
				FormUniqueId = form.GetAttributeValue<Guid>("formidunique"),
				FormId = form.GetAttributeValue<Guid>("formid"),
				Form = form.GetAttributeValue<string>("name"),
				Tab = tabName,
				Entity = entityLogicalName,
				Names = new Dictionary<int, string>()
			};
			crmFormSections.Add(crmFormSection);
		}
		if (crmFormSection.Names.ContainsKey(lcid))
		{
			return value;
		}
		crmFormSection.Names.Add(lcid, value);
		return value;
	}

	public static string ExtractTabName(XmlNode tabNode, int lcid, List<CrmFormTab> crmFormTabs, Entity form, string entityLogicalName)
	{
		if (tabNode.Attributes == null || tabNode.Attributes["id"] == null)
		{
			return string.Empty;
		}
		string tabId = tabNode.Attributes["id"].Value;
		XmlNode xmlNode = tabNode.SelectSingleNode("labels/label[@languagecode='" + lcid + "']");
		if (xmlNode == null || xmlNode.Attributes == null)
		{
			return string.Empty;
		}
		XmlAttribute xmlAttribute = xmlNode.Attributes["description"];
		if (xmlAttribute == null)
		{
			return string.Empty;
		}
		string value = xmlAttribute.Value;
		CrmFormTab crmFormTab = crmFormTabs.FirstOrDefault((CrmFormTab f) => f.Id == new Guid(tabId) && f.FormUniqueId == form.GetAttributeValue<Guid>("formidunique"));
		if (crmFormTab == null)
		{
			crmFormTab = new CrmFormTab
			{
				Id = new Guid(tabId),
				FormUniqueId = form.GetAttributeValue<Guid>("formidunique"),
				FormId = form.GetAttributeValue<Guid>("formid"),
				Form = form.GetAttributeValue<string>("name"),
				Entity = entityLogicalName,
				Names = new Dictionary<int, string>()
			};
			crmFormTabs.Add(crmFormTab);
		}
		if (crmFormTab.Names.ContainsKey(lcid))
		{
			return value;
		}
		crmFormTab.Names.Add(lcid, value);
		return value;
	}

	public static Entity GetCurrentUserSettings(IOrganizationService service)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		QueryExpression val = new QueryExpression("usersettings");
		val.ColumnSet = new ColumnSet(new string[2] { "uilanguageid", "localeid" });
		val.Criteria = new FilterExpression();
		val.Criteria.AddCondition("systemuserid", (ConditionOperator)41, new object[0]);
		EntityCollection val2 = service.RetrieveMultiple((QueryBase)(object)val);
		return val2[0];
	}

	public static List<Entity> GetEntities(IOrganizationService service, string entityLogicalName)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0021: Expected O, but got Unknown
		return ((IEnumerable<Entity>)service.RetrieveMultiple((QueryBase)new QueryExpression
		{
			EntityName = entityLogicalName,
			ColumnSet = new ColumnSet(true)
		}).Entities).ToList();
	}
}
