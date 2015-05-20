using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;

namespace MessageIDToolsAddin
{
    public partial class TemplateEditor : UserControl
    {
        public TemplateEditor()
        {
            InitializeComponent();
        }

        TemplateData _templateData = TemplateData.Empty;

        internal TemplateData TemplateData
        {
            get { return _templateData; }
            set
            {
                _templateData = value;
                UpdateControl(this, EventArgs.Empty);
            }
        }



        private void insertMacroButton_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(_insertMacroButton, new Point(0, _insertMacroButton.Height));
        }

        private readonly Regex _contextMenuRegex = new Regex(@"\((\$[^\$]+\$)\)", RegexOptions.Compiled);

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;

            if (_contextMenuRegex.IsMatch(menuItem.Text) == false)
            {
                throw new InvalidOperationException();
            }

            var macro = _contextMenuRegex.Match(menuItem.Text).Groups[1].Value;
            InsertMacro(macro);
            UpdateControl(sender, e);
        }

        private void InsertMacro(string macro)
        {
            var start = _selectionStart;
            var length = _selectionLength;
            var preSelection = _templateData.TemplateText.Substring(0, start);
            var postSelection = _templateData.TemplateText.Substring(start + length);
            _templateData = new TemplateData(
                preSelection + macro + postSelection,
                _templateData.EnableHotKey,
                _templateData.HotKey);
            _selectionStart = start + macro.Length;
            _selectionLength = 0;
        }

        private void _hotKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            bool isShiftKey = false; //(e.KeyData & Keys.Shift) != Keys.None;
            bool isCtrlKey = (e.KeyData & Keys.Control) != Keys.None;
            bool isAltKey = false; //(e.KeyData & Keys.Alt) != Keys.None;

            var hotKeyData = HotKeyData.Empty;
            for (var i = Keys.Space; i <= Keys.OemClear; i++)
            {
                if (e.KeyCode == i)
                {
                    hotKeyData = new HotKeyData(isShiftKey, isCtrlKey, isAltKey, e.KeyCode);
                }
            }

            _templateData = new TemplateData(
                _templateData.TemplateText,
                _templateData.EnableHotKey,
                hotKeyData);
            UpdateControl(sender, e);
            e.Handled = true;
        }

        private void _hotKeyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private int _selectionStart;
        private int _selectionLength;
        private bool _isUpdating;
        private void UpdateControl(object sender, EventArgs e)
        {
            if (_isUpdating)
            {
                return;
            }
            _isUpdating = true;
            _hotKeyCheckBox.Checked = _templateData.EnableHotKey;
            _hotKeyTextBox.Text = _templateData.HotKey.ToString();
            _templateTextBox.Text = _templateData.TemplateText;
            _templateTextBox.SelectionStart = _selectionStart;
            _templateTextBox.SelectionLength = _selectionLength;
            _isUpdating = false;
        }

        private void UpdateStatus(object sender, EventArgs e)
        {
            if (_isUpdating)
            {
                return;
            }
            _templateData = new TemplateData(
                _templateTextBox.Text,
                _hotKeyCheckBox.Checked,
                _templateData.HotKey);
            _selectionStart = _templateTextBox.SelectionStart;
            _selectionLength = _templateTextBox.SelectionLength;
            UpdateControl(sender, e);
        }

    }
}
