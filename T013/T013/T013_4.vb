Imports System.Xml
Imports System.Data.SqlClient

Public Class T013_4

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T013_4_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sClear()
        Call sSearch()
        Dim frm As T013 = CType(Me.Owner, T013)
        '製品NOの転記
        txtItemMSNo.Text = frm.lblMainItemMSNO.Text
        txtItemMSNo.Enabled = False

    End Sub

    '------------------------------------------------
    '--検索処理                            ----------
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

        Dim dtReader As SqlDataReader
        Dim frm As T013 = CType(Me.Owner, T013)

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 在庫NO, "
            strSQL &= " 製品NO, "
            strSQL &= " 在庫数, "
            strSQL &= " 更新日 "
            strSQL &= "FROM "
            strSQL &= " STOCK_TBL "
            strSQL &= "WHERE "
            strSQL &= " 製品NO = '" & frm.lblMainItemMSNO.Text.Trim & "' "
            strSQL &= "ORDER BY  "
            strSQL &= " 更新日 "

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
                    For y = 0 To 2
                        DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim
                    Next
                Next

                'DataGridView1のすべての列の幅を自動調整する
                DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

                '並び替えができないようにする
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                '製品NOの転記
                txtItemMSNo.Text = frm.lblMainItemMSNO.Text

                '合計在庫数の取得
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= "  製品NO, "
                strSQL &= "  SUM(在庫数) AS 合計在庫数 "
                strSQL &= "FROM "
                strSQL &= "  STOCK_TBL "
                strSQL &= "WHERE "
                strSQL &= "  製品NO = '" & frm.lblMainItemMSNO.Text.Trim & "' "
                strSQL &= "GROUP BY "
                strSQL &= "  製品NO "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    While dtReader.Read
                        lblSumStock.Text = dtReader("合計在庫数")
                    End While
                End If


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
    '--クリア処理                          ----------
    '------------------------------------------------
    Public Sub sClear()

        txtItemMSNo.Clear()
        lblSumStock.Text = ""

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