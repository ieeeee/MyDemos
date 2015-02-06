using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
//--------------------------------------------------------
//--------------------------------------------------------
//--------文件上传辅助工具类
//--------作者：webfans
//--------日期：2013-09-29
//--------HttpPostedFile postFile = Request.Files["pfile"];
//--------
//--------FileUploadHelper uploader = new FileUploadHelper(postFile, "/configs/upload/uploadconfig_logo.xml");
//--------string result = uploader.UploadFileFrom_HttpPostFile().ToString();
//--------string str = uploader.GetUploadRsOfZh_CN(result);
//--------------------------------------------------------
//--------------------------------------------------------
//--------FileUploadHelper uploader = new FileUploadHelper(this.FileUpload1, "/configs/upload/uploadconfig_logo.xml");
//--------string result = uploader.UploadFileFrom_FileUpload().ToString();
//--------string str = uploader.GetUploadRsOfZh_CN(result);
//--------------------------------------------------------
//--------使用上传返回的结果---------------------------------
//--------result:FileUploadHelper.UpLoadResult.SUCCESS
//--------str:上传成功
//--------uploader.FileSrcName:原文件名.jpg 
//--------uploader.FileNewName:logo_201309291633188437.jpg 
//--------uploader.FileFullName:/upfile/logo/logo_201309291633188437.jpg
//--------------------------------------------------------
//--------------------------------------------------------
namespace DM.Common.libs
{
    #region 上传文件辅助工具
    public class Wf_FileUploadHelper
    {
        #region 属性

        /// <summary>
        /// 服务器上传控件
        /// </summary>
        public FileUpload _FileUpload
        {
            set;
            get;
        }

        /// <summary>
        /// 服务器上传控件
        /// </summary>
        public HttpPostedFile Htpostfile
        {
            set;
            get;
        }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string UserConfigPath
        {
            get;
            set;
        }

        /// <summary>
        /// 原文件名称:xxx.jpg
        /// </summary>
        public string FileSrcName
        {
            set;
            get;
        }

        /// <summary>
        /// 上传成功后:返回的文件名称19000101010100.jpg
        /// </summary>
        public string FileNewName
        {
            set;
            get;
        }

        /// <summary>
        /// 完整文件名称包括目录信息
        /// </summary>
        public string FileFullName
        {
            set;
            get;
        }

        /// <summary>
        /// 枚举上传结果
        /// </summary>
        public enum UpLoadResult
        {
            //上传控件未实例化,或者文件为空
            FILEUPLOAD_CONTROL_ISNULL,

            //您未选择文件
            FILE_ISNULL,

            //加载配置文件失败
            LOAD_USERCONFIG_FAIL,

            //文件类型不被允许
            FILE_TYPE_IS_NOT_ALLOW,

            //文件大小超过限制
            FILE_SIZE_IS_TOOMAX,

            //文件目录不存在
            VIRTUALDIRECTORY_IS_NOTFOUND,

            //没有权限创建目录
            CAN_NOT_CREATEDIRECTORY,

            //没有权限保存文件
            CAN_NOT_PERMISSION_TOSAVE,

            //上传文件时出现错误
            UPLOAD_EXCEPTION,

