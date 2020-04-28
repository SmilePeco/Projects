Imports System.Xml
Imports System.Data.SqlClient

Public Class T007

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T007_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sClear()

    End Sub

    '------------------------------------------------
    '--受注先NO ボタン押下処理             ----------
    '------------------------------------------------
    Private Sub btnOrderSearch_Click(sender As Object, e As EventArgs) Handles btnOrderSearch.Click
        '受注先マスタ検索画面を開く
        Dim frm As New T007_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--作業工程NO ボタン押下処理           ----------
    '------------------------------------------------
    Private Sub cmdWorkProcessSearch_Click(sender As Object, e As EventArgs) Handles cmdWorkProcessSearch.Click
        '作業工程マスタ検索画面を開く
        Dim frm As New T007_3
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--更新担当者 ボタン押下処理           ----------
    '------------------------------------------------
    Private Sub btnUserIDSearch_Click(sender As Object, e As EventArgs) Handles btnUserIDSearch.Click
        '更新担当者検索画面を開く
        Dim frm As New T007_4
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--登録ボタン処理                      ----------
    '------------------------------------------------
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim result As Boolean
        'DB接続
        Call sDBConnect()
        '登録前チェック
        result = fSubmitCheck()
        '問題なければ登録できる
        If result = True Then
            'DB接続
            Call sDBConnect()
            '登録処理
            result = fSubmit()
            If result = True Then
                Call sClear()
            End If

        End If

    End Sub

    '------------------------------------------------
    '--受注先NO KeyPress処理               ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderMSNo.KeyPress

        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then

            e.Handled = True

        End If

    End Sub

    '------------------------------------------------
    '--受注先NO Leave処理                  ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_Leave(sender As Object, e As EventArgs) Handles txtOrderMSNo.Leave

        '０埋め処理
        txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")

        Call sOrderMSNO_Search()

    End Sub

    '------------------------------------------------
    '--作業工程NO Leave処理                ----------
    '------------------------------------------------
    Private Sub txtWorkProcessNO_Leave(sender As Object, e As EventArgs) Handles txtWorkProcessNO.Leave

        '０埋め処理
        txtWorkProcessNO.Text = txtWorkProcessNO.Text.PadLeft(3, "0")

        Call sWorkProcess_Search()

    End Sub

    '------------------------------------------------
    '--受注数   KeyPress処理               ----------
    '------------------------------------------------
    Private Sub txtOrderAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderAmount.KeyPress

        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then

            e.Handled = True

        End If

    End Sub

    '------------------------------------------------
    '--受注先NO 検索処理                   ----------
    '------------------------------------------------
    Public Sub sOrderMSNO_Search()
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fOrderMSNO_MainSearch()


    End Sub

    '------------------------------------------------
    '--受注先NO 検索メイン処理             ----------
    '------------------------------------------------
    Public Function fOrderMSNO_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            '検索用SQL
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 受注先NO, "
            strSQL &= " 受注先名 "
            strSQL &= "FROM "
            strSQL &= " ORDER_MS "
            strSQL &= "WHERE "
            strSQL &= " 受注先NO = '" & txtOrderMSNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            '一致した場合は受注先名を取得
            If dtReader.HasRows Then
                While dtReader.Read
                    lblOrderMSName.Text = CStr(dtReader("受注先名")).Trim
                End While
                dtReader.Close()
                Return True

            Else
                dtReader.Close()
                '一致しなかった場合は受注先名を空白
                lblOrderMSName.Text = ""
                'MessageBox.Show("一致する受注先NOが存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    '------------------------------------------------
    '--作業工程NO 検索処理                 ----------
    '------------------------------------------------
    Public Sub sWorkProcess_Search()
        '０埋め処理
        txtWorkProcessNO.Text = txtWorkProcessNO.Text.PadLeft(3, "0")
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fWorkProcess_MainSearch()

    End Sub

    '------------------------------------------------
    '--作業工程NO 検索メイン処理           ----------
    '------------------------------------------------
    Public Function fWorkProcess_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 作業工程NO, "
            strSQL &= " 作業工程名 "
            strSQL &= "FROM "
            strSQL &= " WORKPROCESS_MS "
            strSQL &= "WHERE "
            strSQL &= " 作業工程NO = '" + txtWorkProcessNO.Text.Trim + "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    txtWorkProcessNO.Text = CStr(dtReader("作業工程NO")).Trim
                    lblWorkProcessName.Text = CStr(dtReader("作業工程名")).Trim
                End While
                dtReader.Close()
                Return True
            Else
                dtReader.Close()
                lblWorkProcessName.Text = ""
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

    '------------------------------------------------
    '--登録前チェック処理                  ----------
    '------------------------------------------------
    Public Function fSubmitCheck() As Boolean

        Dim dtReader As SqlDataReader

        Try
            '入力した受注先がマスタに存在するかチェック
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 受注先NO "
            strSQL &= "FROM "
            strSQL &= " ORDER_MS "
            strSQL &= "WHERE "
            strSQL &= " 受注先NO = '" + txtOrderMSNo.Text.Trim + "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                dtReader.Close()

                '入力した作業工程NOがマスタに存在するかチェック
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 作業工程NO "
                strSQL &= "FROM "
                strSQL &= " WORKPROCESS_MS "
                strSQL &= "WHERE "
                strSQL &= " 作業工程NO = '" + txtWorkProcessNO.Text.Trim + "' "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    dtReader.Close()
                    '受注数の入力チェック
                    If txtOrderAmount.Text.Trim <> "" Then
                        '入力した更新担当者がマスタに存在するかチェック
                        strSQL = ""
                        strSQL &= "SELECT "
                        strSQL &= " 名前 "
                        strSQL &= "FROM "
                        strSQL &= " HUMAN_MS "
                        strSQL &= "WHERE "
                        strSQL &= " 名前='" + txtUserID.Text.Trim + "'"

                        cd.CommandText = strSQL
                        cd.Connection = Cn
                        dtReader = cd.ExecuteReader

                        If dtReader.HasRows Then
                            Return True

                        Else
                            MessageBox.Show("入力したIDは登録されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            txtUserID.Focus()
                            Return False
                        End If





                        Return True

                    Else
                        '空欄だった場合はエラーとする
                        MessageBox.Show("受注数が入力されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtOrderAmount.Focus()
                        Return False
                    End If

                    Return True

                Else
                    MessageBox.Show("入力した作業工程先は登録されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtWorkProcessNO.Focus()
                    Return False
                End If

            Else
                MessageBox.Show("入力した受注先NOは登録されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtOrderMSNo.Focus()
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

    '------------------------------------------------
    '--登録処理                            ----------
    '------------------------------------------------
    Public Function fSubmit()

        Dim dtReader As SqlDataReader
        Dim intOrderCount As Integer

        Try
            Dim result As DialogResult = MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                '//受注NO採番のため、現在のMAX値を取得
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " COALESCE(MAX(受注NO), 0) AS 受注NO "
                'strSQL &= " 受注NO "
                strSQL &= "FROM "
                strSQL &= " ORDER_TBL "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    While dtReader.Read
                        intOrderCount = dtReader("受注NO")
                    End While
                    intOrderCount += 1
                    dtReader.Close()

                Else
                    'ありえないけど一応設定
                    '１件も登録されていない場合は１を代入
                    intOrderCount = 1
                    dtReader.Close()
                End If

                '//登録用INSERT文SQL
                strSQL = ""
                strSQL &= "INSERT INTO ORDER_TBL VALUES ( "
                strSQL &= " " & intOrderCount & ","
                strSQL &= " '" & txtOrderMSNo.Text.Trim & "', "
                strSQL &= " '" & txtWorkProcessNO.Text.Trim & "',"
                strSQL &= " " & txtOrderAmount.Text.Trim & ", "
                strSQL &= " '" & dtpOrderDate.Value.ToString & "', "
                strSQL &= " '" & txtUserID.Text.Trim & "', "
                strSQL &= " 'FALSE', "
                strSQL &= " 'FALSE', "
                strSQL &= " SYSDATETIME(), "
                strSQL &= " SYSDATETIME() "
                strSQL &= " ) "

                cd.CommandText = strSQL
                cd.Connection = Cn
                cd.ExecuteNonQuery()

                MessageBox.Show("登録完了しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Return True

            Else
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

    '------------------------------------------------
    '--DB接続の開始                        ----------
    '------------------------------------------------
    Public Function sDBConnect() As Boolean

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

    '------------------------------------------------
    '--クリア処理                          ----------
    '------------------------------------------------
    Public Sub sClear()

        txtOrderMSNo.Clear()
        lblOrderMSName.Text = ""
        dtpOrderDate.Value = DateTime.Now

        txtWorkProcessNO.Clear()
        lblWorkProcessName.Text = ""

        txtOrderAmount.Clear()

        txtUserID.Clear()

        txtOrderMSNo.Focus()



    End Sub




End Class
