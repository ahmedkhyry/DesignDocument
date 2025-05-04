using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DesignDocument.BLL;
using DesignDocument.DAL;
using GemBox.Spreadsheet;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Label = System.Windows.Forms.Label;

namespace DesignDocument.UI;

public class Main : Form
{
	private IOrganizationService _service;

	private List<EntityMetadata> _entities;

	private List<int> _lcids;

	private IContainer components = null;

	private TextBox txtOrganizationService;

	private Label lblOrganizationService;

	private Label lblUserName;

	private TextBox txtUserName;

	private Label lblPassword;

	private TextBox txtPassword;

	private Button btnGenerate;

	private PictureBox pictureBox1;

	private RadCheckedListBox rclEntities;

	private Label lblChooseEntities;

	private Label lblChooseLanguages;

	private RadCheckedDropDownList radCheckedDropDownList1;

	private RootRadElement object_d141d70e_57cd_46da_af45_200a7572dc38;

	private Button btnRefresh;

	private Panel pnlGeneration;

	private CheckBox ckIsIfd;

	public DateTimePicker dtFrom;

	public DateTimePicker dtTo;

	public CheckBox chkDates;

	private IOrganizationService Service
	{
		get
		{
			if (_service != null)
			{
				return _service;
			}
			return _service;
		}
		set
		{
			_service = value;
		}
	}

	private List<EntityMetadata> Entities
	{
		get
		{
			if (_entities != null)
			{
				return _entities;
			}
			return _entities;
		}
		set
		{
			_entities = value;
		}
	}

	private List<int> Lcids
	{
		get
		{
			if (_lcids != null)
			{
				return _lcids;
			}
			return _lcids;
		}
		set
		{
			_lcids = value;
		}
	}

	public Main()
	{
		InitializeComponent();
		CrmConfiguration crmConfiguration = new CrmConfiguration
		{
			OrganizationURL = "/XRMServices/2011/Organization.svc",
			UserName = "crmadmin",
			Password = "linkP@ss",
			ModifiedOn = DateTime.Now
		};
		if (File.Exists("Configurations.JSON"))
		{
			crmConfiguration = (from o in DataManager.ReadFromJsonFile<List<CrmConfiguration>>("Configurations.JSON")
				orderby o.ModifiedOn descending
				select o).FirstOrDefault();
		}
		txtOrganizationService.Text = crmConfiguration.OrganizationURL;
		txtUserName.Text = crmConfiguration.UserName;
		txtPassword.Text = crmConfiguration.Password;
		if (crmConfiguration.Entities != null && !crmConfiguration.Entities.Any())
		{
			if (File.Exists("Lcids.JSON"))
			{
				Lcids = DataManager.ReadFromJsonFile<List<int>>("Lcids.JSON");
			}
			BindEntityData();
		}
	}

