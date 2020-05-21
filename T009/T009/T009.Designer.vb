<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T009
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
        Me.chkClosedStock = New System.Windows.Forms.CheckBox()
        Me.lblOrderMSName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOrderSearch = New System.Windows.Forms.Button()
        Me.txtOrderMSNo = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblWorkProcessName = New System.Windows.Forms.Label()
        Me.cmdWorkProcessSearch = New System.Windows.Forms.Button()
        Me.txtWorkProcessNO = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnUserIDSearch = New System.Windows.Forms.Button()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.dtpCreateTime = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtOrderAmount = New System.Windows.Forms.TextBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblItemNO = New System.Windows.Forms.Label()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkClosedStock)
        Me.GroupBox1.Controls.Add(Me.lblOrderMSName)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnOrderSearch)
        Me.GroupBox1.Controls.Add(Me.txtOrderMSNo)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(641, 58)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkClosedStock
        '
        Me.chkClosedStock.AutoSize = True
        Me.chkClosedStock.Location = New System.Drawing.Point(267, 24)
        Me.chkClosedStock.Name = "chkClosedStock"
        Me.chkClosedStock.Size = New System.Drawing.Size(122, 16)
        Me.chkClosedStock.TabIndex = 2
        Me.chkClosedStock.Text = "臨時在庫として登録"
        Me.chkClosedStock.UseVisualStyleBackColor = True
        '
        'lblOrderMSName
        '
        Me.lblOrderMSName.AutoSize = True
        Me.lblOrderMSName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrderMSName.Location = New System.Drawing.Point(158, 25)
        Me.lblOrderMSName.Name = "lblOrderMSName"
        Me.lblOrderMSName.Size = New System.Drawing.Size(59, 13)
        Me.lblOrderMSName.TabIndex = 18
        Me.lblOrderMSName.Text = "出荷先名"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 12)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "出荷先NO"
        '
        'btnOrderSearch
        '
        Me.btnOrderSearch.Location = New System.Drawing.Point(133, 19)
        Me.btnOrderSearch.Name = "btnOrderSearch"
        Me.btnOrderSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnOrderSearch.TabIndex = 17
        Me.btnOrderSearch.Text = "..."
        Me.btnOrderSearch.UseVisualStyleBackColor = True
        '
        'txtOrderMSNo
        '
        Me.txtOrderMSNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtOrderMSNo.Location = New System.Drawing.Point(92, 21)
        Me.txtOrderMSNo.MaxLength = 3
        Me.txtOrderMSNo.Name = "txtOrderMSNo"
        Me.txtOrderMSNo.Size = New System.Drawing.Size(41, 19)
        Me.txtOrderMSNo.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.lblWorkProcessName)
        Me.GroupBox2.Controls.Add(Me.cmdWorkProcessSearch)
        Me.GroupBox2.Controls.Add(Me.txtWorkProcessNO)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 58)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(641, 110)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'lblWorkProcessName
        '
        Me.lblWorkProcessName.AutoSize = True
        Me.lblWorkProcessName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWorkProcessName.Location = New System.Drawing.Point(158, 39)
        Me.lblWorkProcessName.Name = "lblWorkProcessName"
        Me.lblWorkProcessName.Size = New System.Drawing.Size(72, 13)
        Me.lblWorkProcessName.TabIndex = 14
        Me.lblWorkProcessName.Text = "作業工程名"
        '
        'cmdWorkProcessSearch
        '
        Me.cmdWorkProcessSearch.Location = New System.Drawing.Point(133, 33)
        Me.cmdWorkProcessSearch.Name = "cmdWorkProcessSearch"
        Me.cmdWorkProcessSearch.Size = New System.Drawing.Size(24, 23)
        Me.cmdWorkProcessSearch.TabIndex = 13
        Me.cmdWorkProcessSearch.Text = "..."
        Me.cmdWorkProcessSearch.UseVisualStyleBackColor = True
        '
        'txtWorkProcessNO
        '
        Me.txtWorkProcessNO.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWorkProcessNO.Location = New System.Drawing.Point(92, 35)
        Me.txtWorkProcessNO.MaxLength = 3
        Me.txtWorkProcessNO.Name = "txtWorkProcessNO"
        Me.txtWorkProcessNO.Size = New System.Drawing.Size(41, 19)
        Me.txtWorkProcessNO.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 12)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "作業工程NO"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnUserIDSearch)
        Me.GroupBox3.Controls.Add(Me.txtUserID)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 168)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(641, 62)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'btnUserIDSearch
        '
        Me.btnUserIDSearch.Location = New System.Drawing.Point(187, 20)
        Me.btnUserIDSearch.Name = "btnUserIDSearch"
        Me.btnUserIDSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnUserIDSearch.TabIndex = 5
        Me.btnUserIDSearch.Text = "..."
        Me.btnUserIDSearch.UseVisualStyleBackColor = True
        '
        'txtUserID
        '
        Me.txtUserID.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUserID.Location = New System.Drawing.Point(92, 22)
        Me.txtUserID.MaxLength = 10
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(95, 19)
        Me.txtUserID.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "更新担当者"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.dtpCreateTime)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtOrderAmount)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(0, 230)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(641, 55)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        '
        'dtpCreateTime
        '
        Me.dtpCreateTime.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpCreateTime.Location = New System.Drawing.Point(309, 12)
        Me.dtpCreateTime.Name = "dtpCreateTime"
        Me.dtpCreateTime.Size = New System.Drawing.Size(200, 19)
        Me.dtpCreateTime.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(221, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "生産予定日"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "生産数"
        '
        'txtOrderAmount
        '
        Me.txtOrderAmount.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtOrderAmount.Location = New System.Drawing.Point(92, 12)
        Me.txtOrderAmount.MaxLength = 6
        Me.txtOrderAmount.Name = "txtOrderAmount"
        Me.txtOrderAmount.Size = New System.Drawing.Size(65, 19)
        Me.txtOrderAmount.TabIndex = 6
        Me.txtOrderAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnSubmit
        '
        Me.btnSubmit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSubmit.Location = New System.Drawing.Point(525, 18)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 8
        Me.btnSubmit.Text = "登録"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnSubmit)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.Location = New System.Drawing.Point(0, 285)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(641, 55)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblItemName)
        Me.GroupBox6.Controls.Add(Me.lblItemNO)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox6.Location = New System.Drawing.Point(267, 15)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(371, 92)
        Me.GroupBox6.TabIndex = 15
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "製品情報"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "製品NO："
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "製品名："
        '
        'lblItemNO
        '
        Me.lblItemNO.AutoSize = True
        Me.lblItemNO.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblItemNO.Location = New System.Drawing.Point(69, 22)
        Me.lblItemNO.Name = "lblItemNO"
        Me.lblItemNO.Size = New System.Drawing.Size(61, 13)
        Me.lblItemNO.TabIndex = 19
        Me.lblItemNO.Text = "lblItemNO"
        '
        'lblItemName
        '
        Me.lblItemName.AutoSize = True
        Me.lblItemName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblItemName.Location = New System.Drawing.Point(69, 57)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(75, 13)
        Me.lblItemName.TabIndex = 20
        Me.lblItemName.Text = "lblItemName"
        '
        'T009
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 330)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "T009"
        Me.Text = "T009_生産登録"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblOrderMSName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOrderSearch As System.Windows.Forms.Button
    Friend WithEvents txtOrderMSNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblWorkProcessName As System.Windows.Forms.Label
    Friend WithEvents cmdWorkProcessSearch As System.Windows.Forms.Button
    Friend WithEvents txtWorkProcessNO As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUserIDSearch As System.Windows.Forms.Button
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtOrderAmount As System.Windows.Forms.TextBox
    Friend WithEvents dtpCreateTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkClosedStock As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblItemName As System.Windows.Forms.Label
    Friend WithEvents lblItemNO As System.Windows.Forms.Label

End Class
