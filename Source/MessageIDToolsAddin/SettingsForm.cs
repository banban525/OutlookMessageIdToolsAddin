using System;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MessageIDToolsAddin
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        internal Templates Content { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            _templateEditor1.TemplateData = Content[0];
            _templateEditor2.TemplateData = Content[1];
            _templateEditor3.TemplateData = Content[2];
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Content = new Templates(new[]{
                _templateEditor1.TemplateData,
                _templateEditor2.TemplateData,
                _templateEditor3.TemplateData,
                });
        }

    }
}
