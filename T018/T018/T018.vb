Imports System.Xml
Imports System.Data.SqlClient

Public Class T018

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T018_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー押下処理          ----------
    '------------------------------------------------
    Private Sub T018_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sSearch()
            Case Keys.F2
                'クリア処理
                Call sClear()
            Case Keys.F3
                '終了処理
                Me.Close()

        End Select

    End Sub

    '------------------------------------------------
    '--検索ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '検索処理
        Call sSearch()

    End Sub

    '------------------------------------------------
    '--クリアボタン押下処理                ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'クリア処理
        Call sClear()

    End Sub

    '------------------------------------------------
    '--終了ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--製品NO ボタン押下処理               ----------
    '------------------------------------------------
    Private Sub btnItemSearch_Click(sender As Object, e As EventArgs) Handles btnItemSearch.Click
        Dim frm As New T018_2
        frm.ShowDialog(Me)
    End Sub

    '------------------------------------------------
    '--作業工程NO ボタン押下処理           ----------
    '------------------------------------------------
    Private Sub btnWorkProcessSearch_Click(sender As Object, e As EventArgs) Handles btnWorkProcessSearch.Click
        Dim frm As New T018_3
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--作業工程NO 製品検索ボタン押下処理   ----------
    '------------------------------------------------
    Private Sub btnWorkProcessItemSearch_Click(sender As Object, e As EventArgs) Handles btnWorkProcessItemSearch.Click
        '検索処理
        Call sWorkProcessMS_Search()
    End Sub

    '------------------------------------------------
    '--製品NO Leave処理                    ----------
    '------------------------------------------------
    Private Sub txtItemNo_Leave(sender As Object, e As EventArgs) Handles txtItemNo.Leave
        If txtItemNo.Text.Trim <> "" Then
            '検索処理
            Call sItemMS_Search()
        End If

    End Sub

    '------------------------------------------------
    '--作業工程NO Leave処理                ----------
    '------------------------------------------------
    Private Sub txtWorkProcessNO_Leave(sender As Object, e As EventArgs) Handles txtWorkProcessNO.Leave
        '０埋め処理
        txtWorkProcessNO.Text = txtWorkProcessNO.Text.PadLeft(3, "0")

    End Sub

    '------------------------------------------------
    '--DataGridView ボタン押下処理         ----------
    '------------------------------------------------
    Private Sub DataGridView1_CellContentClick(ByVal sender As Object, _
            ByVal e As DataGridViewCellEventArgs) _
            Handles DataGridView1.CellContentClick
        Dim dgv As DataGridView = CType(sender, DataGridView)
        '０列目（ボタン） がクリックされた
        If dgv.Columns(e.ColumnIndex).Index = 0 Then
            '値の転送
            Dim frm As New T018_4
            frm.txtItemNo.Text = DataGridView1.Item(1, e.RowIndex).Value.ToString.Trim
            '在庫詳細画面の表示
            frm.ShowDialog(Me)

        End If
    End Sub

    '------------------------------------------------
    '--在庫一覧 検索処理                   ----------
    '------------------------------------------------
    Public Sub sSearch()
        'フォーカス外す
        Me.ActiveControl = Nothing
        '初期化処理
        Call sClear_DataGridView()
        'DB接続
        Call sDBconnect()
        '検索メイン処理
        Call fMainSearch()

    End Sub

    '------------------------------------------------
    '--在庫一覧 検索メイン処理             ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= "  A.製品NO, "
            strSQL &= "  B.製品名, "
            strSQL &= " SUM(A.在庫数) AS 在庫数 "
            strSQL &= "FROM "
            strSQL &= " STOCK_TBL A, "
            strSQL &= " ITEM_MS B "
            strSQL &= "WHERE "
            strSQL &= "    A.製品NO = B.製品NO "
            If txtItemNo.Text.Trim <> "" Then
                strSQL &= "AND A.製品NO = '" & txtItemNo.Text.Trim & "' "
            End If
            strSQL &= "GROUP BY "
            strSQL &= " A.製品NO, "
            strSQL &= " B.製品名 "

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

                'トリム処理
                For i = 0 To DataGridView1.RowCount - 1
                    For y = 0 To DataGridView1.ColumnCount - 1
                        DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim
                    Next
                Next

                'ボタンの追加
                Dim chkButton As New DataGridViewButtonColumn
                chkButton.UseColumnTextForButtonValue = True
                chkButton.Text = " ... "
                DataGridView1.Columns.Insert(0, chkButton)



                'ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                Return True


            Else
                MessageBox.Show("検索結果が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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
    '--製品NO 検索処理                     ----------
    '------------------------------------------------
    Public Sub sItemMS_Search()
        '０埋め処理
        txtItemNo.Text = txtItemNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBconnect()
        '検索メイン処理
        Call fItemMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--製品NO 検索メイン処理               ----------
    '------------------------------------------------
    Public Function fItemMS_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 製品NO, "
            strSQL &= " 製品名 "
            strSQL &= "FROM "
            strSQL &= " ITEM_MS "
            strSQL &= "WHERE "
            strSQL &= " 製品NO = '" & txtItemNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    If txtItemNo.Text.Trim <> "" Then
                        lblItemName.Text = dtReader("製品名").ToString.Trim
                    Else
                        lblItemName.Text = ""
                    End If
                End While

                dtReader.Close()
                Return True

            Else
                lblItemName.Text = ""
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
    '--作業工程NO 検索処理                 ----------
    '------------------------------------------------
    Public Sub sWorkProcessMS_Search()
        '０埋め処理
        txtWorkProcessNO.Text = txtWorkProcessNO.Text.PadLeft(3, "0")
        'DB接続
        Call sDBconnect()
        '検索メイン処理
        Call fWorkProcessMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--作業工程NO 検索メイン処理           ----------
    '------------------------------------------------
    Public Function fWorkProcessMS_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " A.製品NO, "
            strSQL &= " B.製品名 "
            strSQL &= "FROM "
            strSQL &= " WORKPROCESS_MS A, "
            strSQL &= " ITEM_MS B "
            strSQL &= "WHERE "
            strSQL &= "    A.製品NO = B.製品NO "
            strSQL &= "AND A.作業工程NO = '" & txtWorkProcessNO.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    txtItemNo.Text = dtReader("製品NO").ToString.Trim()
                    lblItemName.Text = dtReader("製品名").ToString.Trim()
                End While

                dtReader.Close()
                Return True

            Else
                MessageBox.Show("入力した作業項目NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--クリア処理                          ----------
    '------------------------------------------------
    Public Sub sClear()

        'テキストボックス、ラベルクリア処理
        txtItemNo.Clear()
        lblItemName.Text = ""
        txtWorkProcessNO.Clear()
        lblWorkProcessName.Text = ""

        'DataGridView初期化処理
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub

    '------------------------------------------------
    '--クリア処理 DataGridViewのみ         ----------
    '------------------------------------------------
    Public Sub sClear_DataGridView()

        'DataGridView初期化処理
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
