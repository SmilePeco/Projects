Imports System.Xml

Public Class T009_4

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T009_4_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sHumanMS_Search()

    End Sub

    '------------------------------------------------
    '--検索処理                            ----------
    '------------------------------------------------
    Public Sub sHumanMS_Search()
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fHumanMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function fHumanMS_MainSearch()

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 社員NO, "
            strSQL &= " 名前 "
            strSQL &= "FROM "
            strSQL &= " HUMAN_MS "
            strSQL &= "ORDER BY "
            strSQL &= " 社員NO "

            'cd.CommandText = strSQL
            'cd.Connection = Cn

            Dim dsDataSet As New DataSet
            Dim daDataAdapter As New SqlClient.SqlDataAdapter

            daDataAdapter.SelectCommand = New SqlClient.SqlCommand(strSQL, Cn)
            daDataAdapter.SelectCommand.CommandTimeout = 0
            daDataAdapter.Fill(dsDataSet, "TABLE001")
            DataGridView1.DataSource = dsDataSet.Tables("TABLE001")

            For i = 0 To DataGridView1.RowCount - 1
                DataGridView1.Item(0, i).Value = DataGridView1.Item(0, i).Value.ToString.Trim
                DataGridView1.Item(1, i).Value = DataGridView1.Item(1, i).Value.ToString.Trim
            Next

            'ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

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
    '--DataGridView1 ダブルクリック処理    ----------
    '------------------------------------------------
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        'ダブルクリックした行を取得
        Dim intRow As Integer = e.RowIndex
        '値を取得
        Dim strUserID As String = DataGridView1.Item(1, intRow).Value
        '元のフォームへ転送
        Dim frm As T009 = CType(Me.Owner, T009)
        frm.txtUserID.Text = strUserID

        Me.Close()

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