using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Newtonsoft.Json;

namespace Module.Utils
{
    public class BaseCommon
    {
        /// <summary>
        /// Core连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return GetConnectionString("Core");
            }
        }
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionName)
        {
            if (ConfigurationManager.ConnectionStrings[connectionName] != null)
            {
                return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            }

            return null;
        }

        private static Logger commonLogger = new Logger();

        /// <summary>
        /// 系统通用日志对象
        /// </summary>
        public static Logger CommonLogger
        {
            get
            {
                return commonLogger;
            }
        }

        /// <summary>
        /// 判断访问页面的来源。页面来自手机:true  来自其他浏览器：false
        /// </summary>
        /// <returns></returns>
        public static bool IsFromMobile(string userAgent)
        {
            //string userAgent = context.Request.UserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                if (userAgent.IndexOf("Windows Phone OS", StringComparison.CurrentCultureIgnoreCase) > -1
                    || userAgent.IndexOf("iPhone", StringComparison.CurrentCultureIgnoreCase) > -1
                    || userAgent.IndexOf("Android", StringComparison.CurrentCultureIgnoreCase) > -1
                    || userAgent.IndexOf("UC", StringComparison.CurrentCultureIgnoreCase) > -1
                    /*||true*/
                    )
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        /// <summary>
        /// 判断访问页面的来源。页面来自手机:true  来自其他浏览器：false
        /// </summary>
        /// <returns></returns>
        public static bool IsFromMobile()
        {
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                if (userAgent.IndexOf("Windows Phone OS", StringComparison.CurrentCultureIgnoreCase) > -1
                    || userAgent.IndexOf("iPhone", StringComparison.CurrentCultureIgnoreCase) > -1
                    || userAgent.IndexOf("Android", StringComparison.CurrentCultureIgnoreCase) > -1
                    || userAgent.IndexOf("UC", StringComparison.CurrentCultureIgnoreCase) > -1
                    /*||true*/
                    )
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// 跳转到手机小页面
        /// </summary>
        /// <param name="toUrl"></param>
        public static void RedirectToMobilePage(string toUrl)
        {
            if (IsFromMobile())
            {
                string pams = System.Web.HttpContext.Current.Request.QueryString.ToString();
                pams = string.IsNullOrEmpty(pams) ? "" : "?" + pams;
                toUrl = toUrl + pams;
                //System.Web.HttpContext.Current.Server.Transfer(toUrl, true);
                System.Web.HttpContext.Current.Response.Redirect(toUrl, true);
            }
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration">过期时间</param>
        public static void CacheInsert(string key, object value, DateTime absoluteExpiration)
        {
            if (value != null)
            {
                HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
            }
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration">相对过期时间间隔</param>
        public static void CacheInsert(string key, object value, TimeSpan slidingExpiration)
        {
            if (value != null)
            {
                HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        public static T GetCache<T>(string key)
        {
            if (HasCache(key))
            {
                return (T)HttpRuntime.Cache[key];
            }

            return default(T);
        }

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasCache(string key)
        {
            return HttpRuntime.Cache[key] != null;
        }

        /// <summary>
        /// 缓存移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void CacheRemove(string key)
        {
            if (HttpRuntime.Cache[key] != null)
                HttpRuntime.Cache.Remove(key);
        }


        /// <summary>
        /// 对象序列化成json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            return obj == null ? "" : JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// json反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            T t = JsonConvert.DeserializeObject<T>(json);

            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="anonymousTypeObject"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType<T>(json, anonymousTypeObject);

            return t;
        }
    }
}