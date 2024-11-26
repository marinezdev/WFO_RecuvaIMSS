<%@ Page Title="" Language="C#" MasterPageFile="~/Utilerias/Site.Master" AutoEventWireup="true" CodeBehind="TramiteProcesar2.aspx.cs" Inherits="ProcesosMetLife.Procesos.MDM.Operador.TramiteProcesar2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <script type="text/javascript" lang="javascript">
        function Confirmar() {
            var resultado = false;
            resultado = confirm('¿Esta seguro que desea continuar?');
            return resultado;
        }
    </script>
    <!-- Modal para rechazar -->
    <div class="modal fade mRechazar" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel5">Rechzar</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Label runat="server" ID="lblTituloRechazar" Visible="true" Text="Motivos de Rechazo"></asp:Label>
                            <asp:TreeView 
                                ID="tvRechazos" 
                                runat="server" 
                                NodeIndent="15"
                                ShowCheckBoxes="All"
                                ShowLines="false"
                                ShowExpandCollapse="false"
                                
                                ToolTip="Motivos de Rechazo del Trámite">

                                <HoverNodeStyle 
                                    Font-Underline="True" 
                                    ForeColor="#6666AA" />
                                
                                <NodeStyle 
                                    Font-Names="Tahoma" 
                                    Font-Size="10pt" 
                                    ForeColor="Black" 
                                    HorizontalPadding="2px"
                                    NodeSpacing="2px" 
                                    VerticalPadding="3px">
                                </NodeStyle>
                                
                                <ParentNodeStyle Font-Bold="False" />
                                
                                <SelectedNodeStyle 
                                    BackColor="#B5B5B5" 
                                    Font-Underline="False" 
                                    HorizontalPadding="2px"
                                    VerticalPadding="3px" />
                            </asp:TreeView>
                            <strong>Observaciones</strong>
                            <asp:TextBox ID="txtObservacionesRechazo" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterMode="ValidChars" TargetControlID="txtObservacionesRechazo" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-/&" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnTramiteRechazar" runat="server" Text="Rechazar Trámite" class="btn btn-danger" OnClick="btnRechazar_Click"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para pausar -->
    <div class="modal fade mPausar" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel6">Pausar</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <strong>Observaciones</strong>
                            <asp:TextBox ID="txtObservacionesPausar" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txtObservacionesPausar" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-/&" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnPausar" runat="server" Text="Pausar Trámite" class="btn btn-warning" OnClick="btnPausar_Click"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="row">
                <div class="col-md-4 col-sm-4 col-xs-12 text-left" >
                    <div class="x_panel">
                        <div class="x_title">
                            <ul class="nav navbar-right panel_toolbox">
                                <li>
                                    <asp:Label runat="server" ID="LabelFlujo" Text="" Font-Bold="True" ></asp:Label>
                                    <br />
                                    <asp:Label runat="server" ID="LabelNombreMesa" Text="" Font-Bold="True" ></asp:Label>
                      	        </li>
                            </ul>
                            <h2><small> Información del trámite.</small> </h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content text-left" style="max-height:800px; min-height:300px">
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <asp:HiddenField ID="hfIdTramite" runat="server" />
                                    <asp:HiddenField ID="hfIdMesa" runat="server" />
                                    <asp:HiddenField ID="hfIdFlujo" runat="server" />
                                    <asp:HiddenField ID="hfArchivo" runat="server" />

                                    <table border="0" style="width: 100%;">
                                        <tr>
                                           <td colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; background-color:#8EBB53; color:black;">
                                                <strong>Folio:</strong>
                                                <asp:Label runat="server" ID="lblFolio" Text="" Font-Bold="true" class="control-label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                                Matricula
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                                <asp:TextBox ID="txtMatricula" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#26B99A; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                                Póliza
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                                <asp:TextBox ID="txtPoliza" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                                Quincena
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                                <asp:TextBox ID="txtQuincena" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#26B99A; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                                Unidad de Pago
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                                <asp:TextBox ID="txtUP" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                                T. Nómina
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                                <asp:TextBox ID="txtTNomina" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#26B99A; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                                T. Movimiento
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                                <asp:TextBox ID="txtTMovimiento" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                                Importe
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                                <asp:TextBox ID="txtImporte" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#26B99A; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                                Póliza Portal
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                                <asp:TextBox ID="txtPolizaPortal" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:left; font-family:'Arial Rounded MT'">
                                            </td>
                                            <td colspan="2" style="background-color:#ddd; color:black; font-size:smaller; text-align:left; vertical-align:central; font-family:'Arial Rounded MT'">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="UploadFiles" runat="server">
                                            <td colspan="3">
                                                <asp:Label ID ="lblArchivoInfo" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false">
                                                </asp:Label>

                                                <ajaxToolkit:AsyncFileUpload 
                                                    ID="AsyncFileUpload1" 
                                                    runat="server" 
                                                    PersistFile="true" 
                                                    ClientIDMode="AutoID" 
                                                    ErrorBackColor="Red" 
                                                    Width="100%" 
                                                    AllowedFileTypes="pdf" 
                                                    MaximumNumberOfFiles="1"
                                                    UploaderStyle="Modern" 
                                                    UploadingBackColor="#CCFFFF"
                                                    CompleteBackColor="LimeGreen"
                                                    ThrobberID="imgLoader"
                                                    />
                                                    <asp:Image ID="imgLoader" runat="server" ImageUrl="~/Imagenes/please-wait-gif-transparent-12.gif"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align:right">
                                                <asp:Button ID="btnCargarArchivo" runat="server" CssClass="btn btn-primary" Text="Cargar Archivo" OnClientClick="return Confirmar();" OnClick="btnCargarArchivo_Click"/>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="3" style="text-align:right">
                                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" class="btn btn-success" OnClick="btnAceptar_Click"/>
                                                
                                                <button
                                                    type="button"
                                                    class="btn btn-warning"
                                                    data-toggle="modal"
                                                    title="Pausar"
                                                    data-target=".mPausar">Pausar
                                                </button>

                                                <button 
                                                    type="button" 
                                                    class="btn btn-danger" 
                                                    data-toggle="modal" 
                                                    title="Rechazar" 
                                                    data-target=".mRechazar">Rechazar
                                                </button>


                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-8 col-sm-8 col-xs-12 text-left">
                    <div class="x_panel">
                        <div class="x_content text-left" style="width: 100%; height:600px; ">
                            <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>