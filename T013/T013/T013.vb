Imports System.Xml
Imports System.Data.SqlClient

Public Class T013

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String


    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub T013_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call sClear()

    End Sub

    '------------------------------------------------
    '--ファンクションキー処理              ----------
    '------------------------------------------------
    Private Sub T013_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode

            Case Keys.F1
                '検索処理
                Call sSearch()
            Case Keys.F2
                '登録処理
                Call sInsert()
            Case Keys.F3
                'クリア処理
                Call sClear()
            Case Keys.F4
                '終了処理
                Me.Close()

        End Select

    End Sub

    '------------------------------------------------
    '--受注先NOボタン押下処理              ----------
    '------------------------------------------------
    Private Sub btnOrderSearch_Click(sender As Object, e As EventArgs) Handles btnOrderMSNOSearch.Click
        Dim frm As New T013_2
        frm.ShowDialog(Me)
    End Sub

    '------------------------------------------------
    '--受注番号検索ボタン押下処理          ----------
    '------------------------------------------------
    Private Sub btnOrderNOSearch_Click(sender As Object, e As EventArgs) Handles btnOrderNOSearch.Click
        Call fOrderNO_Search()
    End Sub

    '------------------------------------------------
    '--在庫確認ボタン押下処理              ----------
    '------------------------------------------------
    Private Sub btnStockCheck_Click(sender As Object, e As EventArgs) Handles btnMainStockCheck.Click
        Dim frm As New T013_4
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--出荷先NOボタン押下処理              ----------
    '------------------------------------------------
    Private Sub btnMainShipmentMS_Click(sender As Object, e As EventArgs) Handles btnMainShipmentMS.Click
        Dim frm As New T013_5
        frm.ShowDialog(Me)
    End Sub

    '------------------------------------------------
    '--更新担当者ボタン押下処理            ----------
    '------------------------------------------------
    Private Sub btnUserIDSearch_Click(sender As Object, e As EventArgs) Handles btnUserIDSearch.Click
        Dim frm As New T013_6
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--検索ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnMainSearch_Click(sender As Object, e As EventArgs) Handles btnMainSearch.Click
        '検索処理
        Call sSearch()
    End Sub

    '------------------------------------------------
    '--登録ボタン押下処理                  ----------
    '------------------------------------------------
    Private Sub btnMainInsert_Click(sender As Object, e As EventArgs) Handles btnMainInsert.Click
        '登録処理
        Call sInsert()
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
    '--出荷先チェック変更時処理            ----------
    '------------------------------------------------
    Private Sub chkMainShipmentMS_CheckedChanged(sender As Object, e As EventArgs) Handles chkMainShipmentMS.CheckedChanged
        If chkMainShipmentMS.Checked = True Then
            txtMainShipmentMSNO.Enabled = True
            btnMainShipmentMS.Enabled = True

        Else
            txtMainShipmentMSNO.Enabled = False
            btnMainShipmentMS.Enabled = False

        End If

    End Sub

    '------------------------------------------------
    '--受注先NO Leave処理                  ----------
    '------------------------------------------------
    Private Sub txtOrderMSNo_Leave(sender As Object, e As EventArgs) Handles txtOrderMSNo.Leave
        '検索処理
        Call sOrderMS_Search()
    End Sub

    '------------------------------------------------
    '--出荷先NO Leave処理                  ----------
    '------------------------------------------------
    Private Sub txtMainShipmentMSNO_Leave(sender As Object, e As EventArgs) Handles txtMainShipmentMSNO.Leave
        '検索処理
        Call sShipmentMS_MainSearch()
    End Sub

    '------------------------------------------------
    '--出荷数 KeyPress処理                 ----------
    '------------------------------------------------
    Private Sub txtMainShipmentNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMainShipmentNumber.KeyPress
        If (e.KeyChar < "0"c OrElse e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    '------------------------------------------------
    '--受注一覧検索処理                    ----------
    '------------------------------------------------
    Public Sub sSearch()
        Dim result As Boolean
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        result = fMainSearch()
        If result = True Then
            'DB接続
            Call sDBConnect()
            '受注残の計算処理
            Call fReOrderNumbeCal()
            'DB接続
            Call sDBConnect()
            '合計在庫数の取得
            Call fSumStock()
            '出荷数とボタンと出荷先チェックボックスを使用可
            txtMainShipmentNumber.Enabled = True
            btnMainStockCheck.Enabled = True
            chkMainShipmentMS.Enabled = True

        End If

    End Sub

    '------------------------------------------------
    '--受注一覧検索メイン処理              ----------
    '------------------------------------------------
    Public Function fMainSearch() As Boolean

        Dim dtReader As SqlDataReader

        Try
            If txtOrderNo.Text.Trim <> "" Then
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " A.受注NO, "
                strSQL &= " A.受注先NO, "
                strSQL &= "	C.受注先名, "
                strSQL &= " A.作業工程NO, "
                strSQL &= "	B.作業工程名, "
                strSQL &= " B.製品NO, "
                strSQL &= " D.製品名, "
                strSQL &= " A.受注数, "
                strSQL &= " A.受注日, "
                strSQL &= " A.最終更新者, "
                strSQL &= " A.受注チェックフラグ "
                strSQL &= "FROM "
                strSQL &= " ORDER_TBL A, "
                strSQL &= " WORKPROCESS_MS B, "
                strSQL &= " ORDER_MS C, "
                strSQL &= " ITEM_MS D "
                strSQL &= "WHERE "
                strSQL &= "    A.作業工程NO = B.作業工程NO "
                strSQL &= "AND A.受注先NO = C.受注先NO "
                strSQL &= "AND B.製品NO = D.製品NO "
                strSQL &= "AND A.受注NO = " & txtOrderNo.Text.Trim & " "
                '20200513追記 受注チェック画面でチェックしていることが条件追加
                strSQL &= "AND A.受注チェックフラグ = 1 "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    While dtReader.Read
                        lblMainOrderMSNO.Text = dtReader("受注先NO").ToString.Trim
                        lblMainOrderMSName.Text = dtReader("受注先名").ToString.Trim
                        lblMainWorkProcessMSNO.Text = dtReader("作業工程NO").ToString.Trim
                        lblMainWorkProcessMSName.Text = dtReader("作業工程名").ToString.Trim
                        lblMainItemMSNO.Text = dtReader("製品NO").ToString.Trim
                        lblMainItemMSName.Text = dtReader("製品名").ToString.Trim
                        lblMainOrderNumber.Text = dtReader("受注数").ToString.Trim

                    End While

                    dtReader.Close()
                    Return True


                Else
                    MessageBox.Show("検索結果が０件です。" & vbCrLf & "もしくは受注チェックが未処理状態です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    dtReader.Close()
                    Call sMainSearch_Clear()
                    Return False
                End If
            Else
                MessageBox.Show("受注番号が未入力です。" & vbCrLf & "確認してください。", vbCrLf, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Call sMainSearch_Clear()
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
    '--受注一覧受注残計算処理              ----------
    '------------------------------------------------
    Public Function fReOrderNumbeCal()

        Dim dtReader As SqlDataReader
        Dim intOrderNumber As Integer '受注数格納用
        Dim intShipmentNumber As Integer '出荷数格納用
        Dim intReOrderNumber As Integer '受注残数格納用

        Try
            '受注数と合計出荷数を取得する
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " A.受注NO, "
            strSQL &= " A.受注数, "
            strSQL &= " ISNULL(B.合計出荷数,0) AS 合計出荷数 "
            strSQL &= "FROM "
            strSQL &= " ORDER_TBL A "
            strSQL &= " LEFT JOIN "
            strSQL &= " (SELECT "
            strSQL &= "   受注NO, "
            strSQL &= "   SUM(出荷数) AS 合計出荷数 "
            strSQL &= "  FROM "
            strSQL &= "   SHIPMENT_TBL "
            strSQL &= "  WHERE "
            strSQL &= "   受注NO = " & txtOrderNo.Text.Trim & " "
            strSQL &= "  GROUP BY "
            strSQL &= "   受注NO) B ON A.受注NO = B.受注NO "
            strSQL &= "WHERE "
            strSQL &= " A.受注NO = " & txtOrderNo.Text.Trim & " "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    intOrderNumber = dtReader("受注数")
                    intShipmentNumber = dtReader("合計出荷数")
                End While

                dtReader.Close()
                '受注残数の計算
                intReOrderNumber = intOrderNumber - intShipmentNumber
                lblMainReOrderNumber.Text = intReOrderNumber
                Return True

            Else
                dtReader.Close()
                lblMainReOrderNumber.Text = 0
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
    '--受注一覧合計在庫数取得              ----------
    '------------------------------------------------
    Public Function fSumStock()

        Dim dtReader As SqlDataReader

        Try
            '合計在庫数の取得
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= "  製品NO, "
            strSQL &= "  SUM(在庫数) AS 合計在庫数 "
            strSQL &= "FROM "
            strSQL &= "  STOCK_TBL "
            strSQL &= "WHERE "
            strSQL &= "  製品NO = '" & lblMainItemMSNO.Text.Trim & "' "
            strSQL &= "GROUP BY "
            strSQL &= "  製品NO "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    lblMainSumStock.Text = dtReader("合計在庫数")
                End While
            Else
                '取得できない場合は０を代入
                lblMainSumStock.Text = 0
            End If

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
    '--登録処理                    ----------
    '------------------------------------------------
    Public Sub sInsert()
        Dim result As Boolean
        '登録前チェック処理
        result = fCheckInsert()
        If result = True Then
            'DB接続
            Call sDBConnect()
            '登録処理
            result = fMainInsert()
            If result = True Then
                '初期化処理
                Call sClear()
            End If
        End If



    End Sub

    '------------------------------------------------
    '--登録前チェック処理                  ----------
    '------------------------------------------------
    Public Function fCheckInsert() As Boolean

        Dim blnResult As Boolean


        Dim result As DialogResult = MessageBox.Show("出荷登録しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
        If result = Windows.Forms.DialogResult.Yes Then
            '出荷数の入力確認
            If txtMainShipmentNumber.Text.Trim = "" Then
                MessageBox.Show("出荷数が未入力です。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            '受注残数＜入力出荷数の場合は、エラーとする
            If Integer.Parse(lblMainReOrderNumber.Text) < Integer.Parse(txtMainShipmentNumber.Text.Trim) Then
                MessageBox.Show("残数より入力した出荷数のほうが大きくなっています。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            '在庫合計数＜入力出荷数の場合は、エラーとする
            If Integer.Parse(lblMainSumStock.Text) < Integer.Parse(txtMainShipmentNumber.Text.Trim) Then
                MessageBox.Show("合計在庫数より入力した出荷数のほうが大きくなっています。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If



            '//受注先NOと出荷先NOが一致するかチェック
            'DB接続
            Call sDBConnect()
            'チェック処理２
            blnResult = fCheckInsert2()
            If blnResult = False Then
                Return False
            End If

            '//入力した更新担当者がHUMAN_MSに存在するか確認
            'DB接続
            Call sDBConnect()
            blnResult = fCheckInsert3()
            If blnResult = True Then
                Return True
            Else
                Return False
            End If


        Else
            Return False
        End If

    End Function

    '------------------------------------------------
    '--登録前チェック処理２                ----------
    '------------------------------------------------
    Public Function fCheckInsert2() As Boolean

        Dim dtReader As SqlDataReader

        '//「出荷先が受注先と異なる場合」のチェックが入っていない場合で、
        '//受注先NOと出荷先NOが一致するかチェックする
        '//受注先マスタ画面で出荷先マスタも同時登録シている場合は完全一致するため、その確認

        Try
            If chkMainShipmentMS.Checked = False Then
                '出荷先マスタに存在しているか確認
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " 出荷先NO "
                strSQL &= "FROM "
                strSQL &= " SHIPMENT_MS "
                strSQL &= "WHERE "
                strSQL &= "    出荷先NO = '" & lblMainOrderMSNO.Text.Trim & "' "
                strSQL &= "AND 出荷先名 = '" & lblMainOrderMSName.Text.Trim & "' "

                cd.CommandText = strSQL
                cd.Connection = Cn
                dtReader = cd.ExecuteReader

                If dtReader.HasRows Then
                    '完全一致の場合は問題なし
                    dtReader.Close()
                    Return True
                Else
                    '完全一致しない場合はエラー
                    MessageBox.Show("受注先マスタと出荷先マスタが一致しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    dtReader.Close()
                    Return False
                End If


            Else
                'チェックが入っている場合は問題なし
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
    '--登録前チェック処理３                ----------
    '------------------------------------------------
    Public Function fCheckInsert3() As Boolean

        Dim dtReader As SqlDataReader

        '//入力した更新担当者がHUMAN_MSに存在するか確認
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
                MessageBox.Show("入力したログインIDは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    '--登録メイン処理                      ----------
    '------------------------------------------------
    Public Function fMainInsert() As Boolean

        Dim dtReader As SqlDataReader
        Dim intReShipmentNumber As Integer '残りの出荷数
        Dim dtMinUpdateTime As DateTime '更新日の最小値
        Dim intMinUpdateTimeMill As Integer 'ミリ秒
        Dim intStockNO As Integer '在庫NO格納用
        Dim intStockNumber As Integer '在庫数格納用
        Dim intMaxShipmentNO As Integer '出荷NO最大値格納用

        Dim tran As SqlTransaction
        tran = Cn.BeginTransaction

        Try
            '入力した出荷数の取得
            intReShipmentNumber = txtMainShipmentNumber.Text

            '入力した出荷数が０になるまで繰り返す
            Do While intReShipmentNumber > 0

                '製品NO別の更新日の最小値を変数に格納
                strSQL = ""
                strSQL &= "SELECT "
                strSQL &= " MIN(更新日) AS 最古日, "
                strSQL &= " 製品NO "
                strSQL &= "FROM  "
                strSQL &= " STOCK_TBL "
                strSQL &= "WHERE "
                strSQL &= " 製品NO = '" & lblMainItemMSNO.Text.Trim & "' "
                strSQL &= "GROUP BY "
                strSQL &= " 製品NO "
                cd.CommandText = strSQL
                cd.Transaction = tran
                cd.Connection = Cn
                dtReader = cd.ExecuteReader
                If dtReader.HasRows Then
                    While dtReader.Read

                        dtMinUpdateTime = dtReader("最古日")
                        'ミリ秒も取得する
                        intMinUpdateTimeMill = dtMinUpdateTime.Millisecond
                    End While

                    dtReader.Close()

                    '取得した最古日の在庫NO,在庫数を取得
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " 在庫NO, "
                    strSQL &= " 製品NO, "
                    strSQL &= " 在庫数, "
                    strSQL &= " 更新日 "
                    strSQL &= "FROM "
                    strSQL &= " STOCK_TBL "
                    strSQL &= "WHERE "
                    strSQL &= "    更新日 = '" & dtMinUpdateTime & ":" & intMinUpdateTimeMill & "' "
                    strSQL &= "AND 製品NO = '" & lblMainItemMSNO.Text.Trim & "' "

                    cd.CommandText = strSQL
                    cd.Transaction = tran
                    cd.Connection = Cn
                    dtReader = cd.ExecuteReader

                    If dtReader.HasRows Then
                        While dtReader.Read
                            intStockNO = dtReader("在庫NO")
                            intStockNumber = dtReader("在庫数")
                        End While

                        dtReader.Close()

                        '取得した在庫　＜＝　入力した出荷数の場合は取得した在庫をDELETEする
                        If intStockNumber <= intReShipmentNumber Then
                            strSQL = ""
                            strSQL &= "DELETE FROM STOCK_TBL "
                            strSQL &= "WHERE 在庫NO = " & intStockNO & " "

                            cd.CommandText = strSQL
                            cd.Transaction = tran
                            cd.Connection = Cn
                            cd.ExecuteNonQuery()

                            '入力した出荷数から減算
                            intReShipmentNumber = intReShipmentNumber - intStockNumber

                        Else
                            '取得した在庫　＞　入力した出荷数の場合は、在庫テーブルから減算する

                            '//在庫テーブルから最古日の在庫から減算//
                            strSQL = ""
                            strSQL &= "UPDATE STOCK_TBL "
                            strSQL &= "SET 在庫数 -= " & intReShipmentNumber & " "
                            strSQL &= "WHERE 在庫NO = " & intStockNO & " "
                            cd.CommandText = strSQL
                            cd.Transaction = tran
                            cd.Connection = Cn
                            cd.ExecuteNonQuery()

                            '入力した出荷数の減算（必ず０になる）
                            intReShipmentNumber = intReShipmentNumber - intReShipmentNumber

                        End If



                    Else
                        'ありえないが、FALSEを返す
                        Return False

                    End If

                Else
                    'ありえないが、FALSEを返す
                    Return False
                End If
            Loop


            '//出荷テーブル登録//
            '出荷NOの連番を取得
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= "  COALESCE(MAX(出荷NO), 0) AS 出荷NO "
            strSQL &= "FROM "
            strSQL &= " SHIPMENT_TBL "
            cd.CommandText = strSQL
            cd.Transaction = tran
            cd.Connection = Cn
            dtReader = cd.ExecuteReader
            If dtReader.HasRows Then
                While dtReader.Read
                    intMaxShipmentNO = dtReader("出荷NO")
                    intMaxShipmentNO += 1
                End While
            Else
                'ありえないけど、取得できなかった場合は０を代入
                intMaxShipmentNO = 0
            End If

            dtReader.Close()

            '//登録処理//
            strSQL = ""
            strSQL &= "INSERT INTO SHIPMENT_TBL VALUES "
            strSQL &= "( "
            strSQL &= "" & intMaxShipmentNO & ", "
            If chkMainShipmentMS.Checked = False Then
                '出荷先チェックボックスがオフの場合、受注先NOを取得
                strSQL &= "'" & lblMainOrderMSNO.Text.Trim & "', "
            Else
                '出荷先チェックボックスがオンの場合、出荷先NOを取得
                strSQL &= "'" & txtMainShipmentMSNO.Text.Trim & "', "
            End If
            strSQL &= "" & txtOrderNo.Text.Trim & ", "
            strSQL &= "'" & lblMainWorkProcessMSNO.Text.Trim & "', "
            strSQL &= "" & txtMainShipmentNumber.Text.Trim & ", "
            strSQL &= "'FALSE', "
            strSQL &= "SYSDATETIME(), "
            strSQL &= "'" & txtUserID.Text.Trim & "', "
            strSQL &= "SYSDATETIME(), "
            strSQL &= "SYSDATETIME() "
            strSQL &= ") "

            cd.CommandText = strSQL
            cd.Transaction = tran
            cd.Connection = Cn
            cd.ExecuteNonQuery()



            '//完了処理//
            MessageBox.Show("出荷登録が完了しました。", "出荷完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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
    '--受注NO 検索処理                     ----------
    '------------------------------------------------
    Public Sub fOrderNO_Search()
        Dim result As Boolean
        'DB接続
        Call sDBConnect()
        'チェック処理
        result = fOrderNO_CheckSearch()
        If result = True Then
            Dim frm As New T013_3
            frm.ShowDialog(Me)
        End If

    End Sub

    '------------------------------------------------
    '--受注NO 検索前チェック処理           ----------
    '------------------------------------------------
    Public Function fOrderNO_CheckSearch() As Boolean

        Dim dtReader As SqlDataReader

        Try
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 受注NO, "
            strSQL &= " 受注先NO, "
            strSQL &= " 作業工程NO, "
            strSQL &= " 受注数, "
            strSQL &= " 受注日, "
            strSQL &= " 最終更新者 "
            strSQL &= "FROM "
            strSQL &= " ORDER_TBL "
            strSQL &= "WHERE "
            strSQL &= "    受注チェックフラグ = 1 "
            strSQL &= "AND 受注先NO = '" & txtOrderMSNo.Text.Trim & "' "
            strSQL &= "AND 受注日 BETWEEN "
            strSQL &= "    '" & dtpOrderDateFrom.Text.Trim & "' AND '" & dtpOrderDateTo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                dtReader.Close()
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
    '--受注先NO 検索処理                   ----------
    '------------------------------------------------
    Public Sub sOrderMS_Search()
        '０埋め
        txtOrderMSNo.Text = txtOrderMSNo.Text.PadLeft(3, "0")
        'DB接続
        Call sDBConnect()
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
                    lblOrderMSName.Text = dtReader("受注先名")
                End While

                dtReader.Close()
                Return True

            Else
                lblOrderMSName.Text = ""
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
    '--出荷先NO 検索処理       　    ----------
    '------------------------------------------------
    Public Sub sShipmentMS_MainSearch()
        '０埋め
        txtMainShipmentMSNO.Text = txtMainShipmentMSNO.Text.PadLeft(3, "0")
        'DB接続
        Call sDBConnect()
        '検索メイン処理
        Call fShipmentMS_MainSearch()

    End Sub

    '------------------------------------------------
    '--出荷先NO 検索メイン処理       　    ----------
    '------------------------------------------------
    Public Function fShipmentMS_MainSearch()

        Dim dtReader As SqlDataReader

        Try
            '出荷先NOの取得
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 出荷先NO, "
            strSQL &= " 出荷先名 "
            strSQL &= "FROM "
            strSQL &= " SHIPMENT_MS "
            strSQL &= "WHERE "
            strSQL &= " 出荷先NO = '" & txtMainShipmentMSNO.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    lblMainShipmentMSName.Text = dtReader("出荷先名")
                End While

                dtReader.Close()
                Return True

            Else
                lblMainShipmentMSName.Text = ""
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
    '--出荷一覧ラベル箇所クリア処理        ----------
    '------------------------------------------------
    Public Sub sMainSearch_Clear()
        lblMainOrderMSNO.Text = ""
        lblMainWorkProcessMSNO.Text = ""
        lblMainItemMSNO.Text = ""
        lblMainOrderNumber.Text = ""
        lblMainReOrderNumber.Text = ""
        lblMainSumStock.Text = ""
        lblMainOrderMSName.Text = ""
        lblMainWorkProcessMSName.Text = ""
        lblMainItemMSName.Text = ""

        txtMainShipmentNumber.Clear()
        txtMainShipmentMSNO.Clear()
        lblMainShipmentMSName.Text = ""
        txtMainShipmentNumber.Enabled = False
        txtMainShipmentMSNO.Enabled = False
        lblMainShipmentMSName.Enabled = False
        chkMainShipmentMS.Enabled = False
        chkMainShipmentMS.Checked = False

    End Sub

    '------------------------------------------------
    '--クリア処理                          ----------
    '------------------------------------------------
    Public Sub sClear()

        txtOrderNo.Clear()
        txtOrderMSNo.Clear()
        txtMainShipmentNumber.Clear()
        lblOrderMSName.Text = ""
        dtpOrderDateFrom.Text = Date.Now
        dtpOrderDateTo.Text = Date.Now
        txtMainShipmentNumber.Enabled = False
        btnMainStockCheck.Enabled = False
        lblMainSumStock.Text = ""
        chkMainShipmentMS.Checked = False
        chkMainShipmentMS.Enabled = False
        txtMainShipmentMSNO.Clear()
        lblMainShipmentMSName.Text = ""
        lblMainOrderMSNO.Text = ""
        lblMainWorkProcessMSNO.Text = ""
        lblMainItemMSNO.Text = ""
        lblMainOrderNumber.Text = ""
        lblMainReOrderNumber.Text = ""
        txtMainShipmentMSNO.Enabled = False
        btnMainShipmentMS.Enabled = False
        lblMainOrderMSName.Text = ""
        lblMainWorkProcessMSName.Text = ""
        lblMainItemMSName.Text = ""



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
