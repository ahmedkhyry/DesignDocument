using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using DesignDocument.DAL;
using DesignDocument.Model;
using GemBox.Spreadsheet;
using Microsoft.Crm.Sdk.Messages;
//using Microsoft.Crm.Sdk.Samples;
using Microsoft.Office.Interop.Word;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using BorderStyle = Spire.Doc.Documents.BorderStyle;
using Document = Spire.Doc.Document;
using HeaderFooter = Spire.Doc.HeaderFooter;
using HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment;
using MailMessage = System.Net.Mail.MailMessage;
using Paragraph = Spire.Doc.Documents.Paragraph;
using Section = Spire.Doc.Section;
using Style = Spire.Doc.Documents.Style;

namespace DesignDocument.BLL;

public class DataManager
{
	private static List<RolePrivilegesViewModel> rolePrivilegesViewModels;

	private static List<Workflow> workFlowsAll;

	private static List<FieldPermission> fieldPermissions;

	private static List<Role> roles;

	private static List<SdkMessageProcessingStep> sdkMessageProcessingStepsAll;

	private static Dictionary<Guid, Guid> parentChildGuids;

	private static IEnumerable<Entity> webresources;

	private static List<SelectedEntity> selectedEntities;

	public static string[] NonStandard = new string[63]
	{
		"applicationfile", "attachment", "authorizationserver", "businessprocessflowinstance", "businessunitmap", "clientupdate", "commitment", "competitoraddress", "complexcontrol", "dependencynode",
		"displaystringmap", "documentindex", "emailhash", "emailsearch", "filtertemplate", "imagedescriptor", "importdata", "integrationstatus", "interprocesslock", "multientitysearchentities",
		"multientitysearch", "notification", "organizationstatistic", "owner", "partnerapplication", "principalattributeaccessmap", "principalobjectaccessreadsnapshot", "principalobjectaccess", "privilegeobjecttypecodes", "postregarding",
		"postrole", "subscriptionclients", "salesprocessinstance", "recordcountsnapshot", "replicationbacklog", "resourcegroupexpansion", "ribboncommand", "ribboncontextgroup", "ribbondiff", "ribbonrule",
		"ribbontabtocommandmap", "roletemplate", "statusmap", "stringmap", "sqlencryptionaudit", "subscriptionsyncinfo", "subscription", "subscriptiontrackingdeletedobject", "systemapplicationmetadata", "systemuserbusinessunitentitymap",
		"systemuserprincipals", "traceassociation", "traceregarding", "unresolvedaddress", "userapplicationmetadata", "userfiscalcalendar", "webwizard", "wizardaccessprivilege", "wizardpage", "workflowwaitsubscription",
		"bulkdeleteoperation", "reportlink", "rollupjob"
	};

	public static List<CrmFormLabel> CrmFormLabels { get; set; } = new List<CrmFormLabel>();

	public static List<CrmFormSection> CrmFormSections { get; set; } = new List<CrmFormSection>();

	public static List<CrmFormTab> CrmFormTabs { get; set; } = new List<CrmFormTab>();

	public static List<CrmForm> CrmForms { get; set; } = new List<CrmForm>();

	public static bool IsServerConnected(string connectionString)
	{
		using SqlConnection sqlConnection = new SqlConnection(connectionString);
		try
		{
			sqlConnection.Open();
			return true;
		}
		catch (SqlException)
		{
			return false;
		}
	}

	public void ExportWordDocument(List<EntityMetadata> entities, List<SelectedEntity> entityLogicalNames, List<int> languages, IOrganizationService service, string path, CrmConfiguration conf, DocumentSettings documentSettings, bool isVisio = false, bool isIncludeDates = false, DateTime? dateFrom = null, DateTime? dateTo = null)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Expected O, but got Unknown
		selectedEntities = entityLogicalNames;
		string text = path.Replace(".docx", ".vsd");
		Document document = new Document();
		((SummaryDocumentProperties)document.BuiltinDocumentProperties).Author = "RAMY-VICTOR";
		ParagraphStyle val = new ParagraphStyle((IDocument)(object)document);
		((Style)val).Name = "Normal";
		((Style)val).CharacterFormat.FontName = conf.Font.Name;
		((Style)val).CharacterFormat.FontSize = conf.Font.Size;
		document.Styles.Add((IStyle)(object)val);
		InitializeEntityData(service, entities, languages, documentSettings, isIncludeDates, dateFrom, dateTo);
		if (!documentSettings.VisibleSectionsTabs)
		{
			CrmFormLabels.RemoveAll((CrmFormLabel p) => !p.Tab.IsVisible || !p.Section.IsVisible);
		}
		int line = 0;
		Section val2 = document.AddSection();
		Paragraph val3 = val2.AddParagraph();
		val3 = val2.AddParagraph();
		val3.AppendBreak((BreakType)0);
		val3.AppendText("References");
		val3.ApplyStyle((BuiltinStyle)1);
		List<FieldPermission> source = fieldPermissions.Where((FieldPermission p) => CrmFormLabels.Any((CrmFormLabel a) => a.Entity == p.EntityName) && CrmFormLabels.Any((CrmFormLabel a) => a.Attribute == p.AttributeLogicalName)).ToList();
		new ExportSheet(conf, document).PrintSymbolsTable(document, val3, val2);
		new ExportSheet(conf, document).PrintSecurityRoleColorsTable(document, val3, val2);
		if (source.Any())
		{
			new ExportSheet(conf, document).ProductUsersTable(document, val3, val2, source, CrmFormLabels);
		}
		HeaderFooter footer = val2.HeadersFooters.Footer;
		Paragraph val4 = ((Body)footer).AddParagraph();
		val4.AppendField("page number", (FieldType)33);
		val4.AppendText(" of ");
		val4.AppendField("number of pages", (FieldType)66);
		val4.Format.HorizontalAlignment = (HorizontalAlignment)2;
		List<CrmFormLabel> list = CrmFormLabels.Where((CrmFormLabel p) => !p.IsConfigurationEntity).ToList();
		List<CrmFormLabel> list2 = CrmFormLabels.Where((CrmFormLabel p) => p.IsConfigurationEntity).ToList();
		for (int i = 0; i < list.Count; i++)
		{
			CrmFormLabel crmFormLabel = list[i];
			CrmFormLabel crmFormLabelPrev = null;
			if (i > 0)
			{
				crmFormLabelPrev = list[i - 1];
			}
			line = ((i != list.Count - 1 && !(list[i].Entity != list[i + 1].Entity)) ? new ExportSheet(conf, document).PrintNonConfigurationDocument(languages, document, line, crmFormLabelPrev, crmFormLabel, list, val2, rolePrivilegesViewModels, workFlowsAll, sdkMessageProcessingStepsAll, parentChildGuids, conf, documentSettings, isLast: false, isIncludeDates, dateFrom, dateTo) : new ExportSheet(conf, document).PrintNonConfigurationDocument(languages, document, line, crmFormLabelPrev, crmFormLabel, list, val2, rolePrivilegesViewModels, workFlowsAll, sdkMessageProcessingStepsAll, parentChildGuids, conf, documentSettings, isLast: true, isIncludeDates, dateFrom, dateTo));
		}
		if (list2.Any())
		{
			Paragraph val5 = val2.AddParagraph();
			val5.AppendBreak((BreakType)0);
			val5.AppendText("Configuration Entities");
			val5.ApplyStyle((BuiltinStyle)1);
			val5.ListFormat.ApplyStyle("levelstyle");
			((Style)val5.GetStyle()).CharacterFormat.Italic = false;
			((Style)val5.GetStyle()).CharacterFormat.FontSize = 20f;
			val5.GetStyle().ParagraphFormat.Borders.Bottom.BorderType = (BorderStyle)1;
			List<List<Entity>> list3 = RetrieveEntityList(list2.Select((CrmFormLabel s) => s.Entity).Distinct().ToList(), service);
			int num = 0;
			foreach (List<Entity> item in list3)
			{
				if (item.Any())
				{
					line = new ExportSheet(conf, document).PrintConfigurationDocument(line, document, val2, item, list2, num);
					num++;
				}
			}
		}
		Paragraph val6 = val2.Paragraphs[0];
		if (isVisio)
		{
			val6.AppendBreak((BreakType)0);
		}
		((ParagraphBase)val6.AppendText("Table of content")).CharacterFormat.FontSize = 22f;
		val6.AppendTOC(1, 3);
		try
		{
			System.Threading.Tasks.Task.Run(delegate
			{
				document.UpdateTableOfContents();
			}).Wait(2000);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Table of contents issue: " + ex.ToString());
		}
		FileInfo fileInfo = new FileInfo(path);
		if (fileInfo.Extension.Contains("pdf"))
		{
			ToPdfParameterList val7 = new ToPdfParameterList
			{
				IsEmbeddedAllFonts = true
			};
			document.SaveToFile(path, val7);
			return;
		}
		if (fileInfo.Extension.Contains("xml"))
		{
			document.SaveToFile(path, (FileFormat)16);
			return;
		}
		document.SaveToFile(path);
		#region Commented Code
		//Microsoft.Office.Interop.Word.Application application = (Microsoft.Office.Interop.Word.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
		//Documents documents = application.Documents;
		//object FileName = path;
		//object ConfirmConversions = Type.Missing;
		//object ReadOnly = Type.Missing;
		//object AddToRecentFiles = Type.Missing;
		//object PasswordDocument = Type.Missing;
		//object PasswordTemplate = Type.Missing;
		//object Revert = Type.Missing;
		//object WritePasswordDocument = Type.Missing;
		//object WritePasswordTemplate = Type.Missing;
		//object Format = Type.Missing;
		//object Encoding = Type.Missing;
		//object Visible = Type.Missing;
		//object OpenAndRepair = Type.Missing;
		//object DocumentDirection = Type.Missing;
		//object NoEncodingDialog = Type.Missing;
		//object XMLTransform = Type.Missing;

