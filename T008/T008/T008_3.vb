Imports System.Xml
Imports System.Data.SqlClient

Public Class T008_3

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strSQL As String

    Dim strOrderNo As String
    Dim strOrderMS As String
    Dim strWorkProcessNo As String
    Dim intOrderAmount As Integer
    Dim strOrderDate As String
    Dim strHumanNo As String
    Dim strOrderMSName As String
    Dim strWorkProcessName As String

    '------------------------------------------------
    '--コンストラクタ                　    ----------
    '------------------------------------------------
    Sub New(Value1 As String, Value2 As String, Value3 As String, Value4 As Integer, Value5 As DateTime, Value6 As String, Value7 As String, Value8 As String)
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        '呼び出した値を格納
        strOrderNo = Value1
        strOrderMS = Value2
        strWorkProcessNo = Value3
        intOrderAmount = Value4
        strOrderDate = Value5
        strHumanNo = Value6
        strOrderMSName = Value7
        strWorkProcessName = Value8

    End Sub

    '------------------------------------------------
    '--Load処理                      　    ----------
    '------------------------------------------------
    Private Sub T008_3_Load(sender As Object, e As EventArgs) Handles Me.Load

        txtOrderNo.Enabled = False
        txtOrderMS.Enabled = False

        txtOrderNo.Text = strOrderNo
        txtOrderMS.Text = strOrderMS
        txtWorkProcessNo.Text = strWorkProcessNo
        txtOrderAmount.Text = intOrderAmount
        dtpOrderDate.Text = strOrderDate
        txtHumanNo.Text = strHumanNo
        lblOrderMS.Text = strOrderMSName
        lblWorkProcessName.Text = strWorkProcessName

    End Sub

    '------------------------------------------------
    '--作業工程NOボタン押下処理      　    ----------
    '------------------------------------------------
    Private Sub btnWorkProcessMSSeach_Click(sender As Object, e As EventArgs) Handles btnWorkProcessMSSeach.Click
        Dim frm As New T008_4
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--作業工程NOボタン押下処理      　    ----------
    '------------------------------------------------
    Private Sub btnHumanSearch_Click(sender As Object, e As EventArgs) Handles btnHumanSearch.Click
        Dim frm As New T008_5
        frm.ShowDialog(Me)

    End Sub

    '------------------------------------------------
    '--更新ボタン押下処理            　    ----------
    '------------------------------------------------
    Private Sub btnUpate_Click(sender As Object, e As EventArgs) Handles btnUpate.Click
        Call sUpdate()
    End Sub

    '------------------------------------------------
    '--閉じるボタン押下処理          　    ----------
    '------------------------------------------------
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        '終了処理
        Me.Close()

    End Sub

    '------------------------------------------------
    '--作業工程NO  Leave処理         　    ----------
    '------------------------------------------------
    Private Sub txtWorkProcessNo_Leave(sender As Object, e As EventArgs) Handles txtWorkProcessNo.Leave
        '検索処理
        Call sWorkProcess_Search()

    End Sub

    '------------------------------------------------
    '--更新処理                      　    ----------
    '------------------------------------------------
    Public Sub sUpdate()
        Dim result As Boolean
        'DB接続
        Call sDBConnect()
        'チェック処理
        result = fUpdate_Check()
        If result = True Then
            Call sDBConnect()
            result = fUpdate()
            If result = True Then
                Me.Close()
            End If

        End If

    End Sub

    '------------------------------------------------
    '--更新前チェック処理            　    ----------
    '------------------------------------------------
    Public Function fUpdate_Check() As Boolean

        Dim dtReader As SqlDataReader

        Try
            '作業工程NOの存在チェック
            strSQL = ""
            strSQL &= "SELECT "
            strSQL &= " 作業工程NO "
            strSQL &= "FROM "
            strSQL &= " WORKPROCESS_MS "
            strSQL &= "WHERE "
            strSQL &= " 作業工程NO = '" & txtWorkProcessNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                dtReader.Close()
                '受注数が入力されているかチェック
                If txtOrderAmount.Text.Trim <> "" Then
                    '更新担当者の存在チェック
                    strSQL = ""
                    strSQL &= "SELECT "
                    strSQL &= " 名前 "
                    strSQL &= "FROM "
                    strSQL &= " HUMAN_MS "
                    strSQL &= "WHERE "
                    strSQL &= " 名前 = '" & txtHumanNo.Text.Trim & "' "

                    cd.CommandText = strSQL
                    cd.Connection = Cn
                    dtReader = cd.ExecuteReader

                    If dtReader.HasRows Then
                        dtReader.Close()
                        '受注チェックフラグがTRUEの場合はNG
                        strSQL = ""
                        strSQL &= "SELECT "
                        strSQL &= " 受注NO, "
                        strSQL &= " 受注チェックフラグ "
                        strSQL &= "FROM "
                        strSQL &= " ORDER_TBL "
                        strSQL &= "WHERE "
                        strSQL &= " 受注NO = " & txtOrderNo.Text.Trim & " "

                        cd.CommandText = strSQL
                        cd.Connection = Cn
                        dtReader = cd.ExecuteReader

                        Dim result As Boolean
                        While dtReader.Read
                            result = dtReader("受注チェックフラグ")
                            If result = True Then
                                MessageBox.Show("チェック済みの受注NOです。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return False
                            End If
                        End While

                        Return True
                    Else
                        dtReader.Close()
                        MessageBox.Show("入力した更新担当者は存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtHumanNo.Focus()
                        Return False
                    End If
                Else
                    MessageBox.Show("受注数が入力されていません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtOrderAmount.Focus()
                    Return False
                End If
            Else
                dtReader.Close()
                MessageBox.Show("入力した作業工程NOは存在しません。" & vbCrLf & "確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtWorkProcessNo.Focus()
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
    '--更新処理                      　    ----------
    '------------------------------------------------
    Public Function fUpdate()
        Try
            strSQL = ""
            strSQL &= "UPDATE ORDER_TBL "
            strSQL &= "SET "
            strSQL &= " 作業工程NO='" & txtWorkProcessNo.Text.Trim & "', "
            strSQL &= " 受注数=" & txtOrderAmount.Text.Trim & ", "
            strSQL &= " 受注日='" & dtpOrderDate.Text & "',"
            strSQL &= " 最終更新者='" & txtHumanNo.Text.Trim & "',"
            strSQL &= " 更新日=SYSDATETIME() "
            strSQL &= "WHERE "
            strSQL &= " 受注NO=" & txtOrderNo.Text.Trim & " "

            cd.CommandText = strSQL
            cd.Connection = Cn
            cd.ExecuteNonQuery()

            MessageBox.Show("更新完了しました。", "更新完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
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
    '--作業工程NO 検索処理                 ----------
    '------------------------------------------------
    Public Sub sWorkProcess_Search()
        '０埋め処理
        txtWorkProcessNo.Text = txtWorkProcessNo.Text.PadLeft(3, "0")
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
            strSQL &= " 作業工程NO, "
            strSQL &= " 作業工程名 "
            strSQL &= "FROM "
            strSQL &= " WORKPROCESS_MS "
            strSQL &= "WHERE "
            strSQL &= " 作業工程NO = '" & txtWorkProcessNo.Text.Trim & "' "

            cd.CommandText = strSQL
            cd.Connection = Cn
            dtReader = cd.ExecuteReader

            If dtReader.HasRows Then
                While dtReader.Read
                    txtWorkProcessNo.Text = dtReader("作業工程NO").ToString.Trim
                    lblWorkProcessName.Text = dtReader("作業工程名").ToString.Trim
                End While

                dtReader.Close()
                Return True

            Else
                dtReader.Close()
                lblWorkProcessName.Text = ""
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