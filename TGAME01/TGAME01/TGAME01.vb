Public Class TGAME01

    Dim intMeX As Integer '自分のX座標
    Dim intMeY As Integer '自分のY座標
    Dim blnEnd As Boolean '終了フラグ

    '------------------------------------------------
    '--Load処理                            ----------
    '------------------------------------------------
    Private Sub TGAME01_Load(sender As Object, e As EventArgs) Handles Me.Load
        '新規処理
        Call sCreate()

    End Sub

    '------------------------------------------------
    '--クリアボタン押下処理                ----------
    '------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        'クリア処理
        Call sClear()

    End Sub

    '------------------------------------------------
    '--キー操作メイン処理                  ----------
    '------------------------------------------------
    Private Function TGAME01_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.Up
                '//ゲーム終了の場合は操作できない
                If blnEnd = False Then
                    'Y軸が0だった場合はエラーメッセージ
                    If intMeY = 0 Then
                        MsgBox("これ以上、下には進めません。")
                        Return False
                    End If

                    '//下に移動操作
                    If Me.Controls("X" & intMeX & "Y" & intMeY - 1).Text = "●" Then
                        '//下に▲を１マス移動
                        Me.Controls("X" & intMeX & "Y" & intMeY - 1).Text = "▲"
                        '//元の位置を●に戻す
                        Me.Controls("X" & intMeX & "Y" & intMeY).Text = "●"

                        intMeY -= 1
                        '念の為、スリープ処理
                        System.Threading.Thread.Sleep(1)
                        Return True

                    ElseIf Me.Controls("X" & intMeX & "Y" & intMeY - 1).Text = "★" Then
                        '//ゴーム地点だった場合
                        MsgBox("ゲーム終了。" & vbCrLf & "おめでとうございます。")
                        lblGoalMSG01.Visible = True
                        lblGoalMSG02.Visible = True
                        blnEnd = True
                        Return True
                    End If
                End If



            Case Keys.Left

                '//ゲーム終了の場合は操作できない
                If blnEnd = False Then
                    'X軸が0だった場合はエラーメッセージ
                    If intMeX = 0 Then
                        MsgBox("これ以上、左には進めません。")
                        Return False
                    End If

                    '//左に移動操作
                    If Me.Controls("X" & intMeX - 1 & "Y" & intMeY).Text = "●" Then
                        '//右に▲を１マス移動
                        Me.Controls("X" & intMeX - 1 & "Y" & intMeY).Text = "▲"
                        '//元の位置を●に戻す
                        Me.Controls("X" & intMeX & "Y" & intMeY).Text = "●"

                        intMeX -= 1
                        '念の為、スリープ処理
                        System.Threading.Thread.Sleep(1)
                        Return True
                    ElseIf Me.Controls("X" & intMeX - 1 & "Y" & intMeY).Text = "★" Then
                        '//ゴーム地点だった場合
                        MsgBox("ゲーム終了。" & vbCrLf & "おめでとうございます。")
                        lblGoalMSG01.Visible = True
                        lblGoalMSG02.Visible = True
                        blnEnd = True
                        Return True
                    End If
                End If


            Case Keys.Right

                '//ゲーム終了の場合は操作できない
                If blnEnd = False Then
                    '//X軸が7だった場合はエラーメッセージ
                    If intMeX = 7 Then
                        MsgBox("これ以上、右には進めません。")
                        Return False
                    End If

                    '//右に移動操作
                    If Me.Controls("X" & intMeX + 1 & "Y" & intMeY).Text = "●" Then
                        '//右に▲を１マス移動
                        Me.Controls("X" & intMeX + 1 & "Y" & intMeY).Text = "▲"
                        '//元の位置を●に戻す
                        Me.Controls("X" & intMeX & "Y" & intMeY).Text = "●"

                        intMeX += 1
                        '念の為、スリープ処理
                        System.Threading.Thread.Sleep(1)
                        Return True

                    ElseIf Me.Controls("X" & intMeX + 1 & "Y" & intMeY).Text = "★" Then
                        '//ゴーム地点だった場合
                        MsgBox("ゲーム終了。" & vbCrLf & "おめでとうございます。")
                        lblGoalMSG01.Visible = True
                        lblGoalMSG02.Visible = True
                        blnEnd = True
                        Return True
                    End If

                End If



            Case Keys.Down

                '//ゲーム終了の場合は操作できない
                If blnEnd = False Then
                    'Y軸が4だった場合はエラーメッセージ
                    If intMeY = 4 Then
                        MsgBox("これ以上、下には進めません。")
                        Return False
                    End If

                    '//下に移動操作
                    If Me.Controls("X" & intMeX & "Y" & intMeY + 1).Text = "●" Then
                        '//下に▲を１マス移動
                        Me.Controls("X" & intMeX & "Y" & intMeY + 1).Text = "▲"
                        '//元の位置を●に戻す
                        Me.Controls("X" & intMeX & "Y" & intMeY).Text = "●"

                        intMeY += 1
                        '念の為、スリープ処理
                        System.Threading.Thread.Sleep(1)
                        Return True

                    ElseIf Me.Controls("X" & intMeX & "Y" & intMeY + 1).Text = "★" Then
                        '//ゴーム地点だった場合
                        MsgBox("ゲーム終了。" & vbCrLf & "おめでとうございます。")
                        lblGoalMSG01.Visible = True
                        lblGoalMSG02.Visible = True
                        blnEnd = True
                        Return True
                    End If
                End If


            Case Keys.F1
                '新規処理
                Call sCreate()
                Return True

            Case Keys.F2
                'クリア処理
                'Call sClear()
                'Return True


        End Select

    End Function

    '------------------------------------------------
    '--新規処理                            ----------
    '------------------------------------------------
    Public Sub sCreate()

        intMeX = 0
        intMeY = 0

        blnEnd = False

        lblGoalMSG01.Visible = False
        lblGoalMSG02.Visible = False

        'パターン作成
        Dim Random As New Random
        Dim intRandom As Integer = Random.Next(1, 3)
        sCreatePattern(intRandom)

    End Sub

    '------------------------------------------------
    '--新規のパターン処理                  ----------
    '--intPattern:                         ----------
    '--1-3の乱数                           ----------
    '------------------------------------------------
    Public Sub sCreatePattern(ByVal intPattern As Integer)

        If intPattern = 1 Then
            '//パターン１
            '表の初期化
            For X = 0 To 7
                For Y = 0 To 4
                    Me.Controls("X" & X & "Y" & Y).Text = "●"
                Next
            Next

            '▲の作成
            X0Y0.Text = "▲"

            '○の作成
            For y = 0 To 3
                Me.Controls("X1Y" & y).Text = "○"
                Me.Controls("X5Y" & y).Text = "○"
            Next
            For y = 1 To 4
                Me.Controls("X3Y" & y).Text = "○"
                Me.Controls("X7Y" & y).Text = "○"
            Next

            '★の作成
            Me.Controls("X7Y0").Text = "★"

        ElseIf intPattern = 2 Then
            '//パターン２
            '表の初期化
            For X = 0 To 7
                For Y = 0 To 4
                    Me.Controls("X" & X & "Y" & Y).Text = "●"
                Next
            Next

            '▲の作成
            X0Y0.Text = "▲"

            '○の作成
            For X = 0 To 6
                Me.Controls("X" & X & "Y1").Text = "○"
            Next
            For X = 1 To 7
                Me.Controls("X" & X & "Y3").Text = "○"
            Next

            '★の作成
            Me.Controls("X7Y4").Text = "★"

        End If
    End Sub




    '------------------------------------------------
    '--クリア処理                          ----------
    '------------------------------------------------
    Public Sub sClear()

        '表の初期化
        For X = 0 To 7
            For Y = 0 To 4
                Me.Controls("X" & X & "Y" & Y).Text = ""
            Next
        Next
    End Sub


End Class
