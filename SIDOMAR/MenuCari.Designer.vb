<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MenuCari
    Inherits MaterialSkin.Controls.MaterialForm

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MenuCari))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MaterialCard1 = New MaterialSkin.Controls.MaterialCard()
        Me.txtCari = New MaterialSkin.Controls.MaterialTextBox()
        Me.dgvCari = New System.Windows.Forms.DataGridView()
        Me.MaterialButton1 = New MaterialSkin.Controls.MaterialButton()
        Me.Panel1.SuspendLayout()
        Me.MaterialCard1.SuspendLayout()
        CType(Me.dgvCari, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MaterialCard1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 64)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(794, 407)
        Me.Panel1.TabIndex = 0
        '
        'MaterialCard1
        '
        Me.MaterialCard1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MaterialCard1.Controls.Add(Me.MaterialButton1)
        Me.MaterialCard1.Controls.Add(Me.dgvCari)
        Me.MaterialCard1.Controls.Add(Me.txtCari)
        Me.MaterialCard1.Depth = 0
        Me.MaterialCard1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MaterialCard1.Location = New System.Drawing.Point(24, 24)
        Me.MaterialCard1.Margin = New System.Windows.Forms.Padding(14)
        Me.MaterialCard1.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialCard1.Name = "MaterialCard1"
        Me.MaterialCard1.Padding = New System.Windows.Forms.Padding(14)
        Me.MaterialCard1.Size = New System.Drawing.Size(739, 369)
        Me.MaterialCard1.TabIndex = 0
        '
        'txtCari
        '
        Me.txtCari.AnimateReadOnly = False
        Me.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCari.Depth = 0
        Me.txtCari.Font = New System.Drawing.Font("Roboto", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.txtCari.Hint = "Cari data"
        Me.txtCari.LeadingIcon = Nothing
        Me.txtCari.Location = New System.Drawing.Point(41, 17)
        Me.txtCari.MaxLength = 50
        Me.txtCari.MouseState = MaterialSkin.MouseState.OUT
        Me.txtCari.Multiline = False
        Me.txtCari.Name = "txtCari"
        Me.txtCari.Size = New System.Drawing.Size(654, 50)
        Me.txtCari.TabIndex = 0
        Me.txtCari.Text = ""
        Me.txtCari.TrailingIcon = CType(resources.GetObject("txtCari.TrailingIcon"), System.Drawing.Image)
        '
        'dgvCari
        '
        Me.dgvCari.AllowUserToAddRows = False
        Me.dgvCari.AllowUserToDeleteRows = False
        Me.dgvCari.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCari.Location = New System.Drawing.Point(18, 78)
        Me.dgvCari.Name = "dgvCari"
        Me.dgvCari.ReadOnly = True
        Me.dgvCari.Size = New System.Drawing.Size(704, 228)
        Me.dgvCari.TabIndex = 1
        '
        'MaterialButton1
        '
        Me.MaterialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.MaterialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.MaterialButton1.Depth = 0
        Me.MaterialButton1.HighEmphasis = True
        Me.MaterialButton1.Icon = Nothing
        Me.MaterialButton1.Location = New System.Drawing.Point(645, 325)
        Me.MaterialButton1.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.MaterialButton1.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialButton1.Name = "MaterialButton1"
        Me.MaterialButton1.NoAccentTextColor = System.Drawing.Color.Empty
        Me.MaterialButton1.Size = New System.Drawing.Size(76, 36)
        Me.MaterialButton1.TabIndex = 2
        Me.MaterialButton1.Text = "Keluar"
        Me.MaterialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.MaterialButton1.UseAccentColor = False
        Me.MaterialButton1.UseVisualStyleBackColor = True
        '
        'MenuCari
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 474)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "MenuCari"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cari"
        Me.Panel1.ResumeLayout(False)
        Me.MaterialCard1.ResumeLayout(False)
        Me.MaterialCard1.PerformLayout()
        CType(Me.dgvCari, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents MaterialCard1 As MaterialSkin.Controls.MaterialCard
    Friend WithEvents MaterialButton1 As MaterialSkin.Controls.MaterialButton
    Friend WithEvents dgvCari As DataGridView
    Friend WithEvents txtCari As MaterialSkin.Controls.MaterialTextBox
End Class
