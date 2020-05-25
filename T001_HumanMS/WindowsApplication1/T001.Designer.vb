<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T001
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
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblPass = New System.Windows.Forms.Label()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnEntry = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkAdminFLG = New System.Windows.Forms.CheckBox()
        Me.txtHumanNo = New System.Windows.Forms.TextBox()
        Me.lblHumanNo = New System.Windows.Forms.Label()
        Me.grpHuman = New System.Windows.Forms.GroupBox()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnHumanSearch = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grpHuman.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(13, 66)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(29, 12)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "名前"
        '
        'txtName
        '
        Me.txtName.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtName.Location = New System.Drawing.Point(98, 59)
        Me.txtName.MaxLength = 10
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(100, 19)
        Me.txtName.TabIndex = 3
        '
        'lblPass
        '
        Me.lblPass.AutoSize = True
        Me.lblPass.Location = New System.Drawing.Point(13, 91)
        Me.lblPass.Name = "lblPass"
        Me.lblPass.Size = New System.Drawing.Size(52, 12)
        Me.lblPass.TabIndex = 2
        Me.lblPass.Text = "パスワード"
        '
        'txtPass
        '
        Me.txtPass.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPass.Location = New System.Drawing.Point(98, 88)
        Me.txtPass.MaxLength = 10
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(100, 19)
        Me.txtPass.TabIndex = 4
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(386, 18)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 23)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "F4:クリア"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(6, 18)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(90, 23)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "F1:検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnEntry
        '
        Me.btnEntry.Location = New System.Drawing.Point(108, 18)
        Me.btnEntry.Name = "btnEntry"
        Me.btnEntry.Size = New System.Drawing.Size(90, 23)
        Me.btnEntry.TabIndex = 9
        Me.btnEntry.Text = "F2:登録"
        Me.btnEntry.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnd.Location = New System.Drawing.Point(495, 18)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(90, 23)
        Me.btnEnd.TabIndex = 12
        Me.btnEnd.Text = "F5:終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "管理者フラグ"
        '
        'chkAdminFLG
        '
        Me.chkAdminFLG.AutoSize = True
        Me.chkAdminFLG.Location = New System.Drawing.Point(98, 122)
        Me.chkAdminFLG.Name = "chkAdminFLG"
        Me.chkAdminFLG.Size = New System.Drawing.Size(15, 14)
        Me.chkAdminFLG.TabIndex = 5
        Me.chkAdminFLG.UseVisualStyleBackColor = True
        '
        'txtHumanNo
        '
        Me.txtHumanNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtHumanNo.Location = New System.Drawing.Point(98, 14)
        Me.txtHumanNo.MaxLength = 3
        Me.txtHumanNo.Name = "txtHumanNo"
        Me.txtHumanNo.Size = New System.Drawing.Size(51, 19)
        Me.txtHumanNo.TabIndex = 1
        '
        'lblHumanNo
        '
        Me.lblHumanNo.AutoSize = True
        Me.lblHumanNo.Location = New System.Drawing.Point(6, 17)
        Me.lblHumanNo.Name = "lblHumanNo"
        Me.lblHumanNo.Size = New System.Drawing.Size(43, 12)
        Me.lblHumanNo.TabIndex = 9
        Me.lblHumanNo.Text = "社員No"
        '
        'grpHuman
        '
        Me.grpHuman.Controls.Add(Me.lblMode)
        Me.grpHuman.Controls.Add(Me.lblName)
        Me.grpHuman.Controls.Add(Me.txtName)
        Me.grpHuman.Controls.Add(Me.lblPass)
        Me.grpHuman.Controls.Add(Me.chkAdminFLG)
        Me.grpHuman.Controls.Add(Me.txtPass)
        Me.grpHuman.Controls.Add(Me.Label1)
        Me.grpHuman.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpHuman.Location = New System.Drawing.Point(0, 0)
        Me.grpHuman.Name = "grpHuman"
        Me.grpHuman.Size = New System.Drawing.Size(597, 343)
        Me.grpHuman.TabIndex = 2
        Me.grpHuman.TabStop = False
        '
        'lblMode
        '
        Me.lblMode.AutoSize = True
        Me.lblMode.Location = New System.Drawing.Point(154, 145)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(44, 12)
        Me.lblMode.TabIndex = 10
        Me.lblMode.Text = "lblMode"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(222, 18)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 23)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "F3:削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnHumanSearch)
        Me.GroupBox1.Controls.Add(Me.lblHumanNo)
        Me.GroupBox1.Controls.Add(Me.txtHumanNo)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(597, 48)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'btnHumanSearch
        '
        Me.btnHumanSearch.Location = New System.Drawing.Point(150, 12)
        Me.btnHumanSearch.Name = "btnHumanSearch"
        Me.btnHumanSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnHumanSearch.TabIndex = 10
        Me.btnHumanSearch.Text = "..."
        Me.btnHumanSearch.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.Controls.Add(Me.btnDelete)
        Me.GroupBox2.Controls.Add(Me.btnEntry)
        Me.GroupBox2.Controls.Add(Me.btnEnd)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 281)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(597, 62)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        '
        'T001
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(597, 343)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpHuman)
        Me.KeyPreview = True
        Me.Name = "T001"
        Me.Text = "T001_社員マスタメンテナンス"
        Me.grpHuman.ResumeLayout(False)
        Me.grpHuman.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblPass As System.Windows.Forms.Label
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnEntry As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkAdminFLG As System.Windows.Forms.CheckBox
    Friend WithEvents txtHumanNo As System.Windows.Forms.TextBox
    Friend WithEvents lblHumanNo As System.Windows.Forms.Label
    Friend WithEvents grpHuman As System.Windows.Forms.GroupBox
    Friend WithEvents lblMode As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnHumanSearch As System.Windows.Forms.Button

End Class
