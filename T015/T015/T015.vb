Imports System.Xml
Imports System.Data.SqlClient

Public Class T015

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                      　    ----------
    '------------------------------------------------
    Private Sub T015_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー処理        　    ----------
    '------------------------------------------------
    Private Sub T015_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sSearch()
            Case Keys.F2
                'クリア処理
                Call sClear()
            Case Keys.F3
                '終了処理
                Me.Close()
        End Select

    End Sub

    '------------------------------------------------
    '--検索ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '検索処理
        Call sSearch()
    End Sub

    '------------------------------------------------
    '--クリアボタン押下処理          　    ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--終了ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--受注先NO ボタン押下処理       　    ----------
    '------------------------------------------------
    Private Sub btnOrderSearch_Click(sender As Object, e As EventArgs) Handles btnOrderSearch.Click
        Dim frm As T015_2 = New T015_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--受注先NO チェック時処理       　    ----------
    '------------------------------------------------
    Private Sub rboOrder01_CheckedChanged(sender As Object, e As EventArgs) Handles rboOrder01.CheckedChanged
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
    End Sub

    '------------------------------------------------
    '--受注番号 チェック時処理       　    ----------
    '------------------------------------------------
    Private Sub rboOrder02_CheckedChanged(sender As Object, e As EventArgs) Handles rboOrder02.CheckedChanged
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
    End Sub

    '------------------------------------------------
    '--受注先NO Leave時処理          　    ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_Leave(sender As Object, e As EventArgs) Handles txtOrderMSNo.Leave
        '０埋め
        txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")
        '検索処理
        Call sOrderMS_Search()

    End Sub

    '------------------------------------------------
    '--受注番号 KeyPress時処理       　    ----------
    '------------------------------------------------
    Private Sub txtOrderNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNO.KeyPress
        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    '------------------------------------------------
    '--受注一覧 検索処理             　    ----------
    '------------------------------------------------
    Public Sub sSearch()
        '初期化
        Call sClear_DataGridView()
        Dim result As Boolean
        Me.ActiveControl = Nothing
        result = fCheckSearch()
        If result = True Then
            'DB接続
            Call sDBconnect()
            '検索メイン処理
            Call fMainSearch()
        End If


    End Sub

    '------------------------------------------------
    '--受注一覧 検索チェック処理     　    ----------
    '------------------------------------------------
    Public Function fCheckSearch() As Boolean

        'ラジオボタンがチェックされていない場合はエラー
        If rboOrder03.Checked = True Then
            MessageBox.Show("検索条件が選択されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--受注一覧 検索メイン処理       　    ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " A.受注NO, "
            strSQL &= " A.受注先NO, "
            strSQL &= " C.受注先名, "
            strSQL &= " A.作業工程NO, "
            strSQL &= " D.作業工程名, "
            strSQL &= " A.受注日, "
            strSQL &= " A.受注数, "
            strSQL &= " COALESCE(SUM(B.出荷数), 0) AS 出荷数, "
            'strSQL &= " SUM(B.出荷数) AS 出荷数, "
            strSQL &= " A.受注チェックフラグ, "
            strSQL &= " A.出荷チェックフラグ "
            strSQL &= "FROM "
            'strSQL &= " ORDER_TBL A, "
            'strSQL &= " SHIPMENT_TBL B, "
            strSQL &= " ORDER_TBL A LEFT JOIN "
            strSQL &= " SHIPMENT_TBL B ON A.受注NO = B.受注NO, "
            strSQL &= " ORDER_MS C, "
            strSQL &= " WORKPROCESS_MS D "
            strSQL &= "WHERE "
            'strSQL &= "    A.受注NO = B.受注NO "
            strSQL &= "    A.受注先NO = C.受注先NO "
            strSQL &= "AND A.作業工程NO = D.作業工程NO "
            If rboOrder01.Checked = True Then
                strSQL &= "AND A.受注先NO = '" & txtOrderMSNo.Text.Trim & "' "
                strSQL &= "AND A.受注日 BETWEEN "
                strSQL &= "    '" & dtpOrderDateFrom.Text.Trim & "' AND '" & dtpOrderDateTo.Text.Trim & "' "
            ElseIf rboOrder02.Checked = True Then
                strSQL &= "AND A.受注NO = " & txtOrderNO.Text.Trim & " "
            End If
            strSQL &= "GROUP BY "
            strSQL &= " A.受注NO, "
            strSQL &= " A.受注先NO, "
            strSQL &= " C.受注先名, "
            strSQL &= " A.作業工程NO, "
            strSQL &= " D.作業工程名, "
            strSQL &= " A.受注日, "
            strSQL &= " A.受注数, "
            strSQL &= " A.受注チェックフラグ, "
            strSQL &= " A.出荷チェックフラグ "
            strSQL &= "ORDER BY "
            strSQL &= " A.受注NO "


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
                    For y = 0 To 9
                        DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim()
                    Next
                Next

                '受注・出荷チェックフラグは非表示
                DataGridView1.Columns(8).Visible = False
                DataGridView1.Columns(9).Visible = False

                '//受注残セルの追加
                Dim dgvOrder As New DataGridViewTextBoxColumn
                DataGridView1.Columns.Insert(8, dgvOrder)
                'ヘッダー名の設定
                DataGridView1.Columns(8).HeaderText = "受注残"
                '受注残の計算
                For i = 0 To DataGridView1.RowCount - 1
                    '受注残 = 受注数 - 合計出荷数
                    DataGridView1.Item(8, i).Value = DataGridView1.Item(6, i).Value - DataGridView1.Item(7, i).Value
                    '//セルの色変更
                    '0の場合は、Aqua。0以上の場合は、Yellow
                    If DataGridView1.Item(8, i).Value <= 0 Then
                        DataGridView1.Item(8, i).Style.BackColor = Color.Aqua
                    Else
                        DataGridView1.Item(8, i).Style.BackColor = Color.Yellow
                    End If

                Next

                '//判定式セルの追加
                Dim dgvJudge As New DataGridViewTextBoxColumn
                DataGridView1.Columns.Insert(11, dgvJudge)
                'ヘッダー名の設定
                DataGridView1.Columns(11).HeaderText = "判定"
                '判定
                For i = 0 To DataGridView1.RowCount - 1
                    If DataGridView1.Item(10, i).Value = True Then
                        '出荷チェックフラグ=1の場合は、「完了」ライム背景を表示
                        DataGridView1.Item(11, i).Value = "完了"
                        DataGridView1.Item(11, i).Style.BackColor = Color.Lime
                    ElseIf DataGridView1.Item(9, i).Value = False Then
                        '受注チェックフラグ=0の場合は、「未チェック」赤背景表示
                        DataGridView1.Item(11, i).Value = "未チェック"
                        DataGridView1.Item(11, i).Style.BackColor = Color.Red
                    ElseIf DataGridView1.Item(8, i).Value > 0 Then
                        '受注残が0以上の場合は、「残あり」黄色背景表示
                        DataGridView1.Item(11, i).Value = "残あり"
                        DataGridView1.Item(11, i).Style.BackColor = Color.Yellow
                    ElseIf DataGridView1.Item(8, i).Value <= 0 Then
                        '受注残が0の場合は、「残なし」アクア背景表示
                        DataGridView1.Item(11, i).Value = "残なし"
                        DataGridView1.Item(11, i).Style.BackColor = Color.Aqua
                    End If
                Next

                'DataGridView1のすべての列の幅を自動調整する
                DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

                '並び替えができないようにする
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                Return True


            Else
                MessageBox.Show("検索結果が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--受注先NO 検索処理          　    ----------
    '------------------------------------------------
    Public Sub sOrderMS_Search()
        'SB接続
        Call sDBconnect()
        '検索メイン処理
        Call fOrderMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--受注先NO 検索メイン処理       　    ----------
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
