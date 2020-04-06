Imports System.Windows.Forms.VisualStyles.VisualStyleElement



Public Class BlackJack

    '----------------------------------------------------------
    '---Load処理                                            ---
    '----------------------------------------------------------
    Private Sub BlackJack_Load(sender As Object, e As EventArgs) Handles Me.Load

        sClear()

    End Sub

    '----------------------------------------------------------
    '---スタートボタン処理                                  ---
    '----------------------------------------------------------
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        btnYouDrowCard.Enabled = True
        btnYouEnd.Enabled = True

        btnStart.Enabled = False

    End Sub

    '----------------------------------------------------------
    '---リスタートボタン処理                                ---
    '----------------------------------------------------------
    Private Sub btnReStart_Click(sender As Object, e As EventArgs) Handles btnReStart.Click

        Call sClear()

    End Sub

    '----------------------------------------------------------
    '---終了ボタン処理                                      ---
    '----------------------------------------------------------
    Private Sub lblEnd_Click(sender As Object, e As EventArgs) Handles lblEnd.Click
        Me.Close()
    End Sub

    '----------------------------------------------------------
    '---ヒットボタン処理                                    ---
    '----------------------------------------------------------
    Private Sub btnYouDrowCard_Click(sender As Object, e As EventArgs) Handles btnYouDrowCard.Click

        Call frmHit(1)

    End Sub

    '----------------------------------------------------------
    '---スタンドボタン処理                                  ---
    '----------------------------------------------------------
    Private Sub btnYouEnd_Click(sender As Object, e As EventArgs) Handles btnYouEnd.Click

        Call frmHit(2)

    End Sub

    '----------------------------------------------------------
    '---ヒット処理                                 　　　   ---
    '---1=ヒット処理、2=スタンド処理                        ---
    '----------------------------------------------------------
    Public Sub frmHit(SelectMode As Integer)


        If SelectMode = 1 Then
            'ヒット時の処理
            Call sMainHit(1)

        ElseIf SelectMode = 2 Then

            Dim Result As Boolean

            'スタンド時は相手が終わるまで繰り返す
            While Result <> True
                Result = sMainHit(2)

            End While


        End If

        'Call sMainHit()

    End Sub

    '----------------------------------------------------------
    '---ヒットメイン処理                           　　　   ---
    '---1=ヒット処理、2=スタンド処理                        ---
    '----------------------------------------------------------
    Public Function sMainHit(SelectMode As Integer) As Boolean

        Dim intYouCardNumber As Integer '引いたカードの表示
        Dim intYouSumNumber As Integer 'カードの合計数

        'スタンド選択時はカードを引かない。
        If SelectMode = 1 Then
            'カードを一枚引く
            intYouCardNumber = sCardDrow(1)

            '引いたカードを表示
            'lblYouNowDrow.Text = intYouCardNumber
            'カードの合計計算
            If lblYouSumNumber.Text <> "" Then
                intYouSumNumber = CInt(lblYouSumNumber.Text) + intYouCardNumber
            Else
                intYouSumNumber += intYouCardNumber

            End If

            lblYouSumNumber.Text = intYouSumNumber

            If intYouSumNumber > 21 Then
                Call sJudgment(1)
                Return True
            End If

        End If

        '------------------
        '---相手のターン---
        '------------------

        'Randomで取ってきても、値が被ることが多いため、Sleepで100待機
        System.Threading.Thread.Sleep(100)

        intYouCardNumber = 0
        intYouSumNumber = 0

        '//スタンド時の処理
        If SelectMode = 2 And lblYouSumNumber.Text < lblEnemySumNumber.Text Then
            'スタンドで、相手のほうが値が大きかったら終了
            'あなたの負け
            Call sJudgment(1)
            Return True
        Else
            '--合計が19-21だと引かない
            '--スタンド時は引かなくなったらリザルト
            If lblEnemySumNumber.Text >= 19 Then
                lblEnemyMessage.Text = "相手はパスです。"
                If SelectMode = 2 Then
                    If lblYouSumNumber.Text > lblEnemySumNumber.Text Then
                        'あなたの勝ち
                        Call sJudgment(2)
                        Return True
                    ElseIf lblYouSumNumber.Text < lblEnemySumNumber.Text Then
                        'あなたの負け
                        Call sJudgment(1)
                        Return True
                    ElseIf lblYouSumNumber.Text = lblEnemySumNumber.Text Then
                        '引き分け
                        Call sJudgment(3)
                        Return True
                    End If

                Else

                    Return False

                End If

            End If


        End If


        'カードを一枚引く
        intYouCardNumber = sCardDrow(2)

        '引いたカードを表示
        'lblEnemyNowDrow.Text = intYouCardNumber
        'カードの合計計算
        If lblEnemySumNumber.Text <> "" Then
            intYouSumNumber = CInt(lblEnemySumNumber.Text) + intYouCardNumber
        Else
            intYouSumNumber += intYouCardNumber

        End If

        lblEnemySumNumber.Text = intYouSumNumber

        If intYouSumNumber > 21 Then
            Call sJudgment(2)
            Return True
        End If


    End Function

    '----------------------------------------------------------
    '---カードを引いた処理                             　   ---
    '---1 = あなた、 2 = 相手                               ---
    '----------------------------------------------------------
    Public Function sCardDrow(intWho As Integer) As Integer

        Dim intNumber As Integer
        Dim strName As String
        Dim strSumName As String

        If intWho = 1 Then
            strName = "lblYouNumber"
            strSumName = "lblYouNowDrow"
        Else
            strName = "lblEnemyNumber"
            strSumName = "lblEnemyNowDrow"
        End If

        '今まで引いた状態を検索（どこに代入すればよいか検索）
        For i = 1 To 20
            If Me.Controls(strName & i).Text <> "" Then
                intNumber = i + 1
            ElseIf intNumber = 0 Then
                intNumber = 1
            End If

        Next

        '1-13までランダム数値
        Dim Random As New Random
        Dim intRandom As Integer = Random.Next(1, 13)

        If intRandom = 1 Then
            'Aの処理

            If intWho = 1 Then
                '選択式、「はい」で1、「いいえ」で11。
                Dim AResult As DialogResult = MessageBox.Show("(はい = 1を選択。いいえ = 11を選択。)", _
                                 "Aを引きました。", _
                                 MessageBoxButtons.YesNo, _
                                 MessageBoxIcon.Information)

                If AResult = Windows.Forms.DialogResult.Yes Then
                    Me.Controls(strName & intNumber).Text = 1
                    Me.Controls(strSumName).Text = "A"
                    Return 1

                ElseIf AResult = Windows.Forms.DialogResult.No Then
                    Me.Controls(strName & intNumber).Text = 11
                    Me.Controls(strSumName).Text = "A"
                    Return 11

                End If
            Else
                '相手：Aの場合
                '10以下のときは、11を選択。
                If lblEnemySumNumber.Text = "" Then
                    Me.Controls(strSumName).Text = "A"
                    Me.Controls(strName & intNumber).Text = 11
                    Return 11
                ElseIf lblEnemySumNumber.Text <= 10 Then
                    Me.Controls(strSumName).Text = "A"
                    Me.Controls(strName & intNumber).Text = 11
                    Return 11
                Else
                    Me.Controls(strSumName).Text = "A"
                    Me.Controls(strName & intNumber).Text = 1
                    Return 1

                End If


            End If

        ElseIf intRandom = 10 Or intRandom = 11 Or intRandom = 12 Or intRandom = 13 Then
            '10, J, Q ,Kの処理
            If intRandom = 10 Then
                Me.Controls(strSumName).Text = "10"
            ElseIf intRandom = 11 Then
                Me.Controls(strSumName).Text = "J"
            ElseIf intRandom = 12 Then
                Me.Controls(strSumName).Text = "Q"
            ElseIf intRandom = 13 Then
                Me.Controls(strSumName).Text = "K"
            End If

            Me.Controls(strName & intNumber).Text = 10
            Return 10

        Else
            'それ以外はそのまま代入
            'Me.Controls("lblYouNumber" & intNumber).Text = intRandom
            Me.Controls(strName & intNumber).Text = intRandom
            Me.Controls(strSumName).Text = intRandom
            Return intRandom
        End If

    End Function
    '----------------------------------------------------------
    '---勝敗判定処理                                        ---
    '--1=あなたの負け、2=あなたの勝ち、3=引き分け           ---
    '----------------------------------------------------------
    Public Sub sJudgment(intWho As Integer)

        If intWho = 1 Then
            MsgBox("あなたの負けです。")

            lblYouResult.Text = "あなたの負けです。"
            lblYouResult.ForeColor = Color.Blue
            lblEnemyResult.Text = "あなたの勝ちです。"
            lblYouResult.ForeColor = Color.Red

            '連勝記録処理
            lblYouWin.Text = 0
            lblEnemyWin.Text += 1
            lblTotalEnemyWin.Text += 1

            'ヒット、スタンドボタンを使用不可にする
            btnYouDrowCard.Enabled = False
            btnYouEnd.Enabled = False
        ElseIf intWho = 2 Then
            MsgBox("あなたの勝ちです。")

            lblYouResult.Text = "あなたの勝ちです。"
            lblYouResult.ForeColor = Color.Red
            lblEnemyResult.Text = "あなたの負けです。"
            lblYouResult.ForeColor = Color.Blue

            '連勝記録処理
            lblEnemyWin.Text = 0
            lblYouWin.Text += 1
            lblTotalYouWin.Text += 1

            btnYouDrowCard.Enabled = False
            btnYouEnd.Enabled = False
        Else
            MsgBox("引き分けです。")

            lblYouResult.Text = "引き分けです。"
            lblYouResult.ForeColor = Color.Black
            lblEnemyResult.Text = "引き分けです。"
            lblYouResult.ForeColor = Color.Black

            btnYouDrowCard.Enabled = False
            btnYouEnd.Enabled = False



        End If

    End Sub

    '----------------------------------------------------------
    '---クリア処理                                          ---
    '----------------------------------------------------------
    Public Sub sClear()

        btnStart.Enabled = True

        btnYouDrowCard.Enabled = False
        btnYouEnd.Enabled = False

        lblYouResult.Text = ""
        lblEnemyResult.Text = ""
        lblYouSumNumber.Text = ""
        lblEnemySumNumber.Text = ""

        lblYouNowDrow.Text = ""
        lblEnemyNowDrow.Text = ""

        lblYouSumNumber.Text = 0
        lblEnemySumNumber.Text = 0

        lblEnemyMessage.Text = ""


        '数値を全て初期化
        For i = 1 To 20
            Me.Controls("lblYouNumber" & i).Text = ""
            Me.Controls("lblEnemyNumber" & i).Text = ""
        Next i

    End Sub



End Class
