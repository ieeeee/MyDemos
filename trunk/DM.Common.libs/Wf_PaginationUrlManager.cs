using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.Common.libs
{
    /// <summary>
    /// 分页URL管理基类
    /// </summary>
    public class Wf_PaginationUrlManager
    {
        #region 私有属性
        /// <summary>
        /// 默认：每页显示记录数
        /// </summary>
        private const double Default_PageSize = 20;

        /// <summary>
        /// 默认：分页参数名称
        /// </summary>
        private const string Default_PageParameter = "page";

        /// <summary>
        /// 默认：HTML注释格式
        /// </summary>
        private const string HTML_NOTE_FORMAT = "<!--{0}-->";
        #endregion

        #region 公开属性
        /// <summary>
        /// 公开：分页数据条目总数
        /// </summary>
        public double RecordCount
        {
            set;
            get;
        }

        /// <summary>
        /// 公开：每页显示记录数
        /// </summary>
        public double PageSize
        {
            set;
            get;
        }

        /// <summary>
        /// 公开：当前页索引（当前是第几页）
        /// </summary>
        public int IndexOfPage
        {
            set;
            get;
        }

        /// <summary>
        /// 公开：分页参数名称
        /// </summary>
        public string PageParameter
        {
            set;
            get;
        }

        /// <summary>
        /// 公开：其他参数
        /// </summary>
        public string OtherParameter
        {
            set;
            get;
        }

        /// <summary>
        /// 公开：是否始终显示分页栏
        /// </summary>
        public bool IsAlwaysShowPagerBar
        {
            set;
            get;
        }

        /// <summary>
        /// 公开：分页栏使用场景
        /// </summary>
        public enum PageUrlUsingScene
        {
            /// <summary>
            /// 一般使用场景
            /// </summary>
            General = 0,

            /// <summary>
            /// Ajax使用场景
            /// </summary>
            Ajax = 1
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Wf_PaginationUrlManager()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecordCount">分页数据条目总数</param>
        /// <param name="PageSize">每页显示记录数</param>
        /// <param name="PageParameter">分页参数名称</param>
        /// <param name="OtherParameter">其他参数</param>
        public Wf_PaginationUrlManager(double RecordCount, double PageSize = Default_PageSize, string PageParameter = Default_PageParameter, string OtherParameter = "")
        {
            this.RecordCount = RecordCount;
            this.PageSize = PageSize;
            this.PageParameter = PageParameter;
            this.OtherParameter = OtherParameter;
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 生成分页Bar
        /// </summary>
        /// <param name="UsingScene">必需：分页栏使用场景可选值：General,Ajax</param>
        /// <param name="IndexOfPage">必需：当前页索引</param>
        /// <returns>分页Bar HTML</returns>
        public string CreatePageUrl(PageUrlUsingScene UsingScene, int IndexOfPage)
        {
            StringBuilder SbUrlFormat = new StringBuilder();

            //检查：检查分页必要条件
            if (this.RecordCount > 0 && this.PageSize > 0)
            {
                //分页总数
                double PageCount = Math.Ceiling(this.RecordCount / this.PageSize);

                #region 开始生成分页元素
                //判断：分页总数 <= 1页（仅有1页数据）
                if ((PageCount > 1) || (PageCount <= 1 && IsAlwaysShowPagerBar))
                {
                    SbUrlFormat.Append(CreateUrlOfGeneral(UsingScene, PageCount, IndexOfPage));
                }
                else
                {
                    SbUrlFormat.AppendFormat(HTML_NOTE_FORMAT, "当前数据仅有[1]页,默认不显示PageBar,需要显示请设置[IsAlwaysShowPagerBar = treu;]");
                }
                #endregion
            }
            else
            {
                SbUrlFormat.AppendFormat(HTML_NOTE_FORMAT, "初始化分页必要条件[RecordCount,PageSize]检查失败.");
            }

            return SbUrlFormat.ToString();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 生成分页Html
        /// </summary>
        /// <param name="PageCount">必需：分页总数</param>
        /// <param name="IndexOfPage">必需：当前页索引</param>
        /// <returns></returns>        
        private string CreateUrlOfGeneral(PageUrlUsingScene UsingScene, double PageCount, double IndexOfPage)
        {
            #region 分页元素模板
            string PagePerTmp = string.Empty;
            string PageFirtTmpAvailable = string.Empty;
            string PagePrevTmpAvailable = string.Empty;
            string PageNextTmpAvailable = string.Empty;
            string PageLastTmpAvailable = string.Empty;

            string PageCurTmp = "<span class='span_pager_cur'>{0}</span>";
            string PageFirsTmpUnavailable = "<span class='span_pager_firt_unavailable'>第一页</span>";
            string PagePrevTmpUnavailable = "<span class='span_pager_prev_unavailable'>上一页</span>";
            string PageNextTmpUnavailable = "<span class='span_pager_next_unavailable'>下一页</span>";
            string PageLastTmpUnavailable = "<span class='span_pager_last_unavailable'>最后一页</span>";

            switch (UsingScene)
            {
                case PageUrlUsingScene.General:
                    PagePerTmp = "<a class='a_page_per' href='?{0}={1}'>{1}</a>";
                    PageFirtTmpAvailable = "<a class='a_pager_firt_available' href='?{0}={1}'>第一页</a>";
                    PagePrevTmpAvailable = "<a class='a_pager_prev_available' href='?{0}={1}'>上一页</a>";
                    PageNextTmpAvailable = "<a class='a_pager_next_available' href='?{0}={1}'>下一页</a>";
                    PageLastTmpAvailable = "<a class='a_pager_last_available' href='?{0}={1}'>最后一页</a>";
                    break;
                case PageUrlUsingScene.Ajax:
                    PagePerTmp = "<a class='a_page_per' href='javascript:;' data-{0}='{1}'>{1}</a>";
                    PageFirtTmpAvailable = "<a class='a_pager_firt_available' href='javascript:;' data-{0}='{1}'>第一页</a>";
                    PagePrevTmpAvailable = "<a class='a_pager_prev_available' href='javascript:;' data-{0}='{1}'>上一页</a>";
                    PageNextTmpAvailable = "<a class='a_pager_next_available' href='javascript:;' data-{0}='{1}'>下一页</a>";
                    PageLastTmpAvailable = "<a class='a_pager_last_available' href='javascript:;' data-{0}='{1}'>最后一页</a>";
                    break;
                default:
                    break;
            }
            #endregion

            #region 重置当前页索引
            IndexOfPage = (IndexOfPage >= PageCount) ? PageCount : IndexOfPage;
            IndexOfPage = (IndexOfPage <= 1) ? 1 : IndexOfPage;
            #endregion

            #region 开始拼接分页元素并返回
            StringBuilder SbUrlFormat = new StringBuilder();
            SbUrlFormat.Append("<ul class='ul_webfans_libs_pager'>");
            for (int i = 1; i <= PageCount; i++)
            {
                SbUrlFormat.Append("<li class='li'>");
                if (i == IndexOfPage)
                {
                    //当前页
                    SbUrlFormat.AppendFormat(PageCurTmp, i);
                }
                else
                {
                    SbUrlFormat.AppendFormat(PagePerTmp, this.PageParameter, i);
                }
                SbUrlFormat.Append("</li>");
            }
            SbUrlFormat.Append("</ul>");
            return SbUrlFormat.ToString();
            #endregion
        }
        #endregion
    }
}
