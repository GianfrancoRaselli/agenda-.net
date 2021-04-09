<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmarCorreo.aspx.cs" Inherits="UI.Web.ConfirmarCorreo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maxium-scale=1.0, minimum-scale=1.0"/>
    <link href="style/bootstrap.min.css" rel="stylesheet" />
	<script src="https://kit.fontawesome.com/5520773c7b.js" crossorigin="anonymous"></script>
    <title>Confirmar Correo</title>
	<style>
        @media (min-width: 991.5px) {
            .login {
                width: 35%;
            }
		}

        @media (max-width: 991.5px) and (min-width: 600px) {
            .login {
                width: 68%;
            }
		}

		@media (max-width: 600px) {
            .login {
                width: 94%;
            }
		}

        .modalContainer {
            display: block;
            position: absolute;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-repeat: repeat;
            background-size: auto;
        }

		.login {
             font-family: Arial;
             margin: 2% auto;
		 }

        .login * {
            box-sizing: border-box;
        }

        .login *:focus {
            outline: none;
        }

        .login-screen {
            background-color: lightgrey;
            padding: 5%;
            border-radius: 5px;
        }

        .app-title {
            text-align: center;
            color: black;
        }

        .login-form {
            text-align: center;
        }

        .login .control-group {
            margin-bottom: 2%;
        }

        .login input {
            text-align: center;
            background-color: ghostwhite;
            border: 2px solid transparent;
            border-radius: 3px;
            padding: 3% 0;
            width: 100%;
            transition: border .5s;
        }

        .login input:focus {
            border: 2px solid #3498DB;
            box-shadow: none;
        }

        .login .btn {
            border: 2px solid transparent;
            background: #3498DB;
            color: #ffffff;
            padding: 10px 0;
            text-decoration: none;
            text-shadow: none;
            border-radius: 3px;
            box-shadow: none;
            transition: 0.25s;
            display: block;
            width: 100%;
            margin: 0 auto;
        }

        .login .btn:hover {
            background-color: #2980B9;
        }
	</style>
</head>
<body background="img/Agenda.jpg" class="modalContainer">
	<form id="form1" runat="server" style="margin-bottom: 2%;">
		<asp:Panel runat="server" ID="panelConfirmarCorreo" style="margin-bottom: 1%;">
			<div class="login">
				<div class="login-screen">
					<div class="app-title" style="margin-bottom: 1%;">
						<h1 style="font-weight: bold;">Confirmar Correo</h1>
					</div>
		
					<div class="login-form">
                        <div id="divCodigo" runat="server" visible="true">
						    <div class="control-group">
                                <div style="font-size: large; margin-bottom: 1.5%;"><i class="fas fa-exclamation-triangle"></i>&nbsp;<asp:Label ID="lblAvdertencia" runat="server" Text="Advertencia: tiene un intento para ingresar el código enviado a su correo"></asp:Label></div>
							    <asp:TextBox ID="txtCodigo" runat="server" class="login-field" placeholder="Código" required="true" Font-Size="Large"></asp:TextBox>
						    </div>
						
						    <asp:Button ID="btnConfirmarCodigo" runat="server" Text="Confirmar" class="btn btn-primary btn-large btn-block" Font-Size="Large" OnClick="btnConfirmarCodigo_Click"/>
                        </div>

                        <div id="divCodigoIncorrecto" runat="server" visible="false">
						    <div class="control-group">
                                <asp:Label ID="lblCodigoIncorrecto" runat="server" Font-Size="Large" Text="El código ingresado es incorrecto" style="color: red;"></asp:Label>
						    </div>
						
						    <asp:Button ID="btnGenerarNuevoCodigo" runat="server" Text="Generar Nuevo Código" class="btn btn-primary btn-large btn-block" Font-Size="Large" OnClick="reenviarCodigo_Click"/>
                        </div>

                        <div id="divCambioCorreo" runat="server" visible="false">
                            <div class="control-group">
                                <asp:Label ID="lblCambioCorreo" runat="server" Text="Ingresa un nuevo email" Font-Size="X-Large"></asp:Label>
							    <asp:TextBox ID="txtEmail" runat="server" class="login-field" placeholder="Email" required="true" Font-Size="Large"></asp:TextBox>
                                <asp:Label ID="lblErrorEmail" runat="server" Text="" style="color: red; display: none;"></asp:Label>
                            </div>

                            <asp:Button ID="btnConfirmarCorreo" runat="server" Text="Confirmar" class="btn btn-primary btn-large btn-block" Font-Size="Large" OnClick="btnConfirmarCorreo_Click"/>
                        </div>

                        <div style="margin-top: 2%;" runat="server" id="divVolverAtras" visible="false"><asp:LinkButton ID="lnkVolverAtras" runat="server" Text="Volver Atrás" type="buttom" Font-Size="Large" OnClick="lnkVolverAtras_Click"></asp:LinkButton></div>
                        <div style="margin-top: 2%;" runat="server" id="divReenviarCodigo" visible="true"><asp:LinkButton ID="lnkReenviarCodigo" runat="server" Text="Reenviar Código" type="buttom" OnClick="reenviarCodigo_Click" Font-Size="Large"></asp:LinkButton></div>
                        <div style="margin-top: 3%;" runat="server" id="divCambiarCorreo" visible="true"><asp:LinkButton ID="lnkCambiarCorreo" runat="server" Text="Cambiar Correo" type="buttom" Font-Size="Large" OnClick="lnkCambiarCorreo_Click"></asp:LinkButton></div>
                        <div style="margin-top: 4%;"><asp:LinkButton ID="lnkVolver" runat="server" Text="Volver al Inicio" type="buttom" OnClick="lnkVolver_Click" Font-Size="Large"></asp:LinkButton></div>
					</div>
				</div>
			</div>
		</asp:Panel>
	</form>

    <script type="text/javascript" src="js/jquery-3.5.1.min.js"></script>
	<script type="text/javascript" src="js/popper.min.js"></script>
	<script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript">
    </script>
</body>
</html>
