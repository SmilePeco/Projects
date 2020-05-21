Imports System.Xml
Imports System.Data.SqlClient

Public Class T009

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load検索   　                       ----------
    '------------------------------------------------
    Private Sub T009_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '--ファンクションキー検索              ----------
    '------------------------------------------------
    Private Sub T009_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                '登録処理
                Call fUpdate()
            Case Keys.F2
                'クリア処理
                Call sClear()
            Case Keys.F3
                '終了処理
                Me.Close()

        End Select
    End Sub

    '------------------------------------------------
    '--登録ボタン押下処理                   ----------
    '------------------------------------------------
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        '登録処理
        Call fUpdate()
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
    '--出荷先NO KeyPress検索               ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderMSNo.KeyPress

        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then

            e.Handled = True

        End If

    End Sub

    '------------------------------------------------
    '--出荷先NO Leave検索                  ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_Leave(sender As Object, e As EventArgs) Handles txtOrderMSNo.Leave
        '検索処理
        Call sOrderMSNO_Search()
    End Sub

    '------------------------------------------------
    '--作業工程 KeyPress検索               ----------
    '------------------------------------------------
    Private Sub txtWorkProcessNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWorkProcessNO.KeyPress

        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then

            e.Handled = True

        End If

    End Sub

    '------------------------------------------------
    '--作業工程 Leave検索                  ----------
    '------------------------------------------------
    Private Sub txtWorkProcessNO_Leave(sender As Object, e As EventArgs) Handles txtWorkProcessNO.Leave
        '検索処理
        Call sWorkProcess_Search()
    End Sub

    '------------------------------------------------
    '--出荷先NOボタン押下処理              ----------
    '------------------------------------------------
    Private Sub btnOrderSearch_Click(sender As Object, e As EventArgs) Handles btnOrderSearch.Click
        Dim frm As New T009_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--作業工程ボタン押下処理              ----------
    '------------------------------------------------
    Private Sub cmdWorkProcessSearch_Click(sender As Object, e As EventArgs) Handles cmdWorkProcessSearch.Click
        Dim frm As New T009_3
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--更新担当者ボタン押下処理            ----------
    '------------------------------------------------
    Private Sub btnUserIDSearch_Click(sender As Object, e As EventArgs) Handles btnUserIDSearch.Click
        Dim frm As New T009_4
        frm.ShowDialog(Me)

    End Sub



    '------------------------------------------------
    '--出荷先検索 　                       ----------
    '------------------------------------------------
    Public Sub sOrderMSNO_Search()
        '０埋め処理
        txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBConnect()
        'メイン検索
        Call fOrderMSNO_MainSearch()

    End Sub

    '------------------------------------------------
    '--出荷先メイン検索                    ----------
    '------------------------------------------------
    Public Function fOrderMSNO_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            '検索用SQL
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 出荷先NO, "
            strSQL &= " 出荷先名 "
            strSQL &= "FROM "
            strSQL &= " SHIPMENT_MS "
            strSQL &= "WHERE "
            strSQL &= " 出荷先NO = '" & txtOrderMSNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            '一致した場合は受注先名を取得
            If dtReader.HasRows Then
                While dtReader.Read
                    lblOrderMSName.Text = CStr(dtReader("出荷先名")).Trim
                End While
                dtReader.Close()
                Return True

            Else
                dtReader.Close()
                '一致しなかった場合は出荷先名を空白
                lblOrderMSName.Text = ""
                'MessageBox.Show("一致する受注先NOが存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--作業工程NO 検索処理                 ----------
    '------------------------------------------------
    Public Sub sWorkProcess_Search()
        '０埋め処理
        txtWorkProcessNO.Text = txtWorkProcessNO.Text.PadLeft(3, "0")
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fWorkProcess_MainSearch()

    End Sub

    '------------------------------------------------
    '--作業工程NO 検索メイン処理           ----------
    '------------------------------------------------
    Public Function fWorkProcess_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " A.作業工程NO, "
            strSQL &= " A.作業工程名, "
            strSQL &= " B.製品NO, "
            strSQL &= " B.製品名 "
            strSQL &= "FROM "
            strSQL &= " WORKPROCESS_MS A, "
            strSQL &= " ITEM_MS B "
            strSQL &= "WHERE "
            strSQL &= "    A.製品NO = B.製品NO "
            strSQL &= "AND 作業工程NO = '" + txtWorkProcessNO.Text.Trim + "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader



            If dtReader.HasRows Then
                While dtReader.Read
                    txtWorkProcessNO.Text = CStr(dtReader("作業工程NO")).Trim
                    lblWorkProcessName.Text = CStr(dtReader("作業工程名")).Trim
                    lblItemNO.Text = CStr(dtReader("製品NO")).Trim()
                    lblItemName.Text = CStr(dtReader("製品名")).Trim()
                End While

                dtReader.Close()
                Return True
            Else
                dtReader.Close()
                lblWorkProcessName.Text = ""
                lblItemNO.Text = ""
                lblItemName.Text = ""
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
    '--登録処理                            ----------
    '------------------------------------------------
    Public Sub fUpdate()
        Dim result As Boolean
        'DB接続
        Call sDBConnect()
        '登録前チェック１（入力値のチェック）
        result = fCheckUpdate()
        If result = True Then
            '登録前チェック２（臨時在庫のチェック）
            result = fCheckUpdate2()

            If result = True Then
                'DB接続
                Call sDBConnect()
                '登録処理
                result = fMainUpdate()

                If result = True Then
                    Call sClear()

                End If

            End If

        End If

    End Sub

    '------------------------------------------------
    '--登録前チェック処理                  ----------
    '------------------------------------------------
    Public Function fCheckUpdate() As Boolean

        Dim dtReader As SqlDataReader

        Try
            Dim result As DialogResult = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If result = Windows.Forms.DialogResult.Yes Then
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 出荷先NO "
                strSQL &= "FROM "
                strSQL &= " SHIPMENT_MS "
                '臨時在庫オンの場合はWHERE条件なし
                If chkClosedStock.Checked = False Then
                    strSQL &= "WHERE "
                    strSQL &= " 出荷先NO = '" & txtOrderMSNo.Text.Trim & "' "
                End If

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    dtReader.Close()
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " 作業工程NO "
                    strSQL &= "FROM "
                    strSQL &= " WORKPROCESS_MS "
                    '臨時在庫オンの場合はWHERE条件なし
                    If chkClosedStock.Checked = False Then
                        strSQL &= "WHERE "
                        strSQL &= " 作業工程NO = '" & txtWorkProcessNO.Text.Trim & "' "
                    End If

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    dtReader = cd.ExecuteReader

                    If dtReader.HasRows Then
                        dtReader.Close()
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
                            If txtOrderAmount.Text.Trim <> "" Then
                                Return True
                            Else
                                MessageBox.Show("生産数が空白です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return False
                            End If

                        Else
                            dtReader.Close()
                            MessageBox.Show("入力したログインIDが存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return False
                        End If


                    Else
                        dtReader.Close()
                        MessageBox.Show("入力した作業工程NOが存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If

                Else
                    dtReader.Close()
                    MessageBox.Show("入力した出荷先NOが存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False

                End If


            Else
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
    '--登録前チェック処理２                ----------
    '------------------------------------------------
    Public Function fCheckUpdate2() As Boolean
        '出荷先、作業工程のどちらかがブランクだった場合は注意メッセージを促す

        Dim result As DialogResult

        '臨時在庫登録の確認
        If chkClosedStock.Checked = True Then
            result = MessageBox.Show("臨時在庫として登録した場合、" & vbCrLf & "入力した出荷先NO、作業工程NOは反映されません。" & vbCrLf & "臨時在庫として登録しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = Windows.Forms.DialogResult.No Then
                Return False
            End If
        End If

        '空白以外、もしくは「はい」を選択した場合はTRUEを返す
        Return True


    End Function

    '------------------------------------------------
    '--登録メイン処理                      ----------
    '------------------------------------------------
    Public Function fMainUpdate() As Boolean

        Dim dtReader As SqlDataReader
        Dim intCreateCount As Integer '生産NOの連番

        Try
            intCreateCount = 0

            '生産NOのMAX値の取得
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= "  COALESCE(MAX(生産NO), 0) AS 生産NO "
            strSQL &= "FROM "
            strSQL &= " CREATE_TBL "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            '生産NOは、MAX値に＋１をして連番とする
            If dtReader.HasRows Then
                While dtReader.Read
                    intCreateCount = dtReader("生産NO")
                End While

                dtReader.Close()
                intCreateCount += 1

                '登録SQL作成
                strSQL = ""
                strSQL &= "INSERT INTO CREATE_TBL VALUES "
                strSQL &= "( "
                strSQL &= " " & intCreateCount & ", "
                '出荷先NOが空白の場合は、臨時在庫とする
                If chkClosedStock.Checked = False Then
                    strSQL &= " '" & txtOrderMSNo.Text.Trim & "', "
                Else
                    strSQL &= " 'RNZ', "
                End If
                '作業工程NOが空白の場合は、臨時在庫とする
                If chkClosedStock.Checked = False Then
                    strSQL &= " '" & txtWorkProcessNO.Text.Trim & "', "
                Else
                    strSQL &= " 'RNZ', "

                End If
                strSQL &= " '" & txtOrderAmount.Text.Trim & "', "
                strSQL &= " '" & dtpCreateTime.Text & "', "
                strSQL &= " 'FALSE', "
                strSQL &= " '" & txtUserID.Text.Trim & "', "
                strSQL &= " SYSDATETIME(), "
                strSQL &= " SYSDATETIME() "
                strSQL &= ") "

                '登録処理
                cd.CommandText = strSQL
                cd.Connection = Cn
                cd.ExecuteNonQuery()

                MessageBox.Show("登録完了しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Return True

            Else
                'ありえないが、結果はFALSEとする
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
    '--クリア処理                        ----------
    '------------------------------------------------
    Public Sub sClear()
        txtOrderMSNo.Clear()
        lblOrderMSName.Text = ""
        txtWorkProcessNO.Clear()
        lblWorkProcessName.Text = ""
        txtUserID.Clear()
        txtOrderAmount.Clear()
        lblItemNO.Text = ""
        lblItemName.Text = ""
        dtpCreateTime.Text = Date.Now
        chkClosedStock.Checked = False

        '臨時在庫はオフ
        'chkClosedStock.Visible = False

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
