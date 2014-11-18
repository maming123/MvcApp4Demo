using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp4Demo.Module.WeiXin
{
    public class SendMsgManager
    {
        private ReceiveMsgItem _ReceiveMsgItem;
        private SendMsgItem _SendMsgItem;
        public SendMsgManager(ReceiveMsgItem receiveMsgItem)
        {
            _ReceiveMsgItem = receiveMsgItem;

            _SendMsgItem = new SendMsgItem() {
             ToUserName=receiveMsgItem.FromUserName
             ,FromUserName=receiveMsgItem.ToUserName
             ,MsgType= receiveMsgItem.MsgType
             , CreateTime=int.Parse(DateTime.Now.ToString("Hmmss"))
            }; 
        }

        /// <summary>
        /// 获得接收的文本信息
        /// </summary>
        /// <returns></returns>
        public string GetTextMsgItemToXml()
        {
            SendTextMsgItem txtMsgItem = new SendTextMsgItem() { 
             FromUserName=_SendMsgItem.FromUserName
             , ToUserName=_SendMsgItem.ToUserName
             , CreateTime=_SendMsgItem.CreateTime
             , MsgType=_SendMsgItem.MsgType
            };
            string rcontent = ((ReceiveTextMsgItem)_ReceiveMsgItem).Content;
            if (rcontent == "1")
            {
                txtMsgItem.Content = "1、锄禾日当午";
            }
            else
            {
                txtMsgItem.Content = "您输入的内容是："+rcontent;
            }

            return txtMsgItem.GetMsgItemToXml();
        }

    }
    public class SendMsgItem
    {
        private MsgType _MsgType = MsgType.text;

        public MsgType MsgType
        {
            get { return _MsgType; }
            set { _MsgType = value; }
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
        public virtual string GetMsgItemToXml()
        {
            return "需要重写";
        }
    }

    public class SendTextMsgItem : SendMsgItem
    {
        private string _Content = "";

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        public override string GetMsgItemToXml()
        {
            string strXmlTemplate = @"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName>     <CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>";
            string sReturn = "";

            sReturn = string.Format(strXmlTemplate, ToUserName, FromUserName, CreateTime, Content);

            return sReturn;

        }

    }
}