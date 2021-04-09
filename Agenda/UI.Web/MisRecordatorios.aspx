<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MisRecordatorios.aspx.cs" Inherits="UI.Web.MisRecordatorios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/jquery-ui.min.css" rel="stylesheet" />
    <link href="style/clockpicker.css" rel="stylesheet" />
    <title>Mis Recordatorios</title>
    <style>
        @media (min-width: 991.5px) {
            .panelAgregarEvento .modal-content {
                padding: 2%;
                width: 60%;
            }

            .card-evento {
                width: 48.6%;
                margin: 0.7%;
                float: left;
            }
		}

		@media (max-width: 991.5px) {
            .panelAgregarEvento .modal-content {
                padding: 3%;
                width: 96%;
            }

            .card-evento {
                width: 100%;
                margin-bottom: 2%;
            }
		}

        .panelEventos {
            padding-top: 0.5%;
            padding-left: 2%;
            padding-right: 2%;
            padding-bottom: 1%;
        }

        .panelAgregarEvento {
            display: block;
            position: fixed;
            z-index: 2;
            padding-top: 80px;
            padding-bottom: 1%;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.5);
        }

        .panelAgregarEvento .modal-content {
            background-color: lightgrey;
            margin: auto;
            border: 1px solid lightgray;
        }
        
        input[type="checkbox"] {
            position: relative;
            width: 50px;
            height: 24px;
            -webkit-appearance: none;
            background: #c6c6c6;
            outline: none;
            border-radius: 20px;
            box-shadow: inset 0 0 5px rgba(0,0,0,.2);
            transition: .5s;
        }

        input:checked[type="checkbox"] {
            background: #03a9f4;
        }

        input[type="checkbox"]:before {
            content: '';
            position: absolute;
            width: 25px;
            height: 24px;
            border-radius: 20px;
            top: 0;
            left: 0;
            background: #fff;
            transform: scale(1.1);
            box-shadow: 0 2px 5px rgba(0,0,0,.2);
            transition: .5s;
        }

        input:checked[type="checkbox"]:before {
            left: 25px;
        }

        .link {
            background-color: none;
        }

        .link:focus {
            outline: none;
            box-shadow: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="panelAgregarEvento" runat="server" class="panelAgregarEvento" DefaultButton="btnAgregarEvento" Visible="false">
        <asp:Panel class="modal-content" id="modalContent" runat="server">
            <div style="display: inline-flex; margin-bottom: 2%;">
                <div style="text-align: center; width: 100%;" runat="server"><asp:Label ID="lblAgregarEvento" runat="server" Text="Agregar evento" Font-Bold="True" Font-Underline="True" Font-Size="X-Large"></asp:Label></div>
                <div style="margin-left: auto;"><asp:LinkButton runat="server" ID="lnkCerrarEvento" OnClick="lnkCerrarEvento_Click" CausesValidation="false" style="color: red; font-size: x-large;"><i class="fas fa-times"></i></asp:LinkButton></div>
            </div>

            <input ID="idEventoTextBox" runat="server" visible="false"/>

            <div class="form-group" style="margin-bottom: 0.5%;">
		        <asp:Label ID="nombreLabel" runat="server" Text="Nombre evento: " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <asp:TextBox ID="nombreTextBox" runat="server" class="form-control"></asp:TextBox>	                    
                <asp:Label ID="lblErrorNombre" runat="server" Text="Completa este campo" style="color: red; display: none;"></asp:Label>
                <br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%;">
		        <asp:Label ID="descripcionLabel" runat="server" Text="Descripción: " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <textarea ID="descripcionTextArea" runat="server" class="form-control" cols="20" rows="3"></textarea>                 
                <asp:Label ID="lblErrorDescripcion" runat="server" Text="Completa este campo" style="color: red; display: none;"></asp:Label>
                <br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%;">
		        <asp:Label ID="colorLabel" runat="server" Text="Color: " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <asp:DropDownList ID="colorDropDownList" runat="server" Width="100%" class="form-control"></asp:DropDownList>
                <asp:Label ID="lblErrorColor" runat="server" Text="Selecciona un color" style="color: red; display: none;"></asp:Label>
                <br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%;">
		        <asp:Label ID="zonaHorariaLabel" runat="server" Text="Zona Horaria: " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <asp:DropDownList ID="zonaHorariaDropDownList" runat="server" Width="100%" class="form-control"></asp:DropDownList>
                <asp:Label ID="lblErrorZonaHoraria" runat="server" Text="Selecciona una zona horaria" style="color: red; display: none;"></asp:Label>
                <br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%;">
		        <asp:Label ID="todoElDiaLabel" runat="server" Text="Todo el día " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <input type="checkbox" id="todoElDiaCheckBox" runat="server"/>
                <br /><br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%;">
		        <asp:Label ID="fechaEventoLabel" runat="server" Text="Fecha evento: " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <input type="text" id="fechaEventoTextBox" name="fechaEventoTextBox" runat="server" class="form-control calendario" readonly/>         
                <asp:Label ID="lblErrorFechaEvento" runat="server" Text="Selecciona una fecha" style="color: red; display: none;"></asp:Label>
                <br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%;" ID="divHoraEvento" runat="server">
		        <asp:Label ID="horaEventoLabel" runat="server" Text="Hora evento: " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <input type="text" id="horaEventoTextBox" name="horaEventoTextBox" runat="server" class="form-control reloj" readonly/>
                <asp:Label ID="lblErrorHoraEvento" runat="server" Text="Selecciona una hora" style="color: red; display: none;"></asp:Label>
                <br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%;">
                <asp:Label ID="recordatorioLabel" runat="server" Text="Recordatorio " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <input type="checkbox" id="recordatorioCheckBox" runat="server"/>
                <br /><br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%; display: none;" ID="divFechaRecordatorio" runat="server">
		        <asp:Label ID="fechaRecordatorioLabel" runat="server" Text="Fecha recordatorio: " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <input type="text" id="fechaRecordatorioTextBox" name="fechaRecordatorioTextBox" runat="server" class="form-control calendario" readonly/>  
                <asp:Label ID="lblErrorFechaRecordatorio" runat="server" Text="Selecciona una fecha" style="color: red; display: none;"></asp:Label>
                <br />
  		    </div>

            <div class="form-group" style="margin-bottom: 0.5%; display: none;" ID="divHoraRecordatorio" runat="server">
		        <asp:Label ID="horaRecordatorioLabel" runat="server" Text="Hora recordatorio: " Font-Bold="True" Font-Size="Larger"></asp:Label>
                <input type="text" id="horaRecordatorioTextBox" name="horaRecordatorioTextBox" runat="server" class="form-control reloj" readonly/>
                <asp:Label ID="lblErrorHoraRecordatorio" runat="server" Text="Selecciona una hora" style="color: red; display: none;"></asp:Label>
                <br />
  		    </div>
            
            <div style="color: red; display: none;" id="error">
                <i class="fas fa-exclamation-triangle">&nbsp;</i><asp:Label ID="lblErrorAgregarEvento" runat="server" Text="Completa todos los campos"></asp:Label>
		    </div>

            <div style="margin-top: 1.5%; display: inline-flex; margin-left: auto; margin-right: auto; width: 100%;">
                <asp:Button ID="btnAgregarEvento" runat="server" Text="Agregar evento" class="btn btn-success" Width="49%" OnClientClick="return verificarDatosAgregarEvento();" OnClick="btnAgregarEvento_Click"/>
                <asp:Button ID="btnEditarEvento" runat="server" Text="Aceptar" class="btn btn-primary" Visible="true" Width="49%" OnClientClick="return verificarDatosAgregarEvento();" OnClick="btnEditarEvento_Click"/>
                <div style="width: 2%;"></div>
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-danger" Width="49%" CausesValidation="false" OnClick="btnCancelar_Click"/>
            </div>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel runat="server" class="panelEventos" style="margin-bottom: 0.8%;">
        <asp:Label ID="lblEventos" runat="server" Visible="false" Font-Size="Large" Width="100%"><i class="fas fa-angle-right"></i>&nbsp;No hay eventos registrados</asp:Label>

        <nav aria-label="Page navigation example" runat="server" ID="paginacion1">
            <ul class="pagination justify-content-center">
                <asp:Repeater runat="server" ID="rptPaginacion1" ItemType="Business.Util.NumeroPaginacion">
                    <ItemTemplate>
                        <li class="page-item active" style="z-index: 1;" runat="server" visible="<%# Item.Actual %>"><a class="page-link" href="MisRecordatorios.aspx?valor=<%# Item.Numero %>"><%# Item.Numero %></a></li>
                        <li class="page-item" runat="server" visible="<%# !Item.Actual %>"><a class="page-link" href="MisRecordatorios.aspx?valor=<%# Item.Numero %>"><%# Item.Numero %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </nav>

        <asp:Repeater runat="server" ID="rptEventos" ItemType="Business.Entities.Evento" Visible="false">
            <ItemTemplate>
                <div class="card card-evento" style="background-color: <%# Item.color.codigo_hex %>;">
                    <h5 class="card-header"><asp:Label ID="lblFecha" runat="server" Text="<%# Item.fecha_evento_completa_string %>"></asp:Label>&nbsp;<asp:Label ID="lblHora" runat="server" Text="<%# Item.hora_evento_string %>" Visible="<%# !Item.todo_el_dia %>"></asp:Label></h5>
                    <div class="card-body">
                        <h5 class="card-title"><%# Item.nombre %></h5>
                        <p class="card-text"><%# Item.descripcion %></p>
                        <p class="card-text">Recordatorio:&nbsp;<asp:Label ID="lblSinRecordatorio" runat="server" Text="No" Visible="<%# !Item.recordatorio %>"></asp:Label><asp:Label ID="lblConRecordatorio" runat="server" Text="<%# Item.fecha_hora_recordatorio_string %>" Visible="<%# Item.recordatorio %>"></asp:Label></p>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandArgument="<%# Item.id_evento %>" class="btn btn-primary" onclick="btnEditar_Click" />&nbsp;
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandArgument="<%# Item.id_evento %>" class="btn btn-danger" onclick="btnEliminar_Click" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Label ID="lblZonaHoraria" runat="server" Visible="false" Font-Italic="True" Width="100%"></asp:Label>

        <nav aria-label="Page navigation example" runat="server" ID="paginacion2" style="margin-top: 1.5%;">
            <ul class="pagination justify-content-center">
                <asp:Repeater runat="server" ID="rptPaginacion2" ItemType="Business.Util.NumeroPaginacion">
                    <ItemTemplate>
                        <li class="page-item active" style="z-index: 1;" runat="server" visible="<%# Item.Actual %>"><a class="page-link" href="MisRecordatorios.aspx?valor=<%# Item.Numero %>"><%# Item.Numero %></a></li>
                        <li class="page-item" runat="server" visible="<%# !Item.Actual %>"><a class="page-link" href="MisRecordatorios.aspx?valor=<%# Item.Numero %>"><%# Item.Numero %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </nav>
    </asp:Panel>

    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/datepicker-es.js"></script>
    <script src="js/clockpicker.js"></script>
    <script type="text/javascript">
        function verificarDatosAgregarEvento() {
            const nombreTextBox = document.getElementById('<%=nombreTextBox.ClientID %>');
            const descripcionTextArea = document.getElementById('<%=descripcionTextArea.ClientID %>');
            const todoElDiaCheckBox = document.getElementById('<%=todoElDiaCheckBox.ClientID %>');
            const fechaEventoTextBox = document.getElementById('<%=fechaEventoTextBox.ClientID %>');
            const horaEventoTextBox = document.getElementById('<%=horaEventoTextBox.ClientID %>');
            const recordatorioCheckBox = document.getElementById('<%=recordatorioCheckBox.ClientID %>');
            const fechaRecordatorioTextBox = document.getElementById('<%=fechaRecordatorioTextBox.ClientID %>');
            const horaRecordatorioTextBox = document.getElementById('<%=horaRecordatorioTextBox.ClientID %>');
            const lblErrorNombre = document.getElementById('<%=lblErrorNombre.ClientID %>');
            const lblErrorDescripcion = document.getElementById('<%=lblErrorDescripcion.ClientID %>');
            const lblErrorFechaEvento = document.getElementById('<%=lblErrorFechaEvento.ClientID %>');
            const lblErrorHoraEvento = document.getElementById('<%=lblErrorHoraEvento.ClientID %>');
            const lblErrorFechaRecordatorio = document.getElementById('<%=lblErrorFechaRecordatorio.ClientID %>');
            const lblErrorHoraRecordatorio = document.getElementById('<%=lblErrorHoraRecordatorio.ClientID %>');
            const error = document.getElementById('error');

            if (nombreTextBox.value != null && nombreTextBox.value != "" &&
                descripcionTextArea.value != null && descripcionTextArea.value != "" &&
                fechaEventoTextBox.value != null && fechaEventoTextBox.value != "" &&
                (todoElDiaCheckBox.checked == true || (todoElDiaCheckBox.checked == false && horaEventoTextBox.value != null && horaEventoTextBox.value != "")) &&
                (recordatorioCheckBox.checked == false || (recordatorioCheckBox.checked == true && fechaRecordatorioTextBox.value != null && fechaRecordatorioTextBox.value != "" && horaRecordatorioTextBox.value != null && horaRecordatorioTextBox.value != ""))) {
                lblErrorNombre.style.display = "none";
                lblErrorDescripcion.style.display = "none";
                lblErrorFechaEvento.style.display = "none";
                lblErrorHoraEvento.style.display = "none";
                lblErrorFechaRecordatorio.style.display = "none";
                lblErrorHoraRecordatorio.style.display = "none";
                error.style.display = "none";
                return true;
            } else {
                error.style.display = "block";
                if (nombreTextBox.value != null && nombreTextBox.value != "") { lblErrorNombre.style.display = "none"; } else { lblErrorNombre.style.display = "block"; };
                if (descripcionTextArea.value != null && descripcionTextArea.value != "") { lblErrorDescripcion.style.display = "none"; } else { lblErrorDescripcion.style.display = "block"; };
                if (fechaEventoTextBox.value != null && fechaEventoTextBox.value != "") { lblErrorFechaEvento.style.display = "none"; } else { lblErrorFechaEvento.style.display = "block"; };
                if (todoElDiaCheckBox.checked == true || (todoElDiaCheckBox.checked == false && horaEventoTextBox.value != null && horaEventoTextBox.value != "")) { lblErrorHoraEvento.style.display = "none"; } else { lblErrorHoraEvento.style.display = "block"; };
                if (recordatorioCheckBox.checked == false || (recordatorioCheckBox.checked == true && fechaRecordatorioTextBox.value != null && fechaRecordatorioTextBox.value != "")) { lblErrorFechaRecordatorio.style.display = "none"; } else { lblErrorFechaRecordatorio.style.display = "block"; };
                if (recordatorioCheckBox.checked == false || (recordatorioCheckBox.checked == true && horaRecordatorioTextBox.value != null && horaRecordatorioTextBox.value != "")) { lblErrorHoraRecordatorio.style.display = "none"; } else { lblErrorHoraRecordatorio.style.display = "block"; };
                return false;
            }
        }

        $("#<%=todoElDiaCheckBox.ClientID %>").change(function () {
            if (this.checked == true) {
                document.getElementById('<%=divHoraEvento.ClientID %>').style.display = "none";
            }
            else {
                document.getElementById('<%=divHoraEvento.ClientID %>').style.display = "block";
            }
        })

        $("#<%=recordatorioCheckBox.ClientID %>").change(function () {
            if (this.checked == true) {
                document.getElementById('<%=divFechaRecordatorio.ClientID %>').style.display = "block";
                document.getElementById('<%=divHoraRecordatorio.ClientID %>').style.display = "block";
            }
            else {
                document.getElementById('<%=divFechaRecordatorio.ClientID %>').style.display = "none";
                document.getElementById('<%=divHoraRecordatorio.ClientID %>').style.display = "none";
            }
        })

        $(".calendario").datepicker($.datepicker.regional["es"]);

        $('.reloj').clockpicker({
            donetext: 'Listo',
            placement: 'top',
        });
    </script>
</asp:Content>
