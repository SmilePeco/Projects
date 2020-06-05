Imports System.Xml
Imports System.Data.SqlClient

Public Class T019

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '------------------------------------------------
    '--Load処理                      　    ----------
    '------------------------------------------------
    Private Sub T019_Load(sender As Object, e As EventArgs) Handles Me.Load
        'クリア処理
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー処理        　    ----------
    '------------------------------------------------
    Private Sub T019_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                '検索処理
                Call sItemMS_Search()
            Case Keys.F2
                '更新処理
                Call sItemMS_Update()
            Case Keys.F3
                '削除処理
                Call sItemMS_Delete()
            Case Keys.F4
                'クリア処理
                Call sClear()
            Case Keys.F5
                '終了処理
                Me.Close()

        End Select

    End Sub

    '------------------------------------------------
    '--検索ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '検索処理
        Call sItemMS_Search()
    End Sub

    '------------------------------------------------
    '--更新ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '更新処理
        Call sItemMS_Update()
    End Sub

    '------------------------------------------------
    '--削除ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '削除処理
        Call sItemMS_Delete()
    End Sub

    '------------------------------------------------
    '--クリアボタン押下処理          　    ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'クリア処理
        Call sClear()
    End Sub

    '------------------------------------------------
    '-終了ボタン押下処理             　    ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()
    End Sub

    '------------------------------------------------
    '--製品マスタ ボタン押下処理     　    ----------
    '------------------------------------------------
    Private Sub btnItemSearch_Click(sender As Object, e As EventArgs) Handles btnItemSearch.Click
        Dim frm As New T019_2
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--社員マスタ ボタン押下処理     　    ----------
    '------------------------------------------------
    Private Sub btnUpdateUserIDSearch_Click(sender As Object, e As EventArgs) Handles btnUpdateUserIDSearch.Click
        Dim frm As New T019_3
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--製品マスタ エンターキー押下処理     ----------
    '------------------------------------------------
    Private Sub txtItemNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemNo.KeyPress
        'エンターキー押下
        If e.KeyChar = Chr(13) Then
            Call sItemMS_Search()
        End If

    End Sub

    '------------------------------------------------
    '--製品NO KeyPress処理                 ----------
    '------------------------------------------------
    Private Sub txtUpdateItemNO_KeyPressd(sender As Object, e As KeyPressEventArgs) Handles txtUpdateItemNO.KeyPress, txtItemNo.KeyPress
        '0～9と、バックスペース以外の時は、イベントをキャンセルする
        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    '------------------------------------------------
    '--金額 KeyPress処理                   ----------
    '------------------------------------------------
    Private Sub txtUpdateMoney_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUpdateMoney.KeyPress
        '0～9と、バックスペース以外の時は、イベントをキャンセルする
        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    '------------------------------------------------
    '--製品マスタ 検索処理           　    ----------
    '------------------------------------------------
    Public Sub sItemMS_Search()
        '０埋め処理
        If txtItemNo.Text.Trim <> "" Then
            txtItemNo.Text = txtItemNo.Text.PadLeft(3, "0")
        End If
        'DB接続
        Call sDBconnect()
        '検索メイン処理
        Call fItemMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--製品マスタ 検索メイン処理     　    ----------
    '------------------------------------------------
    Public Function fItemMS_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            If txtItemNo.Text.Trim <> "" Then
                '空白以外のときはマスタから検索する
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " A.製品NO, "
                strSQL &= " A.製品名, "
                strSQL &= " B.金額 "
                strSQL &= "FROM "
                strSQL &= " ITEM_MS A, "
                strSQL &= " (SELECT "
                strSQL &= "   金額, "
                strSQL &= "   更新日 "
                strSQL &= "  FROM "
                strSQL &= "   ITEM_HISTORY_MS "
                strSQL &= "  WHERE "
                strSQL &= "   更新日 = (SELECT MAX(更新日) FROM ITEM_HISTORY_MS WHERE 製品NO = '" & txtItemNo.Text.Trim & "' ) ) B "
                strSQL &= "WHERE "
                strSQL &= "    製品NO = '" & txtItemNo.Text.Trim & "' "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    '//画面の設定
                    'GroupBoxをオン
                    GroupBox3.Enabled = True
                    lblMode.Text = "更新"
                    '更新の場合は、製品NOを入力不可
                    txtUpdateItemNO.Enabled = False
                    '値の代入
                    While dtReader.Read
                        txtUpdateItemNO.Text = dtReader("製品NO").ToString.Trim
                        txtUpdateItemName.Text = dtReader("製品名").ToString.Trim
                        txtUpdateMoney.Text = dtReader("金額").ToString.Trim
                    End While
                    dtReader.Close()

                    'DataGridViewの表示
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " 金額, "
                    strSQL &= " 最終更新者 AS 更新者, "
                    strSQL &= " 更新日 "
                    strSQL &= "FROM "
                    strSQL &= " ITEM_HISTORY_MS "
                    strSQL &= "WHERE "
                    strSQL &= " 製品NO = '" & txtItemNo.Text.Trim & "' "
                    strSQL &= "ORDER BY "
                    strSQL &= " 更新日 DESC "

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
                        Next
                    Next

                    'ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                    Return True

                Else
                    Dim result As DialogResult = MessageBox.Show("入力した製品NOが存在しません。" & vbCrLf & "新規作成しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                    If result = Windows.Forms.DialogResult.Yes Then
                        '//「はい」を選んだ場合
                        '「新規作成」モード
                        '画面の初期化
                        Call sClear_CreateNew()
                        'GrouopBox等の設定
                        GroupBox3.Enabled = True
                        txtUpdateItemNO.Enabled = True
                        '製品NOは入力不可
                        txtUpdateItemNO.Text = txtItemNo.Text
                        txtUpdateItemNO.Enabled = False
                        lblMode.Text = "新規作成"

                        Return True
                    Else
                        '//「いいえ」を選んだ場合
                        dtReader.Close()

                        '//画面の初期化
                        Call sClear_CreateNew()
                        Return False
                    End If


                End If
            Else
                '//空白のときは「新規作成」モード
                '画面の初期化
                Call sClear_CreateNew()
                'GrouopBox等の設定
                GroupBox3.Enabled = True
                txtUpdateItemNO.Enabled = True
                '//製品NOが空欄の場合は、連番で設定する
                '製品NOの連番を取得
                Call fItemMS_GetSerialnumber()
                '製品NOは入力不可
                txtUpdateItemNO.Enabled = False
                lblMode.Text = "新規作成"

                Return True

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
    '--製品マスタ_更新処理           　    ----------
    '------------------------------------------------
    Public Sub sItemMS_Update()

        Dim blnResult As Boolean

        blnResult = fItemMS_CheckUpdate()
        If blnResult = True Then
            'DB接続
            Call sDBconnect()
            '検索メイン処理
            blnResult = fItemMS_MainUpdate()
            If blnResult = True Then
                Call sClear()
            End If
        End If



    End Sub

    '------------------------------------------------
    '--製品マスタ_更新チェック処理    　    ----------
    '------------------------------------------------
    Public Function fItemMS_CheckUpdate() As Boolean

        Dim blnResult As Boolean

        Dim result As DialogResult = MessageBox.Show("更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
        If result = Windows.Forms.DialogResult.Yes Then

            If lblMode.Text = "新規作成" Then
                '//「新規作成」モード時のチェック処理
                '製品NOのチェック
                If txtUpdateItemNO.Text.Trim = "" Then
                    '新規作成モードの場合、未入力はエラー
                    MessageBox.Show("製品NOが未入力です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtUpdateItemNO.Focus()
                    Return False
                Else
                    '入力した製品NOをチェック。既に存在した場合エラー
                    Call sDBconnect()
                    blnResult = fItemMS_CheckNull(1, "新規作成")
                    If blnResult = False Then
                        Return False
                    End If
                End If

                '製品名のチェック
                If txtUpdateItemName.Text.Trim = "" Then
                    '新規作成モードの場合、未入力はエラー
                    MessageBox.Show("製品名が未入力です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtUpdateItemName.Focus()
                    Return False
                End If

                '金額のチェック
                If txtUpdateMoney.Text.Trim = "" Then
                    '新規作成モードの場合、未入力はエラー
                    MessageBox.Show("金額が未入力です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtUpdateMoney.Focus()
                    Return False
                End If

                '更新担当者のチェック
                If txtUpdateUserID.Text.Trim = "" Then
                    '新規作成モードの場合、未入力はエラー
                    MessageBox.Show("更新担当者が未入力です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtUpdateUserID.Focus()
                    Return False
                Else
                    '入力した更新更新をチェック。存在しない場合エラー
                    Call sDBconnect()
                    blnResult = fItemMS_CheckNull(2, "新規作成")
                    If blnResult = False Then
                        Return False
                    End If

                End If

                '問題ない場合はTRUEを返す
                Return True

            ElseIf lblMode.Text = "更新" Then
                '//「更新」モード時のチェック処理
                '製品NOは入力不可のため、チェック不要
                '製品名もチェック不要

                '金額が入力済みで、更新担当者が入力されていない場合はエラー
                If txtUpdateMoney.Text.Trim <> "" Then
                    If txtUpdateUserID.Text.Trim = "" Then
                        MessageBox.Show("金額を入力した場合は、更新担当者も入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                End If

                '更新担当者のチェック
                'ただし、金額が空欄の場合はチェックしなくてよい
                If txtUpdateMoney.Text.Trim <> "" Then
                    If txtUpdateUserID.Text.Trim = "" Then
                        '新規作成モードの場合、未入力はエラー
                        MessageBox.Show("更新担当者が未入力です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    Else
                        '入力した更新更新をチェック。存在しない場合エラー
                        Call sDBconnect()
                        blnResult = fItemMS_CheckNull(2, "更新")
                        If blnResult = False Then
                            Return False
                        End If

                    End If


                End If

                '問題ない場合はTRUEを返す
                Return True


            Else
                'ありないが、FALSEを返す
                Return False
            End If

        Else
            Return False
        End If

    End Function

    '------------------------------------------------
    '--製品マスタ_更新処理           　    ----------
    '------------------------------------------------
    Public Function fItemMS_MainUpdate()

        Dim dtReader As SqlDataReader
        Dim intCount As Integer '製品NO連番取得用
        Dim strCount As String = ""   '製品NO連番設定用

        Dim tran As SqlTransaction
        tran = Cn.BeginTransaction

        Try
            If lblMode.Text = "新規作成" Then
                '//新規作成の場合
                '//ITEM_MSのINSERT
                If txtItemNo.Text.Trim = "" Then
                    '製品NOの連番を取得
                    strCount = txtUpdateItemNO.Text.Trim

                    'strSQL = ""
                    'strSQL &= "SELECT "
                    'strSQL &= " MAX(製品NO) AS 製品NO "
                    'strSQL &= "FROM "
                    'strSQL &= " ITEM_MS "
                    ''SQL実行
                    'cd.CommandText = strSQL
                    'cd.Transaction = tran
                    'cd.Connection = Cn
                    'dtReader = cd.ExecuteReader
                    ''連番設定
                    'If dtReader.HasRows Then
                    '    While dtReader.Read
                    '        '取得した値を代入
                    '        intCount = dtReader("製品NO")
                    '        '＋１する
                    '        intCount += 1
                    '    End While
                    'Else
                    '    '０件の場合は１を代入
                    '    intCount = 1
                    'End If
                    'dtReader.Close()
                    ''０埋め処理
                    'strCount = intCount.ToString.PadLeft(3, "0")

                Else
                    '//製品NOが空欄以外の場合は、画面上の製品NOを取得
                    strCount = txtItemNo.Text.Trim
                End If

                'INSERT処理
                strSQL = ""
                strSQL &= "INSERT INTO ITEM_MS VALUES "
                strSQL &= "( "
                strSQL &= " '" & strCount & "', " '//製品NO
                strSQL &= " '" & txtUpdateItemName.Text.Trim & "', " '//製品名
                strSQL &= " '', " '//部品1
                strSQL &= " '', " '//部品2
                strSQL &= " '', " '//部品3
                strSQL &= " SYSDATETIME(), " '//更新日
                strSQL &= " SYSDATETIME()  " '//登録日
                strSQL &= ") "
                'SQL実行
                cd.CommandText = strSQL
                cd.Transaction = tran
                cd.Connection = Cn
                cd.ExecuteNonQuery()

                '//ITEM_HISTORY_MSのINSERT
                strSQL = ""
                strSQL &= "INSERT INTO ITEM_HISTORY_MS VALUES "
                strSQL &= "( "
                strSQL &= " '" & strCount & "', "
                strSQL &= " SYSDATETIME(), "
                strSQL &= " " & txtUpdateMoney.Text.Trim & ", "
                strSQL &= " '" & txtUpdateUserID.Text.Trim & "' "
                strSQL &= ") "
                'SQL実行
                cd.CommandText = strSQL
                cd.Transaction = tran
                cd.Connection = Cn
                cd.ExecuteNonQuery()

            ElseIf lblMode.Text = "更新" Then
                '//更新の場合
                '//製品名が検索結果から変更なければ更新対象外
                'まずはDBの製品名を取得
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " A.製品名, "
                strSQL &= " B.金額, "
                strSQL &= " B.更新日 "
                strSQL &= "FROM "
                strSQL &= " ITEM_MS A, "
                strSQL &= " ITEM_HISTORY_MS B "
                strSQL &= "WHERE "
                strSQL &= "    A.製品NO = B.製品NO "
                'strSQL &= "AND A.製品NO = '" & txtUpdateItemNO.Text.Trim & "' "
                strSQL &= "AND B.更新日 = (SELECT MAX(更新日) FROM ITEM_HISTORY_MS WHERE 製品NO = '" & txtUpdateItemNO.Text.Trim & "') "
                'SQL実行
                cd.CommandText = strSQL
                cd.Transaction = tran
                cd.Connection = Cn
                dtReader = cd.ExecuteReader
                If dtReader.HasRows Then
                    While dtReader.Read
                        strCount = dtReader("製品名").ToString.Trim
                        intCount = dtReader("金額")
                    End While
                    dtReader.Close()
                Else
                    'ありえないが、FALSEを返す
                    dtReader.Close()
                    tran.Rollback()
                    Return False
                End If
                '//ITEM_MSのUPDATE
                '//製品名が検索結果から変更なければ変更対象外とする
                '//空欄の場合も対象外とする。
                If txtUpdateItemName.Text.Trim <> "" Then
                    If txtUpdateItemName.Text <> strCount Then
                        '//更新処理
                        strSQL = ""
                        strSQL &= "UPDATE ITEM_MS "
                        strSQL &= "SET 製品名 = '" & txtUpdateItemName.Text.Trim & "', "
                        strSQL &= "    更新日 = SYSDATETIME() "
                        strSQL &= "WHERE 製品NO = '" & txtUpdateItemNO.Text.Trim & "' "
                        'SQL実行
                        cd.CommandText = strSQL
                        cd.Transaction = tran
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()

                    Else
                        '変更がない場合は、更新しない
                        MessageBox.Show("製品名は変更がないため、更新対象外となりました。", "更新結果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    End If

                Else
                    '空白の場合も更新対象外と判定する
                    MessageBox.Show("製品名欄が空白のため、更新対象外となりました。", "更新結果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

                End If

                '//ITEM_HISTORY_MSのUPDATE
                '//金額が検索結果から変更なければ変更対象外とする
                '//空欄の場合も対象外とする。
                If txtUpdateMoney.Text.Trim <> "" Then
                    If txtUpdateMoney.Text.Trim <> intCount Then
                        '//更新処理
                        strSQL = ""
                        strSQL &= "INSERT INTO ITEM_HISTORY_MS VALUES "
                        strSQL &= "( "
                        strSQL &= " '" & txtUpdateItemNO.Text.Trim & "', "
                        strSQL &= " SYSDATETIME(), "
                        strSQL &= " " & txtUpdateMoney.Text.Trim & ", "
                        strSQL &= " '" & txtUpdateUserID.Text.Trim & "' "
                        strSQL &= ") "
                        'SQL実行
                        cd.CommandText = strSQL
                        cd.Transaction = tran
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()

                    Else
                        '変更がない場合は更新しない
                        MessageBox.Show("金額に変更が無いため、更新対象外となりました。", "更新結果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

                    End If

                Else
                    '空白の場合も更新対象外と判定する
                    MessageBox.Show("金額欄が空白のため、更新対象外となりました。", "更新結果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                End If



            Else
                'ありえないが、FALSEを返す
                tran.Rollback()
                Return False


            End If

            '//更新処理が完了
            MessageBox.Show("更新処理が完了しました。", "更新終了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            tran.Commit()
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
    '--製品マスタ 入力チェック処理         ----------
    '--【intCheck：チェックする項目】      ----------
    '--1 = 製品NOのチェック                ----------
    '--2 = 更新担当者のチェック            ----------
    '--【strCheck：モード】                ----------
    '--新規作成 = 新規作成モード           ----------
    '--更新 = 更新モード                   ----------
    '------------------------------------------------   
    Public Function fItemMS_CheckNull(intCheck As Integer, strMode As String) As Boolean

        Dim dtReader As SqlDataReader

        Try
            If intCheck = 1 Then
                'トリム後、０埋め処理
                txtUpdateItemNO.Text = txtUpdateItemNO.Text.Trim
                txtUpdateItemNO.Text = txtUpdateItemNO.Text.PadLeft(3, "0")

                '//製品NOのチェック
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 製品NO, "
                strSQL &= " 製品名 "
                strSQL &= "FROM "
                strSQL &= " ITEM_MS "
                strSQL &= "WHERE "
                If strMode = "新規作成" Then
                    '新規作成の場合は、更新入力欄の製品NOを参照
                    strSQL &= " 製品NO = '" & txtUpdateItemNO.Text.Trim & "' "
                ElseIf strMode = "更新" Then
                    '更新の場合は、検索入力欄の製品NOを参照
                    strSQL &= " 製品NO = '" & txtItemNo.Text.Trim & "' "
                End If

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If strMode = "新規作成" Then
                    '「新規作成」の場合、存在した場合はエラー
                    If dtReader.HasRows Then
                        MessageBox.Show("入力した製品NOは既に存在しています。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        dtReader.Close()
                        txtUpdateItemNO.Focus()
                        Return False
                    Else
                        dtReader.Close()
                        Return True
                    End If
                ElseIf strMode = "更新" Then
                    '//処理なし
                    '「更新」処理では使用していないが、削除処理で使用
                    If dtReader.HasRows Then
                        dtReader.Close()
                        Return True
                    Else
                        MessageBox.Show("入力した製品NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        dtReader.Close()
                        txtUpdateItemNO.Focus()
                        Return False
                    End If

                Else
                    'ありえないが、FALSEを返す
                    dtReader.Close()
                    Return False

                End If

            ElseIf intCheck = 2 Then
                '//更新担当者のチェック
                '「新規作成」、「更新」でも処理内容は変わらない
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 名前 "
                strSQL &= "FROM "
                strSQL &= " HUMAN_MS "
                strSQL &= "WHERE "
                strSQL &= " 名前 = '" & txtUpdateUserID.Text.Trim & "' "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    dtReader.Close()
                    Return True

                Else
                    MessageBox.Show("入力した更新担当者は存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    dtReader.Close()
                    txtUpdateUserID.Focus()
                    Return False

                End If

            Else
                'ありえないが、FALSEを返す
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
    '--製品マスタ 削除チェック処理   　    ----------
    '------------------------------------------------
    Public Sub sItemMS_Delete()
        Dim blnResult As Boolean
        '０埋め処理
        txtItemNo.Text = txtItemNo.Text.PadLeft(3, "0")
        blnResult = fItemMS_ChechkDelete()
        If blnResult = True Then
            'DB接続
            Call sDBconnect()
            '削除処理
            blnResult = fItemMS_MainDelete()
            If blnResult = True Then
                '初期化
                Call sClear()
            End If
        End If

    End Sub

    '------------------------------------------------
    '--製品マスタ 削除チェック処理   　    ----------
    '------------------------------------------------
    Public Function fItemMS_ChechkDelete() As Boolean

        Dim blnResult As Boolean

        Dim result As DialogResult = MessageBox.Show("金額の履歴も全て削除されます。" & vbCrLf & "本当に削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
        If result = Windows.Forms.DialogResult.Yes Then
            '削除モードがチェック入っていない場合はエラー
            If chkDeleteMode.Checked = False Then
                MessageBox.Show("削除モードにチェックが入っていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            '製品NOが空欄の場合はエラー
            If txtItemNo.Text.Trim = "" Then
                MessageBox.Show("製品NOが未記入です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Return False
            End If

            '製品NOが存在しない場合はエラー
            'DB接続
            Call sDBconnect()
            '存在確認
            blnResult = fItemMS_CheckNull(1, "更新")
            If blnResult = False Then
                Return False
            End If

            '問題なければTRUEを返す
            Return True

        Else
            Return False
        End If

    End Function

    '------------------------------------------------
    '--製品マスタ 削除メイン処理     　    ----------
    '------------------------------------------------
    Public Function fItemMS_MainDelete() As Boolean

        Dim tran As SqlTransaction
        tran = Cn.BeginTransaction

        Try
            '//ITEM_MSのDELETE
            strSQL = ""
            strSQL &= "DELETE FROM ITEM_MS "
            strSQL &= "WHERE 製品NO = '" & txtItemNo.Text.Trim & "' "
            'SQL実行
            cd.CommandText = strSQL
            cd.Transaction = tran
            cd.Connection = Cn
            cd.ExecuteNonQuery()

            '//ITEM_HISTORY_MSのDELETE
            strSQL = ""
            strSQL &= "DELETE FROM ITEM_HISTORY_MS "
            strSQL &= "WHERE 製品NO = '" & txtItemNo.Text.Trim & "' "
            'SQL実行
            cd.CommandText = strSQL
            cd.Transaction = tran
            cd.Connection = Cn
            cd.ExecuteNonQuery()

            '削除完了
            MessageBox.Show("製品NO[" & txtItemNo.Text.Trim & "]のマスタ情報を削除完了しました。", "削除完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            tran.Commit()
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
    '--製品NO 連番取得処理           　    ----------
    '------------------------------------------------
    Public Sub fItemMS_GetSerialnumber()
        '連番取得メイン処理
        Call fItemMS_MainGetSerialnumber()

    End Sub

    '------------------------------------------------
    '--製品NO 連番取得メイン処理     　    ----------
    '------------------------------------------------
    Public Function fItemMS_MainGetSerialnumber()

        Dim dtReader As SqlDataReader
        Dim intCount As Integer '製品NO連番取得用
        Dim strCount As String   '製品NO連番設定用

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " COALESCE(MAX(製品NO),0) AS 製品NO "
            strSQL &= "FROM "
            strSQL &= " ITEM_MS "
            'SQL実行
            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader
            '連番設定
            If dtReader.HasRows Then
                While dtReader.Read
                    '取得した値を代入
                    intCount = dtReader("製品NO")
                    '＋１する
                    intCount += 1
                End While
            Else
                '０件の場合は１を代入
                intCount = 1
            End If
            dtReader.Close()
            '０埋め処理
            strCount = intCount.ToString.PadLeft(3, "0")

            '値の転送
            txtUpdateItemNO.Text = strCount

            Return True

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
        'テキストボックス初期化
        txtItemNo.Clear()
        txtUpdateItemNO.Clear()
        txtUpdateItemName.Clear()
        txtUpdateMoney.Clear()
        txtUpdateUserID.Clear()
        txtUpdateItemNO.Enabled = True

        'ラベル初期化
        lblItemName.Text = ""
        lblMode.Text = ""

        'GroupBox初期化
        GroupBox3.Enabled = False

        'DataGridView初期化
        DataGridView1.Columns.Clear()
        DataGridView1.DataSource = Nothing

    End Sub

    '------------------------------------------------
    '--クリア処理 新規作成時         　    ----------
    '------------------------------------------------
    Public Sub sClear_CreateNew()

        '//画面の初期化
        'テキストボックス初期化
        txtUpdateItemNO.Clear()
        txtUpdateItemName.Clear()
        txtUpdateMoney.Clear()
        txtUpdateUserID.Clear()
        txtUpdateItemNO.Enabled = True

        'ラベル初期化
        lblItemName.Text = ""
        lblMode.Text = ""

        'GroupBox初期化
        GroupBox3.Enabled = False

        'DataGridView初期化
        DataGridView1.Columns.Clear()
        DataGridView1.DataSource = Nothing

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
