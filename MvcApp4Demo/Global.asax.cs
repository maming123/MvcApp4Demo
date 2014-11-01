using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace MvcApp4Demo
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ASP.NET MVC4中有一个Display Mode，它可以依Browser类型，来决定要传回的是那一个View
            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("IE9")
            {
                ContextCondition = (context =>
                    context.Request.UserAgent.Contains("MSIE 9"))
            });
            //如果是监测到手机 不建议用此法（因处理逻辑可能不同），建议跳转到新的手机处理域名中（跳转可在application.beginRequest管道中执行判断并跳转）
            //Windows Phone OS  iPhone  Android UC
            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("WAP") { ContextCondition = context => Utils.BaseCommon.IsFromMobile(context.Request.UserAgent) });

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string str = "";
        }
    }
}