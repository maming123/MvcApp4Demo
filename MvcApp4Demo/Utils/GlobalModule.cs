using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp4Demo.Utils
{
    public class GlobalModule : IHttpModule
    {

        #region IHttpModule 成员

        public void Dispose()
        {

        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += application_BeginRequest;
        }

        void application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;

            

            string url = application.Request.Url.ToString();

            if (Utils.BaseCommon.IsFromMobile(application.Request.UserAgent))
            {
                //application.Response.Redirect("http://手机网址");
            }

            //if (Parser.ContainXssDengerChar(url))
            //{
            //    application.Response.Redirect("/error.aspx?r=xss");
            //    return;
            //}

            #region 解析C登录

            string ssic = application.Request.QueryString["c"];

            if (!string.IsNullOrEmpty(ssic))
            {
                

                #region 登录

                            string oldUrl = url.ToLower();
                            bool flags = (oldUrl.IndexOf("&c=") > -1);
                            string U = "";
                            if (flags)
                                U = url.ToLower().Replace("&c=" + ssic.ToLower(), "");
                            else
                                U = url.ToLower().Replace("c=" + ssic.ToLower(), "");
                            if (U.IndexOf("?&") > -1)
                                U = U.Replace("?&", "?");

                            application.Response.Redirect(U);

                #endregion
            }

            #endregion

            
        }
        #endregion
    }
}