using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MvcApp4Demo.Models;
namespace MvcApp4Demo.API
{
    public class AreaController : ApiController
    {

        public Area[] area = new Area[] { new Area(){ Code="000000",Name="水利部"},new Area(){ Code="110000", Name="北京"} };
        
        //
        // GET: /Area/ 此处的HttpGet 命名空间：System.Web.Http 而不是System.Web.Mvc
        [HttpGet]
        public List<Area> FindAll()
        {
            string s = HttpContext.Current.Request.UserHostAddress;
            return area.ToList();
        }

    }
}
