Imports System.Xml
Imports System.Data.SqlClient

Public Class T011

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T011_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー処理                            ----------
    '------------------------------------------------
    Private Sub T011_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sSearch()
            Case Keys.F2
                '更新処理
                Call sUpdate()
            Case Keys.F3
                'クリア処理
                Call sClear()
            Case Keys.F4
                '終了処理
                Me.Close()

        End Select

    End Sub

    '------------------------------------------------
    '--更新担当者ボタン押下処理            ----------
    '------------------------------------------------
    Private Sub btnUserIDSearch_Click(sender As Object, e As EventArgs) Handles btnUserIDSearch.Click
        Dim frm As New T011_1
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--検索ボタン処理                      ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '検索処理
        Call sSearch()
    End Sub

    '------------------------------------------------
    '--更新ボタン処理                      ----------
    '------------------------------------------------
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '更新処理
        Call sUpdate()
    End Sub

    '------------------------------------------------
    '--クリアボタン処理                    ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--終了ボタン処理                      ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--生産日ラジオボタン処理              ----------
    '------------------------------------------------
    Private Sub rboSearch1_CheckedChanged(sender As Object, e As EventArgs) Handles rboSearch1.CheckedChanged
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
    End Sub

    '------------------------------------------------
    '--生産番号ラジオボタン処理            ----------
    '------------------------------------------------
    Private Sub rboSearch2_CheckedChanged(sender As Object, e As EventArgs) Handles rboSearch2.CheckedChanged
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
    End Sub

    '------------------------------------------------
    '--検索処理                            ----------
    '------------------------------------------------
    Public Sub sSearch()
        Dim result As DialogResult
        'DataGirdViewを初期化
        Call sClear_DataGridView1()
        '検索前チェック処理
        result = fCheckSearch()
        If result = True Then
            'DB接続
            Call sDBConnect()
            '検索メイン処理
            result = fMainSearch()
            If result = True Then
                '正常に検索できた場合は、更新担当者を入力可とする
                GroupBox6.Enabled = True
            End If
        Else
            Call sClear()
        End If

    End Sub

    '------------------------------------------------
    '--検索前チェック処理                  ----------
    '------------------------------------------------
    Public Function fCheckSearch() As Boolean

        'ラジオボタンがオンだった場合、生産番号を確認
        If rboSearch2.Checked = True Then
            If txtCreateNo.Text.Trim = "" Then
                MessageBox.Show("生産番号が空白です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If

    End Function

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function fMainSearch() As Boolean

        Dim dtReader As SqlDataReader

        Try
            If rboSearch3.Checked = False Then
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " A.生産NO,"
                strSQL &= " A.出荷先NO, "
                strSQL &= " A.作業工程NO, "
                strSQL &= "	B.製品NO, "
                strSQL &= "	A.生産数, "
                strSQL &= " A.生産日, "
                strSQL &= " A.最終更新者, "
                strSQL &= " A.更新日, "
                strSQL &= " A.登録日 "
                strSQL &= "FROM "
                strSQL &= " CREATE_TBL A, "
                strSQL &= " WORKPROCESS_MS B "
                strSQL &= "WHERE "
                strSQL &= "     A.生産完了フラグ = 'FALSE' "
                strSQL &= "AND  A.作業工程NO = B.作業工程NO "
                If rboSearch1.Checked = True Then
                    strSQL &= "AND  A.生産日 BETWEEN "
                    strSQL &= "     '" & dtpCreateDateFrom.Text.Trim & "' AND '" & dtpCreateDateTo.Text.Trim & "' "
                ElseIf rboSearch2.Checked = True Then
                    strSQL &= "AND  A.生産NO = " & txtCreateNo.Text.Trim & " "
                End If
                strSQL &= "ORDER BY "
                strSQL &= " A.生産NO "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    dtReader.Close()

                    '一件以上あった場合は、DataGridViewに格納
                    Dim dsDataset As New DataSet
                    Dim daDataAdapter As New SqlClient.SqlDataAdapter

                    daDataAdapter.SelectCommand = New SqlClient.SqlCommand(strSQL, Cn)
                    daDataAdapter.SelectCommand.CommandTimeout = 0
                    daDataAdapter.Fill(dsDataset, "TABLE001")
                    DataGridView1.DataSource = dsDataset.Tables("TABLE001")

                    'Trim処理
                    For i = 0 To DataGridView1.RowCount - 1
                        For y = 0 To 8
                            DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim
                        Next
                    Next

                    Dim chkColumns As New DataGridViewCheckBoxColumn
                    DataGridView1.Columns.Insert(0, chkColumns)


                    'ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                    '1列目以外は編集不可
                    For i = 1 To 9
                        DataGridView1.Columns(i).ReadOnly = True
                    Next

                    Return True

                Else
                    MessageBox.Show("検索結果が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False

                End If
            Else
                MessageBox.Show("検索結果が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--更新処理                            ----------
    '------------------------------------------------
    Public Sub sUpdate()
        Dim result As Boolean
        '編集モードをやめる
        DataGridView1.EndEdit()

        '更新前チェック処理
        result = fCheckUpdate()
        If result = True Then
            'DB接続
            Call sDBConnect()
            '更新メイン処理
            Call fMainUpdate()
            'クリア処理
            Call sClear()

        End If


    End Sub

    '------------------------------------------------
    '--更新前チェック処理                      ----------
    '------------------------------------------------
    Public Function fCheckUpdate() As Boolean

        Dim dtReader As SqlDataReader

        Dim intCount As Integer 'チェックしてあるセルのカウント

        '検索処理は終わっているか確認
        If DataGridView1.DataSource IsNot Nothing Then
            Dim result As DialogResult = MessageBox.Show("更新しますか？", "更新確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                'チェックボックスのチェック数を確認
                For i = 0 To DataGridView1.RowCount - 1
                    If DataGridView1.Item(0, i).Value = True Then
                        intCount += 1
                    End If
                Next

                If intCount > 0 Then
                    'チェック数が１以上の場合は、問題なし
                    '次の更新担当者チェックに移行
                Else
                    MessageBox.Show("チェック数が０です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            Else
                Return False
            End If
        Else
            MessageBox.Show("検索結果が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        'DB接続
        Call sDBConnect()

        '更新担当者のチェック
        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 名前 "
            strSQL &= "FROM "
            strSQL &= " HUMAN_MS "
            strSQL &= "WHERE "
            strSQL &= " 名前 = '" & txtUserID.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                dtReader.Close()
                '入力した更新担当者が存在すれば、問題なし
            Else
                dtReader.Close()
                MessageBox.Show("入力した更新担当者は存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            Return False
        Finally
            Cn.Close()
            Cn.Dispose()
        End Try


        '問題なければ、TRUEを返す
        Return True



    End Function

    '------------------------------------------------
    '--更新メイン処理                      ----------
    '------------------------------------------------
    Public Function fMainUpdate() As Boolean

        Dim dtReader As SqlDataReader
        Dim intCount As Integer '更新数のカウント
        Dim intStockNO As Integer '在庫NOのMAX値取得
        Dim tran As SqlTransaction
        tran = Cn.BeginTransaction

        Try
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Item(0, i).Value = True Then
                    '生産完了フラグの更新
                    strSQL = ""
                    strSQL &= "UPDATE CREATE_TBL "
                    strSQL &= "SET "
                    strSQL &= " 生産完了フラグ = 'TRUE', "
                    strSQL &= " 最終更新者 = '" & txtUserID.Text.Trim & "', "
                    strSQL &= " 更新日 = SYSDATETIME() "
                    strSQL &= "WHERE "
                    strSQL &= "生産NO = " & DataGridView1.Item(1, i).Value & " "
                    '更新処理
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.Transaction = tran
                    cd.ExecuteNonQuery()

                    '在庫登録処理
                    '在庫テーブルのMAX値を取得
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " COALESCE(MAX(在庫NO), 0) AS 在庫NO "
                    strSQL &= "FROM "
                    strSQL &= " STOCK_TBL "
                    '取得処理
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.Transaction = tran
                    dtReader = cd.ExecuteReader
                    intStockNO = 0
                    If dtReader.HasRows Then
                        While dtReader.Read
                            intStockNO = dtReader("在庫NO")
                        End While
                        intStockNO += 1
                    End If
                    dtReader.Close()

                    '登録用SQL
                    strSQL = ""
                    strSQL &= "INSERT INTO STOCK_TBL VALUES ( "
                    strSQL &= " " & intStockNO & ", " '在庫NO
                    strSQL &= " '" & DataGridView1.Item(4, i).Value.trim & "', " '製品NO
                    strSQL &= " " & DataGridView1.Item(5, i).Value & ", " '在庫数
                    strSQL &= " '" & DataGridView1.Item(7, i).Value.trim & "', " '最終更新者
                    strSQL &= " SYSDATETIME(), " '更新日
                    strSQL &= " SYSDATETIME() " '登録日
                    strSQL &= " ) "
                    '登録処理
                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.Transaction = tran
                    cd.ExecuteNonQuery()

                    intCount += 1

                End If
            Next


            MessageBox.Show(intCount & "件更新・登録完了しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            tran.Commit()
            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            tran.Rollback()
            Return False
        Finally
            Cn.Close()
            Cn.Dispose()
        End Try

    End Function

    ''------------------------------------------------
    ''--更新メイン処理 在庫テーブル更新処理       ----
    ''------------------------------------------------
    'Public Function fMainUpdate_Stock() As Boolean

    '    Dim dtReader As SqlDataReader

    '    Try
    '        '在庫テーブルのMAX値を取得
    '        strSQL = ""
    '        strSQL &= "SELECT "
    '        strSQL &= " COALESCE(MAX(在庫NO), 0) AS 在庫NO "
    '        strSQL &= "FROM "
    '        strSQL &= " STOCK_TBL "

    '        cd.CommandText = 




    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "例外発生")
    '        Return False

    '    End Try



    'End Function

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
    '--クリア処理                    　    ----------
    '------------------------------------------------
    Public Sub sClear()

        rboSearch3.Checked = True
        dtpCreateDateFrom.Text = Date.Now
        dtpCreateDateTo.Text = Date.Now
        txtCreateNo.Clear()
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        rboSearch3.Visible = False
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        GroupBox6.Enabled = False

    End Sub

    '------------------------------------------------
    '--クリア処理DataGridViewのみ    　    ----------
    '------------------------------------------------
    Public Sub sClear_DataGridView1()
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub


End Class
