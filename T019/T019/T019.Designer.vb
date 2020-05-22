<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T019
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
        Me.chkDeleteMode = New System.Windows.Forms.CheckBox()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me.btnItemSearch = New System.Windows.Forms.Button()
        Me.txtItemNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.btnUpdateUserIDSearch = New System.Windows.Forms.Button()
        Me.txtUpdateUserID = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUpdateMoney = New System.Windows.Forms.TextBox()
        Me.txtUpdateItemName = New System.Windows.Forms.TextBox()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtUpdateItemNO = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkDeleteMode)
        Me.GroupBox1.Controls.Add(Me.lblItemName)
        Me.GroupBox1.Controls.Add(Me.btnItemSearch)
        Me.GroupBox1.Controls.Add(Me.txtItemNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(699, 52)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "検索条件"
        '
        'chkDeleteMode
        '
        Me.chkDeleteMode.AutoSize = True
        Me.chkDeleteMode.Location = New System.Drawing.Point(317, 23)
        Me.chkDeleteMode.Name = "chkDeleteMode"
        Me.chkDeleteMode.Size = New System.Drawing.Size(76, 16)
        Me.chkDeleteMode.TabIndex = 3
        Me.chkDeleteMode.Text = "削除モード"
        Me.chkDeleteMode.UseVisualStyleBackColor = True
        '
        'lblItemName
        '
        Me.lblItemName.AutoSize = True
        Me.lblItemName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblItemName.Location = New System.Drawing.Point(158, 25)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(46, 13)
        Me.lblItemName.TabIndex = 42
        Me.lblItemName.Text = "製品名"
        '
        'btnItemSearch
        '
        Me.btnItemSearch.Location = New System.Drawing.Point(133, 19)
        Me.btnItemSearch.Name = "btnItemSearch"
        Me.btnItemSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnItemSearch.TabIndex = 2
        Me.btnItemSearch.Text = "..."
        Me.btnItemSearch.UseVisualStyleBackColor = True
        '
        'txtItemNo
        '
        Me.txtItemNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtItemNo.Location = New System.Drawing.Point(92, 21)
        Me.txtItemNo.MaxLength = 3
        Me.txtItemNo.Name = "txtItemNo"
        Me.txtItemNo.Size = New System.Drawing.Size(41, 19)
        Me.txtItemNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 12)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "製品NO"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnDelete)
        Me.GroupBox2.Controls.Add(Me.btnEnd)
        Me.GroupBox2.Controls.Add(Me.btnClear)
        Me.GroupBox2.Controls.Add(Me.btnUpdate)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 426)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(699, 44)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnd.Location = New System.Drawing.Point(612, 15)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnd.TabIndex = 14
        Me.btnEnd.Text = "F5:終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(518, 15)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "F4:クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(109, 15)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 11
        Me.btnUpdate.Text = "F2:更新"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(6, 15)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 10
        Me.btnSearch.Text = "F1:検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox5)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 52)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(699, 374)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.DataGridView1)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(3, 193)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(693, 178)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "更新履歴"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 15)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(687, 160)
        Me.DataGridView1.TabIndex = 9
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox6)
        Me.GroupBox4.Controls.Add(Me.txtUpdateItemName)
        Me.GroupBox4.Controls.Add(Me.lblMode)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtUpdateItemNO)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(3, 15)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(693, 178)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.btnUpdateUserIDSearch)
        Me.GroupBox6.Controls.Add(Me.txtUpdateUserID)
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Controls.Add(Me.Label4)
        Me.GroupBox6.Controls.Add(Me.txtUpdateMoney)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox6.Location = New System.Drawing.Point(3, 91)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(687, 84)
        Me.GroupBox6.TabIndex = 47
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "金額更新"
        '
        'btnUpdateUserIDSearch
        '
        Me.btnUpdateUserIDSearch.Location = New System.Drawing.Point(173, 53)
        Me.btnUpdateUserIDSearch.Name = "btnUpdateUserIDSearch"
        Me.btnUpdateUserIDSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnUpdateUserIDSearch.TabIndex = 8
        Me.btnUpdateUserIDSearch.Text = "..."
        Me.btnUpdateUserIDSearch.UseVisualStyleBackColor = True
        '
        'txtUpdateUserID
        '
        Me.txtUpdateUserID.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUpdateUserID.Location = New System.Drawing.Point(86, 55)
        Me.txtUpdateUserID.MaxLength = 10
        Me.txtUpdateUserID.Name = "txtUpdateUserID"
        Me.txtUpdateUserID.Size = New System.Drawing.Size(87, 19)
        Me.txtUpdateUserID.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = "更新担当者"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "金額"
        '
        'txtUpdateMoney
        '
        Me.txtUpdateMoney.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUpdateMoney.Location = New System.Drawing.Point(86, 22)
        Me.txtUpdateMoney.MaxLength = 6
        Me.txtUpdateMoney.Name = "txtUpdateMoney"
        Me.txtUpdateMoney.Size = New System.Drawing.Size(65, 19)
        Me.txtUpdateMoney.TabIndex = 6
        '
        'txtUpdateItemName
        '
        Me.txtUpdateItemName.Location = New System.Drawing.Point(89, 53)
        Me.txtUpdateItemName.MaxLength = 30
        Me.txtUpdateItemName.Name = "txtUpdateItemName"
        Me.txtUpdateItemName.Size = New System.Drawing.Size(196, 19)
        Me.txtUpdateItemName.TabIndex = 5
        '
        'lblMode
        '
        Me.lblMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMode.AutoSize = True
        Me.lblMode.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMode.Location = New System.Drawing.Point(624, 30)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(51, 12)
        Me.lblMode.TabIndex = 5
        Me.lblMode.Text = "lblMode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "製品名"
        '
        'txtUpdateItemNO
        '
        Me.txtUpdateItemNO.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUpdateItemNO.Location = New System.Drawing.Point(89, 18)
        Me.txtUpdateItemNO.MaxLength = 3
        Me.txtUpdateItemNO.Name = "txtUpdateItemNO"
        Me.txtUpdateItemNO.Size = New System.Drawing.Size(41, 19)
        Me.txtUpdateItemNO.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 12)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "製品NO"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(213, 15)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "F3:削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'T019
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 470)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "T019"
        Me.Text = "T019_製品マスタメンテナンス"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblItemName As System.Windows.Forms.Label
    Friend WithEvents btnItemSearch As System.Windows.Forms.Button
    Friend WithEvents txtItemNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUpdateMoney As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtUpdateItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUpdateItemNO As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblMode As System.Windows.Forms.Label
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateUserIDSearch As System.Windows.Forms.Button
    Friend WithEvents txtUpdateUserID As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkDeleteMode As System.Windows.Forms.CheckBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button

End Class
