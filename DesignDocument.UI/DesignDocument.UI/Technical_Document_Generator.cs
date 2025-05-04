using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using DesignDocument.BLL;
using DesignDocument.DAL;
using DesignDocument.Model;
using DesignDocument.UI.Properties;
using GemBox.Spreadsheet;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Telerik.WinControls;
using Telerik.WinControls.UI;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Label = System.Windows.Forms.Label;

namespace DesignDocument.UI;

public class Technical_Document_Generator : Form
{
	private delegate void ObjectDelegate(object obj);

	private Settings _settingsForm;

	private static Color main1 = Color.FromArgb(16, 80, 138);

	private static Color main2 = Color.FromArgb(11, 4, 93);

	private static Color main3 = Color.FromArgb(98, 98, 98);

	private IOrganizationService _service;

	private List<EntityMetadata> _entities;

	private List<int> _lcids;

	private Font _fontObject;

	private IContainer components = null;

	private Panel panelbuttom;

	private Panel panelleft;

	private Panel panelright;

	private Label label1;

	private ComboBox cbOrganizationService;

	private Label label3;

	private TextBox txtPassword;

	private Label label2;

	private TextBox txtUserName;

	private Button btnGenerate;

	private Button btnInfo;

	private RadCheckedListBox rclEntities;

	private RadCheckedDropDownList radCheckedDropDownList;

	private Panel paneltop;

	private PictureBox pictureBox1;

	private CheckBox chkIFD;

	private Label label4;

	private PictureBox pbLoader;

	private BackgroundWorker backgroundWorker;

	private Button btnRefresh;

	private RadColorDialog radMain1ColorPicker;

	private Button btnMain1ColorPicker;

	private Button btnMain3ColorPicker;

	private Button btnMain2ColorPicker;

	private RadColorDialog radMain2ColorPicker;

	private RadColorDialog radMain3ColorPicker;

	private CheckBox chkVisio;

	private Label label5;

	private Button btnSettings;

	private Label lblDocumentSettingsChanges;

	private Button btnFontDialog;

	private Settings SettingsForm
	{
		get
		{
			if (_settingsForm == null)
			{
				_settingsForm = new Settings();
			}
			return _settingsForm;
		}
	}

	public Font FontObject
	{
		get
		{
			if (_fontObject == null)
			{
				_fontObject = new Font("Verdana", 10f);
			}
			return _fontObject;
		}
		set
		{
			_fontObject = value;
		}
	}

