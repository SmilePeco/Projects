Imports System.Data.SqlClient

Public Class T003_2

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String
    Dim strServerName As String = "SHINYA-PC\NAKADB" 'サーバー名
    Dim strUserID As String = "sa" 'ユーザーID
    Dim strPassword As String = "naka" 'パスワード
    Dim strDatabaseName As String = "NAKADB" 'データベース
    Dim blnResult As Boolean '名前・パスワード判定用


    Private Sub T003_2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sClear()

    End Sub

    '------------------------------------------------
    '--作業工程No KeyDown処理                      --
    '------------------------------------------------
    Private Sub txtWorkProcessNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWorkNo.KeyDown
        '//Enterキーで検索処理
        If e.KeyData = Keys.Enter Then
            txtWorkNo.Text = txtWorkNo.Text.PadLeft(3, "0")

            'DataGridViewの初期化
            DataGridView1.DataSource = Nothing
            DataGridView1.Columns.Clear()

            'DB接続処理
            'Call sDBConnect()
            ''検索メイン処理()
            'blnResult = frmMainSearch()

            Call sDataGridViewConnect()

            'TRUEの場合、続けて検索処理
            If blnResult = True Then
                Call sDataGridViewConnect()
            End If

        End If

    End Sub

    '------------------------------------------------
    '--DB接続の開始                        ----------
    '------------------------------------------------
    Public Sub sDataGridViewConnect()

        Dim dtReader As SqlDataReader
        Dim intShipment As Integer
        Dim intCreate As Integer

        'DBに接続
        strServerName = "SHINYA-PC\NAKADB" 'サーバー名(またはIPアドレス)
        strUserID = "sa" 'ユーザーID
        strPassword = "naka" 'パスワード
        strDatabaseName = "NAKADB" 'データベース

        strConnect = "Server=" & strServerName & ";"
        strConnect &= "User ID=" & strUserID & ";"
        strConnect &= "Password=" & strPassword & ";"
        strConnect &= "Initial Catalog=" & strDatabaseName

        Cn.ConnectionString = strConnect
        Cn.Open()

        '接続SQLの作成
        strSQL = ""
        strSQL &= "SELECT "
        strSQL &= " 作業工程NO, "
        strSQL &= " 作業工程名 "
        strSQL &= " 作業工程名 "
        strSQL &= "FROM "
        strSQL &= " WORKPROCESS_MS "
        strSQL &= "WHERE "
        strSQL &= " 作業工程NO = '" + txtWorkNo.Text + "' "


        'strSQL = ""
        'strSQL &= "SELECT "
        'strSQL &= "isnull(A.""出荷日"",B.""生産日"") as 日付, "
        'strSQL &= "isnull(B.""生産数"", 0) AS 生産数, "
        'strSQL &= "isnull(A.""出荷数"", 0) AS 出荷数 "
        'strSQL &= "FROM "
        'strSQL &= "SHIPMENT_TBL A "
        'strSQL &= "FULL OUTER JOIN "
        'strSQL &= "(SELECT "
        'strSQL &= """作業工程NO"", "
        'strSQL &= """生産数"", "
        'strSQL &= """生産日"" "
        'strSQL &= "FROM "
        'strSQL &= "CREATE_TBL ) B "
        'strSQL &= "ON A.""出荷日"" = B.""生産日"" "
        'strSQL &= "AND B.""生産日"" = A.""出荷日"" "
        'strSQL &= "WHERE "
        'strSQL &= "(    A.""出荷日"" BETWEEN '" + dtpDateFrom.Value.Date + "' AND '" + dtpDateTo.Value.Date + "' "
        'strSQL &= "OR B.""生産日"" BETWEEN '" + dtpDateFrom.Value.Date + "' AND '" + dtpDateTo.Value.Date + "')"
        'strSQL &= "AND (A.""作業工程NO"" = '" + txtWorkProcessNo.Text + "' OR B.""作業工程NO"" = '" + txtWorkProcessNo.Text + "')"
        'strSQL &= "ORDER BY "
        'strSQL &= " 日付"

        'strSQL = ""
        'strSQL &= "SELECT "
        'strSQL &= "A.""作業工程NO"", "
        'strSQL &= "A.""出荷日"","
        'strSQL &= "isnull(B.""生産数"",0) as 生産数, "
        'strSQL &= "isnull(A.""出荷数"",0) as 出荷数 "
        'strSQL &= "FROM "
        'strSQL &= "SHIPMENT_TBL A LEFT JOIN CREATE_TBL B ON A.""出荷日"" = B.""生産日"" "
        'strSQL &= "WHERE "
        'strSQL &= "A.""出荷日"" BETWEEN '20-04-07' AND '20-04-15' "

        Dim dsDataSet As New DataSet
        Dim daDataAdapter As New SqlClient.SqlDataAdapter
        daDataAdapter.SelectCommand = New SqlClient.SqlCommand(strSQL, Cn)
        daDataAdapter.SelectCommand.CommandTimeout = 0
        daDataAdapter.Fill(dsDataSet, "TABLE001")
        DataGridView1.DataSource = dsDataSet.Tables("TABLE001")

        '末尾に１行追加（計算用）
        'DataGridViewTextBoxColumn列を作成する
        Dim textColumn As New DataGridViewTextBoxColumn()
        'データソースの"Column1"をバインドする
        textColumn.DataPropertyName = "Column1"

        Cn.Close()
        Cn.Dispose()

    End Sub



    '---------------------------------------------
    '---クリア処理                             ---
    '---------------------------------------------
    Public Sub sClear()

        txtWorkNo.Clear()

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub


    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        'クリックした行を取得
        Dim intRow As Integer = e.RowIndex

        '作業項目NOを取得
        Dim strWork As String = Me.DataGridView1.Item(0, intRow).Value()

        '渡したいフォームを取得
        Dim frm As T003 = CType(Me.Owner, T003)
        '渡したいデータを書き込む
        frm.txtWorkProcessNo.Text = Me.txtWorkNo.Text
        Me.Close()

    End Sub
End Class