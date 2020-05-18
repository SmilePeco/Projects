Imports System.Xml
Imports System.Data.SqlClient

Public Class T012

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T012_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー処理              ----------
    '------------------------------------------------
    Private Sub T012_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode

            Case Keys.F1
                '検索処理
                Call sSearch()
            Case Keys.F2
                '削除処理
                Call sDelete()
            Case Keys.F3
                'クリア処理
                Call sClear()
            Case Keys.F4
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
    '--削除ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '削除処理
        Call sDelete()
    End Sub

    '------------------------------------------------
    '--クリアボタン押下処理                ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--終了ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--出荷先Leave処理                     ----------
    '------------------------------------------------
    Private Sub txtShipmentMSNo_Leave(sender As Object, e As EventArgs) Handles txtShipmentMSNo.Leave
        '検索処理
        Call sShipmentMS_Search()

    End Sub

    '------------------------------------------------
    '--出荷先ボタン押下処理                ----------
    '------------------------------------------------
    Private Sub btnShipmentSearch_Click(sender As Object, e As EventArgs) Handles btnShipmentSearch.Click
        Dim frm As New T012_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--生産予定ボタンチェック処理          ----------
    '------------------------------------------------
    Private Sub rboCreateDate01_CheckedChanged(sender As Object, e As EventArgs) Handles rboCreateDate01.CheckedChanged
        'ラベル名を「生産予定」に変更
        Label2.Text = "生産予定日"
    End Sub

    '------------------------------------------------
    '--生産完了ボタンチェック処理          ----------
    '------------------------------------------------
    Private Sub rboCreateDate02_CheckedChanged(sender As Object, e As EventArgs) Handles rboCreateDate02.CheckedChanged
        'ラベル名を「生産完了」に変更
        Label2.Text = "生産完了日"

    End Sub

    '------------------------------------------------
    '--グリッド検索処理                    ----------
    '------------------------------------------------
    Public Sub sSearch()
        Dim result As Boolean
        'グリッド初期化
        Call sDataGridView_Clear()
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        result = fMainSearch()
        If result = True Then

        Else
            Call sDataGridView_Clear()

        End If

    End Sub

    '------------------------------------------------
    '--グリッド検索メイン処理              ----------
    '------------------------------------------------
    Public Function fMainSearch() As Boolean

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= "  生産NO, "
            strSQL &= "  出荷先NO, "
            strSQL &= "  作業工程NO, "
            strSQL &= "  生産数, "
            strSQL &= "  生産完了フラグ, "
            strSQL &= "  最終更新者, "
            If rboCreateDate01.Checked = True Then
                strSQL &= "  生産日 AS 生産予定日 "
            ElseIf rboCreateDate02.Checked = True Then
                strSQL &= "  生産日 AS 生産予定日, "
                strSQL &= "  更新日 AS 生産完了日 "
            End If
            strSQL &= "FROM "
            strSQL &= " CREATE_TBL "
            strSQL &= "WHERE "
            strSQL &= "	出荷先NO = '" & txtShipmentMSNo.Text.Trim & "' "
            If rboCreateDate01.Checked = True Then
                '生産予定日チェックの場合、生産日をWHERE条件にする
                strSQL &= "AND 生産日 BETWEEN '" & dtpCreateDateFrom.Text.Trim & "' AND '" & dtpCreateDateTo.Text.Trim & "' "
            ElseIf rboCreateDate02.Checked = True Then
                '生産完了日チェックの場合、更新日をWHERE条件にする
                strSQL &= "AND 生産完了フラグ = 1 "
                strSQL &= "AND 更新日 BETWEEN '" & dtpCreateDateFrom.Text.Trim & " 00:00:00' AND '" & dtpCreateDateTo.Text.Trim & " 23:59:59' "
            End If
            strSQL &= "ORDER BY "
            strSQL &= " 生産NO "

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

                Dim chkColumn As New DataGridViewCheckBoxColumn
                DataGridView1.Columns.Insert(0, chkColumn)

                For i = 0 To DataGridView1.RowCount - 1
                    For y = 1 To 7
                        'トリム処理
                        DataGridView1.Item(y, i).Value = DataGridView1.Item(y, i).Value.ToString.Trim
                        DataGridView1.Columns(y).ReadOnly = True
                    Next
                Next


                'ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                'ヘッダータイトルを折り返さない
                DataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False

                Return True



            Else
                dtReader.Close()
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
    '--削除処理                            ----------
    '------------------------------------------------
    Public Sub sDelete()
        Dim result As Boolean
        '編集モードを解除
        DataGridView1.EndEdit()
        '削除前チェック処理
        result = fCheckDelete()
        If result = True Then
            'DB接続
            Call sDBConnect()
            '削除メイン処理
            result = fMainDelete()
            If result = True Then
                '削除完了後はDatagridView初期化
                Call sDataGridView_Clear()
            End If
        End If

    End Sub

    '------------------------------------------------
    '--削除前チェック処理                  ----------
    '------------------------------------------------
    Public Function fCheckDelete() As Boolean

        Dim intCount As Integer 'チェックの数をカウント

        Dim result As DialogResult = MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
        If result = Windows.Forms.DialogResult.Yes Then
            '1列目のチェック数をカウント
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Item(0, i).Value = True Then
                    intCount += 1
                End If
            Next

            'チェック数が１以上の場合は、TRUE
            If intCount > 0 Then
                Return True
            Else
                MessageBox.Show("削除したい件数が０件です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If


        Else
            Return False

        End If


    End Function

    '------------------------------------------------
    '--削除メイン処理                      ----------
    '------------------------------------------------
    Public Function fMainDelete() As Boolean

        Dim intCount As Integer '削除数のカウント
        Dim tran As SqlTransaction
        tran = Cn.BeginTransaction

        Try
            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Item(0, i).Value = True Then
                    strSQL = ""
                    strSQL &= "DELETE FROM CREATE_TBL "
                    strSQL &= "WHERE 生産NO = " & DataGridView1.Item(1, i).Value.ToString.Trim & " "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    cd.Transaction = tran
                    cd.ExecuteNonQuery()

                    intCount += 1

                End If
            Next

            tran.Commit()
            MessageBox.Show(intCount & "件削除しました。", "削除完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Return True

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
    '--出荷先マスタ検索処理                ----------
    '------------------------------------------------
    Public Sub sShipmentMS_Search()
        '０埋め
        txtShipmentMSNo.Text = txtShipmentMSNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBConnect()
        'メイン検索処理
        Call fShipmentMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--出荷先マスタメイン検索処理          ----------
    '------------------------------------------------
    Public Function fShipmentMS_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= "  出荷先NO, "
            strSQL &= "  出荷先名 "
            strSQL &= "FROM "
            strSQL &= "  SHIPMENT_MS "
            strSQL &= "WHERE "
            strSQL &= " 出荷先NO = '" & txtShipmentMSNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    lblShipmentMSName.Text = dtReader("出荷先名")
                    lblShipmentMSName.Text = lblShipmentMSName.Text.Trim()
                End While

                dtReader.Close()
                Return True

            Else
                dtReader.Close()
                lblShipmentMSName.Text = ""
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
    '--クリア処理                          ----------
    '------------------------------------------------
    Public Sub sClear()

        txtShipmentMSNo.Clear()
        lblShipmentMSName.Text = ""
        rboCreateDate01.Checked = True
        dtpCreateDateFrom.Text = Date.Now
        dtpCreateDateTo.Text = Date.Now
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()

    End Sub

    '------------------------------------------------
    '--クリア処理 DataGridViewのみ         ----------
    '------------------------------------------------
    Public Sub sDataGridView_Clear()
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()
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
