Imports Microsoft.VisualBasic
Imports System.Diagnostics
Imports System
Imports stringRoutine

Public Class dateRoutine
    Public Shared Function dateLong(ByVal oDate As Date) As String
        Dim sRet As String = "Invalid Date"
        Try
            sRet = FormatDateTime(oDate, DateFormat.LongDate)
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Shared Function dateStandard(ByVal oDate As Date) As String
        Dim sRet As String = "Invalid Date"
        Try
            sRet = oDate.Day & ", " & getMonthName(oDate.Month) & " " & oDate.Year
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Shared Function dateShort(ByVal oDate As Date) As String
        Dim sRet As String = "Invalid Date"
        Try
            sRet = oDate.Day & ", " & stringRoutine.strLeft(getMonthName(oDate.Month), 3) & " " & oDate.Year
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Shared Function dateShort(ByVal oDate As Date, ByVal sSpliter As String) As String
        Dim sRet As String = "Invalid Date"
        Try
            sRet = oDate.Day & sSpliter & stringRoutine.strLeft(getMonthName(oDate.Month), 3) & sSpliter & oDate.Year
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function

    Public Shared Function dateShort2(ByVal oDate As Date, ByVal sSpliter As String) As String
        Dim sRet As String = "Invalid Date"
        Try
            sRet = oDate.Day & sSpliter & oDate.Month & sSpliter & oDate.Year
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function


    Public Shared Function dateOracle(ByVal oDate As Date) As String
        Dim sRet As String = "Invalid Date"
        Try
            sRet = oDate.Day & "-" & stringRoutine.strLeft(getMonthName(oDate.Month), 3).ToUpper & "-" & CType(oDate.Year, String).Substring(2)
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Shared Function dateProject(ByVal oDate As Date) As String
        Dim sRet As String = "Invalid Date"
        Try
            sRet = stringRoutine.strLeft(getMonthName(oDate.Month), 3).ToUpper & "-" & CType(oDate.Year, String).Substring(2)
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function

    Public Shared Function timeLong(ByVal oDate As Date) As String
        Dim sRet As String = "Invalid Time"
        Try
            sRet = FormatDateTime(oDate, DateFormat.LongTime)
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Shared Function timeLongEx(ByVal oDate As Date) As String
        Dim sRet As String = "Invalid Time"
        Try
            sRet = FormatDateTime(oDate, DateFormat.LongTime)
            Dim xPos As Short = sRet.IndexOf(":", 0) + 1
            xPos = sRet.IndexOf(":", xPos)
            If xPos > -1 Then
                sRet = sRet.Replace(sRet.Substring(xPos, 3), "")
            End If
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Shared Function timeShort(ByVal oDate As Date) As String
        Dim sRet As String = "Invalid Time"
        Try
            sRet = FormatDateTime(oDate, DateFormat.ShortTime)
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function

    Public Shared Function minute2Hour(ByVal lMinute As Long) As String
        Dim sRet As String = "Invalid Time"
        Try
            Select Case lMinute
                Case 0, 1
                    sRet = lMinute & " Minute"
                Case 2 To 59
                    sRet = lMinute & " Minutes"
                Case 60
                    sRet = "1 Hour"
                Case 61 To 119
                    sRet = "1 Hour" & ", " & minute2Hour(lMinute Mod 60)
                Case Else
                    sRet = CType(lMinute \ 60, Long) & " Hours" & ", " & minute2Hour(lMinute Mod 60)
            End Select
            If lMinute < 60 Then

            End If
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Shared Function workDaysInMonth(ByVal xYear As Integer, ByVal tMonth As Byte) As Byte
        Dim tRet As Byte = 0
        Try
            For tLoop As Byte = 1 To DateTime.DaysInMonth(xYear, tMonth)
                Select Case New Date(xYear, tMonth, tLoop).DayOfWeek
                    Case DayOfWeek.Saturday, DayOfWeek.Sunday
                    Case Else
                        tRet = tRet + 1
                End Select
            Next
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return tRet
    End Function
    Public Shared Function workDaysInMonth(ByVal dDate As Date) As Byte
        Dim tRet As Byte = 0
        Try
            For tLoop As Byte = 1 To dDate.Day
                Select Case New Date(dDate.Year, dDate.Month, tLoop).DayOfWeek
                    Case DayOfWeek.Saturday, DayOfWeek.Sunday
                    Case Else
                        tRet = tRet + 1
                End Select
            Next
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return tRet
    End Function

    Public Shared Function dateIntervalAbs(ByVal oStartMin As Date, ByVal oStopMax As Date, ByVal eInterval As DateInterval) As Long
        Dim iRet As Long = 0
        Try
            iRet = DateDiff(eInterval, oStartMin, oStopMax)
            If iRet < 0 Then iRet = 0
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return iRet
    End Function
    Public Overloads Shared Function monthName(ByVal tMonth As Byte, ByVal tLent As Byte) As String
        Dim sRet As String = "Invalid Month"
        Try
            Dim sTemp As String = getMonthName(tMonth)
            If tLent > sTemp.Length Then tLent = sTemp.Length
            sRet = stringRoutine.strLeft(sTemp, tLent)
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
    Public Overloads Shared Function monthName(ByVal tMonth As Byte) As String
        Dim sRet As String = "Invalid Month"
        Try
            sRet = monthName(tMonth, 3)
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function

#Region "REGION : Private Routine Code Block"
    Private Shared Function getMonthName(ByVal tMonth As Byte) As String
        Dim sRet As String = "Error"
        Try
            Select Case tMonth
                Case 1 : sRet = "January"
                Case 2 : sRet = "February"
                Case 3 : sRet = "March"
                Case 4 : sRet = "April"
                Case 5 : sRet = "May"
                Case 6 : sRet = "June"
                Case 7 : sRet = "July"
                Case 8 : sRet = "August"
                Case 9 : sRet = "September"
                Case 10 : sRet = "October"
                Case 11 : sRet = "November"
                Case 12 : sRet = "December"
            End Select
        Catch ex As Exception
            Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return sRet
    End Function
#End Region

End Class