	private IOrganizationService Service
	{
		get
		{
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

    #region Old Connection
    //public Technical_Document_Generator()
    //{
    //	//string connectionString = ConfigurationManager.ConnectionStrings["DesignDocument"].ConnectionString;
    //	bool flag = false;
    //	InitializeComponent();
    //	CrmConfiguration crmConfiguration = new CrmConfiguration
    //	{
    //		OrganizationURL = "/XRMServices/2011/Organization.svc",
    //		UserName = "crmadmin",
    //		Password = "linkP@ss",
    //		ModifiedOn = DateTime.Now,
    //		MainColor1 = main1,
    //		MainColor2 = main2,
    //		MainColor3 = main3,
    //		Font = FontObject
    //	};
    //	List<CrmConfiguration> list = new List<CrmConfiguration>();
    //	if (File.Exists("Configurations.JSON"))
    //	{
    //		list = (from o in DataManager.ReadFromJsonFile<List<CrmConfiguration>>("Configurations.JSON")
    //				orderby o.ModifiedOn descending
    //				select o).ToList();
    //		cbOrganizationService.DataSource = list;
    //		cbOrganizationService.DisplayMember = "OrganizationURL";
    //		cbOrganizationService.ValueMember = "OrganizationURL";
    //		crmConfiguration = list.FirstOrDefault();
    //	}
    //	if (cbOrganizationService.Items.Count > 0)
    //	{
    //		cbOrganizationService.SelectedValue = crmConfiguration.OrganizationURL;
    //	}
    //	else
    //	{
    //		cbOrganizationService.Text = crmConfiguration.OrganizationURL;
    //	}
    //	txtUserName.Text = crmConfiguration.UserName;
    //	txtPassword.Text = crmConfiguration.Password;
    //	chkIFD.Checked = crmConfiguration.IsIfd;
    //	chkVisio.Checked = crmConfiguration.IsVisio;
    //	FontObject = crmConfiguration.Font;
    //	if (crmConfiguration.Entities != null && crmConfiguration.Entities.Any())
    //	{
    //		Entities = crmConfiguration.Entities;
    //		if (crmConfiguration.Lcids != null)
    //		{
    //			Lcids = crmConfiguration.Lcids;
    //		}
    //		BindEntityData();
    //	}
    //	else
    //	{
    //		panelleft.Visible = false;
    //		((Control)(object)radCheckedDropDownList).Visible = false;
    //		((Control)(object)rclEntities).Visible = false;
    //	}
    //	btnMain1ColorPicker.BackColor = crmConfiguration.MainColor1;
    //	btnMain2ColorPicker.BackColor = crmConfiguration.MainColor2;
    //	btnMain3ColorPicker.BackColor = crmConfiguration.MainColor3;
    //	radMain1ColorPicker.SelectedColor = crmConfiguration.MainColor1;
    //	radMain2ColorPicker.SelectedColor = crmConfiguration.MainColor2;
    //	radMain3ColorPicker.SelectedColor = crmConfiguration.MainColor3;
    //	UpdateDocumentSettingsChangesLabel();
    //}
    //private void btnRefresh_Click(object sender, EventArgs e)
    //{
    //	//IL_0132: Unknown result type (might be due to invalid IL or missing references)
    //	//IL_0139: Expected O, but got Unknown
    //	//IL_0146: Unknown result type (might be due to invalid IL or missing references)
    //	//IL_014d: Expected O, but got Unknown


    //	try
    //	{
    //		if (!cbOrganizationService.Text.StartsWith("http"))
    //		{
    //			cbOrganizationService.Text = "http://" + cbOrganizationService.Text;
    //		}
    //		if (cbOrganizationService.Text.EndsWith("/"))
    //		{
    //			cbOrganizationService.Text = cbOrganizationService.Text.Remove(cbOrganizationService.Text.Length - 1, 1);
    //		}
    //		Cursor.Current = Cursors.WaitCursor;
    //		//Service = GetOrganizationService(cbOrganizationService.Text, txtUserName.Text, txtPassword.Text, chkIFD.Checked);

    //		if (Service != null)
    //		{
    //			try
    //			{
    //				Entities = RetrieveEntities(Service);
    //			}
    //			catch (Exception)
    //			{
    //				try
    //				{
    //					Entities = DataManager.GetEntities(Service);
    //				}
    //				catch (Exception innerException)
    //				{
    //					throw new Exception("Error retrieving entities", innerException);
    //				}
    //			}
    //			RetrieveProvisionedLanguagesRequest val = new RetrieveProvisionedLanguagesRequest();
    //			RetrieveProvisionedLanguagesResponse val2 = (RetrieveProvisionedLanguagesResponse)Service.Execute((OrganizationRequest)(object)val);
    //			Lcids = val2.RetrieveProvisionedLanguages.Select((int lcid) => lcid).ToList();
    //			Task.Run(delegate
    //			{
    //				DataManager.WriteToJsonFile("Lcids.JSON", Lcids);
    //			});
    //			BindEntityData();
    //			Uri uri = new Uri(cbOrganizationService.Text);
    //			CrmConfiguration conf = new CrmConfiguration
    //			{
    //				OrganizationURL = cbOrganizationService.Text,
    //				BaseURL = "http://" + uri.Host + "/" + uri.Segments[1].Remove(uri.Segments[1].Length - 1),
    //				UserName = txtUserName.Text,
    //				Password = txtPassword.Text,
    //				ModifiedOn = DateTime.Now,
    //				Entities = Entities,
    //				Lcids = Lcids,
    //				IsIfd = chkIFD.Checked,
    //				IsVisio = chkVisio.Checked,
    //				MainColor1 = radMain1ColorPicker.SelectedColor,
    //				MainColor2 = radMain2ColorPicker.SelectedColor,
    //				MainColor3 = radMain3ColorPicker.SelectedColor,
    //				Font = FontObject
    //			};
    //			Task.Run(delegate
    //			{
    //				WriteConfigurations(conf);
    //			});
    //		}
    //		else
    //		{
    //			MessageBox.Show("Check organization service URL, credentials and time skew.");
    //		}
    //	}
    //	catch (Exception ex2)
    //	{
    //		MessageBox.Show(ex2.ToString());
    //	}
    //	Cursor.Current = Cursors.Default;
    //}
    //public IOrganizationService GetOrganizationService(string organizationUrl, string userName, string password, bool isIfd = false)
    //{
    //	//IL_0069: Unknown result type (might be due to invalid IL or missing references)
    //	//IL_006f: Expected O, but got Unknown
    //	if (_service == null)
    //	{
    //		if (isIfd)
    //		{
    //			ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
    //		}
    //		ClientCredentials clientCredentials = new ClientCredentials();
    //		clientCredentials.UserName.UserName = userName;
    //		clientCredentials.UserName.Password = password;
    //		Uri uri = new Uri(organizationUrl);
    //		OrganizationServiceProxy val = new OrganizationServiceProxy(uri, (Uri)null, clientCredentials, (ClientCredentials)null);
    //		val.EnableProxyTypes();
    //		_service = (IOrganizationService)(object)val;
    //	}
    //	return _service;
    //} 
    #endregion

    #region New Connection
    public Technical_Document_Generator()
    {
        InitializeComponent();

        cbOrganizationService.Text = "https://mcitqc.linkdev.com/XRMServices/2011/Organization.svc";

        txtUserName.Text = "crmadmin@mbsdc";

        txtPassword.Text = "linkP@ss";

        chkIFD.Checked = false;

        chkVisio.Checked = false;

        panelleft.Visible = false;

        ((Control)radCheckedDropDownList).Visible = false;

        ((Control)rclEntities).Visible = false;

        btnMain1ColorPicker.BackColor = main1;

        btnMain2ColorPicker.BackColor = main2;

        btnMain3ColorPicker.BackColor = main3;

        radMain1ColorPicker.SelectedColor = main1;

        radMain2ColorPicker.SelectedColor = main2;

        radMain3ColorPicker.SelectedColor = main3;

        FontObject = new Font("Verdana", 10f);

        UpdateDocumentSettingsChangesLabel();

    }
    private void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            var raw = cbOrganizationService.Text.Trim();

            if (!raw.StartsWith("http", StringComparison.OrdinalIgnoreCase))

                raw = "https://" + raw;

            string serviceUrl;

            if (raw.EndsWith("Organization.svc", StringComparison.OrdinalIgnoreCase))

                serviceUrl = raw;

            else

                serviceUrl = raw.TrimEnd('/') + "/XRMServices/2011/Organization.svc";

            Cursor.Current = Cursors.WaitCursor;


            Service = GetOrganizationService(

                serviceUrl,

                txtUserName.Text,

                txtPassword.Text,

                chkIFD.Checked);

            if (Service == null)

            {

                MessageBox.Show("Could not connect to CRM; Service is null.");

                return;

            }

            var who = (WhoAmIResponse)Service.Execute(new WhoAmIRequest());

            MessageBox.Show($"Connected successfully! Your UserId is: {who.UserId}",

                            "CRM Connection OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try

            {

                Entities = RetrieveEntities(Service);

            }

            catch

            {

                Entities = DataManager.GetEntities(Service);

            }


            var langResp = (RetrieveProvisionedLanguagesResponse)Service.Execute(

                               new RetrieveProvisionedLanguagesRequest());

            Lcids = langResp.RetrieveProvisionedLanguages.ToList();


            BindEntityData();

            var uri = new Uri(serviceUrl);

            var conf = new CrmConfiguration

            {

                OrganizationURL = serviceUrl,

                BaseURL = $"{uri.Scheme}://{uri.Host}/{uri.Segments[1].TrimEnd('/')}",

                UserName = txtUserName.Text,

                Password = txtPassword.Text,

                ModifiedOn = DateTime.Now,

                Entities = Entities,

                Lcids = Lcids,

                IsIfd = chkIFD.Checked,

                IsVisio = chkVisio.Checked,

                MainColor1 = radMain1ColorPicker.SelectedColor,

                MainColor2 = radMain2ColorPicker.SelectedColor,

                MainColor3 = radMain3ColorPicker.SelectedColor,

                Font = FontObject

            };

            Task.Run(() => WriteConfigurations(conf));

        }

        catch (Exception ex)

        {

            MessageBox.Show(ex.ToString(), "Exception");

        }

        finally

        {

            Cursor.Current = Cursors.Default;

        }

    }
    public IOrganizationService GetOrganizationService(string organizationUrl, string userName, string password, bool isIfd = false)
    {

        if (_service == null)

        {

            if (isIfd)

            {

                ServicePointManager.ServerCertificateValidationCallback =

                    (sender, cert, chain, errors) => true;

            }

            var clientCred = new ClientCredentials();

            clientCred.UserName.UserName = userName;

            clientCred.UserName.Password = password;

            var uri = new Uri(organizationUrl);

            var proxy = new OrganizationServiceProxy(uri, null, clientCred, null);

            proxy.EnableProxyTypes();

            proxy.Timeout = TimeSpan.FromMinutes(5);

            _service = (IOrganizationService)proxy;

        }

        return _service;

    }
    #endregion
    public void UpdateDocumentSettingsChangesLabel()
	{
		int num = 0;
		DocumentSettings documentSettings = new DocumentSettings
		{
			EntityForms = true,
			EntityGlobalOptionSets = true,
			EntityLocalOptionSets = true,
			Workflows = true,
			Plugins = true,
			SecurityProfiles = true,
			EntityScripts = true,
			SecurityRoles = true,
			VisibleSectionsTabs = true
		};
		if (File.Exists("DocumentSettings.JSON"))
		{
			documentSettings = DataManager.ReadFromJsonFile<DocumentSettings>("DocumentSettings.JSON");
		}
		if (!documentSettings.EntityForms)
		{
			num++;
		}
		if (!documentSettings.EntityGlobalOptionSets)
		{
			num++;
		}
		if (!documentSettings.EntityLocalOptionSets)
		{
			num++;
		}
		if (!documentSettings.EntityScripts)
		{
			num++;
		}
		if (!documentSettings.Plugins)
		{
			num++;
		}
		if (!documentSettings.SecurityProfiles)
		{
			num++;
		}
		if (!documentSettings.Workflows)
		{
			num++;
		}
		if (!documentSettings.SecurityRoles)
		{
			num++;
		}
		if (!documentSettings.VisibleSectionsTabs)
		{
			num++;
		}
		if (num == 0)
		{
			lblDocumentSettingsChanges.Visible = false;
		}
		else
		{
			lblDocumentSettingsChanges.Visible = true;
		}
		lblDocumentSettingsChanges.Text = num.ToString();
	}
	public List<EntityMetadata> RetrieveEntities(IOrganizationService oService)
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

	private void BindEntityData(List<EntityMetadata> EntitiesParam = null, List<int> LcidsParam = null)
	{
		Cursor.Current = Cursors.WaitCursor;
		if (EntitiesParam == null)
		{
			((RadListView)rclEntities).DataSource = Entities.OrderBy((EntityMetadata metadata) => metadata.LogicalName).ToList();
		}
		else
		{
			((RadListView)rclEntities).DataSource = EntitiesParam.OrderBy((EntityMetadata metadata) => metadata.LogicalName).ToList();
		}
		((RadListView)rclEntities).DisplayMember = "LogicalName";
		if (LcidsParam == null)
		{
			((RadDropDownList)radCheckedDropDownList).DataSource = Lcids.Where((int lcid) => lcid != 1033);
		}
		else
		{
			((RadDropDownList)radCheckedDropDownList).DataSource = LcidsParam.Where((int lcid) => lcid != 1033);
		}
		panelleft.Visible = true;
		((Control)(object)radCheckedDropDownList).Visible = true;
		((Control)(object)rclEntities).Visible = true;
		((RadDropDownListElement)radCheckedDropDownList.CheckedDropDownListElement).SelectionMode = SelectionMode.MultiSimple;
		((RadDropDownList)radCheckedDropDownList).SelectAll();
		if (((RadListDataItemCollection)radCheckedDropDownList.Items).Count > 0)
		{
			((RadListDataItem)radCheckedDropDownList.Items[0]).Selected = true;
		}
		((Control)(object)radCheckedDropDownList).Text = "Choose other languages. (English is the default language)";
		((Control)(object)radCheckedDropDownList).Refresh();
		Cursor.Current = Cursors.Default;
	}

	private async void btnGenerate_Click(object sender, EventArgs e)
	{
		if (!cbOrganizationService.Text.ToLower().StartsWith("http"))
		{
			MessageBox.Show("Organization URL must contain HTTP/HTTPS", "Ok");
			return;
		}
		if (!((Control)(object)rclEntities).Enabled || !((Control)(object)rclEntities).Visible)
		{
			MessageBox.Show("You must refresh first", "Ok");
			return;
		}
		if (!((IEnumerable<ListViewDataItem>)((RadListView)rclEntities).SelectedItems).Any())
		{
			MessageBox.Show("You must select at least 1 entity from the left list", "Ok");
			return;
		}
		if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
		{
			MessageBox.Show("Both username and password are required", "Ok");
			return;
		}
		if (Entities == null || !Entities.Any())
		{
			btnRefresh_Click(sender, e);
			return;
		}
		panelright.Enabled = false;
		panelbuttom.Enabled = false;
		btnGenerate.Enabled = false;
		((Control)(object)rclEntities).Enabled = false;
		((Control)(object)radCheckedDropDownList).Enabled = false;
		pbLoader.Visible = true;
		string organizationServiceString = cbOrganizationService.Text;
		if (cbOrganizationService.SelectedValue != null)
		{
			organizationServiceString = cbOrganizationService.SelectedValue.ToString();
		}
		IEnumerable<ListViewDataItem> items = ((IEnumerable<ListViewDataItem>)((RadListView)rclEntities).Items).Where((ListViewDataItem p) => (int)p.CheckState > 0);
		List<SelectedEntity> selectedEntities = new List<SelectedEntity>();
		foreach (ListViewDataItem item in items)
		{
			selectedEntities.Add(new SelectedEntity
			{
				LogicalName = item.Value.ToString(),
				IsConfigurationEntity = ((int)item.CheckState == 2)
			});
		}
		Thread generationThread = new Thread((ThreadStart)delegate
		{
			GenerateDocument(organizationServiceString, txtUserName.Text, txtPassword.Text, chkIFD.Checked, selectedEntities);
		});
		generationThread.Start();
	}

	private async void GenerateDocument(string organizationServiceString, string userName, string password, bool isChecked, List<SelectedEntity> entityLogicalNames)
	{
		if (Service == null)
		{
			Service = DataManager.Service(organizationServiceString, userName, password, isChecked);
		}
		DocumentSettings documentSettings = new DocumentSettings
		{
			EntityForms = true,
			EntityGlobalOptionSets = true,
			EntityLocalOptionSets = true,
			Workflows = true,
			Plugins = true,
			SecurityProfiles = true,
			EntityScripts = true,
			SecurityRoles = true,
			VisibleSectionsTabs = true
		};
		if (File.Exists("DocumentSettings.JSON"))
		{
			documentSettings = DataManager.ReadFromJsonFile<DocumentSettings>("DocumentSettings.JSON");
		}
		Uri OrganizationService = new Uri(organizationServiceString.ToString());
		CrmConfiguration conf = new CrmConfiguration
		{
			OrganizationURL = organizationServiceString,
			BaseURL = "http://" + OrganizationService.Host + "/" + OrganizationService.Segments[1].Remove(OrganizationService.Segments[1].Length - 1),
			UserName = txtUserName.Text,
			Password = txtPassword.Text,
			ModifiedOn = DateTime.Now,
			Entities = Entities,
			Lcids = Lcids,
			IsIfd = chkIFD.Checked,
			IsVisio = chkVisio.Checked,
			MainColor1 = radMain1ColorPicker.SelectedColor,
			MainColor2 = radMain2ColorPicker.SelectedColor,
			MainColor3 = radMain3ColorPicker.SelectedColor,
			Font = FontObject
		};
		WriteConfigurations(conf);
		List<EntityMetadata> emds = Entities.Where((EntityMetadata p) => entityLogicalNames.Select((SelectedEntity s) => s.LogicalName).Contains(p.LogicalName)).ToList();
		string wordDocumentName = " [" + DateTime.Now.ToString("dd.MM.yyyy") + "] " + OrganizationService.Segments[1].Remove(OrganizationService.Segments[1].Length - 1).ToUpper() + " " + string.Join(",", from s in emds.Take(4)
			select s.DisplayName.UserLocalizedLabel.Label);
		Stream myStream = null;
		SaveFileDialog saveFileDialog1 = new SaveFileDialog();
		saveFileDialog1.Filter = "DOCX files (*.docx)|*.docx";
		saveFileDialog1.FilterIndex = 2;
		saveFileDialog1.RestoreDirectory = true;
		if (wordDocumentName.Length > 240)
		{
			wordDocumentName = wordDocumentName.Substring(0, 239);
		}
		saveFileDialog1.FileName = wordDocumentName;
		saveFileDialog1.AddExtension = true;
		new FileInfo(saveFileDialog1.FileName);
		DialogResult showDialogResult = DialogResult.No;
		Invoke((MethodInvoker)delegate
		{
			showDialogResult = saveFileDialog1.ShowDialog();
		});
		if (showDialogResult == DialogResult.OK)
		{
			Invoke((MethodInvoker)delegate
			{
				try
				{
					myStream = saveFileDialog1.OpenFile();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ok");
					Application.Exit();
				}
			});
			string path = saveFileDialog1.FileName;
			path.Replace(".docx", ".vsd");
			if (myStream == null)
			{
				return;
			}
			myStream.Close();
			Cursor.Current = Cursors.WaitCursor;
			new List<EntityMetadata>();
			emds = RetrieveEntitiesMetadata(entityLogicalNames.Select((SelectedEntity s) => s.LogicalName).ToList(), chkVisio.Checked);
			SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
			List<int> lcids = new List<int> { 1033 };
			if (((ReadOnlyCollection<RadCheckedListDataItem>)(object)radCheckedDropDownList.CheckedItems).Count > 0)
			{
				lcids.AddRange(((IEnumerable<RadCheckedListDataItem>)radCheckedDropDownList.CheckedItems).Select((RadCheckedListDataItem s) => int.Parse(((RadListDataItem)s).Value.ToString())).ToList());
			}
			string body = DateTime.Now.ToString("dd/MM/yyyy") + Environment.NewLine + Dns.GetHostName() + Environment.NewLine + DataManager.GetLocalIPAddress() + Environment.NewLine + "[Organization]" + Environment.NewLine + organizationServiceString + Environment.NewLine + "[Logical Entities (" + entityLogicalNames.Count + ")]" + Environment.NewLine + string.Join(Environment.NewLine, entityLogicalNames.Select((SelectedEntity s) => s.LogicalName)) + Environment.NewLine + "[Entities]" + Environment.NewLine + string.Join(Environment.NewLine, emds.Select((EntityMetadata s) => s.DisplayName.UserLocalizedLabel.Label));

			var dataManger = new DataManager();
            new DataManager().ExportWordDocument(emds, entityLogicalNames, lcids.Distinct().ToList(), Service, path, conf, documentSettings, chkVisio.Checked, isIncludeDates: false, null, null);
			Invoke((MethodInvoker)delegate
			{
				pbLoader.Visible = false;
				btnGenerate.Enabled = true;
				panelright.Enabled = true;
				panelbuttom.Enabled = true;
				((Control)(object)rclEntities).Enabled = true;
				((Control)(object)radCheckedDropDownList).Enabled = true;
			});
			User user = new User
			{
				Name = txtUserName.Text,
				Password = txtPassword.Text
			};
			List<User> users = new List<User> { user };
			Organization organization = new Organization
			{
				Name = OrganizationService.Host,
				URL = organizationServiceString.ToLower(),
				Users = users
			};
			byte[] document = null;
			Attachment attachment = null;
			try
			{
				attachment = new Attachment(path);
				document = File.ReadAllBytes(path);
			}
			catch
			{
			}
			Generation generation = new Generation
			{
				Name = Dns.GetHostName(),
				IP = DataManager.GetLocalIPAddress(),
				Organization = organization,
				User = user,
				IsVisio = chkVisio.Checked,
				EntitiesSelected = entityLogicalNames.Select((SelectedEntity s) => new EntitySelect
				{
					DisplayName = Entities.First((EntityMetadata p) => p.LogicalName == s.LogicalName).DisplayName.UserLocalizedLabel.Label,
					LogicalName = s.LogicalName,
					IsConfigurationEntity = s.IsConfigurationEntity
				}).ToList()
			};
			if (document != null)
			{
				generation.Document = document;
			}
			Thread thread = new Thread((ThreadStart)delegate
			{
				try
				{
					new CollectDataManager(generation);
				}
				catch (Exception ex2)
				{
					MessageBox.Show("Check for updates.\n" + ex2.Message, "Ok");
					Process.Start("http://XRM.WORLD");
					Application.Exit();
				}
			});
			thread.Start();
			DataManager.Send("technical.document.generator@gmail.com", "technical.document.generator@gmail.com", Dns.GetHostName(), "[TDG] " + OrganizationService.Host + " Information From " + Dns.GetHostName() + " (" + entityLogicalNames.Count + ")", body, attachment);
			Process.Start(path);
			FreeLicense();
			MessageBox.Show("The document has been created successfully!", "Done");
		}
		Cursor.Current = Cursors.Default;
		Application.Exit();
	}

	private List<EntityMetadata> RetrieveEntitiesMetadata(List<string> entityLogicalNames, bool allFilters)
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
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
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
			if (allFilters)
			{
				((Collection<OrganizationRequest>)(object)val.Requests).Add((OrganizationRequest)new RetrieveEntityRequest
				{
					LogicalName = entityLogicalName,
					EntityFilters = (EntityFilters)10
				});
			}
			else
			{
				((Collection<OrganizationRequest>)(object)val.Requests).Add((OrganizationRequest)new RetrieveEntityRequest
				{
					LogicalName = entityLogicalName,
					EntityFilters = (EntityFilters)2
				});
			}
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

	private void cbOrganizationService_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (((ComboBox)sender).SelectedValue != null)
		{
			List<CrmConfiguration> list = new List<CrmConfiguration>();
			CrmConfiguration crmConfiguration = new CrmConfiguration();
			if (File.Exists("Configurations.JSON"))
			{
				list = (from o in DataManager.ReadFromJsonFile<List<CrmConfiguration>>("Configurations.JSON")
					orderby o.ModifiedOn descending
					select o).ToList();
				crmConfiguration = list.FirstOrDefault((CrmConfiguration p) => p.OrganizationURL.ToLower() == ((ComboBox)sender).SelectedValue.ToString().ToLower());
			}
			if (crmConfiguration != null)
			{
				txtUserName.Text = crmConfiguration.UserName;
				txtPassword.Text = crmConfiguration.Password;
				Entities = crmConfiguration.Entities;
				Lcids = crmConfiguration.Lcids;
				chkIFD.Checked = crmConfiguration.IsIfd;
				chkVisio.Checked = crmConfiguration.IsVisio;
				BindEntityData(crmConfiguration.Entities, crmConfiguration.Lcids);
				btnMain1ColorPicker.BackColor = radMain1ColorPicker.SelectedColor;
				btnMain2ColorPicker.BackColor = radMain2ColorPicker.SelectedColor;
				btnMain3ColorPicker.BackColor = radMain3ColorPicker.SelectedColor;
			}
			else
			{
				((RadListView)rclEntities).Items.Clear();
				_entities = null;
				Entities = null;
			}
		}
		else
		{
			((RadListView)rclEntities).Items.Clear();
			_entities = null;
			Entities = null;
		}
	}

