using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DesignDocument.DAL;
using GemBox.Spreadsheet;
using Microsoft.Xrm.Sdk;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Style = Spire.Doc.Documents.Style;
using TextDirection = Spire.Doc.Documents.TextDirection;

namespace DesignDocument.BLL;

public class ExportSheet
{
	private static Table table;

	private static TableRow FRow;

	private static TableRow DataRow;

	private static Paragraph p2;

	private static TextRange TR2;

	private static Paragraph p3;

	private static TextRange TR3;

	private static Paragraph p4;

	private static TextRange TR4;

	private static Paragraph p5;

	private static TextRange TR5;

	private static Paragraph p6;

	private static TextRange TR6;

	private static Paragraph p7;

	private static TextRange TR7;

	private static Paragraph p8;

	private static TextRange TR8;

	private static Paragraph p9;

	private static TextRange TR9;

	private static Paragraph p10;

	private static TextRange TR10;

	private static Paragraph p11;

	private static TextRange TR11;

	private static Paragraph paragraphSymbols;

	private static Color main1;

	private static Color main2;

	private static Color main3;

	private static CrmConfiguration _configuration;

	public static string CrmDocumentSymbolFont = "Segoe UI Symbol";

	public static string CanCreateSymbol = "\ue160";

	public static string CanReadSymbol = "\ue18b";

	public static string CanWriteSymbol = "\ud83d\udcbe";

	public static string SeurityProfileSymbol = "\ud83d\udd11";

	public static string ReadOnlySymbol = "\ud83d\udd12";

	private static Document _document;

	private CharacterFormat SecurityProfileCharacterFormat;

	public static Font CrmDocumentFont { get; set; }

