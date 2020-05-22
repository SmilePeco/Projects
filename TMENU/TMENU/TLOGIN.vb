Imports System.Xml
Imports System.Data.SqlClient

Public Class TLOGIN

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '---------------------------------------------
    '---Load処理                               ---
    '---------------------------------------------
    Private Sub TLOGIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'クリア処理
        Call sClear()


    End Sub

    '---------------------------------------------
    '---エンターキー押下処理                   ---
    '---------------------------------------------
    Private Sub TLOGIN_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                'ログイン処理
                Call sLogin()
        End Select
    End Sub

    '---------------------------------------------
    '---ログインボタン押下処理                 ---
    '---------------------------------------------
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'ログイン処理
        Call sLogin()
    End Sub

    '---------------------------------------------
    '---パスワード変更ボタン押下処理           ---
    '---------------------------------------------
    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        'パスワード変更前処理
        Call sPassChange()

    End Sub

    '---------------------------------------------
    '---ログイン処理                           ---
    '---------------------------------------------
    Public Sub sLogin()
        'DB接続
        Call fDBConnect()
        'ログインメイン処理
        Call fMainLogin()

    End Sub

    '---------------------------------------------
    '---ログインボタン押下処理                 ---
    '---------------------------------------------
    Private Function fMainLogin() As Boolean

        Dim dtReader As SqlDataReader
        Dim frm As New TMENU
        Dim strUserID As String
        Dim intAdminFLG As Integer
        Dim result As DialogResult

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 社員NO, "
            strSQL &= " 名前, "
            strSQL &= " 管理者フラグ "
            strSQL &= "FROM "
            strSQL &= " HUMAN_MS "
            strSQL &= "WHERE "
            strSQL &= "    名前 = '" + txtLoginID.Text.Trim + "' "
            strSQL &= "AND パスワード = '" + txtPassword.Text.Trim + "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader
            If dtReader.Read = True Then
                'ログイン成功
                'Readし、値を取得。メニュー画面に引き継ぐ
                While dtReader.Read
                    strUserID = dtReader("名前")
                    intAdminFLG = dtReader("管理者フラグ")
                End While
                dtReader.Close()
                result = frm.ShowDialog()
                Return True
                'result = frm.ShowDialog(strUserID, intAdminFLG)

            Else
                'ログイン失敗
                MessageBox.Show("ログイン情報に誤りがあります。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                dtReader.Close()
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            Return False
        Finally
            Cn.Close()
            Cn.Dispose()
        End Try






    End Function

    '---------------------------------------------
    '---パスワード変更前処理                 ---
    '---------------------------------------------
    Public Sub sPassChange()
        'DB接続
        Call fDBConnect()
        '変更前メイン処理
        Call fMainPassChange()

    End Sub

    '---------------------------------------------
    '---パスワード変更前メイン処理             ---
    '---------------------------------------------
    Private Function fMainPassChange() As Boolean

        Dim dtReader As SqlDataReader
        Dim frm As TPASSCHANGE = New TPASSCHANGE(txtLoginID.Text.Trim)
        Dim result As DialogResult

        Try

            '変更の前にログインチェックする
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 社員NO "
            strSQL &= "FROM "
            strSQL &= " HUMAN_MS "
            strSQL &= "WHERE "
            strSQL &= "    名前 = '" + txtLoginID.Text.Trim + "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                dtReader.Close()
                'パスワード変更画面を開く
                result = frm.ShowDialog(Me)
                Return True

            Else
                '入力したログインIDが存在しない場合は失敗とする
                dtReader.Close()
                MessageBox.Show("そのログインIDは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            Return False
        Finally
            Cn.Close()
            Cn.Dispose()
        End Try




    End Function

    '---------------------------------------------
    '---DB接続処理                             ---
    '---------------------------------------------
    Public Function fDBConnect() As Boolean

        Dim strServer As String
        Dim strUserID As String
        Dim strPassword As String
        Dim strDatabaseName As String

        Dim xmlNode As XmlNodeList
        Dim strFileAdress As String = "C:\SQLServer\DBConnect.xml"

        Dim xmlDoc As New XmlDocument()

        Try
            If System.IO.File.Exists(strFileAdress) Then
                xmlDoc.Load(strFileAdress)

                'サーバ名の取得
                xmlNode = xmlDoc.GetElementsByTagName("Server")
                strServer = xmlNode.Item(0).InnerText
                'ユーザ名の取得
                xmlNode = xmlDoc.GetElementsByTagName("UserID")
                strUserID = xmlNode.Item(0).InnerText
                'パスワードの取得
                xmlNode = xmlDoc.GetElementsByTagName("Password")
                strPassword = xmlNode.Item(0).InnerText
                'データベース名の取得
                xmlNode = xmlDoc.GetElementsByTagName("DatabaseName")
                strDatabaseName = xmlNode.Item(0).InnerText

                'DB接続
                strConnect = "Server=" & strServer & ";"
                strConnect &= "User ID=" & strUserID & ";"
                strConnect &= "Password=" & strPassword & ";"
                strConnect &= "Initial Catalog=" & strDatabaseName

                Cn.ConnectionString = strConnect

                cd = Cn.CreateCommand

                Cn.Open()

                Return True

            Else
                MessageBox.Show("接続設定ファイルが存在しません。" & vbCrLf & "データベースに接続できませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            Return False

        End Try

    End Function

    '---------------------------------------------
    '---クリア処理                             ---
    '---------------------------------------------
    Public Sub sClear()
        txtLoginID.Clear()
        txtPassword.Clear()
        Me.MaximizeBox = False
    End Sub




End Class