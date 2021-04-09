<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UI.Web.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maxium-scale=1.0, minimum-scale=1.0"/>
    <link href="style/bootstrap.min.css" rel="stylesheet" />
	<script src="https://kit.fontawesome.com/5520773c7b.js" crossorigin="anonymous"></script>
    <title>Ingresar - Registrarse</title>
    <style>
        @media (min-width: 991.5px) {
            .navbar {
                position: fixed;
                z-index: 100;
                width: 100%;
                top: 0;
                background-color: lightgrey;
                padding: 1%;
                border-bottom: 2px solid black;
            }

            #panelIngresar {
                display: none;
                padding-top: 120px;
            }

            #panelRegistrarse {
                padding-top: 120px;
                padding-bottom: 2%;
                padding-left: 2%;
                padding-right: 2%;
            }

            #panelRegistrarse .modal-content {
                width: 75%;
                padding: 2%;
            }

            .login {
                width: 38%;
            }
		}

        @media (max-width: 991.5px) and (min-width: 600px) {
            .navbar {
			    display: none;
		    }

            #panelIngresar {
                display: block;
            }

            #panelRegistrarse {
                padding: 3%;
            }

            #panelRegistrarse .modal-content {
                width: 100%;
                padding: 4%;
            }

            .login {
                width: 68%;
            }
		}

		@media (max-width: 600px) {
            .navbar {
			    display: none;
		    }

            #panelIngresar {
                display: block;
            }

            #panelRegistrarse {
                padding: 3%;
            }

            #panelRegistrarse .modal-content {
                width: 100%;
                padding: 4%;
            }

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

        #panelIngresar {
            position: absolute;
            z-index: 2;
            padding-bottom: 1%;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow-x: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.4);
        }

        .label {
            font-size: large;
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

        #panelRegistrarse {
            display: none;
            position: absolute;
            z-index: 2;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow-x: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.4);
        }

        #panelRegistrarse .modal-content {
            background-color: lightgrey;
            margin: auto;
            border: 1px solid lightgray;
        }
    </style>
