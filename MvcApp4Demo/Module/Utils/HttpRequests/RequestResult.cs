using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp4Demo.Module.Utils.HttpRequests
{
    public class RequestResult
    {
        private List<Header> headers = new List<Header>();

        /// <summary>
        /// 
        /// </summary>
        public int StatusCode
        {
            get;
            set;
        }

        /// <summary>
        /// 头部信息
        /// </summary>
        public List<Header> Headers
        {
            get
            {
                return headers;
            }
        }

        /// <summary>
        /// Html值
        /// </summary>
        public string Html
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetHeader(string key)
        {
            foreach (Header header in Headers)
            {
                if (header.Key == key)
                {
                    return header.Value;
                }
            }

            return "";
        }

        public string Cookie
        {
            get;
            set;
        }

        /// <summary>
        /// 输出流
        /// </summary>
        public byte[] ResponseStreamBytes
        {
            get;
            set;
        }
    }
}
