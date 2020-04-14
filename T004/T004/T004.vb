Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.Xml


Public Class T004

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String
    Dim blnResult As Boolean '名前・パスワード判定用

    '---------------------------------------------
    '---ロード処理                             ---
    '---------------------------------------------
    Private Sub T004_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call sClear()

    End Sub

    '---------------------------------------------
    '---製品NO KeyPress処理                     ---
    '---------------------------------------------
    Private Sub txtItemNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemNO.KeyPress
        '0～9と、バックスペース以外の時は、イベントをキャンセルする
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso _
                e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    '---------------------------------------------
    '---製品NO KeyDown処理                     ---
    '---------------------------------------------
    Private Sub txtItemNO_KeyDown(sender As Object, e As KeyEventArgs) Handles txtItemNO.KeyDown

        If e.KeyData = Keys.Enter Then
            Dim result As Boolean


            '３桁まで０埋め
            txtItemNO.Text = txtItemNO.Text.PadLeft(3, "0")

            'DB接続
            'Call sDBConnect()
            result = sXmlConnection()

            'DataGridViewの値取得
            If result = True Then
                Call sDataGridViewConnect()
            End If





        End If

    End Sub

    '------------------------------------------------
    '--製品検索ボタンの押下                ----------
    '------------------------------------------------
    Private Sub btnIemNoSearch_Click(sender As Object, e As EventArgs) Handles btnIemNoSearch.Click
        Dim frm As T004_2 = New T004_2()
        T004_2.ShowDialog(Me)
        Me.txtItemNO.Focus()

    End Sub

    '------------------------------------------------
    '--保存ボタンの押下                    ----------
    '------------------------------------------------
    Private Sub btnExcelSave_Click(sender As Object, e As EventArgs) Handles btnExcelSave.Click

        Call sExcelSavePrint(1)

    End Sub

    '------------------------------------------------
    '--印刷ボタンの押下                    ----------
    '------------------------------------------------
    Private Sub btnExcelPrint_Click(sender As Object, e As EventArgs) Handles btnExcelPrint.Click

        Call sExcelSavePrint(2)

    End Sub

    '------------------------------------------------
    '--クリアボタンの押下                    ----------
    '------------------------------------------------
    Private Sub btn_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        sClear()
    End Sub

    '------------------------------------------------
    '--終了ボタンの押下                    ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    '------------------------------------------------
    '--SQLServerへの接続の開始             ----------
    '------------------------------------------------
    Public Sub sDBConnect()

        'DBに接続
        'strServerName = "SHINYA-PC\NAKADB" 'サーバー名(またはIPアドレス)
        'strUserID = "sa" 'ユーザーID
        'strPassword = "naka" 'パスワード
        'strDatabaseName = "NAKADB" 'データベース

        'strConnect = "Server=" & strServerName & ";"
        'strConnect &= "User ID=" & strUserID & ";"
        'strConnect &= "Password=" & strPassword & ";"
        'strConnect &= "Initial Catalog=" & strDatabaseName

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

        txtItemNO.Text = ""
        txtItemName.Text = ""
        txtItemName.Enabled = False

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()


        Chart1.Series.Clear()

    End Sub

    '---------------------------------------------
    '---XMLファイル読み込み表示処理            ---
    '---------------------------------------------
    Public Function sXmlConnection() As Boolean

        Dim strServer As String
        Dim strUserID As String
        Dim strPassword As String
        Dim strDatabaseName As String

        Dim xmlNode As XmlNodeList 'XMLノード取得用

        Dim strFileAdress As String 'XMLファイルのアドレス
        strFileAdress = "C:\SQLServer\DBConnect.xml"

        Dim xmlDoc As New XmlDocument()


        Try
            'ファイル存在チェック
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
                'パスワードの取得
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

    '---------------------------------------------
    '---DataGridView表示処理                   ---
    '---------------------------------------------
    Public Function sDataGridViewConnect() As Boolean

        Dim dtReader As SqlDataReader

        Try

            '製品NOが存在するのか確認
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " ""製品NO"","
            strSQL &= " ""製品名""　"
            strSQL &= "FROM "
            strSQL &= " ITEM_MS "
            strSQL &= "WHERE "
            strSQL &= "  ""製品NO"" = '" + txtItemNO.Text.Trim + "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            cd.ExecuteNonQuery()
            dtReader = cd.ExecuteReader()

            If dtReader.HasRows Then

                While dtReader.Read()
                    txtItemName.Text = dtReader("製品名")
                End While

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

                    '生産、出荷の取得
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " CASE "
                    strSQL &= "  WHEN C.""出荷日"" is null THEN B.""生産日"" "
                    strSQL &= "  WHEN B.""生産日"" is null THEN C.""出荷日"" "
                    strSQL &= "  ELSE B.""生産日"" "
                    strSQL &= " END ""日付"", "
                    strSQL &= " B.""生産数"", "
                    strSQL &= " C.""出荷数"" "
                    strSQL &= "FROM "
                    strSQL &= " (SELECT "
                    strSQL &= "   A.""製品NO"", "
                    strSQL &= "   A.""製品名"", "
                    strSQL &= "   B.""作業工程NO"" "
                    strSQL &= "  FROM "
                    strSQL &= "   ITEM_MS A, "
                    strSQL &= "   WORKPROCESS_MS B "
                    strSQL &= "  WHERE "
                    strSQL &= "   A.""製品NO"" = '" + txtItemNO.Text.Trim + "' "
                    strSQL &= "   AND A.""製品NO"" = B.""製品NO"") A, "
                    strSQL &= " (SELECT "
                    strSQL &= "   ""作業工程NO"", "
                    strSQL &= "   ""生産数"", "
                    strSQL &= "   ""生産日"" "
                    strSQL &= "FROM "
                    strSQL &= "     CREATE_TBL "
                    strSQL &= ") B "
                    strSQL &= " FULL OUTER JOIN "
                    strSQL &= " (SELECT "
                    strSQL &= "   ""作業工程NO"", "
                    strSQL &= "   ""出荷数"", "
                    strSQL &= "   ""出荷日"" "
                    strSQL &= "FROM "
                    strSQL &= "   SHIPMENT_TBL "
                    strSQL &= ") C ON B.""生産日"" = C.""出荷日"" "
                    strSQL &= " AND C.""出荷日"" = B.""生産日"" "
                    strSQL &= "WHERE "
                    strSQL &= " (A.""作業工程NO"" = B.""作業工程NO"" "
                    strSQL &= "OR A.""作業工程NO"" = C.""作業工程NO"") "
                    strSQL &= "AND (B.""生産日"" BETWEEN '" + dtpDateFrom.Value.Date + "' AND '" + dtpDateTo.Value.Date + "'  "
                    strSQL &= "OR   C.""出荷日"" BETWEEN '" + dtpDateFrom.Value.Date + "' AND '" + dtpDateTo.Value.Date + "') "
                    strSQL &= " ORDER BY "
                    strSQL &= " ""日付"" "

                    Dim dsDataSet As New DataSet
                    Dim daDataAdapter As New SqlClient.SqlDataAdapter

                    daDataAdapter.SelectCommand = New SqlClient.SqlCommand(strSQL, Cn)
                    daDataAdapter.SelectCommand.CommandTimeout = 0
                    daDataAdapter.Fill(dsDataSet, "TABLE001")
                    DataGridView1.DataSource = dsDataSet.Tables("TABLE001")

                    'グラフの初期化
                    Chart1.Series.Clear()

                    'グラフの設定
                    With Chart1
                        'データソースをつなげる
                        .DataSource = dsDataSet
                        '末尾まで繰り返す
                        For i = 1 To dsDataSet.Tables("TABLE001").Columns.Count - 1
                            Dim strColumnName As String = dsDataSet.Tables("TABLE001").Columns(i).ColumnName.ToString()
                            .Series.Add(strColumnName)
                            'X 軸のラベルテキストの読込・設定(日付の設定)
                            .Series(strColumnName).XValueMember = dsDataSet.Tables("TABLE001").Columns(0).ColumnName.ToString()
                            'Y 軸のラベルテキストの読込・設定(生産数、出荷数の設定)
                            .Series(strColumnName).YValueMembers = strColumnName

                        Next

                        'Chart1.ChartAreas(0).AxisX.Maximum = dsDataSet.Tables(0).Columns.Count - 1

                        'データポイントラベルの設定
                        .Series(0).Label = "#VALY"
                        .Series(1).Label = "#VALY"

                    End With


                    DataGridView1.Columns(1).DefaultCellStyle.Format = "G"
                    DataGridView1.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    DataGridView1.Columns(2).DefaultCellStyle.Format = "G"
                    DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    Cn.Close()
                    Cn.Dispose()

                    Return True



                Else
                    MessageBox.Show("出荷、もしくは生産の実績がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Cn.Close()
                    Cn.Dispose()

                    Return False

                End If

            Else
                MessageBox.Show("その製品NOは存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cn.Close()
                Cn.Dispose()

                Return False


            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            Return False


        Finally
            Cn.Close()
            Cn.Dispose()

        End Try

        '製品NOが登録されているか確認

        '日付で検索して一致するか

        '一致したら取得

    End Function

    '---------------------------------------------
    '---EXCEL保存処理                          ---
    '---------------------------------------------
    Public Sub sExcelSavePrint(intDec As Integer)

        'DataGridViewが０件の場合はエラー
        If DataGridView1.Columns.Count <> 0 Then
            ' EXCEL関連オブジェクトの定義
            Dim objExcel As Excel.Application = New Excel.Application
            Dim objWorkBook As Excel.Workbook = objExcel.Workbooks.Add
            'Dim objSheet As Excel.Worksheet = Nothing



            Dim sfd As New SaveFileDialog()

            '初期設定
            sfd.InitialDirectory = "C:\"
            sfd.Filter = "EXCELファイル(*.xlsx)|*.xlsx"
            sfd.Title = "保存先のファイルを選択してください。"



            'シートの最大表示列項目数
            Dim columnMaxNum As Integer = DataGridView1.Columns.Count - 1
            'シートの最大表示行項目数
            Dim rowMaxNum As Integer = DataGridView1.Rows.Count - 1

            '項目名格納用リストを宣言
            Dim columnList As New List(Of String)
            'ヘッダー名を取得
            For i As Integer = 0 To (columnMaxNum)
                columnList.Add(DataGridView1.Columns(i).HeaderCell.Value)
            Next

            'セルのデータ取得用二次元配列を宣言
            Dim v As String(,) = New String(rowMaxNum, columnMaxNum) {}

            For row As Integer = 0 To rowMaxNum
                For col As Integer = 0 To columnMaxNum
                    If DataGridView1.Rows(row).Cells(col).Value Is Nothing = False Then
                        ' セルに値が入っている場合、二次元配列に格納
                        '1列目：文字列型、2列目：数値、3列目：数値
                        If col = 0 Then
                            '文字列型("YYYY-MM-DD")
                            v(row, col) = Strings.Left(DataGridView1.Rows(row).Cells(col).Value.ToString(), 10)
                        ElseIf col = 1 Or col = 2 Then
                            '数値型
                            If IsDBNull(DataGridView1.Rows(row).Cells(col).Value) = False Then
                                'v(row, col) = CInt(DataGridView1.Rows(row).Cells(col).Value)
                                v(row, col) = DataGridView1.Rows(row).Cells(col).Value
                            Else
                                v(row, col) = 0

                            End If

                        End If

                    End If
                Next
            Next

            ' EXCELに項目名を転送
            For i As Integer = 1 To DataGridView1.Columns.Count
                ' シートの一行目に項目を挿入
                objWorkBook.Sheets(1).Cells(1, i) = columnList(i - 1)

                ' 罫線を設定
                objWorkBook.Sheets(1).Cells(1, i).Borders.LineStyle = True
                '列幅の調整
                objWorkBook.Sheets(1).Columns("A").ColumnWidth = 12
                ' 項目の表示行に背景色を設定
                objWorkBook.Sheets(1).Cells(1, i).Interior.Color = RGB(140, 140, 140)
                ' 文字のフォントを設定
                objWorkBook.Sheets(1).Cells(1, i).Font.Color = RGB(255, 255, 255)
                objWorkBook.Sheets(1).Cells(1, i).Font.Bold = True
            Next

            ' EXCELにデータを範囲指定で転送
            Dim strRange As String = "A2:" & Chr(Asc("A") + columnMaxNum) & DataGridView1.Rows.Count + 1
            objWorkBook.Sheets(1).Range(strRange) = v
            ' データの表示範囲に罫線を設定
            objWorkBook.Sheets(1).Range(strRange).Borders.LineStyle = True





            '保存か印刷判定
            If intDec = 1 Then
                '保存の場合
                If sfd.ShowDialog() = DialogResult.OK Then
                    objWorkBook.SaveAs(Filename:=sfd.FileName)
                    MessageBox.Show("保存できました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                End If
            ElseIf intDec = 2 Then
                '印刷の場合
                ' エクセル表示
                objExcel.Visible = True
                objExcel.DisplayAlerts = False

                objWorkBook.Sheets(1).PrintPreview()


            End If

            'Marshal.ReleaseComObject(objWorkBook)
            'Marshal.ReleaseComObject(objExcel)

            objWorkBook.Close()
            objExcel.Quit()

            objWorkBook = Nothing
            objExcel = Nothing

        Else
            MessageBox.Show("検索結果が０件なので保存できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If

    End Sub



End Class
