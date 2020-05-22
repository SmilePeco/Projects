Imports System.Xml
Imports System.Data.SqlClient

Public Class T020

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理          ----------
    '------------------------------------------------
    Private Sub T020_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--ファンクションキー押下処理          ----------
    '------------------------------------------------
    Private Sub T020_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sShipmentMS_Search()
            Case Keys.F2
                If btnUpdate.Enabled = True Then
                    '更新処理
                    Call sShipmentMS_Update()
                End If
            Case Keys.F3
                If btnDelete.Enabled = True Then
                    '削除処理
                    Call sShipmentMS_Delete()
                End If
            Case Keys.F4
                'クリア処理
                Call sClear()
            Case Keys.F5
                '終了処理
                Me.Close()

        End Select

    End Sub

    '------------------------------------------------
    '--検索ボタン処理                      ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '検索処理
        Call sShipmentMS_Search()
    End Sub

    '------------------------------------------------
    '--更新ボタン処理                      ----------
    '------------------------------------------------
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '更新処理
        Call sShipmentMS_Update()
    End Sub

    '------------------------------------------------
    '--削除ボタン処理                      ----------
    '------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '削除処理
        Call sShipmentMS_Delete()
    End Sub

    '------------------------------------------------
    '--クリアボタン処理                    ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--終了ボタン処理                    ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--出荷先NO エンターキー押下処理       ----------
    '------------------------------------------------
    Private Sub txtShipmentNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShipmentNo.KeyPress
        If e.KeyChar = Chr(13) Then
            '検索処理
            Call sShipmentMS_Search()
        End If
    End Sub

    '------------------------------------------------
    '--出荷先NO検索ボタン押下処理          ----------
    '------------------------------------------------
    Private Sub btnShipmentSearch_Click(sender As Object, e As EventArgs) Handles btnShipmentSearch.Click
        Dim frm As New T020_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--出荷先NO 検索処理                   ----------
    '------------------------------------------------
    Public Sub sShipmentMS_Search()
        '０埋め処理
        If txtShipmentNo.Text.Trim <> "" Then
            txtShipmentNo.Text = txtShipmentNo.Text.PadLeft(3, "0")
        End If
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fShipmentMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--出荷先NO 検索メイン処理             ----------
    '------------------------------------------------
    Public Function fShipmentMS_MainSearch()

        Dim dtReader As SqlDataReader
        Dim result As DialogResult
        Dim intCount As Integer '出荷先NO連番取得用

        Try
            '//空欄の場合は、新規作成モード
            If txtShipmentNo.Text.Trim <> "" Then
                'SQL発行
                '入力した出荷先NOが存在するかチェック
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 出荷先NO, "
                strSQL &= " 出荷先名, "
                strSQL &= " 出荷先担当者名, "
                strSQL &= " 出荷先電話番号, "
                strSQL &= " 出荷先住所１, "
                strSQL &= " 出荷先住所２ "
                strSQL &= "FROM "
                strSQL &= " SHIPMENT_MS "
                strSQL &= "WHERE "
                strSQL &= " 出荷先NO = '" & txtShipmentNo.Text.Trim & "' "
                'SQL実行
                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    '//更新モード
                    While dtReader.Read
                        txtShipmentName.Text = dtReader("出荷先名").ToString.Trim
                        txtOfficer.Text = dtReader("出荷先担当者名").ToString.Trim
                        txtTELL.Text = dtReader("出荷先担当者名").ToString.Trim
                        txtAdress1.Text = dtReader("出荷先住所１").ToString.Trim
                        txtAdress2.Text = dtReader("出荷先住所２").ToString.Trim
                    End While

                    dtReader.Close()
                    lblMode.Text = "更新"

                    '出荷先NOは入力不可
                    txtShipmentNo.Enabled = False
                    'それ以外は入力可
                    txtShipmentName.Enabled = True
                    txtOfficer.Enabled = True
                    txtTELL.Enabled = True
                    txtAdress1.Enabled = True
                    txtAdress2.Enabled = True
                    '更新削除も使用可
                    btnUpdate.Enabled = True
                    btnDelete.Enabled = True

                    txtShipmentName.Focus()

                    Return True

                Else
                    '//存在しない出荷先NOを入力
                    '//新規作成モード
                    result = MessageBox.Show("入力した出荷先NOはマスタに存在しません。" & vbCrLf & "新規作成しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                    If result = Windows.Forms.DialogResult.Yes Then
                        dtReader.Close()

                        '//新規作成モード
                        '出荷先NOは入力不可
                        txtShipmentNo.Enabled = False
                        'それ以外は入力可
                        txtShipmentName.Enabled = True
                        txtOfficer.Enabled = True
                        txtTELL.Enabled = True
                        txtAdress1.Enabled = True
                        txtAdress2.Enabled = True
                        '更新のみ使用可
                        btnUpdate.Enabled = True

                        txtShipmentName.Focus()

                        Return True
                    Else
                        dtReader.Close()
                        Return False
                    End If
                End If

            Else
                '//新規作成モード
                '空欄だった場合は、出荷先NOの連番を取得
                'SQL発行
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " MAX(出荷先NO) AS 出荷先NO "
                strSQL &= "FROM "
                strSQL &= " SHIPMENT_MS "
                'SQL実行
                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader
                If dtReader.HasRows Then
                    While dtReader.Read
                        intCount = dtReader("出荷先NO")
                    End While
                    dtReader.Close()
                    intCount += 1
                Else
                    dtReader.Close()
                    '０件の場合は、＋１のみ
                    intCount += 1
                End If

                '０埋め処理
                txtShipmentNo.Text = intCount.ToString.PadLeft(3, "0")

                '出荷先NOは入力不可
                txtShipmentNo.Enabled = False
                'それ以外は入力可
                txtShipmentName.Enabled = True
                txtOfficer.Enabled = True
                txtTELL.Enabled = True
                txtAdress1.Enabled = True
                txtAdress2.Enabled = True
                '更新のみ使用可
                btnUpdate.Enabled = True

                txtShipmentName.Focus()

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
    '--更新処理                            ----------
    '------------------------------------------------
    Public Sub sShipmentMS_Update()
        Dim result As Boolean
        'DB接続
        Call sDBConnect()
        '更新メイン処理
        result = fShipmentMS_MainUpdate()
        If result = True Then
            Call sClear()
        End If

    End Sub

    '------------------------------------------------
    '--更新メイン処理                      ----------
    '------------------------------------------------
    Public Function fShipmentMS_MainUpdate() As Boolean

        Try
            Dim result As DialogResult = MessageBox.Show("更新しますか？", "更新確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                If lblMode.Text = "新規作成" Then
                    '//新規作成モードの場合
                    'SQL発行
                    strSQL = ""
                    strSQL &= "INSERT INTO SHIPMENT_MS VALUES "
                    strSQL &= "( "
                    strSQL &= "'" & txtShipmentNo.Text.Trim & "', " '出荷先NO
                    strSQL &= "'" & txtShipmentName.Text.Trim & "', " '出荷先名
                    strSQL &= "'" & txtOfficer.Text.Trim & "', " '出荷先担当者
                    strSQL &= "'" & txtTELL.Text.Trim & "', " '出荷先電話番号
                    strSQL &= "'" & txtAdress1.Text.Trim & "'," '出荷先住所１
                    strSQL &= "'" & txtAdress2.Text.Trim & "', " '出荷先住所２
                    strSQL &= "SYSDATETIME(), " '出荷先更新日
                    strSQL &= "SYSDATETIME() " '出荷先登録日
                    strSQL &= ") "
                    'SQL実行
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("登録完了しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Return True

                ElseIf lblMode.Text = "更新" Then
                    '//更新モードの場合
                    'SQL発行
                    strSQL = ""
                    strSQL &= "UPDATE SHIPMENT_MS "
                    strSQL &= "SET "
                    strSQL &= "出荷先名 = '" & txtShipmentName.Text.Trim & "', " '出荷先名
                    strSQL &= "出荷先担当者名 = '" & txtOfficer.Text.Trim & "', " '出荷先担当者名
                    strSQL &= "出荷先電話番号 = '" & txtTELL.Text.Trim & "', " '出荷先電話番号
                    strSQL &= "出荷先住所１ = '" & txtAdress1.Text.Trim & "', " '出荷先住所１
                    strSQL &= "出荷先住所２ = '" & txtAdress2.Text.Trim & "', " '出荷先住所２
                    strSQL &= "更新日 = SYSDATETIME() " '更新日
                    strSQL &= "WHERE 出荷先NO = '" & txtShipmentNo.Text.Trim & "' "
                    'SQL実行
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    MessageBox.Show("更新完了しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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
    '--削除処理                            ----------
    '------------------------------------------------
    Public Sub sShipmentMS_Delete()
        Dim result As Boolean
        'DB接続
        Call sDBConnect()
        '削除チェック処理
        result = fShipmentMS_CheckDelete()
        If result = True Then
            'DB接続
            Call sDBConnect()
            '削除メイン処理
            result = fShipmentMS_MainDelete()
            If result = True Then
                Call sClear()
            End If

        End If

    End Sub

    '------------------------------------------------
    '--削除チェック処理                    ----------
    '------------------------------------------------
    Public Function fShipmentMS_CheckDelete() As Boolean

        Dim dtReader As SqlDataReader

        Try
            '出荷先NOがマスタに存在しているか、念の為確認する
            'SQL発行
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 出荷先NO "
            strSQL &= "FROM "
            strSQL &= " SHIPMENT_MS "
            strSQL &= "WHERE "
            strSQL &= " 出荷先NO = '" & txtShipmentNo.Text.Trim & "' "
            'SQL実行
            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                dtReader.Close()
                Return True
            Else
                MessageBox.Show("入力した出荷先NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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

    '------------------------------------------------
    '--削除メイン処理                      ----------
    '------------------------------------------------
    Public Function fShipmentMS_MainDelete() As Boolean

        Try
            Dim result As DialogResult = MessageBox.Show("削除しますか？", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                '削除処理
                'SQL発行
                strSQL = ""
                strSQL &= "DELETE FROM SHIPMENT_MS "
                strSQL &= "WHERE 出荷先NO = '" & txtShipmentNo.Text.Trim & "' "
                'SQL実行
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
    '--クリア処理                          ----------
    '------------------------------------------------
    Public Sub sClear()
        'テキストボックス関連
        txtShipmentNo.Clear()
        txtShipmentName.Clear()
        txtOfficer.Clear()
        txtTELL.Clear()
        txtAdress1.Clear()
        txtAdress2.Clear()

        'ENABLE設定
        '出荷先NOは入力可
        txtShipmentNo.Enabled = True
        'それ以外は入力不可
        txtShipmentName.Enabled = False
        txtOfficer.Enabled = False
        txtTELL.Enabled = False
        txtAdress1.Enabled = False
        txtAdress2.Enabled = False
        '更新削除は使用不可
        btnUpdate.Enabled = False
        btnDelete.Enabled = False

        'モード関連
        lblMode.Text = "新規作成"

        'フォーカス設定
        txtShipmentNo.Focus()

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