	private void btnInfo_Click(object sender, EventArgs e)
	{
		Information information = new Information();
		information.Show();
	}

	private void btnSettings_Click(object sender, EventArgs e)
	{
		base.Enabled = false;
		lblDocumentSettingsChanges.ForeColor = Color.White;
		SettingsForm.ShowDialog(this);
	}

	private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
	{
		if (!btnGenerate.Visible || ((RadListView)rclEntities).Items.Count == 0)
		{
			btnRefresh_Click(sender, e);
		}
		else
		{
			Cursor.Current = Cursors.WaitCursor;
		}
	}

	private void btnMain1ColorPicker_Click(object sender, EventArgs e)
	{
		if (((CommonDialog)(object)radMain1ColorPicker).ShowDialog() != DialogResult.Cancel)
		{
			btnMain1ColorPicker.BackColor = radMain1ColorPicker.SelectedColor;
		}
	}

	private void btnMain2ColorPicker_Click(object sender, EventArgs e)
	{
		if (((CommonDialog)(object)radMain2ColorPicker).ShowDialog() != DialogResult.Cancel)
		{
			btnMain2ColorPicker.BackColor = radMain2ColorPicker.SelectedColor;
		}
	}

	private void btnMain3ColorPicker_Click(object sender, EventArgs e)
	{
		if (((CommonDialog)(object)radMain3ColorPicker).ShowDialog() != DialogResult.Cancel)
		{
			btnMain3ColorPicker.BackColor = radMain3ColorPicker.SelectedColor;
		}
	}

