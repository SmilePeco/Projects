<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T010
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
        Me.rboSearch3 = New System.Windows.Forms.RadioButton()
        Me.rboSearch2 = New System.Windows.Forms.RadioButton()
        Me.rboSearch1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dtpOrderDateTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpOrderDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblOrderMSName = New System.Windows.Forms.Label()
        Me.btnOrderSearch = New System.Windows.Forms.Button()
        Me.txtOrderMSNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
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
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rboSearch3)
        Me.GroupBox1.Controls.Add(Me.rboSearch2)
        Me.GroupBox1.Controls.Add(Me.rboSearch1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(682, 47)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'rboSearch3
        '
        Me.rboSearch3.AutoSize = True
        Me.rboSearch3.Location = New System.Drawing.Point(299, 20)
        Me.rboSearch3.Name = "rboSearch3"
        Me.rboSearch3.Size = New System.Drawing.Size(71, 16)
        Me.rboSearch3.TabIndex = 2
        Me.rboSearch3.TabStop = True
        Me.rboSearch3.Text = "未選択用"
        Me.rboSearch3.UseVisualStyleBackColor = True
        '
        'rboSearch2
        '
        Me.rboSearch2.AutoSize = True
        Me.rboSearch2.Location = New System.Drawing.Point(179, 20)
        Me.rboSearch2.Name = "rboSearch2"
        Me.rboSearch2.Size = New System.Drawing.Size(71, 16)
        Me.rboSearch2.TabIndex = 1
        Me.rboSearch2.TabStop = True
        Me.rboSearch2.Text = "受注番号"
        Me.rboSearch2.UseVisualStyleBackColor = True
        '
        'rboSearch1
        '
        Me.rboSearch1.AutoSize = True
        Me.rboSearch1.Location = New System.Drawing.Point(12, 20)
        Me.rboSearch1.Name = "rboSearch1"
        Me.rboSearch1.Size = New System.Drawing.Size(107, 16)
        Me.rboSearch1.TabIndex = 0
        Me.rboSearch1.TabStop = True
        Me.rboSearch1.Text = "受注NO、受注日"
        Me.rboSearch1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpOrderDateTo)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.dtpOrderDateFrom)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.lblOrderMSName)
        Me.GroupBox2.Controls.Add(Me.btnOrderSearch)
        Me.GroupBox2.Controls.Add(Me.txtOrderMSNo)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 47)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(682, 52)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'dtpOrderDateTo
        '
        Me.dtpOrderDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrderDateTo.Location = New System.Drawing.Point(488, 18)
        Me.dtpOrderDateTo.Name = "dtpOrderDateTo"
        Me.dtpOrderDateTo.Size = New System.Drawing.Size(130, 19)
        Me.dtpOrderDateTo.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(460, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 15)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "～"
        '
        'dtpOrderDateFrom
        '
        Me.dtpOrderDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrderDateFrom.Location = New System.Drawing.Point(327, 18)
        Me.dtpOrderDateFrom.Name = "dtpOrderDateFrom"
        Me.dtpOrderDateFrom.Size = New System.Drawing.Size(130, 19)
        Me.dtpOrderDateFrom.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(249, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "受注日"
        '
        'lblOrderMSName
        '
        Me.lblOrderMSName.AutoSize = True
        Me.lblOrderMSName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrderMSName.Location = New System.Drawing.Point(156, 22)
        Me.lblOrderMSName.Name = "lblOrderMSName"
        Me.lblOrderMSName.Size = New System.Drawing.Size(59, 13)
        Me.lblOrderMSName.TabIndex = 14
        Me.lblOrderMSName.Text = "受注先名"
        '
        'btnOrderSearch
        '
        Me.btnOrderSearch.Location = New System.Drawing.Point(131, 16)
        Me.btnOrderSearch.Name = "btnOrderSearch"
        Me.btnOrderSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnOrderSearch.TabIndex = 13
        Me.btnOrderSearch.Text = "..."
        Me.btnOrderSearch.UseVisualStyleBackColor = True
        '
        'txtOrderMSNo
        '
        Me.txtOrderMSNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtOrderMSNo.Location = New System.Drawing.Point(90, 18)
        Me.txtOrderMSNo.MaxLength = 3
        Me.txtOrderMSNo.Name = "txtOrderMSNo"
        Me.txtOrderMSNo.Size = New System.Drawing.Size(41, 19)
        Me.txtOrderMSNo.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 12)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "受注先NO"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtOrderNo)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 99)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(682, 46)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'txtOrderNo
        '
        Me.txtOrderNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtOrderNo.Location = New System.Drawing.Point(90, 16)
        Me.txtOrderNo.MaxLength = 3
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(41, 19)
        Me.txtOrderNo.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "受注番号"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.DataGridView1)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 145)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(682, 235)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnEnd)
        Me.GroupBox5.Controls.Add(Me.btnClear)
        Me.GroupBox5.Controls.Add(Me.btnUpdate)
        Me.GroupBox5.Controls.Add(Me.btnSearch)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox5.Location = New System.Drawing.Point(3, 184)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(676, 48)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnd.Location = New System.Drawing.Point(583, 19)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnd.TabIndex = 4
        Me.btnEnd.Text = "F4:終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(485, 19)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "F3:クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(111, 19)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Text = "F2:更新"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(10, 19)
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
        Me.DataGridView1.Size = New System.Drawing.Size(676, 217)
        Me.DataGridView1.TabIndex = 0
        '
        'T010
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(682, 380)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "T010"
        Me.Text = "T010_受注チェック"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rboSearch2 As System.Windows.Forms.RadioButton
    Friend WithEvents rboSearch1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpOrderDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpOrderDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblOrderMSName As System.Windows.Forms.Label
    Friend WithEvents btnOrderSearch As System.Windows.Forms.Button
    Friend WithEvents txtOrderMSNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rboSearch3 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button

End Class