		//      Document document2 = documents.Open(ref FileName, ref ConfirmConversions, ref ReadOnly, ref AddToRecentFiles, ref PasswordDocument, ref PasswordTemplate, ref Revert, ref WritePasswordDocument, ref WritePasswordTemplate, ref Format, ref Encoding, ref Visible, ref OpenAndRepair, ref DocumentDirection, ref NoEncodingDialog, ref XMLTransform);
		//Paragraphs paragraphs = document2.Paragraphs;
		//Paragraph first = paragraphs.First;
		//Range range = first.Range;
		//Sentences sentences = range.Sentences;
		//Range first2 = sentences.First;
		//application.ActiveWindow.View.ReadingLayout = false;
		//first2.Select();
		//if (isVisio)
		//{
		//	try
		//	{
		//		DiagramBuilder.Engine(service, entities.Select((EntityMetadata p) => p.LogicalName).ToArray(), entities.ToArray(), text);
		//		first2.Text = "\f";
		//		InlineShapes inlineShapes = first2.InlineShapes;
		//		XMLTransform = Type.Missing;
		//		NoEncodingDialog = text;
		//		DocumentDirection = false;
		//		OpenAndRepair = false;
		//		Visible = Type.Missing;
		//		Encoding = Type.Missing;
		//		Format = Type.Missing;
		//		WritePasswordTemplate = Type.Missing;
		//		InlineShape inlineShape = inlineShapes.AddOLEObject(ref XMLTransform, ref NoEncodingDialog, ref DocumentDirection, ref OpenAndRepair, ref Visible, ref Encoding, ref Format, ref WritePasswordTemplate);
		//	}
		//	catch (Exception ex2)
		//	{
		//		first2.Text = string.Empty;
		//		MessageBox.Show(ex2.Message);
		//	}
		//}
		//else
		//{
		//	first2.Text = string.Empty;
		//}
		//WritePasswordTemplate = Type.Missing;
		//Format = Type.Missing;
		//Encoding = Type.Missing;
		//document2.Close(ref WritePasswordTemplate, ref Format, ref Encoding);
		//Encoding = Type.Missing;
		//Format = Type.Missing;
		//WritePasswordTemplate = Type.Missing;
		//application.Quit(ref Encoding, ref Format, ref WritePasswordTemplate); 
		#endregion
	}

	public static void DatabaseFileRead(string path, Guid generationId)
	{
		DataContext dataContext = new DataContext();
		try
		{
			GenericRepository<DataContext, Generation> genericRepository = new GenericRepository<DataContext, Generation>(dataContext);
			byte[] document = genericRepository.FindFirstBy((Generation p) => p.Id == generationId).Document;
			using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
			fileStream.Write(document, 0, document.Length);
		}
		finally
		{
			((IDisposable)dataContext)?.Dispose();
		}
	}

	private void UpdateTableOfContents(Document document)
	{
		try
		{
			document.UpdateTableOfContents();
		}
		catch (Exception)
		{
		}
	}

	private static void InitializeEntityData(IOrganizationService service, List<EntityMetadata> entities, List<int> languages, DocumentSettings documentSettings, bool isIncludeDates = false, DateTime? dateFrom = null, DateTime? dateTo = null)
	{
		Entity currentUserSettings = GetCurrentUserSettings(service);
		rolePrivilegesViewModels = new List<RolePrivilegesViewModel>();
		workFlowsAll = new List<Workflow>();
		fieldPermissions = new List<FieldPermission>();
		roles = new List<Role>();
		sdkMessageProcessingStepsAll = new List<SdkMessageProcessingStep>();
		if (documentSettings.SecurityRoles)
		{
			rolePrivilegesViewModels = RetrieveSecurityRolesMatrix(service, entities.Select((EntityMetadata s) => s.LogicalName).ToList());
		}
		if (documentSettings.Plugins)
		{
			sdkMessageProcessingStepsAll = RetrievSdkMessageProcessingStep(entities.Select((EntityMetadata s) => s.ObjectTypeCode).ToList(), service, isIncludeDates: false, null, null).ToList();
		}
		if (documentSettings.Workflows)
		{
			workFlowsAll.AddRange(RetrieveWorkFlows(service, entities.Select((EntityMetadata s) => s.LogicalName).ToList(), isIncludeDates: false, null, null).ToList());
		}
		if (documentSettings.SecurityProfiles)
		{
			fieldPermissions.AddRange(RetrieveSeurityProfiles(service, entities.Select((EntityMetadata s) => s.LogicalName).ToList(), isIncludeDates: false, null, null).ToList());
		}
		List<SystemForm> list = new List<SystemForm>();
		list = (from p in RetrieveEntityFormList(entities.Select((EntityMetadata s) => s.LogicalName).ToList(), service)
			where ((OptionSetValue)((DataCollection<string, object>)(object)((Entity)p).Attributes)["type"]).Value == 2
			select p).ToList();
		foreach (int item in languages.OrderByDescending((int o) => o))
		{
			int attributeValue = currentUserSettings.GetAttributeValue<int>("uilanguageid");
			int num = attributeValue;
			if (attributeValue != item)
			{
				currentUserSettings["localeid"] = item;
				currentUserSettings["uilanguageid"] = item;
				service.Update(currentUserSettings);
				num = item;
			}
			int num2 = 0;
			foreach (EntityMetadata item2 in entities.OrderBy((EntityMetadata e) => e.LogicalName))
			{
				if (((MetadataBase)item2).MetadataId.HasValue)
				{
					FillCrmFormLabels(service, item2, item, list, workFlowsAll, isIncludeDates: false, null, null);
					num2++;
				}
			}
			if (attributeValue != num)
			{
				currentUserSettings["localeid"] = attributeValue;
				currentUserSettings["uilanguageid"] = attributeValue;
				service.Update(currentUserSettings);
			}
		}
	}

	private static void FillCrmFormLabels(IOrganizationService service, EntityMetadata entity, int lcid, List<SystemForm> formsAll, List<Workflow> workFlowsAll, bool isIncludeDates = false, DateTime? dateFrom = null, DateTime? dateTo = null)
	{
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Expected O, but got Unknown
		//IL_02e1: Expected O, but got Unknown
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Expected O, but got Unknown
		IEnumerable<SystemForm> enumerable = formsAll.Where((SystemForm p) => p.ObjectTypeCode == entity.LogicalName);
		List<Workflow> workFlows = workFlowsAll.Where((Workflow p) => p.PrimaryEntity == entity.LogicalName).ToList();
		foreach (SystemForm form in enumerable)
		{
			string attributeValue = ((Entity)form).GetAttributeValue<string>("formxml");
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(attributeValue);
			CrmForm crmForm = CrmForms.FirstOrDefault((CrmForm f) => f.FormUniqueId == ((Entity)form).GetAttributeValue<Guid>("formidunique"));
			if (crmForm == null)
			{
				crmForm = new CrmForm
				{
					FormUniqueId = ((Entity)form).GetAttributeValue<Guid>("formidunique"),
					Id = ((Entity)form).GetAttributeValue<Guid>("formid"),
					Entity = entity.LogicalName,
					Names = new Dictionary<int, string>(),
					Descriptions = new Dictionary<int, string>(),
					Events = ExtractFormEvents(form.FormXml)
				};
				CrmForms.Add(crmForm);
			}
			foreach (XmlNode item in xmlDocument.SelectNodes("//tab"))
			{
				CrmFormTab crmFormTab = ExtractTabName(item, lcid, CrmFormTabs, (Entity)(object)form, entity);
				if (crmFormTab == null)
				{
					continue;
				}
				foreach (XmlNode item2 in item.SelectNodes("columns/column/sections/section"))
				{
					CrmFormSection sectionName = ExtractSection(item2, lcid, CrmFormSections, (Entity)(object)form, crmFormTab.Names[lcid], entity);
					foreach (XmlNode item3 in item2.SelectNodes("rows/row/cell"))
					{
						CrmFormLabel crmFormField = new CrmFormLabel();
						ExtractField(item3, CrmFormLabels, workFlows, (Entity)(object)form, crmForm, crmFormTab, sectionName, entity, lcid, out crmFormField, service);
					}
				}
			}
			RetrieveLocLabelsRequest val = new RetrieveLocLabelsRequest
			{
				AttributeName = "name",
				EntityMoniker = new EntityReference("systemform", ((Entity)form).Id)
			};
			RetrieveLocLabelsResponse val2 = (RetrieveLocLabelsResponse)service.Execute((OrganizationRequest)(object)val);
			foreach (LocalizedLabel item4 in ((IEnumerable<LocalizedLabel>)val2.Label.LocalizedLabels).Where((LocalizedLabel p) => !crmForm.Names.ContainsKey(p.LanguageCode)))
			{
				crmForm.Names.Add(item4.LanguageCode, item4.Label);
			}
		}
	}

	private static Entity GetCurrentUserSettings(IOrganizationService service)
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

	public static List<Workflow> RetrieveWorkFlows(IOrganizationService service, List<string> entityLogical, bool isIncludeDates = false, DateTime? dateFrom = null, DateTime? dateTo = null)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		QueryExpression val = new QueryExpression();
		val.EntityName = "workflow";
		object[] array = new object[entityLogical.ToList().Count];
		for (int i = 0; i < entityLogical.ToList().Count; i++)
		{
			array[i] = entityLogical[i];
		}
		FilterExpression val2 = new FilterExpression((LogicalOperator)0);
		((Collection<ConditionExpression>)(object)val2.Conditions).Add(new ConditionExpression("primaryentity", (ConditionOperator)8, array));
		((Collection<ConditionExpression>)(object)val2.Conditions).Add(new ConditionExpression("statecode", (ConditionOperator)0, (object)1));
		((Collection<ConditionExpression>)(object)val2.Conditions).Add(new ConditionExpression("statuscode", (ConditionOperator)0, (object)2));
		((Collection<ConditionExpression>)(object)val2.Conditions).Add(new ConditionExpression("type", (ConditionOperator)0, (object)1));
		val.Criteria.AddFilter(val2);
		val.ColumnSet = new ColumnSet(new string[15]
		{
			"name", "subprocess", "ondemand", "triggeroncreate", "triggerondelete", "mode", "triggeronupdateattributelist", "parentworkflowid", "primaryentity", "category",
			"clientdata", "statecode", "statuscode", "runas", "scope"
		});
		EntityCollection val3 = service.RetrieveMultiple((QueryBase)(object)val);
		List<Workflow> list = ((IEnumerable<Entity>)val3.Entities).Select((Entity s) => s.ToEntity<Workflow>()).ToList();
		List<Dependency> workflowDependencies = GetWorkflowDependencies(service, list);
		foreach (Workflow item in list)
		{
			List<Dependency> workflowDependencies2 = GetWorkflowDependencies(service, ((Entity)item).Id);
			if (workflowDependencies2 == null || !workflowDependencies2.Any())
			{
				continue;
			}
			foreach (Dependency parentWfDependency in workflowDependencies2)
			{
				Workflow workflow = list.FirstOrDefault(delegate(Workflow p)
				{
					Guid id = ((Entity)p).Id;
					Guid? dependentComponentObjectId = parentWfDependency.DependentComponentObjectId;
					return id == dependentComponentObjectId;
				});
				if (workflow != null && workflow.Name != item.Name)
				{
					if (workflow.ChildWorkflows == null)
					{
						workflow.ChildWorkflows = new List<Workflow>();
					}
					workflow.ChildWorkflows.Add(item);
				}
			}
		}
		return list;
	}

	public static List<FieldPermission> RetrieveSeurityProfiles(IOrganizationService service, List<string> entityLogical, bool isIncludeDates = false, DateTime? dateFrom = null, DateTime? dateTo = null)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		QueryExpression val = new QueryExpression();
		val.EntityName = "fieldpermission";
		FieldSecurityProfile fieldSecurityProfile = new FieldSecurityProfile();
		IEnumerable<FieldPermission> lk_fieldpermission_fieldsecurityprofileid = fieldSecurityProfile.lk_fieldpermission_fieldsecurityprofileid;
		object[] array = new object[entityLogical.ToList().Count];
		for (int i = 0; i < entityLogical.ToList().Count; i++)
		{
			array[i] = entityLogical[i];
		}
		FilterExpression val2 = new FilterExpression((LogicalOperator)0);
		((Collection<ConditionExpression>)(object)val2.Conditions).Add(new ConditionExpression("entityname", (ConditionOperator)8, array));
		val.Criteria.AddFilter(val2);
		val.ColumnSet = new ColumnSet(true);
		((Collection<LinkEntity>)(object)val.LinkEntities).Add(new LinkEntity("fieldpermission", "fieldsecurityprofile", "fieldsecurityprofileid", "fieldsecurityprofileid", (JoinOperator)0));
		((Collection<LinkEntity>)(object)val.LinkEntities)[0].Columns = new ColumnSet(true);
		((Collection<LinkEntity>)(object)val.LinkEntities)[0].EntityAlias = "fieldsecurityprofile";
		FilterExpression val3 = new FilterExpression((LogicalOperator)1);
		EntityCollection val4 = service.RetrieveMultiple((QueryBase)(object)val);
		List<FieldPermission> list = ((IEnumerable<Entity>)val4.Entities).Select((Entity s) => s.ToEntity<FieldPermission>()).ToList();
		if (list.Any())
		{
			foreach (FieldPermission item in list)
			{
				item.FieldSecurityProfileId.Name = ((AliasedValue)((Entity)item)["fieldsecurityprofile.name"]).Value.ToString();
			}
		}
		return list.Where((FieldPermission p) => p.FieldSecurityProfileId.Name != "System Administrator").ToList();
	}

	public static List<Workflow> RetrieveWorkFlowsOld(IOrganizationService service, List<string> entityLogical)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		QueryByAttribute val = new QueryByAttribute("workflow");
		foreach (string item in entityLogical)
		{
			val.Attributes.AddRange(new string[4] { "primaryentity", "statecode", "statuscode", "type" });
			val.Values.AddRange(new object[4] { item, 1, 2, 1 });
			val.ColumnSet = new ColumnSet(new string[10] { "name", "subprocess", "ondemand", "triggeroncreate", "triggerondelete", "mode", "triggeronupdateattributelist", "parentworkflowid", "primaryentity", "category" });
		}
		EntityCollection val2 = service.RetrieveMultiple((QueryBase)(object)val);
		List<Workflow> list = ((IEnumerable<Entity>)val2.Entities).Select((Entity s) => s.ToEntity<Workflow>()).ToList();
		foreach (Workflow item2 in list)
		{
			List<Dependency> workflowDependencies = GetWorkflowDependencies(service, ((Entity)item2).Id);
			if (workflowDependencies == null || !workflowDependencies.Any())
			{
				continue;
			}
			foreach (Dependency parentWfDependency in workflowDependencies)
			{
				Workflow workflow = list.FirstOrDefault(delegate(Workflow p)
				{
					Guid id = ((Entity)p).Id;
					Guid? dependentComponentObjectId = parentWfDependency.DependentComponentObjectId;
					return id == dependentComponentObjectId;
				});
				if (workflow != null && workflow.Name != item2.Name)
				{
					if (workflow.ChildWorkflows == null)
					{
						workflow.ChildWorkflows = new List<Workflow>();
					}
					workflow.ChildWorkflows.Add(item2);
				}
			}
		}
		return list;
	}

	public static IEnumerable<Entity> RetrieveWebResources(IOrganizationService service)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_0020: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		QueryByAttribute val = new QueryByAttribute
		{
			EntityName = "webresource",
			ColumnSet = new ColumnSet(true)
		};
		val.Attributes.AddRange(new string[1] { "webresourcetype" });
		val.Values.AddRange(new object[1] { 3 });
		Entity val2 = null;
		EntityCollection val3 = service.RetrieveMultiple((QueryBase)(object)val);
		if (((Collection<Entity>)(object)val3.Entities).Count == 0)
		{
			throw new InvalidPluginExecutionException("Specified Webresource does not exist");
		}
		return (IEnumerable<Entity>)val3.Entities;
	}

	private static EntityCollection GetFieldDependencies(IOrganizationService service, Guid attributeMetadataId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		try
		{
			RetrieveDependenciesForDeleteRequest val = new RetrieveDependenciesForDeleteRequest
			{
				ComponentType = 2,
				ObjectId = attributeMetadataId
			};
			RetrieveDependenciesForDeleteResponse val2 = (RetrieveDependenciesForDeleteResponse)service.Execute((OrganizationRequest)(object)val);
			return val2.EntityCollection;
		}
		catch (Exception)
		{
			return null;
		}
	}

	private static EntityCollection GetFieldDependencies(IOrganizationService service, List<Guid> attributeMetadataIds)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		try
		{
			RetrieveDependenciesForDeleteRequest val = new RetrieveDependenciesForDeleteRequest();
			foreach (Guid attributeMetadataId in attributeMetadataIds)
			{
				val.ComponentType = 2;
				val.ObjectId = attributeMetadataId;
			}
			RetrieveDependenciesForDeleteResponse val2 = (RetrieveDependenciesForDeleteResponse)service.Execute((OrganizationRequest)(object)val);
			return val2.EntityCollection;
		}
		catch (Exception)
		{
			return null;
		}
	}

	private static EntityCollection GetFormDependencies(IOrganizationService service, Guid formMetadataId)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		RetrieveRequiredComponentsRequest val = new RetrieveRequiredComponentsRequest
		{
			ComponentType = 60,
			ObjectId = formMetadataId
		};
		RetrieveRequiredComponentsResponse val2 = (RetrieveRequiredComponentsResponse)service.Execute((OrganizationRequest)(object)val);
		return val2.EntityCollection;
	}

	private static List<Dependency> GetWorkflowDependencies(IOrganizationService service, Guid workflowId)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		RetrieveDependenciesForDeleteRequest val = new RetrieveDependenciesForDeleteRequest
		{
			ComponentType = 29,
			ObjectId = workflowId
		};
		RetrieveDependenciesForDeleteResponse val2 = (RetrieveDependenciesForDeleteResponse)service.Execute((OrganizationRequest)(object)val);
		return ((IEnumerable<Entity>)val2.EntityCollection.Entities).Select((Entity s) => s.ToEntity<Dependency>()).ToList();
	}

	private static List<Dependency> GetWorkflowDependencies(IOrganizationService service, List<Workflow> workflows)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002f: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		ExecuteMultipleRequest val = new ExecuteMultipleRequest
		{
			Settings = new ExecuteMultipleSettings
			{
				ContinueOnError = true,
				ReturnResponses = true
			},
			Requests = new OrganizationRequestCollection()
		};
		foreach (Workflow workflow in workflows)
		{
			((Collection<OrganizationRequest>)(object)val.Requests).Add((OrganizationRequest)new RetrieveDependenciesForDeleteRequest
			{
				ComponentType = 29,
				ObjectId = ((Entity)workflow).Id
			});
		}
		ExecuteMultipleResponse val2 = (ExecuteMultipleResponse)service.Execute((OrganizationRequest)(object)val);
		List<List<Dependency>> source = (from s in ((IEnumerable<ExecuteMultipleResponseItem>)val2.Responses).ToList()
			select ((IEnumerable<Entity>)((RetrieveDependenciesForDeleteResponse)s.Response).EntityCollection.Entities).Select((Entity ss) => ss.ToEntity<Dependency>()).ToList()).ToList();
		return (from grp in source.SelectMany((List<Dependency> x) => x)
			group grp by grp.DependencyId into sss
			select sss.First()).ToList();
	}

	private static IEnumerable<SystemForm> RetrieveEntityFormList(List<string> logicalNames, IOrganizationService oService)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		QueryExpression val = new QueryExpression();
		val.EntityName = "systemform";
		FilterExpression val2 = new FilterExpression((LogicalOperator)1);
		object[] array = new object[logicalNames.ToList().Count];
		for (int i = 0; i < logicalNames.ToList().Count; i++)
		{
			array[i] = logicalNames[i];
		}
		FilterExpression val3 = new FilterExpression((LogicalOperator)0);
		((Collection<ConditionExpression>)(object)val3.Conditions).Add(new ConditionExpression("objecttypecode", (ConditionOperator)8, array));
		((Collection<ConditionExpression>)(object)val3.Conditions).Add(new ConditionExpression("type", (ConditionOperator)0, (object)2));
		((Collection<ConditionExpression>)(object)val3.Conditions).Add(new ConditionExpression("formactivationstate", (ConditionOperator)0, (object)1));
		((Collection<FilterExpression>)(object)val2.Filters).Add(val3);
		val.Criteria.AddFilter(val2);
		val.ColumnSet = new ColumnSet(true);
		EntityCollection val4 = oService.RetrieveMultiple((QueryBase)(object)val);
		return ((IEnumerable<Entity>)val4.Entities).Select((Entity s) => s.ToEntity<SystemForm>());
	}

	private static List<List<Entity>> RetrieveEntityList(List<string> logicalNames, IOrganizationService oService)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		List<List<Entity>> list = new List<List<Entity>>();
		foreach (string logicalName in logicalNames)
		{
			QueryExpression val = new QueryExpression();
			val.EntityName = logicalName;
			val.ColumnSet = new ColumnSet(true);
			EntityCollection val2 = oService.RetrieveMultiple((QueryBase)(object)val);
			list.Add(((IEnumerable<Entity>)val2.Entities).ToList());
		}
		return list;
	}

	private static IEnumerable<SystemForm> RetrieveEntityFormList(string logicalName, IOrganizationService oService)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		QueryExpression val = new QueryExpression();
		val.EntityName = "systemform";
		((Collection<ConditionExpression>)(object)val.Criteria.Conditions).Add(new ConditionExpression("objecttypecode", (ConditionOperator)0, (object)logicalName));
		((Collection<ConditionExpression>)(object)val.Criteria.Conditions).Add(new ConditionExpression("type", (ConditionOperator)0, (object)2));
		((Collection<ConditionExpression>)(object)val.Criteria.Conditions).Add(new ConditionExpression("formactivationstate", (ConditionOperator)0, (object)1));
		val.ColumnSet = new ColumnSet(true);
		EntityCollection val2 = oService.RetrieveMultiple((QueryBase)(object)val);
		return ((IEnumerable<Entity>)val2.Entities).Select((Entity s) => s.ToEntity<SystemForm>());
	}

	private static IEnumerable<SystemForm> RetrieveEntityFormList2(List<string> logicalNames, IOrganizationService oService)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		QueryByAttribute val = new QueryByAttribute("systemform");
		foreach (string logicalName in logicalNames)
		{
			val.Attributes.AddRange(new string[3] { "objecttypecode", "type", "formactivationstate" });
			val.Values.AddRange(new object[3] { logicalName, 2, 1 });
			val.ColumnSet = new ColumnSet(true);
		}
		EntityCollection val2 = oService.RetrieveMultiple((QueryBase)(object)val);
		return ((IEnumerable<Entity>)val2.Entities).Select((Entity s) => s.ToEntity<SystemForm>());
	}

	private static IEnumerable<PluginAssembly> RetrievePluginAssemblies(string logicalName, IOrganizationService oService)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		QueryByAttribute val = new QueryByAttribute("pluginassembly");
		val.Attributes.AddRange(new string[2] { "ishidden", "ismanaged" });
		val.Values.AddRange(new object[2] { false, false });
		val.ColumnSet = new ColumnSet(true);
		EntityCollection val2 = oService.RetrieveMultiple((QueryBase)(object)val);
		return ((IEnumerable<Entity>)val2.Entities).Select((Entity s) => s.ToEntity<PluginAssembly>());
	}

	private static List<RolePrivilegesViewModel> RetrieveSecurityRolesMatrix(IOrganizationService Service, List<string> entityLogicalNames)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		string format = " <value>prvCreate{0}</value>\r\n                                      <value>prvRead{0}</value>\r\n                                      <value>prvWrite{0}</value>\r\n                                      <value>prvDelete{0}</value>\r\n                                      <value>prvAppend{0}</value>\r\n                                      <value>prvAppendTo{0}</value>\r\n                                      <value>prvAssign{0}</value>\r\n                                      <value>prvShare{0}</value>\r\n                                      <value>prvReparent{0}</value>";
		string text = string.Empty;
		foreach (string entityLogicalName2 in entityLogicalNames)
		{
			text += string.Format(format, entityLogicalName2);
		}
		string text2 = "<fetch version='1.0' mapping='logical' distinct='false'>\r\n                              <entity name='roleprivileges'>\r\n                                <attribute name='privilegeid'/>\r\n                                <attribute name='privilegedepthmask'/>\r\n                                <link-entity name='role' alias='roles' to='roleid' from='roleid' link-type='inner'>\r\n                                  <attribute name='name'/>\r\n                                  <attribute name='roleid'/>\r\n                                </link-entity>\r\n                                <link-entity name='privilege' alias='privileges' to='privilegeid' from='privilegeid' link-type='inner'>\r\n                                  <attribute name='name'/>\r\n                                  <attribute name='accessright'/>\r\n                                  <attribute name='canbebasic'/>\r\n                                  <attribute name='canbedeep'/>\r\n                                  <attribute name='canbeglobal'/>\r\n                                  <attribute name='canbelocal'/>\r\n                                  <filter type='and'>\r\n                                    <condition attribute='name' operator='in'>\r\n                                      " + text + "\r\n                                    </condition>\r\n                                  </filter>\r\n                                </link-entity>\r\n                              </entity>\r\n                            </fetch>";
		FetchExpression val = new FetchExpression(text2);
		EntityCollection val2 = Service.RetrieveMultiple((QueryBase)(object)val);
		DataCollection<Entity> entities = val2.Entities;
		IEnumerable<object> enumerable = ((IEnumerable<Entity>)val2.Entities).Select((Entity p) => p["roles.name"]).Distinct();
		List<RolePrivilegesViewModel> list = new List<RolePrivilegesViewModel>();
		foreach (Entity item in (Collection<Entity>)(object)entities)
		{
			string privilege = ((AliasedValue)item["privileges.name"]).Value.ToString();
			string roleName = ((AliasedValue)item["roles.name"]).Value.ToString();
			Guid roleId = new Guid(((AliasedValue)item["roles.roleid"]).Value.ToString());
			int accessRight = (int)((AliasedValue)item["privileges.accessright"]).Value;
			int privilegeDepthMask = (int)item["privilegedepthmask"];
			string entityLogicalName = ExtractEntityLogicalName(privilege);
			bool flag = list.Any((RolePrivilegesViewModel p) => p.EntityLogicalName == entityLogicalName && p.RoleName == roleName);
			RolePrivilegesViewModel rolePrivilegesViewModel = new RolePrivilegesViewModel
			{
				EntityLogicalName = entityLogicalName,
				RoleName = roleName,
				RoleId = roleId
			};
			if (flag)
			{
				rolePrivilegesViewModel = list.First((RolePrivilegesViewModel p) => p.EntityLogicalName == entityLogicalName && p.RoleName == roleName);
				rolePrivilegesViewModel = AssignAccessRight(rolePrivilegesViewModel, privilege, accessRight, privilegeDepthMask);
			}
			else
			{
				rolePrivilegesViewModel = AssignAccessRight(rolePrivilegesViewModel, privilege, accessRight, privilegeDepthMask);
				list.Add(rolePrivilegesViewModel);
			}
		}
		return list;
	}

	private static string ExtractEntityLogicalName(string privilege)
	{
		string empty = string.Empty;
		empty = privilege.Replace("prvCreate", "");
		empty = empty.Replace("prvRead", "");
		empty = empty.Replace("prvWrite", "");
		empty = empty.Replace("prvDelete", "");
		empty = empty.Replace("prvAppendTo", "");
		empty = empty.Replace("prvAppend", "");
		empty = empty.Replace("prvAssign", "");
		empty = empty.Replace("prvShare", "");
		empty = empty.Replace("prvReparent", "");
		return empty.ToLower();
	}

	private static RolePrivilegesViewModel AssignAccessRight(RolePrivilegesViewModel rolePrivilegesViewModel, string privilege, int accessRight, int privilegeDepthMask)
	{
		if (privilege.StartsWith("prvCreate"))
		{
			rolePrivilegesViewModel.CreateAccessRight = privilegeDepthMask;
		}
		else if (privilege.StartsWith("prvRead"))
		{
			rolePrivilegesViewModel.ReadAccessRight = privilegeDepthMask;
		}
		else if (privilege.StartsWith("prvWrite"))
		{
			rolePrivilegesViewModel.WriteAccessRight = privilegeDepthMask;
		}
		else if (privilege.StartsWith("prvDelete"))
		{
			rolePrivilegesViewModel.DeleteAccessRight = privilegeDepthMask;
		}
		else if (privilege.StartsWith("prvAppendTo"))
		{
			rolePrivilegesViewModel.AppendToAccessRight = privilegeDepthMask;
		}
		else if (privilege.StartsWith("prvAppend"))
		{
			rolePrivilegesViewModel.AppendAccessRight = privilegeDepthMask;
		}
		else if (privilege.StartsWith("prvAssign"))
		{
			rolePrivilegesViewModel.AssignAccessRight = privilegeDepthMask;
		}
		else if (privilege.StartsWith("prvShare"))
		{
			rolePrivilegesViewModel.ShareAccessRight = privilegeDepthMask;
		}
		else if (privilege.StartsWith("prvReparent"))
		{
			rolePrivilegesViewModel.ReparentAccessRight = privilegeDepthMask;
		}
		return rolePrivilegesViewModel;
	}

	private static List<SdkMessageProcessingStep> RetrievSdkMessageProcessingStep(List<int?> primaryObjectTypeCode, IOrganizationService oService, bool isIncludeDates = false, DateTime? dateFrom = null, DateTime? dateTo = null)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		QueryExpression val = new QueryExpression();
		val.EntityName = "sdkmessageprocessingstep";
		((Collection<ConditionExpression>)(object)val.Criteria.Conditions).Add(new ConditionExpression("ishidden", (ConditionOperator)0, (object)false));
		((Collection<ConditionExpression>)(object)val.Criteria.Conditions).Add(new ConditionExpression("ismanaged", (ConditionOperator)0, (object)false));
		val.ColumnSet = new ColumnSet(true);
		((Collection<LinkEntity>)(object)val.LinkEntities).Add(new LinkEntity("sdkmessageprocessingstep", "sdkmessagefilter", "sdkmessagefilterid", "sdkmessagefilterid", (JoinOperator)0));
		((Collection<LinkEntity>)(object)val.LinkEntities)[0].Columns = new ColumnSet(true);
		((Collection<LinkEntity>)(object)val.LinkEntities)[0].EntityAlias = "sdkmessagefilter";
		FilterExpression val2 = new FilterExpression((LogicalOperator)1);
		object[] array = new object[primaryObjectTypeCode.Where((int? p) => p.HasValue).ToList().Count];
		for (int i = 0; i < primaryObjectTypeCode.Where((int? p) => p.HasValue).ToList().Count; i++)
		{
			array[i] = primaryObjectTypeCode[i];
		}
		val2.AddCondition("primaryobjecttypecode", (ConditionOperator)8, array);
		((Collection<LinkEntity>)(object)val.LinkEntities)[0].LinkCriteria.AddFilter(val2);
		return ((IEnumerable<Entity>)oService.RetrieveMultiple((QueryBase)(object)val).Entities).Select((Entity s) => s.ToEntity<SdkMessageProcessingStep>()).ToList();
	}

	private static IEnumerable<SdkMessage> RetrieveSdkMessages(string logicalName, IOrganizationService oService)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		QueryByAttribute val = new QueryByAttribute("sdkmessage");
		val.Attributes.AddRange(new string[1] { "isprivate" });
		val.Values.AddRange(new object[1] { false });
		val.ColumnSet = new ColumnSet(true);
		EntityCollection val2 = oService.RetrieveMultiple((QueryBase)(object)val);
		return ((IEnumerable<Entity>)val2.Entities).Select((Entity s) => s.ToEntity<SdkMessage>());
	}

	private static List<CrmFieldEvent> ExtractFieldEvents(string FieldLogicalName, string formXmlString)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(formXmlString);
		List<CrmFieldEvent> list = new List<CrmFieldEvent>();
		XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//event[@attribute='" + FieldLogicalName + "']");
		if (xmlNodeList != null)
		{
			foreach (XmlNode item in xmlNodeList)
			{
				if (item == null)
				{
					continue;
				}
				XmlNode xmlNode2 = item.SelectSingleNode("Handlers");
				if (xmlNode2 == null)
				{
					continue;
				}
				foreach (XmlNode item2 in xmlNode2)
				{
					CrmFieldEvent crmFieldEvent = new CrmFieldEvent
					{
						IsEnabled = bool.Parse(item2.Attributes["enabled"].Value),
						LibraryName = item2.Attributes["libraryName"].Value,
						FunctionName = item2.Attributes["functionName"].Value,
						EventName = item.Attributes["name"].Value
					};
					if (item2.Attributes["passExecutionContext"] != null)
					{
						crmFieldEvent.IsExecutionContext = bool.Parse(item2.Attributes["passExecutionContext"].Value);
					}
					if (item2.Attributes["parameters"] != null && !string.IsNullOrEmpty(item2.Attributes["parameters"].Value))
					{
						string text = string.Empty;
						string[] array = item2.Attributes["parameters"].Value.Split(',');
						foreach (string text2 in array)
						{
							text = text2 + ",";
						}
						crmFieldEvent.Parameters = text.Remove(text.Length - 1);
					}
					list.Add(crmFieldEvent);
				}
			}
		}
		return list;
	}

	private static List<CrmFieldSecurity> ExtractFieldSecurity(string EntityName, string AttributeLogicalName)
	{
		List<CrmFieldSecurity> list = new List<CrmFieldSecurity>();
		List<FieldPermission> list2 = fieldPermissions.Where((FieldPermission p) => p.EntityName == EntityName && p.AttributeLogicalName == AttributeLogicalName).ToList();
		foreach (FieldPermission item2 in list2)
		{
			CrmFieldSecurity item = new CrmFieldSecurity
			{
				CanCreate = item2.CanCreate.Value,
				CanUpdate = item2.CanUpdate.Value,
				CanRead = item2.CanRead.Value,
				SecurityProfileName = item2.FieldSecurityProfileId.Name
			};
			list.Add(item);
		}
		return list;
	}

	private static List<CrmFieldEvent> ExtractFormEvents(string formXmlString)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(formXmlString);
		List<CrmFieldEvent> list = new List<CrmFieldEvent>();
		XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//event");
		if (xmlNodeList != null)
		{
			foreach (XmlNode item in xmlNodeList)
			{
				if (item == null)
				{
					continue;
				}
				XmlNode xmlNode2 = item.SelectSingleNode("Handlers");
				if (xmlNode2 == null)
				{
					continue;
				}
				foreach (XmlNode item2 in xmlNode2)
				{
					CrmFieldEvent crmFieldEvent = new CrmFieldEvent
					{
						IsEnabled = bool.Parse(item2.Attributes["enabled"].Value),
						LibraryName = item2.Attributes["libraryName"].Value,
						FunctionName = item2.Attributes["functionName"].Value,
						EventName = item.Attributes["name"].Value
					};
					if (item.Attributes["attribute"] != null)
					{
						crmFieldEvent.AttributeName = item.Attributes["attribute"].Value;
					}
					list.Add(crmFieldEvent);
				}
			}
		}
		return list;
	}

	public static string GetBookMarkText(string input)
	{
		int num = 38;
		if (input.Length < num)
		{
			num = input.Length;
		}
		return input.Replace("-", "_").Replace(" ", "_").Substring(0, num);
	}

	private static void ExtractField(XmlNode cellNode, List<CrmFormLabel> crmFormLabels, IEnumerable<Workflow> workFlows, Entity form, CrmForm crmForm, CrmFormTab tabName, CrmFormSection sectionName, EntityMetadata entity, int lcid, out CrmFormLabel crmFormField, IOrganizationService service)
	{
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Expected O, but got Unknown
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Invalid comparison between Unknown and I4
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Invalid comparison between Unknown and I4
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Invalid comparison between Unknown and I4
		if (cellNode.Attributes == null)
		{
			crmFormField = null;
			return;
		}
		XmlAttribute cellIdAttr = cellNode.Attributes["id"];
		if (cellIdAttr == null)
		{
			crmFormField = null;
			return;
		}
		if (cellNode.ChildNodes.Count == 0)
		{
			crmFormField = null;
			return;
		}
		XmlNode xmlNode = cellNode.SelectSingleNode("control");
		if (xmlNode == null || xmlNode.Attributes == null)
		{
			crmFormField = null;
			return;
		}
		string attribute = xmlNode.Attributes["id"].Value;
		AttributeMetadata attributeMetaData = entity.Attributes.FirstOrDefault((AttributeMetadata p) => p.LogicalName == attribute);
		string text = string.Empty;
		if (attributeMetaData != null && attributeMetaData.AttributeTypeName != (AttributeTypeDisplayName)null)
		{
			text = ((ConstantsBase<string>)(object)attributeMetaData.AttributeTypeName).Value;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = ((attribute == "notescontrol") ? "Notes Control" : ((xmlNode.FirstChild != null && xmlNode.FirstChild.FirstChild != null && xmlNode.FirstChild.FirstChild.Name == "QuickForms") ? "Quick Form" : ((!(attribute == "kbviewer")) ? "Sub-grid" : "Knowledgebase Viewer")));
		}
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		crmFormField = crmFormLabels.FirstOrDefault((CrmFormLabel f) => f.Id == new Guid(cellIdAttr.Value) && f.FormUniqueId == form.GetAttributeValue<Guid>("formidunique"));
		if (crmFormField == null)
		{
			crmFormField = new CrmFormLabel
			{
				Id = new Guid(cellIdAttr.Value),
				Form = form.GetAttributeValue<string>("name"),
				FormUniqueId = form.GetAttributeValue<Guid>("formidunique"),
				FormId = form.GetAttributeValue<Guid>("formid"),
				Tab = tabName,
				Section = sectionName,
				Entity = entity.LogicalName,
				EntityObjectTypeCode = entity.ObjectTypeCode,
				EntityId = ((MetadataBase)entity).MetadataId.Value,
				EntityDisplayName = new List<KeyValuePair<int, string>>
				{
					new KeyValuePair<int, string>(lcid, ((IEnumerable<LocalizedLabel>)entity.DisplayName.LocalizedLabels).First((LocalizedLabel p) => p.LanguageCode == lcid).Label)
				},
				Attribute = xmlNode.Attributes["id"].Value,
				AttributeType = text,
				Names = new Dictionary<int, string>(),
				Values = new Dictionary<int, string>(),
				Descriptions = new Dictionary<int, string>(),
				LabelForm = crmForm,
				IsRequired = false,
				IsRecommended = false,
				IsAudit = false,
				IsSecured = false,
				IsRollup = false,
				IsCalculated = false,
				IsReadOnly = false,
				Events = ExtractFieldEvents(xmlNode.Attributes["id"].Value, form["formxml"].ToString()),
				SecurityProfilePermissions = ExtractFieldSecurity(entity.LogicalName, xmlNode.Attributes["id"].Value),
				MetaData = new AttributeMetadata(),
				IsConfigurationEntity = selectedEntities.First((SelectedEntity p) => p.LogicalName == entity.LogicalName).IsConfigurationEntity
			};
			if (attributeMetaData is StringAttributeMetadata)
			{
				crmFormField.MaxLength = ((StringAttributeMetadata)attributeMetaData).MaxLength;
			}
			if (xmlNode.Attributes["disabled"] != null)
			{
				crmFormField.IsReadOnly = bool.Parse(xmlNode.Attributes["disabled"].Value);
			}
			if (xmlNode.Attributes["uniqueid"] != null)
			{
				crmFormField.ControlUniqueId = Guid.Parse(xmlNode.Attributes["uniqueid"].Value);
			}
			if (attributeMetaData != null)
			{
				crmFormField.MetaData = attributeMetaData;
				if (attributeMetaData is LookupAttributeMetadata)
				{
					crmFormField.RelatedEntityLogicalName = ((LookupAttributeMetadata)attributeMetaData).Targets[0];
				}
				if ((int)((ManagedProperty<AttributeRequiredLevel>)(object)attributeMetaData.RequiredLevel).Value == 1 || (int)((ManagedProperty<AttributeRequiredLevel>)(object)attributeMetaData.RequiredLevel).Value == 2)
				{
					crmFormField.IsRequired = true;
				}
				if ((int)((ManagedProperty<AttributeRequiredLevel>)(object)attributeMetaData.RequiredLevel).Value == 3)
				{
					crmFormField.IsRecommended = true;
				}
				if (((ManagedProperty<bool>)(object)attributeMetaData.IsAuditEnabled).Value)
				{
					crmFormField.IsAudit = true;
				}
				if (attributeMetaData.IsSecured.HasValue && attributeMetaData.IsSecured.Value)
				{
					crmFormField.IsSecured = true;
				}
				if (attributeMetaData.SourceType.HasValue && attributeMetaData.SourceType.Value != 0)
				{
					switch (attributeMetaData.SourceType.Value)
					{
					case 1:
						crmFormField.IsCalculated = true;
						break;
					case 2:
						crmFormField.IsRollup = true;
						break;
					}
				}
				if (attributeMetaData.Description.UserLocalizedLabel != null && !crmFormField.Descriptions.ContainsKey(attributeMetaData.Description.UserLocalizedLabel.LanguageCode))
				{
					crmFormField.Descriptions.Add(attributeMetaData.Description.UserLocalizedLabel.LanguageCode, attributeMetaData.Description.UserLocalizedLabel.Label);
				}
				List<Workflow> list = workFlows.Where((Workflow p) => p.TriggerOnUpdateAttributeList != null && p.TriggerOnUpdateAttributeList.Contains(attributeMetaData.LogicalName)).ToList();
				if (list.Any())
				{
					crmFormField.RelatedWorkflows = new List<Workflow>();
					crmFormField.RelatedWorkflows.AddRange(list);
				}
				if (((MetadataBase)attributeMetaData).MetadataId.HasValue)
				{
					crmFormField.AttributeId = ((MetadataBase)attributeMetaData).MetadataId.Value;
				}
			}
			crmFormLabels.Add(crmFormField);
		}
		else
		{
			if (crmFormField.Section == null)
			{
				crmFormField.Section = sectionName;
			}
			if (crmFormField.Tab == null)
			{
				crmFormField.Tab = tabName;
			}
			if (crmFormField.Form == string.Empty)
			{
				crmFormField.Form = form.GetAttributeValue<string>("name");
			}
			try
			{
				crmFormField.EntityDisplayName.Add(new KeyValuePair<int, string>(lcid, ((IEnumerable<LocalizedLabel>)entity.DisplayName.LocalizedLabels).First((LocalizedLabel p) => p.LanguageCode == lcid).Label));
			}
			catch (Exception)
			{
			}
		}
		XmlAttribute xmlAttribute = (cellNode.SelectSingleNode("labels/label[@languagecode='" + lcid + "']")?.Attributes)?["description"];
		if (crmFormField.Names.ContainsKey(lcid))
		{
			return;
		}
		if (attributeMetaData != null && attributeMetaData.AttributeType.HasValue)
		{
			text = attributeMetaData.AttributeType.ToString();
			if (text == "Picklist")
			{
				bool? isGlobal = ((OptionSetMetadataBase)((EnumAttributeMetadata)(PicklistAttributeMetadata)attributeMetaData).OptionSet).IsGlobal;
				crmFormField.AttributeType = text;
				if (isGlobal.HasValue && isGlobal.Value)
				{
					crmFormField.AttributeType = "Global " + text;
				}
				foreach (OptionMetadata item in ((IEnumerable<OptionMetadata>)((EnumAttributeMetadata)(PicklistAttributeMetadata)attributeMetaData).OptionSet.Options).OrderBy((OptionMetadata p) => p.Value))
				{
					if (crmFormField.Values.Count != ((Collection<OptionMetadata>)(object)((EnumAttributeMetadata)(PicklistAttributeMetadata)attributeMetaData).OptionSet.Options).Count && item.Value.HasValue)
					{
						crmFormField.Values.Add(item.Value.Value, item.Label.UserLocalizedLabel.Label);
					}
				}
			}
		}
		crmFormField.Names.Add(lcid, (xmlAttribute == null) ? string.Empty : xmlAttribute.Value);
	}

	private static CrmFormSection ExtractSection(XmlNode sectionNode, int lcid, List<CrmFormSection> crmFormSections, Entity form, string tabName, EntityMetadata entity)
	{
		if (sectionNode.Attributes == null || sectionNode.Attributes["id"] == null)
		{
			return null;
		}
		string sectionId = sectionNode.Attributes["id"].Value;
		XmlNode xmlNode = sectionNode.SelectSingleNode("labels/label[@languagecode='" + lcid + "']");
		if (xmlNode == null || xmlNode.Attributes == null)
		{
			return null;
		}
		XmlAttribute xmlAttribute = xmlNode.Attributes["description"];
		if (xmlAttribute == null)
		{
			return null;
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
				Entity = entity.LogicalName,
				Names = new Dictionary<int, string>()
			};
			crmFormSection.IsVisible = true;
			if (sectionNode.Attributes["visible"] != null)
			{
				crmFormSection.IsVisible = bool.Parse(sectionNode.Attributes["visible"].Value);
			}
			crmFormSections.Add(crmFormSection);
		}
		if (crmFormSection.Names.ContainsKey(lcid))
		{
			return crmFormSection;
		}
		crmFormSection.Names.Add(lcid, value);
		return crmFormSection;
	}

	private static CrmFormTab ExtractTabName(XmlNode tabNode, int lcid, List<CrmFormTab> crmFormTabs, Entity form, EntityMetadata entity)
	{
		if (tabNode.Attributes == null || tabNode.Attributes["id"] == null)
		{
			return null;
		}
		string tabId = tabNode.Attributes["id"].Value;
		XmlNode xmlNode = tabNode.SelectSingleNode("labels/label[@languagecode='" + lcid + "']");
		if (xmlNode == null || xmlNode.Attributes == null)
		{
			return null;
		}
		XmlAttribute xmlAttribute = xmlNode.Attributes["description"];
		if (xmlAttribute == null)
		{
			return null;
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
				Entity = entity.LogicalName,
				Names = new Dictionary<int, string>()
			};
			crmFormTab.IsVisible = true;
			if (tabNode.Attributes["visible"] != null)
			{
				crmFormTab.IsVisible = bool.Parse(tabNode.Attributes["visible"].Value);
			}
			crmFormTabs.Add(crmFormTab);
		}
		if (crmFormTab.Names.ContainsKey(lcid))
		{
			return crmFormTab;
		}
		crmFormTab.Names.Add(lcid, value);
		return crmFormTab;
	}

	private static void AddFormHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
	{
		int num = 0;
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form Unique Id";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form Id";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Entity Logical Name";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Type";
		foreach (int language in languages)
		{
			((AbstractRange)sheet.Cells[0, num++]).Value = language.ToString(CultureInfo.InvariantCulture);
		}
	}

	private static void AddFormTabHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
	{
		int num = 0;
		((AbstractRange)sheet.Cells[0, num++]).Value = "Tab Id";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Entity Logical Name";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form Name";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form Unique Id";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form Id";
		foreach (int language in languages)
		{
			((AbstractRange)sheet.Cells[0, num++]).Value = language.ToString(CultureInfo.InvariantCulture);
		}
	}

	private static void AddFormSectionHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
	{
		int num = 0;
		((AbstractRange)sheet.Cells[0, num++]).Value = "Section Id";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Entity Logical Name";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form Name";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form Unique Id";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form Id";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Tab Name";
		foreach (int language in languages)
		{
			((AbstractRange)sheet.Cells[0, num++]).Value = language.ToString(CultureInfo.InvariantCulture);
		}
	}

	private static void AddFormLabelsHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
	{
		int num = 0;
		((AbstractRange)sheet.Cells[0, num++]).Value = "Dependent Processes";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Field Logical Name";
		foreach (int language in languages)
		{
			((AbstractRange)sheet.Cells[0, num++]).Value = language.ToString(CultureInfo.InvariantCulture);
		}
		((AbstractRange)sheet.Cells[0, num++]).Value = "Description";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Type";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Section";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Tab";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Form";
		((AbstractRange)sheet.Cells[0, num++]).Value = "Entity";
		((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[0].Width = 25600;
		((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[1].Width = 7680;
		((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[2].Width = 7680;
		((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[3].Width = 7680;
		((ExcelColumnRowBase)((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[0]).Style.HorizontalAlignment = (HorizontalAlignmentStyle)3;
		((ExcelColumnRowBase)((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[1]).Style.HorizontalAlignment = (HorizontalAlignmentStyle)3;
		((ExcelColumnRowBase)((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[2]).Style.HorizontalAlignment = (HorizontalAlignmentStyle)3;
		((ExcelColumnRowBase)((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[3]).Style.HorizontalAlignment = (HorizontalAlignmentStyle)3;
		((ExcelColumnRowBase)((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[0]).Style.VerticalAlignment = (VerticalAlignmentStyle)0;
		((ExcelColumnRowBase)((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[1]).Style.VerticalAlignment = (VerticalAlignmentStyle)0;
		((ExcelColumnRowBase)((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[2]).Style.VerticalAlignment = (VerticalAlignmentStyle)0;
		((ExcelColumnRowBase)((ExcelRowColumnCollectionBase<ExcelColumn>)(object)sheet.Columns)[3]).Style.VerticalAlignment = (VerticalAlignmentStyle)0;
	}

	public static IOrganizationService Service(string organizationUrl, string userName, string password, bool isIfd = false)
	{
		return DataService.Service(organizationUrl, userName, password, isIfd);
	}

	public static void PrintFormFields()
	{
		throw new NotImplementedException();
	}

	public List<string> GetFieldDetails(string fieldLogicalName)
	{
		throw new NotImplementedException();
	}

	public List<string> FetFormDetails(string fieldLogicalName)
	{
		throw new NotImplementedException();
	}

	public List<string> GetProcessDetails(string proccessName)
	{
		throw new NotImplementedException();
	}

	public static void SerializeObject<T>(T serializableObject, string fileName)
	{
		if (serializableObject == null)
		{
			return;
		}
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlSerializer xmlSerializer = new XmlSerializer(serializableObject.GetType());
			using MemoryStream memoryStream = new MemoryStream();
			xmlSerializer.Serialize(memoryStream, serializableObject);
			memoryStream.Position = 0L;
			xmlDocument.Load(memoryStream);
			xmlDocument.Save(fileName);
			memoryStream.Close();
		}
		catch (Exception)
		{
		}
	}

	public static T DeSerializeObject<T>(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			return default(T);
		}
		T result = default(T);
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(fileName);
			string outerXml = xmlDocument.OuterXml;
			using StringReader stringReader = new StringReader(outerXml);
			Type typeFromHandle = typeof(T);
			XmlSerializer xmlSerializer = new XmlSerializer(typeFromHandle);
			using (XmlReader xmlReader = new XmlTextReader(stringReader))
			{
				result = (T)xmlSerializer.Deserialize(xmlReader);
				xmlReader.Close();
			}
			stringReader.Close();
		}
		catch (Exception)
		{
		}
		return result;
	}

	public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
	{
		TextWriter textWriter = null;
		try
		{
			string value = JsonConvert.SerializeObject((object)objectToWrite);
			textWriter = new StreamWriter(filePath, append);
			textWriter.Write(value);
		}
		catch
		{
		}
		finally
		{
			textWriter?.Close();
		}
	}

	public static T ReadFromJsonFile<T>(string filePath) where T : new()
	{
		TextReader textReader = null;
		try
		{
			textReader = new StreamReader(filePath);
			string text = textReader.ReadToEnd();
			return JsonConvert.DeserializeObject<T>(text);
		}
		finally
		{
			textReader?.Close();
		}
	}

	public static bool Send(string fromEmail, string toEmail, string fromDisplayName, string subject, string messageBody, Attachment attachment = null)
	{
		SmtpClient smtpClient = new SmtpClient();
		MailMessage mailMessage = new MailMessage();
		try
		{
			MailAddress from = new MailAddress(fromEmail, fromDisplayName);
			if (attachment != null)
			{
				mailMessage.Attachments.Add(attachment);
			}
			smtpClient.Host = "smtp.gmail.com";
			smtpClient.Port = 587;
			smtpClient.EnableSsl = true;
			mailMessage.From = from;
			mailMessage.To.Add(toEmail);
			mailMessage.Subject = subject;
			mailMessage.IsBodyHtml = false;
			mailMessage.Body = messageBody;
			smtpClient.Credentials = new NetworkCredential("technical.document.generator@gmail.com", "tgd.2016");
			smtpClient.Send(mailMessage);
			return true;
		}
		catch (SmtpException)
		{
			try
			{
				smtpClient.Credentials = new NetworkCredential("technical.document.generator@gmail.com", "tgd.2016");
				smtpClient.Send(mailMessage);
			}
			catch
			{
				return false;
			}
		}
		return false;
	}

	public static string GetLocalIPAddress()
	{
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress[] addressList = hostEntry.AddressList;
		foreach (IPAddress iPAddress in addressList)
		{
			if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				return iPAddress.ToString();
			}
		}
		throw new Exception("Local IP Address Not Found!");
	}

	public static List<EntityMetadata> GetEntities(IOrganizationService service, params string[] attributes)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		MetadataPropertiesExpression val = new MetadataPropertiesExpression
		{
			AllProperties = false
		};
		val.PropertyNames.AddRange(new string[1] { "IsIntersect" });
		val.PropertyNames.AddRange(new string[1] { "DisplayName" });
		if (attributes != null && attributes.Any())
		{
			val.PropertyNames.AddRange(attributes);
		}
		EntityQueryExpression query = new EntityQueryExpression
		{
			Properties = val
		};
		RetrieveMetadataChangesRequest val2 = new RetrieveMetadataChangesRequest
		{
			Query = query,
			ClientVersionStamp = null
		};
		RetrieveMetadataChangesResponse val3 = (RetrieveMetadataChangesResponse)service.Execute((OrganizationRequest)(object)val2);
		return ((IEnumerable<EntityMetadata>)val3.EntityMetadata).Where((EntityMetadata entity) => entity.LogicalName != null && entity.IsIntersect != true && !NonStandard.Contains(entity.LogicalName)).ToList();
	}

	public static List<EntityMetadata> RetrieveEntities(IOrganizationService oService)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		List<EntityMetadata> list = new List<EntityMetadata>();
		RetrieveAllEntitiesRequest val = new RetrieveAllEntitiesRequest
		{
			EntityFilters = (EntityFilters)1
		};
		RetrieveAllEntitiesResponse val2 = (RetrieveAllEntitiesResponse)oService.Execute((OrganizationRequest)(object)val);
		EntityMetadata[] entityMetadata = val2.EntityMetadata;
		foreach (EntityMetadata val3 in entityMetadata)
		{
			if (val3.DisplayName.UserLocalizedLabel != null && (((ManagedProperty<bool>)(object)val3.IsCustomizable).Value || !val3.IsManaged.Value))
			{
				list.Add(val3);
			}
		}
		return list;
	}

	public static EntityMetadata RetrieveEntity(string logicalName, IOrganizationService oService)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		try
		{
			RetrieveEntityRequest val = new RetrieveEntityRequest
			{
				LogicalName = logicalName,
				EntityFilters = (EntityFilters)2,
				RetrieveAsIfPublished = true
			};
			RetrieveEntityResponse val2 = (RetrieveEntityResponse)oService.Execute((OrganizationRequest)(object)val);
			return val2.EntityMetadata;
		}
		catch (Exception)
		{
			throw new Exception("Error while retrieving entity: ");
		}
	}

	public static XmlDocument RetrieveEntityForms(string logicalName, IOrganizationService oService)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		QueryByAttribute val = new QueryByAttribute("systemform");
		val.Attributes.AddRange(new string[2] { "objecttypecode", "type" });
		val.Values.AddRange(new object[2] { logicalName, 2 });
		val.ColumnSet = new ColumnSet(true);
		EntityCollection val2 = oService.RetrieveMultiple((QueryBase)(object)val);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<root>");
		foreach (Entity item in (Collection<Entity>)(object)val2.Entities)
		{
			stringBuilder.Append(item["formxml"]);
		}
		stringBuilder.Append("</root>");
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(stringBuilder.ToString());
		return xmlDocument;
	}
}
