using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace uploadfiles
{
    /// <summary>
    /// Summary description for UploadFiles1
    /// </summary>
    public class UploadFiles1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string upload_directory = "uploaded_files";
            string physical_upload_directory =context.Server.MapPath( "~/") + upload_directory;

            if (Directory.Exists(physical_upload_directory))
            {
                physical_upload_directory = context.Server.MapPath("~/" + upload_directory + "/");
            }
            else
            {
                Directory.CreateDirectory(context.Server.MapPath("~/")+upload_directory);
                physical_upload_directory = context.Server.MapPath("~/" + upload_directory + "/");
            }
            try
            {
                var files = context.Request.Files;
                HttpPostedFile file;
                for (int i = 0; i < files.Count; i++)
                {
                    file = files[i];
                    //getting the file name  'cutting the path'
                    var file_name="";
                    if (file.FileName.IndexOf("\\") != -1)
                    {
                        var index1 = file.FileName.LastIndexOf("\\");
                        var index2 = file.FileName.Length;
                            file_name = file.FileName.Substring(index1 + 1, index2 - index1 - 1);
                    }
                    var file_to_save = physical_upload_directory + file_name;
                    //saving the file
                    files[i].SaveAs(file_to_save);
                }
                var directory2 = context.Server.MapPath("~/");
                //  var xcs2 = Directory.GetFiles("~/"+upload_directory);
                var files_in_upload_directory =String.Join(" , ", (Directory.GetFiles(physical_upload_directory)));
                 
                context.Response.Write(files_in_upload_directory);
            }
            catch (Exception ex)
            {

                context.Response.Write(ex.Message);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}