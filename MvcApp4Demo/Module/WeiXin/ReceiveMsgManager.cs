using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MvcApp4Demo.Module.WeiXin
{
    public class ReceiveMsgManager
    {
        private XmlNode _XmlNode ;
        private ReceiveMsgItem _MsgItem;
        public ReceiveMsgManager(string xmlBody)
        {
            XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xmlBody);
            _XmlNode = xmlDoc.DocumentElement;

            _MsgItem = GetMsgItem();
        }

        public ReceiveMsgItem GetMsgItem()
        {
            ReceiveMsgItem msgItem = new ReceiveMsgItem();
            string msgType = _XmlNode.SelectSingleNode("MsgType").InnerText;
            msgItem.MsgType=(MsgType)Enum.Parse(typeof(MsgType), msgType);
            msgItem.ToUserName = _XmlNode.SelectSingleNode("ToUserName").InnerText;
            msgItem.FromUserName = _XmlNode.SelectSingleNode("FromUserName").InnerText;
            msgItem.MsgId = long.Parse(_XmlNode.SelectSingleNode("MsgId").InnerText);
            msgItem.CreateTime = int.Parse(_XmlNode.SelectSingleNode("CreateTime").InnerText);
            return msgItem;
        }

        /// <summary>
        /// 解析接收的文本信息
        /// </summary>
        /// <returns></returns>
        public ReceiveTextMsgItem GetTextMsgItem()
        {
            ReceiveTextMsgItem txtMsg = new ReceiveTextMsgItem() { FromUserName=_MsgItem.FromUserName, ToUserName=_MsgItem.ToUserName
            , CreateTime=_MsgItem.CreateTime
            , MsgId=_MsgItem.MsgId
            , MsgType=_MsgItem.MsgType};

            txtMsg.Content = _XmlNode.SelectSingleNode("Content").InnerText;
            return txtMsg;
        }

    }
    public class ReceiveMsgItem
    {
        //<xml><ToUserName><![CDATA[gh_62b877ddf2dc]]></ToUserName>
//<FromUserName><![CDATA[oNK_qjgPljFVprXzWC7hnJmxyL18]]></FromUserName>
//<CreateTime>1416283659</CreateTime>
//<MsgType><![CDATA[text]]></MsgType>
//<Content><![CDATA[测试]]></Content>
//<MsgId>6082891997465315618</MsgId>
//</xml>


       private MsgType _MsgType = MsgType.text;

        public MsgType MsgType
        {
            get { return _MsgType; }
            set { _MsgType = value; }
        }

        private long _MsgId = 0;

        public long MsgId
        {
            get { return _MsgId; }
            set { _MsgId = value; }
        }

        private int _CreateTime = 0;

        public int CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private string _FromUserName = "";

        public string FromUserName
        {
            get { return _FromUserName; }
            set { _FromUserName = value; }
        }
        private string _ToUserName = "";

        public string ToUserName
        {
            get { return _ToUserName; }
            set { _ToUserName = value; }
        }
    }

    public enum MsgType:int
    {
        text
    }

    public class ReceiveTextMsgItem : ReceiveMsgItem
    {
        private string _Content = "";

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
    }
}