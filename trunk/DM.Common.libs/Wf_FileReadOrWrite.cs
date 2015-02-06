using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace DM.Common.libs
{
    public class Wf_FileReadOrWrite
    {
        private bool isServerPath = true;

        /// <summary>
        /// 是否虚拟路径
        /// </summary>
        public bool IsServerPath
        {
            get { return isServerPath; }
            set { isServerPath = value; }
        }
        /// <summary>
        /// 文件名

        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        private Encoding fileEncding = Encoding.GetEncoding("UTF-8");

        public Encoding FileEncding
        {
            get { return fileEncding; }
            set { fileEncding = value; }
        }

        private bool fileAppend = false;

        public bool FileAppend
        {
            get { return fileAppend; }
            set { fileAppend = value; }
        }

        /// <summary>
        /// 文件读取
        /// </summary>
        /// <returns></returns>
        public string FileRead()
        {
            StreamReader srRead = null;
            try
            {
                if (IsServerPath)
                {
                    srRead = new StreamReader(GetPath(FilePath), System.Text.Encoding.Default);  //读取文件
                }
                else
                {
                    srRead = new StreamReader(FilePath, System.Text.Encoding.Default);  //读取文件
                }
                string strValue = srRead.ReadToEnd();
                return strValue;
            }
            catch (Exception )
            {
                return "";
            }
            finally
            {
                if (srRead != null)
                {
                    srRead.Close();
                    srRead.Dispose();
                }
            }
        }

        /// <summary>
        /// 返回虚拟路径
        /// </summary>
        /// <param name="strPath">实际路径</param>
        /// <returns></returns>
        public static string GetPath(string strPath)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }

        #region 文件读取
        /// <summary>
        /// 文件读取
        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <returns></returns>
        public string FileRead(string strPath)
        {
            FilePath = strPath;
            return FileRead();
        }

        /// <summary>
        /// 文件读取
        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <returns></returns>
        public string FileRead(string strPath, Encoding encoding)
        {
            FilePath = strPath;
            fileEncding = encoding;
            return FileRead();
        }
        #endregion

        #region 文件写入
        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="strValue">要写入的内容</param>
        /// <returns></returns>
        public bool FileWrite(string strValue)
        {
            StreamWriter srWrite = null;
            try
            {
                if (IsServerPath)
                {
                    srWrite = new StreamWriter(GetPath(FilePath), fileAppend, fileEncding);
                }
                else
                {
                    srWrite = new StreamWriter(FilePath, fileAppend, fileEncding);
                }
                srWrite.Write(strValue);
                return true;
            }
            catch (Exception )
            {
                return false;
            }
            finally
            {
                if (srWrite != null)
                {
                    srWrite.Close();
                    srWrite.Dispose();
                }
            }
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="strValue">要写入的内容</param>
        /// <returns></returns>
        public bool FileWrite(string strValue, string strPath)
        {
            FilePath = strPath;
            return FileWrite(strValue);
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="strValue">要写入的内容</param>
        /// <returns></returns>
        public bool FileWrite(string strValue, string strPath, Encoding encoding)
        {
            fileEncding = encoding;
            FilePath = strPath;
            return FileWrite(strValue);
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="strValue">要写入的内容</param>
        /// <returns></returns>
        public bool FileWrite(string strValue, string strPath, Encoding encoding, bool append)
        {
            fileAppend = append;
            fileEncding = encoding;
            FilePath = strPath;
            return FileWrite(strValue);
        }
        #endregion

        #region 判断文件是否存在
        /// <summary>
        /// 判断文件是否存在 
        /// </summary>
        /// <param name="strPath">路径</param>
        /// <param name="strType">类型  0为文件,1为目录</param>
        /// <returns></returns>
        public bool FileExists(string strPath, string strType)
        {
            FilePath = strPath;
            return FileExists(strType);
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="strType">类型  0为文件,1为目录</param>
        /// <returns></returns>
        public bool FileExists(string strType)
        {
            if (strType == "0") //类型 0为文件,1为目录 
            {
                if (IsServerPath)
                {
                    return File.Exists(GetPath(FilePath));
                }
                else
                {
                    return File.Exists(FilePath);
                }

            }
            else //目录 
            {
                if (IsServerPath)
                {
                    return Directory.Exists(GetPath(FilePath));
                }
                else
                {
                    return Directory.Exists(FilePath);
                }
            }
        }
        #endregion

        #region 获取文件类
        /// <summary>
        /// 获取文件类

        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <returns></returns>
        public FileInfo GetFileInfo(string strPath)
        {
            FilePath = strPath;
            return GetFileInfo();
        }

        /// <summary>
        /// 获取文件类

        /// </summary>
        /// <returns></returns>
        public FileInfo GetFileInfo()
        {
            if (IsServerPath)
            {
                return new FileInfo(GetPath(FilePath));
            }
            else
            {
                return new FileInfo(FilePath);
            }
        }
        #endregion

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool DeleteFileByPath(string strPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(strPath))
                {
                    if (FileExists(strPath, "0"))
                    {
                        strPath = HttpContext.Current.Server.MapPath(strPath);
                        File.Delete(strPath);
                        return true;
                    }
                    else
                    {
                        //文件不存在
                        return false;
                    }
                }
                else
                {
                    //路径为空
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
