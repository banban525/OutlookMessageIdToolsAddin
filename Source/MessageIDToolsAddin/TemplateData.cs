using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MessageIDToolsAddin
{
    [DataContract]
    class Templates : IEnumerable<TemplateData>
    {
        [DataMember]
        private readonly TemplateData[] _list;

        public Templates(IEnumerable<TemplateData> initialDatas)
        {
            _list = initialDatas.ToArray();
            if (_list.Length != 3)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public TemplateData this[int index]
        {
            get
            {
                if (index < 0 || 3 <= index)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _list[index];
            }
        }

        public static readonly Templates Empty = new Templates(new[] { TemplateData.Empty, TemplateData.Empty, TemplateData.Empty });
        public IEnumerator<TemplateData> GetEnumerator()
        {
            return _list.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(TemplateData template)
        {
            return _list.ToList().IndexOf(template);
        }
    }


    [DataContract]
    class TemplateData
    {
        [DataMember]
        private readonly string _templateText;
        [DataMember]
        private readonly bool _enableHotKey;
        [DataMember]
        private readonly HotKeyData _hotKey;

        public TemplateData(string templateText, bool enableHotKey, HotKeyData hotKey)
        {
            _templateText = templateText;
            _enableHotKey = enableHotKey;
            _hotKey = hotKey;
        }

        public string TemplateText
        {
            get { return _templateText; }
        }

        public bool EnableHotKey
        {
            get { return _enableHotKey; }
        }

        public HotKeyData HotKey
        {
            get { return _hotKey; }
        }

        public static readonly TemplateData Empty = new TemplateData("",false, HotKeyData.Empty);
    }

    [DataContract]
    class HotKeyData
    {
        [DataMember]
        private readonly bool _isShiftKey;
        [DataMember]
        private readonly bool _isCtrlKey;
        [DataMember]
        private readonly bool _isAltKey;
        [DataMember]
        private readonly Keys _key;

        public HotKeyData(bool isShiftKey, bool isCtrlKey, bool isAltKey, Keys key)
        {
            _isShiftKey = isShiftKey;
            _isCtrlKey = isCtrlKey;
            _isAltKey = isAltKey;
            _key = key;
        }

        public bool IsShiftKey
        {
            get { return _isShiftKey; }
        }

        public bool IsCtrlKey
        {
            get { return _isCtrlKey; }
        }

        public bool IsAltKey
        {
            get { return _isAltKey; }
        }

        public Keys Key
        {
            get { return _key; }
        }

        public override string ToString()
        {
            if (Key == Keys.None)
            {
                return "";
            }
            string result = "";
            if (IsShiftKey)
            {
                if (string.IsNullOrEmpty(result) == false)
                {
                    result += " + ";
                }
                result += "Shift";
            }
            if (IsCtrlKey)
            {
                if (string.IsNullOrEmpty(result) == false)
                {
                    result += " + ";
                }
                result += "Ctrl";
            }
            if (IsAltKey)
            {
                if (string.IsNullOrEmpty(result) == false)
                {
                    result += " + ";
                }
                result += "Alt";
            }
            if (true)
            {
                if (string.IsNullOrEmpty(result) == false)
                {
                    result += " + ";
                }
                result += Key.ToString();
            }
            return result;
        }

        public static readonly HotKeyData Empty = new HotKeyData(false, false, false, Keys.None);
    }

    class TemplatesSerializer
    {
        public Templates Deserialize(Stream stream)
        {
            var serializer = new DataContractSerializer(typeof(Templates));
            return (Templates)serializer.ReadObject(stream);
        }
        public void Serialize(Stream stream, Templates data)
        {
            var serializer = new DataContractSerializer(typeof (Templates));
            serializer.WriteObject(stream, data);
        }
        public Templates XmlDeserialize(string xml)
        {
            var serializer = new DataContractSerializer(typeof(Templates));
            var reader = new StringReader(xml);
            return (Templates)serializer.ReadObject(XmlReader.Create(reader));
        }
        public string XmlSerialize(Templates data)
        {
            var serializer = new DataContractSerializer(typeof(Templates));
            var writer = new StringWriter(new StringBuilder());
            serializer.WriteObject(XmlWriter.Create(writer), data);

            return writer.GetStringBuilder().ToString();
        }
    }
}
