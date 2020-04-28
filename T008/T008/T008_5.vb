Imports System.Xml

Public Class T008_5

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T008_5_Load(sender As Object, e As EventArgs) Handles Me.Load
        '検索処理
        Call sHumanMS_Search()

    End Sub

    '------------------------------------------------
    '--終了ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--DataGridView1 ダブルクリック処理    ----------
    '------------------------------------------------
    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        '現在の行番号を取得
        Dim intRow As Integer = e.RowIndex
        '値を格納
        Dim strHumanNo As String = DataGridView1.Item(1, intRow).Value
        '元のフォームに転送
        Dim frm As T008_3 = CType(Me.Owner, T008_3)
        frm.txtHumanNo.Text = strHumanNo
        Me.Close()


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

            Dim dsDataset As New DataSet
            Dim daDataAdapter As New SqlClient.SqlDataAdapter
            daDataAdapter.SelectCommand = New SqlClient.SqlCommand(strSQL, Cn)
            daDataAdapter.SelectCommand.CommandTimeout = 0
            daDataAdapter.Fill(dsDataset, "TABLE001")
            DataGridView1.DataSource = dsDataset.Tables("TABLE001")

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