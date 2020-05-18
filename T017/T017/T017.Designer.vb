<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class T017
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
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chkWhereShipment01 = New System.Windows.Forms.CheckBox()
        Me.chkWhereOrder01 = New System.Windows.Forms.CheckBox()
        Me.rboSales03 = New System.Windows.Forms.RadioButton()
        Me.rboSales02 = New System.Windows.Forms.RadioButton()
        Me.rboSales01 = New System.Windows.Forms.RadioButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.GroupBox()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.chkSelectShipment01 = New System.Windows.Forms.CheckBox()
        Me.chkSelectOrder01 = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dtpSales01 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblOrderMSName = New System.Windows.Forms.Label()
        Me.btnOrderSearch = New System.Windows.Forms.Button()
        Me.txtOrderMSNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblShipmentMSName = New System.Windows.Forms.Label()
        Me.btnShipmentSearch = New System.Windows.Forms.Button()
        Me.txtShipmentMSNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.btnSearch.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.rboSales03)
        Me.GroupBox1.Controls.Add(Me.rboSales02)
        Me.GroupBox1.Controls.Add(Me.rboSales01)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(860, 98)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "年月検索条件"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.chkWhereShipment01)
        Me.GroupBox4.Controls.Add(Me.chkWhereOrder01)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox4.Location = New System.Drawing.Point(3, 46)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(854, 49)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "    追加検索条件"
        '
        'chkWhereShipment01
        '
        Me.chkWhereShipment01.AutoSize = True
        Me.chkWhereShipment01.Location = New System.Drawing.Point(129, 21)
        Me.chkWhereShipment01.Name = "chkWhereShipment01"
        Me.chkWhereShipment01.Size = New System.Drawing.Size(76, 16)
        Me.chkWhereShipment01.TabIndex = 5
        Me.chkWhereShipment01.Text = "出荷先NO"
        Me.chkWhereShipment01.UseVisualStyleBackColor = True
        '
        'chkWhereOrder01
        '
        Me.chkWhereOrder01.AutoSize = True
        Me.chkWhereOrder01.Location = New System.Drawing.Point(24, 21)
        Me.chkWhereOrder01.Name = "chkWhereOrder01"
        Me.chkWhereOrder01.Size = New System.Drawing.Size(76, 16)
        Me.chkWhereOrder01.TabIndex = 4
        Me.chkWhereOrder01.Text = "受注先NO"
        Me.chkWhereOrder01.UseVisualStyleBackColor = True
        '
        'rboSales03
        '
        Me.rboSales03.AutoSize = True
        Me.rboSales03.Location = New System.Drawing.Point(249, 19)
        Me.rboSales03.Name = "rboSales03"
        Me.rboSales03.Size = New System.Drawing.Size(66, 16)
        Me.rboSales03.TabIndex = 6
        Me.rboSales03.TabStop = True
        Me.rboSales03.Text = "条件なし"
        Me.rboSales03.UseVisualStyleBackColor = True
        '
        'rboSales02
        '
        Me.rboSales02.AutoSize = True
        Me.rboSales02.Location = New System.Drawing.Point(90, 19)
        Me.rboSales02.Name = "rboSales02"
        Me.rboSales02.Size = New System.Drawing.Size(90, 16)
        Me.rboSales02.TabIndex = 3
        Me.rboSales02.TabStop = True
        Me.rboSales02.Text = "年月指定なし"
        Me.rboSales02.UseVisualStyleBackColor = True
        '
        'rboSales01
        '
        Me.rboSales01.AutoSize = True
        Me.rboSales01.Location = New System.Drawing.Point(13, 19)
        Me.rboSales01.Name = "rboSales01"
        Me.rboSales01.Size = New System.Drawing.Size(71, 16)
        Me.rboSales01.TabIndex = 0
        Me.rboSales01.TabStop = True
        Me.rboSales01.Text = "売上年月"
        Me.rboSales01.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.DataGridView1)
        Me.GroupBox6.Controls.Add(Me.btnSearch)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(0, 285)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(860, 277)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
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
        Me.DataGridView1.Size = New System.Drawing.Size(854, 210)
        Me.DataGridView1.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Controls.Add(Me.btnEnd)
        Me.btnSearch.Controls.Add(Me.btnClear)
        Me.btnSearch.Controls.Add(Me.Button1)
        Me.btnSearch.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnSearch.Location = New System.Drawing.Point(3, 225)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(854, 49)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.TabStop = False
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnd.Location = New System.Drawing.Point(761, 19)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnd.TabIndex = 2
        Me.btnEnd.Text = "F3:終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(658, 19)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "F2:クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(10, 19)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "F1:検索"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.chkSelectShipment01)
        Me.GroupBox7.Controls.Add(Me.chkSelectOrder01)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox7.Location = New System.Drawing.Point(0, 98)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(860, 50)
        Me.GroupBox7.TabIndex = 5
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "検索結果表示項目"
        '
        'chkSelectShipment01
        '
        Me.chkSelectShipment01.AutoSize = True
        Me.chkSelectShipment01.Location = New System.Drawing.Point(113, 22)
        Me.chkSelectShipment01.Name = "chkSelectShipment01"
        Me.chkSelectShipment01.Size = New System.Drawing.Size(76, 16)
        Me.chkSelectShipment01.TabIndex = 7
        Me.chkSelectShipment01.Text = "出荷先NO"
        Me.chkSelectShipment01.UseVisualStyleBackColor = True
        '
        'chkSelectOrder01
        '
        Me.chkSelectOrder01.AutoSize = True
        Me.chkSelectOrder01.Location = New System.Drawing.Point(9, 22)
        Me.chkSelectOrder01.Name = "chkSelectOrder01"
        Me.chkSelectOrder01.Size = New System.Drawing.Size(76, 16)
        Me.chkSelectOrder01.TabIndex = 6
        Me.chkSelectOrder01.Text = "受注先NO"
        Me.chkSelectOrder01.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpSales01)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 148)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(860, 49)
        Me.GroupBox2.TabIndex = 102
        Me.GroupBox2.TabStop = False
        '
        'dtpSales01
        '
        Me.dtpSales01.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSales01.Location = New System.Drawing.Point(70, 15)
        Me.dtpSales01.Name = "dtpSales01"
        Me.dtpSales01.Size = New System.Drawing.Size(130, 19)
        Me.dtpSales01.TabIndex = 40
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "売上年月"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblOrderMSName)
        Me.GroupBox3.Controls.Add(Me.btnOrderSearch)
        Me.GroupBox3.Controls.Add(Me.txtOrderMSNo)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 197)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(860, 42)
        Me.GroupBox3.TabIndex = 103
        Me.GroupBox3.TabStop = False
        '
        'lblOrderMSName
        '
        Me.lblOrderMSName.AutoSize = True
        Me.lblOrderMSName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrderMSName.Location = New System.Drawing.Point(157, 16)
        Me.lblOrderMSName.Name = "lblOrderMSName"
        Me.lblOrderMSName.Size = New System.Drawing.Size(59, 13)
        Me.lblOrderMSName.TabIndex = 34
        Me.lblOrderMSName.Text = "受注先名"
        '
        'btnOrderSearch
        '
        Me.btnOrderSearch.Location = New System.Drawing.Point(132, 10)
        Me.btnOrderSearch.Name = "btnOrderSearch"
        Me.btnOrderSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnOrderSearch.TabIndex = 33
        Me.btnOrderSearch.Text = "..."
        Me.btnOrderSearch.UseVisualStyleBackColor = True
        '
        'txtOrderMSNo
        '
        Me.txtOrderMSNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtOrderMSNo.Location = New System.Drawing.Point(91, 12)
        Me.txtOrderMSNo.MaxLength = 3
        Me.txtOrderMSNo.Name = "txtOrderMSNo"
        Me.txtOrderMSNo.Size = New System.Drawing.Size(41, 19)
        Me.txtOrderMSNo.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 12)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "受注先NO"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblShipmentMSName)
        Me.GroupBox5.Controls.Add(Me.btnShipmentSearch)
        Me.GroupBox5.Controls.Add(Me.txtShipmentMSNo)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.Location = New System.Drawing.Point(0, 239)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(860, 46)
        Me.GroupBox5.TabIndex = 104
        Me.GroupBox5.TabStop = False
        '
        'lblShipmentMSName
        '
        Me.lblShipmentMSName.AutoSize = True
        Me.lblShipmentMSName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShipmentMSName.Location = New System.Drawing.Point(157, 19)
        Me.lblShipmentMSName.Name = "lblShipmentMSName"
        Me.lblShipmentMSName.Size = New System.Drawing.Size(59, 13)
        Me.lblShipmentMSName.TabIndex = 38
        Me.lblShipmentMSName.Text = "出荷先名"
        '
        'btnShipmentSearch
        '
        Me.btnShipmentSearch.Location = New System.Drawing.Point(132, 13)
        Me.btnShipmentSearch.Name = "btnShipmentSearch"
        Me.btnShipmentSearch.Size = New System.Drawing.Size(24, 23)
        Me.btnShipmentSearch.TabIndex = 37
        Me.btnShipmentSearch.Text = "..."
        Me.btnShipmentSearch.UseVisualStyleBackColor = True
        '
        'txtShipmentMSNo
        '
        Me.txtShipmentMSNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShipmentMSNo.Location = New System.Drawing.Point(91, 15)
        Me.txtShipmentMSNo.MaxLength = 3
        Me.txtShipmentMSNo.Name = "txtShipmentMSNo"
        Me.txtShipmentMSNo.Size = New System.Drawing.Size(41, 19)
        Me.txtShipmentMSNo.TabIndex = 36
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 12)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "出荷先NO"
        '
        'T017
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 562)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "T017"
        Me.Text = "T017_売上確認"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.btnSearch.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rboSales02 As System.Windows.Forms.RadioButton
    Friend WithEvents rboSales01 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rboSales03 As System.Windows.Forms.RadioButton
    Friend WithEvents chkWhereShipment01 As System.Windows.Forms.CheckBox
    Friend WithEvents chkWhereOrder01 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents chkSelectShipment01 As System.Windows.Forms.CheckBox
    Friend WithEvents chkSelectOrder01 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpSales01 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblOrderMSName As System.Windows.Forms.Label
    Friend WithEvents btnOrderSearch As System.Windows.Forms.Button
    Friend WithEvents txtOrderMSNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblShipmentMSName As System.Windows.Forms.Label
    Friend WithEvents btnShipmentSearch As System.Windows.Forms.Button
    Friend WithEvents txtShipmentMSNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
