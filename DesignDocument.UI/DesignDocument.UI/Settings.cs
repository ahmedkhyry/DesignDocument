using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DesignDocument.BLL;
using DesignDocument.DAL;
using DesignDocument.UI.Properties;

namespace DesignDocument.UI;

public class Settings : Form
{
	private IContainer components = null;

	private CheckBox chkForms;

	private Button btnSettingsSave;

	private CheckBox chkWorkflows;

	private CheckBox chkPlugins;

	private CheckBox chkSecurityProfiles;

	private CheckBox chkScripts;

	private CheckBox chkLocalOptionSets;

	private CheckBox chkGlobalOptionSets;

	private CheckBox chkVisibleSectionsTabs;

	private Label lblTitle;

	private CheckBox chkSecurityRoles;

	public Settings()
	{
		InitializeComponent();
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
		else
		{
			DataManager.WriteToJsonFile("DocumentSettings.JSON", documentSettings);
		}
		chkForms.Checked = documentSettings.EntityForms;
		chkGlobalOptionSets.Checked = documentSettings.EntityGlobalOptionSets;
		chkLocalOptionSets.Checked = documentSettings.EntityLocalOptionSets;
		chkWorkflows.Checked = documentSettings.Workflows;
		chkPlugins.Checked = documentSettings.Plugins;
		chkSecurityProfiles.Checked = documentSettings.SecurityProfiles;
		chkScripts.Checked = documentSettings.EntityScripts;
		chkSecurityRoles.Checked = documentSettings.SecurityRoles;
		chkVisibleSectionsTabs.Checked = documentSettings.VisibleSectionsTabs;
	}

