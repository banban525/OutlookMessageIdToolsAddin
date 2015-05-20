using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace MessageIDToolsAddin
{
    public partial class ThisAddIn
    {
        private MessageIDToolsController _controller = new MessageIDToolsController();

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            Ribbon1.Controller = _controller;
            var ribbon1 = new Ribbon1();

            return Globals.Factory.GetRibbonFactory().CreateRibbonManager(
               new Microsoft.Office.Tools.Ribbon.IRibbonExtension[] { 
                  ribbon1});
        }
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _controller.Initialize();
            _controller.SetApplication(this.Application);

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            _controller.Dispose();


        }

        #region VSTO で生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