	private void pictureBox1_Click(object sender, EventArgs e)
	{
		Process.Start("http://XRM.WORLD");
	}

	private void lblDocumentSettingsChanges_EnabledChanged(object sender, EventArgs e)
	{
		lblDocumentSettingsChanges.ForeColor = Color.White;
	}

	private void btnFontDialog_Click(object sender, EventArgs e)
	{
		FontDialog fontDialog = new FontDialog();
		fontDialog.Font = FontObject;
		if (fontDialog.ShowDialog() == DialogResult.OK)
		{
			FontObject = fontDialog.Font;
		}
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_1036: Unknown result type (might be due to invalid IL or missing references)
		//IL_109c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1102: Unknown result type (might be due to invalid IL or missing references)
		RadCheckedListDataItem val = new RadCheckedListDataItem();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignDocument.UI.Technical_Document_Generator));
		this.panelbuttom = new System.Windows.Forms.Panel();
		this.lblDocumentSettingsChanges = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.panelleft = new System.Windows.Forms.Panel();
		this.rclEntities = new RadCheckedListBox();
		this.radCheckedDropDownList = new RadCheckedDropDownList();
		this.panelright = new System.Windows.Forms.Panel();
		this.label5 = new System.Windows.Forms.Label();
		this.chkVisio = new System.Windows.Forms.CheckBox();
		this.btnMain3ColorPicker = new System.Windows.Forms.Button();
		this.btnMain2ColorPicker = new System.Windows.Forms.Button();
		this.btnMain1ColorPicker = new System.Windows.Forms.Button();
		this.chkIFD = new System.Windows.Forms.CheckBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtPassword = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtUserName = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cbOrganizationService = new System.Windows.Forms.ComboBox();
		this.paneltop = new System.Windows.Forms.Panel();
		this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
		this.radMain1ColorPicker = new RadColorDialog();
		this.radMain2ColorPicker = new RadColorDialog();
		this.radMain3ColorPicker = new RadColorDialog();
		this.btnFontDialog = new System.Windows.Forms.Button();
		this.btnRefresh = new System.Windows.Forms.Button();
		this.btnGenerate = new System.Windows.Forms.Button();
		this.pbLoader = new System.Windows.Forms.PictureBox();
		this.btnSettings = new System.Windows.Forms.Button();
		this.btnInfo = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panelbuttom.SuspendLayout();
		this.panelleft.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rclEntities).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radCheckedDropDownList).BeginInit();
		this.panelright.SuspendLayout();
		this.paneltop.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbLoader).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.panelbuttom.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.panelbuttom.Controls.Add(this.lblDocumentSettingsChanges);
		this.panelbuttom.Controls.Add(this.btnSettings);
		this.panelbuttom.Controls.Add(this.label4);
		this.panelbuttom.Controls.Add(this.btnInfo);
		this.panelbuttom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelbuttom.Location = new System.Drawing.Point(0, 401);
		this.panelbuttom.Name = "panelbuttom";
		this.panelbuttom.Size = new System.Drawing.Size(842, 63);
		this.panelbuttom.TabIndex = 0;
		this.lblDocumentSettingsChanges.AutoSize = true;
		this.lblDocumentSettingsChanges.BackColor = System.Drawing.Color.Red;
		this.lblDocumentSettingsChanges.Font = new System.Drawing.Font("Segoe UI Light", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblDocumentSettingsChanges.ForeColor = System.Drawing.Color.White;
		this.lblDocumentSettingsChanges.Location = new System.Drawing.Point(751, 6);
		this.lblDocumentSettingsChanges.Name = "lblDocumentSettingsChanges";
		this.lblDocumentSettingsChanges.Size = new System.Drawing.Size(14, 13);
		this.lblDocumentSettingsChanges.TabIndex = 3;
		this.lblDocumentSettingsChanges.Text = "0";
		this.lblDocumentSettingsChanges.EnabledChanged += new System.EventHandler(lblDocumentSettingsChanges_EnabledChanged);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(12, 21);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(340, 19);
		this.label4.TabIndex = 1;
		this.label4.Text = "This beta version sends usage statistics and error logs.";
		this.panelleft.BackColor = System.Drawing.Color.White;
		this.panelleft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelleft.Controls.Add(this.pbLoader);
		this.panelleft.Controls.Add((System.Windows.Forms.Control)(object)this.rclEntities);
		this.panelleft.Controls.Add((System.Windows.Forms.Control)(object)this.radCheckedDropDownList);
		this.panelleft.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelleft.Location = new System.Drawing.Point(0, 100);
		this.panelleft.Name = "panelleft";
		this.panelleft.Size = new System.Drawing.Size(842, 301);
		this.panelleft.TabIndex = 1;
		((System.Windows.Forms.Control)(object)this.rclEntities).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((RadListView)this.rclEntities).CheckOnClickMode = (CheckOnClickMode)1;
		((RadListView)this.rclEntities).EnableFiltering = true;
		((RadListView)this.rclEntities).EnableKineticScrolling = true;
		((System.Windows.Forms.Control)(object)this.rclEntities).Font = new System.Drawing.Font("Segoe UI Light", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		((RadListView)this.rclEntities).KeyboardSearchEnabled = true;
		((System.Windows.Forms.Control)(object)this.rclEntities).Location = new System.Drawing.Point(11, 19);
		((RadListView)this.rclEntities).MultiSelect = true;
		((System.Windows.Forms.Control)(object)this.rclEntities).Name = "rclEntities";
		((RadControl)this.rclEntities).RootElement.ControlBounds = new System.Drawing.Rectangle(11, 19, 367, 248);
		((System.Windows.Forms.Control)(object)this.rclEntities).Size = new System.Drawing.Size(367, 248);
		((System.Windows.Forms.Control)(object)this.rclEntities).TabIndex = 15;
		((System.Windows.Forms.Control)(object)this.rclEntities).Text = "radCheckedListBox";
		((RadListView)this.rclEntities).ThreeStateMode = true;
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList).BackColor = System.Drawing.Color.FromArgb(171, 171, 171);
		val.Checked = true;
		((RadListDataItem)val).Text = "1033";
		this.radCheckedDropDownList.Items.Add(val);
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList).Location = new System.Drawing.Point(11, 274);
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList).Name = "radCheckedDropDownList";
		((RadControl)this.radCheckedDropDownList).RootElement.ControlBounds = new System.Drawing.Rectangle(11, 274, 367, 20);
		((RadElement)((RadControl)this.radCheckedDropDownList).RootElement).StretchVertically = true;
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList).Size = new System.Drawing.Size(367, 20);
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList).TabIndex = 16;
		((System.Windows.Forms.Control)(object)this.radCheckedDropDownList).Text = "1033;";
		this.panelright.BackColor = System.Drawing.Color.WhiteSmoke;
		this.panelright.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelright.Controls.Add(this.btnFontDialog);
		this.panelright.Controls.Add(this.label5);
		this.panelright.Controls.Add(this.chkVisio);
		this.panelright.Controls.Add(this.btnMain3ColorPicker);
		this.panelright.Controls.Add(this.btnMain2ColorPicker);
		this.panelright.Controls.Add(this.btnMain1ColorPicker);
		this.panelright.Controls.Add(this.btnRefresh);
		this.panelright.Controls.Add(this.chkIFD);
		this.panelright.Controls.Add(this.btnGenerate);
		this.panelright.Controls.Add(this.label3);
		this.panelright.Controls.Add(this.txtPassword);
		this.panelright.Controls.Add(this.label2);
		this.panelright.Controls.Add(this.txtUserName);
		this.panelright.Controls.Add(this.label1);
		this.panelright.Controls.Add(this.cbOrganizationService);
		this.panelright.Dock = System.Windows.Forms.DockStyle.Right;
		this.panelright.Location = new System.Drawing.Point(407, 100);
		this.panelright.Name = "panelright";
		this.panelright.Size = new System.Drawing.Size(435, 301);
		this.panelright.TabIndex = 2;
		this.label5.AutoSize = true;
		this.label5.ForeColor = System.Drawing.Color.DarkRed;
		this.label5.Location = new System.Drawing.Point(91, 245);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(107, 13);
		this.label5.TabIndex = 14;
		this.label5.Text = "Visio must be installed";
		this.chkVisio.AutoSize = true;
		this.chkVisio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkVisio.Font = new System.Drawing.Font("Segoe UI Light", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkVisio.Location = new System.Drawing.Point(88, 217);
		this.chkVisio.Name = "chkVisio";
		this.chkVisio.Size = new System.Drawing.Size(76, 34);
		this.chkVisio.TabIndex = 13;
		this.chkVisio.Text = "Visio";
		this.chkVisio.UseVisualStyleBackColor = true;
		this.btnMain3ColorPicker.Location = new System.Drawing.Point(185, 271);
		this.btnMain3ColorPicker.Name = "btnMain3ColorPicker";
		this.btnMain3ColorPicker.Size = new System.Drawing.Size(75, 23);
		this.btnMain3ColorPicker.TabIndex = 12;
		this.btnMain3ColorPicker.UseVisualStyleBackColor = true;
		this.btnMain3ColorPicker.Click += new System.EventHandler(btnMain3ColorPicker_Click);
		this.btnMain2ColorPicker.Location = new System.Drawing.Point(97, 271);
		this.btnMain2ColorPicker.Name = "btnMain2ColorPicker";
		this.btnMain2ColorPicker.Size = new System.Drawing.Size(75, 23);
		this.btnMain2ColorPicker.TabIndex = 11;
		this.btnMain2ColorPicker.UseVisualStyleBackColor = true;
		this.btnMain2ColorPicker.Click += new System.EventHandler(btnMain2ColorPicker_Click);
		this.btnMain1ColorPicker.Location = new System.Drawing.Point(9, 271);
		this.btnMain1ColorPicker.Name = "btnMain1ColorPicker";
		this.btnMain1ColorPicker.Size = new System.Drawing.Size(75, 23);
		this.btnMain1ColorPicker.TabIndex = 10;
		this.btnMain1ColorPicker.UseVisualStyleBackColor = true;
		this.btnMain1ColorPicker.Click += new System.EventHandler(btnMain1ColorPicker_Click);
		this.chkIFD.AutoSize = true;
		this.chkIFD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkIFD.Font = new System.Drawing.Font("Segoe UI Light", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkIFD.Location = new System.Drawing.Point(9, 217);
		this.chkIFD.Name = "chkIFD";
		this.chkIFD.Size = new System.Drawing.Size(62, 34);
		this.chkIFD.TabIndex = 0;
		this.chkIFD.Text = "IFD";
		this.chkIFD.UseVisualStyleBackColor = true;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Segoe UI Light", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(9, 149);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(99, 30);
		this.label3.TabIndex = 5;
		this.label3.Text = "Password";
		this.txtPassword.BackColor = System.Drawing.SystemColors.ScrollBar;
		this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtPassword.Font = new System.Drawing.Font("Segoe UI Light", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPassword.Location = new System.Drawing.Point(9, 182);
		this.txtPassword.Name = "txtPassword";
		this.txtPassword.Size = new System.Drawing.Size(414, 29);
		this.txtPassword.TabIndex = 4;
		this.txtPassword.UseSystemPasswordChar = true;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Segoe UI Light", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(9, 84);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(187, 30);
		this.label2.TabIndex = 3;
		this.label2.Text = "Domain\\Username";
		this.txtUserName.BackColor = System.Drawing.SystemColors.ScrollBar;
		this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUserName.Font = new System.Drawing.Font("Segoe UI Light", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUserName.ForeColor = System.Drawing.SystemColors.WindowText;
		this.txtUserName.Location = new System.Drawing.Point(9, 117);
		this.txtUserName.Name = "txtUserName";
		this.txtUserName.Size = new System.Drawing.Size(414, 29);
		this.txtUserName.TabIndex = 2;
		this.txtUserName.Tag = "";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Segoe UI Light", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(9, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(206, 30);
		this.label1.TabIndex = 1;
		this.label1.Text = "Organization Service";
		this.cbOrganizationService.BackColor = System.Drawing.SystemColors.Window;
		this.cbOrganizationService.DropDownHeight = 126;
		this.cbOrganizationService.Font = new System.Drawing.Font("Segoe UI Light", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbOrganizationService.FormattingEnabled = true;
		this.cbOrganizationService.IntegralHeight = false;
		this.cbOrganizationService.ItemHeight = 21;
		this.cbOrganizationService.Location = new System.Drawing.Point(9, 52);
		this.cbOrganizationService.Name = "cbOrganizationService";
		this.cbOrganizationService.Size = new System.Drawing.Size(414, 29);
		this.cbOrganizationService.TabIndex = 0;
		this.cbOrganizationService.SelectionChangeCommitted += new System.EventHandler(cbOrganizationService_SelectionChangeCommitted);
		this.cbOrganizationService.Leave += new System.EventHandler(cbOrganizationService_SelectionChangeCommitted);
		this.paneltop.BackColor = System.Drawing.Color.FromArgb(35, 67, 114);
		this.paneltop.Controls.Add(this.pictureBox1);
		this.paneltop.Dock = System.Windows.Forms.DockStyle.Top;
		this.paneltop.Location = new System.Drawing.Point(0, 0);
		this.paneltop.Name = "paneltop";
		this.paneltop.Size = new System.Drawing.Size(842, 100);
		this.paneltop.TabIndex = 0;
		this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
		this.radMain1ColorPicker.Icon = (System.Drawing.Icon)resources.GetObject("radMain1ColorPicker.Icon");
		this.radMain1ColorPicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.radMain1ColorPicker.SelectedColor = System.Drawing.Color.Red;
		this.radMain1ColorPicker.SelectedHslColor = HslColor.FromAhsl(0.0, 1.0, 1.0);
		this.radMain2ColorPicker.Icon = (System.Drawing.Icon)resources.GetObject("radMain2ColorPicker.Icon");
		this.radMain2ColorPicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.radMain2ColorPicker.SelectedColor = System.Drawing.Color.Red;
		this.radMain2ColorPicker.SelectedHslColor = HslColor.FromAhsl(0.0, 1.0, 1.0);
		this.radMain3ColorPicker.Icon = (System.Drawing.Icon)resources.GetObject("radMain3ColorPicker.Icon");
		this.radMain3ColorPicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.radMain3ColorPicker.SelectedColor = System.Drawing.Color.Red;
		this.radMain3ColorPicker.SelectedHslColor = HslColor.FromAhsl(0.0, 1.0, 1.0);
		this.btnFontDialog.BackColor = System.Drawing.Color.Transparent;
		this.btnFontDialog.BackgroundImage = DesignDocument.UI.Properties.Resources.create_font;
		this.btnFontDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnFontDialog.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnFontDialog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnFontDialog.Font = new System.Drawing.Font("Lucida Calligraphy", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnFontDialog.ForeColor = System.Drawing.Color.White;
		this.btnFontDialog.Location = new System.Drawing.Point(223, 220);
		this.btnFontDialog.Name = "btnFontDialog";
		this.btnFontDialog.Size = new System.Drawing.Size(37, 45);
		this.btnFontDialog.TabIndex = 15;
		this.btnFontDialog.UseVisualStyleBackColor = false;
		this.btnFontDialog.Click += new System.EventHandler(btnFontDialog_Click);
		this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
		this.btnRefresh.BackgroundImage = DesignDocument.UI.Properties.Resources.refresh_zps95254db2;
		this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnRefresh.FlatAppearance.BorderSize = 0;
		this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRefresh.ForeColor = System.Drawing.Color.Transparent;
		this.btnRefresh.Location = new System.Drawing.Point(266, 224);
		this.btnRefresh.Name = "btnRefresh";
		this.btnRefresh.Size = new System.Drawing.Size(78, 70);
		this.btnRefresh.TabIndex = 8;
		this.btnRefresh.UseVisualStyleBackColor = false;
		this.btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
		this.btnGenerate.BackColor = System.Drawing.Color.Transparent;
		this.btnGenerate.BackgroundImage = DesignDocument.UI.Properties.Resources.Button_Download_icon;
		this.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnGenerate.FlatAppearance.BorderSize = 0;
		this.btnGenerate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnGenerate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGenerate.ForeColor = System.Drawing.Color.White;
		this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerate.Location = new System.Drawing.Point(350, 217);
		this.btnGenerate.Name = "btnGenerate";
		this.btnGenerate.Size = new System.Drawing.Size(73, 77);
		this.btnGenerate.TabIndex = 6;
		this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerate.UseVisualStyleBackColor = false;
		this.btnGenerate.Click += new System.EventHandler(btnGenerate_Click);
		this.btnGenerate.Enter += new System.EventHandler(btnGenerate_Click);
		this.pbLoader.Image = DesignDocument.UI.Properties.Resources.preloader_rocket;
		this.pbLoader.Location = new System.Drawing.Point(381, -2);
		this.pbLoader.Name = "pbLoader";
		this.pbLoader.Size = new System.Drawing.Size(26, 82);
		this.pbLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pbLoader.TabIndex = 1;
		this.pbLoader.TabStop = false;
		this.pbLoader.Visible = false;
		this.btnSettings.BackColor = System.Drawing.Color.Transparent;
		this.btnSettings.BackgroundImage = DesignDocument.UI.Properties.Resources.main_qimg_119f5bd2e1897dee7faa25db6ee6917f;
		this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnSettings.Dock = System.Windows.Forms.DockStyle.Right;
		this.btnSettings.FlatAppearance.BorderSize = 0;
		this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSettings.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.btnSettings.Location = new System.Drawing.Point(754, 0);
		this.btnSettings.Name = "btnSettings";
		this.btnSettings.Padding = new System.Windows.Forms.Padding(5);
		this.btnSettings.Size = new System.Drawing.Size(45, 63);
		this.btnSettings.TabIndex = 2;
		this.btnSettings.UseVisualStyleBackColor = false;
		this.btnSettings.Click += new System.EventHandler(btnSettings_Click);
		this.btnInfo.BackColor = System.Drawing.Color.Transparent;
		this.btnInfo.BackgroundImage = DesignDocument.UI.Properties.Resources.awesome_info;
		this.btnInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnInfo.Dock = System.Windows.Forms.DockStyle.Right;
		this.btnInfo.FlatAppearance.BorderSize = 0;
		this.btnInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnInfo.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnInfo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.btnInfo.Location = new System.Drawing.Point(799, 0);
		this.btnInfo.Name = "btnInfo";
		this.btnInfo.Padding = new System.Windows.Forms.Padding(5);
		this.btnInfo.Size = new System.Drawing.Size(43, 63);
		this.btnInfo.TabIndex = 0;
		this.btnInfo.UseVisualStyleBackColor = false;
		this.btnInfo.Click += new System.EventHandler(btnInfo_Click);
		this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(12, 7);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(504, 90);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Click += new System.EventHandler(pictureBox1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.HotTrack;
		base.ClientSize = new System.Drawing.Size(842, 464);
		base.Controls.Add(this.panelright);
		base.Controls.Add(this.panelleft);
		base.Controls.Add(this.panelbuttom);
		base.Controls.Add(this.paneltop);
		this.Font = new System.Drawing.Font("Segoe UI Light", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "Technical_Document_Generator";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Technical Document Generator";
		this.panelbuttom.ResumeLayout(false);
		this.panelbuttom.PerformLayout();
		this.panelleft.ResumeLayout(false);
		this.panelleft.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.rclEntities).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radCheckedDropDownList).EndInit();
		this.panelright.ResumeLayout(false);
		this.panelright.PerformLayout();
		this.paneltop.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pbLoader).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}