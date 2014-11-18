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
    }
}
