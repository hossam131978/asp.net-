<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEmployees.aspx.cs" Inherits="WebFormPages_Managers_AddEmployees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/HeadMaster_css.css" rel="stylesheet" />


</head>
<body>
    <form id="form2" runat="server" onkeyup="checkAllTextboxes();">
        <div>
            <div id="div_addEmployees">
                <table>
                    <thead>
                        <tr>
                            <td colspan="2"><a href="Managers.aspx">Go Back To Managers Page</a></td>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            <asp:Label ID="LabelId" runat="server" Text="Label">Employee Id</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxId" onkeyup="isnumber(this.value,this.id);" onkeydown="isnumber(this.value,this.id);" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelName" runat="server" Text="Label">Employee Name</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="LabelUserName" runat="server" Text="Label">Employee UserName</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="LabelPassword" runat="server" Text="Label">Employee Password</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tbody>
                        <tr>
                            <td colspan="2" id="tdbutton">
                                <asp:Button ID="ButtonSubmit" OnClientClick="if (!confirmation()) return false;" runat="server" Text="Add Employee" OnClick="ButtonSubmit_Click" />
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
