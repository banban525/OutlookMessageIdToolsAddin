namespace MessageIDToolsAddin
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// デザイナー変数が必要です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon1));
            this.tab1 = this.Factory.CreateRibbonTab();
            this._customCopyGroup = this.Factory.CreateRibbonGroup();
            this._copyButton1 = this.Factory.CreateRibbonButton();
            this._copyButton2 = this.Factory.CreateRibbonButton();
            this._copyButton3 = this.Factory.CreateRibbonButton();
            this._searchGroup = this.Factory.CreateRibbonGroup();
            this._messageIDEditBox = this.Factory.CreateRibbonEditBox();
            this._toolsGroup = this.Factory.CreateRibbonGroup();
            this._settingsButton = this.Factory.CreateRibbonButton();
            this._helpButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this._customCopyGroup.SuspendLayout();
            this._searchGroup.SuspendLayout();
            this._toolsGroup.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this._customCopyGroup);
            this.tab1.Groups.Add(this._searchGroup);
            this.tab1.Groups.Add(this._toolsGroup);
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Name = "tab1";
            // 
            // _customCopyGroup
            // 
            this._customCopyGroup.Items.Add(this._copyButton1);
            this._customCopyGroup.Items.Add(this._copyButton2);
            this._customCopyGroup.Items.Add(this._copyButton3);
            resources.ApplyResources(this._customCopyGroup, "_customCopyGroup");
            this._customCopyGroup.Name = "_customCopyGroup";
            // 
            // _copyButton1
            // 
            this._copyButton1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this._copyButton1.Image = global::MessageIDToolsAddin.Properties.Resources.Customcopy1;
            resources.ApplyResources(this._copyButton1, "_copyButton1");
            this._copyButton1.Name = "_copyButton1";
            this._copyButton1.ShowImage = true;
            this._copyButton1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this._copyButton1_Click);
            // 
            // _copyButton2
            // 
            this._copyButton2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this._copyButton2.Image = global::MessageIDToolsAddin.Properties.Resources.Customcopy2;
            resources.ApplyResources(this._copyButton2, "_copyButton2");
            this._copyButton2.Name = "_copyButton2";
            this._copyButton2.ShowImage = true;
            this._copyButton2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this._copyButton1_Click);
            // 
            // _copyButton3
            // 
            this._copyButton3.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this._copyButton3.Image = global::MessageIDToolsAddin.Properties.Resources.Customcopy3;
            resources.ApplyResources(this._copyButton3, "_copyButton3");
            this._copyButton3.Name = "_copyButton3";
            this._copyButton3.ShowImage = true;
            this._copyButton3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this._copyButton1_Click);
            // 
            // _searchGroup
            // 
            this._searchGroup.Items.Add(this._messageIDEditBox);
            resources.ApplyResources(this._searchGroup, "_searchGroup");
            this._searchGroup.Name = "_searchGroup";
            // 
            // _messageIDEditBox
            // 
            resources.ApplyResources(this._messageIDEditBox, "_messageIDEditBox");
            this._messageIDEditBox.MaxLength = 256;
            this._messageIDEditBox.Name = "_messageIDEditBox";
            this._messageIDEditBox.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this._messageIDEditBox_TextChanged);
            // 
            // _toolsGroup
            // 
            this._toolsGroup.Items.Add(this._settingsButton);
            this._toolsGroup.Items.Add(this._helpButton);
            resources.ApplyResources(this._toolsGroup, "_toolsGroup");
            this._toolsGroup.Name = "_toolsGroup";
            // 
            // _settingsButton
            // 
            this._settingsButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this._settingsButton.Image = global::MessageIDToolsAddin.Properties.Resources.Settings;
            resources.ApplyResources(this._settingsButton, "_settingsButton");
            this._settingsButton.Name = "_settingsButton";
            this._settingsButton.ShowImage = true;
            this._settingsButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this._settingsButton_Click);
            // 
            // _helpButton
            // 
            this._helpButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this._helpButton.Image = global::MessageIDToolsAddin.Properties.Resources.Help;
            resources.ApplyResources(this._helpButton, "_helpButton");
            this._helpButton.Name = "_helpButton";
            this._helpButton.ShowImage = true;
            this._helpButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this._helpButton_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this._customCopyGroup.ResumeLayout(false);
            this._customCopyGroup.PerformLayout();
            this._searchGroup.ResumeLayout(false);
            this._searchGroup.PerformLayout();
            this._toolsGroup.ResumeLayout(false);
            this._toolsGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup _customCopyGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton _copyButton1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton _copyButton2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton _copyButton3;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup _searchGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox _messageIDEditBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup _toolsGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton _settingsButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton _helpButton;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
