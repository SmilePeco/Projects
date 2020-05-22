Imports System.Data.SqlClient
Imports System.Xml

Public Class T010

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                      　    ----------
    '------------------------------------------------
    Private Sub T010_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sClear()
    End Sub

    '------------------------------------------------
    '--ファンクションキー処理        　    ----------
    '------------------------------------------------
    Private Sub T010_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

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
    '--検索ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '検索処理
        Call sSearch()
    End Sub

    '------------------------------------------------
    '--更新ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '更新処理
        Call sUpdate()
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
        Dim frm As New T010_2
        frm.ShowDialog(Me)
    End Sub

    '------------------------------------------------
    '--受注先NO Leave処理            　    ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_Leave(sender As Object, e As EventArgs) Handles txtOrderMSNo.Leave
        '検索処理
        Call fOrderMS_Search()

    End Sub

    '------------------------------------------------
    '--受注NO、受注日 チェック時処理 　    ----------
    '------------------------------------------------
    Private Sub rboSearch1_CheckedChanged(sender As Object, e As EventArgs) Handles rboSearch1.CheckedChanged
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
    End Sub

    '------------------------------------------------
    '--受注番号 チェック時処理       　    ----------
    '------------------------------------------------
    Private Sub rboSearch2_CheckedChanged(sender As Object, e As EventArgs) Handles rboSearch2.CheckedChanged
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
    End Sub

    '------------------------------------------------
    '--受注一覧　検索メイン処理            ----------
    '------------------------------------------------
    Public Sub sSearch()
        Me.ActiveControl = Nothing
        'DataGridViewの初期化
        Call sDataGridView_Clear()
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fMainSearch()

    End Sub

    '------------------------------------------------
    '--受注一覧　検索メイン処理            ----------
    '------------------------------------------------
    Public Function fMainSearch()

        Dim dtReader As SqlDataReader

        Try
            If rboSearch1.Checked = True Then
                '受注NO、受注日検索の場合
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 受注NO, "
                strSQL &= " 受注先NO, "
                strSQL &= " 作業工程NO, "
                strSQL &= " 受注数, "
                strSQL &= " 受注日, "
                strSQL &= " 最終更新者, "
                strSQL &= " 更新日, "
                strSQL &= " 登録日 "
                strSQL &= "FROM "
                strSQL &= " ORDER_TBL "
                'WHERE条件が異なる
                strSQL &= "WHERE "
                strSQL &= "    受注先NO='" & txtOrderMSNo.Text.Trim & "' "
                strSQL &= "AND 受注日 BETWEEN "
                strSQL &= "'" & dtpOrderDateFrom.Text & "' AND '" & dtpOrderDateTo.Text & "' "
                strSQL &= "ORDER BY "
                strSQL &= "受注NO "

            ElseIf rboSearch2.Checked = True Then
                '受注番号検索の場合
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 受注NO, "
                strSQL &= " 受注先NO, "
                strSQL &= " 作業工程NO, "
                strSQL &= " 受注数, "
                strSQL &= " 受注日, "
                strSQL &= " 最終更新者, "
                strSQL &= " 更新日, "
                strSQL &= " 登録日 "
                strSQL &= "FROM "
                strSQL &= " ORDER_TBL "
                'WHERE条件が異なる
                strSQL &= "WHERE "
                strSQL &= "    受注NO='" & txtOrderNo.Text.Trim & "' "
                strSQL &= "ORDER BY "
                strSQL &= "受注NO "


            Else
                '何も選択されていない場合
                MessageBox.Show("検索条件が未選択です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False

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



                '１行目にチェックボックスを追加
                Dim chkColumns As New DataGridViewCheckBoxColumn
                DataGridView1.Columns.Insert(0, chkColumns)
                'DBからチェックボックスの値を反映
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 受注チェックフラグ "
                strSQL &= "FROM "
                strSQL &= " ORDER_TBL "
                strSQL &= "WHERE "
                If rboSearch1.Checked = True Then
                    strSQL &= "    受注先NO='" & txtOrderMSNo.Text.Trim & "' "
                    strSQL &= "AND 受注日 BETWEEN "
                    strSQL &= "'" & dtpOrderDateFrom.Text & "' AND '" & dtpOrderDateTo.Text & "' "
                Else
                    strSQL &= "    受注NO='" & txtOrderNo.Text.Trim & "' "
                End If
                strSQL &= "ORDER BY "
                strSQL &= "受注NO "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                Dim i As Integer = 0
                'チェック情報の反映
                While dtReader.Read
                    DataGridView1.Item(0, i).Value = dtReader("受注チェックフラグ")
                    i += 1
                End While

                'トリム処理
                For i = 0 To DataGridView1.RowCount - 1
                    For y = 0 To 8
                        DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim
                    Next
                Next

                'ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                '最初の列以外は編集不可
                For i = 1 To 8
                    DataGridView1.Columns(i).ReadOnly = True
                Next


                Return True

            Else
                '検索結果が０件の場合
                MessageBox.Show("検索結果が一致しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    ''------------------------------------------------
    ''--受注一覧 検索前チェック処理         ----------
    ''------------------------------------------------
    'Public Function fCheckSearch() As Boolean

    '    Dim dtReader As SqlDataReader

    '    Try
    '        '受注先NOが存在するかチェック
    '        strSQL = ""
    '        strSQL &= "SELECT "
    '        strSQL &= " 受注先NO "
    '        strSQL &= "FROM "
    '        strSQL &= " ORDER_MS "
    '        strSQL &= "WHERE "
    '        strSQL &= " 受注先NO = '" & txtOrderMSNo.Text.Trim & "' "

    '        cd.CommandText = strSQL
    '        cd.Connection = Cn
    '        dtReader = cd.ExecuteReader

    '        If dtReader.HasRows Then
    '            '受注番号が存在するかチェック
    '            dtReader.Close()

    '            strSQL = ""
    '            strSQL &= "SELECT "
    '            strSQL &= " 受注NO "
    '            strSQL &= "FROM "
    '            strSQL &= " ORDER_TBL "
    '            strSQL &= "WHERE "
    '            strSQL &= " 受注NO = '" & txtOrderNo.Text.Trim & "' "

    '            cd.CommandText = strSQL
    '            cd.Connection = Cn
    '            dtReader = cd.ExecuteReader

    '            If dtReader.HasRows Then
    '                dtReader.Close()

    '                Return True

    '            Else
    '                MessageBox.Show("入力した受注NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                dtReader.Close()
    '                txtOrderNo.Focus()
    '                Return False

    '            End If


    '        Else
    '            MessageBox.Show("入力した受注先NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            dtReader.Close()
    '            txtOrderMSNo.Focus()
    '            Return False


    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "例外発生")
    '        Return False
    '    Finally
    '        Cn.Close()
    '        Cn.Dispose()
    '    End Try

    'End Function

    '------------------------------------------------
    '--受注一覧 更新処理 　　　            ----------
    '------------------------------------------------
    Public Sub sUpdate()
        Dim result As Boolean
        'DB接続
        Call sDBConnect()
        '更新メイン処理
        result = fMainUpdate()
        If result = True Then
            Call sDataGridView_Clear()
        End If

    End Sub

    '------------------------------------------------
    '--受注一覧 更新メイン処理             ----------
    '------------------------------------------------
    Public Function fMainUpdate() As Boolean

        '編集モードを解除
        DataGridView1.EndEdit()

        Try
            If DataGridView1.RowCount > 0 Then
                Dim result As DialogResult = MessageBox.Show("更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                If result = Windows.Forms.DialogResult.Yes Then
                    For i = 0 To DataGridView1.RowCount - 1
                        strSQL = ""
                        strSQL &= "UPDATE ORDER_TBL "
                        strSQL &= "SET "
                        strSQL &= "受注チェックフラグ = '" & DataGridView1.Item(0, i).Value & "', "
                        strSQL &= "更新日 = SYSDATETIME() "
                        strSQL &= "WHERE "
                        strSQL &= "受注NO = " & DataGridView1.Item(1, i).Value & " "

                        cd.CommandText = strSQL
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()

                    Next

                    MessageBox.Show("更新しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Return True
                Else
                    Return False
                End If

            Else
                '検索結果が０件の場合
                MessageBox.Show("検索結果が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--受注先 検索処理                     ----------
    '------------------------------------------------
    Public Sub fOrderMS_Search()
        '０埋め処理
        txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fOrderMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--受注先 検索メイン処理         　    ----------
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
                    txtOrderMSNo.Text = dtReader("受注先NO")
                    lblOrderMSName.Text = dtReader("受注先名")
                End While
                dtReader.Close()
                Return True
            Else
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

        rboSearch3.Checked = True
        rboSearch3.Visible = False

        txtOrderMSNo.Clear()
        lblOrderMSName.Text = ""
        txtOrderNo.Clear()

        dtpOrderDateFrom.Text = DateTime.Now
        dtpOrderDateTo.Text = DateTime.Now

        GroupBox2.Enabled = False
        GroupBox3.Enabled = False

        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub

    '------------------------------------------------
    '--DayaGridViewのみクリア処理    　    ----------
    '------------------------------------------------
    Public Sub sDataGridView_Clear()
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
    End Sub


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
