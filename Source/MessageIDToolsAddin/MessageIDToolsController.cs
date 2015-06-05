using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Win32;

namespace MessageIDToolsAddin
{
    public class MessageIDToolsController : IDisposable
    {
        private InterceptKeys _interceptKeys = new InterceptKeys();
        private Microsoft.Office.Interop.Outlook.Application _application;
        public static readonly string DefaultTemplate = @"MessageID: $MessageID$
Received:  $RecievedDateTime$
Sender:    $Sender$ ($SenderAddress$)
Subject:   $Subject$

$SelectedText$
";
        private Templates _templates = Templates.Empty;

        private readonly string _settingsFilePath;

        public MessageIDToolsController()
        {
            _interceptKeys.KeyIntercepted += _interceptKeys_KeyIntercepted;

            string installFolder = null;
            using (var registryKey = Registry.CurrentUser.OpenSubKey(@"Software\banban525\MessageIDTools"))
            {
                if (registryKey != null)
                {
                    installFolder = registryKey.GetValue("InstallFolder").ToString();
                }
            }
            if (installFolder != null)
            {
                _settingsFilePath = Path.Combine(installFolder, "Settings.xml");
            }



            if (_settingsFilePath == null || File.Exists(_settingsFilePath) == false)
            {
                SetDefaultTemplates();
            }
            else
            {
                try
                {
                    var serializer = new TemplatesSerializer();
                    using (var stream = new FileStream(_settingsFilePath, FileMode.Open))
                    {
                        _templates = serializer.Deserialize(stream);
                    }
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    SetDefaultTemplates();
                }
            }
        }

        private void SetDefaultTemplates()
        {
            _templates = new Templates(new[]
            {
                new TemplateData(DefaultTemplate, false, HotKeyData.Empty),
                new TemplateData(DefaultTemplate, false, HotKeyData.Empty),
                new TemplateData(DefaultTemplate, false, HotKeyData.Empty),
            });
            Save();
        }

        private void Save()
        {
            if (_settingsFilePath == null)
            {
                return;
            }
            var serializer = new TemplatesSerializer();
            using (var stream = new FileStream(_settingsFilePath, FileMode.Create))
            {
                serializer.Serialize(stream, _templates);
            }
        }

        private void _interceptKeys_KeyIntercepted(object sender, InterceptKeysEventArgs e)
        {
            Trace.WriteLine(e.KeyData);
            foreach (var template in _templates)
            {
                if (template.EnableHotKey)
                {
                    if (template.HotKey.IsShiftKey == e.IsShiftKeyDown &&
                        template.HotKey.IsCtrlKey == e.IsCtrlKeyDown)
                    {
                        if (template.HotKey.Key == e.KeyData)
                        {
                            ExecuteCopy(_templates.IndexOf(template));
                        }
                    }
                }
            }
        }

        public void SetApplication(Microsoft.Office.Interop.Outlook.Application application)
        {
            _application = application;
        }

        public void Initialize()
        {
            _interceptKeys.SetHook();
        }


        public void ExecuteCopy(int index)
        {
            var explorerWindow = _application.ActiveWindow() as Explorer;
            var readerWindow = _application.ActiveWindow() as Microsoft.Office.Interop.Outlook.Inspector;

            MailItem mailItem = null;
            if (explorerWindow != null)
            {
                if (explorerWindow.Selection.Count > 0)
                {
                    var selections = explorerWindow.Selection.GetEnumerator();
                    selections.MoveNext();
                    mailItem = selections.Current as MailItem;
                }
            }
            if (readerWindow != null)
            {
                mailItem = readerWindow.CurrentItem as MailItem;
            }


            var messageID = "";
            DateTime received = DateTime.MinValue;
            string subject = "";
            string sender = "";
            string senderName = "";
            string body = "";

            if (mailItem != null)
            {
                var messageIdObj =
                    mailItem.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x1035001E");
                if (messageIdObj != null)
                {
                    messageID = messageIdObj.ToString();
                }
                var address = mailItem.Sender.Address;
                sender = mailItem.SenderEmailAddress;
                received = mailItem.ReceivedTime;
                subject = mailItem.Subject;
                senderName = mailItem.Sender.Name;
                body = mailItem.Body;
                var entryID = mailItem.EntryID;
            }


            Clipboard.Clear();

            string content = "";
            SendKeys.SendWait("^c");
            var dataObj = Clipboard.GetDataObject();
            if (dataObj.GetDataPresent(typeof (string)))
            {
                content = dataObj.GetData(typeof (string)).ToString();
            }

            var contents = _templates[index].TemplateText;
            contents = contents.Replace("$MessageID$", messageID);
            contents = contents.Replace("$RecievedDateTime$", received.ToString());
            contents = contents.Replace("$Subject$", subject);
            contents = contents.Replace("$SenderAddress$", sender);
            contents = contents.Replace("$Sender$", senderName);
            contents = contents.Replace("$SelectedText$", content);
            contents = contents.Replace("$Body$", body);

            Clipboard.SetData(DataFormats.UnicodeText, contents);

        }


