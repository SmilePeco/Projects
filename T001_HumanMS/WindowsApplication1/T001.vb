Imports System.Data.SqlClient
Imports System.Xml

Public Class T001

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String
    Dim blnResult As Boolean '名前・パスワード判定用

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T001_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
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

            Case Keys.F2
                If btnEntry.Enabled = True Then
                    '//「F2登録」押下
                    '新規登録・更新処理
                    Call frmEntry()
                End If

            Case Keys.F3
                If btnDelete.Enabled = True Then
                    '//「F3削除」押下
                    '削除処理
                    Call frmDelete()
                End If

            Case Keys.F4
                '//「F4キャンセル」押下
                'クリア処理
                Call sClear()

            Case Keys.F5
                '//「F5終了」押下
                'クローズ処理
                Me.Close()

        End Select
    End Sub

    '------------------------------------------------
    '--出荷先NO 検索ボタン押下処理         ----------
    '------------------------------------------------
    Private Sub btnHumanSearch_Click(sender As Object, e As EventArgs) Handles btnHumanSearch.Click
        Dim frm As New T001_2
        frm.ShowDialog(Me)

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
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso
            e.KeyChar <> ControlChars.Back Then
            '押されたキーが 0～9でない場合は、イベントをキャンセルする
            e.Handled = True
        ElseIf e.KeyChar = Chr(13) Then
            '検索処理
            Call frmSearch()
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
        Call sDBconnect()
        '検索メイン処理()
        Call frmMainSearch()

    End Sub

    '------------------------------------------------
    '--新規登録・更新処理                  ----------
    '------------------------------------------------
    Public Sub frmEntry()

        'DB接続処理
        Call sDBconnect()
        '新規登録・更新処理
        Call frmMainEntry()

    End Sub

    '------------------------------------------------
    '--削除処理                            ----------
    '------------------------------------------------
    Public Sub frmDelete()

        'DB接続処理
        Call sDBconnect()
        '削除処理
        Call frmMainDelete()

    End Sub

    '------------------------------------------------
    '--新規登録・更新　事前処理  　        ----------
    '------------------------------------------------
    Private Sub sgrpHumanOpen()

        'GroupBoxを入力可能にする
        grpHuman.Enabled = True
        txtName.Focus()
        txtHumanNo.Enabled = False
        btnHumanSearch.Enabled = False
        btnSearch.Enabled = False
        btnEntry.Enabled = True

        If lblMode.Text = "新規作成" Then
            btnEntry.Text = "F2:新規作成"
        ElseIf lblMode.Text = "更新" Then
            btnEntry.Text = "F2:更新"

        End If

    End Sub

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function frmMainSearch() As Boolean

        Dim dtReader As SqlDataReader
        Dim intCount As Integer '社員NO連番カウント用


        Try
            If txtHumanNo.Text.Trim <> "" Then
                '//入力した社員NOが存在するか確認
                'SQL発行
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= "社員No, "
                strSQL &= "名前, "
                strSQL &= "パスワード, "
                strSQL &= "管理者フラグ "
                strSQL &= "FROM "
                strSQL &= "HUMAN_MS "
                strSQL &= "WHERE "
                strSQL &= "    社員No= '" & txtHumanNo.Text & "' "
                'SQL実行
                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    '存在した場合は「更新」モード
                    While dtReader.Read
                        txtName.Text = dtReader("名前").ToString.Trim()
                        If dtReader("管理者フラグ") = 0 Then
                            chkAdminFLG.Checked = False
                        ElseIf dtReader("管理者フラグ") = 1 Then
                            chkAdminFLG.Checked = True
                        End If
                    End While

                    dtReader.Close()

                    lblMode.Text = "更新"
                    btnDelete.Enabled = True
                    Call sgrpHumanOpen()

                    Return True

                Else
                    Dim result As DialogResult = MessageBox.Show("入力した社員NOはマスタに存在しません。" & vbCrLf & "新規作成しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                    If result = Windows.Forms.DialogResult.Yes Then
                        '存在しなかった場合は「新規作成」モード
                        dtReader.Close()
                        lblMode.Text = "新規作成"
                        Call sgrpHumanOpen()
                        Return True
                    Else
                        Return False
                    End If
                End If


            Else

                '//空白の場合は「新規作成」モード
                '//社員NOの連番で採番する
                'SQL発行
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " MAX(社員NO) AS 社員NO "
                strSQL &= "FROM "
                strSQL &= " HUMAN_MS "
                'SQL実行
                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader
                If dtReader.HasRows Then
                    While dtReader.Read
                        intCount = dtReader("社員NO")
                    End While
                    dtReader.Close()
                    intCount += 1
                Else
                    '０件の場合は、＋１のみ
                    dtReader.Close()
                    intCount += 1
                End If

                lblMode.Text = "新規作成"
                txtHumanNo.Text = intCount
                Call sgrpHumanOpen()
                Return True

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
    '--新規登録・更新メイン処理 　          ----------
    '------------------------------------------------
    Public Function frmMainEntry() As Boolean

        Try
            Dim result As DialogResult = MessageBox.Show("更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                If lblMode.Text.Trim = "新規作成" Then

                    blnResult = frmCheckValue()
                    If blnResult = False Then
                        Return False
                    End If

                    '新規作成（INSERT INTO）
                    strSQL = ""
                    strSQL &= "INSERT INTO HUMAN_MS VALUES ( "
                    strSQL &= txtHumanNo.Text.Trim & ", " '//社員NO
                    strSQL &= "'" & txtName.Text.Trim & "', " '//名前
                    strSQL &= "'" & txtPass.Text & "', " '//パスワード
                    strSQL &= " '', " '//更新担当者名
                    If chkAdminFLG.Checked = True Then
                        strSQL &= "1, " '//管理者フラグ
                    Else
                        strSQL &= "0, " '//管理者フラグ
                    End If
                    strSQL &= " SYSDATETIME(), " '//更新日
                    strSQL &= " SYSDATETIME() " '//登録日
                    strSQL &= ") "

                    'SQLコマンド設定
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("新規登録しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
                    strSQL &= "名前 = '" & txtName.Text.Trim & "', "
                    strSQL &= "パスワード = '" & txtPass.Text & "', "
                    If chkAdminFLG.Checked = True Then
                        strSQL &= "管理者フラグ = 1, "
                    Else
                        strSQL &= "管理者フラグ = 0, "
                    End If
                    strSQL &= "更新日 = SYSDATETIME() "
                    strSQL &= "WHERE "
                    strSQL &= "社員No = " & txtHumanNo.Text.Trim

                    'SQLコマンド設定
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("更新完了しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Call sClear()

                    Return True

                Else
                    'ありえないが、FALSEを返す
                    Return False

                End If
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
    '--削除メイン処理          　          ----------
    '------------------------------------------------
    Public Function frmMainDelete()

        If lblMode.Text = "更新" Then

            Try
                Dim drResult As DialogResult = MessageBox.Show("本当に削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)

                If drResult = DialogResult.Yes Then
                    strSQL = ""
                    strSQL &= "DELETE FROM HUMAN_MS "
                    strSQL &= "WHERE 社員No = " & txtHumanNo.Text.Trim

                    'SQLコマンド設定
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("削除完了しました。", "削除完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    '------------------------------------------------
    '--クリア処理                          ----------
    '------------------------------------------------
    Private Sub sClear()

        txtHumanNo.Enabled = True
        txtHumanNo.Clear()
        txtHumanNo.Focus()
        btnHumanSearch.Enabled = True
        grpHuman.Enabled = False
        txtName.Clear()
        txtPass.Clear()
        chkAdminFLG.Checked = False

        btnSearch.Enabled = True

        btnEntry.Enabled = False
        btnEntry.Text = "F2:"

        btnDelete.Enabled = False

        lblMode.Text = ""

    End Sub

    '------------------------------------------------
    '--DB接続の開始                        ----------
    '------------------------------------------------
    Public Function sDBconnect() As Boolean

        Dim strServer As String
        Dim strUserID As String
        Dim strPassWord As String
        Dim strDatabaseName As String

        Dim xmlNode As XmlNodeList
        Dim strFileAdress As String = "C:\SQLServer\DBConnect.xml"

        Dim xmlDoc As New XmlDocument()

        Try
            If System.IO.File.Exists(strFileAdress) Then
                xmlDoc.Load(strFileAdress)
                'サーバ名の取得
                'サーバ名の取得
                xmlNode = xmlDoc.GetElementsByTagName("Server")
                strServer = xmlNode.Item(0).InnerText
                'ユーザ名の取得
                xmlNode = xmlDoc.GetElementsByTagName("UserID")
                strUserID = xmlNode.Item(0).InnerText
                'パスワードの取得
                xmlNode = xmlDoc.GetElementsByTagName("Password")
                strPassWord = xmlNode.Item(0).InnerText
                'データベース名の取得
                xmlNode = xmlDoc.GetElementsByTagName("DatabaseName")
                strDatabaseName = xmlNode.Item(0).InnerText

                'DB接続
                strConnect = "Server=" & strServer & ";"
                strConnect &= "User ID=" & strUserID & ";"
                strConnect &= "Password=" & strPassWord & ";"
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




End Class


