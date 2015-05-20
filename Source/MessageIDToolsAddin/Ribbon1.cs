using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;

namespace MessageIDToolsAddin
{
    public partial class Ribbon1
    {
        public static MessageIDToolsController Controller;

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            Controller.ClearMessageIDSearchEditBox += _controller_ClearMessageIDSearchEditBox;

            if (RibbonId == "Microsoft.Outlook.Mail.Read")
            {
                _toolsGroup.Visible = false;
                _searchGroup.Visible = false;
                _helpButton.Visible = false;
            }

        }

        private void _controller_ClearMessageIDSearchEditBox(object sender, EventArgs e)
        {
            _messageIDEditBox.Text = "";
        }


        private void _settingsButton_Click(object sender, RibbonControlEventArgs e)
        {
            Controller.ExecuteShowSettings();
        }

        private void _copyButton1_Click(object sender, RibbonControlEventArgs e)
        {
            int index = -1;
            if (e.Control.Id == _copyButton1.Id)
            {
                index = 0;
            }
            if (e.Control.Id == _copyButton2.Id)
            {
                index = 1;
            }
            if (e.Control.Id == _copyButton3.Id)
            {
                index = 2;
            }
            Controller.ExecuteCopy(index);
        }

        private void _messageIDEditBox_TextChanged(object sender, RibbonControlEventArgs e)
        {
            Controller.ExecuteMessageIDSearch(_messageIDEditBox.Text);
        }

        private void _helpButton_Click(object sender, RibbonControlEventArgs e)
        {
            Controller.ExecuteShowHelp();
        }
    }
}
