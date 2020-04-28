Imports System.Xml
Imports System.Data.SqlClient

Public Class T007_2

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--ファンクションキー押下処理          ----------
    '------------------------------------------------
    Private Sub T007_2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
        Me.Close()
    End Sub

    '------------------------------------------------
    '--受注先NO KeyPress処理               ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderMSNo.KeyPress

        'エンターキーが押された場合
        If e.KeyChar = Chr(13) Then
            Call sSearch()
        End If

    End Sub

    '------------------------------------------------
    '--DataGridView ダブルクリック処理     ----------
    '------------------------------------------------
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        'クリックした行を取得
        Dim intRow As Integer = e.RowIndex
        '受注先NOと受注先名を取得
        Dim strOrderNO As String = Me.DataGridView1.Item(0, intRow).Value()
        Dim strOrderName As String = Me.DataGridView1.Item(1, intRow).Value()
        '渡したいデータに書き込む
        Dim frm As T007 = CType(Me.Owner, T007)
        frm.txtOrderMSNo.Text = strOrderNO
        frm.lblOrderMSName.Text = strOrderName
        'フォームを閉じる
        Me.Close()

    End Sub

    '------------------------------------------------
    '--検索処理                            ----------
    '------------------------------------------------
    Public Sub sSearch()
        '０埋め処理
        If txtOrderMSNo.Text.Trim <> "" Then
            txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")
        End If
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

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 受注先NO, "
            strSQL &= " 受注先名 "
            strSQL &= "FROM "
            strSQL &= " ORDER_MS "
            If txtOrderMSNo.Text.Trim <> "" Then
                strSQL &= "WHERE "
                strSQL &= " 受注先NO = '" + txtOrderMSNo.Text.Trim + "'"
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

                'DataGridView1の空白をトリム
                For i = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Item(0, i).Value = DataGridView1.Item(0, i).Value.ToString.Trim()
                    DataGridView1.Item(1, i).Value = DataGridView1.Item(1, i).Value.ToString.Trim()

                Next


                Return True

            Else
                dtReader.Close()
                MessageBox.Show("入力した受注先NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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