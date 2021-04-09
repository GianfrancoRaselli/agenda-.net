<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="UI.Web.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Perfil</title>
    <style type="text/css">
        @media (min-width: 991.5px) {
            .alerta{
                width: 40%;
            }

            .panelInfo {
                margin-top: 5%;
                margin-bottom: 3%;
                padding-left: 25%;
                padding-right: 25%;
            }
		}

		@media (max-width: 991.5px) {
            .alerta{
                width: 80%;
            }

            .panelInfo{
                margin-top: 5%;
                margin-bottom: 2%;
                padding-left: 2%;
                padding-right: 2%;
            }
		}

        .alerta{
            top: auto; 
            bottom: 0; 
            right: 0; 
            left: auto; 
            position: fixed; 
            z-index: 3;
            height: auto;
            color: white
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" style="margin-bottom: 2%;">
    <div class="alert alert-dismissible fade show alerta" role="alert" id="alerta" runat="server" visible="false">
  	    <h5 id='textoAlerta' runat="server"></h5>
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>

    <asp:Panel runat="server" class="panelInfo" style="margin-bottom: 2%;">
        <div style="text-align: center;"><asp:Label ID="lblTitulo" runat="server" Text="Información Personal" Font-Underline="True" Font-Size="XX-Large" Font-Bold="True"></asp:Label></div>
        <br />

        <div style="display: inline-flex; width: 100%;">
            <div><asp:Label ID="lbl1" runat="server" Text="Nombre Usuario" Font-Bold="True" class="label"></asp:Label></div>
            <div style="margin-right: 1.5%; margin-left:1.5%;">
                <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" Visible="false" class="form-control form-control-sm"></asp:TextBox>
            </div>
            <div style="width: auto; margin-left: auto;">
                <asp:LinkButton ID="LinkButtonEditarNombreUsuario" runat="server" Font-Size="Small" OnClick="LinkButtonEditar_Click">Editar</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonGuardarNombreUsuario" runat="server" Visible="false" OnClick="LinkButtonGuardar_Click">Guardar</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonCancelarNombreUsuario" runat="server" Visible="false" OnClick="LinkButtonCancelar_Click">Cancelar</asp:LinkButton>
            </div>
        </div>
        <hr />

        <div style="display: inline-flex; width: 100%;">
            <div><asp:Label ID="lbl2" runat="server" Text="Contraseña" Font-Bold="True" class="label"></asp:Label></div>
            <div style="margin-right: 1.5%; margin-left:1.5%;">
                <asp:Label ID="lblClave" runat="server"></asp:Label>
                <asp:TextBox ID="txtClave" runat="server" Visible="false" type="password" class="form-control form-control-sm"></asp:TextBox>
            </div>
            <div style="width: auto; margin-left: auto;">
                <asp:LinkButton ID="LinkButtonEditarClave" runat="server" Font-Size="Small" OnClick="LinkButtonEditar_Click">Editar</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonGuardarClave" runat="server" Visible="false" OnClick="LinkButtonGuardar_Click">Guardar</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonCancelarClave" runat="server" Visible="false" OnClick="LinkButtonCancelar_Click">Cancelar</asp:LinkButton>
            </div>
        </div>
        <hr />

        <div style="display: inline-flex; width: 100%;">
            <div><asp:Label ID="lbl3" runat="server" Text="Nombre y Apellido" Font-Bold="True" class="label"></asp:Label></div>
            <div style="margin-right: 1.5%; margin-left:1.5%;">
                <asp:Label ID="lblNombreApellido" runat="server"></asp:Label>
                <asp:TextBox ID="txtNombreApellido" runat="server" Visible="false" class="form-control form-control-sm"></asp:TextBox>
            </div>
            <div style="width: auto; margin-left: auto;">
                <asp:LinkButton ID="LinkButtonEditarNombreApellido" runat="server" Font-Size="Small" OnClick="LinkButtonEditar_Click">Editar</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonGuardarNombreApellido" runat="server" Visible="false" OnClick="LinkButtonGuardar_Click">Guardar</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonCancelarNombreApellido" runat="server" Visible="false" OnClick="LinkButtonCancelar_Click">Cancelar</asp:LinkButton>
            </div>
        </div>
        <hr />

        <div style="display: inline-flex; width: 100%;">
            <div><asp:Label ID="lbl4" runat="server" Text="Teléfono" Font-Bold="True" class="label"></asp:Label></div>
            <div style="margin-right: 1.5%; margin-left:1.5%;">
                <asp:Label ID="lblTelefono" runat="server"></asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" Visible="false" class="form-control form-control-sm"></asp:TextBox>
            </div>
            <div style="width: auto; margin-left: auto;">
                <asp:LinkButton ID="LinkButtonEditarTelefono" runat="server" Font-Size="Small" OnClick="LinkButtonEditar_Click">Editar</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonGuardarTelefono" runat="server" Visible="false" OnClick="LinkButtonGuardar_Click">Guardar</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonCancelarTelefono" runat="server" Visible="false" OnClick="LinkButtonCancelar_Click">Cancelar</asp:LinkButton>
            </div>
        </div>
        <hr />

        <div style="display: inline-flex; width: 100%;">
            <div><asp:Label ID="lbl5" runat="server" Text="Email" Font-Bold="True" class="label"></asp:Label></div>
            <div style="margin-right: 1.5%; margin-left:1.5%;">
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" Visible="false" class="form-control form-control-sm"></asp:TextBox>
            </div>
            <div style="width: auto; margin-left: auto;">
                <asp:LinkButton ID="LinkButtonEditarEmail" runat="server" Font-Size="Small" OnClick="LinkButtonEditar_Click">Editar</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonGuardarEmail" runat="server" Visible="false" OnClick="LinkButtonGuardar_Click">Guardar</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonCancelarEmail" runat="server" Visible="false" OnClick="LinkButtonCancelar_Click">Cancelar</asp:LinkButton>
            </div>
        </div>
        <hr />

        <div style="display: inline-flex; width: 100%;">
            <div><asp:Label ID="lbl6" runat="server" Text="Zona Horaria" Font-Bold="True" class="label"></asp:Label></div>
            <div style="margin-right: 1.5%; margin-left:1.5%;">
                <asp:Label ID="lblZonaHoraria" runat="server"></asp:Label>
                <asp:DropDownList ID="dropDownListZonasHorarias" runat="server" Visible="false" class="form-control form-control-sm"></asp:DropDownList>
            </div>
            <div style="width: auto; margin-left: auto;">
                <asp:LinkButton ID="LinkButtonEditarZonaHoraria" runat="server" Font-Size="Small" OnClick="LinkButtonEditar_Click">Editar</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonGuardarZonaHoraria" runat="server" Visible="false" OnClick="LinkButtonGuardar_Click">Guardar</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonCancelarZonaHoraria" runat="server" Visible="false" OnClick="LinkButtonCancelar_Click">Cancelar</asp:LinkButton>
            </div>
        </div>

    </asp:Panel>
</asp:Content>
