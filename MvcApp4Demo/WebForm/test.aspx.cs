﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MvcApp4Demo.WebForm
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(System.Web.HttpContext.Current.Request.UserHostAddress);
             //   Response.Write(RouteData.Values["id"]);
            
        }
    }
}