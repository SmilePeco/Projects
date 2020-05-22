Imports System.Xml
Imports System.Data.SqlClient

Public Class T001_2

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String


    '------------------------------------------------
    '--ファンクションキー処理              ----------
    '------------------------------------------------
    Private Sub T001_2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sSearch()
            Case Keys.F2
                '終了処理
                Me.Close()

        End Select

    End Sub

    '------------------------------------------------
    '--DataGridView ダブルクリック処理     ----------
    '------------------------------------------------
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        '現在の行を取得
        Dim intRow As Integer = e.RowIndex
        '値の取得
        Dim intHumanNo As Integer = DataGridView1.Item(0, intRow).Value
        '値の転送
        Dim frm As T001 = CType(Me.Owner, T001)
        frm.txtHumanNo.Text = intHumanNo
        '終了処理
        Me.Close()


    End Sub

    '------------------------------------------------
    '--検索ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '検索処理
        Call sSearch()
    End Sub

    '------------------------------------------------
    '--終了ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--検索処理                            ----------
    '------------------------------------------------
    Public Sub sSearch()
        'DB接続
        Call sDBconnect()
        '検索メイン処理
        Call fMainSearch()

    End Sub

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            'SQL発行
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 社員NO, "
            strSQL &= " 名前 "
            strSQL &= "FROM "
            strSQL &= " HUMAN_MS "
            If txtHumanMSNo.Text.Trim <> "" Then
                strSQL &= "WHERE "
                strSQL &= " 社員NO = '" & txtHumanMSNo.Text.Trim & "' "
            End If
            strSQL &= "ORDER BY "
            strSQL &= " 社員NO "

            'SQL実行
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

                'DataGridView1のすべての列の幅を自動調整する
                DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
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