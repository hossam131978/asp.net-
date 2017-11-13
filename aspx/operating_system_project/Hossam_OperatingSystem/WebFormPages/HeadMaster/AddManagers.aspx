<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddManagers.aspx.cs" Inherits="AddManagers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/HeadMaster_css.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" onkeyup = "check_textboxes();">
        <div>
            <div id="div_addEmployees">
                <table>
                    <thead><tr><td colspan="2"><a href="HeadMaster.aspx">Go To Head Master Page</a></td></tr></thead>
                    <tr>
                        <td>
                            <asp:Label ID="LabelId" runat="server" Text="Label">Manager Id</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxId" onkeyup="isnumber(this.value,this.id);" onkeydown="isnumber(this.value,this.id);" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelName" runat="server" Text="Label">Manager Name</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelField" runat="server" Text="Label">Manager Field</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxField" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelUserName" runat="server" Text="Label">Manager UserName</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:Label ID="LabelPassword" runat="server" Text="Label">Manager Password</asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tbody>
                        <tr>
                            <td colspan="2" id="tdbutton">
                                <asp:Button ID="ButtonSubmit"   OnClientClick="if (!confirmation()) return false;" runat="server" Text="Add Manager" OnClick="ButtonSubmit_Click" />
                            </td>
                        </tr>
                    </tbody>
                     <tfoot>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="ErrorLabel" runat="server"    Text=""></asp:Label>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        
    </form>
<%--    <script src="~/java%20script/JavaScript.js"></script> --%>
 <script src="../../java%20script/JavaScript.js"></script>
   
</body>
</html>
