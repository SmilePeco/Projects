Imports System.Xml
Imports System.Data.SqlClient

Public Class T008

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String
    Dim intDeleteCount As Integer '削除する数のカウント

    '------------------------------------------------
    '--Load処理                      　    ----------
    '------------------------------------------------
    Private Sub T008_Load(sender As Object, e As EventArgs) Handles Me.Load
        sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー処理        　    ----------
    '------------------------------------------------
    Private Sub T008_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sOrderTBL_Search()
            Case Keys.F2
                '削除処理
                Call sOrderTBL_Delete()
            Case Keys.F3
                'クリア処理
                Call sClear()
            Case Keys.F4
                '終了処理
                Me.Close()

        End Select

    End Sub

    '------------------------------------------------
    '--検索ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '検索処理
        Call sOrderTBL_Search()
    End Sub

    '------------------------------------------------
    '--削除ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '削除処理
        Call sOrderTBL_Delete()
    End Sub

    '------------------------------------------------
    '--クリアボタン押下処理          　    ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--終了ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--受注先NOボタン押下処理        　    ----------
    '------------------------------------------------
    Private Sub btnOrderSearch_Click(sender As Object, e As EventArgs) Handles btnOrderSearch.Click
        '受注先マスタ検索画面を呼び出す
        Dim frm As New T008_2
        frm.ShowDialog(Me)
    End Sub

    '------------------------------------------------
    '--受注先NO Leave処理            　    ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_Leave(sender As Object, e As EventArgs) Handles txtOrderMSNo.Leave
        '検索処理
        Call sOrderMS_Search()

    End Sub

    '------------------------------------------------
    '--DataGridView CellClick処理    　    ----------
    '------------------------------------------------
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "詳細" Then
            'DataGridView1の値の格納
            Dim strOrderNo As String = DataGridView1.Item(2, e.RowIndex).Value.ToString.Trim
            Dim strOrderMS As String = DataGridView1.Item(3, e.RowIndex).Value.ToString.Trim
            Dim strWorkProcessNo As String = DataGridView1.Item(5, e.RowIndex).Value.ToString.Trim
            Dim intOrderAmount As Integer = DataGridView1.Item(7, e.RowIndex).Value.ToString.Trim
            Dim strOrderDate As String = DataGridView1.Item(8, e.RowIndex).Value.ToString.Trim
            Dim strHumanNo As String = DataGridView1.Item(9, e.RowIndex).Value.ToString.Trim
            Dim strOrderMSName As String = DataGridView1.Item(4, e.RowIndex).Value.ToString.Trim
            Dim strWorkProcessName As String = DataGridView1.Item(6, e.RowIndex).Value.ToString.Trim

            '引数設定し、画面を開く
            Dim frm As New T008_3(strOrderNo, strOrderMS, strWorkProcessNo, intOrderAmount, strOrderDate, strHumanNo, strOrderMSName, strWorkProcessName)
            frm.ShowDialog()
            Call sOrderTBL_Search()

        End If
    End Sub

    '------------------------------------------------
    '--受注先NO 検索処理             　    ----------
    '------------------------------------------------
    Public Sub sOrderMS_Search()
        '０埋め処理
        txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fOrderMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--受注先NO 検索メイン処理       　    ----------
    '------------------------------------------------
    Public Function fOrderMS_MainSearch()

        Dim dtReader As SqlDataReader

        Try
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

            If dtReader.HasRows Then
                While dtReader.Read
                    txtOrderMSNo.Text = dtReader("受注先NO").ToString.Trim
                    lblOrderMSName.Text = dtReader("受注先名").ToString.Trim
                End While

                dtReader.Close()
                Return True

            Else
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
    '--受注一覧　検索処理            　    ----------
    '------------------------------------------------
    Public Sub sOrderTBL_Search()
        Me.ActiveControl = Nothing
        Dim result As Boolean
        'DataGridView1クリア処理
        Call sClear_DataGridView1()
        'DB接続
        Call sDBConnect()
        'チェック処理
        result = fOrderTBL_SearchCheck()
        If result = True Then
            'DB接続
            Call sDBConnect()
            '検索メイン処理
            Call fOrderTBL_MainSearch()
        End If

    End Sub

    '------------------------------------------------
    '--受注一覧　検索メイン処理      　    ----------
    '------------------------------------------------
    Public Function fOrderTBL_MainSearch() As Boolean

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " A.受注NO, "
            strSQL &= " A.受注先NO, "
            strSQL &= " B.受注先名, "
            strSQL &= " A.作業工程NO, "
            strSQL &= " C.作業工程名, "
            strSQL &= " A.受注数, "
            strSQL &= " A.受注日, "
            strSQL &= " A.最終更新者, "
            strSQL &= " A.更新日, "
            strSQL &= " A.登録日 "
            strSQL &= "FROM "
            strSQL &= " ORDER_TBL A, "
            strSQL &= " ORDER_MS B, "
            strSQL &= " WORKPROCESS_MS C "
            strSQL &= "WHERE "
            strSQL &= "    A.受注先NO = '" & txtOrderMSNo.Text.Trim & "' "
            strSQL &= "AND A.受注先NO = B.受注先NO "
            strSQL &= "AND A.作業工程NO = C.作業工程NO "
            strSQL &= "AND 受注日 BETWEEN "
            strSQL &= "'" & dtpOrderDateFrom.Text & "' AND '" & dtpOrderDateTo.Text & "' "

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

                '１列目にチェックボタンを追加
                Dim chkColumns As New DataGridViewCheckBoxColumn
                DataGridView1.Columns.Insert(0, chkColumns)

                '２列目に詳細ボタンを追加
                Dim btnColumn As New DataGridViewButtonColumn

                btnColumn.Name = "詳細"
                btnColumn.HeaderText = "詳細"
                btnColumn.Text = "詳細"
                btnColumn.UseColumnTextForButtonValue = True

                DataGridView1.Columns.Insert(1, btnColumn)


                '文字列は全て編集不可
                For i = 1 To 11
                    DataGridView1.Columns(i).ReadOnly = True

                Next

                '名前列をトリム
                For i = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Item(4, i).Value = DataGridView1.Item(4, i).Value.ToString.Trim
                    DataGridView1.Item(6, i).Value = DataGridView1.Item(6, i).Value.ToString.Trim
                Next

                'ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                Return True

            Else
                MessageBox.Show("受注データが存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--受注一覧　検索前値チェック処理      ----------
    '------------------------------------------------
    Public Function fOrderTBL_SearchCheck()

        Dim dtReader As SqlDataReader

        Try
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
                Return True

            Else
                MessageBox.Show("入力した受注先NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--受注一覧　削除処理                  ----------
    '------------------------------------------------
    Public Sub sOrderTBL_Delete()

        Dim result As Boolean

        result = fOrderTBL_DeleteCheck()
        If result = True Then
            Call sDBConnect()
            result = fOrderTBL_MainDelete()
            If result = True Then
                Call sClear()
            End If
        End If
    End Sub

    '------------------------------------------------
    '--受注一覧　削除メイン処理            ----------
    '------------------------------------------------
    Public Function fOrderTBL_MainDelete() As Boolean

        Try
            Dim result As DialogResult = MessageBox.Show(intDeleteCount & "件を削除します。" & vbCrLf & "よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                For i = 0 To DataGridView1.RowCount - 1
                    If DataGridView1.Item(0, i).Value = True Then
                        'チェックが付いている項目のみ
                        strSQL = ""
                        strSQL &= "DELETE FROM ORDER_TBL "
                        strSQL &= "WHERE 受注NO = " & DataGridView1.Item(2, i).Value & " "

                        cd.CommandText = strSQL
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()
                    End If
                Next

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
    '--受注一覧　チェック処理              ----------
    '------------------------------------------------
    Public Function fOrderTBL_DeleteCheck() As Boolean

        intDeleteCount = 0

        '編集中だった場合は終了する
        DataGridView1.EndEdit()

        'チェックした数をカウント
        For i = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Item(0, i).Value = True Then
                intDeleteCount += 1
            End If
        Next

        'カウント数の確認
        If intDeleteCount <> 0 Then
            Return True
        Else
            MessageBox.Show("チェックした件数が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    '------------------------------------------------
    '--クリア処理                    　    ----------
    '------------------------------------------------
    Public Sub sClear()
        txtOrderMSNo.Clear()
        lblOrderMSName.Text = ""

        dtpOrderDateFrom.Text = DateTime.Now
        dtpOrderDateTo.Text = DateTime.Now

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub

    '------------------------------------------------
    '--クリア処理 DataGridViewのみ   　    ----------
    '------------------------------------------------
    Public Sub sClear_DataGridView1()

        DataGridView1.Columns.Clear()
        DataGridView1.DataSource = ""

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
