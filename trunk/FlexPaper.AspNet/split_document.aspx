<%@ Page Language="C#" AutoEventWireup="true" CodeFile="split_document.aspx.cs" Inherits="split_document" %>


<!doctype html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
<head>
    <title>FlexPaper</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="initial-scale=1,user-scalable=no,maximum-scale=1,width=device-width" />
    <style type="text/css" media="screen">
        html, body {
            height: 100%;
        }

        body {
            margin: 0;
            padding: 0;
            overflow: auto;
        }

        #flashContent {
            display: none;
        }
    </style>

    <link rel="stylesheet" type="text/css" href="css/flexpaper.css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.extensions.min.js"></script>
    <script type="text/javascript" src="js/flexpaper.js"></script>
    <script type="text/javascript" src="js/flexpaper_handlers.js"></script>
</head>
<body>
    <%
        // Setting current document from parameter or defaulting to 'Paper.pdf'
        String doc = string.Empty, docfullname = string.Empty;

        String pdfFilePath = configManager.getConfig("path.pdf");
        String swfFilePath = configManager.getConfig("path.swf");

        if (Request["doc"] != null)
        {
            //权限判断
            //code here

            //查询文档详细信息
            doc = Request["doc"].ToString();

            //doc = "57B1AF6029C072D2DA6BCBF609BF47D0";

            //docfullname = @"2015\01\380A3EF2B27FCF17AB7D9005D2B63FDE\banche.pdf";
            //docfullname = @"2015\01\57B1AF6029C072D2DA6BCBF609BF47D0\CSharpLanguageSpecification.pdf";
            Guid g = Guid.NewGuid();
            if (Guid.TryParse(doc, out g))
            {
                //FlexPaper.AspNet.Codem.DBHelper dbh = new FlexPaper.AspNet.Codem.DBHelper();
                //FlexPaper.AspNet.Codem.DocModel m = dbh.getDoc(new Guid(doc));

                using (DocServiceClient client = new DocServiceClient())
                {
                    Tencent.OA.DocEncyclic.Contract.Entity.Doc m = client.GetByDocID(new Guid(doc));

                    if (m != null && !string.IsNullOrWhiteSpace(m.PreviewURL))
                    {
                        pdfFilePath = pdfFilePath + m.PreviewURL;
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write("doc 文档未找到.");
                        Response.End();
                    }
                }
            }
            else
            {
                Response.Clear();
                Response.Write("doc 参数错误.");
                Response.End();
            }
        }
    %>
    <div id="documentViewer" class="flexpaper_viewer" style="position: absolute; left: 10px; top: 10px; width: 770px; height: 500px"></div>

    <script type="text/javascript">
        var numPages 			= <%=FlexPaper.AspNet.Codem.Common.getTotalPages(pdfFilePath,swfFilePath,doc) %>;

        function getDocumentUrl(document){
            var url = "{services/view.ashx?doc={doc}&format={format}&page=[*,0],{numPages}}";
            url = url.replace("{doc}",document);
            url = url.replace("{numPages}",numPages);
            return url;	        
        }
        var doc 				= '<%=doc %>';
        var swfFileUrl 			= escape('{services/view.ashx?doc='+doc+'&page=[*,0],'+numPages+'}');
        var searchServiceUrl	= escape('services/containstext.ashx?doc='+doc+'&page=[page]&searchterm=[searchterm]');

        $('#documentViewer').FlexPaperViewer(
          { config : {						 
              DOC : escape(getDocumentUrl("<%=doc %>")),
              Scale : 0.6, 
              ZoomTransition : 'easeOut',
              ZoomTime : 0.5, 
              ZoomInterval : 0.1,
              FitPageOnLoad : true,
              FitWidthOnLoad : false, 
              FullScreenAsMaxWindow : false,
              ProgressiveLoading : false,
              MinZoomSize : 0.2,
              MaxZoomSize : 5,
              SearchMatchAll : false,
              SearchServiceUrl : searchServiceUrl,
              RenderingOrder : '<%=(configManager.getConfig("renderingorder.primary") + ',' + configManager.getConfig("renderingorder.secondary")) %>',						 
              ViewModeToolsVisible : true,
              ZoomToolsVisible : true,
              NavToolsVisible : true,
              CursorToolsVisible : true,
              SearchToolsVisible : true,
              key : '<%=configManager.getConfig("licensekey") %>',  						 
              DocSizeQueryService : 'services/swfsize.ashx?doc=<%=doc %>',
              JSONDataType : 'jsonp',						   						 
              localeChain: 'en_US'
          }}
				);
    </script>

</body>
</html>
