using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp4Demo.Models
{
    public class Area
    {
        private string _Name = "";
        private string _Code = "";

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}