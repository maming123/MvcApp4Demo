using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Module.Utils.HttpRequests
{
    public class RequestData
    {
        private List<FormValue> formValue = new List<FormValue>();
        private List<Header> headers = new List<Header>();
        private RequestMethods method = RequestMethods.Post;
        private Encoding requestEncoding = Encoding.UTF8;
        private Encoding responseEncoding = Encoding.UTF8;
        private int timeout = 300000;
        private string contentType = "application/x-www-form-urlencoded";
        private string userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
        private string accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        private bool keepAlive = true;


        /// <summary>
        /// 添加Cookie
        /// </summary>
        public string Cookie
        {
            get;
            set;
        }

        /// <summary>
        /// 请求类型
        /// </summary>
        public RequestMethods Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }

        }

        /// <summary>
        /// 请求的编码格式，默认为UTF8
        /// </summary>
        public Encoding RequestEncoding
        {
            get
            {
                return requestEncoding;
            }
            set
            {
                requestEncoding = value;
            }
        }

        /// <summary>
        /// 响应的编码格式，默认为UTF8
        /// </summary>
        public Encoding ResponseEncoding
        {
            get
            {
                return responseEncoding;
            }
            set
            {
                responseEncoding = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }

        /// <summary>
        /// 默认：application/x-www-form-urlencoded
        /// 如有图片请写：multipart/form-data;
        /// </summary>
        public string ContentType
        {
            get
            {
                return contentType;
            }
            set
            {
                contentType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserAgent
        {
            get
            {
                return userAgent;
            }
            set
            {
                userAgent = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Accept
        {
            get
            {
                return accept;
            }
            set
            {
                accept = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool KeepAlive
        {
            get
            {
                return keepAlive;
            }
            set
            {
                keepAlive = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WebProxy WebProxy
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
        /// Form提交的表单内容。
        /// </summary>
        public List<FormValue> FormValue
        {
            get
            {
                return formValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddFormValue(string name, string value)
        {
            FormValue item = new FormValue();
            item.Name = name;
            item.Value = value;
            FormValue.Add(item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddFormValue(string name, string fileName, byte[] binaryData)
        {
            FormValue item = new FormValue();
            item.Name = name;
            item.Value = fileName;
            item.BinaryData = binaryData;
            FormValue.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddHeader(string key, string value)
        {
            Header item = new Header();
            item.Key = key;
            item.Value = value;
            Headers.Add(item);
        }
    }
}