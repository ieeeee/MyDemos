﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="split_document.aspx.cs" Inherits="split_document" %>
<%@ Import Namespace="lib" %>
<%@ Register TagPrefix="flexpaper" TagName="annotations_handler" Src="~/annotations_handlers.ascx" %>
<!doctype html>
<html>
    <head> 
        <title>FlexPaper</title>         
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
        <style type="text/css" media="screen"> 
			html, body	{ height:100%; }
			body { margin:0; padding:0; overflow:auto; }   
			#flashContent { display:none; }
        </style> 
		
		<link rel="stylesheet" type="text/css" href="css/flexpaper_flat.css" />
		<script type="text/javascript" src="js/jquery.min.js"></script>
		<script type="text/javascript" src="js/jquery.extensions.min.js"></script>
		<script type="text/javascript" src="js/flexpaper.js"></script>
		<script type="text/javascript" src="js/flexpaper_handlers.js"></script>
    </head> 
    <body>
        <%
			// Setting current document from parameter or defaulting to 'Paper.pdf'
			String doc = "Paper.pdf";
			if(Request["doc"]!=null)
			    doc = Request["doc"].ToString();
			
			String pdfFilePath = configManager.getConfig("path.pdf") + doc;
			String swfFilePath = configManager.getConfig("path.swf");
		%>
			<div id="documentViewer" class="flexpaper_viewer" style="position:absolute;left:10px;top:10px;width:800px;height:500px"></div>

	        <script type="text/javascript">
                var numPages 			= <%=Common.getTotalPages(pdfFilePath,swfFilePath,doc) %>;

		        function getDocumentUrl(document){
					var url = "{services/view.ashx?doc={doc}&format={format}&page=[*,0],{numPages}}";
						url = url.replace("{doc}",document);
						url = url.replace("{numPages}",numPages);
						return url;	        
		        }

		        function append_log(msg){
                    $('#txt_eventlog').val(msg+'\n'+$('#txt_eventlog').val());
                }

                String.format = function() {
                    var s = arguments[0];
                    for (var i = 0; i < arguments.length - 1; i++) {
                        var reg = new RegExp("\\{" + i + "\\}", "gm");
                        s = s.replace(reg, arguments[i + 1]);
                    }

                    return s;
                }

                var startDocument       = "<% if(Request["doc"]!=null){Response.Write(Request["doc"]);}else{%>Paper.pdf<% } %>";
		        var swfFileUrl 			= escape('{services/view.ashx?doc='+startDocument+'&page=[*,0],'+numPages+'}');
				var searchServiceUrl	= escape('aspnet/services/containstext.ashx?doc='+startDocument+'&page=[page]&searchterm=[searchterm]');

                <% if(configManager.getConfig("sql.verified") == "true"){ %>
                <flexpaper:annotations_handler id="annotations_handler1" runat="server" />
                <% } %>

                jQuery.get((!window.isTouchScreen)?'UI_flexpaper_desktop_flat.html':'UI_flexpaper_mobile.html',
                function(toolbarData) {
				$('#documentViewer').FlexPaperViewer(
				  { config : {
						 
						 DOC : escape(getDocumentUrl(startDocument)),
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
						 InitViewMode : 'Portrait',
						 RenderingOrder : '<%=(configManager.getConfig("renderingorder.primary") + ',' + configManager.getConfig("renderingorder.secondary")) %>',
						 
						 ViewModeToolsVisible : true,
						 ZoomToolsVisible : true,
						 NavToolsVisible : true,
						 CursorToolsVisible : true,
						 SearchToolsVisible : true,

                         Toolbar         : toolbarData,
                         BottomToolbar           : 'UI_flexpaper_annotations.html',

  						 key : '<%=configManager.getConfig("licensekey") %>',
  						 
  						 DocSizeQueryService : 'services/swfsize.ashx?doc=<%=doc %>',
						 JSONDataType : 'jsonp',
						   						 
  						 localeChain: 'en_US'
						 }}
                    );
                });
	        </script>
            <% if(configManager.getConfig("sql.verified") == "true"){ %>
            <div style="position:absolute;left:830px;top:10px;font-family:Arial;font-size:12px"><b>Database Event Log</b><br/><textarea rows=6 cols=28 id="txt_eventlog" style="width:370px;font-size:9px;" wrap="off"></textarea></div>
            <% } %>
   </body> 
</html> 