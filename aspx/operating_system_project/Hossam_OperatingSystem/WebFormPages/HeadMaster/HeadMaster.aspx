<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HeadMaster.aspx.cs" Inherits="HeadMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/HeadMaster_css.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" method="get">
        <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>

                <div class="main_div">

                    <div id="header_div">
                        <h2>welcome to the <span id="page-type">head master </span> page </h2>
                        <div>
                            <div><h2> <asp:Label ID="UserName_Label" runat="server" Text="user name"></asp:Label></h2></div><br />
                            <div class="logout"> <a  id="logout" href="../../Login.aspx">Log out</a></div>
                        </div>
                    </div>

                    <div id="Footer_div">

                        <div id="left_div">
                            <ul>
                                <li>
                                    <asp:Button ID="Button_managers" runat="server" Text="get managers" OnClick="Button_managers_Click" />
                                </li>
                                <li>
                                    <asp:Button ID="Button_employees" runat="server" Text="get employees" OnClick="Button_employees_Click" /></li>
                                <li>
                                    <asp:Button ID="Button_employees_by_managers" runat="server" Text="get employees by manager id" OnClick="Button_employees_by_managers_Click" Width="199px" />&nbsp;
                            <asp:TextBox ID="TextBoxManagerId" onfocus="this.value=''" onkeyup="isnumber(this.value,this.id);" onkeydown="isnumber(this.value,this.id);" runat="server">Manager Id</asp:TextBox>

                                </li>
                                <li>
                                    <asp:Button ID="Button_AddManagers" runat="server" Text="Add Manager" OnClick="Button_AddManagers_Click" Width="199px" />&nbsp;
                                </li>
                            </ul>
                        </div>
                        <div id="content_div">
                            <div id="div_gridView">
                                <asp:GridView ID="GridView" runat="server"></asp:GridView>
                            </div>


                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script src="../../java%20script/JavaScript.js"></script>

</body>
</html>
