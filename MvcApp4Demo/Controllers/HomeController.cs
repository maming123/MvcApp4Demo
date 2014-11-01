using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp4Demo.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index(FormCollection formC,int id=0)
        {

            return View();
            //在controller里边重定向
            //return Redirect("http://www.baidu.com");
           
        }

        //注入JsonP跨域访问方法
        //http://116.113.33.54/mm/index.html
        //var eleScript= document.createElement("script");
        //eleScript.type = "text/javascript";
        //eleScript.src = "http://maming.fx-func.com:8071/home/GetTestJsForJsonP?jscallback=test";
        //document.getElementsByTagName("HEAD")[0].appendChild(eleScript);
        // GET: /Home/
        [AllowAnonymous]
        public JavaScriptResult GetTestJsForJsonP(string jscallback)
        {
            JavaScriptResult js =new JavaScriptResult();
            js.Script = string.Format(@"{0}('测试JsonP CallBack');", jscallback);
            return js;
        }

    }
}
