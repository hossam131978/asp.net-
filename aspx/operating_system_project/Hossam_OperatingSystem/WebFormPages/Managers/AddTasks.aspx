<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTasks.aspx.cs" Inherits="WebFormPages_Managers_AddTasks" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="~/css/HeadMaster_css.css" rel="stylesheet" />--%>
    <link href="~/css/Managers_css.css" rel="stylesheet" />

  


</head>
<body>
    <form id="form_AddTask" runat="server" onkeyup="checkAllTextboxes();checkAllTextAreas();">
        <div>
            <div id="div_addEmployees">
                <table>
                    <thead>
                        <tr>
                            <td colspan="2" class="auto-style1"><a href="Managers.aspx">Go Back To Managers Page</a></td>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            <asp:Label ID="LabelTaskId" runat="server" Text="Label">Task Id</asp:Label>

                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="TextBoxTaskId" onkeyup="isnumber(this.value,this.id);" onkeydown="isnumber(this.value,this.id);" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                   
                    <tr>
                        <td>
                            <asp:Label ID="EmployeeId" runat="server" Text="Employee Id "></asp:Label>

                        </td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="DropDownListEmployeeId" runat="server"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Fulfillment" runat="server" Text="Task Fulfillment Time "></asp:Label>

                        </td>
                        <td >
                            <asp:TextBox ID="TextBoxTaskFulfillmentTime" onkeyup="isnumber(this.value,this.id);" onkeydown="isnumber(this.value,this.id);" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Priority" runat="server" Text="Task Priority"></asp:Label>

                        </td>
                        <td >
                           <asp:DropDownList ID="DropDownListTaskPriority" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Description" runat="server" Text="Task Description"></asp:Label>

                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="TextBoxTaskDescription" runat="server" Rows="5"   TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tbody>
                        <tr>
                            <td colspan="2" id="tdbutton">
                                <asp:Button ID="ButtonSubmit" OnClientClick="if (!confirmation()) return false;" runat="server" Text="Add Task" OnClick="ButtonSubmit_Click" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>

    </form>
    <script src="../../java%20script/JavaScript.js"></script>
    </body>
</html>