	private void btnRefresh_Click(object sender, EventArgs e)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		Cursor.Current = Cursors.WaitCursor;
		Uri uri = new Uri(txtOrganizationService.Text);
		Service = DataManager.Service(uri.OriginalString, txtUserName.Text, txtPassword.Text, ckIsIfd.Checked);
		Entities = DataManager.RetrieveEntities(Service);
		RetrieveProvisionedLanguagesRequest val = new RetrieveProvisionedLanguagesRequest();
		RetrieveProvisionedLanguagesResponse val2 = (RetrieveProvisionedLanguagesResponse)Service.Execute((OrganizationRequest)(object)val);
		Lcids = val2.RetrieveProvisionedLanguages.Select((int lcid) => lcid).ToList();
		DataManager.WriteToJsonFile("Lcids.JSON", Lcids);
		BindEntityData();
		Uri uri2 = new Uri(txtOrganizationService.Text);
		CrmConfiguration conf = new CrmConfiguration
		{
			OrganizationURL = txtOrganizationService.Text,
			BaseURL = "http://" + uri2.Host + "/" + uri2.Segments[1].Remove(uri2.Segments[1].Length - 1),
			UserName = txtUserName.Text,
			Password = txtPassword.Text,
			ModifiedOn = DateTime.Now,
			Entities = Entities
		};
		WriteConfigurations(conf);
		Cursor.Current = Cursors.Default;
	}

	public void WriteConfigurations(CrmConfiguration conf)
	{
		List<CrmConfiguration> list = new List<CrmConfiguration>();
		if (File.Exists("Configurations.JSON"))
		{
			list = DataManager.ReadFromJsonFile<List<CrmConfiguration>>("Configurations.JSON");
		}
		CrmConfiguration crmConfiguration = list.FirstOrDefault((CrmConfiguration p) => p.OrganizationURL.ToLower() == conf.OrganizationURL.ToLower());
		if (crmConfiguration != null)
		{
			list.Remove(crmConfiguration);
		}
		list.Add(conf);
		DataManager.WriteToJsonFile("Configurations.JSON", list);
	}

	private void BindEntityData()
	{
		Cursor.Current = Cursors.WaitCursor;
		((RadListView)rclEntities).DataSource = Entities.OrderBy((EntityMetadata metadata) => metadata.LogicalName).ToList();
		((RadListView)rclEntities).DisplayMember = "LogicalName";
		((RadDropDownList)radCheckedDropDownList1).DataSource = Lcids.Where((int lcid) => lcid != 1033);
		((RadDropDownListElement)radCheckedDropDownList1.CheckedDropDownListElement).SelectionMode = SelectionMode.MultiSimple;
		((RadDropDownList)radCheckedDropDownList1).SelectAll();
		if (((RadListDataItemCollection)radCheckedDropDownList1.Items).Count > 0)
		{
			((RadListDataItem)radCheckedDropDownList1.Items[0]).Selected = true;
		}
		((Control)(object)radCheckedDropDownList1).Text = "Choose other languages. (English is the default language)";
		pnlGeneration.Visible = true;
		Cursor.Current = Cursors.Default;
	}

	private void btnGenerate_Click(object sender, EventArgs e)
	{
		Uri uri = new Uri(txtOrganizationService.Text);
		CrmConfiguration conf = new CrmConfiguration
		{
			OrganizationURL = txtOrganizationService.Text,
			BaseURL = "http://" + uri.Host + "/" + uri.Segments[1].Remove(uri.Segments[1].Length - 1),
			UserName = txtUserName.Text,
			Password = txtPassword.Text,
			ModifiedOn = DateTime.Now,
			Entities = Entities
		};
		WriteConfigurations(conf);
		List<string> entityLogicalNames = ((IEnumerable<ListViewDataItem>)((RadListView)rclEntities).CheckedItems).Select((ListViewDataItem s) => s.Value.ToString()).ToList();
		List<EntityMetadata> source = Entities.Where((EntityMetadata p) => entityLogicalNames.Contains(p.LogicalName)).ToList();
		string text = " [" + DateTime.Now.ToString("dd.MM.yyyy") + "] " + string.Join(",", from s in source.Take(4)
			select s.DisplayName.UserLocalizedLabel.Label);
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "DOCX files (*.docx)|*.docx";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		if (text.Length > 240)
		{
			text = text.Substring(0, 239);
		}
		saveFileDialog.FileName = text;
		saveFileDialog.AddExtension = true;
		FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			Stream stream;
			using (stream = saveFileDialog.OpenFile())
			{
				string fileName = saveFileDialog.FileName;
				stream.Close();
				Cursor.Current = Cursors.WaitCursor;
				source = new List<EntityMetadata>();
				source = RetrieveEntitiesMetadata(entityLogicalNames);
				SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
				List<int> list = new List<int> { 1033 };
				if (((ReadOnlyCollection<RadCheckedListDataItem>)(object)radCheckedDropDownList1.CheckedItems).Count > 0)
				{
					list.AddRange(((IEnumerable<RadCheckedListDataItem>)radCheckedDropDownList1.CheckedItems).Select((RadCheckedListDataItem s) => int.Parse(((RadListDataItem)s).Value.ToString())).ToList());
				}
				string text2 = DateTime.Now.ToString("dd/MM/yyyy") + Environment.NewLine + Dns.GetHostName() + Environment.NewLine + DataManager.GetLocalIPAddress() + Environment.NewLine + "[Organization]" + Environment.NewLine + txtOrganizationService.Text + Environment.NewLine + "[Logical Entities (" + entityLogicalNames.Count + ")]" + Environment.NewLine + string.Join(Environment.NewLine, entityLogicalNames) + Environment.NewLine + "[Entities]" + Environment.NewLine + string.Join(Environment.NewLine, source.Select((EntityMetadata s) => s.DisplayName.UserLocalizedLabel.Label));
				FreeLicense();
				MessageBox.Show("The document has been created successfully!", "Done");
			}
		}
		Cursor.Current = Cursors.Default;
	}

	private List<EntityMetadata> RetrieveEntitiesMetadata(List<string> entityLogicalNames)
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
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		ExecuteMultipleRequest val = new ExecuteMultipleRequest
		{
			Settings = new ExecuteMultipleSettings
			{
				ContinueOnError = true,
				ReturnResponses = true
			},
			Requests = new OrganizationRequestCollection()
		};
		foreach (string entityLogicalName in entityLogicalNames)
		{
			((Collection<OrganizationRequest>)(object)val.Requests).Add((OrganizationRequest)new RetrieveEntityRequest
			{
				LogicalName = entityLogicalName,
				EntityFilters = (EntityFilters)2
			});
		}
		ExecuteMultipleResponse val2 = (ExecuteMultipleResponse)Service.Execute((OrganizationRequest)(object)val);
		return (from s in ((IEnumerable<ExecuteMultipleResponseItem>)val2.Responses).ToList()
			select ((RetrieveEntityResponse)s.Response).EntityMetadata).ToList();
	}

	private void FreeLicense()
	{
		SpreadsheetInfo.FreeLimitReached += delegate(object sender, FreeLimitEventArgs e)
		{
			e.FreeLimitReachedAction = (FreeLimitReachedAction)2;
		};
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignDocument.UI.Main));
		RadCheckedListDataItem val = new RadCheckedListDataItem();
		this.txtOrganizationService = new System.Windows.Forms.TextBox();
		this.lblOrganizationService = new System.Windows.Forms.Label();
		this.lblUserName = new System.Windows.Forms.Label();
		this.txtUserName = new System.Windows.Forms.TextBox();
		this.lblPassword = new System.Windows.Forms.Label();
		this.txtPassword = new System.Windows.Forms.TextBox();
		this.btnGenerate = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.rclEntities = new RadCheckedListBox();
		this.lblChooseEntities = new System.Windows.Forms.Label();
		this.lblChooseLanguages = new System.Windows.Forms.Label();
		this.radCheckedDropDownList1 = new RadCheckedDropDownList();
		this.object_d141d70e_57cd_46da_af45_200a7572dc38 = new RootRadElement();
		this.btnRefresh = new System.Windows.Forms.Button();
		this.pnlGeneration = new System.Windows.Forms.Panel();
		this.ckIsIfd = new System.Windows.Forms.CheckBox();
		this.dtFrom = new System.Windows.Forms.DateTimePicker();
		this.dtTo = new System.Windows.Forms.DateTimePicker();
		this.chkDates = new System.Windows.Forms.CheckBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rclEntities).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radCheckedDropDownList1).BeginInit();
		this.pnlGeneration.SuspendLayout();
		base.SuspendLayout();
		this.txtOrganizationService.BackColor = System.Drawing.SystemColors.AppWorkspace;
		this.txtOrganizationService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtOrganizationService.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Underline);
		this.txtOrganizationService.ForeColor = System.Drawing.Color.Blue;
		this.txtOrganizationService.Location = new System.Drawing.Point(12, 143);
		this.txtOrganizationService.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.txtOrganizationService.Name = "txtOrganizationService";
		this.txtOrganizationService.Size = new System.Drawing.Size(367, 20);
		this.txtOrganizationService.TabIndex = 0;
		this.lblOrganizationService.AutoSize = true;
		this.lblOrganizationService.ForeColor = System.Drawing.SystemColors.Window;
		this.lblOrganizationService.Location = new System.Drawing.Point(13, 127);
		this.lblOrganizationService.Name = "lblOrganizationService";
		this.lblOrganizationService.Size = new System.Drawing.Size(105, 13);
		this.lblOrganizationService.TabIndex = 1;
		this.lblOrganizationService.Text = "Organization Service";
		this.lblUserName.AutoSize = true;
		this.lblUserName.ForeColor = System.Drawing.SystemColors.Window;
		this.lblUserName.Location = new System.Drawing.Point(13, 174);
		this.lblUserName.Name = "lblUserName";
		this.lblUserName.Size = new System.Drawing.Size(96, 13);
		this.lblUserName.TabIndex = 5;
		this.lblUserName.Text = "Domain\\Username";
		this.txtUserName.BackColor = System.Drawing.SystemColors.AppWorkspace;
		this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtUserName.Location = new System.Drawing.Point(12, 192);
		this.txtUserName.Name = "txtUserName";
		this.txtUserName.Size = new System.Drawing.Size(367, 20);
		this.txtUserName.TabIndex = 4;
		this.lblPassword.AutoSize = true;
		this.lblPassword.ForeColor = System.Drawing.SystemColors.Window;
		this.lblPassword.Location = new System.Drawing.Point(13, 222);
		this.lblPassword.Name = "lblPassword";
		this.lblPassword.Size = new System.Drawing.Size(53, 13);
		this.lblPassword.TabIndex = 7;
		this.lblPassword.Text = "Password";
		this.txtPassword.BackColor = System.Drawing.SystemColors.AppWorkspace;
		this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtPassword.Location = new System.Drawing.Point(12, 238);
		this.txtPassword.Name = "txtPassword";
		this.txtPassword.Size = new System.Drawing.Size(367, 20);
		this.txtPassword.TabIndex = 6;
		this.txtPassword.UseSystemPasswordChar = true;
		this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnGenerate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnGenerate.Location = new System.Drawing.Point(301, 311);
		this.btnGenerate.Name = "btnGenerate";
		this.btnGenerate.Size = new System.Drawing.Size(75, 23);
		this.btnGenerate.TabIndex = 9;
		this.btnGenerate.Text = "Generate";
		this.btnGenerate.UseVisualStyleBackColor = true;
		this.btnGenerate.Click += new System.EventHandler(btnGenerate_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(454, 3);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(367, 97);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 10;
		this.pictureBox1.TabStop = false;
		((System.Windows.Forms.Control)(object)this.rclEntities).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((RadListView)this.rclEntities).CheckOnClickMode = (CheckOnClickMode)1;
		((RadListView)this.rclEntities).EnableFiltering = true;
		((RadListView)this.rclEntities).EnableKineticScrolling = true;
		((RadListView)this.rclEntities).KeyboardSearchEnabled = true;
		((System.Windows.Forms.Control)(object)this.rclEntities).Location = new System.Drawing.Point(9, 16);
		((RadListView)this.rclEntities).MultiSelect = true;
		((System.Windows.Forms.Control)(object)this.rclEntities).Name = "rclEntities";
		((RadItem)((RadControl)this.rclEntities).RootElement).AccessibleDescription = null;
		((RadItem)((RadControl)this.rclEntities).RootElement).AccessibleName = null;
		((RadControl)this.rclEntities).RootElement.ControlBounds = new System.Drawing.Rectangle(9, 16, 120, 95);
		((System.Windows.Forms.Control)(object)this.rclEntities).Size = new System.Drawing.Size(367, 248);
		((System.Windows.Forms.Control)(object)this.rclEntities).TabIndex = 11;
		((System.Windows.Forms.Control)(object)this.rclEntities).Text = "radCheckedListBox1";
		((RadControl)this.rclEntities).ThemeName = "telerikMetroTheme1";
		this.lblChooseEntities.AutoSize = true;
		this.lblChooseEntities.ForeColor = System.Drawing.SystemColors.Window;
		this.lblChooseEntities.Location = new System.Drawing.Point(10, 0);
		this.lblChooseEntities.Name = "lblChooseEntities";
		this.lblChooseEntities.Size = new System.Drawing.Size(80, 13);
		this.lblChooseEntities.TabIndex = 12;
		this.lblChooseEntities.Text = "Choose Entities";
		this.lblChooseLanguages.AutoSize = true;
		this.lblChooseLanguages.ForeColor = System.Drawing.SystemColors.Window;
		this.lblChooseLanguages.Location = new System.Drawing.Point(10, 270);
		this.lblChooseLanguages.Name = "lblChooseLanguages";
		this.lblChooseLanguages.Size = new System.Drawing.Size(99, 13);
		this.lblChooseLanguages.TabIndex = 13;
		this.lblChooseLanguages.Text = "Choose Languages";
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList1).BackColor = System.Drawing.Color.FromArgb(171, 171, 171);
		val.Checked = true;
		((RadListDataItem)val).Text = "1033";
		this.radCheckedDropDownList1.Items.Add(val);
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList1).Location = new System.Drawing.Point(9, 285);
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList1).Name = "radCheckedDropDownList1";
		((RadItem)((RadControl)this.radCheckedDropDownList1).RootElement).AccessibleDescription = null;
		((RadItem)((RadControl)this.radCheckedDropDownList1).RootElement).AccessibleName = null;
		((RadControl)this.radCheckedDropDownList1).RootElement.ControlBounds = new System.Drawing.Rectangle(9, 285, 125, 20);
		((RadElement)((RadControl)this.radCheckedDropDownList1).RootElement).StretchVertically = true;
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList1).Size = new System.Drawing.Size(367, 20);
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList1).TabIndex = 14;
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList1).Text = "1033;";
		((RadElement)this.object_d141d70e_57cd_46da_af45_200a7572dc38).StretchHorizontally = true;
		((RadElement)this.object_d141d70e_57cd_46da_af45_200a7572dc38).StretchVertically = true;
		this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnRefresh.Location = new System.Drawing.Point(304, 268);
		this.btnRefresh.Name = "btnRefresh";
		this.btnRefresh.Size = new System.Drawing.Size(75, 23);
		this.btnRefresh.TabIndex = 15;
		this.btnRefresh.Text = "Refresh";
		this.btnRefresh.UseVisualStyleBackColor = true;
		this.btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
		this.pnlGeneration.BackColor = System.Drawing.Color.Transparent;
		this.pnlGeneration.Controls.Add(this.lblChooseEntities);
		this.pnlGeneration.Controls.Add((System.Windows.Forms.Control)(object)this.rclEntities);
		this.pnlGeneration.Controls.Add((System.Windows.Forms.Control)(object)this.radCheckedDropDownList1);
		this.pnlGeneration.Controls.Add(this.btnGenerate);
		this.pnlGeneration.Controls.Add(this.lblChooseLanguages);
		this.pnlGeneration.Location = new System.Drawing.Point(434, 127);
		this.pnlGeneration.Name = "pnlGeneration";
		this.pnlGeneration.Size = new System.Drawing.Size(387, 349);
		this.pnlGeneration.TabIndex = 16;
		this.pnlGeneration.Visible = false;
		this.ckIsIfd.AutoSize = true;
		this.ckIsIfd.Location = new System.Drawing.Point(336, 126);
		this.ckIsIfd.Name = "ckIsIfd";
		this.ckIsIfd.Size = new System.Drawing.Size(43, 17);
		this.ckIsIfd.TabIndex = 17;
		this.ckIsIfd.Text = "IFD";
		this.ckIsIfd.UseVisualStyleBackColor = true;
		this.dtFrom.Location = new System.Drawing.Point(179, 297);
		this.dtFrom.Name = "dtFrom";
		this.dtFrom.Size = new System.Drawing.Size(200, 20);
		this.dtFrom.TabIndex = 18;
		this.dtTo.Location = new System.Drawing.Point(179, 323);
		this.dtTo.Name = "dtTo";
		this.dtTo.Size = new System.Drawing.Size(200, 20);
		this.dtTo.TabIndex = 19;
		this.dtTo.Value = new System.DateTime(2016, 9, 11, 23, 31, 26, 0);
		this.chkDates.AutoSize = true;
		this.chkDates.Location = new System.Drawing.Point(12, 272);
		this.chkDates.Name = "chkDates";
		this.chkDates.Size = new System.Drawing.Size(109, 17);
		this.chkDates.TabIndex = 20;
		this.chkDates.Text = "Use Date Interval";
		this.chkDates.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
		base.ClientSize = new System.Drawing.Size(877, 504);
		base.Controls.Add(this.chkDates);
		base.Controls.Add(this.dtTo);
		base.Controls.Add(this.dtFrom);
		base.Controls.Add(this.ckIsIfd);
		base.Controls.Add(this.pnlGeneration);
		base.Controls.Add(this.btnRefresh);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.lblPassword);
		base.Controls.Add(this.txtPassword);
		base.Controls.Add(this.lblUserName);
		base.Controls.Add(this.txtUserName);
		base.Controls.Add(this.lblOrganizationService);
		base.Controls.Add(this.txtOrganizationService);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "Main";
		this.Text = "Document Generator 1.0";
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rclEntities).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radCheckedDropDownList1).EndInit();
		this.pnlGeneration.ResumeLayout(false);
		this.pnlGeneration.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