        public void ExecuteMessageIDSearch(string messageId)
        {
            var list = new List<MailItem>();
            string query = string.Format("@SQL=\"http://schemas.microsoft.com/mapi/proptag/0x1035001E\" = '{0}'",
                messageId);
            list.AddRange(FindMail(_application.Session.Folders, query));

            if (list.Any())
            {
                var mail = list[0];
                var recieved = mail.ReceivedTime.ToShortDateString() + " " + mail.ReceivedTime.ToShortTimeString();
                var folder = (Folder) mail.Parent;

                var enumerator2 = _application.Explorers.GetEnumerator();
                enumerator2.MoveNext();
                ((Explorer) enumerator2.Current).CurrentFolder = folder;
                ((Explorer) enumerator2.Current).Search(
                    //Properties.Resources.SizeKeyword + ":" + ((mail.Size + 512) / 1000) + "KB " +
                    Properties.Resources.RecievedKeyword + ":" + recieved + " " + 
                    Properties.Resources.SenderKeyword + ":(" + mail.Sender.Name + ")",
                    OlSearchScope.olSearchScopeCurrentFolder);

            }
            OnClearMessageIDSearchEditBox();
        }

        public IEnumerable<MailItem> FindMail(Folders folders, string query)
        {

            var list = new List<MailItem>();

            foreach (Folder folder in folders)
            {
                MailItem foundMail = (MailItem) folder.Items.Find(query);
                if (foundMail != null)
                {
                    list.Add(foundMail);
                }
                while ((foundMail = (MailItem) folder.Items.FindNext()) != null)
                {
                    list.Add(foundMail);
                }
                list.AddRange(FindMail(folder.Folders, query));
            }
            return list;
        }


        private void OnClearMessageIDSearchEditBox()
        {
            if (ClearMessageIDSearchEditBox == null)
            {
                return;
            }
            ClearMessageIDSearchEditBox(this, EventArgs.Empty);

        }

        public event EventHandler ClearMessageIDSearchEditBox;

        public void ExecuteShowSettings()
        {
            var dlg = new SettingsForm()
            {
                Content = _templates
            };

            var dialogResult = dlg.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _templates = dlg.Content;
                Save();
            }
        }

        public void Dispose()
        {
            _interceptKeys.Dispose();
        }

        public void ExecuteShowHelp()
        {

            Help.ShowHelp(null, GetHelpFilePath(), HelpNavigator.Topic, "/ToolBar.html");
        }

        public static string GetHelpFilePath()
        {
            var pluginDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (var subkey = Registry.CurrentUser.OpenSubKey(@"Software\banban525\MessageIDTools"))
            {
                if (subkey == null)
                {
                    return string.Empty;
                }
                var registryValue = subkey.GetValue("BinFolder", "").ToString();
                if (string.IsNullOrEmpty(registryValue) == false)
                {
                    pluginDir = registryValue;
                }
            }

            if (pluginDir == null)
            {
                return string.Empty;
            }
            var helpFilePath1 = Path.Combine(pluginDir, Properties.Resources.HelpFileName);
            if (File.Exists(helpFilePath1) == false)
            {
                return string.Empty;
            }
            return helpFilePath1;
        }
    }
}
