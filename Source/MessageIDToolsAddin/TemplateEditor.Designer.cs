namespace MessageIDToolsAddin
{
    partial class TemplateEditor
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this._hotKeyTextBox = new System.Windows.Forms.TextBox();
            this._hotKeyCheckBox = new System.Windows.Forms.CheckBox();
            this._insertMacroButton = new System.Windows.Forms.Button();
            this._templateTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.messageIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.senderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.senderAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recievedDateTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._hotKeyTextBox);
            this.panel1.Controls.Add(this._hotKeyCheckBox);
            this.panel1.Controls.Add(this._insertMacroButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 230);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 31);
            this.panel1.TabIndex = 0;
            // 
            // _hotKeyTextBox
            // 
            this._hotKeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._hotKeyTextBox.Location = new System.Drawing.Point(408, 7);
            this._hotKeyTextBox.Name = "_hotKeyTextBox";
            this._hotKeyTextBox.Size = new System.Drawing.Size(135, 19);
            this._hotKeyTextBox.TabIndex = 2;
            this._hotKeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this._hotKeyTextBox_KeyDown);
            this._hotKeyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._hotKeyTextBox_KeyPress);
            // 
            // _hotKeyCheckBox
            // 
            this._hotKeyCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._hotKeyCheckBox.AutoSize = true;
            this._hotKeyCheckBox.Location = new System.Drawing.Point(337, 9);
            this._hotKeyCheckBox.Name = "_hotKeyCheckBox";
            this._hotKeyCheckBox.Size = new System.Drawing.Size(65, 16);
            this._hotKeyCheckBox.TabIndex = 1;
            this._hotKeyCheckBox.Text = "&Hot Key";
            this._hotKeyCheckBox.UseVisualStyleBackColor = true;
            this._hotKeyCheckBox.CheckedChanged += new System.EventHandler(this.UpdateStatus);
            // 
            // _insertMacroButton
            // 
            this._insertMacroButton.Location = new System.Drawing.Point(3, 5);
            this._insertMacroButton.Name = "_insertMacroButton";
            this._insertMacroButton.Size = new System.Drawing.Size(95, 23);
            this._insertMacroButton.TabIndex = 0;
            this._insertMacroButton.Text = "&Insert Macro";
            this._insertMacroButton.UseVisualStyleBackColor = true;
            this._insertMacroButton.Click += new System.EventHandler(this.insertMacroButton_Click);
            // 
            // _templateTextBox
            // 
            this._templateTextBox.AcceptsReturn = true;
            this._templateTextBox.AcceptsTab = true;
            this._templateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._templateTextBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._templateTextBox.HideSelection = false;
            this._templateTextBox.Location = new System.Drawing.Point(0, 0);
            this._templateTextBox.Multiline = true;
            this._templateTextBox.Name = "_templateTextBox";
            this._templateTextBox.Size = new System.Drawing.Size(543, 230);
            this._templateTextBox.TabIndex = 1;
            this._templateTextBox.Leave += new System.EventHandler(this.UpdateStatus);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messageIDToolStripMenuItem,
            this.subjectToolStripMenuItem,
            this.bodyToolStripMenuItem,
            this.selectedTextToolStripMenuItem,
            this.senderToolStripMenuItem,
            this.senderAddressToolStripMenuItem,
            this.recievedDateTimeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(330, 158);
            // 
            // messageIDToolStripMenuItem
            // 
            this.messageIDToolStripMenuItem.Name = "messageIDToolStripMenuItem";
            this.messageIDToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.messageIDToolStripMenuItem.Text = "&Message ID ($MessageID$)";
            this.messageIDToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // subjectToolStripMenuItem
            // 
            this.subjectToolStripMenuItem.Name = "subjectToolStripMenuItem";
            this.subjectToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.subjectToolStripMenuItem.Text = "&Subject ($Subject$)";
            this.subjectToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // bodyToolStripMenuItem
            // 
            this.bodyToolStripMenuItem.Name = "bodyToolStripMenuItem";
            this.bodyToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.bodyToolStripMenuItem.Text = "&Body ($Body$)";
            this.bodyToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // selectedTextToolStripMenuItem
            // 
            this.selectedTextToolStripMenuItem.Name = "selectedTextToolStripMenuItem";
            this.selectedTextToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.selectedTextToolStripMenuItem.Text = "Selected &Text ($SelectedText$)";
            this.selectedTextToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // senderToolStripMenuItem
            // 
            this.senderToolStripMenuItem.Name = "senderToolStripMenuItem";
            this.senderToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.senderToolStripMenuItem.Text = "S&ender ($Sender$)";
            this.senderToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // senderAddressToolStripMenuItem
            // 
            this.senderAddressToolStripMenuItem.Name = "senderAddressToolStripMenuItem";
            this.senderAddressToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.senderAddressToolStripMenuItem.Text = "Sender &Address ($SenderAddress$)";
            this.senderAddressToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // recievedDateTimeToolStripMenuItem
            // 
            this.recievedDateTimeToolStripMenuItem.Name = "recievedDateTimeToolStripMenuItem";
            this.recievedDateTimeToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.recievedDateTimeToolStripMenuItem.Text = "&Recieved Date Time ($RecievedDateTime$)";
            this.recievedDateTimeToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // TemplateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._templateTextBox);
            this.Controls.Add(this.panel1);
            this.Name = "TemplateEditor";
            this.Size = new System.Drawing.Size(543, 261);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _insertMacroButton;
        private System.Windows.Forms.TextBox _templateTextBox;
        private System.Windows.Forms.CheckBox _hotKeyCheckBox;
        private System.Windows.Forms.TextBox _hotKeyTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem messageIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bodyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem senderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem senderAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recievedDateTimeToolStripMenuItem;
    }
}
