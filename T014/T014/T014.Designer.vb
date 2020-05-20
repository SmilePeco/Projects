<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T014
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rboShipment02 = New System.Windows.Forms.RadioButton()
        Me.rboShipment04 = New System.Windows.Forms.RadioButton()
        Me.rboShipment01 = New System.Windows.Forms.RadioButton()
        Me.rboShipment03 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dtpShipmentDateTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpShipmentDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblShipmentMSName = New System.Windows.Forms.Label()
        Me.btnShipmentSearch = New System.Windows.Forms.Button()
        Me.txtShipmentMSNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtOrderNO = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtShipmentNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.btnUserIDSearch = New System.Windows.Forms.Button()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rboShipment02)
        Me.GroupBox1.Controls.Add(Me.rboShipment04)
        Me.GroupBox1.Controls.Add(Me.rboShipment01)
        Me.GroupBox1.Controls.Add(Me.rboShipment03)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(677, 53)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'rboShipment02
        '
        Me.rboShipment02.AutoSize = True
        Me.rboShipment02.Location = New System.Drawing.Point(159, 19)
        Me.rboShipment02.Name = "rboShipment02"
        Me.rboShipment02.Size = New System.Drawing.Size(71, 16)
        Me.rboShipment02.TabIndex = 1
        Me.rboShipment02.TabStop = True
        Me.rboShipment02.Text = "受注番号"
        Me.rboShipment02.UseVisualStyleBackColor = True
        '
        'rboShipment04
        '
        Me.rboShipment04.AutoSize = True
        Me.rboShipment04.Location = New System.Drawing.Point(389, 19)
        Me.rboShipment04.Name = "rboShipment04"
        Me.rboShipment04.Size = New System.Drawing.Size(66, 16)
        Me.rboShipment04.TabIndex = 3
        Me.rboShipment04.TabStop = True
        Me.rboShipment04.Text = "選択なし"
        Me.rboShipment04.UseVisualStyleBackColor = True
        '
        'rboShipment01
        '
        Me.rboShipment01.AutoSize = True
        Me.rboShipment01.Location = New System.Drawing.Point(12, 19)
        Me.rboShipment01.Name = "rboShipment01"
        Me.rboShipment01.Size = New System.Drawing.Size(119, 16)
        Me.rboShipment01.TabIndex = 0
        Me.rboShipment01.TabStop = True
        Me.rboShipment01.Text = "出荷先NO、出荷日"
        Me.rboShipment01.UseVisualStyleBackColor = True
        '
        'rboShipment03
        '
        Me.rboShipment03.AutoSize = True
        Me.rboShipment03.Location = New System.Drawing.Point(267, 19)
        Me.rboShipment03.Name = "rboShipment03"
        Me.rboShipment03.Size = New System.Drawing.Size(71, 16)
        Me.rboShipment03.TabIndex = 2
        Me.rboShipment03.TabStop = True
        Me.rboShipment03.Text = "出荷番号"
        Me.rboShipment03.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpShipmentDateTo)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.dtpShipmentDateFrom)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.lblShipmentMSName)
        Me.GroupBox2.Controls.Add(Me.btnShipmentSearch)
        Me.GroupBox2.Controls.Add(Me.txtShipmentMSNo)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 53)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(677, 57)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'dtpShipmentDateTo
        '
        Me.dtpShipmentDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpShipmentDateTo.Location = New System.Drawing.Point(488, 21)
        Me.dtpShipmentDateTo.Name = "dtpShipmentDateTo"
        Me.dtpShipmentDateTo.Size = New System.Drawing.Size(130, 19)
        Me.dtpShipmentDateTo.TabIndex = 26
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(460, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 15)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "～"
        '
        'dtpShipmentDateFrom
        '
        Me.dtpShipmentDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpShipmentDateFrom.Location = New System.Drawing.Point(327, 21)
        Me.dtpShipmentDateFrom.Name = "dtpShipmentDateFrom"
        Me.dtpShipmentDateFrom.Size = New System.Drawing.Size(130, 19)
        Me.dtpShipmentDateFrom.TabIndex = 24
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(249, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "出荷日"
        '
        'lblShipmentMSName
        '
        Me.lblShipmentMSName.AutoSize = True
        Me.lblShipmentMSName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShipmentMSName.Location = New System.Drawing.Point(156, 25)
        Me.lblShipmentMSName.Name = "lblShipmentMSName"
        Me.lblShipmentMSName.Size = New System.Drawing.Size(59, 13)
        Me.lblShipmentMSName.TabIndex = 22
        Me.lblShipmentMSName.Text = "出荷先名"
        '
        'btnShipmentSearch
        '
        Me.btnShipmentSearch.Location = New System.Drawing.Point(131, 19)
        Me.btnShipmentSearch.Name = "btnShipmentSearch"
        Me.btnShipmentSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnShipmentSearch.TabIndex = 21
        Me.btnShipmentSearch.Text = "..."
        Me.btnShipmentSearch.UseVisualStyleBackColor = True
        '
        'txtShipmentMSNo
        '
        Me.txtShipmentMSNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShipmentMSNo.Location = New System.Drawing.Point(90, 21)
        Me.txtShipmentMSNo.MaxLength = 3
        Me.txtShipmentMSNo.Name = "txtShipmentMSNo"
        Me.txtShipmentMSNo.Size = New System.Drawing.Size(41, 19)
        Me.txtShipmentMSNo.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 12)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "出荷先NO"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtOrderNO)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 110)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(677, 39)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'txtOrderNO
        '
        Me.txtOrderNO.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtOrderNO.Location = New System.Drawing.Point(94, 12)
        Me.txtOrderNO.MaxLength = 3
        Me.txtOrderNO.Name = "txtOrderNO"
        Me.txtOrderNO.Size = New System.Drawing.Size(41, 19)
        Me.txtOrderNO.TabIndex = 24
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "受注番号"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtShipmentNo)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(0, 149)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(677, 39)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        '
        'txtShipmentNo
        '
        Me.txtShipmentNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShipmentNo.Location = New System.Drawing.Point(94, 12)
        Me.txtShipmentNo.MaxLength = 3
        Me.txtShipmentNo.Name = "txtShipmentNo"
        Me.txtShipmentNo.Size = New System.Drawing.Size(41, 19)
        Me.txtShipmentNo.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "出荷番号"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.GroupBox7)
        Me.GroupBox5.Controls.Add(Me.GroupBox6)
        Me.GroupBox5.Controls.Add(Me.DataGridView1)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(0, 188)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(677, 475)
        Me.GroupBox5.TabIndex = 7
        Me.GroupBox5.TabStop = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.btnUserIDSearch)
        Me.GroupBox7.Controls.Add(Me.txtUserID)
        Me.GroupBox7.Controls.Add(Me.Label6)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox7.Location = New System.Drawing.Point(3, 370)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(671, 52)
        Me.GroupBox7.TabIndex = 2
        Me.GroupBox7.TabStop = False
        '
        'btnUserIDSearch
        '
        Me.btnUserIDSearch.Location = New System.Drawing.Point(184, 19)
        Me.btnUserIDSearch.Name = "btnUserIDSearch"
        Me.btnUserIDSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnUserIDSearch.TabIndex = 17
        Me.btnUserIDSearch.Text = "..."
        Me.btnUserIDSearch.UseVisualStyleBackColor = True
        '
        'txtUserID
        '
        Me.txtUserID.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUserID.Location = New System.Drawing.Point(89, 21)
        Me.txtUserID.MaxLength = 10
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(95, 19)
        Me.txtUserID.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "更新担当者"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnEnd)
        Me.GroupBox6.Controls.Add(Me.btnClear)
        Me.GroupBox6.Controls.Add(Me.btnUpdate)
        Me.GroupBox6.Controls.Add(Me.btnSearch)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox6.Location = New System.Drawing.Point(3, 422)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(671, 50)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnd.Location = New System.Drawing.Point(577, 18)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnd.TabIndex = 3
        Me.btnEnd.Text = "F4:終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(485, 18)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "F3:クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(109, 18)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "F2:更新"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(9, 18)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 0
        Me.btnSearch.Text = "F1:検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 15)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(671, 457)
        Me.DataGridView1.TabIndex = 0
        '
        'T014
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(677, 663)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "T014"
        Me.Text = "T014_出荷チェック"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rboShipment04 As System.Windows.Forms.RadioButton
    Friend WithEvents rboShipment01 As System.Windows.Forms.RadioButton
    Friend WithEvents rboShipment03 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpShipmentDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpShipmentDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblShipmentMSName As System.Windows.Forms.Label
    Friend WithEvents btnShipmentSearch As System.Windows.Forms.Button
    Friend WithEvents txtShipmentMSNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOrderNO As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rboShipment02 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtShipmentNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUserIDSearch As System.Windows.Forms.Button
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button

End Class
