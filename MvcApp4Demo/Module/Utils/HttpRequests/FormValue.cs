using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Utils.HttpRequests
{
    public class FormValue
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 值,如果是文件，传文件名称
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// 文件二进制文件数据
        /// </summary>
        public byte[] BinaryData
        {
            get;
            set;
        }
    }
}
