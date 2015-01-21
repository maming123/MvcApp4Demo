using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcApp4Demo.Module.WeiXin;

namespace MvcApp4Demo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ReceiveMsgManager rmsg = new ReceiveMsgManager(@"<xml><ToUserName><![CDATA[gh_62b877ddf2dc]]></ToUserName>
<FromUserName><![CDATA[oNK_qjgPljFVprXzWC7hnJmxyL18]]></FromUserName>
<CreateTime>1416283659</CreateTime>
<MsgType><![CDATA[text]]></MsgType>
<Content><![CDATA[测试]]></Content>
<MsgId>6082891997465315618</MsgId>
</xml>");

        }
         [TestMethod]
        public void Getjsapi_ticketTest()
        {
           //string ss = "C931F576498A3A6BF9CF07072230FDAF8EBD0A66".ToLower();
            string s = new WXJsSdkManager("http://mm8.iego.cn/WebForm/testjsapi.aspx").Signature;
        }
    }
}
