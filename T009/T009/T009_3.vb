Imports System.Xml
Imports System.Data.SqlClient

Public Class T009_3

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--ファンクションキー処理             ----------
    '------------------------------------------------
    Private Sub T009_3_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
    '--終了ボタン処理                      ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--作業工程NO KeyPress処理             ----------
    '------------------------------------------------
    Private Sub txtWorkProcessNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWorkProcessNO.KeyPress

        'Enterキーが押された場合
        If e.KeyChar = Chr(13) Then
            '検索処理
            Call sSearch()

        End If

    End Sub

    '------------------------------------------------
    '--DataGridView1 ダブルクリック処理    ----------
    '------------------------------------------------
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        'ダブルクリックしたセルを取得
        Dim intRow As Integer = e.RowIndex
        '作業工程NOと作業工程名を取得
        Dim strWorkProcessNO As String = DataGridView1.Item(0, intRow).Value
        Dim strWorkProcessName As String = DataGridView1.Item(1, intRow).Value
        '元のフォームに値を転送
        Dim frm As T009 = CType(Me.Owner, T009)
        frm.txtWorkProcessNO.Text = strWorkProcessNO
        frm.lblWorkProcessName.Text = strWorkProcessName
        'フォームを閉じる
        Me.Close()

    End Sub

    '------------------------------------------------
    '--作業工程NO 検索メイン処理           ----------
    '------------------------------------------------
    Public Sub sSearch()
        '０埋め処理
        If txtWorkProcessNO.Text.Trim <> "" Then
            txtWorkProcessNO.Text = txtWorkProcessNO.Text.PadLeft(3, "0")
        End If
        'DB接続
        Call sDBConnect()
        '検索処理
        Call fMainSearch()

    End Sub

    '------------------------------------------------
    '--作業工程NO 検索メイン処理           ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 作業工程NO, "
            strSQL &= " 作業工程名 "
            strSQL &= "FROM "
            strSQL &= " WORKPROCESS_MS "
            If txtWorkProcessNO.Text.Trim <> "" Then
                strSQL &= "WHERE "
                strSQL &= " 作業工程NO = '" + txtWorkProcessNO.Text.Trim + "' "

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

                '検索結果のトリム
                For i = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Item(0, i).Value = DataGridView1.Item(0, i).Value.ToString.Trim
                    DataGridView1.Item(1, i).Value = DataGridView1.Item(1, i).Value.ToString.Trim
                Next

                Return True

            Else
                dtReader.Close()
                MessageBox.Show("入力した作業工程NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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