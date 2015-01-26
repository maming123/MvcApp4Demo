using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MvcApp4Demo.Module.Utils;
using MvcApp4Demo.Module.Utils.HttpRequests;

namespace MvcApp4Demo.Module.WeiXin
{
    /// <summary>
    /// 微信JsSDK相关类
    /// </summary>
    public class WXJsSdkManager
    {
        public WXJsSdkManager(string url)
        {
            _url = url;
        }

        #region 属性
        private string _secret = "0c870848230d4bcc30228a6ccc15b33c";

        public string Secret
        {
            get { return _secret; }
            private set { _secret = value; }
        }
        private string _appId = "wx18929fbee7bdc45a";//: 'wx18929fbee7bdc45a',

        public string AppId
        {
            get { return _appId; }
            private set { _appId = value; }
        }
        private int _timestamp = 1421135724;

        public int Timestamp
        {
            get { return _timestamp; }
            private set { _timestamp = value; }
        }
        private string _nonceStr = "545804391";

        public string NonceStr
        {
            get { return _nonceStr; }
            private set { _nonceStr = value; }
        }



        private string _url = "";

        public string Url
        {
            get { return _url; }
            private set { _url = value; }
        }
        #endregion

        private string _signature = "";//: '0946570f179b38ecb2b40d159fe182d826730a1e',

        /// <summary>
        /// 获取网页签名
        /// </summary>
        public string Signature
        {
            get
            {
                //jsapi_ticket=sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg&noncestr=Wm3WZYTPz0wzccnW&timestamp=1414587457&url=http://mp.weixin.qq.com
                _signature = string.Format(@"jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}",Getjsapi_ticket(),NonceStr,Timestamp,Url);
                _signature= FormsAuthentication.HashPasswordForStoringInConfigFile(_signature, "SHA1").ToLower(); 
                return _signature;
            }
        }

        private string Getaccess_token()
        {
            RequestData rd = new RequestData();
            rd.Method = RequestMethods.Get;

            string url = string.Format(@"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",AppId,Secret);
            RequestResult r = HttpRequestHelper.RequestExtend(url, rd);
            //{"access_token":"j5iw8vUbInl2gEXp8F0mHGW3vyKAhmllAPwnEEv4WkFB3XAM5QkT2_TmdLzzVfHJsoRj8v2drw2ujcRQ3-7M-EG3pI0VN9BpmlHJDSPpHro","expires_in":7200}
            var obj = new
            {
                access_token = "",
                expires_in = 0
            };
            var objJson = BaseCommon.JsonToObject(r.Html, obj);
            if (objJson.access_token != "")
                return objJson.access_token;
            else
                return "";
        }
        private string Getjsapi_ticket()
        {
            string cacheKey = string.Format(@"weixinticket");
            if (BaseCommon.HasCache(cacheKey))
                return BaseCommon.GetCache<string>(cacheKey);

            string access_token = Getaccess_token();
            RequestData rd = new RequestData();
            rd.Method = RequestMethods.Get;

            //https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=ACCESS_TOKEN&type=jsapi
            string url = string.Format(@"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
            RequestResult r = HttpRequestHelper.RequestExtend(url, rd);
//            {
//"errcode":0,
//"errmsg":"ok",
//"ticket":"bxLdikRXVbTPdHSM05e5u5sUoXNKd8-41ZO3MhKoyN5OfkWITDGgnr2fwJ0m9E8NYzWKVZvdVtaUgWvsdshFKA",
//"expires_in":7200
//}
            var obj = new
            {
                errcode = 0,
                errmsg = "",
                ticket="",
                expires_in=0
            };
            var objJson = BaseCommon.JsonToObject(r.Html, obj);
            if(objJson.ticket!="")
            {
                BaseCommon.CacheInsert(cacheKey, objJson.ticket, DateTime.Now.AddSeconds(objJson.expires_in - 10));
                return objJson.ticket;
            }else
            {
                return "";
            }

        }
    }
}