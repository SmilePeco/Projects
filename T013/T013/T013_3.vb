Imports System.Xml

Public Class T013_3

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T013_3_Load(sender As Object, e As EventArgs) Handles Me.Load

        'クリア処理
        Call sClear()

        '検索処理
        Call sSearch()

    End Sub

    '------------------------------------------------
    '--DataGridViewダブルクリック処理      ----------
    '------------------------------------------------
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        '現在の行番号を取得
        Dim intRow As Integer = e.RowIndex
        '値を取得
        Dim strOrderNO As Integer = DataGridView1.Item(0, intRow).Value
        '元のフォームに転送
        Dim frm As T013 = CType(Me.Owner, T013)
        frm.txtOrderNo.Text = strOrderNO
        Me.Close()

    End Sub

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Sub sSearch()
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fMainSearch()

    End Sub

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim frm As T013 = CType(Me.Owner, T013)

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 受注NO, "
            strSQL &= " 受注先NO, "
            strSQL &= " 作業工程NO, "
            strSQL &= " 受注数, "
            strSQL &= " 受注日, "
            strSQL &= " 最終更新者 "
            strSQL &= "FROM "
            strSQL &= " ORDER_TBL "
            strSQL &= "WHERE "
            strSQL &= "    受注チェックフラグ = 1 "
            strSQL &= "AND 受注先NO = '" & frm.txtOrderMSNo.Text.Trim & "' "
            strSQL &= "AND 受注日 BETWEEN "
            strSQL &= "    '" & frm.dtpOrderDateFrom.Text.Trim & "' AND '" & frm.dtpOrderDateTo.Text.Trim & "' "
            strSQL &= " ORDER BY "
            strSQL &= " 受注NO "

            Dim dsDataset As New DataSet
            Dim daDataAdapter As New SqlClient.SqlDataAdapter
            daDataAdapter.SelectCommand = New SqlClient.SqlCommand(strSQL, Cn)
            daDataAdapter.SelectCommand.CommandTimeout = 0
            daDataAdapter.Fill(dsDataset, "TABLE001")
            DataGridView1.DataSource = dsDataset.Tables("TABLE001")

            For i = 0 To DataGridView1.RowCount - 1
                For y = 0 To 5
                    DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim
                Next
            Next

            'DataGridView1のすべての列の幅を自動調整する
            DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

            '並び替えができないようにする
            For Each c As DataGridViewColumn In DataGridView1.Columns
                c.SortMode = DataGridViewColumnSortMode.NotSortable
            Next c


            Return True

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

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

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