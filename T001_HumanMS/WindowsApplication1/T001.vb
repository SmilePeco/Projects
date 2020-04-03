Imports System.Data.SqlClient

Public Class T001

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
    Private Sub T001_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー操作処理          ----------
    '------------------------------------------------
    Private Sub T001_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

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
    '--社員No KeyPress処理                 ----------
    '------------------------------------------------
    Private Sub txtHumanNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHumanNo.KeyPress

        '入力文字制御(数値のみ)
        If e.KeyChar < "0"c OrElse "9"c < e.KeyChar Then
            '押されたキーが 0～9でない場合は、イベントをキャンセルする
            e.Handled = True
        End If

    End Sub

    '------------------------------------------------
    '--社員No KeyDown処理                 ----------
    '------------------------------------------------
    Private Sub txtHumanNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHumanNo.KeyDown

        '//Enterキーで検索処理
        If e.KeyData = Keys.Enter Then
            Call frmSearch()
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
    '--新規登録・更新処理                  ----------
    '------------------------------------------------
    Public Sub frmEntry()

        'DB接続処理
        Call sDBConnect()
        '新規登録・更新処理
        Call frmMainEntry()

    End Sub

    '------------------------------------------------
    '--削除処理                            ----------
    '------------------------------------------------
    Public Sub frmDelete()

        'DB接続処理
        Call sDBConnect()
        '削除処理
        Call frmMainDelete()

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
    '--フォーム初期値に戻す                ----------
    '------------------------------------------------
    Private Sub sClear()

        txtHumanNo.Enabled = True
        txtHumanNo.Clear()
        txtHumanNo.Focus()
        grpHuman.Enabled = False
        txtName.Clear()
        txtPass.Clear()
        chkAdminFLG.Checked = False

        btnSearch.Enabled = True

        btnEntry.Enabled = False
        btnEntry.Text = "F5:"

        btnDelete.Enabled = False

        lblMode.Text = ""

    End Sub

    '------------------------------------------------
    '--新規登録・更新　事前処理  　        ----------
    '------------------------------------------------
    Private Sub sgrpHumanOpen()

        'GroupBoxを入力可能にする
        grpHuman.Enabled = True
        txtName.Focus()
        txtHumanNo.Enabled = False
        btnSearch.Enabled = False
        btnEntry.Enabled = True

        If lblMode.Text = "新規作成" Then
            btnEntry.Text = "F5:新規作成"
        ElseIf lblMode.Text = "更新" Then
            btnEntry.Text = "F5:更新"

        End If

    End Sub

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function frmMainSearch() As Boolean

        Dim dtReader As SqlDataReader

        If txtHumanNo.Text.Trim <> "" Then

            Try
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= "社員No, "
                strSQL &= "名前, "
                strSQL &= "パスワード, "
                strSQL &= "管理者フラグ "
                strSQL &= "FROM "
                strSQL &= "HUMAN_MS "
                strSQL &= "WHERE "
                strSQL &= "    社員No= '" + txtHumanNo.Text + "' "
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
    '--新規登録・更新メイン処理 　          ----------
    '------------------------------------------------
    Public Function frmMainEntry() As Boolean

        Dim dtReader As SqlDataReader
        Dim strName As String
        Dim strPass As String

        Try
            If lblMode.Text.Trim = "新規作成" Then

                blnResult = frmCheckValue()
                If blnResult = False Then
                    Return False
                End If

                '新規作成（INSERT INTO）
                strSQL = ""
                strSQL &= "INSERT INTO HUMAN_MS VALUES ( "
                strSQL &= txtHumanNo.Text.Trim + ", "
                strSQL &= "'" + txtName.Text.Trim + "', "
                strSQL &= "'" + txtPass.Text + "', "
                If chkAdminFLG.Checked = True Then
                    strSQL &= "1, "
                Else
                    strSQL &= "0, "
                End If
                strSQL &= " GETDATE(), "
                strSQL &= " GETDATE() "
                strSQL &= ") "

                'SQLコマンド設定
                cd.CommandText = strSQL
                cd.Connection = Cn
                cd.ExecuteNonQuery()

                MessageBox.Show("新規登録できました", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Call sClear()

                Return True

            ElseIf lblMode.Text.Trim = "更新" Then

                blnResult = frmCheckValue()
                If blnResult = False Then
                    Return False
                End If

                '更新（UPDATE）
                strSQL = ""
                strSQL &= "UPDATE HUMAN_MS "
                strSQL &= "SET "
                strSQL &= "名前 = '" + txtName.Text.Trim + "', "
                strSQL &= "パスワード = '" + txtPass.Text + "', "
                If chkAdminFLG.Checked = True Then
                    strSQL &= "管理者フラグ = 1, "
                Else
                    strSQL &= "管理者フラグ = 0, "
                End If
                strSQL &= "更新日 = GETDATE() "
                strSQL &= "WHERE "
                strSQL &= "社員No = " + txtHumanNo.Text.Trim

                'SQLコマンド設定
                cd.CommandText = strSQL
                cd.Connection = Cn
                cd.ExecuteNonQuery()

                MessageBox.Show("更新できました", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Call sClear()

                Return True

            End If

        Catch ex As Exception

            MessageBox.Show(ex.ToString, "例外発生")
            Return False

        Finally

            Cn.Close()
            Cn.Dispose()

        End Try

        Return True

    End Function

    '------------------------------------------------
    '--削除メイン処理          　          ----------
    '------------------------------------------------
    Public Function frmMainDelete()

        If lblMode.Text = "更新" Then

            Try
                Dim drResult As DialogResult = MessageBox.Show("本当に削除しますか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)

                If drResult = DialogResult.Yes Then
                    strSQL = ""
                    strSQL &= "DELETE FROM HUMAN_MS "
                    strSQL &= "WHERE 社員No = " + txtHumanNo.Text.Trim

                    'SQLコマンド設定
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("削除できました", "削除完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Call sClear()

                    Return True

                ElseIf drResult = DialogResult.No Then

                    Return False

                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString, "例外発生")
                Return False

            Finally
                Cn.Close()
                Cn.Dispose()

            End Try

        End If

        Return True

    End Function

    '------------------------------------------------
    '--名前・パスワード確認処理　          ----------
    '------------------------------------------------
    Public Function frmCheckValue() As Boolean

        '名前が入力されているかチェック
        If txtName.Text.Trim = "" Then
            MessageBox.Show("名前が未入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return False
        End If

        'パスワードが入力されているかチェック
        If txtPass.Text.Trim = "" Then
            MessageBox.Show("パスワードが未入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPass.Focus()
            Return False
        End If

        Return True

    End Function

End Class
