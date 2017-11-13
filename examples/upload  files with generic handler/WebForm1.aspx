<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="uploadfiles.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="Scripts/jquery-1.11.2.js"></script>
   

    <%--upload files using ajax  --%> 
    <script type="text/javascript">
        var text;
        $(document).ready(function () {
            $("#upload_files").click(function ()
            {
                var files = $("#select_files")[0].files;
                if (files.length > 0)
                {
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++)
                    {
                        data.append(files[i].name, files[i])
                    }
                    $.ajax({
                            url: "UploadFiles.ashx",
                            method:"post",
                            contentType: false,
                            processData: false,
                            data: data,
                            error: function (data) {alert("Error  " + data.status);
                            },
                            success: function (data) {
                                text = data.replace(/,/g, "<br/>");
                               $('#result').html(text);
                            }
                        })
                }
            })
        });
        
        
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="file" id="select_files" multiple="multiple" />
            <input type="button" id="upload_files" value="upload files"  />
             <p id="result"> </p>

        </div>
    </form>
   
</body>

</html>
