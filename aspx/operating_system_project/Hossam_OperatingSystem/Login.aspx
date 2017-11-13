<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/Login_css.css" rel="stylesheet" />
</head>
<body>
   
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel id="up1" runat="server">
        <ContentTemplate>
        <div id="main_div">
            <div id="login_div">
                <asp:Label CssClass="labels" ID="Label1" runat="server" Text="user name"></asp:Label>
                <asp:TextBox ID="TextBoxName" onkeyup="check_password();" runat="server"></asp:TextBox>
                <asp:Label CssClass="labels" ID="Label2" runat="server" Text="password"></asp:Label>
                <asp:TextBox ID="TextBoxPassword" onkeyup="check_password();" runat="server"></asp:TextBox>
                <asp:Button ID="Button_login" runat="server" Text="login" OnClick="Button_login_click" />
                <br />
              <div>  <asp:Label  ID="ErrorLabel" runat="server"  Text="password">err</asp:Label>
            </div>
                </div>
        </div>
    </ContentTemplate>
        </asp:UpdatePanel>
    </form>
        
    <script src="java%20script/JavaScript.js"></script>
    <script type="text/javascript">
        
        window.onload = function () { firstLoad(); }

        function firstLoad()
        {
            document.getElementById("Button_login").disabled = true; 
            document.getElementById("TextBoxName").focus();
        }
        
       

       
    </script>
</body>
</html>
