<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T007
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
        Me.lblOrderMSName = New System.Windows.Forms.Label()
        Me.dtpOrderDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnOrderSearch = New System.Windows.Forms.Button()
        Me.txtOrderMSNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblWorkProcessName = New System.Windows.Forms.Label()
        Me.cmdWorkProcessSearch = New System.Windows.Forms.Button()
        Me.txtWorkProcessNO = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtOrderAmount = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnUserIDSearch = New System.Windows.Forms.Button()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblOrderMSName)
        Me.GroupBox1.Controls.Add(Me.dtpOrderDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnOrderSearch)
        Me.GroupBox1.Controls.Add(Me.txtOrderMSNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(576, 44)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblOrderMSName
        '
        Me.lblOrderMSName.AutoSize = True
        Me.lblOrderMSName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrderMSName.Location = New System.Drawing.Point(158, 16)
        Me.lblOrderMSName.Name = "lblOrderMSName"
        Me.lblOrderMSName.Size = New System.Drawing.Size(59, 13)
        Me.lblOrderMSName.TabIndex = 6
        Me.lblOrderMSName.Text = "受注先名"
        '
        'dtpOrderDate
        '
        Me.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrderDate.Location = New System.Drawing.Point(376, 12)
        Me.dtpOrderDate.Name = "dtpOrderDate"
        Me.dtpOrderDate.Size = New System.Drawing.Size(130, 19)
        Me.dtpOrderDate.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(335, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "受注日"
        '
        'btnOrderSearch
        '
        Me.btnOrderSearch.Location = New System.Drawing.Point(133, 10)
        Me.btnOrderSearch.Name = "btnOrderSearch"
        Me.btnOrderSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnOrderSearch.TabIndex = 2
        Me.btnOrderSearch.Text = "..."
        Me.btnOrderSearch.UseVisualStyleBackColor = True
        '
        'txtOrderMSNo
        '
        Me.txtOrderMSNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtOrderMSNo.Location = New System.Drawing.Point(92, 12)
        Me.txtOrderMSNo.MaxLength = 3
        Me.txtOrderMSNo.Name = "txtOrderMSNo"
        Me.txtOrderMSNo.Size = New System.Drawing.Size(41, 19)
        Me.txtOrderMSNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "受注先NO"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblWorkProcessName)
        Me.GroupBox2.Controls.Add(Me.cmdWorkProcessSearch)
        Me.GroupBox2.Controls.Add(Me.txtWorkProcessNO)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 44)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(576, 54)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'lblWorkProcessName
        '
        Me.lblWorkProcessName.AutoSize = True
        Me.lblWorkProcessName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWorkProcessName.Location = New System.Drawing.Point(158, 24)
        Me.lblWorkProcessName.Name = "lblWorkProcessName"
        Me.lblWorkProcessName.Size = New System.Drawing.Size(59, 13)
        Me.lblWorkProcessName.TabIndex = 10
        Me.lblWorkProcessName.Text = "受注先名"
        '
        'cmdWorkProcessSearch
        '
        Me.cmdWorkProcessSearch.Location = New System.Drawing.Point(133, 18)
        Me.cmdWorkProcessSearch.Name = "cmdWorkProcessSearch"
        Me.cmdWorkProcessSearch.Size = New System.Drawing.Size(24, 23)
        Me.cmdWorkProcessSearch.TabIndex = 9
        Me.cmdWorkProcessSearch.Text = "..."
        Me.cmdWorkProcessSearch.UseVisualStyleBackColor = True
        '
        'txtWorkProcessNO
        '
        Me.txtWorkProcessNO.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWorkProcessNO.Location = New System.Drawing.Point(92, 20)
        Me.txtWorkProcessNO.MaxLength = 3
        Me.txtWorkProcessNO.Name = "txtWorkProcessNO"
        Me.txtWorkProcessNO.Size = New System.Drawing.Size(41, 19)
        Me.txtWorkProcessNO.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "作業工程NO"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "受注数"
        '
        'txtOrderAmount
        '
        Me.txtOrderAmount.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtOrderAmount.Location = New System.Drawing.Point(92, 22)
        Me.txtOrderAmount.MaxLength = 6
        Me.txtOrderAmount.Name = "txtOrderAmount"
        Me.txtOrderAmount.Size = New System.Drawing.Size(65, 19)
        Me.txtOrderAmount.TabIndex = 12
        Me.txtOrderAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnUserIDSearch)
        Me.GroupBox3.Controls.Add(Me.txtUserID)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 98)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(576, 51)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'btnUserIDSearch
        '
        Me.btnUserIDSearch.Location = New System.Drawing.Point(187, 19)
        Me.btnUserIDSearch.Name = "btnUserIDSearch"
        Me.btnUserIDSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnUserIDSearch.TabIndex = 11
        Me.btnUserIDSearch.Text = "..."
        Me.btnUserIDSearch.UseVisualStyleBackColor = True
        '
        'txtUserID
        '
        Me.txtUserID.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUserID.Location = New System.Drawing.Point(92, 21)
        Me.txtUserID.MaxLength = 10
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(95, 19)
        Me.txtUserID.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "更新担当者"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(14, 18)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 13
        Me.btnSubmit.Text = "F1:登録"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtOrderAmount)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(0, 149)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(576, 55)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnEnd)
        Me.GroupBox5.Controls.Add(Me.btnClear)
        Me.GroupBox5.Controls.Add(Me.btnSubmit)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox5.Location = New System.Drawing.Point(0, 210)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(576, 52)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(401, 18)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 14
        Me.btnClear.Text = "F2:クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnd.Location = New System.Drawing.Point(495, 18)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnd.TabIndex = 15
        Me.btnEnd.Text = "F3:終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'T007
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 262)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "T007"
        Me.Text = "T007_受注登録"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpOrderDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOrderSearch As System.Windows.Forms.Button
    Friend WithEvents txtOrderMSNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblOrderMSName As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblWorkProcessName As System.Windows.Forms.Label
    Friend WithEvents cmdWorkProcessSearch As System.Windows.Forms.Button
    Friend WithEvents txtWorkProcessNO As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtOrderAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUserIDSearch As System.Windows.Forms.Button
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button

End Class
