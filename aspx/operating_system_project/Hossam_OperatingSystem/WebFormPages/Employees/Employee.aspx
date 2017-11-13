<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/Employees_css.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server" method="get">
        <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>

                <div id="header_div">
                    <h2>welcome to the Employees page </h2>
                    <div>
                        <div>
                            <asp:Label ID="UserName_Label" runat="server" Text="user name"></asp:Label>
                        </div>
                        <br />
                        <div><a id="logout" href="../../Login.aspx">Log out</a></div>
                    </div>
                </div>

                <div id="main_div">
                    <div id="Footer_div">

                        <div id="left_div" runat="server">
                            <ul>
                                <li>
                                    <button runat="server" id="btnRun" onserverclick="Button_GetNewTasks_Click ">
                                        <asp:Button ID="new_tasks_number" runat="server" Text="0"  />
                                        New Tasks
                                    </button>
                                </li>
                                <li>
                                    <asp:Button ID="Button_GetAcceptedTasks" runat="server" Text="Accepted Tasks" ValidateRequestMode="Disabled" OnClick="Button_GetAcceptedTasks_Click" /></li>
                            </ul>
                        </div>

                        <div id="content_div">
                            <div id="div_gridView">
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateSelectButton="True" OnSorting="GridView1_Sorting" HeaderStyle-Wrap="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanging">
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
                            <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateEditButton="True" Height="50px" OnItemUpdating="DetailsView1_ItemUpdating1" OnModeChanging="DetailsView1_ModeChanging" Width="125px">
                                <Fields>
                                    <asp:TemplateField HeaderText="EmployeePermition">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DetailsView1DropDownListEmployeePermition" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelEmployeePermition" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DetailsView1DropDownListStatus" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelStatus" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                            <br />
                            <div id="description_div">
                                <asp:Label ID="label_description_header" runat="server" Text="Task Description">

                                </asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="label_description" runat="server" Text="">

                                </asp:Label>
                                <br />
                                <br />
                                <br />

                                <asp:Label ID="labelError" runat="server" Text="">
                                </asp:Label>


                            </div>
                        </div>

                    </div>

                </div>
<%--                hidden button to check if tasks updated and if there is a new tasks--%>
       <button id="check_new_task" runat="server"  onserverclick="check_new_task_Click"  />

            </ContentTemplate>
        </asp:UpdatePanel>

        <script src="../../java%20script/JavaScript.js"></script>

        <script type="text/javascript">
            setInterval("document.getElementById('check_new_task').click()", 5000);
        </script>
    </form>

</body>
</html>