</head>
<body background="img/Agenda.jpg" class="modalContainer">
    <form id="form1" runat="server" style="margin-bottom: 2%;">
        <div class="form-inline navbar">
            <div id="titulo">
                <h1><asp:Label runat="server" ID="lblTitulo" Text="Agenda" Font-Names="Microsoft YaHei" Font-Italic="True" Font-Bold="True"></asp:Label></h1>
            </div>
            <div id="botones" style="margin-top: auto; margin-bottom: auto;">
                <asp:Button runat="server" ID="btnIngresar" Text="Ingresar" class="btn btn-primary" CausesValidation="false" OnClick="btnIngresar_Click"/>&nbsp;
                <asp:Button runat="server" ID="btnRegistrarse" Text="Registrarse" class="btn btn-warning" CausesValidation="false" OnClick="btnRegistrarse_Click"/>
            </div>
        </div>

        <asp:Panel runat="server" ID="panelIngresar" DefaultButton="btnLogin" style="margin-bottom: 1%;">
            <div class="login">
	            <div class="login-screen">
		            <div class="app-title" style="margin-bottom: 1%;">
                        <h1 style="font-weight: bold;">Inicio sesión</h1>
			        </div>
		
		            <div class="login-form">
			            <div class="control-group">
                            <asp:TextBox ID="txtUsuario" runat="server" class="login-field" placeholder="Nombre de usuario" Font-Size="Large"></asp:TextBox>
                        </div>
		
				        <div class="control-group">
                            <asp:TextBox ID="txtContrasenia" runat="server" type="password" class="login-field" placeholder="Contraseña" Font-Size="Large"></asp:TextBox>
				        </div>
					
			            <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" class="btn btn-primary btn-large btn-block" CausesValidation="false" OnClick="btnLogin_Click" Font-Size="Large"/>
						
				        <div style="margin-top: 4%;" runat="server"><asp:LinkButton ID="lnkRecordarClave" runat="server" Text="Olvidé mi Clave" type="buttom" CausesValidation="false" OnClick="lnkRecordarClave_Click" Font-Size="Large"></asp:LinkButton></div>

	                    <div style="color: red; margin-top: 4%; text-align: center;">
                            <asp:Label ID="lblErrorInicioSesion" runat="server" Text="" Font-Size="Large"></asp:Label>
		                </div>

                        <div style="margin-top: 7%;"><asp:LinkButton ID="lnkRegistrarse" runat="server" Text="Crear nueva cuenta" type="buttom" CausesValidation="false" OnClick="lnkRegistrarse_Click" Font-Size="Large"></asp:LinkButton></div>
	                </div>
                </div>
	        </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="panelRegistrarse" DefaultButton="btnRegistrarte" style="margin-bottom: 1%;">
            <asp:Panel class="modal-content" id="modalContent" runat="server">
                <div class="app-title" style="margin-bottom: 2.2%;">
                    <h1 style="font-weight: bold;">Crear cuenta</h1>
			    </div>

                <div class="form-group" style="margin-bottom: 1.8%;">
		            <asp:Label ID="nombreUsuarioLabel" runat="server" Text="Nombre Usuario: " Font-Bold="True" class="label"></asp:Label>
                    <asp:TextBox ID="nombreUsuarioTextBox" runat="server" class="form-control"></asp:TextBox>	                    
                    <asp:Label ID="lblErrorNombreUsuario" runat="server" Text="" style="color: red; display: none;"></asp:Label>
  		        </div>

                <div class="form-group" style="margin-bottom: 1.8%">
		            <asp:Label ID="contraseniaLabel" runat="server" Text="Contraseña: " Font-Bold="True" class="label"></asp:Label>
                    <asp:TextBox ID="contraseniaTextBox" runat="server" class="form-control" type="password"></asp:TextBox>
                    <asp:Label ID="lblErrorContrasenia" runat="server" Text="" style="color: red; display: none;"></asp:Label>
                    <div style="margin-top: 0.5%;"><input type="checkbox" id="mostrarClaveCheckBox" style="width: auto; height: 16px; width: 16px;" />&nbsp;<asp:Label ID="lblMostrarClave" runat="server" Text="Mostrar Contraseña"></asp:Label></div>
  		        </div>

                <div class="form-group" style="margin-bottom: 1.8%">
		            <asp:Label ID="nombreApellidoLabel" runat="server" Text="Nombre y Apellido: " Font-Bold="True" class="label"></asp:Label>
                    <asp:TextBox ID="nombreApellidoTextBox" runat="server" class="form-control"></asp:TextBox>
                    <asp:Label ID="lblErrorNombreApellido" runat="server" Text="Completa este campo" style="color: red; display: none;"></asp:Label>
  		        </div>

                <!--<div class="form-group" style="margin-bottom: 1.8%">
		            <asp:Label ID="telefonoLabel" runat="server" Text="Teléfono: " Font-Bold="True" class="label"></asp:Label>
                    <asp:TextBox ID="telefonoTextBox" runat="server" class="form-control"></asp:TextBox>
                    <asp:Label ID="lblErrorTelefono" runat="server" Text="" style="color: red;"></asp:Label>
                </div>-->

                <div class="form-group" style="margin-bottom: 1.8%;">
		            <asp:Label ID="emailLabel" runat="server" Text="Email: " Font-Bold="True" class="label"></asp:Label>
                    <asp:TextBox ID="emailTextBox" runat="server" class="form-control"></asp:TextBox>
                    <asp:Label ID="lblErrorEmail" runat="server" Text="" style="color: red; display: none;"></asp:Label>
                </div>

                <div class="form-group" style="margin-bottom: 2.2%;">
		            <asp:Label ID="zonaHorariaLabel" runat="server" Text="Zona Horaria: " Font-Bold="True" class="label"></asp:Label>
                    <br />
                    <asp:DropDownList ID="dropDownListZonasHorarias" runat="server" Width="100%" class="form-control"></asp:DropDownList>
                    <asp:Label ID="lblErrorZonasHorarias" runat="server" Text="Selecciona una zona horaria" style="color: red; display: none;"></asp:Label>
                </div>

                <div style="color: red; display: none;" id="error">
                    <i class="fas fa-exclamation-triangle">&nbsp;</i><asp:Label ID="lblErrorRegistrarse" runat="server" Text="Completa todos los campos"></asp:Label>
		        </div>
    
                <asp:Button ID="btnRegistrarte" runat="server" Text="Registrarte" style="margin-top: 2.2%;" class="btn btn-success" OnClientClick="return verificarDatosCrearCuenta();" OnClick="btnRegistrarte_Click" Font-Size="Large" />
            
                <div style="margin-top: 3%; margin-left: auto; margin-right: auto;"><asp:LinkButton ID="lnkIngresar" runat="server" Text="¿Ya tienes una cuenta?" type="buttom" CausesValidation="false" OnClick="lnkIngresar_Click" Font-Size="Large"></asp:LinkButton></div>
            </asp:Panel>    
	    </asp:Panel>
    </form>

    <script type="text/javascript" src="js/jquery-3.5.1.min.js"></script>
	<script type="text/javascript" src="js/popper.min.js"></script>
	<script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function verificarDatosCrearCuenta() {
            const nombreUsuarioTextBox = document.getElementById('<%=nombreUsuarioTextBox.ClientID %>');
            const contraseniaTextBox = document.getElementById('<%=contraseniaTextBox.ClientID %>');
            const nombreApellidoTextBox = document.getElementById('<%=nombreApellidoTextBox.ClientID %>');
            const emailTextBox = document.getElementById('<%=emailTextBox.ClientID %>');
            const dropDownListZonasHorarias = document.getElementById('<%=dropDownListZonasHorarias.ClientID %>');
            const lblErrorNombreUsuario = document.getElementById('<%=lblErrorNombreUsuario.ClientID %>');
            const lblErrorContrasenia = document.getElementById('<%=lblErrorContrasenia.ClientID %>');
            const lblErrorNombreApellido = document.getElementById('<%=lblErrorNombreApellido.ClientID %>');
            const lblErrorEmail = document.getElementById('<%=lblErrorEmail.ClientID %>');
            const lblErrorZonasHorarias = document.getElementById('<%=lblErrorZonasHorarias.ClientID %>');
            const error = document.getElementById('error');

            if (nombreUsuarioTextBox.value != null && nombreUsuarioTextBox.value != "" &&
                contraseniaTextBox.value != null && contraseniaTextBox.value != "" && contraseniaTextBox.value.length >= 8 &&
                nombreApellidoTextBox.value != null && nombreApellidoTextBox.value != "" &&
                emailTextBox.value != null && emailTextBox.value != "" && /^\w+([\.\+\-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(emailTextBox.value) &&
                dropDownListZonasHorarias.value != null && dropDownListZonasHorarias.value != "Seleccione su zona horaria") {
                lblErrorNombreUsuario.style.display = "none";
                lblErrorContrasenia.style.display = "none";
                lblErrorNombreApellido.style.display = "none";
                lblErrorEmail.style.display = "none";
                lblErrorZonasHorarias.style.display = "none";
                error.style.display = "none";
                return true;
            } else {
                if (nombreUsuarioTextBox.value != null && nombreUsuarioTextBox.value != "" &&
                    contraseniaTextBox.value != null && contraseniaTextBox.value != "" &&
                    nombreApellidoTextBox.value != null && nombreApellidoTextBox.value != "" &&
                    emailTextBox.value != null && emailTextBox.value != "" &&
                    dropDownListZonasHorarias.value != null && dropDownListZonasHorarias.value != "Seleccione su zona horaria") {
                    error.style.display = "none";
                } else {
                    error.style.display = "block";
                }
                if (nombreUsuarioTextBox.value != null && nombreUsuarioTextBox.value != "") { lblErrorNombreUsuario.style.display = "none"; } else { lblErrorNombreUsuario.innerText = "Completa este campo"; lblErrorNombreUsuario.style.display = "block"; };
                if (contraseniaTextBox.value != null && contraseniaTextBox.value != "") { lblErrorContrasenia.style.display = "none"; if (contraseniaTextBox.value.length < 8) { lblErrorContrasenia.innerText = "Ingresa 8 caracteres mínimamente"; lblErrorContrasenia.style.display = "block"; } } else { lblErrorContrasenia.innerText = "Completa este campo"; lblErrorContrasenia.style.display = "block"; };
                if (nombreApellidoTextBox.value != null && nombreApellidoTextBox.value != "") { lblErrorNombreApellido.style.display = "none"; } else { lblErrorNombreApellido.style.display = "block"; };
                if (emailTextBox.value != null && emailTextBox.value != "") { lblErrorEmail.style.display = "none"; if (!(/^\w+([\.\+\-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(emailTextBox.value))) { lblErrorEmail.innerText = "Ingresa un email válido"; lblErrorEmail.style.display = "block"; } } else { lblErrorEmail.innerText = "Completa este campo"; lblErrorEmail.style.display = "block"; };
                if (dropDownListZonasHorarias.value != null && dropDownListZonasHorarias.value != "Seleccione su zona horaria") { lblErrorZonasHorarias.style.display = "none"; } else { lblErrorZonasHorarias.style.display = "block"; };
                return false;
            }
        }

        $("#mostrarClaveCheckBox").change(function () {
            if (this.checked == true) {
                document.getElementById('<%=contraseniaTextBox.ClientID %>').type = "text";
            }
            else {
                document.getElementById('<%=contraseniaTextBox.ClientID %>').type = "password";
            }
        })
    </script>
</body>
</html>
