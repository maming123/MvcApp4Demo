using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApp4Demo.Module.Utils;

namespace MvcApp4Demo.Handler
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : BaseHandler
    {
        public Handler1()
        {
            base.dictAction.Add("Hello", Hello);
            base.dictAction.Add("AddRouteToRouteTable", AddRouteToRouteTable);
            base.dictAction.Add("ClearAllRouteTable", ClearAllRouteTable);
        }

        private void ClearAllRouteTable()
        {
            if (System.Web.Routing.RouteTable.Routes.Count > 0)
            {
                System.Web.Routing.RouteTable.Routes.Clear();
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<object>(){code=1, m="清理成功"}));
            }else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<object>() { code = 2, m = "RouteTable中已没有路由信息" }));
            }
        }

        private void Hello()
        {
            Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<object>() { code = 1, m = "HelloWorld" }));
            return;
            //throw new NotImplementedException();
        }

        private void AddRouteToRouteTable()
        {
           object defaults=new { controller = "Home", action = "Index", id = System.Web.Mvc.UrlParameter.Optional };
            // 参数默认值
            //System.Web.Mvc.MvcRouteHandler
            System.Web.Routing.Route route = new System.Web.Routing.Route("{controller}/{action}/{id}", new System.Web.Mvc.MvcRouteHandler()) { Defaults = new System.Web.Routing.RouteValueDictionary(defaults) };
            System.Web.Routing.RouteTable.Routes.Add(route);

        }
    }
}