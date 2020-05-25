Imports System.Data.SqlClient
Imports System.Xml

Public Class T002

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

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
                    '０埋め処理
                    If txtProcessNo.Text.Trim <> "" Then
                        txtProcessNo.Text = txtProcessNo.Text.PadLeft(3, "0")

                    End If

                    '検索処理
                    Call frmSearch()
                End If

            Case Keys.F2
                If btnEntry.Enabled = True Then
                    '//「F2登録」押下
                    '新規登録・更新処理
                    If lblMode.Text = "新規作成" Then
                        '新規作成の場合
                        Call frmEntry(1)

                    Else
                        '更新の場合
                        Call frmEntry(2)

                    End If
                End If

            Case Keys.F3
                If btnDelete.Enabled = True Then
                    '//「F3削除」押下
                    'DB接続処理
                    Call sDBConnect()
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
        If lblMode.Text = "新規作成" Then
            '新規作成の場合
            Call frmEntry(1)

        Else
            '更新の場合
            Call frmEntry(2)

        End If

    End Sub

    '------------------------------------------------
    '--F6削除ボタン押下処理                ----------
    '------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        'DB接続処理
        Call sDBConnect()
        '削除処理
        Call frmDelete()

    End Sub

    '------------------------------------------------
    '--F11クリアボタン押下処理             ----------
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
    '--工程No KeyPress処理                 ----------
    '------------------------------------------------
    Private Sub txtProcessNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtProcessNo.KeyPress

        '入力文字制御(数値のみ)
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso _
            e.KeyChar <> ControlChars.Back Then
            '押されたキーが 0～9でない場合は、イベントをキャンセルする
            e.Handled = True
        End If

    End Sub

    '------------------------------------------------
    '--工程No Leave処理                 ----------
    '------------------------------------------------
    Private Sub txtProcessNo_Leave(sender As Object, e As EventArgs) Handles txtProcessNo.Leave

        '０埋め処理
        If txtProcessNo.Text.Trim <> "" Then
            txtProcessNo.Text = txtProcessNo.Text.PadLeft(3, "0")

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
    '--更新処理                            ----------
    '--intMode:                            ----------
    '--1=新規作成                          ----------
    '--2=更新                              ----------
    '------------------------------------------------
    Public Sub frmEntry(intMode As Integer)

        'DB接続処理
        Call sDBConnect()
        '新規作成・更新メイン処理()
        If intMode = 1 Then
            '新規作成の場合
            Call frmMainInsert()

        Else
            '更新の場合
            Call frmMainEntry()

        End If
    End Sub

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function frmMainSearch() As Boolean

        Dim dtReader As SqlDataReader

        If txtProcessNo.Text.Trim <> "" Then

            Try
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 工程NO, "
                strSQL &= " 工程名 "
                strSQL &= "FROM "
                strSQL &= " PROCESS_MS "
                strSQL &= "WHERE "
                strSQL &= " 工程NO = '" & txtProcessNo.Text.Trim & "'"

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader()

                If dtReader.HasRows Then
                    lblMode.Text = "更新"
                    txtProcessNo.Enabled = False
                    btnEntry.Enabled = True
                    btnDelete.Enabled = True
                    While dtReader.Read()
                        '工程名の取得
                        txtProcessName.Text = CStr(dtReader("工程名")).Trim

                    End While


                    Return True
                Else
                    lblMode.Text = "新規作成"
                    btnEntry.Enabled = True
                    Return True

                End If

                dtReader.Close()

            Catch ex As Exception
                MessageBox.Show(ex.ToString, "例外発生")
                Return False

            Finally
                Cn.Close()
                Cn.Dispose()


            End Try

        Else
            Cn.Close()
            Cn.Dispose()

            Return False
        End If

    End Function

    '------------------------------------------------
    '--更新メイン処理                      ----------
    '------------------------------------------------
    Public Function frmMainEntry() As Boolean

        If txtProcessNo.Text.Trim <> "" Then
            Try
                Dim result As DialogResult = MessageBox.Show("更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                If result = Windows.Forms.DialogResult.Yes Then
                    strSQL = ""
                    strSQL &= "UPDATE PROCESS_MS "
                    strSQL &= "SET 工程名 = '" & txtProcessName.Text.Trim & "', "
                    strSQL &= "    更新日 = SYSDATETIME() "
                    strSQL &= "WHERE 工程NO = '" & txtProcessNo.Text.Trim & "'"

                    'SQLの実行
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("更新完了しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Call sClear()

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

        Else
            '工程NOが未記入の場合
            MessageBox.Show("工程NOが未記入です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        End If

    End Function

    '------------------------------------------------
    '--新規作成メイン処理                  ----------
    '------------------------------------------------
    Public Function frmMainInsert() As Boolean

        Dim dtReader As SqlDataReader
        Dim intMaxCount As Integer 'PROCESS_MSのMAX値を取得用
        Dim strProcessNo As String 'MAX値から＋１した値を格納用

        '//工程NO空白で登録は、連番で登録する
        '//空白以外で登録は、重複していなかったら登録する
        Try
            Dim result As DialogResult = MessageBox.Show("新規登録しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                If txtProcessNo.Text.Trim <> "" Then
                    '//空白以外で登録
                    '//既に登録されていないかチェック
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " 工程NO "
                    strSQL &= "FROM "
                    strSQL &= " PROCESS_MS "
                    strSQL &= "WHERE "
                    strSQL &= " 工程NO ='" & txtProcessNo.Text.Trim & "' "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    dtReader = cd.ExecuteReader

                    '一致する場合はエラー
                    If dtReader.Read = False Then
                        dtReader.Close()

                        'INSERTのSQL
                        strSQL = ""
                        strSQL &= "INSERT INTO PROCESS_MS VALUES "
                        strSQL &= " ( "
                        strSQL &= " '" & txtProcessNo.Text.Trim & "', "
                        strSQL &= " '" & txtProcessName.Text.Trim & "', "
                        strSQL &= " SYSDATETIME(), "
                        strSQL &= " SYSDATETIME() "
                        strSQL &= " ) "

                        cd.CommandText = strSQL
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()

                        MessageBox.Show("登録完了しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                        Call sClear()

                        Return True

                    Else
                        MessageBox.Show("その工程NOは既に登録されています。" & vbCrLf & "入力し直してください。 ", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False

                    End If

                Else
                    '//空白で登録
                    '//MAX値を取得
                    'PROCESS_MSのMAX値を取得するSQL
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " MAX(工程NO) AS 工程NO "
                    strSQL &= "FROM "
                    strSQL &= " PROCESS_MS "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    dtReader = cd.ExecuteReader

                    'SQLを取得
                    While dtReader.Read()
                        intMaxCount = dtReader("工程NO")
                        intMaxCount += 1
                    End While

                    dtReader.Close()
                    'MAX値＋１した値を文字列化
                    strProcessNo = CStr(intMaxCount).PadLeft(3, "0")

                    '//INSERT処理
                    strSQL = ""
                    strSQL &= "INSERT INTO PROCESS_MS VALUES "
                    strSQL &= " ( "
                    strSQL &= " '" & strProcessNo & "', "
                    strSQL &= " '" & txtProcessName.Text.Trim & "', "
                    strSQL &= " SYSDATETIME(), "
                    strSQL &= " SYSDATETIME() "
                    strSQL &= " ) "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("登録が完了しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Return True

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
    '--削除メイン処理                      ----------
    '------------------------------------------------
    Public Function frmDelete()

        Dim result As DialogResult = MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
        Try
            If result = Windows.Forms.DialogResult.Yes Then

                'SQL生成
                strSQL = ""
                strSQL &= "DELETE FROM PROCESS_MS "
                strSQL &= "WHERE 工程NO = '" & txtProcessNo.Text.Trim & "' "

                cd.CommandText = strSQL
                cd.Connection = Cn
                cd.ExecuteNonQuery()

                MessageBox.Show("削除完了しました。", "削除完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Call sClear()

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
    '--クリア処理                          ----------
    '------------------------------------------------
    Private Sub sClear()

        lblMode.Text = "新規作成"

        txtProcessNo.Clear()
        txtProcessNo.Enabled = True
        txtProcessName.Clear()

        btnDelete.Enabled = False

    End Sub

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

End Class
