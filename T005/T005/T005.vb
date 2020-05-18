Imports System.Xml

Public Class T005

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    '---------------------------------------------
    '--T005 Load処理                           ---
    '---------------------------------------------
    Private Sub T005_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call sClear()

    End Sub

    '---------------------------------------------
    '--T005 KeyDown処理                        ---
    '---------------------------------------------
    Private Sub T005_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                '検索
                '作業工程NOは０埋め
                If txtWorkProcessNo.Text.Trim <> "" Then
                    txtWorkProcessNo.Text = txtWorkProcessNo.Text.PadLeft(3, "0")
                End If
                Dim frm As New T005_2(1, txtWorkProcessNo.Text.Trim)

                frm.Main()

            Case Keys.F2
                '出力
                Dim frm As New T005_2(5, txtWorkProcessNo.Text.Trim)
                frm.Main()

            Case Keys.F3
                '取込
                Dim frm As New T005_2(6, txtWorkProcessNo.Text.Trim)
                frm.Main()

            Case Keys.F4
                '更新
                Dim frm As New T005_2(2, txtWorkProcessNo.Text.Trim)
                frm.Main()

            Case Keys.F5
                '印刷
                Dim frm As New T005_2(3, txtWorkProcessNo.Text.Trim)
                frm.Main()

            Case Keys.F6
                'クリア
                Call sClear()

            Case Keys.F7
                '終了
                Me.Close()

        End Select

    End Sub

    '---------------------------------------------
    '--検索ボタン押下処理                      ---
    '---------------------------------------------
    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        '検索
        '作業工程NOは０埋め
        If txtWorkProcessNo.Text.Trim <> "" Then
            txtWorkProcessNo.Text = txtWorkProcessNo.Text.PadLeft(3, "0")
        End If
        Dim frm As New T005_2(1, txtWorkProcessNo.Text.Trim)

        frm.Main()

    End Sub

    '---------------------------------------------
    '--出力ボタン押下処理                      ---
    '---------------------------------------------
    Private Sub btnOutput_Click(sender As Object, e As EventArgs)
        '出力
        Dim frm As New T005_2(5, txtWorkProcessNo.Text.Trim)
        frm.Main()

    End Sub

    '---------------------------------------------
    '--取込ボタン押下処理                      ---
    '---------------------------------------------
    Private Sub btnInput_Click(sender As Object, e As EventArgs)
        '取込
        Dim frm As New T005_2(6, txtWorkProcessNo.Text.Trim)
        frm.Main()

    End Sub

    '---------------------------------------------
    '--更新ボタン押下処理                      ---
    '---------------------------------------------
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
        '更新
        Dim frm As New T005_2(2, txtWorkProcessNo.Text.Trim)
        frm.Main()

    End Sub

    '---------------------------------------------
    '--印刷ボタン押下処理                      ---
    '---------------------------------------------
    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        '印刷
        Dim frm As New T005_2(3, txtWorkProcessNo.Text.Trim)
        frm.Main()

    End Sub

    '---------------------------------------------
    '--クリアボタン押下処理                    ---
    '---------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        'クリア
        Call sClear()

    End Sub

    '---------------------------------------------
    '--終了ボタン押下処理                      ---
    '---------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs)
        '終了
        Me.Close()

    End Sub

    '---------------------------------------------
    '---製品工程NO KeyPress処理                ---
    '---------------------------------------------
    Private Sub txtWorkProcessNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWorkProcessNo.KeyPress
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso _
            e.KeyChar <> ControlChars.Back Then
            e.Handled = True

        End If
    End Sub

    '---------------------------------------------
    '---DataGridView EditingControlShowing処理 ---
    '---------------------------------------------
    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs)
        '表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
        If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
            '編集のために表示されているコントロールを取得
            Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

            'イベントハンドラを削除
            RemoveHandler tb.KeyPress, AddressOf sDataGridViewTextBox_KeyPress

            If DataGridView1.CurrentCell.OwningColumn.Name = "工程NO" Or DataGridView1.CurrentCell.OwningColumn.Name = "製品NO" Then
                'KeyPressイベントハンドラを追加
                AddHandler tb.KeyPress, AddressOf sDataGridViewTextBox_KeyPress

            End If

        End If

    End Sub

    '---------------------------------------------
    '---DataGridView キー入力制限処理          ---
    '---------------------------------------------
    Private Sub sDataGridViewTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
        '数字しか入力できないようにする
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) AndAlso _
            e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    '---------------------------------------------
    '---クリア処理                             ---
    '---------------------------------------------
    Public Sub sClear()
        txtWorkProcessNo.Clear()

        'DataGridViewの初期化
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()

    End Sub

    '---------------------------------------------
    '---DB接続処理                             ---
    '---------------------------------------------
    Public Function fDBConnect() As Boolean

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
