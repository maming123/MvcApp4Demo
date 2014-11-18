using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp4Demo.Module.Email
{
    public interface IEmailHelper
    {
        string SendEmail();
    }
}