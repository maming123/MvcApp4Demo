using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using MvcApp4Demo.Module.WeiXin;
namespace MvcApp4Demo.API
{
    /// <summary>
    /// 微信开发者测试
    /// </summary>
    public class WeiXinController : ApiController
    {
        /// <summary>
        /// define your token
        /// </summary>
        private string Token = "nsmis";

        /// <summary>
        /// 开发者提交信息后，微信服务器将发送GET请求到填写的URL上，GET请求携带四个参数：
        //参数	描述
        //signature	微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
        //timestamp	时间戳
        //nonce	随机数
        //echostr	随机字符串
        //开发者通过检验signature对请求进行校验（下面有校验方式）。若确认此次GET请求来自微信服务器，请原样返回echostr参数内容，则接入生效，成为开发者成功，否则接入失败。

        //加密/校验流程如下：
        //1. 将token、timestamp、nonce三个参数进行字典序排序
        //2. 将三个参数字符串拼接成一个字符串进行sha1加密
        //3. 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Check(string signature, string timestamp, string nonce, string echostr)
        {
            WriteLog(string.Format(@"signature:{0},timestamp:{1},nonce:{2},echostr:{3}",signature,timestamp,nonce,echostr));
            string r = "";
            r = Valid(signature, timestamp, nonce, echostr);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(r, Encoding.UTF8);

            return response;
        }
        [HttpPost]
        public HttpResponseMessage Check(string signature, string timestamp, string nonce)
        {
            string r = "";
            if (CheckSignature(signature, timestamp, nonce))
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string postStr = Encoding.UTF8.GetString(b);
                WriteLog(string.Format(@"signature:{0},timestamp:{1},nonce:{2},echostr:{3}", signature, timestamp, nonce,""));
                WriteLog("postStr:" + postStr);
                if (!string.IsNullOrEmpty(postStr))
                {
//                    //ResponseMsg(postStr);
//                    r = postStr;
//                    r = string.Format(@"<xml>
//<ToUserName><![CDATA[oNK_qjgPljFVprXzWC7hnJmxyL18]]></ToUserName>
//<FromUserName><![CDATA[gh_62b877ddf2dc]]></FromUserName>
//<CreateTime>12345678</CreateTime>
//<MsgType><![CDATA[text]]></MsgType>
//<Content><![CDATA[你好{0}]]></Content>
//</xml>","宝贝儿");

                    ReceiveMsgManager rmm = new ReceiveMsgManager(postStr);
                    ReceiveTextMsgItem receivetxtMsgItem = rmm.GetTextMsgItem();

                    SendMsgManager smm = new SendMsgManager(receivetxtMsgItem);
                    r = smm.GetTextMsgItemToXml();
                }
            }
            
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(r, Encoding.UTF8);
            return response;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature(string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            string tmpStrSHA1 = SHA1(tmpStr);
            tmpStr = tmpStrSHA1.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证来自微信的TOKEN
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        private string  Valid(string signature, string timestamp, string nonce, string echostr)
        {
            string echoStr = echostr;
            if (CheckSignature(signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    return echoStr;
                }
                else
                    return "";
            }
            else
                return "";
        }

        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 写日志(用于跟踪)
        /// </summary>
        private void WriteLog(string strMemo)
        {
            //如果其他宿主的webapi 那么需要用此转换
            //HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            //HttpRequestBase request = context.Request;//定义传统request对象            
            //string filename = context.Server.MapPath("~/log.txt");
            string filename = System.Web.HttpContext.Current.Server.MapPath("~/log.txt");
            StreamWriter sr = null;
            try
            {
                if (!File.Exists(filename))
                {
                    sr = File.CreateText(filename);
                }
                else
                {
                    sr = File.AppendText(filename);
                }
                sr.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" : "+strMemo);
            }
            catch
            {
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        private static string SHA1(string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }



    #region php 文档
    //    <?php
///**
//  * wechat php test
//  */

////define your token
//define("TOKEN", "weixin");
//$wechatObj = new wechatCallbackapiTest();
//$wechatObj->valid();

//class wechatCallbackapiTest
//{
//    public function valid()
//    {
//        $echoStr = $_GET["echostr"];

//        //valid signature , option
//        if($this->checkSignature()){
//            echo $echoStr;
//            exit;
//        }
//    }
		
//    private function checkSignature()
//    {
//        // you must define TOKEN by yourself
//        if (!defined("TOKEN")) {
//            throw new Exception('TOKEN is not defined!');
//        }
        
//        $signature = $_GET["signature"];
//        $timestamp = $_GET["timestamp"];
//        $nonce = $_GET["nonce"];
        		
//        $token = TOKEN;
//        $tmpArr = array($token, $timestamp, $nonce);
//        // use SORT_STRING rule
//        sort($tmpArr, SORT_STRING);
//        $tmpStr = implode( $tmpArr );
//        $tmpStr = sha1( $tmpStr );
		
//        if( $tmpStr == $signature ){
//            return true;
//        }else{
//            return false;
//        }
//    }

    //    public function responseMsg()
    //    {
    //        //get post data, May be due to the different environments
    //        $postStr = $GLOBALS["HTTP_RAW_POST_DATA"];

    //        //extract post data
    //        if (!empty($postStr)){
    //                /* libxml_disable_entity_loader is to prevent XML eXternal Entity Injection,
    //                   the best way is to check the validity of xml by yourself */
    //                libxml_disable_entity_loader(true);
    //                $postObj = simplexml_load_string($postStr, 'SimpleXMLElement', LIBXML_NOCDATA);
    //                $fromUsername = $postObj->FromUserName;
    //                $toUsername = $postObj->ToUserName;
    //                $keyword = trim($postObj->Content);
    //                $time = time();
    //                $textTpl = "<xml>
    //                            <ToUserName><![CDATA[%s]]></ToUserName>
    //                            <FromUserName><![CDATA[%s]]></FromUserName>
    //                            <CreateTime>%s</CreateTime>
    //                            <MsgType><![CDATA[%s]]></MsgType>
    //                            <Content><![CDATA[%s]]></Content>
    //                            <FuncFlag>0</FuncFlag>
    //                            </xml>";             
    //                if(!empty( $keyword ))
    //                {
    //                    $msgType = "text";
    //                    $contentStr = "Welcome to wechat world!";
    //                    $resultStr = sprintf($textTpl, $fromUsername, $toUsername, $time, $msgType, $contentStr);
    //                    echo $resultStr;
    //                }else{
    //                    echo "Input something...";
    //                }

    //        }else {
    //            echo "";
    //            exit;
    //        }
    //    }

//}

    //?>
    #endregion
}
