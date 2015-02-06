//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DM.Common.libs;

//namespace HandleByOffice.COM.Console
//{
//    public class ConvertToHtml
//    {
//        protected void Convert2HtmlRich(string rawFilePath, string previewFilePath)
//        {
//            using (FileStream fs = new FileStream(rawFilePath, FileMode.Open, FileAccess.Read))
//            {
//                BinaryReader br = new BinaryReader(fs);
//                br.BaseStream.Seek(0, SeekOrigin.Begin);
//                byte[] data = br.ReadBytes((int)br.BaseStream.Length);
//                //string docId = previewFilePath.Substring(19, 32);
//                Dictionary<string, byte[]> convertedData = Convert2HtmlRich(data, Path.GetFileName(rawFilePath));
//            }
//        }

//        public Dictionary<string, byte[]> Convert2HtmlRich(byte[] rawData, string fileName)
//        {
//            try
//            {
//                Stopwatch sw = new Stopwatch();
//                sw.Start();

//                string fileRealName = Convert(rawData, fileName);
//                if (string.IsNullOrEmpty(fileRealName))
//                {
//                    Lgr.Log.Info("转换后的文件名为空，转换失败");
//                    return null;
//                }

//                Dictionary<string, byte[]> convertedDic = new Dictionary<string, byte[]>();
//                long totalBytes = 0;
//                //添加Html文件
//                string destFilePath = Path.Combine(Converted, string.Format("{0}{1}", fileRealName, TargetFileExt));
//                byte[] convertedBytes;
//                using (FileStream fileStream = new FileStream(destFilePath, FileMode.Open, FileAccess.Read))
//                {
//                    totalBytes += fileStream.Length;
//                    convertedBytes = new byte[fileStream.Length];
//                    fileStream.Read(convertedBytes, 0, convertedBytes.Length);
//                    fileStream.Close();
//                }
//                convertedDic.Add(fileName.Replace(Path.GetExtension(fileName), TargetFileExt), convertedBytes);

//                //添加其他文件，假如有
//                string othersBasePath = Path.Combine(Converted, string.Format("{0}.files", fileRealName));
//                if (Directory.Exists(othersBasePath))
//                {
//                    DirectoryInfo di = new DirectoryInfo(othersBasePath);
//                    FileInfo[] attcFiles = di.GetFiles();
//                    foreach (FileInfo file in attcFiles)
//                    {
//                        using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
//                        {
//                            totalBytes += fileStream.Length;
//                            convertedBytes = new byte[fileStream.Length];
//                            fileStream.Read(convertedBytes, 0, convertedBytes.Length);
//                            fileStream.Close();
//                            convertedDic.Add(Path.Combine(string.Format("{0}.files", fileRealName), file.Name), convertedBytes);
//                        }
//                    }
//                }
//                sw.Stop();

//                Lgr.Log.Info(string.Format("SystemId:{0},请求转换文件：{1}，生成文件{2}个，共计：{3}，耗时：{4}s",
//                                  systemId, fileName, convertedDic.Count, FileSizeHelper.FormatFileSize(totalBytes), sw.ElapsedMilliseconds / 1000.0));

//                return convertedDic;
//            }
//            catch (Exception ex)
//            {
//                Lgr.Log.Error(ex);
//                throw;
//            }
//        }

//        /// <summary>
//        /// 转换
//        /// </summary>
//        /// <param name="rawData"></param>
//        /// <param name="fileName"></param>
//        /// <returns>返回转换后的文件名，不包括后缀</returns>
//        private string Convert(byte[] rawData, string fileName)
//        {
//            try
//            {
//                string fileExt = Path.GetExtension(fileName);
//                string fileRealName = fileName.Substring(0, fileName.LastIndexOf('.'));
//                if (!SupportFileExt.Contains(fileExt.ToLower()))
//                {
//                    Lgr.Log.Info(string.Format("扩展名：{0} 不支持转换", fileExt));
//                    return string.Empty;
//                }

//                //先存成本地文件
//                string tmpFilePath = Path.Combine(ConvertorTmp, fileName);
//                //假如存在，先删除
//                if (File.Exists(tmpFilePath))
//                {
//                    Lgr.Log.Info(string.Format("文件 {0} 已存在，删除后重新生成", tmpFilePath));
//                    File.Delete(tmpFilePath);
//                }
//                FileStream file = new FileStream(tmpFilePath, FileMode.CreateNew, FileAccess.Write);
//                file.Write(rawData, 0, rawData.Length);
//                file.Flush();
//                file.Close();
//                if (!File.Exists(tmpFilePath))
//                {
//                    Lgr.Log.Info("生成文件临时失败");
//                    return string.Empty;
//                }

//                string destFilePath = Path.Combine(Converted, string.Format("{0}{1}", fileRealName, TargetFileExt));
//                Word2Html(tmpFilePath, destFilePath);
//                if (!File.Exists(destFilePath))
//                {
//                    Lgr.Log.Info("转换失败");
//                    return string.Empty;
//                }
//                return fileRealName;
//            }
//            catch (Exception ex)
//            {
//                Lgr.Log.Error(ex);
//                throw;
//            }
//        }
//    }
//}
