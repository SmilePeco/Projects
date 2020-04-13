Public Class INIConnect

    Private strIniFileName As String = ""

    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (
    ByVal lpApplicationName As String,
    ByVal lpKeyName As String,
    ByVal lpDefault As String,
    ByVal lpReturnedString As System.Text.StringBuilder,
    ByVal nSize As UInt32,
    ByVal lpFileName As String) As UInt32

    Sub New(ByVal strIniFile As String)
        Me.strIniFileName = strIniFile  'ファイル名退避
    End Sub


    Public Function GetProfileString(ByVal strAppName As String,
                                  ByVal strKeyName As String,
                                  ByVal strDefault As String) As String
        Try
            Dim strWork As System.Text.StringBuilder = New System.Text.StringBuilder(1024)
            Dim intRet As Integer = GetPrivateProfileString(strAppName, strKeyName,
                                                                       strDefault, strWork,
                                                                       strWork.Capacity - 1, strIniFileName)
            If intRet > 0 Then
                'エスケープ文字を解除して返す
                Return ResetEscape(strWork.ToString())
            Else
                Return strDefault
            End If
        Catch ex As Exception
            Return strDefault
        End Try
    End Function

    Private Function ResetEscape(ByVal strSet As String) As String
        Dim strEscape As String = ";#=:"
        Dim strRet As String = strSet
        Try
            For i = 0 To strEscape.Length - 1
                Dim str As String = strEscape.Substring(i, 1)
                strRet = strRet.Replace("\" & str, str)
            Next
            Return strRet
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class

