<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfficeDocToPdfDemo.aspx.cs" Inherits="WebApplication2.OfficeDocToPdfDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .tbConvertRsInfo {
        }
    </style>

    <style type="text/css">
        body, table {
            font-size: 12px;
        }

        table {
            table-layout: fixed;
            empty-cells: show;
            border-collapse: collapse;
            margin: 0 auto;
        }

        td {
            height: 30px;
        }

        h1, h2, h3 {
            font-size: 12px;
            margin: 0;
            padding: 0;
        }

        .table {
            border: 1px solid #cad9ea;
            color: #666;
        }

            .table th {
                background-repeat: repeat-x;
                height: 30px;
            }

            .table td, .table th {
                border: 1px solid #cad9ea;
                padding: 0 1em 0;
            }

            .table tr.alter {
                background-color: #f5fafe;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal runat="server" ID="litConvertRs"></asp:Literal>
        </div>
    </form>
</body>
</html>
