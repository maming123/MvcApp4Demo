using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;


namespace MvcApp4Demo.Module.Utils
{
    public class Logger
    {

        private string logTable = "Common_Log";

        public Logger()
        {

        }

        public Logger(string logTable)
        {
            if (!string.IsNullOrEmpty(logTable))
            {
                this.logTable = logTable;
            }
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="ex"></param>
        public void Info(Exception ex)
        {
            string logInfo = "";
            if (ex != null)
            {
                logInfo = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
                Info(logInfo, -1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="appendInfo"></param>
        public void Info(Exception ex, string appendInfo)
        {
            Info(ex, appendInfo, -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="appendInfo"></param>
        /// <param name="logType"> </param>
        public void Info(Exception ex, string appendInfo, int logType)
        {
            if (appendInfo == null)
            {
                appendInfo = "";
            }


            string logInfo = "";
            if (ex != null)
            {
                logInfo = String.Format("[{0}]\r\n{1}\r\n{2}", appendInfo, ex.Message, ex.StackTrace);
                ex = ex.InnerException;
                while (ex != null)
                {
                    logInfo += string.Format("\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    ex = ex.InnerException;
                }
            }
            else
            {
                logInfo = String.Format("[{0}]", appendInfo);
            }
            Info(logInfo, logType);
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logType"></param>
        public void Info(Exception ex, int logType)
        {
            if (ex != null)
            {
                string logInfo = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
                Info(logInfo, logType);
            }
        }

        /// <summary>
        /// 记录日志，非异常
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="appendInfo"> </param>
        public void Info(string logInfo, string appendInfo)
        {
            string info = String.Format("[{0}]{1}", appendInfo, logInfo);
            Info(info, 0);
        }

        /// <summary>
        /// 记录日志，非异常
        /// </summary>
        /// <param name="logInfo"></param>
        public void Info(string logInfo)
        {
            Info(logInfo, 0);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="type">0信息，-1异常</param>
        public static void Info(string logInfo, int type)
        {
            if (logInfo == null)
            {
                logInfo = "";
            }
            string sClassName = "";
            try
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
                for (int i = 0; i < trace.FrameCount; i++)
                {
                    System.Reflection.MethodBase mb = trace.GetFrame(i).GetMethod();
                    //只记录最多5层堆栈
                    if (mb.DeclaringType.FullName == "System.RuntimeMethodHandle" || i >= 5)
                        break;
                    sClassName += " | " + mb.DeclaringType.FullName + "." + mb.Name;
                }
            }
            catch (Exception ex)
            {
                sClassName = ex.Message + ex.Source + ex.StackTrace;
            }

            logInfo = string.Format("{0}:{1},{2}", Environment.MachineName, logInfo, sClassName);

            string sql = String.Format(
                "insert into {0} (Info, Type) values(@Info, @Type) ", Database.ErrorLogTable);

            SqlParameter[] paras = new SqlParameter[]{
                            new SqlParameter("@Info", logInfo),
                            new SqlParameter("@Type", type)
                        };
            string strpath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Replace("/", "\\");
            strpath = strpath.Substring(0, strpath.LastIndexOf("\\")).Replace(@"file:\\\", "");

            try
            {
                //TODO:已注释掉往数据库写日志
                //SqlHelper.ExecuteSql(sql, paras, BaseCommon.ConnectionString);
                string content = string.Format(@"{0}:Info:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), logInfo + "\r\n");
                WriteToDisk(strpath, content);
            }
            catch (Exception ex)
            {
                string content = string.Format(@"{0}:ERROR:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message + ex.Source + ex.StackTrace + "\r\n");
                WriteToDisk(strpath, content);
            }
        }


        #region 错误写到硬盘文件
        /// <summary>
        /// 同步锁
        /// </summary>
        public static object syncWriteTxtLock = new object();

        /// <summary>
        /// 向指定路径写入日志,写到文本文件中
        /// </summary>
        /// <param name="strpath">只要给出路径就可以，e.g: d:\</param>
        public static void WriteToDisk(string strpath, string content)
        {
            lock (syncWriteTxtLock)
            {
                try
                {
                    string path = strpath + @"\log_" + DateTime.Now.ToString("yyyyMM") + ".txt";
                    if (!File.Exists(path))
                    {
                        using (FileStream fs = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(content);
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    }
                    else
                    {
                        System.IO.StreamWriter sw = new StreamWriter(path, true);
                        sw.Write(content + "\r\n");
                        sw.Close();
                        sw.Dispose();
                    }
                }
                catch { }
            }
        }


        #endregion

        #region write log

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(Exception ex)
        {
            string fileName = string.Format(
                AppDomain.CurrentDomain.BaseDirectory + "/AtallServiceLog_Exception_{0}.txt",
                DateTime.Now.ToString("yyyyMMdd"));

            var writer = new StreamWriter(fileName, true);
            string str = string.Format("-----------------{0}-BEGIN-----------------------------", DateTime.Now);
            writer.WriteLine(str);
            while (ex != null)
            {
                writer.WriteLine(ex.Message);
                writer.WriteLine(ex.StackTrace);
                ex = ex.InnerException;
            }

            str = string.Format("-----------------{0}-THE END-----------------------------", DateTime.Now);
            writer.WriteLine(str);
            writer.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg)
        {
            string fileName = string.Format(AppDomain.CurrentDomain.BaseDirectory + "/AtallServiceLog_info_{0}.txt",
                DateTime.Now.ToString("yyyyMMdd"));

            var writer = new StreamWriter(fileName, true);
            string str = string.Format("-----------------{0}-BEGIN-----------------------------\r\n", DateTime.Now);
            writer.WriteLine(str);
            if (!string.IsNullOrEmpty(msg))
            {
                writer.WriteLine(msg);
            }

            str = string.Format("-----------------{0}-THE END-----------------------------\r\n", DateTime.Now);
            writer.WriteLine(str);
            writer.Close();
        }

        #endregion
    }

}