	private void btnSettingsSave_Click(object sender, EventArgs e)
	{
		DocumentSettings objectToWrite = new DocumentSettings
		{
			EntityForms = chkForms.Checked,
			EntityGlobalOptionSets = chkGlobalOptionSets.Checked,
			EntityLocalOptionSets = chkLocalOptionSets.Checked,
			Workflows = chkWorkflows.Checked,
			Plugins = chkPlugins.Checked,
			SecurityProfiles = chkSecurityProfiles.Checked,
			EntityScripts = chkScripts.Checked,
			SecurityRoles = chkSecurityRoles.Checked,
			VisibleSectionsTabs = chkVisibleSectionsTabs.Checked
		};
		DataManager.WriteToJsonFile("DocumentSettings.JSON", objectToWrite);
		base.Owner.Enabled = true;
		((Technical_Document_Generator)base.Owner).UpdateDocumentSettingsChangesLabel();
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignDocument.UI.Settings));
		this.chkForms = new System.Windows.Forms.CheckBox();
		this.chkWorkflows = new System.Windows.Forms.CheckBox();
		this.chkPlugins = new System.Windows.Forms.CheckBox();
		this.chkSecurityProfiles = new System.Windows.Forms.CheckBox();
		this.chkScripts = new System.Windows.Forms.CheckBox();
		this.chkLocalOptionSets = new System.Windows.Forms.CheckBox();
		this.chkGlobalOptionSets = new System.Windows.Forms.CheckBox();
		this.btnSettingsSave = new System.Windows.Forms.Button();
		this.chkVisibleSectionsTabs = new System.Windows.Forms.CheckBox();
		this.lblTitle = new System.Windows.Forms.Label();
		this.chkSecurityRoles = new System.Windows.Forms.CheckBox();
		base.SuspendLayout();
		this.chkForms.AutoSize = true;
		this.chkForms.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkForms.Location = new System.Drawing.Point(17, 118);
		this.chkForms.Name = "chkForms";
		this.chkForms.Size = new System.Drawing.Size(137, 34);
		this.chkForms.TabIndex = 0;
		this.chkForms.Text = "Form Fields";
		this.chkForms.UseVisualStyleBackColor = true;
		this.chkWorkflows.AutoSize = true;
		this.chkWorkflows.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkWorkflows.Location = new System.Drawing.Point(17, 318);
		this.chkWorkflows.Name = "chkWorkflows";
		this.chkWorkflows.Size = new System.Drawing.Size(127, 34);
		this.chkWorkflows.TabIndex = 2;
		this.chkWorkflows.Text = "Workflows";
		this.chkWorkflows.UseVisualStyleBackColor = true;
		this.chkPlugins.AutoSize = true;
		this.chkPlugins.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkPlugins.Location = new System.Drawing.Point(17, 278);
		this.chkPlugins.Name = "chkPlugins";
		this.chkPlugins.Size = new System.Drawing.Size(99, 34);
		this.chkPlugins.TabIndex = 3;
		this.chkPlugins.Text = "Plugins";
		this.chkPlugins.UseVisualStyleBackColor = true;
		this.chkSecurityProfiles.AutoSize = true;
		this.chkSecurityProfiles.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkSecurityProfiles.Location = new System.Drawing.Point(17, 78);
		this.chkSecurityProfiles.Name = "chkSecurityProfiles";
		this.chkSecurityProfiles.Size = new System.Drawing.Size(177, 34);
		this.chkSecurityProfiles.TabIndex = 4;
		this.chkSecurityProfiles.Text = "Security Profiles";
		this.chkSecurityProfiles.UseVisualStyleBackColor = true;
		this.chkScripts.AutoSize = true;
		this.chkScripts.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkScripts.Location = new System.Drawing.Point(17, 238);
		this.chkScripts.Name = "chkScripts";
		this.chkScripts.Size = new System.Drawing.Size(146, 34);
		this.chkScripts.TabIndex = 5;
		this.chkScripts.Text = "Form Scripts";
		this.chkScripts.UseVisualStyleBackColor = true;
		this.chkLocalOptionSets.AutoSize = true;
		this.chkLocalOptionSets.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkLocalOptionSets.Location = new System.Drawing.Point(17, 198);
		this.chkLocalOptionSets.Name = "chkLocalOptionSets";
		this.chkLocalOptionSets.Size = new System.Drawing.Size(195, 34);
		this.chkLocalOptionSets.TabIndex = 6;
		this.chkLocalOptionSets.Text = "Local Option Sets";
		this.chkLocalOptionSets.UseVisualStyleBackColor = true;
		this.chkGlobalOptionSets.AutoSize = true;
		this.chkGlobalOptionSets.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkGlobalOptionSets.Location = new System.Drawing.Point(17, 158);
		this.chkGlobalOptionSets.Name = "chkGlobalOptionSets";
		this.chkGlobalOptionSets.Size = new System.Drawing.Size(207, 34);
		this.chkGlobalOptionSets.TabIndex = 7;
		this.chkGlobalOptionSets.Text = "Global Option Sets";
		this.chkGlobalOptionSets.UseVisualStyleBackColor = true;
		this.btnSettingsSave.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.btnSettingsSave.Image = DesignDocument.UI.Properties.Resources.save_btn;
		this.btnSettingsSave.Location = new System.Drawing.Point(17, 484);
		this.btnSettingsSave.Name = "btnSettingsSave";
		this.btnSettingsSave.Size = new System.Drawing.Size(275, 71);
		this.btnSettingsSave.TabIndex = 1;
		this.btnSettingsSave.UseVisualStyleBackColor = true;
		this.btnSettingsSave.Click += new System.EventHandler(btnSettingsSave_Click);
		this.chkVisibleSectionsTabs.AutoSize = true;
		this.chkVisibleSectionsTabs.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkVisibleSectionsTabs.Location = new System.Drawing.Point(17, 398);
		this.chkVisibleSectionsTabs.Name = "chkVisibleSectionsTabs";
		this.chkVisibleSectionsTabs.Size = new System.Drawing.Size(275, 34);
		this.chkVisibleSectionsTabs.TabIndex = 8;
		this.chkVisibleSectionsTabs.Text = "Invisible Sections and Tabs";
		this.chkVisibleSectionsTabs.UseVisualStyleBackColor = true;
		this.lblTitle.AutoSize = true;
		this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 24f, System.Drawing.FontStyle.Underline);
		this.lblTitle.Location = new System.Drawing.Point(9, 20);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new System.Drawing.Size(307, 45);
		this.lblTitle.TabIndex = 9;
		this.lblTitle.Text = "Show/Hide Settings";
		this.chkSecurityRoles.AutoSize = true;
		this.chkSecurityRoles.Font = new System.Drawing.Font("Segoe UI Light", 16f);
		this.chkSecurityRoles.Location = new System.Drawing.Point(17, 358);
		this.chkSecurityRoles.Name = "chkSecurityRoles";
		this.chkSecurityRoles.Size = new System.Drawing.Size(160, 34);
		this.chkSecurityRoles.TabIndex = 10;
		this.chkSecurityRoles.Text = "Security Roles";
		this.chkSecurityRoles.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		base.ClientSize = new System.Drawing.Size(323, 567);
		base.ControlBox = false;
		base.Controls.Add(this.chkSecurityRoles);
		base.Controls.Add(this.lblTitle);
		base.Controls.Add(this.chkVisibleSectionsTabs);
		base.Controls.Add(this.chkGlobalOptionSets);
		base.Controls.Add(this.chkLocalOptionSets);
		base.Controls.Add(this.chkScripts);
		base.Controls.Add(this.chkSecurityProfiles);
		base.Controls.Add(this.chkPlugins);
		base.Controls.Add(this.chkWorkflows);
		base.Controls.Add(this.btnSettingsSave);
		base.Controls.Add(this.chkForms);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "Settings";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Settings";
		base.TopMost = true;
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
