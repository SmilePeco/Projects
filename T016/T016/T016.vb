Imports System.Xml
Imports System.Data.SqlClient

Public Class T016

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                      　    ----------
    '------------------------------------------------
    Private Sub T016_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'クリア処理
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー押下処理    　    ----------
    '------------------------------------------------
    Private Sub T016_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sSearch()
            Case Keys.F2
                '更新処理
                Call sUpdate()
            Case Keys.F3
                'クリア処理
                Call sClear()
            Case Keys.F4
                '終了処理
                Me.Close()

        End Select

    End Sub

    '------------------------------------------------
    '--検索ボタン押下押下処理        　    ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        '検索処理
        Call sSearch()
    End Sub

    '------------------------------------------------
    '--更新ボタン押下押下処理        　    ----------
    '------------------------------------------------
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
        '更新処理
        Call sUpdate()
    End Sub

    '------------------------------------------------
    '--クリアボタン押下押下処理      　    ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--終了ボタン押下押下処理        　    ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs)
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--受注先NO Leave処理            　    ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_Leave(sender As Object, e As EventArgs) Handles txtOrderMSNo.Leave
        '検索処理
        Call sOrderMS_Search()

    End Sub

    '------------------------------------------------
    '--受注番号 KeyPress処理            　    ----------
    '------------------------------------------------
    Private Sub txtOrderNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNO.KeyPress

        If (e.KeyChar < "0"c OrElse e.KeyChar > "9c") AndAlso _
            e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    '------------------------------------------------
    '--受注先NO チェック処理         　    ----------
    '------------------------------------------------
    Private Sub rboOrder01_CheckedChanged(sender As Object, e As EventArgs) Handles rboOrder01.CheckedChanged
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
    End Sub

    '------------------------------------------------
    '--受注番号 チェック処理         　    ----------
    '------------------------------------------------
    Private Sub rboOrder02_CheckedChanged(sender As Object, e As EventArgs) Handles rboOrder02.CheckedChanged
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
    End Sub

    '------------------------------------------------
    '--受注先NO ボタン押下処理       　    ----------
    '------------------------------------------------
    Private Sub btnOrderSearch_Click(sender As Object, e As EventArgs) Handles btnOrderSearch.Click
        Dim frm As New T016_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--更新担当者 ボタン押下処理     　    ----------
    '------------------------------------------------
    Private Sub btnUserIDSearch_Click(sender As Object, e As EventArgs)
        Dim frm As New T016_3
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--出荷完了 検索処理             　    ----------
    '------------------------------------------------
    Public Sub sSearch()
        Dim result As Boolean
        Me.ActiveControl = Nothing
        '初期化
        Call sClear_DataGridView()
        'チェック処理
        result = fCheckSearch()
        If result = True Then
            'DB接続
            Call sDBconnect()
            '検索処理
            Call fMainSearch()
        End If

    End Sub

    '------------------------------------------------
    '--出荷完了 検索チェック処理     　    ----------
    '------------------------------------------------
    Public Function fCheckSearch() As Boolean

        '検索条件が未選択の場合は、エラー
        If rboOrder03.Checked = True Then
            MessageBox.Show("検索条件が未選択です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        '受注番号が未入力の場合はエラー
        If rboOrder02.Checked = True Then
            If txtOrderNO.Text.Trim = "" Then
                MessageBox.Show("受注番号が未入力です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        End If

        '問題なければTRUEを返す
        Return True


    End Function

    '------------------------------------------------
    '--出荷完了 検索メイン処理       　    ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " A.受注NO, "
            strSQL &= " A.受注先NO, "
            strSQL &= " B.出荷先NO, "
            strSQL &= " A.受注数, "
            strSQL &= " B.出荷数, "
            strSQL &= " B.受注残, "
            strSQL &= " A.受注日, "
            strSQL &= " A.最終更新者, "
            strSQL &= " A.更新日, "
            strSQL &= " C.製品NO, "
            strSQL &= " C.金額 "
            strSQL &= "FROM "
            strSQL &= " ORDER_TBL A, "
            strSQL &= " (SELECT "
            strSQL &= "   A.受注NO, "
            strSQL &= "   B.出荷先NO, "
            strSQL &= "	  A.受注数, "
            strSQL &= "   COALESCE(SUM(B.出荷数), 0) AS 出荷数, "
            strSQL &= "   A.受注数 - COALESCE(SUM(B.出荷数), 0) as 受注残, "
            strSQL &= "   B.出荷チェックフラグ "
            strSQL &= "  FROM "
            strSQL &= "   ORDER_TBL A, "
            strSQL &= "   SHIPMENT_TBL B "
            strSQL &= "  WHERE "
            strSQL &= "   A.受注NO = B.受注NO "
            strSQL &= "  GROUP BY "
            strSQL &= "   A.受注NO, "
            strSQL &= "   B.出荷先NO, "
            strSQL &= "   A.受注数, "
            strSQL &= "   B.出荷チェックフラグ "
            strSQL &= " ) B,  "
            strSQL &= " (SELECT "
            strSQL &= "   A.受注NO, "
            strSQL &= "   C.製品NO, "
            strSQL &= "   C.金額 "
            strSQL &= "  FROM "
            strSQL &= "   ORDER_TBL A, "
            strSQL &= "   WORKPROCESS_MS B, "
            strSQL &= "   ITEM_HISTORY_MS C "
            strSQL &= "  WHERE "
            strSQL &= "       A.作業工程NO = B.作業工程NO "
            strSQL &= "   AND B.製品NO = C.製品NO "
            strSQL &= "   AND C.更新日 = ( "
            strSQL &= "                SELECT "
            strSQL &= "                    MAX(更新日) "
            strSQL &= "                FROM "
            strSQL &= "                    ITEM_HISTORY_MS A, "
            strSQL &= "                    ( "
            strSQL &= "                        SELECT "
            strSQL &= "                            B.製品NO "
            strSQL &= "                        FROM "
            strSQL &= "                            ORDER_TBL A, "
            strSQL &= "                            WORKPROCESS_MS B "
            strSQL &= "                        WHERE "
            strSQL &= "                            A.作業工程NO = B.作業工程NO "
            '                              検索条件に受注先NOを選択していた場合
            If rboOrder01.Checked = True Then
                strSQL &= "                        AND A.受注先NO = '" & txtOrderMSNo.Text.Trim & "' "
                strSQL &= "                        AND A.受注日 BETWEEN '" & dtpOrderDateFrom.Text.Trim & "' AND '" & dtpOrderDateTo.Text.Trim & "' "
            ElseIf rboOrder02.Checked = True Then
                strSQL &= "                        AND A.受注NO = " & txtOrderNO.Text.Trim & " "
            End If
            strSQL &= "                    ) B "
            strSQL &= "                WHERE "
            strSQL &= "                    A.製品NO = B.製品NO "
            strSQL &= "            ) "
            strSQL &= "   ) C "
            strSQL &= "WHERE "
            strSQL &= "    A.受注NO = B.受注NO "
            strSQL &= "AND A.受注NO = C.受注NO "
            strSQL &= "AND B.受注残 = 0 "
            strSQL &= "AND A.受注チェックフラグ = 1 "
            strSQL &= "AND A.出荷チェックフラグ = 0 "
            strSQL &= "AND B.出荷チェックフラグ = 1 "
            If rboOrder01.Checked = True Then
                strSQL &= "AND A.受注先NO = '" & txtOrderMSNo.Text.Trim & "' "
                strSQL &= "AND A.受注日 BETWEEN "
                strSQL &= "    '" & dtpOrderDateFrom.Text.Trim & "' AND '" & dtpOrderDateTo.Text.Trim & "' "
            ElseIf rboOrder02.Checked = True Then
                strSQL &= "AND A.受注NO = " & txtOrderNO.Text.Trim & " "
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

                'トリム処理
                For i = 0 To DataGridView1.RowCount - 1
                    For y = 0 To DataGridView1.ColumnCount - 1
                        DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim
                        DataGridView1.Item(y, i).ReadOnly = True
                    Next
                Next
                'チェックボックスの追加
                Dim dgvCheck As New DataGridViewCheckBoxColumn
                DataGridView1.Columns.Insert(0, dgvCheck)

                '製品NOと金額はVisible=FALSE
                DataGridView1.Columns(10).Visible = False
                DataGridView1.Columns(11).Visible = False

                'DataGridView1のすべての列の幅を自動調整する
                DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

                Return True

            Else
                MessageBox.Show("検索結果が０件です。" & vbCrLf & "受注残チェック画面で確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--出荷完了 更新処理             　    ----------
    '------------------------------------------------
    Public Sub sUpdate()
        Dim result As Boolean
        '編集モードを解除
        DataGridView1.EndEdit()
        'チェック処理
        result = fCheckUpdate()
        If result = True Then
            'DB接続
            Call sDBconnect()
            '更新処理
            result = fMainUpdate()
            If result = True Then
                Call sClear()
            End If

        End If


    End Sub

    '------------------------------------------------
    '--出荷完了 更新前チェック処理   　    ----------
    '------------------------------------------------
    Public Function fCheckUpdate() As Boolean

        Dim intCount As Integer 'チェック数をカウント
        Dim blnResult As Boolean

        Dim result As DialogResult = MessageBox.Show("更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
        If result = Windows.Forms.DialogResult.Yes Then
            'DataGridView1が空の場合は、エラー
            If DataGridView1.RowCount = 0 Then
                MessageBox.Show("検索されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            '入力した更新担当者が存在するかチェック
            Call sDBconnect()
            blnResult = fChechkUpdate2()
            If blnResult = False Then
                MessageBox.Show("入力した更新担当者が存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If



            'チェックした数をカウント
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Item(0, i).Value = True Then
                    intCount += 1
                End If
            Next

            'チェックが０件の場合は、エラー
            If intCount = 0 Then
                MessageBox.Show("チェック数が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            '問題なければTRUEを返す
            Return True

        Else
            Return False
        End If

    End Function

    '------------------------------------------------
    '--出荷完了 更新チェック処理(更新担当者)----------
    '------------------------------------------------
    Public Function fChechkUpdate2() As Boolean

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 名前 "
            strSQL &= "FROM "
            strSQL &= " HUMAN_MS "
            strSQL &= "WHERE "
            strSQL &= " 名前 = '" & txtUserID.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                dtReader.Close()
                '問題なければTRUEを返す
                Return True

            Else
                '存在しない場合は、エラー
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
    '--出荷完了 更新メイン処理       　    ----------
    '------------------------------------------------
    Public Function fMainUpdate() As Boolean


        Dim intCount As Integer '更新数のカウント
        Dim intSalesNO As Integer '売上テーブルの売上NOカウント数
        Dim intSalesMoney As Integer '売上金額計算用（製品単価✕売上数）
        Dim dtReader As SqlDataReader
        Dim tran As SqlTransaction
        tran = Cn.BeginTransaction

        Try
            For i = 0 To DataGridView1.RowCount - 1
                'チェック済み項目のみ更新対象
                If DataGridView1.Item(0, i).Value = True Then
                    '受注テーブルの更新
                    strSQL = ""
                    strSQL &= "UPDATE ORDER_TBL "
                    strSQL &= "SET "
                    strSQL &= " 出荷チェックフラグ = 1, "
                    strSQL &= " 最終更新者 = '" & txtUserID.Text.Trim & "', "
                    strSQL &= " 更新日 = SYSDATETIME() "
                    strSQL &= "WHERE "
                    strSQL &= " 受注NO = " & DataGridView1(1, i).Value.ToString.Trim & " "

                    cd.CommandText = strSQL
                    cd.Transaction = tran
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    '//売上テーブルへの新規登録
                    '売上NOの取得。連番取得する。
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= "  COALESCE(MAX(売上NO), 0)AS 売上NO "
                    strSQL &= "FROM "
                    strSQL &= " SALES_TBL "

                    cd.CommandText = strSQL
                    cd.Transaction = tran
                    cd.Connection = Cn
                    dtReader = cd.ExecuteReader

                    If dtReader.HasRows Then
                        While dtReader.Read
                            '売上NOの値を代入し、＋１加算する
                            intSalesNO = dtReader("売上NO")
                        End While
                        dtReader.Close()
                        intSalesNO += 1
                    Else
                        'ありないが、１を加算しておく
                        dtReader.Close()
                        intSalesNO += 1
                    End If

                    '売上金額計算（製品単価✕売上数）
                    intSalesMoney = DataGridView1.Item(4, i).Value * DataGridView1.Item(11, i).Value

                    '新規登録のSQL発行
                    strSQL = ""
                    strSQL &= "INSERT INTO SALES_TBL VALUES "
                    strSQL &= "( "
                    strSQL &= "" & intSalesNO & ", " '売上NO
                    strSQL &= "'" & DataGridView1.Item(1, i).Value.ToString.Trim & "', " '受注NO
                    strSQL &= "'" & DataGridView1.Item(2, i).Value.ToString.Trim & "', " '受注先NO
                    strSQL &= "'" & DataGridView1.Item(3, i).Value.ToString.Trim & "', " '出荷先NO
                    strSQL &= "'" & DataGridView1.Item(10, i).Value.ToString.Trim & "', " '製品NO
                    strSQL &= "" & DataGridView1.Item(11, i).Value.ToString.Trim & ", " '製品単価
                    strSQL &= "" & DataGridView1.Item(4, i).Value.ToString.Trim & ", " '売上数
                    strSQL &= "" & intSalesMoney & ", " '売上金額
                    strSQL &= "SYSDATETIME(), " '売上日
                    strSQL &= "'" & txtUserID.Text.Trim & "', " '最終更新者
                    strSQL &= "SYSDATETIME(), " '更新日
                    strSQL &= "SYSDATETIME() " '登録日
                    strSQL &= ") "

                    cd.CommandText = strSQL
                    cd.Transaction = tran
                    cd.Connection = Cn
                    cd.ExecuteNonQuery()

                    intCount += 1

                End If

            Next

            '更新処理
            If intCount <> 0 Then
                MessageBox.Show(intCount & "件更新完了しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                tran.Commit()
                Return True
            Else
                '０件はありえないけど、RollbackしてFALSEを返しておく
                tran.Rollback()
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            tran.Rollback()
            Return False
        Finally
            Cn.Close()
            Cn.Dispose()
        End Try



    End Function

    '------------------------------------------------
    '--受注先NO 検索処理             　    ----------
    '------------------------------------------------
    Public Sub sOrderMS_Search()
        '０埋め
        txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBconnect()
        '検索メイン処理
        Call fOrderMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--受注先NO 検索メイン処理        　    ----------
    '------------------------------------------------
    Public Function fOrderMS_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 受注先NO, "
            strSQL &= " 受注先名 "
            strSQL &= "FROM "
            strSQL &= " ORDER_MS "
            strSQL &= "WHERE "
            strSQL &= " 受注先NO = '" & txtOrderMSNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    lblOrderMSName.Text = dtReader("受注先名").ToString.Trim()
                End While
                dtReader.Close()
                Return True

            Else
                dtReader.Close()
                lblOrderMSName.Text = ""
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
    '--クリア処理                    　    ----------
    '------------------------------------------------
    Public Sub sClear()

        'ラジオボタン関連
        rboOrder03.Visible = False
        rboOrder03.Checked = True
        'テキストボックス、ラベル関連
        txtOrderNO.Clear()
        txtOrderMSNo.Clear()
        lblOrderMSName.Text = ""
        '日付関連
        dtpOrderDateFrom.Text = Date.Now
        dtpOrderDateTo.Text = Date.Now
        'DataGridView関連
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        'Groupbox関連
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False


    End Sub

    '------------------------------------------------
    '--クリア処理DataGridViewのみ    　    ----------
    '------------------------------------------------
    Public Sub sClear_DataGridView()
        'DataGridView関連
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub

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