	public ExportSheet(CrmConfiguration conf, Document document)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		main1 = conf.MainColor1;
		main2 = conf.MainColor2;
		main3 = conf.MainColor3;
		_document = document;
		SecurityProfileCharacterFormat = new CharacterFormat((IDocument)(object)_document)
		{
			TextColor = Color.FromArgb(128, 96, 0),
			FontName = CrmDocumentSymbolFont
		};
		_configuration = conf;
	}

	public int PrintNonConfigurationDocument(List<int> languages, Document document, int line, CrmFormLabel crmFormLabelPrev, CrmFormLabel crmFormLabel, List<CrmFormLabel> crmFormLabels, Section section, List<RolePrivilegesViewModel> rolePrivilegesViewModels, List<Workflow> workflows, List<SdkMessageProcessingStep> sdkMessageProcessingSteps, Dictionary<Guid, Guid> parentChildGuids, CrmConfiguration conf, DocumentSettings documentSettings, bool isLast, bool isIncludeDates = false, DateTime? dateFrom = null, DateTime? dateTo = null)
	{
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		//IL_0fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fad: Expected O, but got Unknown
		//IL_0fdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe2: Expected O, but got Unknown
		//IL_1dfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_101f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1026: Expected O, but got Unknown
		//IL_21ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_108e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1095: Expected O, but got Unknown
		//IL_1352: Unknown result type (might be due to invalid IL or missing references)
		//IL_1357: Unknown result type (might be due to invalid IL or missing references)
		//IL_1363: Unknown result type (might be due to invalid IL or missing references)
		//IL_136f: Unknown result type (might be due to invalid IL or missing references)
		//IL_137c: Expected O, but got Unknown
		CrmDocumentFont = conf.Font;
		int num = 0;
		int num2 = crmFormLabels.Count((CrmFormLabel p) => p.FormId == crmFormLabel.FormId);
		int num3 = (from p in crmFormLabels
			where p.FormId == crmFormLabel.FormId
			select p into s
			select s.Tab.Id).Distinct().Count();
		int num4 = (from p in crmFormLabels
			where p.FormId == crmFormLabel.FormId
			select p into s
			select s.Section.Id).Distinct().Count();
		int crmFormLabelsCount = num2 + num3 + num4;
		string text = conf.BaseURL;
		if (text.Contains(".api"))
		{
			text = text.Replace(".api", string.Empty);
			text = text.Replace("/XRMServices", string.Empty);
		}
		string text2 = string.Concat(text, "/tools/systemcustomization/attributes/manageAttribute.aspx?attributeId=%7b", crmFormLabel.FormId, "%7d&entityId=%7b", crmFormLabel.EntityId.ToString(), "%7d");
		string text3 = string.Concat(text, "/tools/systemcustomization/attributes/manageAttribute.aspx?attributeId=%7b", crmFormLabel.AttributeId, "%7d&entityId=%7b", crmFormLabel.EntityId.ToString(), "%7d");
		string text4 = text + "/tools/systemcustomization/attributes/manageAttribute.aspx?attributeId=%7b{0}%7d&entityId=%7b" + crmFormLabel.EntityId.ToString() + "%7d";
		string text5 = string.Empty;
		string empty = string.Empty;
		ListStyle val = new ListStyle((IDocument)(object)document, (ListType)0);
		((Style)val).Name = "levelstyle";
		((Style)val).CharacterFormat.FontName = CrmDocumentFont.Name;
		val.Levels[0].PatternType = (ListPatternType)0;
		val.Levels[0].CharacterFormat.FontName = CrmDocumentFont.Name;
		val.Levels[0].ParagraphFormat.LeftIndent = 0f;
		val.Levels[1].NumberPrefix = "\0.";
		val.Levels[1].PatternType = (ListPatternType)0;
		val.Levels[1].CharacterFormat.FontName = CrmDocumentFont.Name;
		val.Levels[2].NumberPrefix = "\0.\u0001.";
		val.Levels[2].PatternType = (ListPatternType)0;
		val.Levels[2].CharacterFormat.FontName = CrmDocumentFont.Name;
		document.ListStyles.Add(val);
		if (crmFormLabel.RelatedWorkflows != null && crmFormLabel.RelatedWorkflows.Any())
		{
			text5 = string.Join(Environment.NewLine, crmFormLabel.RelatedWorkflows.Select((Workflow s) => s.Name));
		}
		string text6 = string.Empty;
		if (crmFormLabel.Values != null && crmFormLabel.Values.Any())
		{
			foreach (KeyValuePair<int, string> value in crmFormLabel.Values)
			{
				text6 = text6 + value.Value + Environment.NewLine;
			}
		}
		if (crmFormLabel.EntityDisplayName != null)
		{
			empty = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
		}
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		bool flag4 = true;
		if (crmFormLabelPrev != null)
		{
			flag = crmFormLabelPrev.Entity != crmFormLabel.Entity;
			flag2 = crmFormLabelPrev.FormId != crmFormLabel.FormId;
			flag3 = crmFormLabelPrev.Tab.Id != crmFormLabel.Tab.Id;
			flag4 = crmFormLabelPrev.Section.Id != crmFormLabel.Section.Id;
		}
		if (flag || flag2 || flag3 || flag4)
		{
			Paragraph paragraph = ((((DocumentSubsetCollection)section.Paragraphs).Count > 0) ? section.Paragraphs[0] : section.AddParagraph());
			string[] array = new string[4] { "Name", "Type", "Description", "Audit" };
			string[] array2 = new string[3] { "Option Set", "Values", "Description" };
			string[] eventHeaders = new string[3] { "Library", "Function", "Attribute" };
			if (flag)
			{
				paragraph = section.AddParagraph();
				if (line == 0)
				{
				}
				paragraph.AppendBreak((BreakType)0);
				line = 1;
				CreateHeading(string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value)), paragraph, (BuiltinStyle)1);
				paragraph.AppendBookmarkStart(crmFormLabel.Entity);
				paragraph.AppendBookmarkEnd(crmFormLabel.Entity);
				if (text5 != string.Empty)
				{
					paragraph = section.AddParagraph();
					paragraph.AppendText(text5);
				}
				if (documentSettings.EntityForms)
				{
					paragraph = section.AddParagraph();
					CreateHeading(crmFormLabel.Form, paragraph, (BuiltinStyle)2);
					paragraph = section.AddParagraph();
					CreateHeading("Form Fields", paragraph, (BuiltinStyle)3);
					CreateTable(document, section, array, crmFormLabelsCount, isAutoFit: true);
				}
			}
			else if (flag2 && documentSettings.EntityForms)
			{
				line = 1;
				IEnumerable<CrmFormLabel> source = crmFormLabels.Where((CrmFormLabel p) => p.FormId == crmFormLabelPrev.FormId);
				IEnumerable<CrmFormLabel> source2 = source.Where((CrmFormLabel p) => p.AttributeType == "Global Picklist");
				IEnumerable<CrmFormLabel> source3 = source.Where((CrmFormLabel p) => p.AttributeType == "Picklist");
				UpdateGlobalOptoinSet(document, section, table, paragraph, source2.ToList(), text);
				UpdateLocalOptoinSet(document, section, table, paragraph, source3.ToList(), text);
				PrintEvents(crmFormLabels, crmFormLabelPrev, document, paragraph, section, text, eventHeaders);
				line = 1;
				paragraph = section.AddParagraph();
				paragraph.AppendBreak((BreakType)0);
				CreateHeading(crmFormLabel.Form, paragraph, (BuiltinStyle)2);
				paragraph = section.AddParagraph();
				CreateHeading("Form Fields", paragraph, (BuiltinStyle)3);
				CreateTable(document, section, array, crmFormLabelsCount);
			}
			if (flag3 && documentSettings.EntityForms)
			{
				DataRow = table.Rows[line];
				table.ApplyHorizontalMerge(line, 0, array.Length - 1);
				p2 = ((Body)DataRow.Cells[0]).AddParagraph();
				DataRow.RowFormat.BackColor = main2;
				TR2 = p2.AppendText(string.Join(Environment.NewLine, crmFormLabel.Tab.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				((ParagraphBase)TR2).CharacterFormat.TextColor = Color.White;
				((ParagraphBase)TR2).CharacterFormat.Bold = true;
				line++;
			}
			if (flag4 && documentSettings.EntityForms)
			{
				DataRow = table.Rows[line];
				table.ApplyHorizontalMerge(line, 0, array.Length - 1);
				p2 = ((Body)DataRow.Cells[0]).AddParagraph();
				DataRow.RowFormat.BackColor = main3;
				TR2 = p2.AppendText(string.Join(Environment.NewLine, crmFormLabel.Section.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				((ParagraphBase)TR2).CharacterFormat.TextColor = Color.White;
				((ParagraphBase)TR2).CharacterFormat.Bold = true;
				line++;
			}
		}
		if (documentSettings.EntityForms)
		{
			DataRow = table.Rows[line];
			p5 = ((Body)DataRow.Cells[0]).AddParagraph();
			string text7 = string.Empty;
			string text8 = string.Empty;
			if (crmFormLabel.IsSecured)
			{
				text7 = SeurityProfileSymbol;
			}
			if (crmFormLabel.IsReadOnly)
			{
				text8 = ReadOnlySymbol;
			}
			string fieldTags = GetFieldTags(crmFormLabel);
			if (crmFormLabel.IsRequired)
			{
				foreach (KeyValuePair<int, string> name in crmFormLabel.Names)
				{
					((ParagraphBase)p5.AppendText(text7)).ApplyCharacterFormat(SecurityProfileCharacterFormat);
					p5.AppendText(name.Value);
					((ParagraphBase)p5.AppendText("*")).CharacterFormat.TextColor = Color.Red;
					((ParagraphBase)p5.AppendText(text8)).CharacterFormat.FontName = CrmDocumentSymbolFont;
					p5.AppendText(Environment.NewLine);
				}
			}
			else if (crmFormLabel.IsRecommended)
			{
				foreach (KeyValuePair<int, string> name2 in crmFormLabel.Names)
				{
					((ParagraphBase)p5.AppendText(text7)).ApplyCharacterFormat(SecurityProfileCharacterFormat);
					p5.AppendText(name2.Value);
					((ParagraphBase)p5.AppendText("†")).CharacterFormat.TextColor = main2;
					((ParagraphBase)p5.AppendText(text8)).CharacterFormat.FontName = CrmDocumentSymbolFont;
					p5.AppendText(Environment.NewLine);
				}
			}
			else
			{
				foreach (KeyValuePair<int, string> name3 in crmFormLabel.Names)
				{
					((ParagraphBase)p5.AppendText(text7)).ApplyCharacterFormat(SecurityProfileCharacterFormat);
					p5.AppendText(name3.Value);
					((ParagraphBase)p5.AppendText(text8)).CharacterFormat.FontName = CrmDocumentSymbolFont;
					p5.AppendText(Environment.NewLine);
				}
			}
			if (crmFormLabel.AttributeType != string.Empty)
			{
				p2 = ((Body)DataRow.Cells[1]).AddParagraph();
				if (crmFormLabel.AttributeType.EndsWith("Type"))
				{
					crmFormLabel.AttributeType = crmFormLabel.AttributeType.Substring(0, crmFormLabel.AttributeType.Length - 4);
				}
				TR2 = p2.AppendText(crmFormLabel.AttributeType);
				if (crmFormLabel.AttributeType.Contains("String") && crmFormLabel.MaxLength.HasValue)
				{
					p2.AppendText($" MAX({crmFormLabel.MaxLength.Value.ToString()})");
				}
				else if (crmFormLabel.RelatedEntityLogicalName != string.Empty && crmFormLabel.AttributeType.Contains("Lookup"))
				{
					p2.AppendText(" On ");
					p2.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabel.RelatedEntityLogicalName), crmFormLabel.RelatedEntityLogicalName, (HyperlinkType)4);
				}
				if (crmFormLabel.IsCalculated)
				{
					p2.AppendText(" (Calculated)");
				}
				if (crmFormLabel.IsRollup)
				{
					p2.AppendText(" (Roll-up)");
				}
			}
			p2 = ((Body)DataRow.Cells[1]).AddParagraph();
			if (crmFormLabel.AttributeId != default(Guid))
			{
				TR2 = (TextRange)(object)p2.AppendHyperlink(text3, crmFormLabel.Attribute, (HyperlinkType)2);
			}
			else
			{
				TR2 = p2.AppendText(crmFormLabel.Attribute);
			}
			p2.AppendBookmarkStart(DataManager.GetBookMarkText(crmFormLabel.FormId.ToString().Substring(0, 7) + crmFormLabel.AttributeId.ToString()));
			p2.AppendBookmarkEnd(DataManager.GetBookMarkText(crmFormLabel.FormId.ToString().Substring(0, 7) + crmFormLabel.AttributeId.ToString()));
			if (crmFormLabel.Values.Any() && crmFormLabel.AttributeType.EndsWith("Picklist"))
			{
				p2 = ((Body)DataRow.Cells[1]).AddParagraph();
				p2.AppendText("Values");
				p2 = ((Body)DataRow.Cells[1]).AddParagraph();
				StructureDocumentTagInline val2 = new StructureDocumentTagInline(document);
				((DocumentObject)p2).ChildObjects.Add((IDocumentObject)(object)val2);
				val2.SDTProperties.SDTType = (SdtType)3;
				((ParagraphBase)val2).CharacterFormat.Bold = true;
				SdtComboBox val3 = new SdtComboBox();
				string empty2 = string.Empty;
				foreach (KeyValuePair<int, string> value2 in crmFormLabel.Values)
				{
					SdtListItem val4 = new SdtListItem(value2.Value, value2.Key.ToString());
					((SdtDropDownListBase)val3).ListItems.Add(val4);
				}
				((SdtDropDownListBase)val3).ListItems.SelectedValue = ((SdtDropDownListBase)val3).ListItems[0];
				empty2 = ((SdtDropDownListBase)val3).ListItems[0].DisplayText;
				val2.SDTProperties.ControlProperties = (SdtControlProperties)(object)val3;
				TextRange val5 = new TextRange((IDocument)(object)document);
				val5.Text = empty2;
				((ParagraphBase)val5).CharacterFormat.Bold = true;
				((DocumentObject)val2.SDTContent).ChildObjects.Add((IDocumentObject)(object)val5);
			}
			if (crmFormLabel.Descriptions.Any() && !string.IsNullOrEmpty(crmFormLabel.Descriptions.First().Value))
			{
				p3 = ((Body)DataRow.Cells[2]).AddParagraph();
				((ParagraphBase)p3.AppendText("Description: ")).CharacterFormat.Bold = true;
				p3 = ((Body)DataRow.Cells[2]).AddParagraph();
				p3.AppendText((crmFormLabel.Descriptions.Count > 1) ? string.Format(Environment.NewLine, crmFormLabel.Descriptions.Select((KeyValuePair<int, string> s) => s.Value)) : crmFormLabel.Descriptions.First().Value);
			}
			if (crmFormLabel.SecurityProfilePermissions.Any((CrmFieldSecurity p) => p.SecurityProfileName != "System Administrator"))
			{
				p3 = ((Body)DataRow.Cells[2]).AddParagraph();
				((ParagraphBase)p3.AppendText("Permissions: ")).CharacterFormat.Bold = true;
				p3 = ((Body)DataRow.Cells[2]).AddParagraph();
				foreach (CrmFieldSecurity item in crmFormLabel.SecurityProfilePermissions.Where((CrmFieldSecurity p) => p.SecurityProfileName != "System Administrator"))
				{
					string arg = string.Empty;
					string arg2 = string.Empty;
					string arg3 = string.Empty;
					if (item.CanCreate != 0)
					{
						arg = CanCreateSymbol;
					}
					if (item.CanUpdate != 0)
					{
						arg2 = CanWriteSymbol;
					}
					if (item.CanRead != 0)
					{
						arg3 = CanReadSymbol;
					}
					((ParagraphBase)p3.AppendText(SeurityProfileSymbol)).ApplyCharacterFormat(SecurityProfileCharacterFormat);
					((ParagraphBase)p3.AppendText(" " + item.SecurityProfileName)).CharacterFormat.FontName = CrmDocumentFont.Name;
					((ParagraphBase)p3.AppendText($" {arg}{arg2}{arg3}")).ApplyCharacterFormat(new CharacterFormat((IDocument)(object)document)
					{
						FontName = CrmDocumentSymbolFont,
						TextColor = Color.DarkRed,
						Bold = true
					});
					p3.AppendText(Environment.NewLine);
				}
			}
			if (crmFormLabel.RelatedWorkflows != null && crmFormLabel.RelatedWorkflows.Count() > 0)
			{
				p3 = ((Body)DataRow.Cells[2]).AddParagraph();
				((ParagraphBase)p3.AppendText("Dependent Processes: ")).CharacterFormat.Bold = true;
				foreach (Workflow relatedWorkflow in crmFormLabel.RelatedWorkflows)
				{
					p3 = ((Body)DataRow.Cells[2]).AddParagraph();
					p3.AppendHyperlink(DataManager.GetBookMarkText(((Entity)relatedWorkflow).Id.ToString()), "■ " + relatedWorkflow.Name, (HyperlinkType)4);
				}
			}
			if (crmFormLabel.Events != null && crmFormLabel.Events.Count() > 0 && documentSettings.EntityScripts)
			{
				p3 = ((Body)DataRow.Cells[2]).AddParagraph();
				((ParagraphBase)p3.AppendText("JavaScript: ")).CharacterFormat.Bold = true;
				string empty3 = string.Empty;
				string empty4 = string.Empty;
				foreach (CrmFieldEvent item2 in crmFormLabel.Events.OrderBy((CrmFieldEvent o) => o.LibraryName).ThenBy((CrmFieldEvent t) => t.EventName))
				{
					p3 = ((Body)DataRow.Cells[2]).AddParagraph();
					string text9 = string.Empty;
					if (item2.IsExecutionContext)
					{
						text9 = text9 + Environment.NewLine + "(ExecutionContext";
					}
					if (!string.IsNullOrEmpty(item2.Parameters))
					{
						text9 = (item2.IsExecutionContext ? (text9 + ",") : (text9 + Environment.NewLine + "("));
						text9 += item2.Parameters;
					}
					if (!string.IsNullOrEmpty(item2.Parameters) || item2.IsExecutionContext)
					{
						text9 += ")";
					}
					string text10 = "■ " + item2.FunctionName;
					if (!item2.IsEnabled)
					{
						((ParagraphBase)p3.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabel.EntityId.ToString().Substring(0, 7) + crmFormLabel.FormId.ToString().Substring(0, 7) + item2.FunctionName), text10, (HyperlinkType)4)).CharacterFormat.IsStrikeout = true;
						((ParagraphBase)p3.AppendText(text9)).CharacterFormat.Italic = true;
					}
					else
					{
						p3.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabel.EntityId.ToString().Substring(0, 7) + crmFormLabel.FormId.ToString().Substring(0, 7) + item2.FunctionName), text10, (HyperlinkType)4);
					}
					((ParagraphBase)p3.AppendText(text9)).CharacterFormat.Italic = true;
					empty4 = item2.LibraryName;
					empty3 = item2.EventName;
				}
			}
			if (!crmFormLabel.IsAudit)
			{
				p4 = ((Body)DataRow.Cells[3]).AddParagraph();
				DataRow.Cells[3].CellFormat.VerticalAlignment = (VerticalAlignment)1;
				p4.Format.HorizontalAlignment = (HorizontalAlignment)1;
				TR4 = p4.AppendText("Audit✘");
				((ParagraphBase)TR4).CharacterFormat.Bold = true;
				((ParagraphBase)TR4).CharacterFormat.SubSuperScript = (SubSuperScript)1;
			}
		}
		if (isLast)
		{
			Paragraph val6 = ((((DocumentSubsetCollection)section.Paragraphs).Count > 0) ? section.Paragraphs[0] : section.AddParagraph());
			string[] array3 = new string[5] { "Type", "Description", "Logical Name", "1033", "1025" };
			string[] rolePrivilegesHeaders = new string[10] { "Role Name", "Create", "Read", "Write", "Delete", "Append", "Append To", "Assign", "Share", "Reparent" };
			string[] eventHeaders2 = new string[3] { "Library", "Function", "Attribute" };
			string[] workflowHeaders = new string[2] { "Workflow", "Description" };
			string[] formHeaders = new string[3] { "Plugin", "Stage", "Message" };
			string[] array4 = new string[1] { "Name" };
			string[] array5 = new string[3] { "Option Set", "Values", "Description" };
			line = 1;
			IEnumerable<CrmFormLabel> source4 = crmFormLabels.Where((CrmFormLabel p) => p.FormId == crmFormLabelPrev.FormId);
			IEnumerable<CrmFormLabel> source5 = source4.Where((CrmFormLabel p) => p.AttributeType == "Global Picklist");
			IEnumerable<CrmFormLabel> source6 = source4.Where((CrmFormLabel p) => p.AttributeType == "Picklist");
			if (documentSettings.EntityGlobalOptionSets)
			{
				UpdateGlobalOptoinSet(document, section, table, val6, source5.ToList(), text);
			}
			if (documentSettings.EntityLocalOptionSets)
			{
				UpdateLocalOptoinSet(document, section, table, val6, source6.ToList(), text);
			}
			if (documentSettings.EntityScripts)
			{
				PrintEvents(crmFormLabels, crmFormLabelPrev, document, val6, section, text, eventHeaders2);
			}
			line = 1;
			string text11 = string.Empty;
			int num5 = 0;
			if (sdkMessageProcessingSteps.Any((SdkMessageProcessingStep p) => ((OptionSetValue)((DataCollection<string, object>)(object)((Entity)p).Attributes)["mode"]).Value == 0 && ((AliasedValue)((Entity)p)["sdkmessagefilter.primaryobjecttypecode"]).Value.ToString() == crmFormLabel.Entity && !p.Name.StartsWith("ActivityFeeds.Plugins")))
			{
				val6 = section.AddParagraph();
				val6.AppendBreak((BreakType)0);
				CreateHeading("Sync Plugins", val6, (BuiltinStyle)2);
				IOrderedEnumerable<SdkMessageProcessingStep> orderedEnumerable = from p in sdkMessageProcessingSteps
					where ((OptionSetValue)((DataCollection<string, object>)(object)((Entity)p).Attributes)["mode"]).Value == 0 && ((AliasedValue)((Entity)p)["sdkmessagefilter.primaryobjecttypecode"]).Value.ToString() == crmFormLabel.Entity && !p.Name.StartsWith("ActivityFeeds.Plugins")
					select p into o
					orderby o.EventHandler.Name, ((EntityReference)((AliasedValue)((Entity)o)["sdkmessagefilter.sdkmessageid"]).Value).Name
					select o;
				CreateTable(document, section, formHeaders, orderedEnumerable.Count());
				foreach (SdkMessageProcessingStep item3 in orderedEnumerable)
				{
					DataRow = table.Rows[line];
					if (text11 == item3.EventHandler.Name)
					{
						table.ApplyVerticalMerge(0, num5, line);
					}
					else
					{
						p2 = ((Body)DataRow.Cells[0]).AddParagraph();
						if (item3.StateCode.Value == SdkMessageProcessingStepState.Disabled)
						{
							((ParagraphBase)p2.AppendText(item3.EventHandler.Name)).CharacterFormat.IsStrikeout = true;
						}
						else
						{
							TR2 = p2.AppendText(item3.EventHandler.Name);
						}
						num5 = line;
					}
					text11 = item3.EventHandler.Name;
					p3 = ((Body)DataRow.Cells[1]).AddParagraph();
					p3.AppendText(SdkMessageProcessingStep.Enums.GetLabel("stage", item3.Stage.Value));
					if (!string.IsNullOrEmpty(item3.FilteringAttributes))
					{
						List<string> list = new List<string>();
						p2 = ((Body)DataRow.Cells[0]).AddParagraph();
						p2.AppendText("Filtering Attributes: ");
						IEnumerable<string> enumerable = from s in item3.FilteringAttributes.Split(',')
							select (s);
						foreach (string s2 in enumerable)
						{
							p2 = ((Body)DataRow.Cells[0]).AddParagraph();
							CrmFormLabel crmFormLabel2 = crmFormLabels.FirstOrDefault((CrmFormLabel p) => p.Attribute == s2);
							if (crmFormLabel2 != null)
							{
								string text12 = string.Format(text4, crmFormLabel2.AttributeId);
								p2.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabel.FormId.ToString().Substring(0, 7) + crmFormLabel2.AttributeId.ToString()), "■ " + crmFormLabel2.Names.First().Value, (HyperlinkType)4);
							}
							else
							{
								p2.AppendText("■ " + s2);
							}
						}
					}
					p4 = ((Body)DataRow.Cells[2]).AddParagraph();
					p4.AppendText(((EntityReference)((AliasedValue)((Entity)item3)["sdkmessagefilter.sdkmessageid"]).Value).Name);
					line++;
				}
			}
			line = 1;
			text11 = string.Empty;
			num5 = 0;
			if (sdkMessageProcessingSteps.Any((SdkMessageProcessingStep p) => ((OptionSetValue)((DataCollection<string, object>)(object)((Entity)p).Attributes)["mode"]).Value == 1 && ((AliasedValue)((Entity)p)["sdkmessagefilter.primaryobjecttypecode"]).Value.ToString() == crmFormLabel.Entity))
			{
				val6 = section.AddParagraph();
				val6.AppendBreak((BreakType)0);
				CreateHeading("Async Plugins", val6, (BuiltinStyle)2);
				IOrderedEnumerable<SdkMessageProcessingStep> orderedEnumerable2 = from p in sdkMessageProcessingSteps
					where ((OptionSetValue)((DataCollection<string, object>)(object)((Entity)p).Attributes)["mode"]).Value == 1 && ((AliasedValue)((Entity)p)["sdkmessagefilter.primaryobjecttypecode"]).Value.ToString() == crmFormLabel.Entity && !p.Name.StartsWith("ActivityFeeds.Plugins")
					select p into o
					orderby o.EventHandler.Name, ((EntityReference)((AliasedValue)((Entity)o)["sdkmessagefilter.sdkmessageid"]).Value).Name
					select o;
				CreateTable(document, section, formHeaders, orderedEnumerable2.Count());
				foreach (SdkMessageProcessingStep item4 in orderedEnumerable2)
				{
					DataRow = table.Rows[line];
					if (text11 == item4.EventHandler.Name)
					{
						table.ApplyVerticalMerge(0, num5, line);
					}
					else
					{
						p2 = ((Body)DataRow.Cells[0]).AddParagraph();
						if (item4.StateCode.Value == SdkMessageProcessingStepState.Disabled)
						{
							((ParagraphBase)p2.AppendText(item4.EventHandler.Name)).CharacterFormat.IsStrikeout = true;
						}
						else
						{
							TR2 = p2.AppendText(item4.EventHandler.Name);
						}
						num5 = line;
					}
					text11 = item4.EventHandler.Name;
					p3 = ((Body)DataRow.Cells[1]).AddParagraph();
					p3.AppendText(SdkMessageProcessingStep.Enums.GetLabel("stage", item4.Stage.Value));
					if (!string.IsNullOrEmpty(item4.FilteringAttributes))
					{
						List<string> list2 = new List<string>();
						p2 = ((Body)DataRow.Cells[0]).AddParagraph();
						p2.AppendText("Filtering Attributes: ");
						IEnumerable<string> enumerable2 = from s in item4.FilteringAttributes.Split(',')
							select (s);
						foreach (string s3 in enumerable2)
						{
							p2 = ((Body)DataRow.Cells[0]).AddParagraph();
							CrmFormLabel crmFormLabel3 = crmFormLabels.FirstOrDefault((CrmFormLabel p) => p.Attribute == s3);
							if (crmFormLabel3 != null)
							{
								string text13 = string.Format(text4, crmFormLabel3.AttributeId);
								p2.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabel.FormId.ToString().Substring(0, 7) + crmFormLabel3.AttributeId.ToString()), "■ " + crmFormLabel3.Names.First().Value, (HyperlinkType)4);
							}
							else
							{
								p2.AppendText("■ " + s3);
							}
						}
					}
					p4 = ((Body)DataRow.Cells[2]).AddParagraph();
					p4.AppendText(((EntityReference)((AliasedValue)((Entity)item4)["sdkmessagefilter.sdkmessageid"]).Value).Name);
					line++;
				}
			}
			line = 1;
			PrintWorkflow(workflows, crmFormLabel, document, val6, section, workflowHeaders, line, conf, crmFormLabels, text4, 2, 1, text);
			line = 1;
			PrintWorkflow(workflows, crmFormLabel, document, val6, section, workflowHeaders, line, conf, crmFormLabels, text4, 1, 0, text);
			line = 1;
			PrintWorkflow(workflows, crmFormLabel, document, val6, section, workflowHeaders, line, conf, crmFormLabels, text4, 3, 0, text);
			line = 1;
			PrintWorkflow(workflows, crmFormLabel, document, val6, section, workflowHeaders, line, conf, crmFormLabels, text4, 4, 0, text);
			line = 1;
			PrintWorkflow(workflows, crmFormLabel, document, val6, section, workflowHeaders, line, conf, crmFormLabels, text4, 0, 1, text);
			line = 1;
			PrintWorkflow(workflows, crmFormLabel, document, val6, section, workflowHeaders, line, conf, crmFormLabels, text4, 0, 0, text);
			line = 1;
			PrintSecurityRoles(crmFormLabels, (from p in rolePrivilegesViewModels
				where p.EntityLogicalName == crmFormLabel.Entity
				orderby p.RoleName
				select p).ToList(), document, val6, section, text, rolePrivilegesHeaders);
			line = 1;
		}
		line++;
		return line;
	}

	internal int PrintConfigurationDocument(int line, Document document, Section section, List<Entity> entityRecords, List<CrmFormLabel> configurationFormLabels, int recordsIndex)
	{
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Expected O, but got Unknown
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Expected O, but got Unknown
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Invalid comparison between O and Unknown
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Expected O, but got Unknown
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Expected O, but got Unknown
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Expected O, but got Unknown
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Expected O, but got Unknown
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Expected O, but got Unknown
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Expected O, but got Unknown
		string text = _configuration.BaseURL;
		if (text.Contains(".api"))
		{
			text = text.Replace(".api", string.Empty);
			text = text.Replace("/XRMServices", string.Empty);
		}
		string value = configurationFormLabels.FirstOrDefault((CrmFormLabel p) => p.Entity == entityRecords[0].LogicalName).EntityDisplayName.First().Value;
		Paragraph val = section.AddParagraph();
		CreateHeading(value + " Records", val, (BuiltinStyle)2);
		if (recordsIndex != 0)
		{
			val.AppendBreak((BreakType)0);
		}
		int num = 0;
		foreach (Entity entityRecord in entityRecords)
		{
			line = 1;
			val = section.AddParagraph();
			if (num != 0)
			{
				val.AppendBreak((BreakType)0);
			}
			string empty = string.Empty;
			empty = (((IEnumerable<KeyValuePair<string, object>>)entityRecord.Attributes).Any((KeyValuePair<string, object> p) => p.Key.EndsWith("name") && p.Value is string) ? ((IEnumerable<KeyValuePair<string, object>>)entityRecord.Attributes).First((KeyValuePair<string, object> p) => p.Key.EndsWith("name") && p.Value is string).Value.ToString() : ((IEnumerable<KeyValuePair<string, object>>)entityRecord.Attributes).First((KeyValuePair<string, object> p) => p.Value is string).Value.ToString());
			CreateHeading(empty, val, (BuiltinStyle)3);
			string[] array = new string[2] { "Key", "Value" };
			IEnumerable<CrmFormLabel> enumerable = configurationFormLabels.Where((CrmFormLabel p) => ((IEnumerable<KeyValuePair<string, object>>)entityRecord.Attributes).Select((KeyValuePair<string, object> s) => s.Key).Contains(p.Attribute));
			int num2 = enumerable.Select((CrmFormLabel p) => p.Tab.Id).Distinct().Count();
			int num3 = enumerable.Select((CrmFormLabel p) => p.Section.Id).Distinct().Count();
			int crmFormLabelsCount = enumerable.Count() + num2 + num3;
			ITable val2 = (ITable)(object)CreateTable(document, section, array, crmFormLabelsCount);
			CrmFormLabel crmFormLabel = new CrmFormLabel();
			foreach (CrmFormLabel crmFormLabel2 in enumerable)
			{
				bool isRequired = crmFormLabel2.IsRequired;
				bool flag = false;
				KeyValuePair<string, object> keyValuePair = ((IEnumerable<KeyValuePair<string, object>>)entityRecord.Attributes).FirstOrDefault((KeyValuePair<string, object> p) => p.Key == crmFormLabel2.Attribute);
				if (crmFormLabel.Tab != crmFormLabel2.Tab)
				{
					DataRow = val2.Rows[line];
					val2.ApplyHorizontalMerge(line, 0, array.Length - 1);
					p2 = ((Body)DataRow.Cells[0]).AddParagraph();
					DataRow.RowFormat.BackColor = main2;
					TR2 = p2.AppendText(string.Join(Environment.NewLine, crmFormLabel2.Tab.Names.Select((KeyValuePair<int, string> s) => s.Value)));
					((ParagraphBase)TR2).CharacterFormat.TextColor = Color.White;
					((ParagraphBase)TR2).CharacterFormat.Bold = true;
					line++;
				}
				if (crmFormLabel.Section != crmFormLabel2.Section)
				{
					DataRow = val2.Rows[line];
					val2.ApplyHorizontalMerge(line, 0, array.Length - 1);
					p2 = ((Body)DataRow.Cells[0]).AddParagraph();
					DataRow.RowFormat.BackColor = main3;
					TR2 = p2.AppendText(string.Join(Environment.NewLine, crmFormLabel2.Section.Names.Select((KeyValuePair<int, string> s) => s.Value)));
					((ParagraphBase)TR2).CharacterFormat.TextColor = Color.White;
					((ParagraphBase)TR2).CharacterFormat.Bold = true;
					line++;
				}
				if (crmFormLabel2 != null)
				{
					DataRow = val2.Rows[line];
					p2 = ((Body)DataRow.Cells[0]).AddParagraph();
					((ParagraphBase)p2.AppendText(crmFormLabel2.Names.First().Value)).ApplyCharacterFormat(new CharacterFormat((IDocument)(object)document)
					{
						FontName = CrmDocumentSymbolFont,
						Bold = true
					});
					if (crmFormLabel2.IsRequired)
					{
						((ParagraphBase)p2.AppendText("*")).CharacterFormat.TextColor = Color.Red;
					}
					if (crmFormLabel2.IsReadOnly)
					{
						((ParagraphBase)p2.AppendText(ReadOnlySymbol)).CharacterFormat.FontName = CrmDocumentSymbolFont;
					}
					p3 = ((Body)DataRow.Cells[1]).AddParagraph();
					flag = keyValuePair.Value == null;
					if (keyValuePair.Value is EntityReference)
					{
						EntityReference val3 = (EntityReference)keyValuePair.Value;
						string text2 = string.Concat(text, "main.aspx/etn=", val3.LogicalName, "&pagetype=entityrecord&id={", val3.Id, "}");
						p3.AppendHyperlink(text2, val3.Name, (HyperlinkType)2);
						flag = (object)val3 == (object)new EntityReference() || val3 == null;
					}
					else if (keyValuePair.Value is OptionSetValue)
					{
						OptionSetValue val4 = (OptionSetValue)keyValuePair.Value;
						StructureDocumentTagInline val5 = new StructureDocumentTagInline(document);
						((DocumentObject)p3).ChildObjects.Add((IDocumentObject)(object)val5);
						val5.SDTProperties.SDTType = (SdtType)3;
						SdtComboBox val6 = new SdtComboBox();
						string text3 = string.Empty;
						foreach (KeyValuePair<int, string> value2 in crmFormLabel2.Values)
						{
							SdtListItem val7 = new SdtListItem(value2.Value, value2.Key.ToString());
							((SdtDropDownListBase)val6).ListItems.Add(val7);
							if (value2.Key == val4.Value)
							{
								((SdtDropDownListBase)val6).ListItems.SelectedValue = val7;
								text3 = val7.DisplayText;
							}
						}
						val5.SDTProperties.ControlProperties = (SdtControlProperties)(object)val6;
						TextRange val8 = new TextRange((IDocument)(object)document);
						val8.Text = text3;
						((DocumentObject)val5.SDTContent).ChildObjects.Add((IDocumentObject)(object)val8);
					}
					else if (keyValuePair.Value is Money)
					{
						Money val9 = (Money)keyValuePair.Value;
						p3.AppendText(val9.Value.ToString("#.##"));
					}
					else if (keyValuePair.Value is decimal)
					{
						decimal num4 = (decimal)keyValuePair.Value;
						p3.AppendText(num4.ToString("#.##"));
					}
					else
					{
						flag = string.IsNullOrEmpty(keyValuePair.Value.ToString());
						p3.AppendText(keyValuePair.Value.ToString());
					}
					if (flag && isRequired)
					{
						DataRow.RowFormat.BackColor = Color.Red;
					}
					line++;
				}
				crmFormLabel = crmFormLabel2;
			}
			num++;
		}
		return line;
	}

	public static string GetFieldTags(CrmFormLabel crmFormLabel)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		if (crmFormLabel.IsSecured)
		{
			text = SeurityProfileSymbol;
		}
		if (crmFormLabel.IsCalculated)
		{
			text3 = "⍯";
		}
		if (crmFormLabel.IsRollup)
		{
			text2 = "\ud83d\udd03";
		}
		if (crmFormLabel.IsReadOnly)
		{
			text4 = ReadOnlySymbol;
		}
		return text4 + text + text3 + text2;
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

	public static string GetWorkflowURL(Workflow workflow, string baseURL)
	{
		string format = baseURL + "/sfa/workflow/edit.aspx?id=%7b{0}%7d";
		string format2 = baseURL + "/tools/ProcessControl/bpfConfigurator.aspx?id=%7b{0}%7d";
		int value = workflow.Category.Value;
		if (value == 4)
		{
			return string.Format(format2, workflow.WorkflowId);
		}
		return string.Format(format, workflow.WorkflowId);
	}

	public static Table CreateTable(Document document, Section section, string[] formHeaders, int crmFormLabelsCount, bool isAutoFit = false, bool isFlip = false)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		table = new Table((IDocument)(object)document);
		table = section.Body.AddTable(true);
		table.ResetCells(crmFormLabelsCount + 1, formHeaders.Length);
		FRow = table.Rows[0];
		FRow.RowFormat.BackColor = main1;
		for (int i = 0; i < formHeaders.Length; i++)
		{
			if (isFlip || formHeaders[i] == "Audit")
			{
				if (formHeaders[i] != "Role Name")
				{
					FRow.Cells[i].CellFormat.TextDirection = (TextDirection)5;
				}
				FRow.Height = 54f;
				if (formHeaders[i] == "Audit")
				{
					FRow.Cells[i].SetCellWidth(20f, (CellWidthType)3);
					FRow.Height = 34f;
				}
			}
			Paragraph val = ((Body)FRow.Cells[i]).AddParagraph();
			FRow.Cells[i].CellFormat.VerticalAlignment = (VerticalAlignment)1;
			val.Format.HorizontalAlignment = (HorizontalAlignment)1;
			TextRange val2 = val.AppendText(formHeaders[i]);
			((ParagraphBase)val2).CharacterFormat.Bold = true;
			((ParagraphBase)val2).CharacterFormat.TextColor = Color.White;
		}
		table.TableFormat.Borders.Color = Color.LightGray;
		table.TableFormat.Borders.Vertical.Color = Color.LightGray;
		table.TableFormat.Borders.Horizontal.Color = Color.LightGray;
		if (isAutoFit)
		{
			//table.AutoFitBehavior((AutoFitBehaviorType)1);
		}
		return table;
	}

	public static string GetScopeName(int scope)
	{
		return scope switch
		{
			1 => "User", 
			2 => "Business Unit", 
			3 => "Parent: Child Business Units", 
			4 => "Organization", 
			_ => string.Empty, 
		};
	}

	private static string GetAttributeDescription(AttributeCollection attr)
	{
		string text = string.Empty;
		if ((bool)((DataCollection<string, object>)(object)attr)["subprocess"])
		{
			text += "#CHILD";
		}
		if ((bool)((DataCollection<string, object>)(object)attr)["ondemand"])
		{
			text += "#ONDEMAND";
		}
		if ((bool)((DataCollection<string, object>)(object)attr)["triggeroncreate"])
		{
			text += "#ONCREATE";
		}
		if ((bool)((DataCollection<string, object>)(object)attr)["triggerondelete"])
		{
			text += "#ONDELETE";
		}
		if (!(bool)((DataCollection<string, object>)(object)attr)["triggerondelete"] && !(bool)((DataCollection<string, object>)(object)attr)["triggeroncreate"] && !(bool)((DataCollection<string, object>)(object)attr)["subprocess"])
		{
			int num = ((DataCollection<string, object>)(object)attr)["clientdata"].ToString().IndexOf("ldv_", StringComparison.Ordinal) + 4;
			int num2 = ((DataCollection<string, object>)(object)attr)["clientdata"].ToString().LastIndexOf("')", StringComparison.Ordinal);
			string text2 = ((DataCollection<string, object>)(object)attr)["clientdata"].ToString().Substring(num, num2 - num);
			text += "#ONUPDATE";
		}
		return " " + text;
	}

	private static string GetAttributeDescription(Workflow attr)
	{
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		List<string> list = new List<string>();
		if ((bool)((Entity)attr)["triggeroncreate"])
		{
			list.Add("#CREATE");
		}
		if ((bool)((Entity)attr)["triggerondelete"])
		{
			list.Add("#DELETE");
		}
		if (attr.TriggerOnUpdateAttributeList != null || (!(bool)((Entity)attr)["triggerondelete"] && !(bool)((Entity)attr)["triggeroncreate"] && !(bool)((Entity)attr)["subprocess"]))
		{
			list.Add("#UPDATE");
		}
		if ((bool)((Entity)attr)["subprocess"])
		{
			list.Add("#CHILD");
		}
		if ((bool)((Entity)attr)["ondemand"])
		{
			list.Add("#ONDEMAND");
		}
		if (((OptionSetValue)((Entity)attr)["category"]).Value != 0)
		{
			List<string> list2 = new List<string>();
			list2.Add("#" + Enum.GetName(typeof(Workflow.WorkflowCategory), ((OptionSetValue)((Entity)attr)["category"]).Value).ToUpper());
			list = list2;
		}
		return string.Join(Environment.NewLine, list);
	}

	public void PrintSymbolsTable(Document document, Paragraph paragraph, Section section)
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		_document = document;
		Dictionary<string, string> dictionary = new Dictionary<string, string>
		{
			{ "*", "Required field." },
			{ "†", "Business required field." },
			{ ReadOnlySymbol, "Read-only field." },
			{ "✘ Audit", "Audit is disabled for this field." },
			{ SeurityProfileSymbol, "Field security is enabled for this field." },
			{ CanCreateSymbol, "Create permission." },
			{ CanWriteSymbol, "Update permission." },
			{ CanReadSymbol, "Read permission." }
		};
		int num = 1;
		paragraph = section.AddParagraph();
		paragraph.AppendText("Table of Symbols");
		paragraph.ApplyStyle((BuiltinStyle)2);
		CreateTable(document, section, new string[2] { "Symbol", "Description" }, dictionary.Count, isAutoFit: true);
		foreach (KeyValuePair<string, string> item in dictionary)
		{
			DataRow = table.Rows[num];
			p2 = ((Body)DataRow.Cells[0]).AddParagraph();
			((ParagraphBase)p2.AppendText(item.Key)).ApplyCharacterFormat(new CharacterFormat((IDocument)(object)document)
			{
				FontName = CrmDocumentSymbolFont,
				TextColor = Color.DarkRed,
				Bold = true
			});
			p3 = ((Body)DataRow.Cells[1]).AddParagraph();
			p3.AppendText(item.Value);
			num++;
		}
	}

	public void PrintSecurityRoleColorsTable(Document document, Paragraph paragraph, Section section)
	{
		_document = document;
		Dictionary<Color, string> dictionary = new Dictionary<Color, string>
		{
			{
				Color.Yellow,
				"Basic (User)"
			},
			{
				Color.Orange,
				"Local (Business Unit)"
			},
			{
				Color.LightGreen,
				"Deep (Parent: Child)"
			},
			{
				Color.Green,
				"Global (Organisation)"
			}
		};
		int num = 1;
		paragraph = section.AddParagraph();
		paragraph.AppendText("Table of Security Role Privileges");
		paragraph.ApplyStyle((BuiltinStyle)2);
		CreateTable(document, section, new string[2] { "Color", "Privilege" }, dictionary.Count, isAutoFit: true);
		foreach (KeyValuePair<Color, string> item in dictionary)
		{
			DataRow = table.Rows[num];
			DataRow.Cells[0].CellFormat.BackColor = item.Key;
			p3 = ((Body)DataRow.Cells[1]).AddParagraph();
			p3.AppendText(item.Value);
			num++;
		}
	}

	public void ProductUsersTable(Document document, Paragraph paragraph, Section section, List<FieldPermission> fieldPermissions, List<CrmFormLabel> crmFormLabels)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		_document = document;
		List<string> list = fieldPermissions.Select((FieldPermission p) => p.FieldSecurityProfileId.Name).Distinct().ToList();
		int num = 1;
		paragraph = section.AddParagraph();
		paragraph.AppendBreak((BreakType)0);
		paragraph.AppendText("Table of Product Users");
		paragraph.ApplyStyle((BuiltinStyle)2);
		CreateTable(document, section, new string[2] { "Security Profile", "Fields" }, list.Count);
		foreach (string securityProfileName in list)
		{
			DataRow = table.Rows[num];
			p2 = ((Body)DataRow.Cells[0]).AddParagraph();
			((ParagraphBase)p2.AppendText(securityProfileName)).ApplyCharacterFormat(new CharacterFormat((IDocument)(object)document)
			{
				Bold = true
			});
			IOrderedEnumerable<FieldPermission> orderedEnumerable = from p in fieldPermissions
				where p.FieldSecurityProfileId.Name == securityProfileName && p.CanCreate.Value != 0 && crmFormLabels.Any((CrmFormLabel a) => a.Entity == p.EntityName)
				orderby p.EntityName
				select p;
			IOrderedEnumerable<FieldPermission> orderedEnumerable2 = from p in fieldPermissions
				where p.FieldSecurityProfileId.Name == securityProfileName && p.CanUpdate.Value != 0 && crmFormLabels.Any((CrmFormLabel a) => a.Entity == p.EntityName)
				orderby p.EntityName
				select p;
			IOrderedEnumerable<FieldPermission> orderedEnumerable3 = from p in fieldPermissions
				where p.FieldSecurityProfileId.Name == securityProfileName && p.CanRead.Value != 0 && crmFormLabels.Any((CrmFormLabel a) => a.Entity == p.EntityName)
				orderby p.EntityName
				select p;
			string empty = string.Empty;
			if (orderedEnumerable.Any())
			{
				ProductUsersTableHelper(document, paragraph, section, orderedEnumerable, crmFormLabels, "Create", CanCreateSymbol);
			}
			if (orderedEnumerable2.Any())
			{
				ProductUsersTableHelper(document, paragraph, section, orderedEnumerable2, crmFormLabels, "Write", CanWriteSymbol);
			}
			if (orderedEnumerable3.Any())
			{
				ProductUsersTableHelper(document, paragraph, section, orderedEnumerable3, crmFormLabels, "Read", CanReadSymbol);
			}
			num++;
		}
	}

	private static void ProductUsersTableHelper(Document document, Paragraph paragraph, Section section, IOrderedEnumerable<FieldPermission> fieldPermissions, List<CrmFormLabel> crmFormLabels, string privilege, string privilegeSymbol)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		string text = string.Empty;
		p3 = ((Body)DataRow.Cells[1]).AddParagraph();
		p3.Format.BackColor = main1;
		((ParagraphBase)p3.AppendText(privilegeSymbol + " " + privilege)).ApplyCharacterFormat(new CharacterFormat((IDocument)(object)document)
		{
			FontName = CrmDocumentSymbolFont,
			TextColor = Color.White,
			Bold = true
		});
		foreach (FieldPermission fieldPermission in fieldPermissions)
		{
			CrmFormLabel crmFormLabel = crmFormLabels.FirstOrDefault((CrmFormLabel p) => p.Attribute == fieldPermission.AttributeLogicalName && p.Entity == fieldPermission.EntityName);
			if (text != fieldPermission.EntityName)
			{
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
				p3.Format.BackColor = main2;
				((ParagraphBase)p3.AppendText(crmFormLabel.EntityDisplayName.FirstOrDefault().Value + " ")).ApplyCharacterFormat(new CharacterFormat((IDocument)(object)document)
				{
					Bold = true,
					TextColor = Color.White
				});
			}
			text = fieldPermission.EntityName;
			p3 = ((Body)DataRow.Cells[1]).AddParagraph();
			p3.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabel.FormId.ToString().Substring(0, 7) + crmFormLabel.AttributeId.ToString()), "■ " + crmFormLabel.Names.First().Value, (HyperlinkType)4);
		}
	}

	private static void PrintEvents(List<CrmFormLabel> crmFormLabels, CrmFormLabel crmFormLabelPrev, Document document, Paragraph paragraph, Section section, string baseURL, string[] eventHeaders)
	{
		int num = 1;
		if (crmFormLabelPrev.LabelForm == null || !crmFormLabelPrev.LabelForm.Events.Any())
		{
			return;
		}
		List<CrmFieldEvent> source = (from p in crmFormLabelPrev.LabelForm.Events
			group p by p.FunctionName into g
			select g.First()).ToList();
		List<CrmFieldEvent> events = crmFormLabelPrev.LabelForm.Events;
		paragraph = section.AddParagraph();
		paragraph.AppendBreak((BreakType)0);
		CreateHeading("Form Scripts", paragraph, (BuiltinStyle)3);
		CreateTable(document, section, eventHeaders, source.Count());
		string empty = string.Empty;
		string text = string.Empty;
		int num2 = 0;
		string empty2 = string.Empty;
		foreach (CrmFieldEvent ev in (from p in crmFormLabelPrev.LabelForm.Events
			group p by p.FunctionName into g
			select g.First() into o
			orderby o.LibraryName
			select o).ThenByDescending((CrmFieldEvent t) => t.EventName).ThenBy((CrmFieldEvent t) => t.FunctionName))
		{
			DataRow = table.Rows[num];
			p2 = ((Body)DataRow.Cells[0]).AddParagraph();
			if (text == ev.LibraryName)
			{
				table.ApplyVerticalMerge(0, num2, num);
			}
			else
			{
				p2.AppendHyperlink(baseURL + "/WebResources/" + ev.LibraryName, ev.LibraryName, (HyperlinkType)2);
				num2 = num;
			}
			text = ev.LibraryName;
			p3 = ((Body)DataRow.Cells[2]).AddParagraph();
			if (ev.EventName != "onchange")
			{
				((ParagraphBase)p3.AppendText("[" + ev.EventName.ToUpper() + "]")).CharacterFormat.Bold = true;
				p3.AppendText(Environment.NewLine);
			}
			foreach (CrmFieldEvent fev in events.Where((CrmFieldEvent p) => p.FunctionName == ev.FunctionName))
			{
				if (!string.IsNullOrEmpty(fev.AttributeName))
				{
					CrmFormLabel crmFormLabel = crmFormLabels.FirstOrDefault((CrmFormLabel p) => p.Attribute == fev.AttributeName && p.FormId == crmFormLabelPrev.FormId && p.EntityId == crmFormLabelPrev.EntityId);
					if (crmFormLabel != null)
					{
						p3.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabelPrev.FormId.ToString().Substring(0, 7) + crmFormLabel.AttributeId.ToString()), "■ " + crmFormLabel.Names[1033], (HyperlinkType)4);
						p3.AppendText(Environment.NewLine);
					}
				}
			}
			p4 = ((Body)DataRow.Cells[1]).AddParagraph();
			if (!ev.IsEnabled)
			{
				((ParagraphBase)p4.AppendText(ev.FunctionName)).CharacterFormat.IsStrikeout = true;
			}
			else
			{
				p4.AppendText(ev.FunctionName);
			}
			p4.AppendBookmarkStart(DataManager.GetBookMarkText(crmFormLabelPrev.EntityId.ToString().Substring(0, 7) + crmFormLabelPrev.FormId.ToString().Substring(0, 7) + ev.FunctionName));
			p4.AppendBookmarkEnd(DataManager.GetBookMarkText(crmFormLabelPrev.EntityId.ToString().Substring(0, 7) + crmFormLabelPrev.FormId.ToString().Substring(0, 7) + ev.FunctionName));
			num++;
		}
	}

	private static void PrintSecurityRoles(List<CrmFormLabel> crmFormLabels, List<RolePrivilegesViewModel> rolePrivilegesViewModels, Document document, Paragraph paragraph, Section section, string baseURL, string[] rolePrivilegesHeaders)
	{
		if (!rolePrivilegesViewModels.Any())
		{
			return;
		}
		int num = 1;
		paragraph = section.AddParagraph();
		paragraph.AppendBreak((BreakType)0);
		CreateHeading("Security Roles", paragraph, (BuiltinStyle)2);
		CreateTable(document, section, rolePrivilegesHeaders, rolePrivilegesViewModels.Count(), isAutoFit: false, isFlip: true);
		DataRow.Cells[0].SetCellWidth(60f, (CellWidthType)1);
		foreach (RolePrivilegesViewModel rolePrivilegesViewModel in rolePrivilegesViewModels)
		{
			DataRow = table.Rows[num];
			p2 = ((Body)DataRow.Cells[0]).AddParagraph();
			p2.AppendHyperlink(baseURL + $"/biz/roles/edit.aspx?id=%7b{rolePrivilegesViewModel.RoleId}%7d", rolePrivilegesViewModel.RoleName, (HyperlinkType)2);
			DataRow.Cells[1].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.CreateAccessRight);
			DataRow.Cells[2].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.ReadAccessRight);
			DataRow.Cells[3].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.WriteAccessRight);
			DataRow.Cells[4].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.DeleteAccessRight);
			DataRow.Cells[5].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.AppendAccessRight);
			DataRow.Cells[6].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.AppendToAccessRight);
			DataRow.Cells[7].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.AssignAccessRight);
			DataRow.Cells[8].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.ShareAccessRight);
			DataRow.Cells[9].CellFormat.BackColor = GetPrivilegeDepthMaskColor(rolePrivilegesViewModel.ReparentAccessRight);
			num++;
		}
		num = 1;
		paragraph = section.AddParagraph();
		((ParagraphBase)paragraph.AppendText(Environment.NewLine + "Key Legend")).CharacterFormat.Bold = true;
		Dictionary<Color, string> source = new Dictionary<Color, string>
		{
			{
				Color.Yellow,
				"Basic (User)"
			},
			{
				Color.Orange,
				"Local (Business Unit)"
			},
			{
				Color.LightGreen,
				"Deep (Parent: Child)"
			},
			{
				Color.Green,
				"Global (Organisation)"
			}
		};
		CreateTable(document, section, source.Select((KeyValuePair<Color, string> p) => p.Value).ToArray(), 1);
		DataRow = table.Rows[num];
		DataRow.Cells[0].SetCellWidth(60f, (CellWidthType)1);
		DataRow.Cells[0].CellFormat.BackColor = Color.Yellow;
		DataRow.Cells[1].CellFormat.BackColor = Color.Orange;
		DataRow.Cells[2].CellFormat.BackColor = Color.LightGreen;
		DataRow.Cells[3].CellFormat.BackColor = Color.Green;
	}

	private static Color GetPrivilegeDepthMaskColor(int privilegeDepthMask)
	{
		Color result = Color.White;
		switch (privilegeDepthMask)
		{
		case 1:
			result = Color.Yellow;
			break;
		case 2:
			result = Color.Orange;
			break;
		case 4:
			result = Color.LightGreen;
			break;
		case 8:
			result = Color.Green;
			break;
		}
		return result;
	}

	private static void PrintSecurityProfiles(List<CrmFieldSecurity> crmFormLabels, CrmFormLabel crmFormLabelPrev, Document document, Paragraph paragraph, Section section, string baseURL, string[] eventHeaders)
	{
		int num = 1;
	}

	private void UpdateGlobalOptoinSet(Document document, Section section, Table table, Paragraph paragraph, List<CrmFormLabel> previousGlobalOptionSets, string baseURL)
	{
		string[] formHeaders = new string[3] { "Option Set", "Values", "Description" };
		int num = 1;
		if (!previousGlobalOptionSets.Any())
		{
			return;
		}
		paragraph = section.AddParagraph();
		paragraph.AppendBreak((BreakType)0);
		CreateHeading("Form Global Option Sets", paragraph, (BuiltinStyle)3);
		Table val = CreateTable(document, section, formHeaders, previousGlobalOptionSets.Count());
		foreach (CrmFormLabel previousGlobalOptionSet in previousGlobalOptionSets)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			if (previousGlobalOptionSet.IsSecured)
			{
				text = SeurityProfileSymbol;
			}
			if (previousGlobalOptionSet.IsReadOnly)
			{
				text2 = ReadOnlySymbol;
			}
			DataRow = val.Rows[num];
			p2 = ((Body)DataRow.Cells[0]).AddParagraph();
			((ParagraphBase)p2.AppendText(text)).ApplyCharacterFormat(SecurityProfileCharacterFormat);
			TR2 = p2.AppendText(string.Join(Environment.NewLine, previousGlobalOptionSet.Names.Select((KeyValuePair<int, string> s) => s.Value)));
			((ParagraphBase)p2.AppendText(text2)).CharacterFormat.FontName = CrmDocumentSymbolFont;
			p2 = ((Body)DataRow.Cells[0]).AddParagraph();
			p2.AppendHyperlink(string.Concat(baseURL, "/tools/systemcustomization/attributes/manageAttribute.aspx?attributeId=%7b", previousGlobalOptionSet.AttributeId, "%7d&entityId=%7b", previousGlobalOptionSet.EntityId.ToString(), "%7d"), previousGlobalOptionSet.Attribute, (HyperlinkType)2);
			p3 = ((Body)DataRow.Cells[1]).AddParagraph();
			TR3 = p3.AppendText(string.Join(Environment.NewLine, previousGlobalOptionSet.Values.Select((KeyValuePair<int, string> s) => "■ " + s.Value)));
			if (previousGlobalOptionSet.Descriptions.Any() && !string.IsNullOrEmpty(previousGlobalOptionSet.Descriptions.First().Value))
			{
				p4 = ((Body)DataRow.Cells[2]).AddParagraph();
				((ParagraphBase)p4.AppendText("Description: ")).CharacterFormat.Bold = true;
				p4 = ((Body)DataRow.Cells[2]).AddParagraph();
				p4.AppendText((previousGlobalOptionSet.Descriptions.Count > 1) ? string.Format(Environment.NewLine, previousGlobalOptionSet.Descriptions.Select((KeyValuePair<int, string> s) => s.Value)) : previousGlobalOptionSet.Descriptions.First().Value);
			}
			if (previousGlobalOptionSet.RelatedWorkflows != null)
			{
				List<Workflow> relatedWorkflows = previousGlobalOptionSet.RelatedWorkflows;
				p4 = ((Body)DataRow.Cells[2]).AddParagraph();
				((ParagraphBase)p4.AppendText("Dependent Processes:")).CharacterFormat.Bold = true;
				foreach (Workflow item in relatedWorkflows)
				{
					p4 = ((Body)DataRow.Cells[2]).AddParagraph();
					p4.AppendHyperlink(DataManager.GetBookMarkText(((Entity)item).Id.ToString()), "■ " + item.Name, (HyperlinkType)4);
				}
			}
			num++;
		}
	}

	private void UpdateLocalOptoinSet(Document document, Section section, Table table, Paragraph paragraph, List<CrmFormLabel> previousLocalOptionSets, string baseURL)
	{
		try
		{
			string[] formHeaders = new string[3] { "Option Set", "Values", "Description" };
			int num = 1;
			if (!previousLocalOptionSets.Any())
			{
				return;
			}
			paragraph = section.AddParagraph();
			paragraph.AppendBreak((BreakType)0);
			CreateHeading("Form Option Sets", paragraph, (BuiltinStyle)3);
			Table val = CreateTable(document, section, formHeaders, previousLocalOptionSets.Count());
			foreach (CrmFormLabel previousLocalOptionSet in previousLocalOptionSets)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				if (previousLocalOptionSet.IsSecured)
				{
					text = SeurityProfileSymbol;
				}
				if (previousLocalOptionSet.IsReadOnly)
				{
					text2 = ReadOnlySymbol;
				}
				DataRow = val.Rows[num];
				p2 = ((Body)DataRow.Cells[0]).AddParagraph();
				((ParagraphBase)p2.AppendText(text)).ApplyCharacterFormat(SecurityProfileCharacterFormat);
				TR2 = p2.AppendText(string.Join(Environment.NewLine, previousLocalOptionSet.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				((ParagraphBase)p2.AppendText(text2)).CharacterFormat.FontName = CrmDocumentSymbolFont;
				p2 = ((Body)DataRow.Cells[0]).AddParagraph();
				p2.AppendHyperlink(string.Concat(baseURL, "/tools/systemcustomization/attributes/manageAttribute.aspx?attributeId=%7b", previousLocalOptionSet.AttributeId, "%7d&entityId=%7b", previousLocalOptionSet.EntityId.ToString(), "%7d"), previousLocalOptionSet.Attribute, (HyperlinkType)2);
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
				TR3 = p3.AppendText(string.Join(Environment.NewLine, previousLocalOptionSet.Values.Select((KeyValuePair<int, string> s) => "■ " + s.Value)));
				if (previousLocalOptionSet.Descriptions.Any() && !string.IsNullOrEmpty(previousLocalOptionSet.Descriptions.First().Value))
				{
					p4 = ((Body)DataRow.Cells[2]).AddParagraph();
					((ParagraphBase)p4.AppendText("Description: ")).CharacterFormat.Bold = true;
					p4 = ((Body)DataRow.Cells[2]).AddParagraph();
					p4.AppendText((previousLocalOptionSet.Descriptions.Count > 1) ? string.Format(Environment.NewLine, previousLocalOptionSet.Descriptions.Select((KeyValuePair<int, string> s) => s.Value)) : previousLocalOptionSet.Descriptions.First().Value);
				}
				if (previousLocalOptionSet.RelatedWorkflows != null)
				{
					List<Workflow> relatedWorkflows = previousLocalOptionSet.RelatedWorkflows;
					p4 = ((Body)DataRow.Cells[2]).AddParagraph();
					((ParagraphBase)p4.AppendText("Dependent Processes:")).CharacterFormat.Bold = true;
					foreach (Workflow item in relatedWorkflows)
					{
						p4 = ((Body)DataRow.Cells[2]).AddParagraph();
						p4.AppendHyperlink(DataManager.GetBookMarkText(((Entity)item).Id.ToString()), "■ " + item.Name, (HyperlinkType)4);
					}
				}
				num++;
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private static void PrintWorkflow(List<Workflow> workflows, CrmFormLabel crmFormLabel, Document document, Paragraph paragraph, Section section, string[] workflowHeaders, int line, CrmConfiguration conf, List<CrmFormLabel> crmFormLabels, string dynamicFieldURL, int category, int mode, string baseURL)
	{
		IEnumerable<Workflow> enumerable = workflows.Where((Workflow p) => ((OptionSetValue)((DataCollection<string, object>)(object)((Entity)p).Attributes)["mode"]).Value == mode && p.PrimaryEntity == crmFormLabel.Entity && ((OptionSetValue)((DataCollection<string, object>)(object)((Entity)p).Attributes)["category"]).Value == category);
		if (!enumerable.Any())
		{
			return;
		}
		paragraph = section.AddParagraph();
		paragraph.AppendBreak((BreakType)0);
		string text = string.Empty;
		if (category != 0)
		{
			switch (category)
			{
			case 1:
				text = "Dialogs";
				break;
			case 2:
				text = "Business Rules";
				break;
			case 3:
				text = "Actions";
				break;
			case 4:
				text = "Business Process Flows";
				break;
			}
		}
		else
		{
			switch (mode)
			{
			case 0:
				text = "Async Procsses";
				break;
			case 1:
				text = "Sync Procsses";
				break;
			}
		}
		CreateHeading(text, paragraph, (BuiltinStyle)2);
		CreateTable(document, section, workflowHeaders, enumerable.Count());
		foreach (Workflow item in enumerable)
		{
			string attributeDescription = GetAttributeDescription(item);
			DataRow = table.Rows[line];
			p2 = ((Body)DataRow.Cells[0]).AddParagraph();
			if (item.StateCode == WorkflowState.Draft)
			{
				((ParagraphBase)p2.AppendHyperlink(GetWorkflowURL(item, baseURL), ((Entity)item)["name"].ToString() + Environment.NewLine + Environment.NewLine, (HyperlinkType)2)).CharacterFormat.IsStrikeout = true;
			}
			else
			{
				p2.AppendHyperlink(GetWorkflowURL(item, baseURL), ((Entity)item)["name"].ToString() + Environment.NewLine + Environment.NewLine, (HyperlinkType)2);
			}
			if (category == 0)
			{
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
				if (item.RunAs != null && item.RunAs.Value != 0)
				{
					((ParagraphBase)p3.AppendText("(Calling User)" + Environment.NewLine)).CharacterFormat.TextColor = Color.DarkRed;
				}
				if (item.Scope.Value != 4)
				{
					((ParagraphBase)p3.AppendText("(Scope: " + GetScopeName(item.Scope.Value) + ")" + Environment.NewLine)).CharacterFormat.TextColor = Color.DarkRed;
				}
			}
			if (!string.IsNullOrEmpty(item.Description))
			{
				p3.AppendText("Description: ");
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
				p3.AppendText(item.Description);
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
			}
			if (category == 0)
			{
				p3.AppendText("Message: ");
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
				((ParagraphBase)p3.AppendText(attributeDescription)).CharacterFormat.Bold = true;
			}
			p2.AppendBookmarkStart(DataManager.GetBookMarkText(((Entity)item).Id.ToString()));
			p3.AppendBookmarkEnd(DataManager.GetBookMarkText(((Entity)item).Id.ToString()));
			if (item.ChildWorkflows != null && item.ChildWorkflows.Any())
			{
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
				p3.AppendText("Child Workflows: ");
				foreach (Workflow childWorkflow in item.ChildWorkflows)
				{
					p3 = ((Body)DataRow.Cells[1]).AddParagraph();
					p3.AppendHyperlink(GetWorkflowURL(item, baseURL), "■ " + childWorkflow.Name + Environment.NewLine, (HyperlinkType)2);
				}
			}
			if (category == 2 && item.ClientData != null)
			{
				List<string> list = new List<string>();
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
				p3.AppendText("Attributes: ");
				List<string> list2 = ExtractFromString(item.ClientData, ".attributes.get('", "')");
				foreach (string s in list2)
				{
					p3 = ((Body)DataRow.Cells[1]).AddParagraph();
					CrmFormLabel crmFormLabel2 = crmFormLabels.FirstOrDefault((CrmFormLabel p) => p.Attribute == s);
					if (crmFormLabel2 != null)
					{
						string text2 = string.Format(dynamicFieldURL, crmFormLabel2.AttributeId);
						p3.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabel.FormId.ToString().Substring(0, 7) + crmFormLabel2.AttributeId.ToString()), "■ " + crmFormLabel2.Names.First().Value, (HyperlinkType)4);
					}
					else
					{
						p3.AppendText("■ " + s);
					}
				}
			}
			if (item.TriggerOnUpdateAttributeList != null)
			{
				List<string> list3 = new List<string>();
				p3 = ((Body)DataRow.Cells[1]).AddParagraph();
				p3.AppendText("Triggers: ");
				string[] array = item.TriggerOnUpdateAttributeList.Split(',');
				foreach (string s2 in array)
				{
					p3 = ((Body)DataRow.Cells[1]).AddParagraph();
					CrmFormLabel crmFormLabel3 = crmFormLabels.FirstOrDefault((CrmFormLabel p) => p.Attribute == s2);
					if (crmFormLabel3 != null)
					{
						string text3 = string.Format(dynamicFieldURL, crmFormLabel3.AttributeId);
						p3.AppendHyperlink(DataManager.GetBookMarkText(crmFormLabel.FormId.ToString().Substring(0, 7) + crmFormLabel3.AttributeId.ToString()), "■ " + crmFormLabel3.Names.First().Value, (HyperlinkType)4);
					}
					else
					{
						p3.AppendText("■ " + s2);
					}
				}
			}
			line++;
		}
	}

	private static List<string> ExtractFromString(string text, string startString, string endString)
	{
		List<string> list = new List<string>();
		int num = 0;
		int num2 = 0;
		bool flag = false;
		while (!flag)
		{
			num = text.IndexOf(startString);
			num2 = text.IndexOf(endString);
			if (num != -1 && num2 != -1)
			{
				list.Add(text.Substring(num + startString.Length, num2 - num - startString.Length));
				text = text.Substring(num2 + endString.Length);
			}
			else
			{
				flag = true;
			}
		}
		return list;
	}

	public static string ReadHTMLFromURL(string urlAddress)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlAddress);
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		if (httpWebResponse.StatusCode == HttpStatusCode.OK)
		{
			Stream responseStream = httpWebResponse.GetResponseStream();
			StreamReader streamReader = null;
			streamReader = ((httpWebResponse.CharacterSet != null) ? new StreamReader(responseStream, Encoding.GetEncoding(httpWebResponse.CharacterSet)) : new StreamReader(responseStream));
			string result = streamReader.ReadToEnd();
			httpWebResponse.Close();
			streamReader.Close();
			return result;
		}
		using (WebClient webClient = new WebClient())
		{
			string text = webClient.DownloadString(urlAddress);
		}
		return string.Empty;
	}

	public static void CreateHeading(string text, Paragraph paragraph, BuiltinStyle headingStyle)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected I4, but got Unknown
		paragraph.AppendText(text);
		paragraph.ApplyStyle(headingStyle);
		paragraph.ListFormat.ApplyStyle("levelstyle");
		((Style)paragraph.GetStyle()).CharacterFormat.Italic = false;
		switch (headingStyle - 1)
		{
		case 0:
			((Style)paragraph.GetStyle()).CharacterFormat.FontSize = 20f;
			paragraph.GetStyle().ParagraphFormat.Borders.Bottom.BorderType = (BorderStyle)1;
			((Style)paragraph.GetStyle()).CharacterFormat.FontName = CrmDocumentFont.Name;
			break;
		//case 1:
		//	paragraph.ListFormat.ListLevelNumber = 1;
		//	((Style)paragraph.GetStyle()).CharacterFormat.FontName = CrmDocumentFont.Name;
		//	break;
		//case 2:
		//	paragraph.ListFormat.ListLevelNumber = 2;
		//	paragraph.GetStyle().ParagraphFormat.Borders.Top.BorderType = (BorderStyle)1;
		//	paragraph.GetStyle().ParagraphFormat.Borders.Right.BorderType = (BorderStyle)1;
		//	((Style)paragraph.GetStyle()).CharacterFormat.FontName = CrmDocumentFont.Name;
		//	break;
		}
	}

	public static int ExportField(List<int> languages, ExcelWorksheet labelSheet, int line, CrmFormLabel crmFormLabelPrev, CrmFormLabel crmFormLabel)
	{
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		bool flag4 = true;
		if (crmFormLabelPrev != null)
		{
			flag = crmFormLabelPrev.Entity != crmFormLabel.Entity;
			flag2 = crmFormLabelPrev.FormId != crmFormLabel.FormId;
			flag3 = crmFormLabelPrev.Tab.Id != crmFormLabel.Tab.Id;
			flag4 = crmFormLabelPrev.Section.Id != crmFormLabel.Section.Id;
		}
		if (flag || flag2 || flag3 || flag4)
		{
			int num2 = line;
			if (flag)
			{
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				ExcelCell obj = labelSheet.Cells[line, 9];
				object value = (((AbstractRange)labelSheet.Cells[line++, num++]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value)));
				((AbstractRange)obj).Value = value;
				num = 0;
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				ExcelCell obj2 = labelSheet.Cells[line, 8];
				ExcelCell obj3 = labelSheet.Cells[line++, num];
				object obj4 = (((AbstractRange)labelSheet.Cells[line, 8]).Value = crmFormLabel.Form);
				value = (((AbstractRange)obj3).Value = obj4);
				((AbstractRange)obj2).Value = value;
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				ExcelCell obj6 = labelSheet.Cells[line, 7];
				ExcelCell obj7 = labelSheet.Cells[line++, num];
				obj4 = (((AbstractRange)labelSheet.Cells[line, 7]).Value = string.Join(Environment.NewLine, crmFormLabel.Tab.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				value = (((AbstractRange)obj7).Value = obj4);
				((AbstractRange)obj6).Value = value;
				((AbstractRange)labelSheet.Cells[line, 8]).Value = crmFormLabel.Form;
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				ExcelCell obj9 = labelSheet.Cells[line, 6];
				value = (((AbstractRange)labelSheet.Cells[line++, num]).Value = string.Join(Environment.NewLine, crmFormLabel.Section.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				((AbstractRange)obj9).Value = value;
			}
			else if (flag2)
			{
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				ExcelCell obj10 = labelSheet.Cells[line, 8];
				ExcelCell obj11 = labelSheet.Cells[line++, num];
				object obj4 = (((AbstractRange)labelSheet.Cells[line, 8]).Value = crmFormLabel.Form);
				object value = (((AbstractRange)obj11).Value = obj4);
				((AbstractRange)obj10).Value = value;
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				ExcelCell obj13 = labelSheet.Cells[line, 7];
				ExcelCell obj14 = labelSheet.Cells[line++, num];
				obj4 = (((AbstractRange)labelSheet.Cells[line, 7]).Value = string.Join(Environment.NewLine, crmFormLabel.Tab.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				value = (((AbstractRange)obj14).Value = obj4);
				((AbstractRange)obj13).Value = value;
				((AbstractRange)labelSheet.Cells[line, 8]).Value = crmFormLabel.Form;
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				ExcelCell obj16 = labelSheet.Cells[line, 6];
				value = (((AbstractRange)labelSheet.Cells[line++, num]).Value = string.Join(Environment.NewLine, crmFormLabel.Section.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				((AbstractRange)obj16).Value = value;
				((AbstractRange)labelSheet.Cells[line, 7]).Value = string.Join(Environment.NewLine, crmFormLabel.Tab.Names.Select((KeyValuePair<int, string> s) => s.Value));
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
			}
			else if (flag3)
			{
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				((AbstractRange)labelSheet.Cells[line, 8]).Value = crmFormLabel.Form;
				ExcelCell obj17 = labelSheet.Cells[line, 7];
				ExcelCell obj18 = labelSheet.Cells[line++, num];
				object obj4 = (((AbstractRange)labelSheet.Cells[line, 7]).Value = string.Join(Environment.NewLine, crmFormLabel.Tab.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				object value = (((AbstractRange)obj18).Value = obj4);
				((AbstractRange)obj17).Value = value;
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				((AbstractRange)labelSheet.Cells[line, 8]).Value = crmFormLabel.Form;
				ExcelCell obj20 = labelSheet.Cells[line, 6];
				value = (((AbstractRange)labelSheet.Cells[line++, num]).Value = string.Join(Environment.NewLine, crmFormLabel.Section.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				((AbstractRange)obj20).Value = value;
			}
			else
			{
				((AbstractRange)labelSheet.Cells[line, 9]).Value = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
				((AbstractRange)labelSheet.Cells[line, 8]).Value = crmFormLabel.Form;
				((AbstractRange)labelSheet.Cells[line, 7]).Value = string.Join(Environment.NewLine, crmFormLabel.Tab.Names.Select((KeyValuePair<int, string> s) => s.Value));
				ExcelCell obj21 = labelSheet.Cells[line, 6];
				object value = (((AbstractRange)labelSheet.Cells[line++, num]).Value = string.Join(Environment.NewLine, crmFormLabel.Section.Names.Select((KeyValuePair<int, string> s) => s.Value)));
				((AbstractRange)obj21).Value = value;
			}
			int num3 = 0;
			int num4 = line - num2;
			//for (int i = num2; i < line; i++)
			//{
			//	labelSheet.Cells.GetSubrangeAbsolute(i, 0, i, 5 + num4 - num3).Merged = true;
			//	if (5 + num4 - num3 == 9)
			//	{
			//		((AbstractRange)labelSheet.Cells.GetSubrangeAbsolute(i, 0, i, 5 + num4 - num3)).Style.FillPattern.SetSolid(SpreadsheetColor.op_Implicit(Color.Black));
			//	}
			//	else if (5 + num4 - num3 == 8)
			//	{
			//		((AbstractRange)labelSheet.Cells.GetSubrangeAbsolute(i, 0, i, 5 + num4 - num3)).Style.FillPattern.SetSolid(SpreadsheetColor.op_Implicit(Color.DarkRed));
			//	}
			//	else if (5 + num4 - num3 == 7)
			//	{
			//		((AbstractRange)labelSheet.Cells.GetSubrangeAbsolute(i, 0, i, 5 + num4 - num3)).Style.FillPattern.SetSolid(SpreadsheetColor.op_Implicit(Color.IndianRed));
			//	}
			//	else if (5 + num4 - num3 == 6)
			//	{
			//		((AbstractRange)labelSheet.Cells.GetSubrangeAbsolute(i, 0, i, 5 + num4 - num3)).Style.FillPattern.SetSolid(SpreadsheetColor.op_Implicit(Color.Red));
			//	}
			//	((AbstractRange)labelSheet.Cells.GetSubrangeAbsolute(i, 0, i, 5 + num4 - num3)).Style.Font.Color = SpreadsheetColor.op_Implicit(Color.White);
			//	((AbstractRange)labelSheet.Cells.GetSubrangeAbsolute(i, 0, i, 5 + num4 - num3)).Style.Font.Weight = 700;
			//	num3++;
			//}
		}
		string value2 = string.Empty;
		if (crmFormLabel.WorkflowDependencies != null && crmFormLabel.WorkflowDependencies.Count > 0)
		{
			value2 = string.Join(Environment.NewLine, crmFormLabel.WorkflowDependencies.Select((Workflow p) => string.Concat(((DataCollection<string, object>)(object)((Entity)p).Attributes)["name"], GetAttributeDescription(((Entity)p).Attributes))));
		}
		((AbstractRange)labelSheet.Cells[line, num++]).Value = value2;
		((AbstractRange)labelSheet.Cells[line, num++]).Value = crmFormLabel.Attribute;
		foreach (int lcid in from o in languages.ToList()
			orderby o descending
			select o)
		{
			bool flag5 = crmFormLabel.Names.ContainsKey(lcid);
			((AbstractRange)labelSheet.Cells[line, num++]).Value = (flag5 ? crmFormLabel.Names.First((KeyValuePair<int, string> n) => n.Key == lcid).Value : string.Empty);
		}
		string value3 = string.Empty;
		string value4 = string.Empty;
		if (crmFormLabel.RelatedWorkflows != null && crmFormLabel.RelatedWorkflows.Any())
		{
			value3 = string.Join(Environment.NewLine, crmFormLabel.RelatedWorkflows.Select((Workflow s) => s.Name));
		}
		if (crmFormLabel.EntityDisplayName != null)
		{
			value4 = string.Join(Environment.NewLine, crmFormLabel.EntityDisplayName.Select((KeyValuePair<int, string> s) => s.Value));
		}
		((AbstractRange)labelSheet.Cells[line, num++]).Value = value3;
		((AbstractRange)labelSheet.Cells[line, num++]).Value = crmFormLabel.AttributeType;
		((AbstractRange)labelSheet.Cells[line, num++]).Value = string.Join(Environment.NewLine, crmFormLabel.Section.Names.Select((KeyValuePair<int, string> s) => s.Value));
		((AbstractRange)labelSheet.Cells[line, num++]).Value = string.Join(Environment.NewLine, crmFormLabel.Tab.Names.Select((KeyValuePair<int, string> s) => s.Value));
		((AbstractRange)labelSheet.Cells[line, num++]).Value = crmFormLabel.Form;
		((AbstractRange)labelSheet.Cells[line, num++]).Value = value4;
		line++;
		return line;
	}

	public static int ExportSection(List<int> languages, ExcelWorksheet sectionSheet, int line, CrmFormSection crmFormSection)
	{
		int num = 0;
		((AbstractRange)sectionSheet.Cells[line, num++]).Value = crmFormSection.Id.ToString("B");
		((AbstractRange)sectionSheet.Cells[line, num++]).Value = crmFormSection.Entity;
		((AbstractRange)sectionSheet.Cells[line, num++]).Value = crmFormSection.Form;
		((AbstractRange)sectionSheet.Cells[line, num++]).Value = crmFormSection.FormUniqueId.ToString("B");
		((AbstractRange)sectionSheet.Cells[line, num++]).Value = crmFormSection.FormId.ToString("B");
		((AbstractRange)sectionSheet.Cells[line, num++]).Value = crmFormSection.Tab;
		foreach (int lcid in languages)
		{
			bool flag = crmFormSection.Names.ContainsKey(lcid);
			((AbstractRange)sectionSheet.Cells[line, num++]).Value = (flag ? crmFormSection.Names.First((KeyValuePair<int, string> n) => n.Key == lcid).Value : string.Empty);
		}
		line++;
		return line;
	}

	public static int ExportTab(List<int> languages, ExcelWorksheet tabSheet, int line, CrmFormTab crmFormTab)
	{
		int num = 0;
		((AbstractRange)tabSheet.Cells[line, num++]).Value = crmFormTab.Id.ToString("B");
		((AbstractRange)tabSheet.Cells[line, num++]).Value = crmFormTab.Entity;
		((AbstractRange)tabSheet.Cells[line, num++]).Value = crmFormTab.Form;
		((AbstractRange)tabSheet.Cells[line, num++]).Value = crmFormTab.FormUniqueId.ToString("B");
		((AbstractRange)tabSheet.Cells[line, num++]).Value = crmFormTab.FormId.ToString("B");
		foreach (int lcid in languages)
		{
			bool flag = crmFormTab.Names.ContainsKey(lcid);
			((AbstractRange)tabSheet.Cells[line, num++]).Value = (flag ? crmFormTab.Names.First((KeyValuePair<int, string> n) => n.Key == lcid).Value : string.Empty);
		}
		line++;
		return line;
	}

	public static int ExportForm(List<int> languages, ExcelWorksheet formSheet, int line, CrmForm crmForm)
	{
		int num = 0;
		((AbstractRange)formSheet.Cells[line, num++]).Value = crmForm.FormUniqueId.ToString("B");
		((AbstractRange)formSheet.Cells[line, num++]).Value = crmForm.Id.ToString("B");
		((AbstractRange)formSheet.Cells[line, num++]).Value = crmForm.Entity;
		((AbstractRange)formSheet.Cells[line, num++]).Value = "Name";
		foreach (int lcid in languages)
		{
			KeyValuePair<int, string> keyValuePair = crmForm.Names.FirstOrDefault((KeyValuePair<int, string> n) => n.Key == lcid);
			if (keyValuePair.Value != null)
			{
				((AbstractRange)formSheet.Cells[line, num++]).Value = keyValuePair.Value;
			}
			else
			{
				num++;
			}
		}
		line++;
		num = 0;
		((AbstractRange)formSheet.Cells[line, num++]).Value = crmForm.FormUniqueId.ToString("B");
		((AbstractRange)formSheet.Cells[line, num++]).Value = crmForm.Id.ToString("B");
		((AbstractRange)formSheet.Cells[line, num++]).Value = crmForm.Entity;
		((AbstractRange)formSheet.Cells[line, num++]).Value = "Description";
		foreach (int lcid2 in languages)
		{
			KeyValuePair<int, string> keyValuePair2 = crmForm.Descriptions.FirstOrDefault((KeyValuePair<int, string> n) => n.Key == lcid2);
			if (keyValuePair2.Value != null)
			{
				((AbstractRange)formSheet.Cells[line, num++]).Value = keyValuePair2.Value;
			}
			else
			{
				num++;
			}
		}
		line++;
		return line;
	}
}
