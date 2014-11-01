using System.Web;
using System.Web.Mvc;

namespace MvcApp4Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeExAttribute());
        }
        //验证权限
        public class AuthorizeExAttribute : AuthorizeAttribute
        {
            /// <summary>
            /// 验证授权。
            /// </summary>
            /// <param name="httpContext">HTTP 上下文，它封装有关单个 HTTP 请求的所有 HTTP 特定的信息。</param>
            /// <returns>如果用户已经过授权，则为 true；否则为 false。</returns>
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                return true;
            }
            /// <summary>
            /// 重写验证。
            /// </summary>
            /// <param name="filterContext">验证信息上下文。</param>
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                base.OnAuthorization(filterContext);
                
            }

      
        }
    }
}