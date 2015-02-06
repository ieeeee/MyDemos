

Office 文档（doc,docx,xls,xlsx,ppt,pptx）转 PDF 工具

使用要求：
1.安装office 2007；

2.安装本目录SaveAsPDFandXPS.exe插件；

3.项目添加COM引用：
1）Microsoft.Office.Interop.Excel
2）Microsoft.Office.Interop.PowerPoint
3）Microsoft.Office.Interop.Word

4.可能出现的问题（主要是权限问题）解决方法见：
1） “WIN7中组件服务中的DCOM配置找不到Microsoft Excel应用程序的解决办法和 - 程序与人生 - 博客频道 - CSDN.pdf”文档
2）无法嵌入互操作类型“Microsoft.Office.Interop.Word.ApplicationClass”。。。
--将引用属性【无法嵌入互操作类型】设置为false

