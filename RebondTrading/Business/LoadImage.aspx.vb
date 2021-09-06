Imports System.IO
Imports System.Threading
Imports RebondTrading.Models

Public Class LoadImage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("FileName") IsNot Nothing Then
            Try
                ' Read the file and convert it to Byte Array

                'Dim filePath As String = "D:\Apps Folders\UploadedPhotos\"
                Dim filePath As String = Server.MapPath("~") & "\" & System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString & "\"
                Dim filename As String = Request.QueryString("FileName")
                If Not File.Exists(filePath & filename) Then
                    filename = "NoImg.png"
                End If

                'Dim filename As String = "sample.jpg"
                Dim contenttype As String = "image/" & Path.GetExtension(filename).Replace(".", "")


                Dim fs As FileStream = New FileStream(filePath & filename, FileMode.Open, FileAccess.Read)
                Dim br As BinaryReader = New BinaryReader(fs)

                Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))

                br.Close()

                fs.Close()



                'Write the file to Reponse

                Response.Buffer = True

                Response.Charset = ""

                Response.Cache.SetCacheability(HttpCacheability.NoCache)

                Response.ContentType = contenttype

                Response.AddHeader("content-disposition", "attachment;filename=" & filename)

                Response.BinaryWrite(bytes)

                Response.Flush()

                Response.End()
            Catch ex As Exception When Not TypeOf ex Is ThreadAbortException
                AppSpecificFunc.WriteLog(ex)
            End Try
        End If
    End Sub

End Class