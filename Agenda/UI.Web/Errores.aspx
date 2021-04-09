<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Errores.aspx.cs" Inherits="UI.Web.Errores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maxium-scale=1.0, minimum-scale=1.0"/>
    <link href="style/bootstrap.min.css" rel="stylesheet" />
	<script src="https://kit.fontawesome.com/5520773c7b.js" crossorigin="anonymous"></script>
    <title>Errores</title>
    <style>
        body{		
	        background-color: gainsboro;
		}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin-bottom: 2%;">
        <div style="padding: 2%; margin-bottom: 1%;">
            <asp:Label ID="lblTituloError" runat="server" Text="Se produjo un error inesperado" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            <br /><br />
            <asp:Label runat="server" Text="" ID="lblError" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <hr />
            <asp:LinkButton ID="lnkVolver" runat="server" OnClick="lnkVolver_Click" Font-Size="Large">Volver al inicio</asp:LinkButton>
        </div>
    </form>
</body>
</html>
