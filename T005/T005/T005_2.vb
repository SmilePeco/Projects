Imports System.Xml
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports System.Text
Imports System.IO
Imports Microsoft.Office.Interop

Public Class T005_2

    Dim ProgramName As String = "T005" 'エラーログ出力
    Dim ClassName As String 'エラーログ出力


    Dim intJudge As Integer
    Dim strWorkProcessNo As String

    Dim strConnect As String 'DB接続用
    Dim Cn As New System.Data.SqlClient.SqlConnection
    Dim cd As System.Data.SqlClient.SqlCommand
    Dim strMasterSQL As String

    'T005をアクティブフォームに設定
    Dim frm As New T005()

    '---------------------------------------------
    '---コンストラクタ                         ---
    '---------------------------------------------
    Sub New(intNewJudge As Integer, strNewWorkProcessNo As String)
        intJudge = intNewJudge
        strWorkProcessNo = strNewWorkProcessNo
        frm = T005.ActiveForm

    End Sub

    '---------------------------------------------
    '---メイン処理                             ---
    '---------------------------------------------
    Sub Main()

        Dim result As Boolean

        'DB接続の判定
        result = fDBConnect()

        If result = True Then
            If intJudge = 1 Then
                '検索処理
                Call fSelect()
            ElseIf intJudge = 2 Then
                '更新処理
                Call fUpdate()
            ElseIf intJudge = 3 Then
                '印刷処理
                Call fPrint()
            ElseIf intJudge = 5 Then
                'CSV出力
                Call fSelect()
            ElseIf intJudge = 6 Then
                'CSV取込
                Call fInputCheck()

            End If

        End If

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

    '---------------------------------------------
    '---検索、CSV出力処理                      ---
    '---------------------------------------------
    Public Function fSelect() As Boolean

        Try
            '空白の場合は全件検索
            strMasterSQL = ""
            strMasterSQL &= "SELECT "
            strMasterSQL &= " 作業工程NO, "
            strMasterSQL &= " 工程NO, "
            strMasterSQL &= " 製品NO, "
            strMasterSQL &= " trim(作業工程名) AS 作業工程名, "
            strMasterSQL &= " 更新日, "
            strMasterSQL &= " 登録日 "
            strMasterSQL &= "FROM "
            strMasterSQL &= " WORKPROCESS_MS "
            '作業工程NOが空白以外かつ、SELET文の場合はWHERE文を追加する
            If frm.txtWorkProcessNo.Text.Trim <> "" And intJudge = 1 Then
                strMasterSQL &= "WHERE "
                strMasterSQL &= " 作業工程NO = '" + strWorkProcessNo + "' "

            End If
            strMasterSQL &= "ORDER BY "
            strMasterSQL &= " 作業工程NO "

            Dim dsDataset As New DataSet
            Dim daDataAdapter As New SqlClient.SqlDataAdapter

            daDataAdapter.SelectCommand = New SqlClient.SqlCommand(strMasterSQL, Cn)
            daDataAdapter.SelectCommand.CommandTimeout = 0
            daDataAdapter.Fill(dsDataset, "TABLE001")

            'SELECT時のみDataGridViewの表示
            If intJudge = 1 Then

                frm.DataGridView1.DataSource = dsDataset.Tables("TABLE001")
                'frm.DataGridView1.DataSource = dsDataset.Tables("TABLE001")

                '作業工程NO、更新日、登録日は編集不可
                'frm.DataGridView1.Columns(0).ReadOnly = True
                frm.DataGridView1.Columns(4).ReadOnly = True
                frm.DataGridView1.Columns(5).ReadOnly = True
                '桁数制限
                DirectCast(frm.DataGridView1.Columns(1), DataGridViewTextBoxColumn).MaxInputLength = 3
                DirectCast(frm.DataGridView1.Columns(2), DataGridViewTextBoxColumn).MaxInputLength = 3
                DirectCast(frm.DataGridView1.Columns(3), DataGridViewTextBoxColumn).MaxInputLength = 20

                '//メソッド呼び出しだとうまくいかないため、コメント化
                'DataGridViewの初期設定
                'Call sDataGridViewSetting()



                Cn.Close()
                Cn.Dispose()

                Return True

            ElseIf intJudge = 5 Then
                'CSV出力処理

                'ダイアログボックスの設定
                Dim FileDialog As New SaveFileDialog
                FileDialog.FileName = "OutputFile.csv"
                FileDialog.InitialDirectory = "C:\"
                FileDialog.Filter = "CSVファイル(*.csv)|*.csv"

                If FileDialog.ShowDialog() = DialogResult.OK Then
                    'Dim strSaveFile As String = "C:\Users\Shinya\Documents\CSV\OutputFile.csv"

                    'データテーブルの設定
                    Dim dtDataTable As DataTable
                    dtDataTable = dsDataset.Tables("TABLE001")

                    'エンコード設定
                    Dim encode As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
                    'CSVファイルを開く
                    'Dim CSV As New System.IO.StreamWriter(strSaveFile, False, encode)
                    Dim CSV As New System.IO.StreamWriter(FileDialog.FileName, False, encode)

                    Dim intColCount As Integer = dtDataTable.Columns.Count
                    Dim intLastIndes As Integer = intColCount - 1
                    Dim i As Integer

                    'ヘッダーの書き込み
                    For i = 0 To intColCount - 1
                        Dim strFiled As String = dtDataTable.Columns(i).Caption
                        '""で囲む
                        strFiled = fEncloseDoubleQuotes(strFiled)
                        CSV.Write(strFiled)
                        If intLastIndes > i Then
                            CSV.Write(","c)
                        End If
                    Next
                    '改行
                    CSV.Write(vbCrLf)

                    'データの書き込み
                    Dim Row As DataRow
                    For Each Row In dtDataTable.Rows
                        For i = 0 To intColCount - 1
                            Dim strFiled As String = Row(i).ToString()
                            'トリムで空白削除
                            strFiled = strFiled.Trim()

                            '""で囲む
                            '(BULK INSERTで取り込む際は、""は不要のため、コメント
                            'strFiled = fEncloseDoubleQuotes(strFiled)
                            CSV.Write(strFiled)
                            If intLastIndes > i Then
                                CSV.Write(","c)
                            End If
                        Next
                        '改行
                        CSV.Write(vbCrLf)
                    Next

                    'CSV出力完了メッセージ
                    MessageBox.Show("CSV出力完了しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    CSV.Close()

                    Cn.Close()
                    Cn.Dispose()

                    Return True

                End If

                'CSV出力がキャンセルされた場合
                Cn.Close()
                Cn.Dispose()

                Return False

            End If


            Cn.Close()
            Cn.Dispose()

            Return False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "例外発生")
            Return False

        End Try

    End Function

    '---------------------------------------------
    '---更新処理                               ---
    '---------------------------------------------
    Public Function fUpdate() As Boolean

        Dim intRowCount As Integer '現在の行数を格納
        Dim blnFalse As Boolean = False '取込失敗しているか判定（失敗している場合はDataGridViewを閉じない）
        Dim strDt As String = Now.ToString("yyyyMMddHHmmss")        'エラーログファイル名（重複を避けるため、For文前で作成）
        Dim intErrCount As Integer        'エラー数カウント
        Dim result As Boolean 'エラーチェック処理用
        Dim strMasterSQL As String '変更不可なマスタSQL(INSERT、UPDATE、DELETE)
        Dim dtReader As SqlDataReader
        'Dim strErrText As String          'エラー内容
        'Dim intErrRow As Integer          'エラーの起きた行番号

        Try


            If frm.DataGridView1.DataSource IsNot Nothing Then
                '最終確認
                Dim dr As DialogResult = MessageBox.Show("更新してもよろしいですか？", "更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If dr = DialogResult.Yes Then
                    'DataSourceをDatatableにキャストする
                    Dim dtTable As DataTable = DirectCast(frm.DataGridView1.DataSource, DataTable)
                    '判定の初期化

                    For Each Row As DataRow In dtTable.Rows

                        intRowCount += 1

                        '●SQL文の生成

                        Select Case Row.RowState
                            Case DataRowState.Added
                                Dim strMax As String = ""

                                '//新規作成の場合

                                '//作業工程NOが空欄で登録した場合
                                '//WORKPROCESS_MSをチェックし、連番で登録する（穴空きは無視とする）
                                If Row("作業工程NO").ToString.Trim = "" Then
                                    Dim intMax As Integer 'MAX値を格納
                                    '作業工程NOのMAX値を取得
                                    strMasterSQL = ""
                                    strMasterSQL &= "SELECT "
                                    strMasterSQL &= " MAX(作業工程NO) AS 作業工程NO "
                                    strMasterSQL &= "FROM "
                                    strMasterSQL &= " WORKPROCESS_MS "
                                    'SQLの実行
                                    cd.CommandText = strMasterSQL
                                    cd.Connection = Cn
                                    'cd.ExecuteNonQuery()
                                    dtReader = cd.ExecuteReader()
                                    While dtReader.Read()
                                        '最大件数の取得
                                        intMax = dtReader("作業工程NO")
                                        intMax += 1
                                    End While
                                    '０埋め
                                    strMax = CStr(intMax).PadLeft(3, "0")
                                    dtReader.Close()
                                End If

                                '工程NO、製品NOは０埋め（空白の場合は処理しない）
                                If Row("工程NO").ToString.Trim <> "" Then
                                    Row("工程NO") = Row("工程NO").PadLeft(3, "0")
                                End If
                                If Row("製品NO").ToString.Trim <> "" Then
                                    Row("製品NO") = Row("製品NO").PadLeft(3, "0")
                                End If

                                strMasterSQL = ""
                                strMasterSQL &= "INSERT INTO WORKPROCESS_MS VALUES ( "
                                '最大値を取得している場合はそちらを参照する
                                If strMax = "" Then
                                    strMasterSQL &= " '" & Row("作業工程NO") & "', "
                                Else
                                    strMasterSQL &= " '" & strMax & "', "
                                End If
                                strMasterSQL &= " '" & Row("工程NO") & "', "
                                strMasterSQL &= " '" & Row("製品NO") & "', "
                                strMasterSQL &= " '" & Row("作業工程名") & "', "
                                strMasterSQL &= " SYSDATETIME(), "
                                strMasterSQL &= " SYSDATETIME() "
                                strMasterSQL &= " ) "

                                '作業工程NOのチェック
                                If strMax = "" Then
                                    result = fUpdateCheck(1, intRowCount, Row("作業工程NO"))
                                Else
                                    result = fUpdateCheck(1, intRowCount, strMax)
                                End If

                                If result = False Then
                                    blnFalse = True
                                    intErrCount += 1
                                    Continue For
                                End If

                                '工程NOのチェック
                                If IsDBNull(Row("工程NO")) = False Then
                                    result = fUpdateCheck(2, intRowCount, Row("工程NO"))
                                Else
                                    result = fUpdateCheck(2, intRowCount, "")
                                End If
                                If result = False Then
                                    blnFalse = True
                                    intErrCount += 1
                                    Continue For
                                End If

                                '製品NOのチェック
                                If IsDBNull(Row("製品NO")) = False Then
                                    result = fUpdateCheck(3, intRowCount, Row("製品NO"))
                                Else
                                    result = fUpdateCheck(3, intRowCount, "")

                                End If
                                If result = False Then
                                    blnFalse = True
                                    intErrCount += 1
                                    Continue For
                                End If


                            Case DataRowState.Deleted
                                '削除の場合
                                strMasterSQL = ""
                                strMasterSQL &= "DELETE FROM WORKPROCESS_MS "
                                strMasterSQL &= "WHERE 作業工程NO ='" & Row("作業工程NO", DataRowVersion.Original) & "'"

                            Case DataRowState.Modified
                                '更新の場合
                                '工程NO、製品NOは０埋め（空白の場合は処理しない）
                                If Row("工程NO").ToString.Trim <> "" Then
                                    Row("工程NO") = Row("工程NO").PadLeft(3, "0")
                                End If
                                If Row("製品NO").ToString.Trim <> "" Then
                                    Row("製品NO") = Row("製品NO").PadLeft(3, "0")
                                End If

                                strMasterSQL = ""
                                strMasterSQL &= "UPDATE WORKPROCESS_MS "
                                strMasterSQL &= "SET "
                                strMasterSQL &= " 作業工程NO = '" & Row("作業工程NO") & "', "
                                strMasterSQL &= " 工程NO = '" & Row("工程NO") & "', "
                                strMasterSQL &= " 製品NO = '" & Row("製品NO") & "', "
                                strMasterSQL &= " 作業工程名 = '" & Row("作業工程名") & "', "
                                strMasterSQL &= " 更新日 = SYSDATETIME() "
                                strMasterSQL &= "WHERE "
                                strMasterSQL &= " 作業工程NO = '" & Row("作業工程NO", DataRowVersion.Original) & "' "

                                ''作業工程NOが変更されたか判定()
                                'If Row("作業工程NO") <> Row("作業工程NO", DataRowVersion.Original) Then
                                'result = fUpdateCheck(1, intRowCount, Row("作業工程NO"))

                                '作業工程NOが変更されたか判定
                                '変更された作業工程NOのみをチェック対象外とする。
                                '変更していないNOを対象とすると、DB登録済みとチェックし必ず重複となってしまうため。
                                If IsDBNull(Row("作業工程NO")) = False Then
                                    If Row("作業工程NO") <> Row("作業工程NO", DataRowVersion.Original) Then
                                        result = fUpdateCheck(1, intRowCount, Row("作業工程NO"))
                                    Else
                                        '変更されていない作業工程NOは問題なしとする
                                        result = True
                                    End If
                                Else
                                    result = fUpdateCheck(1, intRowCount, "")
                                End If
                                If result = False Then
                                    blnFalse = True
                                    intErrCount += 1
                                    Continue For
                                End If


                                ''作業工程NOの被りを判定
                                'strMasterSQL = ""
                                'strMasterSQL &= "SELECT "
                                'strMasterSQL &= " 作業工程NO "
                                'strMasterSQL &= "FROM "
                                'strMasterSQL &= " WORKPROCESS_MS "
                                'strMasterSQL &= "WHERE "
                                'strMasterSQL &= " 作業工程NO = '" & Row("作業工程NO") & "' "

                                ''SQLの実行
                                'cd.CommandText = strMasterSQL
                                'Dim count As Integer = CInt(cd.ExecuteScalar())

                                'If count > 0 Then
                                '    'MessageBox.Show("作業工程NOが重複しています。" & vbCrLf & "処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '    'T005.DataGridView1(0, dtTable.Rows.Count()).Style.BackColor = Color.Red
                                '    strErrText = "作業工程NOが重複しています。修正してください。"
                                '    intErrRow = intRowCount
                                '    sLogWrite(2, strErrText, strDt, intErrRow)
                                '    intErrCount += 1
                                '    Continue For

                                'End If

                                'End If

                                '工程NOのチェック(NULLだった場合は空白検索=必ずエラーとなる)
                                If IsDBNull(Row("工程NO")) = False Then
                                    result = fUpdateCheck(2, intRowCount, Row("工程NO"))
                                Else
                                    result = fUpdateCheck(2, intRowCount, "")
                                End If
                                If result = False Then
                                    blnFalse = True
                                    intErrCount += 1
                                    Continue For
                                End If

                                '製品NOのチェック
                                If IsDBNull(Row("製品NO")) = False Then
                                    result = fUpdateCheck(3, intRowCount, Row("製品NO"))
                                Else
                                    result = fUpdateCheck(3, intRowCount, "")

                                End If
                                If result = False Then
                                    blnFalse = True
                                    intErrCount += 1
                                    Continue For
                                End If

                            Case Else
                                Continue For

                        End Select

                        '成功した場合は、マスターSQLを実行
                        cd.CommandText = strMasterSQL
                        cd.Connection = Cn
                        cd.ExecuteNonQuery()
                        MessageBox.Show("取込完了しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

                    Next

                    If intErrCount > 0 Then
                        MessageBox.Show("エラーが" & intErrCount & "件ありました。" & vbCrLf & "ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    Cn.Close()
                    Cn.Dispose()

                    Return False


                Else
                    'キャンセルした場合
                    blnFalse = True
                    Return False

                End If







            Else
                MessageBox.Show("データが０件のため、更新できませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False

            End If

        Catch ex As Exception
            MessageBox.Show("例外処理が発生しました。" & vbCrLf & "ログを確認してください。", "例外発生")
            ClassName = "fUpdate"
            sLogWrite(1, ex.Message, ProgramName, ClassName)
            Return False
        Finally
            'エラーが有った場合は、初期化しない
            If blnFalse = False Then
                'DataGridViewの初期化
                frm.DataGridView1.DataSource = Nothing
                frm.DataGridView1.Columns.Clear()

            End If

        End Try


    End Function

    '---------------------------------------------
    '---CSV取込メイン処理                      ---
    '---------------------------------------------
    Public Function fInputMain() As Boolean

        Dim strMasterSQL As String '変更不可なマスタSQL(INSERT、UPDATE、DELETE)
        Dim Tran As SqlTransaction
        Dim dtReader As SqlDataReader
        Dim intRowCount As Integer = 0 'エラーチェック用
        Dim result As Boolean

        Try

            'トランザクション開始
            Tran = Cn.BeginTransaction

            '末尾の空白行は読み込まないように、-2にする
            For i = 0 To frm.DataGridView1.Rows.Count - 2
                intRowCount += 1

                '件数チェック用SQL
                strMasterSQL = ""
                strMasterSQL &= "SELECT "
                strMasterSQL &= " 作業工程NO "
                strMasterSQL &= "FROM "
                strMasterSQL &= " WORKPROCESS_MS "
                strMasterSQL &= "WHERE "
                strMasterSQL &= " 作業工程NO = '" + frm.DataGridView1(0, i).Value + "'"
                'SQLの実行
                cd.CommandText = strMasterSQL
                cd.Connection = Cn
                cd.Transaction = Tran
                dtReader = cd.ExecuteReader()

                '1件以上あった場合はUPDATE、0件だった場合はINSERT
                If dtReader.Read = True Then
                    'UPDATE処理
                    strMasterSQL = ""
                    strMasterSQL &= "UPDATE "
                    strMasterSQL &= " WORKPROCESS_MS "
                    strMasterSQL &= "SET "
                    strMasterSQL &= " 工程NO = '" + CStr(frm.DataGridView1(1, i).Value).Trim + "', "
                    strMasterSQL &= " 製品NO = '" + CStr(frm.DataGridView1(2, i).Value).Trim + "', "
                    strMasterSQL &= " 作業工程名 = '" + CStr(frm.DataGridView1(3, i).Value).Trim + "', "
                    strMasterSQL &= " 更新日 = SYSDATETIME() "
                    strMasterSQL &= "WHERE "
                    strMasterSQL &= " 作業工程NO = '" + frm.DataGridView1(0, i).Value + "'"


                Else
                    'INSERT処理
                    strMasterSQL = ""
                    strMasterSQL &= "INSERT INTO WORKPROCESS_MS VALUES "
                    strMasterSQL &= "( "
                    '空文字指定はNULLとする
                    If frm.DataGridView1(0, i).Value = "" Then
                        strMasterSQL &= " NULL,"
                    Else
                        strMasterSQL &= " '" + CStr(frm.DataGridView1(0, i).Value).Trim + "', "
                    End If

                    strMasterSQL &= " '" + CStr(frm.DataGridView1(1, i).Value).Trim + "', "
                    strMasterSQL &= " '" + CStr(frm.DataGridView1(2, i).Value).Trim + "', "
                    strMasterSQL &= " '" + CStr(frm.DataGridView1(3, i).Value).Trim + "', "
                    strMasterSQL &= " SYSDATETIME(), "
                    strMasterSQL &= " SYSDATETIME() "
                    strMasterSQL &= ") "

                End If

                '//値のチェック

                'dtReaderのクローズ
                dtReader.Close()

                '工程NOのチェック
                result = fUpdateCheck(2, intRowCount, CStr(frm.DataGridView1(1, i).Value).Trim)
                If result = False Then
                    Exit For
                End If

                '製品NOのチェック
                result = fUpdateCheck(3, intRowCount, CStr(frm.DataGridView1(2, i).Value).Trim)
                If result = False Then
                    Exit For
                End If

                'SQL実行
                cd.CommandText = strMasterSQL
                cd.Connection = Cn
                cd.Transaction = Tran
                cd.ExecuteNonQuery()
                result = True

            Next
            If result = True Then
                Tran.Commit()
                '全て成功した場合はCommit
                MessageBox.Show("CSV取込完了しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Return True
            Else
                Tran.Rollback()
                MessageBox.Show("エラーが発生しました。" & vbCrLf & "ログファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If


        Catch ex As Exception
            Tran.Rollback()

            MessageBox.Show("例外処理が発生しました。" & vbCrLf & "ログを確認してください。", "例外発生")
            ClassName = "fInput"
            sLogWrite(1, ex.Message, ProgramName, ClassName)
            Return False
        End Try

    End Function

    '---------------------------------------------
    '---CSV取込事前処理                        ---
    '---------------------------------------------
    Public Function fInputCheck() As Boolean

        Dim result As Boolean

        'ダイアログボックスの設定
        Dim FileDialog As New OpenFileDialog
        FileDialog.FileName = "OutputFile.csv"
        FileDialog.InitialDirectory = "C:\"
        FileDialog.Filter = "CSVファイル(*.csv)|*.csv"

        Try
            If FileDialog.ShowDialog() = DialogResult.OK Then
                'DataGridView初期化
                frm.DataGridView1.DataSource = Nothing
                frm.DataGridView1.Columns.Clear()


                '初期設定
                Dim SR As New StreamReader(FileDialog.FileName, System.Text.Encoding.GetEncoding("SHIFT-JIS"))
                Dim strLineBuf As String
                strLineBuf = SR.ReadLine
                Dim strColArr() As String = strLineBuf.Split(",")
                'ヘッダーの取得
                For Each buf In strColArr
                    frm.DataGridView1.Columns.Add(buf, buf)
                Next

                '値の取得
                Do
                    strLineBuf = SR.ReadLine()
                    If strLineBuf = Nothing Then Exit Do
                    Dim strRowArr() As String = strLineBuf.Split(",")
                    frm.DataGridView1.Rows.Add(strRowArr)

                Loop
                result = fInputMain()
                If result = True Then
                    Return True
                Else
                    Return False
                End If


            Else
                Return False

            End If

        Catch ex As Exception
            MessageBox.Show("例外処理が発生しました。" & vbCrLf & "ログを確認してください。", "例外発生")
            ClassName = "fInput"
            sLogWrite(1, ex.Message, ProgramName, ClassName)
            Return False
        Finally
            'DataGridViewの初期化
            frm.DataGridView1.DataSource = Nothing
            frm.DataGridView1.Columns.Clear()
        End Try



    End Function

    'Public Function fInput() As Boolean

    '    'ダイアログボックスの設定
    '    Dim FileDialog As New OpenFileDialog
    '    FileDialog.FileName = "OutputFile.csv"
    '    FileDialog.InitialDirectory = "C:\"
    '    FileDialog.Filter = "CSVファイル(*.csv)|*.csv"

    '    Try
    '        If FileDialog.ShowDialog() = DialogResult.OK Then
    '            Using parser As New TextFieldParser(FileDialog.FileName, Encoding.GetEncoding("Shift_JIS"))
    '                'カンマ区切りの指定
    '                parser.TextFieldType = FieldType.Delimited
    '                parser.SetDelimiters(",")
    '                'フィールドが引用符で囲まれているか
    '                parser.HasFieldsEnclosedInQuotes = False
    '                ' フィールドの空白トリム設定
    '                parser.TrimWhiteSpace = True

    '                'ファイルの末尾までループ
    '                While Not parser.EndOfData
    '                    Dim row As String() = parser.ReadFields()
    '                    For Each field As String In row
    '                        ' MsgBox(field)
    '                        T005.DataGridView1.Rows.Add(row)
    '                    Next


    '                End While

    '            End Using

    '        Else
    '            Return False

    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("例外処理が発生しました。" & vbCrLf & "ログを確認してください。", "例外発生")
    '        ClassName = "fInput"
    '        sLogWrite(1, ex.Message, ProgramName, ClassName)
    '        Return False

    '    End Try



    'End Function

    ''---------------------------------------------
    ''---CSV取込処理                            ---
    ''---------------------------------------------
    'Public Function _fInput() As Boolean

    '    'ダイアログボックスの設定
    '    Dim FileDialog As New OpenFileDialog
    '    FileDialog.FileName = "OutputFile.csv"
    '    FileDialog.InitialDirectory = "C:\"
    '    FileDialog.Filter = "CSVファイル(*.csv)|*.csv"

    '    Try
    '        'ダイアログを表示する
    '        If FileDialog.ShowDialog() = DialogResult.OK Then


    '            'BULK INSERTを実行する
    '            strMasterSQL = ""
    '            strMasterSQL &= "BULK INSERT WORKPROCESS_MS  "
    '            strMasterSQL &= "FROM '" + FileDialog.FileName + "'  "
    '            strMasterSQL &= "WITH "
    '            strMasterSQL &= "( "
    '            strMasterSQL &= " FIRSTROW = 2, "
    '            strMasterSQL &= " FIELDTERMINATOR  = ',', "
    '            strMasterSQL &= " CHECK_CONSTRAINTS, "
    '            strMasterSQL &= " FIRE_TRIGGERS "
    '            strMasterSQL &= ") "

    '            cd.CommandText = strMasterSQL
    '            cd.Connection = Cn
    '            cd.ExecuteNonQuery()
    '            'dtReader = cd.ExecuteReader()

    '            'CSV出力完了メッセージ
    '            MessageBox.Show("CSV取込完了しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

    '            Cn.Close()
    '            Cn.Dispose()
    '            'dtReader.Close()

    '            Return True


    '        Else
    '            Return False

    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("例外処理が発生しました。" & vbCrLf & "ログを確認してください。", "例外発生")
    '        ClassName = "fInput"
    '        sLogWrite(1, ex.Message, ProgramName, ClassName)
    '        Return False

    '    End Try

    'End Function

    '---------------------------------------------
    '---印刷プレビュー処理                     ---
    '---------------------------------------------
    Public Sub fPrint()

        'DataGridViewが０件の場合はエラー
        If frm.DataGridView1.Columns.Count <> 0 Then
            'EXCEL関連のオブジェクト
            Dim objExcel As Excel.Application = New Excel.Application
            Dim objWorkbook As Excel.Workbook = objExcel.Workbooks.Add

            '行列の最大値を取得
            Dim intColMax As Integer = frm.DataGridView1.Columns.Count - 1
            Dim intRowMAx As Integer = frm.DataGridView1.Rows.Count - 1

            '項目名格納用リストを宣言
            Dim lstCol As New List(Of String)
            'ヘッダー名を取得
            For i As Integer = 0 To (intColMax)
                lstCol.Add(frm.DataGridView1.Columns(i).HeaderCell.Value)
            Next

            'セルデータ取得用の配列
            Dim v As String(,) = New String(intRowMAx, intColMax) {}

            For row As Integer = 0 To intRowMAx
                For col As Integer = 0 To intColMax
                    If frm.DataGridView1.Rows(row).Cells(col).Value Is Nothing = False Then
                        v(row, col) = frm.DataGridView1.Rows(row).Cells(col).Value
                    End If
                Next
            Next

            'EXCELに項目転送
            For i As Integer = 1 To frm.DataGridView1.Columns.Count
                'シート１行目にヘッダー挿入
                objWorkbook.Sheets(1).cells(1, i) = lstCol(i - 1)
                '罫線挿入
                objWorkbook.Sheets(1).cells(1, i).Borders.Linestyle = True
                ' 項目の表示行に背景色を設定
                objWorkbook.Sheets(1).Cells(1, i).Interior.Color = RGB(140, 140, 140)
                ' 文字のフォントを設定
                objWorkbook.Sheets(1).Cells(1, i).Font.Color = RGB(255, 255, 255)
                objWorkbook.Sheets(1).Cells(1, i).Font.Bold = True

            Next

            ' EXCELにデータを範囲指定で転送
            Dim strRange As String = "A2:" & Chr(Asc("A") + intColMax) & frm.DataGridView1.Rows.Count
            objWorkbook.Sheets(1).Range(strRange) = v
            ' データの表示範囲に罫線を設定
            objWorkbook.Sheets(1).Range(strRange).Borders.LineStyle = True
            '横幅の調整
            objExcel.Sheets(1).Range("D:F").ColumnWidth = 20


            '印刷の場合
            ' エクセル表示
            objExcel.Visible = True
            objExcel.DisplayAlerts = False

            objWorkbook.Sheets(1).PrintPreview()

            objWorkbook.Close()
            objExcel.Quit()

            objWorkbook = Nothing
            objExcel = Nothing


        Else
            MessageBox.Show("検索結果が０件なので保存できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If

    End Sub


    '---------------------------------------------
    '---更新前チェック書き込み             　　---
    '---CheckNo：                            　---
    '---1=Update:作業工程NO重複チェック        ---
    '---2=Update:工程NO存在チェック            ---
    '---3=Update:製品NO存在チェック            ---
    '---------------------------------------------
    Public Function fUpdateCheck(intCheckNo As Integer, intRowCount As Integer, strRowValue As String) As Boolean

        Dim strDt As String = Now.ToString("yyyyMMddHHmmss")        'エラーログファイル名（重複を避けるため、For文前で作成）
        Dim strErrText As String          'エラー内容
        Dim intErrRow As Integer          'エラーの起きた行番号

        If intCheckNo = 1 Then
            '作業工程NOの被りを判定
            strMasterSQL = ""
            strMasterSQL &= "SELECT "
            strMasterSQL &= " 作業工程NO "
            strMasterSQL &= "FROM "
            strMasterSQL &= " WORKPROCESS_MS "
            strMasterSQL &= "WHERE "
            strMasterSQL &= " 作業工程NO = '" & strRowValue & "' "
            'strMasterSQL &= " 作業工程NO = '" & Row("作業工程NO") & "' "
            'SQLの実行
            cd.CommandText = strMasterSQL
            Dim count As Integer = CInt(cd.ExecuteScalar())

            '重複していた場合はエラーを返す
            If count > 0 Then
                strErrText = "作業工程NOが重複しています。修正してください。"
                intErrRow = intRowCount
                'エラーログ書き込み処理を呼び出す
                sLogWrite(2, strErrText, strDt, intErrRow)
                Return False
            Else
                Return True
            End If

        ElseIf intCheckNo = 2 Then
            strMasterSQL = ""
            strMasterSQL &= "SELECT "
            strMasterSQL &= " 工程NO "
            strMasterSQL &= "FROM "
            strMasterSQL &= " PROCESS_MS "
            strMasterSQL &= "WHERE "
            strMasterSQL &= " 工程NO = '" & strRowValue & "' "
            'SQLの実行
            cd.CommandText = strMasterSQL
            Dim count As Integer = CInt(cd.ExecuteScalar())

            '存在しない場合はエラーを返す
            If count = 0 Then
                strErrText = "マスタに存在しない工程NOが入力されています。修正してください。"
                intErrRow = intRowCount
                'エラーログ書き込み処理を呼び出す
                sLogWrite(2, strErrText, strDt, intErrRow)
                Return False
            Else
                Return True
            End If

        ElseIf intCheckNo = 3 Then
            strMasterSQL = ""
            strMasterSQL &= "SELECT "
            strMasterSQL &= " 製品NO "
            strMasterSQL &= "FROM "
            strMasterSQL &= " ITEM_MS "
            strMasterSQL &= "WHERE "
            strMasterSQL &= " 製品NO = '" & strRowValue & "' "

            'SQLの実行
            cd.CommandText = strMasterSQL
            Dim count As Integer = CInt(cd.ExecuteScalar())

            '存在しない場合はエラーを返す
            If count = 0 Then
                strErrText = "マスタに存在しない製品NOが入力されています。修正してください。"
                intErrRow = intRowCount
                'エラーログ書き込み処理を呼び出す
                sLogWrite(2, strErrText, strDt, intErrRow)
                Return False
            Else
                Return True
            End If

        Else
            'ありえない場合
            Return False
        End If


    End Function

    '---------------------------------------------
    '---ログ書き込み                           ---
    '---LogNo：                                ---
    '---1=CSV出力エラーログ                　　---
    '---2=UPDATE処理エラーログ             　　---
    '---------------------------------------------
    Public Sub sLogWrite(LogNo As String, strErrMessage As String, strName1 As String, strName2 As String)

        '保存先のファイルディレクトリ
        Dim strLogFile As String

        If LogNo = 1 Then
            'CSVエラー出力エラーログ
            '現在時刻を取得
            Dim dt As DateTime = Now
            strLogFile = "C:\Logs\"

            Dim sw As New System.IO.StreamWriter(strLogFile & strName1 & ".log", True, System.Text.Encoding.GetEncoding("Shift_JIS"))

            'ログファイルに書き込む
            sw.Write("Error：[" & strName1 & "] [" & strName2 & "] [" & dt & "] [" & strErrMessage & "] ")
            sw.Write(vbCrLf)
            'ログファイルを閉じる
            sw.Close()

        ElseIf LogNo = 2 Then
            'UPDATE処理エラーログ

            strLogFile = CStr(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal))

            Dim sw As New System.IO.StreamWriter(strLogFile & "\" & strName1 & ".log", True, System.Text.Encoding.GetEncoding("Shift_JIS"))

            'ログファイルに書き込む
            'sw.Write("Error：[" & strName1 & "] [" & strName2 & "] [" & strName1 & "] [" & strErrMessage & "] ")
            sw.Write("Error:[" & strName2 & "]行目 " & strErrMessage & " ")
            sw.Write(vbCrLf)
            'ログファイルを閉じる
            sw.Close()

        End If




    End Sub

    '---------------------------------------------
    '---DataGridView初期設定処理               ---
    '---------------------------------------------
    Public Sub sDataGridViewSetting()


        '作業工程NO、更新日、登録日は編集不可
        'T005.DataGridView1.Columns(0).ReadOnly = True
        frm.DataGridView1.Columns(4).ReadOnly = True
        frm.DataGridView1.Columns(5).ReadOnly = True
        '桁数制限
        DirectCast(frm.DataGridView1.Columns(1), DataGridViewTextBoxColumn).MaxInputLength = 3
        DirectCast(frm.DataGridView1.Columns(2), DataGridViewTextBoxColumn).MaxInputLength = 3
        DirectCast(frm.DataGridView1.Columns(3), DataGridViewTextBoxColumn).MaxInputLength = 20

    End Sub

    '---------------------------------------------
    '---CSV出力　ダブルクォートで囲う処理      ---
    '---------------------------------------------
    Private Function fEncloseDoubleQuotes(field As String) As String
        If field.IndexOf(""""c) > -1 Then
            '"を""とする
            field = field.Replace("""", """""")
        End If
        Return """" & field & """"
    End Function

End Class
