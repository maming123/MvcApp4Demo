using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using MvcApp4Demo.Module.Utils.HttpRequests;

namespace MvcApp4Demo.Module.WeiXin
{
    public class HouseManager
    {
        private string _keyWord = "";

        public string KeyWord
        {
            get { return _keyWord; }
            set { _keyWord = value; }
        }
        public HouseManager(string keyWord)
        {
            _keyWord = keyWord;
        }
        public string GetMsg()
        {
            string s =  GetHtmlFromUri();

            int i = s.IndexOf(@"<td width=""90"" background=""images/g09.gif"" align=""center"" class=""b1"">发布时间</td>");
            string s2 = s.Substring(i);
            int i2 = s2.IndexOf("</table>");
            string s3 = s2.Substring(0, i2);
            s3=  Regex.Replace(s3, "<td><a href=\"(.+)\" target=_blank>", "");
            s3 = s3.Replace(@"<td width=""90"" background=""images/g09.gif"" align=""center"" class=""b1"">发布时间</td>", "").Replace("<tr bgcolor=\"#ffffff\" align=\"center\">", "").Replace("<td height=\"30\" align=\"left\">", "").Replace("<tr bgcolor=\"#ffffff\" align=center>", "").Replace("<td height=30 align=left>", "").Replace("</tr>", "|").Replace("<td>", "").Replace("</td>", " , ").Replace("\n", "").Replace("             ", "").Replace("\"", "'").Replace("           ", "").Replace("个人", "").Replace("<img src=\"images/cam.jpg\" title=\"此信息含有图片\">", "").Replace("<img src=images/cam.jpg title='此信息含有图片'>", "");
              //<td><a href="http://www.hlgnet.com/user/seeinfo.php?seename=18810207034" target=_blank>18810207034</td>
            //最多2048个字节 哎...
            return "观网搜索：http://search.hlgnet.com/?c=2&m=1&q=&c1=1&t=1 " + s3.Substring(0, 1024);
        }
        private string GetHtmlFromUri()
        {
            string url = string.Format(@"http://search.hlgnet.com/?c=2&m=1&q={0}&c1=1&t=1&x=23&y=10",System.Web.HttpUtility.UrlEncode(KeyWord));

            RequestData rd = new Utils.HttpRequests.RequestData();
            rd.Method = RequestMethods.Get;

            RequestResult r = HttpRequestHelper.RequestExtend(url,rd);
           
            return r.Html;
        }
    }
}