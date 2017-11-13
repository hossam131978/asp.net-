<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Managers.aspx.cs" Inherits="Managers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/Managers_css.css" rel="stylesheet" />

</head>

<body>
    <form id="form1" runat="server" method="get">
        <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>


               

                <div id="header_div">
                    <h2>welcome to the Managers page </h2>
                    <div>
                        <div>
                            <asp:Label ID="UserName_Label" runat="server" Text="user name"></asp:Label>
                        </div>
                        <br />
                        <div id="ld"><a id="logout" href="../../Login.aspx">Log out</a></div>
                    </div>
                </div>
                 <div id="main_div">
                    <div id="Footer_div">

                        <div id="left_div" runat="server">
                            <ul>

                                <li>
                                    <asp:Button ID="Button_AddEmployee" runat="server" Text="Add Employee" OnClick="Button_AddEmployee_Click" /></li>
                                <li>
                                    <asp:Button ID="Button_GetEmployees" runat="server" Text="get Employees" OnClick="Button_GetEmployees_Click" /></li>
                                <li>
                                    <asp:Button ID="Button_AddTask" runat="server" Text="Add Tasks" OnClick="Button_AddTasks_Click" /></li>
                                <li>
                                    <asp:Button ID="Button_moveTask" runat="server" Text="move Tasks" OnClick="Button_moveTask_Click" /></li>
                                <li>
                                    <asp:Button ID="Button_GetTask" runat="server" Text="Get Tasks" OnClick="Button_GetTask_Click" /></li>
                                <li>
                                    <asp:Button ID="Button_GetTaskBy" runat="server" Text="Get Tasks By" OnClick="Button_GetTaskBy_Click" />
                                    <br />
                                    <asp:DropDownList ID="DropDownListGetTaskBy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListGetTaskBy_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="DropDownListFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListFilter_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </li>

                            </ul>
                            
                        </div>
                        <div id="content_div">
                            <div id="div_gridView">
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateSelectButton="True" OnSorting="GridView1_Sorting" HeaderStyle-Wrap="False" CellPadding="2" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanging" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" ForeColor="Black">
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                    <FooterStyle BackColor="Tan" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" Wrap="True" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#CC9900" ForeColor="DarkSlateBlue" />
                                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                </asp:GridView>


                            </div>


                        </div>
                        <div id="right_div">
                            <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateEditButton="True" Height="50px" OnItemUpdating="DetailsView1_ItemUpdating1" OnModeChanging="DetailsView1_ModeChanging" Width="250px"  BackColor="#CC9900" ForeColor="Black">
                                <EditRowStyle BackColor="#996633" Width="250px" />
                                <Fields>
                                    <asp:TemplateField HeaderText="TaskPriority">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DetailsView1DropDownListTaskPriority" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelTaskPriority" runat="server" Text=""> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DetailsView1DropDownListStatus" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                                <FooterStyle BackColor="#663300" BorderColor="#999966" Font-Bold="True" Font-Italic="True" ForeColor="#000099" />
                                <RowStyle BackColor="#C3A74B" Width="250px" />
                            </asp:DetailsView>
                            <br />
                                <br />
                                <br />

                                <asp:Label ID="labelError" runat="server" Text="">

                                </asp:Label>
                        </div>
                    </div>

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <script src="../../java%20script/JavaScript.js"></script>


    </form>

</body>
</html>
