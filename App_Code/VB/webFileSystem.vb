Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Diagnostics
Imports System

Public Class webFileSystem
    Public Shared Function deleteThisFileName(ByVal sFilePath As String, ByVal bThrowErr As Boolean) As Boolean
        Dim bRet As Boolean
        Try
            Dim oFile As IO.FileInfo
            oFile = New IO.FileInfo(sFilePath)
            oFile.Attributes = IO.FileAttributes.Archive
            oFile.Attributes = IO.FileAttributes.Normal
            oFile.Delete()
            bRet = True
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return bRet
    End Function
    Public Shared Function deleteThisFileType(ByVal sDirPath As String, ByVal sFileExt As String, ByVal bThrowErr As Boolean) As Boolean
        Dim bRet As Boolean
        Try
            Dim oFile As IO.FileInfo
            For Each sFile As String In My.Computer.FileSystem.GetFiles(sDirPath, FileIO.SearchOption.SearchAllSubDirectories, "*." & sFileExt)
                oFile = New IO.FileInfo(sFile)
                oFile.Attributes = IO.FileAttributes.Archive
                oFile.Attributes = IO.FileAttributes.Normal
                oFile.Delete()
            Next
            bRet = True
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return bRet
    End Function
    Public Shared Function deleteSubAllFolders(ByVal sDirPath As String, ByVal bThrowErr As Boolean) As Boolean
        Dim bRet As Boolean
        Try
            Dim oDir As IO.DirectoryInfo 'LUXOR error filter 
            For Each sDir As String In My.Computer.FileSystem.GetDirectories(sDirPath, FileIO.SearchOption.SearchTopLevelOnly)
                oDir = New IO.DirectoryInfo(sDir)
                oDir.Attributes = IO.FileAttributes.Archive
                oDir.Attributes = IO.FileAttributes.Normal
                oDir.Delete(True)
            Next
            bRet = True
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return bRet
    End Function

    Public Function streamFileToByte(ByVal sFileUrl As String) As Byte()
        Dim tRet As Byte() = Nothing
        Try
            Dim oRequest As WebRequest = WebRequest.Create(sFileUrl)
            Dim tBuffer As Byte() = New Byte(4096) {}
            Dim oResponse As WebResponse = oRequest.GetResponse()
            Dim oStream As IO.Stream = oResponse.GetResponseStream()
            Dim oMemory As IO.MemoryStream = New IO.MemoryStream()
            Dim iChunkSize As Integer = 0
            Do
                iChunkSize = oStream.Read(tBuffer, 0, tBuffer.Length)
                oMemory.Write(tBuffer, 0, iChunkSize)
            Loop While (iChunkSize <> 0)
            tRet = oMemory.ToArray()
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return tRet
    End Function
    Public Function streamFileToByte(ByRef oStream As IO.Stream) As Byte()
        Dim tRet As Byte() = Nothing
        Try
            Dim tBuffer As Byte() = New Byte(4096) {}
            Dim oMemory As IO.MemoryStream = New IO.MemoryStream()
            Dim iChunkSize As Integer = 0
            Do
                iChunkSize = oStream.Read(tBuffer, 0, tBuffer.Length)
                oMemory.Write(tBuffer, 0, iChunkSize)
            Loop While (iChunkSize <> 0)
            tRet = oMemory.ToArray()
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return tRet
    End Function
End Class