using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp4Demo.Module.Email
{
    public class EmailHelper163 : IEmailHelper
    {
        private string _Email163 = "";

        public string Email163
        {
            get { return _Email163; }
            set { _Email163 = value; }
        }

        #region IEmailHelper 成员

        public string SendEmail()
        {
            return "EmailHelper.SendEmail Method,SendEmail 163 OK!!";
            //throw new NotImplementedException();
        }

        #endregion
    }
}