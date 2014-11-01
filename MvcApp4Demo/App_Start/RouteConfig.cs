using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApp4Demo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //MVC中以路由方式使用WebForm，也可以直接通过物理路径访问webform页面
            //routes.MapPageRoute("test-webform", "webform/test/{id}", "~/WebForm/test.aspx");

            //如果有一般处理程序 即使这里模拟了ashx的路由 那么也会先去查找一般处理程序
            routes.MapRoute(
                            name: "Default3",
                            url: "{controller}/{action}.ashx",
                            defaults: new { controller = "handler", action = "handler1" }
                        );


            routes.MapRoute(
                name: "Default2",
                url: "{controller}/{action}/{id}.html",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}