            //上传成功
            SUCCESS
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fupd">服务器上传控件</param>
        /// <param name="UserConfigPath">上传文件工具类所需的上传参数配置文件路径</param>
        public Wf_FileUploadHelper(FileUpload _FileUpload, string UserConfigPath)
        {
            this._FileUpload = _FileUpload;
            this.UserConfigPath = UserConfigPath;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fupd">Post文件对象</param>
        /// <param name="UserConfigPath">上传文件工具类所需的上传参数配置文件路径</param>
        public Wf_FileUploadHelper(HttpPostedFile Htpostfile, string UserConfigPath)
        {
            this.Htpostfile = Htpostfile;
            this.UserConfigPath = UserConfigPath;
        }
        #endregion

        #region 执行文件上传方法
        /// <summary>
        /// 执行上传:From FileUpload
        /// </summary>
        /// <returns>UpLoadResult</returns>
        public UpLoadResult UploadFileFrom_FileUpload()
        {
            if (_FileUpload != null)
            {
                return UploadFileFrom_HttpPostFile(_FileUpload.PostedFile);
            }
            else
            {
                return UpLoadResult.FILEUPLOAD_CONTROL_ISNULL;
            }
        }

        /// <summary>
        /// 执行上传:From HttpPostFile
        /// </summary>
        /// <returns>UpLoadResult</returns>
        public UpLoadResult UploadFileFrom_HttpPostFile()
        {
            return UploadFileFrom_HttpPostFile(Htpostfile);
        }

        /// <summary>
        /// 执行上传:From HttpPostFile
        /// </summary>
        /// <param name="PostFile"></param>
        /// <returns>UpLoadResult</returns>
        public UpLoadResult UploadFileFrom_HttpPostFile(HttpPostedFile PostFile)
        {
            try
            {
                HttpPostedFile HtpostfileModel = PostFile;

                if (HtpostfileModel != null && !string.IsNullOrEmpty(PostFile.FileName))
                {
                    //加载配置文件
                    wfs_UploadConfig configModel = LoadUploadConfig();

                    //检查配置文件是否有效
                    if (IsValidOfUploadConfig(configModel))
                    {
                        //检测虚拟路径是否存在（暂时没有考虑创建目录权限问题）
                        string VirtualDirectory = HttpContext.Current.Server.MapPath(configModel.wfs_VirtualDirectory);
                        if (!Directory.Exists(VirtualDirectory))
                        {
                            //不存在虚拟目录 >> 创建
                            Directory.CreateDirectory(VirtualDirectory);
                        }

                        //获取文件扩展名
                        string fileExt = Path.GetExtension(HttpUtility.UrlDecode(PostFile.FileName)).ToLower();

                        //是否允许上传的文件类型
                        bool flag_IsAllow_Ext = configModel.wfs_AllowFileType.Contains(fileExt.Trim('.'));

                        //文件检测(防止修改文件后缀,上传非法文件)
                        bool flag_FileIsValid = IsVliadOfPostFile(configModel.wfs_FileType, PostFile);

                        if (flag_IsAllow_Ext && flag_FileIsValid)
                        {
                            //获取文件大小
                            double fileSize = PostFile.ContentLength;

                            //将文件大小单位转为MB
                            fileSize = Math.Round(fileSize / 1024.0 / 1024.0, 1);

                            //是否满足文件最大限制
                            bool flag_IsLeftFileMaxSize_Ext = fileSize <= configModel.wfs_FileMaxSize;

                            if (flag_IsLeftFileMaxSize_Ext)
                            {
                                //文件名前缀
                                string UserCustomFileName = (!string.IsNullOrEmpty(configModel.wfs_FilePrefix)) ? configModel.wfs_FilePrefix : string.Empty;

                                //是否以当前日期+时间作为文件名
                                if (configModel.wfs_FileNameIsDate == 1)
                                {
                                    UserCustomFileName = UserCustomFileName + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                                }
                                else
                                {
                                    UserCustomFileName = UserCustomFileName + Path.GetRandomFileName();
                                }

                                //原文件名
                                FileSrcName = PostFile.FileName;

                                //新的文件名称
                                FileNewName = UserCustomFileName + fileExt;

                                //文件物理名称
                                FileFullName = configModel.wfs_VirtualDirectory + FileNewName;

                                //保存文件
                                PostFile.SaveAs(VirtualDirectory + FileNewName);

                                //上传成功
                                return UpLoadResult.SUCCESS;
                            }
                            else
                            {
                                return UpLoadResult.FILE_SIZE_IS_TOOMAX;
                            }
                        }
                        else
                        {
                            return UpLoadResult.FILE_TYPE_IS_NOT_ALLOW;
                        }
                    }
                    else
                    {
                        return UpLoadResult.LOAD_USERCONFIG_FAIL;
                    }
                }
                else
                {
                    return UpLoadResult.FILE_ISNULL;
                }
            }
            catch (Exception ex)
            {
                return UpLoadResult.UPLOAD_EXCEPTION;
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns>wfs_UploadConfig</returns>
        private wfs_UploadConfig LoadUploadConfig()
        {
            wfs_UploadConfig configModel = null;
            try
            {
                //将对象序列化到XML文档中,或者从XML文档中反序列化对象
                XmlSerializer xs = new XmlSerializer(typeof(wfs_UploadConfig));

                string userConfigPath = HttpContext.Current.Server.MapPath(UserConfigPath);

                FileStream fs = new FileStream(userConfigPath, FileMode.Open, FileAccess.Read);

                configModel = (wfs_UploadConfig)xs.Deserialize(fs);

                fs.Close();
            }
            catch (Exception ex)
            { }
            return configModel;
        }

        /// <summary>
        /// 检测上传配置文件是否有效
        /// </summary>
        /// <returns>有效：true, 无效：false</returns>
        private bool IsValidOfUploadConfig()
        {
            return IsValidOfUploadConfig(LoadUploadConfig());
        }

        /// <summary>
        /// 检测上传配置文件是否有效
        /// </summary>
        /// <param name="configModel">wfs_UploadConfig</param>
        /// <returns>有效：true, 无效：false</returns>
        private bool IsValidOfUploadConfig(wfs_UploadConfig configModel)
        {
            if (configModel != null)
            {
                if (string.IsNullOrEmpty(configModel.wfs_FileType))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(configModel.wfs_VirtualDirectory))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(configModel.wfs_AllowFileType))
                {
                    return false;
                }
                if (configModel.wfs_FileNameIsDate == null)
                {
                    return false;
                }
                if (configModel.wfs_FileMaxSize == null)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断文件是否是图片文件
        /// </summary>
        /// <param name="filePath">文本路径</param>
        /// <returns>布尔值</returns>
        private bool IsImage(string filePath)
        {
            System.Drawing.Image image;
            try
            {
                image = System.Drawing.Image.FromFile(filePath);
                image.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断文件是否是图片文件
        /// </summary>
        /// <param name="filePath">文件流</param>
        /// <returns>布尔值</returns>
        private bool IsImage(Stream stream)
        {
            System.Drawing.Image image;
            try
            {
                image = System.Drawing.Image.FromStream(stream, false, true);
                image.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                stream.Close();
                return false;
            }
        }

        /// <summary>
        ///文件检测(防止修改文件后缀,上传非法文件)
        /// </summary>
        /// <param name="wfs_FileType">文件类型（IMG...）</param>
        /// <param name="PostFile">HttpPostedFile</param>
        /// <returns>true,false</returns>
        private bool IsVliadOfPostFile(string wfs_FileType, HttpPostedFile PostFile)
        {
            switch (wfs_FileType)
            {
                case "IMG": 
                    return IsImage(PostFile.InputStream);
                default: 
                    return false;
            }
        }

        /// <summary>
        /// 获取上传结果中文解释
        /// </summary>
        /// <param name="Item">结果代码</param>
        /// <returns>代码对应的中文解释</returns>
        public string GetUploadRsOfZh_CN(string UploadRsOfCode)
        {
            string rsMsgOfZh_Cn = "";

            if (string.IsNullOrEmpty(UploadRsOfCode))
            { rsMsgOfZh_Cn = "上传失败"; }

            if (UploadRsOfCode == UpLoadResult.FILEUPLOAD_CONTROL_ISNULL.ToString())
            { rsMsgOfZh_Cn = "上传控件未实例化"; }

            if (UploadRsOfCode == UpLoadResult.LOAD_USERCONFIG_FAIL.ToString())
            { rsMsgOfZh_Cn = "加载配置文件失败"; }

            if (UploadRsOfCode == UpLoadResult.FILE_ISNULL.ToString())
            { rsMsgOfZh_Cn = "您未选择文件"; }

            if (UploadRsOfCode == UpLoadResult.FILE_TYPE_IS_NOT_ALLOW.ToString())
            { rsMsgOfZh_Cn = "文件类型不被允许,或者文件已损坏"; }

            if (UploadRsOfCode == UpLoadResult.FILE_SIZE_IS_TOOMAX.ToString())
            { rsMsgOfZh_Cn = "文件大小超过限制"; }

            if (UploadRsOfCode == UpLoadResult.VIRTUALDIRECTORY_IS_NOTFOUND.ToString())
            { rsMsgOfZh_Cn = "文件目录不存在"; }

            if (UploadRsOfCode == UpLoadResult.CAN_NOT_CREATEDIRECTORY.ToString())
            { rsMsgOfZh_Cn = "没有权限创建目录"; }

            if (UploadRsOfCode == UpLoadResult.CAN_NOT_PERMISSION_TOSAVE.ToString())
            { rsMsgOfZh_Cn = "没有权限保存文件"; }

            if (UploadRsOfCode == UpLoadResult.UPLOAD_EXCEPTION.ToString())
            { rsMsgOfZh_Cn = "上传文件时出现错误"; }

            if (UploadRsOfCode == UpLoadResult.SUCCESS.ToString())
            { rsMsgOfZh_Cn = "上传成功"; }

            return rsMsgOfZh_Cn;
        }
        #endregion

        #region 生成缩略图 暂无
        #endregion
    }
    #endregion

    #region 上传文件参数模型类定义
    /// <summary>
    /// 上传文件参数模型
    /// </summary>
    [XmlRoot("wfs_UploadConfig")]
    public class wfs_UploadConfig
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public wfs_UploadConfig()
        { }

        /// <summary>
        /// 文件类型（）
        /// </summary>
        [XmlElement(ElementName = "wfs_FileType")]
        public string wfs_FileType
        { get; set; }

        /// <summary>
        /// 虚拟路径
        /// </summary>
        [XmlElement(ElementName = "wfs_ThumbVirtualDirectory")]
        public string wfs_ThumbVirtualDirectory
        { get; set; }

        /// <summary>
        /// 缩略图虚拟路径
        /// </summary>
        [XmlElement(ElementName = "wfs_VirtualDirectory")]
        public string wfs_VirtualDirectory
        { get; set; }

        /// <summary>
        /// 允许的文件类型
        /// </summary>
        [XmlElement(ElementName = "wfs_AllowFileType")]
        public string wfs_AllowFileType
        { get; set; }

        /// <summary>
        /// 文件最大大小(单位:MB)
        /// </summary>
        [XmlElement(ElementName = "wfs_FileMaxSize")]
        public int? wfs_FileMaxSize
        { get; set; }

        /// <summary>
        /// 保存文件前缀(可选)
        /// </summary>
        [XmlElement(ElementName = "wfs_FilePrefix")]
        public string wfs_FilePrefix
        { get; set; }

        /// <summary>
        /// 是否以日期格式作为文件名(可选值:1是,0否(随即文件名))
        /// </summary>
        [XmlElement(ElementName = "wfs_FileNameIsDate")]
        public int? wfs_FileNameIsDate
        { get; set; }
    }

    /*XML文档模板
    <?xml version="1.0" encoding="utf-8" ?>
    <wfs_UploadConfig>
        <![CDATA[必须,文件类型,用于上传类型检查可取值有:IMG,AUDIO,VIDEO...]]>
        <wfs_FileType>IMG</wfs_FileType>
  
        <![CDATA[必须,虚拟路径]]>
        <wfs_VirtualDirectory>/upfile/logo/</wfs_VirtualDirectory>
  
        <![CDATA[必须,缩略图虚拟路径-暂未使用]]>
        <wfs_ThumbVirtualDirectory>~/upfile/logo/Thumb/</wfs_ThumbVirtualDirectory>
  
        <![CDATA[必须,允许的文件类型]]>
        <wfs_AllowFileType>jpg|gif|png|bmp</wfs_AllowFileType>
  
        <![CDATA[必须,文件最大大小(单位:MB)]]>
        <wfs_FileMaxSize>1</wfs_FileMaxSize>  
  
        <![CDATA[必须,是否以日期格式作为文件名(可选值:1是,0否(随机文件名))]]>
        <wfs_FileNameIsDate>1</wfs_FileNameIsDate>
  
        <![CDATA[可选,保存文件前缀]]>
        <wfs_FilePrefix>logo_</wfs_FilePrefix>
    </wfs_UploadConfig>
    */
    #endregion
}
