<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoveTasks.aspx.cs" Inherits="WebFormPages_Managers_MoveTasks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/Managers_css.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 264px;
        }
    </style>
</head>
<body>

    <form id="form_MoveTask" runat="server" onkeyup="checkAllTextboxes();">
        <div>
            <div id="div_addEmployees">
                <table>
                    <thead>
                        <tr>
                            <td colspan="3"><a href="Managers.aspx">Go Back To Managers Page</a></td>
                        </tr>
                    </thead>

                    <tr>
                        <td>
                            <asp:Label ID="LabelSource" runat="server" Text="Label">Move Task from</asp:Label></td>
                        <td>
                            <asp:Label ID="LabelTaskId" runat="server" Text="Label">Task Id</asp:Label>
                            <asp:DropDownList ID="DropDownListTaskId" onchange="changed()"   runat="server"></asp:DropDownList>

                        </td>

                        <td class="auto-style1">
                            <asp:Label ID="EmployeeId" runat="server" Text="Employee Id : "></asp:Label>

                            <asp:Label ID="labelSourceEmployeeId" runat="server" Text=""></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelDestenation" runat="server" Text="Label">Move Task To</asp:Label></td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="To Employee Id "></asp:Label>
                        </td>
                        <td  >
                            <asp:DropDownList ID="DropDownListDestinationEmployeeId" runat="server"></asp:DropDownList>
                        </td>
                    </tr>

                    
                    <tbody>
                        <tr>
                            <td colspan="3" id="tdbutton">
                                <asp:Button ID="ButtonMove" OnClientClick="if (!confirmation('move task')) return false;" runat="server" Text="Move Task" OnClick="ButtonMove_Click" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="ErrorLabel" runat="server" Text="err"></asp:Label>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        <br />
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
         <asp:DropDownList ID="DropDownListTaskEmployeesId" runat="server" style="visibility:hidden;" Enabled="false"></asp:DropDownList>

    </form>
    <script src="../../java%20script/JavaScript.js"></script>
    <script type="text/javascript">
        function changed()
        {
            var index_ddl = document.getElementById("DropDownListTaskId");
            var value_ddl = document.getElementById("DropDownListTaskEmployeesId");
            document.getElementById("labelSourceEmployeeId").innerText = value_ddl.options[index_ddl.selectedIndex].value;
        }
    </script>
</body>
</html>
