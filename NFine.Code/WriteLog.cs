using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NFine.Code
{
    public class WriteLog
    {
        static string errorPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Log\";
        static object obj = new object();
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        public static void Write(string errorMessage)
        {
            try
            {
                lock (obj)
                {
                    DirectoryInfo errorDir = new DirectoryInfo(errorPath);
                    if (!errorDir.Exists)
                    {
                        errorDir.Create();
                    }
                    StreamWriter swr;
                    string strDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                    string strLogFilePath = errorPath + "\\" + strDate + ".log"; //日志文件的文件路径
                    swr = File.AppendText(strLogFilePath);
                    swr.WriteLine(DateTime.Now.ToString() + " " + errorMessage);
                    swr.Close();
                }
            }
            catch
            { }
        }
        /// <summary>
        /// 记录错误信息。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="errorMsg"></param>
        public static void Write(string fileName, string errorMsg)
        { 
            try
            {
                lock (obj)
                {
                    DirectoryInfo errorDir = new DirectoryInfo(errorPath);
                    if (!errorDir.Exists)
                    {
                        errorDir.Create();
                    }
                    StreamWriter swr;
                    string strDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                    string strLogFilePath = string.Format("{0}\\{1}.log",errorPath,fileName); //日志文件的文件路径
                    swr = File.AppendText(strLogFilePath);
                    swr.WriteLine(DateTime.Now.ToString() + " " + errorMsg);
                    swr.Close();
                }
            }
            catch
            { }
        }
        /// <summary>
        /// 删除日志文件
        /// </summary>
        /// <param name="date">删除该日期前的日志</param>
        public static void DeleteLog(DateTime date)
        {
            if (Directory.Exists(errorPath))
            {
                string[] dirs = Directory.GetFiles(errorPath);
                for  (int i=0;i< dirs.Length;i++)
                { 
                    DateTime fileDate = File.GetLastWriteTime(dirs[i]);
                    if (date.CompareTo(fileDate)  > 0)
                    {
                        File.Delete(dirs[i]);
                    }
                }
            }
        }
    }
}
