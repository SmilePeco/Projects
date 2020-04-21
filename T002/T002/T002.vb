Imports System.Data.SqlClient

Public Class T002

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String
    Dim strServerName As String = "SHINYA-PC\NAKADB" 'サーバー名
    Dim strUserID As String = "sa" 'ユーザーID
    Dim strPassword As String = "naka" 'パスワード
    Dim strDatabaseName As String = "NAKADB" 'データベース
    Dim blnResult As Boolean '名前・パスワード判定用

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T002_Load(sender As Object, e As EventArgs) Handles Me.Load

        sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー操作処理          ----------
    '------------------------------------------------
    Private Sub T002_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode

            Case Keys.F1
                If btnSearch.Enabled = True Then
                    '//「F1検索」押下
                    '検索処理
                    Call frmSearch()
                End If

            Case Keys.F5
                If btnEntry.Enabled = True Then
                    '//「F5登録」押下
                    '新規登録・更新処理
                    Call frmEntry()
                End If

            Case Keys.F6
                If btnDelete.Enabled = True Then
                    '//「F6削除」押下
                    '削除処理
                    Call frmDelete()
                End If

            Case Keys.F11
                '//「F11キャンセル」押下
                'クリア処理
                Call sClear()

            Case Keys.F12
                '//「F12終了」押下
                'クローズ処理
                Me.Close()

        End Select
    End Sub



    '------------------------------------------------
    '--F1検索ボタン押下処理                ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        '検索処理
        Call frmSearch()

    End Sub

    '------------------------------------------------
    '--F5新規登録・更新ボタン押下処理      ----------
    '------------------------------------------------
    Private Sub btnEntry_Click(sender As Object, e As EventArgs) Handles btnEntry.Click

        '新規登録・更新処理
        Call frmEntry()

    End Sub

    '------------------------------------------------
    '--F6削除ボタン押下処理                ----------
    '------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        '削除処理
        Call frmDelete()

    End Sub

    '------------------------------------------------
    '--F11キャンセルボタン押下処理         ----------
    '------------------------------------------------
    Private Sub blnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Call sClear()

    End Sub

    '------------------------------------------------
    '--F12終了ボタン押下処理               ----------
    '------------------------------------------------
    Private Sub blnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click

        'クローズ処理
        Me.Close()

    End Sub

    '------------------------------------------------
    '--作業No KeyPress処理                 ----------
    '------------------------------------------------
    Private Sub txtWorkNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWorkNo.KeyPress

        '入力文字制御(数値のみ)
        If e.KeyChar < "0"c OrElse "9"c < e.KeyChar Then
            '押されたキーが 0～9でない場合は、イベントをキャンセルする
            e.Handled = True
        End If

    End Sub

    '------------------------------------------------
    '--検索処理                            ----------
    '------------------------------------------------
    Public Sub frmSearch()

        'DB接続処理
        Call sDBConnect()
        '検索メイン処理()
        Call frmMainSearch()

    End Sub

    '------------------------------------------------
    '--DB接続の開始                        ----------
    '------------------------------------------------
    Public Sub sDBConnect()

        'DBに接続
        strServerName = "SHINYA-PC\NAKADB" 'サーバー名(またはIPアドレス)
        strUserID = "sa" 'ユーザーID
        strPassword = "naka" 'パスワード
        strDatabaseName = "NAKADB" 'データベース

        strConnect = "Server=" & strServerName & ";"
        strConnect &= "User ID=" & strUserID & ";"
        strConnect &= "Password=" & strPassword & ";"
        strConnect &= "Initial Catalog=" & strDatabaseName

        Cn.ConnectionString = strConnect

        cd = Cn.CreateCommand

        Cn.Open()

    End Sub


    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function frmMainSearch() As Boolean

        Dim dtReader As SqlDataReader

        If txtWorkNo.Text.Trim <> "" Then

            Try
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= "作業No, "
                strSQL &= "作業NO "
                strSQL &= "FROM "
                strSQL &= "WORK_MS "
                strSQL &= "WHERE "
                strSQL &= "    社員No= '" + txtWorkNo.Text.ToString("000") + "' "
                'SQLコマンド設定
                cd.CommandText = strSQL
                cd.Connection = Cn
                cd.ExecuteNonQuery()

                dtReader = cd.ExecuteReader()

                If dtReader.Read() = True Then
                    lblMode.Text = "更新"
                    btnDelete.Enabled = True
                    Call sgrpHumanOpen()
                    txtName.Text = dtReader("名前").ToString.Trim()
                    If dtReader("管理者フラグ") = 0 Then
                        chkAdminFLG.Checked = False
                    ElseIf dtReader("管理者フラグ") = 1 Then
                        chkAdminFLG.Checked = True
                    End If
                    Return True
                Else

                    lblMode.Text = "新規作成"

                    Call sgrpHumanOpen()
                    Cn.Close()
                    Cn.Dispose()

                    Return True

                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString, "例外発生")
                Return False

            Finally
                Cn.Close()
                Cn.Dispose()
                dtReader.Close()

            End Try

        Else
            Cn.Close()
            Cn.Dispose()

            Return False
        End If

    End Function

    '------------------------------------------------
    '--フォーム初期値に戻す                ----------
    '------------------------------------------------
    Private Sub sClear()

        txtWorkNo.Clear()
        txtWorkName.Clear()

    End Sub
End Class
