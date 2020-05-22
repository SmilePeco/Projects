<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T020
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
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.txtAdress2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAdress1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.txtTELL = New System.Windows.Forms.TextBox()
        Me.txtOfficer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtShipmentName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtShipmentNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnShipmentSearch = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnd.Location = New System.Drawing.Point(445, 19)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnd.TabIndex = 4
        Me.btnEnd.Text = "F5:終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(350, 19)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "F4:クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(223, 19)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "F3:削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(14, 19)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 0
        Me.btnSearch.Text = "F1:検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblMode
        '
        Me.lblMode.AutoSize = True
        Me.lblMode.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMode.Location = New System.Drawing.Point(456, 242)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(51, 12)
        Me.lblMode.TabIndex = 10
        Me.lblMode.Text = "lblMode"
        '
        'txtAdress2
        '
        Me.txtAdress2.Location = New System.Drawing.Point(76, 177)
        Me.txtAdress2.MaxLength = 20
        Me.txtAdress2.Name = "txtAdress2"
        Me.txtAdress2.Size = New System.Drawing.Size(362, 19)
        Me.txtAdress2.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 180)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 12)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "住所２"
        '
        'txtAdress1
        '
        Me.txtAdress1.Location = New System.Drawing.Point(76, 139)
        Me.txtAdress1.MaxLength = 20
        Me.txtAdress1.Name = "txtAdress1"
        Me.txtAdress1.Size = New System.Drawing.Size(362, 19)
        Me.txtAdress1.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 142)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 12)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "住所１"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnEnd)
        Me.GroupBox3.Controls.Add(Me.btnClear)
        Me.GroupBox3.Controls.Add(Me.btnDelete)
        Me.GroupBox3.Controls.Add(Me.btnUpdate)
        Me.GroupBox3.Controls.Add(Me.btnSearch)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Location = New System.Drawing.Point(0, 318)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(532, 57)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(117, 19)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "F2:更新"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'txtTELL
        '
        Me.txtTELL.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTELL.Location = New System.Drawing.Point(76, 100)
        Me.txtTELL.MaxLength = 13
        Me.txtTELL.Name = "txtTELL"
        Me.txtTELL.Size = New System.Drawing.Size(116, 19)
        Me.txtTELL.TabIndex = 4
        '
        'txtOfficer
        '
        Me.txtOfficer.Location = New System.Drawing.Point(76, 62)
        Me.txtOfficer.MaxLength = 8
        Me.txtOfficer.Name = "txtOfficer"
        Me.txtOfficer.Size = New System.Drawing.Size(142, 19)
        Me.txtOfficer.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "担当者名"
        '
        'txtShipmentName
        '
        Me.txtShipmentName.Location = New System.Drawing.Point(76, 26)
        Me.txtShipmentName.MaxLength = 20
        Me.txtShipmentName.Name = "txtShipmentName"
        Me.txtShipmentName.Size = New System.Drawing.Size(194, 19)
        Me.txtShipmentName.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "出荷先名"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblMode)
        Me.GroupBox2.Controls.Add(Me.txtAdress2)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtAdress1)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtTELL)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtOfficer)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtShipmentName)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 48)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(532, 327)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "電話番号"
        '
        'txtShipmentNo
        '
        Me.txtShipmentNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShipmentNo.Location = New System.Drawing.Point(76, 13)
        Me.txtShipmentNo.MaxLength = 3
        Me.txtShipmentNo.Name = "txtShipmentNo"
        Me.txtShipmentNo.Size = New System.Drawing.Size(57, 19)
        Me.txtShipmentNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "出荷先NO"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnShipmentSearch)
        Me.GroupBox1.Controls.Add(Me.txtShipmentNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(532, 48)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'btnShipmentSearch
        '
        Me.btnShipmentSearch.Location = New System.Drawing.Point(134, 11)
        Me.btnShipmentSearch.Name = "btnShipmentSearch"
        Me.btnShipmentSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnShipmentSearch.TabIndex = 3
        Me.btnShipmentSearch.Text = "..."
        Me.btnShipmentSearch.UseVisualStyleBackColor = True
        '
        'T020
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 375)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "T020"
        Me.Text = "T020_出荷先マスタメンテナンス"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblMode As System.Windows.Forms.Label
    Friend WithEvents txtAdress2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAdress1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents txtTELL As System.Windows.Forms.TextBox
    Friend WithEvents txtOfficer As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtShipmentName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtShipmentNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnShipmentSearch As System.Windows.Forms.Button

End Class
