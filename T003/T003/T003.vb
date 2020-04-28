Imports System.Data.SqlClient

Public Class T003

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String
    Dim strServerName As String = "SHINYA-PC\NAKADB" 'サーバー名
    Dim strUserID As String = "sa" 'ユーザーID
    Dim strPassword As String = "naka" 'パスワード
    Dim strDatabaseName As String = "NAKADB" 'データベース
    Dim blnResult As Boolean '名前・パスワード判定用

    '---------------------------------------------
    '---ロード処理                             ---
    '---------------------------------------------
    Private Sub T003_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sClear()
    End Sub

    '------------------------------------------------
    '--作業工程No KeyPress処理                　　 --
    '------------------------------------------------
    Private Sub txtWorkProcessNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWorkProcessNo.KeyPress

        '0～9と、バックスペース以外の時は、イベントをキャンセルする
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso _
                e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    '------------------------------------------------
    '--作業工程No KeyDown処理                      --
    '------------------------------------------------
    Private Sub txtWorkProcessNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWorkProcessNo.KeyDown

        '//Enterキーで検索処理
        If e.KeyData = Keys.Enter Then
            txtWorkProcessNo.Text = txtWorkProcessNo.Text.PadLeft(3, "0")

            'DataGridViewの初期化
            DataGridView1.DataSource = Nothing
            DataGridView1.Columns.Clear()

            'DB接続処理
            Call sDBConnect()
            '検索メイン処理()
            blnResult = frmMainSearch()

            'TRUEの場合、続けて検索処理
            If blnResult = True Then
                Call sDataGridViewConnect()
            End If

        End If

    End Sub

    '------------------------------------------------
    '--クリアボタン押下処理　                      --
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call sClear()

    End Sub

    '------------------------------------------------
    '--作業工程NO検索ボタン押下処理                --
    '------------------------------------------------
    Private Sub txtWorkProcessNoSearch_Click(sender As Object, e As EventArgs) Handles txtWorkProcessNoSearch.Click
        Dim frm As T003_2 = New T003_2()
        T003_2.ShowDialog(Me)
        Me.txtWorkProcessNo.Focus()


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
        strSQL &= "isnull(A.""出荷日"",B.""生産日"") as 日付, "
        strSQL &= "isnull(B.""生産数"", 0) AS 生産数, "
        strSQL &= "isnull(A.""出荷数"", 0) AS 出荷数 "
        strSQL &= "FROM "
        strSQL &= "SHIPMENT_TBL A "
        strSQL &= "FULL OUTER JOIN "
        strSQL &= "(SELECT "
        strSQL &= """作業工程NO"", "
        strSQL &= """生産数"", "
        strSQL &= """生産日"" "
        strSQL &= "FROM "
        strSQL &= "CREATE_TBL ) B "
        strSQL &= "ON A.""出荷日"" = B.""生産日"" "
        strSQL &= "AND B.""生産日"" = A.""出荷日"" "
        strSQL &= "WHERE "
        strSQL &= "(    A.""出荷日"" BETWEEN '" + dtpDateFrom.Value.Date + "' AND '" + dtpDateTo.Value.Date + "' "
        strSQL &= "OR B.""生産日"" BETWEEN '" + dtpDateFrom.Value.Date + "' AND '" + dtpDateTo.Value.Date + "')"
        strSQL &= "AND (A.""作業工程NO"" = '" + txtWorkProcessNo.Text + "' OR B.""作業工程NO"" = '" + txtWorkProcessNo.Text + "')"
        strSQL &= "ORDER BY "
        strSQL &= " 日付"

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
        '名前とヘッダーを設定する
        'textColumn.Name = "Column1"
        textColumn.HeaderText = "理論在庫"
        '列を追加する
        DataGridView1.Columns.Add(textColumn)

        '理論在庫の計算（末尾行まで繰り返す）
        For i = 0 To DataGridView1.RowCount - 1
            intCreate = Me.DataGridView1.Item(1, i).Value
            intShipment = Me.DataGridView1.Item(2, i).Value

            '１行目の計算はヘッダー部分の在庫から計算する。
            '２票目以降は、理論在庫部分から引いていく。
            If i <> 0 Then
                Me.DataGridView1.Item(3, i).Value = Me.DataGridView1.Item(3, i - 1).Value + intCreate - intShipment
            Else
                Me.DataGridView1.Item(3, i).Value = txtStock.Text + intCreate - intShipment

            End If

        Next

        Cn.Close()
        Cn.Dispose()

    End Sub

    '------------------------------------------------
    '--DataGridView接続の開始              ----------
    '------------------------------------------------
    Public Sub sDBConnect()

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

        cd = Cn.CreateCommand

        Cn.Open()

    End Sub

    '---------------------------------------------
    '---クリア処理                             ---
    '---------------------------------------------
    Public Sub sClear()

        dtpDateFrom.Text = Now
        dtpDateTo.Text = Now.AddDays(5)

        txtWorkProcessNo.Text = ""
        txtWorkProcessName.Text = ""
        txtWorkProcessName.Enabled = False

        txtProcessNo.Text = ""
        txtProcessName.Text = ""
        txtProcessNo.Enabled = False
        txtProcessName.Enabled = False

        txtItemNo.Text = ""
        txtItemName.Text = ""
        txtItemNo.Enabled = False
        txtItemName.Enabled = False

        txtStock.Enabled = False
        txtStock.Text = ""

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub

    '---------------------------------------------
    '---クリア処理                             ---
    '---------------------------------------------

    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    '------------------------------------------------
    '--検索メイン処理                      ----------
    '------------------------------------------------
    Public Function frmMainSearch() As Boolean

        Dim dtReader As SqlDataReader

        If txtWorkProcessNo.Text.Trim <> "" Then

            Try
                '基本情報の取得
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " A.""作業工程NO"", "
                strSQL &= " A.""作業工程名"", "
                strSQL &= " A.""工程NO"", "
                strSQL &= " B.""工程名"", "
                strSQL &= " A.""製品NO"", "
                strSQL &= " C.""製品名"" "
                strSQL &= "FROM "
                strSQL &= " WORKPROCESS_MS A, "
                strSQL &= " PROCESS_MS B, "
                strSQL &= " ITEM_MS C "
                strSQL &= "WHERE "
                strSQL &= "    A.""作業工程NO"" = '" + txtWorkProcessNo.Text.Trim + "'"
                strSQL &= "AND A.""工程NO"" = B.""工程NO"" "
                strSQL &= "AND A.""製品NO"" = C.""製品NO"" "

                'strSQL = ""
                'strSQL &= "SELECT "
                'strSQL &= "A.""出荷NO"", "
                'strSQL &= "A.""作業工程NO"", "
                'strSQL &= "B.""作業工程名"", "
                'strSQL &= "C.""工程NO"", "
                'strSQL &= "C.""工程名"", "
                'strSQL &= "C.""製品NO"", "
                'strSQL &= "C.""製品名"", "
                'strSQL &= "A.""出荷数"", "
                'strSQL &= "A.""出荷日"", "
                'strSQL &= "A.""更新日"", "
                'strSQL &= "A.""登録日"" "
                'strSQL &= "FROM "
                'strSQL &= "SHIPMENT_TBL A, "
                'strSQL &= "WORKPROCESS_MS B, "
                'strSQL &= "(SELECT "
                'strSQL &= "A.""工程NO"", "
                'strSQL &= "B.""工程名"", "
                'strSQL &= "A.""製品NO"", "
                'strSQL &= "C.""製品名"" "
                'strSQL &= "FROM "
                'strSQL &= "WORKPROCESS_MS A, "
                'strSQL &= "PROCESS_MS B, "
                'strSQL &= "ITEM_MS C "
                'strSQL &= "WHERE "
                'strSQL &= "    A.""作業工程NO"" = '" + txtWorkProcessNo.Text + "' "
                'strSQL &= "AND A.""工程NO"" = B.""工程NO"" "
                'strSQL &= "AND A.""製品NO"" = C.""製品NO"" "
                'strSQL &= ") C "
                'strSQL &= "WHERE "
                'strSQL &= "    A.""作業工程NO"" = B.""作業工程NO"" "
                'strSQL &= "AND A.""作業工程NO"" = '" + txtWorkProcessNo.Text + "' "
                'strSQL &= "AND A.""出荷日"" = '" + dtpDateFrom.Value.Date + "' "

                'SQLコマンド設定
                cd.CommandText = strSQL
                cd.Connection = Cn
                cd.ExecuteNonQuery()
                dtReader = cd.ExecuteReader()

                If dtReader.Read() = True Then
                    txtWorkProcessName.Text = dtReader("作業工程名").ToString.Trim()
                    txtProcessNo.Text = dtReader("工程NO").ToString.Trim()
                    txtProcessName.Text = dtReader("工程名").ToString.Trim()
                    txtItemNo.Text = dtReader("製品NO").ToString.Trim()
                    txtItemName.Text = dtReader("製品名").ToString.Trim()

                    dtReader.Close()

                    '出荷、生産があるか確認
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= "  A.""出荷日"", "
                    strSQL &= "  A.""出荷数"", "
                    strSQL &= "  B.""生産日"", "
                    strSQL &= "  B.""生産数"" "
                    strSQL &= " FROM "
                    strSQL &= "  SHIPMENT_TBL A, "
                    strSQL &= "  CREATE_TBL B "
                    strSQL &= " WHERE "
                    strSQL &= "(    A.""出荷日"" BETWEEN '" + dtpDateFrom.Value.Date + "' AND '" + dtpDateTo.Value.Date + "' "
                    strSQL &= "OR B.""生産日"" BETWEEN '" + dtpDateFrom.Value.Date + "' AND '" + dtpDateTo.Value.Date + "') "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()
                    dtReader = cd.ExecuteReader()

                    If dtReader.Read = True Then
                        dtReader.Close()

                        '在庫数の取得
                        strSQL = ""
                        strSQL &= "SELECT "
                        strSQL &= "  SUM(""在庫数"") AS 在庫数 "
                        strSQL &= "FROM "
                        strSQL &= "STOCK_TBL "
                        strSQL &= "WHERE "
                        strSQL &= """作業工程NO""='" + txtWorkProcessNo.Text + "' "

                        'SQLコマンド設定
                        cd.CommandText = strSQL
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()
                        dtReader = cd.ExecuteReader()

                        If dtReader.Read() = True Then
                            txtStock.Text = dtReader("在庫数").ToString.Trim()
                        Else
                            txtStock.Text = 0

                        End If

                        Return True



                    Else
                        MessageBox.Show("出荷、もしくは生産の実績がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Cn.Close()
                        Cn.Dispose()

                        Return False

                    End If

                Else
                    MessageBox.Show("その作業工程NOは登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Cn.Close()
                    Cn.Dispose()

                    Return False

                End If


                'If dtReader.Read() = True Then
                '    txtWorkProcessName.Text = dtReader("作業工程名").ToString.Trim()
                '    txtProcessNo.Text = dtReader("工程NO").ToString.Trim()
                '    txtProcessName.Text = dtReader("工程名").ToString.Trim()
                '    txtItemNo.Text = dtReader("製品NO").ToString.Trim()
                '    txtItemName.Text = dtReader("製品名").ToString.Trim()

                '    dtReader.Close()

                '    '在庫数の取得
                '    strSQL = ""
                '    strSQL &= "SELECT "
                '    strSQL &= "  SUM(""在庫数"") AS 在庫数 "
                '    strSQL &= "FROM "
                '    strSQL &= "STOCK_TBL "
                '    strSQL &= "WHERE "
                '    strSQL &= """作業工程NO""='" + txtWorkProcessNo.Text + "' "

                '    'SQLコマンド設定
                '    cd.CommandText = strSQL
                '    cd.Connection = Cn
                '    cd.ExecuteNonQuery()
                '    dtReader = cd.ExecuteReader()

                '    If dtReader.Read() = True Then
                '        txtStock.Text = dtReader("在庫数").ToString.Trim()
                '    Else
                '        txtStock.Text = 0

                '    End If

                '    Return True
                'Else

                '    'lblMode.Text = "新規作成"

                '    MessageBox.Show("その作業工程NOは登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                '    'Call sgrpHumanOpen()
                '    Cn.Close()
                '    Cn.Dispose()

                '    Return False

                'End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString, "例外発生")
                Return False

            Finally
                Cn.Close()
                Cn.Dispose()
                dtReader.Close()

            End Try

        Else

        End If

    End Function

End Class
