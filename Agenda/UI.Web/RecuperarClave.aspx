<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarClave.aspx.cs" Inherits="UI.Web.RecuperarClave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maxium-scale=1.0, minimum-scale=1.0"/>
    <link href="style/bootstrap.min.css" rel="stylesheet" />
	<script src="https://kit.fontawesome.com/5520773c7b.js" crossorigin="anonymous"></script>
    <title>Recuperar Contraseña</title>
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
		<asp:Panel runat="server" ID="panelRecuperarClave" style="margin-bottom: 1%;">
			<div class="login">
				<div class="login-screen">
					<div class="app-title" style="margin-bottom: 1%;">
						<h1 style="font-weight: bold;">Recuperar Contraseña</h1>
					</div>
		
					<div class="login-form">
                        <div id="divNombreUsuario" runat="server">
						    <div class="control-group">
							    <asp:TextBox ID="txtNombreUsuario" runat="server" class="login-field" placeholder="Nombre de usuario" required="true" Font-Size="Large"></asp:TextBox>
							    <asp:Label ID="lblErrorNombreUsuario" runat="server" Text="Ingresa un nombre de usuario válido" Visible="false" class="login-field-icon fui-user" style="color: red;" Font-Size="Medium"></asp:Label>
						    </div>
						
						    <asp:Button ID="btnBuscar" runat="server" Text="Buscar Usuario" OnClick="btnBuscar_Click" class="btn btn-primary btn-large btn-block" Font-Size="Large"/>
                        </div>

                        <div id="divCodigo" runat="server" visible="false">
						    <div class="control-group">
                                <asp:Label ID="lblAvdertencia" runat="server" Font-Size="Large" style="margin-bottom: 1.5%;"><i class="fas fa-exclamation-triangle"></i>&nbsp;Advertencia: tiene un intento para ingresar el código enviado a su correo</asp:Label>
							    <asp:TextBox ID="txtCodigo" runat="server" class="login-field" placeholder="Código" required="true" Font-Size="Large"></asp:TextBox>
						    </div>
						
						    <asp:Button ID="btnConfirmarCodigo" runat="server" Text="Confirmar" class="btn btn-primary btn-large btn-block" Font-Size="Large" OnClick="btnConfirmarCodigo_Click"/>
                        </div>

                        <div id="divCambiarClave" runat="server" visible="false">
                            <div class="control-group">
                                <asp:Label ID="lblCambiarClave" runat="server" Text="Ingresa una nueva contraseña" Font-Size="X-Large"></asp:Label>
							    <asp:TextBox ID="txtClave" runat="server" class="login-field" placeholder="Contraseña" required="true" type="password" Font-Size="Large"></asp:TextBox>
                                <asp:Label ID="lblErrorClave" runat="server" Text="" style="color: red; display: none;"></asp:Label>
                                <div style="margin-top: 1.8%;"><input type="checkbox" id="mostrarClaveCheckBox" style="width: auto; height: 16px; width: 16px;"/>&nbsp;<asp:Label ID="lblMostrarClave" runat="server" Text="Mostrar Contraseña"></asp:Label></div>
                            </div>

						    <asp:Button ID="btnConfirmarClave" runat="server" Text="Confirmar" style="margin-top: 1%;" class="btn btn-primary btn-large btn-block" Font-Size="Large" OnClientClick="return verificarClave();" OnClick="btnConfirmarClave_Click"/>
                        </div>

                        <div id="divCodigoIncorrecto" runat="server" visible="false">
						    <div class="control-group">
                                <asp:Label ID="lblCodigoIncorrecto" runat="server" Font-Size="Large" Text="El código ingresado es incorrecto" style="color: red;"></asp:Label>
						    </div>
						
						    <asp:Button ID="btnGenerarNuevoCodigo" runat="server" Text="Generar Nuevo Código" class="btn btn-primary btn-large btn-block" Font-Size="Large" OnClick="reenviarCodigo_Click"/>
                        </div>

                        <div style="margin-top: 2%;" runat="server" id="divReenviarCodigo" visible="false"><asp:LinkButton ID="lnkReenviarCodigo" runat="server" Text="Reenviar Código" type="buttom" OnClick="reenviarCodigo_Click" Font-Size="Large"></asp:LinkButton></div>
                        <div style="margin-top: 3%;" runat="server" id="divVolver" visible="false"><asp:LinkButton ID="lnkVolver" runat="server" Text="Volver Atrás" type="buttom" OnClick="lnkVolver_Click" Font-Size="Large"></asp:LinkButton></div>
                        <div style="margin-top: 4%;"><asp:LinkButton ID="lnkVolverInicioSesion" runat="server" Text="Volver a Inicio de Sesión" type="buttom" OnClick="lnkVolverInicioSesion_Click" Font-Size="Large"></asp:LinkButton></div>
					</div>

                    <div style="color: red; margin-top: 6%; text-align: center;" runat="server" id="divError" visible="false">
				        <asp:Label ID="lblError" runat="server" Text="" Font-Size="Large"></asp:Label>
				    </div>
				</div>
			</div>
		</asp:Panel>
	</form>

    <script type="text/javascript" src="js/jquery-3.5.1.min.js"></script>
	<script type="text/javascript" src="js/popper.min.js"></script>
	<script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function verificarClave() {
            const txtClave = document.getElementById('<%=txtClave.ClientID %>');
            const lblErrorClave = document.getElementById('<%=lblErrorClave.ClientID %>');

            if (txtClave.value != "" && txtClave.value.length >= 8) {
                lblErrorClave.style.display = "none";
                return true;
            } else if (txtClave.value == "") {
                lblErrorClave.innerText = "Completa este campo";
                lblErrorClave.style.display = "block";
                return false;
            } else {
                lblErrorClave.innerText = "Ingresa 8 caracteres mínimamente";
                lblErrorClave.style.display = "block";
                return false;
            }
        }

        $("#mostrarClaveCheckBox").change(function () {
            if (this.checked == true) {
                document.getElementById('<%=txtClave.ClientID %>').type = "text";
            }
            else {
                document.getElementById('<%=txtClave.ClientID %>').type = "password";
            }
        })
    </script>
</body>
</html>