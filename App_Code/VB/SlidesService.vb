Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.IO.DirectoryInfo

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class SlidesService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetSlides() As AjaxControlToolkit.Slide()

        Dim di As New System.IO.DirectoryInfo("C:\App\Office Projects\FieldVisit\App\PDCC\CostSavingStories\")

        Dim smFiles() As IO.FileInfo
        smFiles = di.GetFiles("*.*")

        Dim MySlides(smFiles.Length) As AjaxControlToolkit.Slide

        Try
            Dim i As Integer
            i = 0

            For Each fi As System.IO.FileInfo In smFiles
                MySlides(i) = New AjaxControlToolkit.Slide(fi.FullName, fi.Name, fi.Name)
                i = i + 1
            Next

        Catch ex As Exception
            appMonitor.logAppExceptions(ex)
        End Try

        'MySlides(0) = New AjaxControlToolkit.Slide("App/PDCC/CostSavingStories/Blue hills.jpg", "Blue Hills", "Go Blue")
        'MySlides(1) = New AjaxControlToolkit.Slide("App/PDCC/CostSavingStories/Sunset.jpg", "Sunset", "Setting sun")
        'MySlides(2) = New AjaxControlToolkit.Slide("App/PDCC/CostSavingStories/Winter.jpg", "Winter", "Wintery...")
        'MySlides(3) = New AjaxControlToolkit.Slide("App/PDCC/CostSavingStories/Water lilies.jpg", "Water lillies", "Lillies in the water")
        'MySlides(4) = New AjaxControlToolkit.Slide("App/PDCC/CostSavingStories/VerticalPicture.jpg", "Sedona", "Portrait style picture")

        Return MySlides
    End Function
End Class
