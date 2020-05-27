Imports System.Xml
Imports System.Data.SqlClient

Public Class T014

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String


    '------------------------------------------------
    '--Load処理                      　    ----------
    '------------------------------------------------
    Private Sub T014_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー処理        　    ----------
    '------------------------------------------------
    Private Sub T014_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

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
    '-検索ボタン押下処理             　    ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        '検索処理
        Call sSearch()
    End Sub

    '------------------------------------------------
    '-更新ボタン押下処理             　    ----------
    '------------------------------------------------
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
        '更新処理
        Call sUpdate()
    End Sub

    '------------------------------------------------
    '-クリアボタン押下処理           　    ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '-終了ボタン押下処理             　    ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs)
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--出荷先NO ボタン押下処理       　    ----------
    '------------------------------------------------
    Private Sub btnShipmentSearch_Click(sender As Object, e As EventArgs) Handles btnShipmentSearch.Click
        Dim frm As New T014_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--更新担当者 ボタン押下処理     　    ----------
    '------------------------------------------------
    Private Sub btnUserIDSearch_Click(sender As Object, e As EventArgs)
        Dim frm As New T014_3
        frm.ShowDialog(Me)
    End Sub

    '------------------------------------------------
    '--出荷先NOラジオボタンチェック処理    ----------
    '------------------------------------------------
    Private Sub rboShipment01_CheckedChanged(sender As Object, e As EventArgs) Handles rboShipment01.CheckedChanged
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
        GroupBox4.Enabled = False
    End Sub

    '------------------------------------------------
    '--受注番号ラジオボタンチェック処理    ----------
    '------------------------------------------------
    Private Sub rboShipment02_CheckedChanged(sender As Object, e As EventArgs) Handles rboShipment02.CheckedChanged
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
        GroupBox4.Enabled = False
    End Sub

    '------------------------------------------------
    '--出荷番号ラジオボタンチェック処理    ----------
    '------------------------------------------------
    Private Sub rboShipment03_CheckedChanged(sender As Object, e As EventArgs) Handles rboShipment03.CheckedChanged
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        GroupBox4.Enabled = True

    End Sub

    '------------------------------------------------
    '--出荷先NO Leave処理            　    ----------
    '------------------------------------------------
    Private Sub txtShipmentMSNo_Leave(sender As Object, e As EventArgs) Handles txtShipmentMSNo.Leave
        '検索処理
        Call sShipmentMSNO_Search()

    End Sub

    '------------------------------------------------
    '--出荷一覧 検索処理                   ----------
    '------------------------------------------------
    Public Sub sSearch()
        Dim result As Boolean
        Me.ActiveControl = Nothing
        'クリア処理
        Call sClear_DataGridView()
        'チェック処理
        result = fCheckSearch()
        If result = True Then
            'DB接続
            Call sDBconnect()
            '検索メイン処理
            Call fMainSearch()

        End If

    End Sub

    '------------------------------------------------
    '--出荷一覧 検索チェック処理           ----------
    '------------------------------------------------
    Public Function fCheckSearch() As Boolean

        If rboShipment04.Checked = True Then
            MessageBox.Show("検索条件が選択されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        '問題なければTRUE
        Return True

    End Function

    '------------------------------------------------
    '--出荷一覧 検索メイン処理             ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " A.出荷NO, "
            strSQL &= " A.出荷先NO, "
            strSQL &= " B.出荷先名, "
            strSQL &= " A.受注NO, "
            strSQL &= " A.作業工程NO, "
            strSQL &= " A.出荷数, "
            strSQL &= " A.出荷日, "
            strSQL &= " A.更新日 "
            strSQL &= "FROM "
            strSQL &= " SHIPMENT_TBL A, "
            strSQL &= " SHIPMENT_MS B "
            strSQL &= "WHERE "
            strSQL &= "    A.出荷先NO = B.出荷先NO "
            strSQL &= "AND A.出荷チェックフラグ = 0 "
            If rboShipment01.Checked = True Then
                strSQL &= "AND A.出荷先NO = '" & txtShipmentMSNo.Text.Trim & "' "
                strSQL &= "AND A.出荷日 BETWEEN "
                strSQL &= "    '" & dtpShipmentDateFrom.Text.Trim & "' AND '" & dtpShipmentDateTo.Text.Trim & "' "
            ElseIf rboShipment02.Checked = True Then
                strSQL &= "AND A.受注NO = " & txtOrderNO.Text.Trim & " "
            ElseIf rboShipment03.Checked = True Then
                strSQL &= "AND A.出荷NO = " & txtShipmentNo.Text.Trim & " "
            End If

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                dtReader.Close()

                Dim dsDataset As New DataSet
                Dim daDataAdapter As New SqlClient.SqlDataAdapter
                daDataAdapter.SelectCommand = New SqlClient.SqlCommand(strSQL, Cn)
                daDataAdapter.SelectCommand.CommandTimeout = 0
                daDataAdapter.Fill(dsDataset, "TABLE001")
                DataGridView1.DataSource = dsDataset.Tables("TABLE001")

                'チェックボックスの追加
                Dim dgvCheck As New DataGridViewCheckBoxColumn
                DataGridView1.Columns.Insert(0, dgvCheck)

                'トリム処理
                For i = 0 To DataGridView1.RowCount - 1
                    For y = 1 To 8
                        DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim()
                        DataGridView1.Item(y, i).ReadOnly = True
                    Next
                Next
                'ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                '並び替えができないようにする
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                Return True


            Else
                MessageBox.Show("検索結果が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--出荷一覧 更新処理                   ----------
    '------------------------------------------------
    Public Sub sUpdate()
        Dim result As Boolean
        '編集モードを解除
        DataGridView1.EndEdit()
        'チェック処理
        result = fCheckUpdate()
        If result = True Then
            'DB接続
            Call sDBconnect()
            '更新処理
            result = fMainUpdate()
            If result = True Then
                Call sClear()
            End If

        End If

    End Sub

    '------------------------------------------------
    '--出荷一覧 更新チェック処理           ----------
    '------------------------------------------------
    Public Function fCheckUpdate() As Boolean

        Dim intCount As Integer 'チェック数をカウント
        Dim blnResult As Boolean

        Dim result As DialogResult = MessageBox.Show("更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
        If result = Windows.Forms.DialogResult.Yes Then
            If DataGridView1.RowCount = 0 Then
                MessageBox.Show("検索されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            'DataGridViewのチェック数をカウント
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Item(0, i).Value = True Then
                    intCount += 1
                End If
            Next

            'カウントが０の場合は、FALSE
            If intCount = 0 Then
                MessageBox.Show("チェック数が０です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            '//更新担当者が存在するか確認
            'DB接続
            Call sDBconnect()
            'チェック処理２
            blnResult = fCheckUpdate2()
            If blnResult = False Then
                Return False
            End If

            '問題なければTRUEを返す
            Return True

        Else
            Return False

        End If




    End Function

    '------------------------------------------------
    '--出荷一覧更新チェック処理(更新担当者)----------
    '------------------------------------------------
    Public Function fCheckUpdate2() As Boolean

        Dim dtReader As SqlDataReader

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
                '問題なければTRUEを返す
                Return True

            Else
                MessageBox.Show("入力したログインIDは存在しません" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--出荷一覧 更新メイン処理             ----------
    '------------------------------------------------
    Public Function fMainUpdate() As Boolean

        Dim intCheckCount As Integer 'チェック数のカウント
        'トランザクション開始
        Dim tran As SqlTransaction
        tran = Cn.BeginTransaction


        Try
            'チェックしているセルのカウント
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Item(0, i).Value = True Then
                    'チェックが付いていたら、更新処理
                    strSQL = ""
                    strSQL &= "UPDATE SHIPMENT_TBL "
                    strSQL &= "SET "
                    strSQL &= " 出荷チェックフラグ = 1, "
                    strSQL &= " 最終更新者 = '" & txtUserID.Text.Trim & "', "
                    strSQL &= " 更新日 = SYSDATETIME() "
                    strSQL &= "WHERE "
                    strSQL &= " 出荷NO = " & DataGridView1.Item(1, i).Value.ToString.Trim & " "

                    cd.CommandText = strSQL
                    cd.Transaction = tran
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    intCheckCount += 1
                End If
            Next

            If intCheckCount > 0 Then
                MessageBox.Show(intCheckCount & "件の更新が完了しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                tran.Commit()
                Return True

            Else
                MessageBox.Show("チェック数が０です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tran.Rollback()
                Return False

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            tran.Rollback()
            Return False
        Finally
            Cn.Close()
            Cn.Dispose()
        End Try

    End Function


    '------------------------------------------------
    '--出荷先NO 検索処理                   ----------
    '------------------------------------------------
    Public Sub sShipmentMSNO_Search()
        '０埋め
        txtShipmentMSNo.Text = txtShipmentMSNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBconnect()
        '検索メイン処理
        Call fShipmentMSNO_MainSearch()

    End Sub

    '------------------------------------------------
    '--出荷先NO 検索メイン処理             ----------
    '------------------------------------------------
    Public Function fShipmentMSNO_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 出荷先NO, "
            strSQL &= " 出荷先名 "
            strSQL &= "FROM "
            strSQL &= " SHIPMENT_MS "
            strSQL &= "WHERE "
            strSQL &= " 出荷先NO = '" & txtShipmentMSNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    lblShipmentMSName.Text = dtReader("出荷先名").ToString.Trim()
                End While

                dtReader.Close()
                Return True

            Else
                lblShipmentMSName.Text = ""
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
    '--クリア処理                    　    ----------
    '------------------------------------------------
    Public Sub sClear()

        rboShipment04.Visible = False
        rboShipment04.Checked = True

        txtShipmentMSNo.Clear()
        lblShipmentMSName.Text = ""
        dtpShipmentDateFrom.Text = Date.Now
        dtpShipmentDateTo.Text = Date.Now

        txtShipmentNo.Clear()

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        GroupBox4.Enabled = False


    End Sub

    '------------------------------------------------
    '--クリア処理 DataGridViuewのみ  　    ----------
    '------------------------------------------------
    Public Sub sClear_DataGridView()

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

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
