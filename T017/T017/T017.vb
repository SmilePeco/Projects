Imports System.Data.SqlClient
Imports System.Xml

Public Class T017

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                      　    ----------
    '------------------------------------------------
    Private Sub T017_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--ファンクションキー処理        　    ----------
    '------------------------------------------------
    Private Sub T017_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sSearch()

        End Select

    End Sub

    '------------------------------------------------
    '--売上年月 ラジオボタン選択処理 　    ----------
    '------------------------------------------------
    Private Sub rboSales01_CheckedChanged(sender As Object, e As EventArgs) Handles rboSales01.CheckedChanged
        GroupBox2.Enabled = True
    End Sub

    '------------------------------------------------
    '--年月指定なし ラジオボタン選択処理   ----------
    '------------------------------------------------
    Private Sub rboSales02_CheckedChanged(sender As Object, e As EventArgs) Handles rboSales02.CheckedChanged
        GroupBox2.Enabled = False
    End Sub

    '------------------------------------------------
    '-受注先NO チェック処理                ----------
    '------------------------------------------------
    Private Sub chkOrder01_CheckedChanged(sender As Object, e As EventArgs) Handles chkWhereOrder01.CheckedChanged
        If chkWhereOrder01.Checked = True Then
            GroupBox3.Enabled = True
            '検索結果にも表示されるようにする
            chkSelectOrder01.Checked = True
        Else
            GroupBox3.Enabled = False
            chkSelectOrder01.Checked = False
        End If

    End Sub

    '------------------------------------------------
    '-出荷先NO チェック処理                ----------
    '------------------------------------------------
    Private Sub chkShipment01_CheckedChanged(sender As Object, e As EventArgs) Handles chkWhereShipment01.CheckedChanged
        If chkWhereShipment01.Checked = True Then
            GroupBox5.Enabled = True
            '検索結果にも表示されるようにする
            chkSelectShipment01.Checked = True
        Else
            GroupBox5.Enabled = False
            chkSelectShipment01.Checked = False
        End If


    End Sub

    '------------------------------------------------
    '--受注先NO ボタン押下処理        　   ----------
    '------------------------------------------------
    Private Sub btnOrderSearch_Click(sender As Object, e As EventArgs) Handles btnOrderSearch.Click
        Dim frm As New T017_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--出荷先NO ボタン押下処理        　   ----------
    '------------------------------------------------
    Private Sub btnShipmentSearch_Click(sender As Object, e As EventArgs) Handles btnShipmentSearch.Click
        Dim frm As New T017_3
        frm.ShowDialog(Me)
    End Sub

    '------------------------------------------------
    '--受注先NO Leave処理             　   ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_Leave(sender As Object, e As EventArgs) Handles txtOrderMSNo.Leave
        '検索処理
        Call sOrderMS_Search()
    End Sub

    '------------------------------------------------
    '--出荷先NO Leave処理             　   ----------
    '------------------------------------------------
    Private Sub txtShipmentMSNo_Leave(sender As Object, e As EventArgs) Handles txtShipmentMSNo.Leave
        '検索処理
        Call sShipmentMS_Search()
    End Sub

    '------------------------------------------------
    '--売上確認 検索処理             　    ----------
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
            '検索メイン処理
            Call fMainSearch()

        End If

    End Sub

    '------------------------------------------------
    '--売上確認 検索チェック処理       　    ----------
    '------------------------------------------------
    Public Function fCheckSearch()

        '検索条件に無い場合は、エラー
        If rboSales03.Checked = True Then
            MessageBox.Show("年月検索条件が選択されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Return False
        End If

        '問題ない場合はTRUEを返す
        Return True


    End Function

    '------------------------------------------------
    '--売上確認 検索メイン処理       　    ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " STUFF(LEFT(CONVERT(VARCHAR, A.売上日, 112), 6),5,0,'-') AS 売上年月, "
            'SELECT受注先NO指定
            If chkSelectOrder01.Checked = True Then
                strSQL &= " A.受注先NO, "
                strSQL &= " B.受注先名, "
            End If
            'SELECT出荷先NO指定
            If chkSelectShipment01.Checked = True Then
                strSQL &= " A.出荷先NO, "
                strSQL &= " C.出荷先名, "
            End If
            strSQL &= " SUM(A.売上数) AS 売上数 "
            strSQL &= "FROM "
            strSQL &= " SALES_TBL A, "
            strSQL &= " ORDER_MS B, "
            strSQL &= " SHIPMENT_MS C "
            strSQL &= "WHERE "
            strSQL &= "     A.受注先NO = B.受注先NO  "
            strSQL &= " AND A.出荷先NO = C.出荷先NO  "
            'WHERE売上年月指定
            If rboSales01.Checked = True Then
                strSQL &= " AND LEFT(売上日,7) = REPLACE('" & dtpSales01.Text.Trim & "','/','-')  "
            End If
            'WHERE受注先NO指定
            If chkWhereOrder01.Checked = True Then
                strSQL &= " AND A.受注先NO = '" & txtOrderMSNo.Text.Trim & "'  "
            End If
            'WHERE出荷先NO指定
            If chkWhereShipment01.Checked Then
                strSQL &= " AND A.出荷先NO = '" & txtShipmentMSNo.Text.Trim & "'  "
            End If
            strSQL &= "GROUP BY "
            'GROUP BY受注先NO指定
            If chkSelectOrder01.Checked = True Then
                strSQL &= " A.受注先NO, "
                strSQL &= " B.受注先名, "
            End If
            'GROUP BY出荷先NO指定
            If chkSelectShipment01.Checked = True Then
                strSQL &= " A.出荷先NO, "
                strSQL &= " C.出荷先名, "
            End If
            strSQL &= " STUFF(LEFT(CONVERT(VARCHAR, A.売上日, 112), 6),5,0,'-') "

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
                        DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim()
                    Next
                Next

                'DataGridView1のすべての列の幅を自動調整する
                DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

                Return True

            Else
                MessageBox.Show("検索結果が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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
    '--受注先NO 検索処理             　    ----------
    '------------------------------------------------
    Public Sub sOrderMS_Search()
        '０埋め処理
        txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")
        'DB接続
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
                    lblOrderMSName.Text = dtReader("受注先名").ToString.Trim
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
    '--出荷先NO 検索処理             　    ----------
    '------------------------------------------------
    Public Sub sShipmentMS_Search()
        '０埋め処理
        txtShipmentMSNo.Text = txtShipmentMSNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBconnect()
        '検索メイン処理
        Call fShipmentMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--出荷先NO 検索メイン処理       　    ----------
    '------------------------------------------------
    Public Function fShipmentMS_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 出荷先NO, "
            strSQL &= " 出荷先名 "
            strSQL &= "FROM "
            strSQL &= " SHIPMENT_MS "
            strSQL &= "WHERE "
            strSQL &= " 出荷先NO = '" & txtShipmentMSNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    lblShipmentMSName.Text = dtReader("出荷先名").ToString.Trim
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

        'テキストボックス、ラベルの初期設定
        txtOrderMSNo.Clear()
        lblOrderMSName.Text = ""
        txtShipmentMSNo.Clear()
        lblShipmentMSName.Text = ""

        'ラジオボタンの初期設定
        rboSales03.Checked = True
        rboSales03.Visible = False

        'チェックボックスの設定
        chkWhereOrder01.Checked = False
        chkWhereShipment01.Checked = False

        '年月の初期設定
        dtpSales01.Format = DateTimePickerFormat.Custom
        dtpSales01.CustomFormat = "yyyy/MM"
        dtpSales01.Text = Date.Now

        'GroupBoxの初期設定
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        GroupBox5.Enabled = False

        'DataGridViewの初期設定
        DataGridView1.DataSource = ""
        DataGridView1.Columns.Clear()


    End Sub

    '------------------------------------------------
    '--クリア処理 DataGridViewのみ   　    ----------
    '------------------------------------------------
    Public Sub sClear_DataGridView()
        'DataGridViewの初期設定
        DataGridView1.DataSource = ""
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
