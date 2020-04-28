Imports System.Xml
Imports System.Data.SqlClient

Public Class T006

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '---------------------------------------------
    '---Load処理                             ---
    '---------------------------------------------
    Private Sub T006_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー操作処理          ----------
    '------------------------------------------------
    Private Sub T006_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode

            Case Keys.F1
                '検索処理
                Call sSearch()

            Case Keys.F2
                '新規登録・更新処理
                If btnUpdate.Enabled = True Then
                    Call sUpdate()
                End If

            Case Keys.F3
                '削除処理
                If btnDelete.Enabled = True Then
                    Call sDelete()
                End If

            Case Keys.F4
                'クリア処理
                Call sClear()

            Case Keys.F5
                '終了処理
                Me.Close()

        End Select

    End Sub

    '---------------------------------------------
    '---検索ボタン Click処理                   ---
    '---------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '検索処理
        Call sSearch()
    End Sub

    '---------------------------------------------
    '---更新ボタン Click処理                   ---
    '---------------------------------------------
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '新規登録・更新処理
        Call sUpdate()
    End Sub

    '---------------------------------------------
    '---削除ボタン Click処理                   ---
    '---------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '削除処理
        Call sDelete()
    End Sub

    '---------------------------------------------
    '---クリアボタン Click処理              　 ---
    '---------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'クリア処理
        Call sClear()
    End Sub

    '---------------------------------------------
    '---終了ボタン Click処理                   ---
    '---------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '---------------------------------------------
    '---受注先NO KeyPress処理                  ---
    '---------------------------------------------
    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then

            e.Handled = True

        End If

    End Sub

    '---------------------------------------------
    '---受注先NO Leave処理                  ---
    '---------------------------------------------
    Private Sub txtOrderNo_Leave(sender As Object, e As EventArgs) Handles txtOrderNo.Leave
        '０埋め処理
        If txtOrderNo.Text.Trim <> "" Then
            txtOrderNo.Text = txtOrderNo.Text.PadLeft(3, "0")
        End If
    End Sub

    '---------------------------------------------
    '---検索処理                               ---
    '---------------------------------------------
    Public Sub sSearch()
        Dim result As Boolean
        '０埋め処理
        If txtOrderNo.Text.Trim <> "" Then
            txtOrderNo.Text = txtOrderNo.Text.PadLeft(3, "0")
        End If
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        result = fMainSearch()
        '結果TRUEならば、登録・更新可能とする
        If result = True Then
            btnUpdate.Enabled = True
            GroupBox2.Enabled = True
            txtOrderNo.Enabled = False
            If txtOrderNo.Text.Trim <> "" Then
                btnDelete.Enabled = True
            End If
        End If

    End Sub

    '---------------------------------------------
    '---検索メイン処理                         ---
    '---------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            '//空白以外は検索処理。見つからない場合はそのまま新規登録
            '//空白だった場合は、そのまま新規登録
            If txtOrderNo.Text.Trim <> "" Then
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 受注先名, "
                strSQL &= " 受注先担当者名, "
                strSQL &= " 受注先電話番号, "
                strSQL &= " 受注先住所１, "
                strSQL &= " 受注先住所２ "
                strSQL &= "FROM "
                strSQL &= " ORDER_MS "
                strSQL &= "WHERE "
                strSQL &= " 受注先NO ='" + txtOrderNo.Text.Trim + "' "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader()

                If dtReader.HasRows Then
                    While dtReader.Read
                        txtOrderName.Text = CStr(dtReader("受注先名")).Trim
                        txtOfficer.Text = CStr(dtReader("受注先担当者名")).Trim
                        txtTELL.Text = CStr(dtReader("受注先電話番号")).Trim
                        txtAdress1.Text = CStr(dtReader("受注先住所１")).Trim
                        txtAdress2.Text = CStr(dtReader("受注先住所２")).Trim

                        lblMode.Text = "更新"

                    End While

                End If

                dtReader.Close()
                Return True


            Else
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

    '---------------------------------------------
    '---新規登録、更新処理               　　　---
    '---------------------------------------------
    Public Sub sUpdate()
        Dim result As DialogResult
        'DB接続
        Call sDBConnect()
        '新規登録、更新処理
        result = fMainUpdate()
        '結果がTRUEの場合は、クリア処理
        If result = True Then
            Call sClear()
        End If

    End Sub

    '---------------------------------------------
    '---新規登録、更新メイン処理               ---
    '---------------------------------------------
    Public Function fMainUpdate()

        Dim dtReader As SqlDataReader
        Dim intMaxCount As Integer 'ORDER_MSの最大値を取得
        Dim strMaxCount As String

        Try
            '最終確認
            Dim result As DialogResult = MessageBox.Show("更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                '空白以外は０埋め新規登録か更新
                '空白だった場合は、新規登録
                If txtOrderNo.Text.Trim <> "" Then
                    '既に登録されている受注先NOか確認する
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " 受注先NO "
                    strSQL &= "FROM "
                    strSQL &= " ORDER_MS "
                    strSQL &= "WHERE "
                    strSQL &= " 受注先NO = '" + txtOrderNo.Text.Trim + "' "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    dtReader = cd.ExecuteReader

                    If dtReader.HasRows Then
                        '//既に登録されている場合はUPDATE
                        dtReader.Close()

                        strSQL = ""
                        strSQL &= "UPDATE ORDER_MS "
                        strSQL &= "SET "
                        strSQL &= " 受注先名 = '" + txtOrderName.Text.Trim + "', "
                        strSQL &= " 受注先担当者名 = '" + txtOfficer.Text.Trim + "', "
                        strSQL &= " 受注先電話番号 = '" + txtTELL.Text.Trim + "', "
                        strSQL &= " 受注先住所１ = '" + txtAdress1.Text.Trim + "', "
                        strSQL &= " 受注先住所２ = '" + txtAdress2.Text.Trim + "' , "
                        strSQL &= " 更新日 = SYSDATETIME() "
                        strSQL &= "WHERE "
                        strSQL &= " 受注先NO = '" + txtOrderNo.Text.Trim + "' "

                        cd.CommandText = strSQL
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()

                        MessageBox.Show("更新完了しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                        Return True

                    Else
                        '//登録されていない場合はINSERT
                        dtReader.Close()

                        strSQL = ""
                        strSQL &= "INSERT INTO ORDER_MS VALUES ( "
                        strSQL &= " '" + txtOrderNo.Text.Trim + "', "
                        strSQL &= " '" + txtOrderName.Text.Trim + "', "
                        strSQL &= " '" + txtOfficer.Text.Trim + "', "
                        strSQL &= " '" + txtTELL.Text.Trim + "', "
                        strSQL &= " '" + txtAdress1.Text.Trim + "', "
                        strSQL &= " '" + txtAdress2.Text.Trim + "', "
                        strSQL &= " SYSDATETIME(), "
                        strSQL &= " SYSDATETIME()  "
                        strSQL &= " ) "

                        cd.CommandText = strSQL
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()

                        MessageBox.Show("登録完了しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                        Call sClear()

                        Return True

                    End If

                Else
                    '//連番で登録
                    'ORDER_NOの最大値を取得
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " MAX(受注先NO) AS 受注先NO "
                    strSQL &= "FROM "
                    strSQL &= " ORDER_MS "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    dtReader = cd.ExecuteReader

                    If dtReader.HasRows Then
                        While dtReader.Read
                            intMaxCount = dtReader("受注先NO")
                            intMaxCount += 1

                        End While
                        strMaxCount = CStr(intMaxCount).PadLeft(3, "0")
                    Else
                        strMaxCount = "001"
                    End If
                    dtReader.Close()

                    '//INSERT文の作成
                    strSQL = ""
                    strSQL &= "INSERT INTO ORDER_MS VALUES ( "
                    strSQL &= " '" + strMaxCount + "',"
                    strSQL &= " '" + txtOrderName.Text.Trim + "', "
                    strSQL &= " '" + txtOfficer.Text.Trim + "', "
                    strSQL &= " '" + txtTELL.Text.Trim + "', "
                    strSQL &= " '" + txtAdress1.Text.Trim + "', "
                    strSQL &= " '" + txtAdress2.Text.Trim + "', "
                    strSQL &= " SYSDATETIME(), "
                    strSQL &= " SYSDATETIME()  "
                    strSQL &= " ) "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("登録完了しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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
    '-削除処理                             ----------
    '------------------------------------------------
    Public Sub sDelete()

        Dim result As DialogResult
        'DB接続
        Call sDBConnect()
        '削除処理
        result = fMainDelete()
        '結果がTRUEの場合は、クリア処理
        If result = True Then
            Call sClear()
        End If


    End Sub

    '------------------------------------------------
    '-削除メイン処理                       ----------
    '------------------------------------------------
    Public Function fMainDelete()

        Try
            Dim result As DialogResult = MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                '削除SQL
                strSQL = ""
                strSQL &= "DELETE FROM ORDER_MS "
                strSQL &= "WHERE 受注先NO = '" + txtOrderNo.Text.Trim + "' "

                cd.CommandText = strSQL
                cd.Connection = Cn
                cd.ExecuteNonQuery()

                MessageBox.Show("削除完了しました。", "削除完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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

    '---------------------------------------------
    '---クリア処理                             ---
    '---------------------------------------------
    Public Sub sClear()

        lblMode.Text = "新規作成"

        txtOrderNo.Clear()
        txtOrderName.Clear()
        txtOfficer.Clear()
        txtTELL.Clear()
        txtAdress1.Clear()
        txtAdress2.Clear()

        txtOrderNo.Enabled = True

        btnUpdate.Enabled = False
        btnDelete.Enabled = False
        GroupBox2.Enabled = False

    End Sub







End Class
