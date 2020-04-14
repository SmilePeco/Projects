Imports System.Xml

Public Class T004_2

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    Private Sub T004_2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sClear()

        'Enterキーでフォーカス移動初期設定
        'AcceptButtonを無効にする
        Me.AcceptButton = Nothing

        'キーイベントをフォームで受け取る
        Me.KeyPreview = True
        'KeyDownイベントハンドラを追加
        AddHandler Me.KeyDown, New KeyEventHandler(AddressOf T004_2_KeyDown)

    End Sub

    '---------------------------------------------
    '---フォーム KeyDown処理                   ---
    '---------------------------------------------
    Private Sub T004_2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        'Enterキーが押されているか確認
        'AltかCtrlキーが押されている時は無視する
        If (e.KeyCode = Keys.Enter) AndAlso _
            Not e.Alt AndAlso Not e.Control Then
            'あたかもTabキーが押されたかのようにする
            'Shiftが押されている時は前のコントロールのフォーカスを移動
            Me.ProcessTabKey(Not e.Shift)

            e.Handled = True
            '.NET Framework 2.0以降
            e.SuppressKeyPress = True
        End If

    End Sub

    '---------------------------------------------
    '---製品NO KeyPress処理                    ---
    '---------------------------------------------
    Private Sub txtItemNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemNo.KeyPress

        '数値とバックスペース、エンターキー以外は入力できない
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso _
             e.KeyChar <> ControlChars.Back AndAlso _
             e.KeyChar <> Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            e.Handled = True

        End If

    End Sub

    '---------------------------------------------
    '---製品NO Leave処理                       ---
    '---------------------------------------------
    Private Sub txtItemNo_Leave(sender As Object, e As EventArgs) Handles txtItemNo.Leave

        Dim result As Boolean

        If txtItemNo.Text.Trim <> "" Then
            '空白以外は３桁まで０埋め
            txtItemNo.Text = txtItemNo.Text.PadLeft(3, "0")

        End If

        'DataGridViewの初期化
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

        result = sXmlConnection()

        If result = True Then
            Call sDataGridViewConnect()

        End If


    End Sub

    '---------------------------------------------
    '---CellDataGridViewダブルクリック時       ---
    '---------------------------------------------
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Dim intRow As Integer = e.RowIndex

        '製品NOを取得
        Dim strItem As String = Me.DataGridView1.Item(0, intRow).Value
        '渡したいフォームの取得
        Dim frm As T004 = CType(Me.Owner, T004)
        'オブジェクトに渡す
        frm.txtItemNO.Text = Me.DataGridView1.Item(0, intRow).Value
        Me.Close()


    End Sub

    '---------------------------------------------
    '---DB接続                                 ---
    '---------------------------------------------
    Public Function sXmlConnection() As Boolean

        Dim strServer As String
        Dim strUserID As String
        Dim srePass As String
        Dim strDatabaseName As String

        Dim xmlNode As XmlNodeList

        Dim xmlDoc As New XmlDocument()

        Dim strFileName As String = "C:\SQLServer\DBConnect.xml"

        Try
            If System.IO.File.Exists(strFileName) Then

                xmlDoc.Load(strFileName)

                'サーバ名を取得
                xmlNode = xmlDoc.GetElementsByTagName("Server")
                strServer = xmlNode.Item(0).InnerText
                'ユーザ名を取得
                xmlNode = xmlDoc.GetElementsByTagName("UserID")
                strUserID = xmlNode.Item(0).InnerText
                'パスワードを取得
                xmlNode = xmlDoc.GetElementsByTagName("Password")
                srePass = xmlNode.Item(0).InnerText
                'データベース名を取得
                xmlNode = xmlDoc.GetElementsByTagName("DatabaseName")
                strDatabaseName = xmlNode.Item(0).InnerText

                'DB接続
                strConnect = "Server=" & strServer & ";"
                strConnect &= "User ID=" & strUserID & ";"
                strConnect &= "Password=" & srePass & ";"
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

    '---------------------------------------------
    '---DataGridView取得                       ---
    '---------------------------------------------
    Public Sub sDataGridViewConnect()

        strSQL = ""
        strSQL &= "SELECT "
        strSQL &= "製品NO, "
        strSQL &= "製品名 "
        strSQL &= "FROM "
        strSQL &= "ITEM_MS "
        '製品NOが空白のときはWHERE文なし、全件検索
        'それ以は、指定検索
        If txtItemNo.Text.Trim <> "" Then
            strSQL &= "WHERE "
            strSQL &= "製品NO = '" + txtItemNo.Text.Trim + "' "
        End If

        Dim dsDataset As New DataSet
        Dim DaDataSetAdapter As New SqlClient.SqlDataAdapter
        DaDataSetAdapter.SelectCommand = New SqlClient.SqlCommand(strSQL, Cn)
        DaDataSetAdapter.SelectCommand.CommandTimeout = 0
        DaDataSetAdapter.Fill(dsDataset, "TABLE001")
        DataGridView1.DataSource = dsDataset.Tables("TABLE001")

        Cn.Close()
        Cn.Dispose()


    End Sub

    '---------------------------------------------
    '---クリア処理                             ---
    '---------------------------------------------
    Public Sub sClear()

        txtItemNo.Clear()

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub





End Class