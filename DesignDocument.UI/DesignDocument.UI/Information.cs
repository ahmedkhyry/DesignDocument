using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DesignDocument.UI.Properties;

namespace DesignDocument.UI;

public class Information : Form
{
	private IContainer components = null;

	private PictureBox pictureBox1;

	private LinkLabel lnkHomePage;

	private LinkLabel llEmail;

	private PictureBox picFacebook;

	private Button btnVisitUs;

	public Information()
	{
		InitializeComponent();
	}

	private void lnkHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start("http://XRM.WORLD");
	}

	private void llEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start("mailto:ramy.victor@gmail.com");
	}

	private void picFacebook_Click(object sender, EventArgs e)
	{
		Process.Start("https://www.facebook.com/xrm.world/?ref=application");
	}

	private void btnVisitUs_Click(object sender, EventArgs e)
	{
		Process.Start("http://xrm.world/");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignDocument.UI.Information));
		this.lnkHomePage = new System.Windows.Forms.LinkLabel();
		this.llEmail = new System.Windows.Forms.LinkLabel();
		this.btnVisitUs = new System.Windows.Forms.Button();
		this.picFacebook = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.picFacebook).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.lnkHomePage.AutoSize = true;
		this.lnkHomePage.LinkColor = System.Drawing.Color.White;
		this.lnkHomePage.Location = new System.Drawing.Point(12, 32);
		this.lnkHomePage.Name = "lnkHomePage";
		this.lnkHomePage.Size = new System.Drawing.Size(126, 13);
		this.lnkHomePage.TabIndex = 12;
		this.lnkHomePage.TabStop = true;
		this.lnkHomePage.Text = "Visit us on XRM.WORLD";
		this.lnkHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkHomePage_LinkClicked);
		this.llEmail.AutoSize = true;
		this.llEmail.LinkColor = System.Drawing.Color.DeepSkyBlue;
		this.llEmail.Location = new System.Drawing.Point(124, 233);
		this.llEmail.Name = "llEmail";
		this.llEmail.Size = new System.Drawing.Size(116, 13);
		this.llEmail.TabIndex = 13;
		this.llEmail.TabStop = true;
		this.llEmail.Text = "ramy.victor@gmail.com";
		this.llEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(llEmail_LinkClicked);
		this.btnVisitUs.BackColor = System.Drawing.Color.White;
		this.btnVisitUs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnVisitUs.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnVisitUs.Image = DesignDocument.UI.Properties.Resources.tdg_logo_web;
		this.btnVisitUs.Location = new System.Drawing.Point(12, 32);
		this.btnVisitUs.Name = "btnVisitUs";
		this.btnVisitUs.Size = new System.Drawing.Size(517, 56);
		this.btnVisitUs.TabIndex = 15;
		this.btnVisitUs.UseVisualStyleBackColor = false;
		this.btnVisitUs.Click += new System.EventHandler(btnVisitUs_Click);
		this.picFacebook.BackColor = System.Drawing.Color.Transparent;
		this.picFacebook.BackgroundImage = DesignDocument.UI.Properties.Resources.like_us_on_facebook_new;
		this.picFacebook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.picFacebook.Cursor = System.Windows.Forms.Cursors.Hand;
		this.picFacebook.Location = new System.Drawing.Point(107, 101);
		this.picFacebook.Name = "picFacebook";
		this.picFacebook.Size = new System.Drawing.Size(149, 72);
		this.picFacebook.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picFacebook.TabIndex = 14;
		this.picFacebook.TabStop = false;
		this.picFacebook.Click += new System.EventHandler(picFacebook_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(544, 396);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 11;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.DimGray;
		base.ClientSize = new System.Drawing.Size(544, 396);
		base.Controls.Add(this.btnVisitUs);
		base.Controls.Add(this.picFacebook);
		base.Controls.Add(this.llEmail);
		base.Controls.Add(this.lnkHomePage);
		base.Controls.Add(this.pictureBox1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Information";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Information";
		((System.ComponentModel.ISupportInitialize)this.picFacebook).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
