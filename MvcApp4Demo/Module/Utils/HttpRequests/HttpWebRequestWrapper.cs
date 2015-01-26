
/*********************************************************
 * 开发人员：王中海
 * 创建时间：2013/6/24 11:55:45
 * 描述说明：
 *          1、
 * 更改历史：
 *          1、Jack - 2013/6/24 11:55:45 - Add
 * *******************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace MvcApp4Demo.Module.Utils.HttpRequests
{
    /// <summary>
    /// HttpWebRequest调用包装
    /// </summary>
    public static class HttpWebRequestWrapper
    {
        /// <summary>
        /// 用Get方法请求，请求响应均使用UTF8编码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        public static string Request(string url)
        {
            return Request(url, RequestMethod.Get, null, null, Encoding.UTF8, Encoding.UTF8, null);
        }

        /// <summary>
        /// 用Post方法请求，请求响应均使用UTF8编码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Request(string url, string requestContentType, string postData)
        {
            return Request(url, RequestMethod.Post, requestContentType, postData, Encoding.UTF8, Encoding.UTF8, null);
        }

        /// <summary>
        /// 发送请求，获取相应信息
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="requestMethod">请求方法</param>
        /// <param name="postData">Post数据</param>
        /// <param name="requestEncoding">请求数据编码</param>
        /// <param name="responseEncoding">响应数据编码</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static string Request(string url, RequestMethod requestMethod, string requestContentType, string postData, Encoding requestEncoding, Encoding responseEncoding, int? timeOut)
        {
            if (String.IsNullOrEmpty(url) &&
                !Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                return null;
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                if (request == null)
                {
                    return null;
                }

                request.Method = requestMethod == RequestMethod.Post ? "POST" : "GET";

                if (requestContentType != null)
                {
                    request.ContentType = requestContentType;
                }

                if (requestMethod == RequestMethod.Post)
                {
                    if (postData != null)
                    {
                        byte[] bData = null;
                        bData = requestEncoding.GetBytes(postData);
                        request.ContentLength = bData.Length;
                        Stream newStream = request.GetRequestStream();
                        newStream.Write(bData, 0, bData.Length);
                        newStream.Close();
                    }
                }

                if (timeOut != null && timeOut > 0)
                {
                    request.Timeout = timeOut.Value;
                }

                // Get response 
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response == null)
                {
                    return null;
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), responseEncoding);
                    string content = reader.ReadToEnd();
                    reader.Close();
                    return content;
                }
            }
            catch
            {

            }

            return null;
        }

        /// <summary>
        /// 用Post方法请求，请求响应均使用UTF8编码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestContentType"></param>
        /// <param name="postData">key:value</param>
        /// <returns></returns>
        public static string Request(string url, string requestContentType, Dictionary<string, string> postData)
        {
            return Request(url, RequestMethod.Post, requestContentType, GetPostData(postData), Encoding.UTF8, Encoding.UTF8, null);
        }

        /// <summary>
        /// 取得Post的数据 key value
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static string GetPostData(Dictionary<string, string> dic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in dic)
            {
                sb.AppendFormat(@"&{0}={1}", kv.Key, kv.Value);
            }
            if (sb.ToString() != "")
                return sb.ToString().Substring(1);
            else
                return "";
        }
    }

    /// <summary>
    /// 请求方法 
    /// </summary>
    public enum RequestMethod
    {
        Post,
        Get
    }
}
