<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T008_3
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.btnUpate = New System.Windows.Forms.Button()
        Me.lblWorkProcessName = New System.Windows.Forms.Label()
        Me.lblOrderMS = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnWorkProcessMSSeach = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtHumanNo = New System.Windows.Forms.TextBox()
        Me.dtpOrderDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOrderAmount = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtWorkProcessNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtOrderMS = New System.Windows.Forms.TextBox()
        Me.btnHumanSearch = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "受注NO"
        '
        'txtOrderNo
        '
        Me.txtOrderNo.Location = New System.Drawing.Point(86, 22)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(100, 19)
        Me.txtOrderNo.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtOrderNo)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(407, 56)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnHumanSearch)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.lblWorkProcessName)
        Me.GroupBox2.Controls.Add(Me.lblOrderMS)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.btnWorkProcessMSSeach)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtHumanNo)
        Me.GroupBox2.Controls.Add(Me.dtpOrderDate)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtOrderAmount)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtWorkProcessNo)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtOrderMS)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(407, 317)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnEnd)
        Me.GroupBox3.Controls.Add(Me.btnUpate)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Location = New System.Drawing.Point(3, 263)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(401, 51)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        '
        'btnEnd
        '
        Me.btnEnd.Location = New System.Drawing.Point(317, 18)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnd.TabIndex = 1
        Me.btnEnd.Text = "閉じる"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'btnUpate
        '
        Me.btnUpate.Location = New System.Drawing.Point(194, 18)
        Me.btnUpate.Name = "btnUpate"
        Me.btnUpate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpate.TabIndex = 0
        Me.btnUpate.Text = "更新"
        Me.btnUpate.UseVisualStyleBackColor = True
        '
        'lblWorkProcessName
        '
        Me.lblWorkProcessName.AutoSize = True
        Me.lblWorkProcessName.Location = New System.Drawing.Point(173, 111)
        Me.lblWorkProcessName.Name = "lblWorkProcessName"
        Me.lblWorkProcessName.Size = New System.Drawing.Size(99, 12)
        Me.lblWorkProcessName.TabIndex = 14
        Me.lblWorkProcessName.Text = "lblWorkProcessMS"
        '
        'lblOrderMS
        '
        Me.lblOrderMS.AutoSize = True
        Me.lblOrderMS.Location = New System.Drawing.Point(144, 29)
        Me.lblOrderMS.Name = "lblOrderMS"
        Me.lblOrderMS.Size = New System.Drawing.Size(61, 12)
        Me.lblOrderMS.TabIndex = 13
        Me.lblOrderMS.Text = "lblOrderMS"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(28, 73)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(300, 12)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "登録不備の場合は、削除し、再度登録し直してください。"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(15, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(174, 12)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "※受注先NOは訂正できません。"
        '
        'btnWorkProcessMSSeach
        '
        Me.btnWorkProcessMSSeach.Location = New System.Drawing.Point(144, 105)
        Me.btnWorkProcessMSSeach.Name = "btnWorkProcessMSSeach"
        Me.btnWorkProcessMSSeach.Size = New System.Drawing.Size(28, 23)
        Me.btnWorkProcessMSSeach.TabIndex = 5
        Me.btnWorkProcessMSSeach.Text = "..."
        Me.btnWorkProcessMSSeach.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 230)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "更新担当者"
        '
        'txtHumanNo
        '
        Me.txtHumanNo.Location = New System.Drawing.Point(90, 227)
        Me.txtHumanNo.Name = "txtHumanNo"
        Me.txtHumanNo.Size = New System.Drawing.Size(53, 19)
        Me.txtHumanNo.TabIndex = 8
        '
        'dtpOrderDate
        '
        Me.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrderDate.Location = New System.Drawing.Point(90, 187)
        Me.dtpOrderDate.Name = "dtpOrderDate"
        Me.dtpOrderDate.Size = New System.Drawing.Size(125, 19)
        Me.dtpOrderDate.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 192)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "受注日"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 154)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "受注数"
        '
        'txtOrderAmount
        '
        Me.txtOrderAmount.Location = New System.Drawing.Point(90, 151)
        Me.txtOrderAmount.Name = "txtOrderAmount"
        Me.txtOrderAmount.Size = New System.Drawing.Size(53, 19)
        Me.txtOrderAmount.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "作業工程NO"
        '
        'txtWorkProcessNo
        '
        Me.txtWorkProcessNo.Location = New System.Drawing.Point(90, 107)
        Me.txtWorkProcessNo.Name = "txtWorkProcessNo"
        Me.txtWorkProcessNo.Size = New System.Drawing.Size(53, 19)
        Me.txtWorkProcessNo.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "受注先NO"
        '
        'txtOrderMS
        '
        Me.txtOrderMS.Location = New System.Drawing.Point(90, 25)
        Me.txtOrderMS.Name = "txtOrderMS"
        Me.txtOrderMS.Size = New System.Drawing.Size(53, 19)
        Me.txtOrderMS.TabIndex = 2
        '
        'btnHumanSearch
        '
        Me.btnHumanSearch.Location = New System.Drawing.Point(144, 225)
        Me.btnHumanSearch.Name = "btnHumanSearch"
        Me.btnHumanSearch.Size = New System.Drawing.Size(28, 23)
        Me.btnHumanSearch.TabIndex = 16
        Me.btnHumanSearch.Text = "..."
        Me.btnHumanSearch.UseVisualStyleBackColor = True
        '
        'T008_3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 373)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "T008_3"
        Me.Text = "T008_3_受注データ詳細"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtWorkProcessNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtOrderMS As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtHumanNo As System.Windows.Forms.TextBox
    Friend WithEvents dtpOrderDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtOrderAmount As System.Windows.Forms.TextBox
    Friend WithEvents btnWorkProcessMSSeach As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblOrderMS As System.Windows.Forms.Label
    Friend WithEvents lblWorkProcessName As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnUpate As System.Windows.Forms.Button
    Friend WithEvents btnHumanSearch As System.Windows.Forms.Button
End Class
