Imports Microsoft.VisualBasic
Imports System.Diagnostics
Imports System

Public Class pathRoutines
    Public Enum fileExtension
        pixJpeg = 1
        pixGif = 2
        pixPng = 3
        pixBmp = 4

        docWord = 8
        docWordX = 9
        docXls = 10
        docXlsX = 11
        docPdf = 12
        txtFile = 13

        swfFile = 25 'adobe flash
    End Enum
    Public Shared Function fileExtensionDesc(ByVal eExtension As fileExtension) As String
        Dim sRet As String = ""
        Try
            Select Case eExtension
                Case fileExtension.docPdf : sRet = ".pdf"
                Case fileExtension.docWord : sRet = ".doc"
                Case fileExtension.docWordX : sRet = ".docx"
                Case fileExtension.docXls : sRet = ".xls"
                Case fileExtension.docXlsX : sRet = ".xlsx"
                Case fileExtension.txtFile : sRet = ".txt"

                Case fileExtension.pixJpeg : sRet = ".jpg"
                Case fileExtension.pixGif : sRet = ".gif"
                Case fileExtension.pixPng : sRet = ".png"
                Case fileExtension.pixBmp : sRet = ".bmp"

                Case fileExtension.swfFile : sRet = ".swf"
            End Select
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Shared Function fileExtnIsValid(ByVal sFileName As String, ByVal eExtension As fileExtension) As Boolean
        Dim bRet As Boolean
        Try
            sFileName = IO.Path.GetExtension(sFileName).ToLower
            If sFileName = fileExtensionDesc(eExtension) Then
                bRet = True
            End If
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return bRet
    End Function
    Public Shared Function fileExtnIsPicture(ByVal sFileName As String) As Boolean
        Dim bRet As Boolean
        Try
            Select Case IO.Path.GetExtension(sFileName).ToLower
                Case fileExtensionDesc(fileExtension.pixGif), fileExtensionDesc(fileExtension.pixJpeg), fileExtensionDesc(fileExtension.pixPng), fileExtensionDesc(fileExtension.pixBmp)
                    bRet = True
            End Select
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return bRet
    End Function
    Public Shared Function getFileExtnName(ByVal sFileName As String) As String
        Dim sRet As String = ""
        Try
            sRet = IO.Path.GetExtension(sFileName).ToLower
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
End Class