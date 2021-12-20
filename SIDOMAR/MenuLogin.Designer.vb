<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MenuLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MenuLogin))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MaterialCard2 = New MaterialSkin.Controls.MaterialCard()
        Me.txtPassword = New MaterialSkin.Controls.MaterialTextBox()
        Me.MaterialLabel3 = New MaterialSkin.Controls.MaterialLabel()
        Me.txtAddress = New MaterialSkin.Controls.MaterialTextBox2()
        Me.MaterialLabel5 = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialLabel4 = New MaterialSkin.Controls.MaterialLabel()
        Me.btnMasuk = New MaterialSkin.Controls.MaterialButton()
        Me.showPass = New MaterialSkin.Controls.MaterialCheckbox()
        Me.MaterialLabel2 = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialLabel1 = New MaterialSkin.Controls.MaterialLabel()
        Me.txtUsername = New MaterialSkin.Controls.MaterialTextBox2()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MaterialCard2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.MaterialCard2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(909, 644)
        Me.Panel1.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(31, 264)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(410, 277)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
        '
        'MaterialCard2
        '
        Me.MaterialCard2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MaterialCard2.Controls.Add(Me.txtPassword)
        Me.MaterialCard2.Controls.Add(Me.MaterialLabel3)
        Me.MaterialCard2.Controls.Add(Me.txtAddress)
        Me.MaterialCard2.Controls.Add(Me.MaterialLabel5)
        Me.MaterialCard2.Controls.Add(Me.MaterialLabel4)
        Me.MaterialCard2.Controls.Add(Me.btnMasuk)
        Me.MaterialCard2.Controls.Add(Me.showPass)
        Me.MaterialCard2.Controls.Add(Me.MaterialLabel2)
        Me.MaterialCard2.Controls.Add(Me.MaterialLabel1)
        Me.MaterialCard2.Controls.Add(Me.txtUsername)
        Me.MaterialCard2.Depth = 0
        Me.MaterialCard2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MaterialCard2.Location = New System.Drawing.Point(497, 85)
        Me.MaterialCard2.Margin = New System.Windows.Forms.Padding(14)
        Me.MaterialCard2.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialCard2.Name = "MaterialCard2"
        Me.MaterialCard2.Padding = New System.Windows.Forms.Padding(14)
        Me.MaterialCard2.Size = New System.Drawing.Size(380, 456)
        Me.MaterialCard2.TabIndex = 21
        '
        'txtPassword
        '
        Me.txtPassword.AnimateReadOnly = False
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Depth = 0
        Me.txtPassword.Font = New System.Drawing.Font("Roboto", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.txtPassword.Hint = "Password"
        Me.txtPassword.LeadingIcon = Nothing
        Me.txtPassword.Location = New System.Drawing.Point(20, 291)
        Me.txtPassword.MaxLength = 50
        Me.txtPassword.MouseState = MaterialSkin.MouseState.OUT
        Me.txtPassword.Multiline = False
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Password = True
        Me.txtPassword.Size = New System.Drawing.Size(343, 50)
        Me.txtPassword.TabIndex = 22
        Me.txtPassword.Text = ""
        Me.txtPassword.TrailingIcon = Nothing
        '
        'MaterialLabel3
        '
        Me.MaterialLabel3.AutoSize = True
        Me.MaterialLabel3.Depth = 0
        Me.MaterialLabel3.Font = New System.Drawing.Font("Roboto", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel3.Location = New System.Drawing.Point(20, 94)
        Me.MaterialLabel3.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel3.Name = "MaterialLabel3"
        Me.MaterialLabel3.Size = New System.Drawing.Size(58, 19)
        Me.MaterialLabel3.TabIndex = 21
        Me.MaterialLabel3.Text = "Address"
        '
        'txtAddress
        '
        Me.txtAddress.AnimateReadOnly = False
        Me.txtAddress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtAddress.Depth = 0
        Me.txtAddress.Font = New System.Drawing.Font("Roboto", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.txtAddress.HideSelection = True
        Me.txtAddress.Hint = "Address"
        Me.txtAddress.LeadingIcon = Nothing
        Me.txtAddress.Location = New System.Drawing.Point(20, 118)
        Me.txtAddress.MaxLength = 32767
        Me.txtAddress.MouseState = MaterialSkin.MouseState.OUT
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtAddress.PrefixSuffixText = Nothing
        Me.txtAddress.ReadOnly = False
        Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress.SelectedText = ""
        Me.txtAddress.SelectionLength = 0
        Me.txtAddress.SelectionStart = 0
        Me.txtAddress.ShortcutsEnabled = True
        Me.txtAddress.Size = New System.Drawing.Size(346, 48)
        Me.txtAddress.TabIndex = 20
        Me.txtAddress.TabStop = False
        Me.txtAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtAddress.TrailingIcon = Nothing
        Me.txtAddress.UseSystemPasswordChar = False
        '
        'MaterialLabel5
        '
        Me.MaterialLabel5.AutoSize = True
        Me.MaterialLabel5.Depth = 0
        Me.MaterialLabel5.Font = New System.Drawing.Font("Roboto", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel5.Location = New System.Drawing.Point(17, 53)
        Me.MaterialLabel5.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel5.Name = "MaterialLabel5"
        Me.MaterialLabel5.Size = New System.Drawing.Size(115, 19)
        Me.MaterialLabel5.TabIndex = 19
        Me.MaterialLabel5.Text = "Silahkan Masuk"
        '
        'MaterialLabel4
        '
        Me.MaterialLabel4.AutoSize = True
        Me.MaterialLabel4.Depth = 0
        Me.MaterialLabel4.Font = New System.Drawing.Font("Roboto Medium", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel4.FontType = MaterialSkin.MaterialSkinManager.fontType.H6
        Me.MaterialLabel4.Location = New System.Drawing.Point(16, 29)
        Me.MaterialLabel4.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel4.Name = "MaterialLabel4"
        Me.MaterialLabel4.Size = New System.Drawing.Size(55, 24)
        Me.MaterialLabel4.TabIndex = 18
        Me.MaterialLabel4.Text = "Hai! " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btnMasuk
        '
        Me.btnMasuk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMasuk.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.btnMasuk.Depth = 0
        Me.btnMasuk.HighEmphasis = True
        Me.btnMasuk.Icon = Nothing
        Me.btnMasuk.Location = New System.Drawing.Point(151, 400)
        Me.btnMasuk.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnMasuk.MouseState = MaterialSkin.MouseState.HOVER
        Me.btnMasuk.Name = "btnMasuk"
        Me.btnMasuk.NoAccentTextColor = System.Drawing.Color.Empty
        Me.btnMasuk.Size = New System.Drawing.Size(72, 36)
        Me.btnMasuk.TabIndex = 5
        Me.btnMasuk.Text = "Masuk"
        Me.btnMasuk.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.btnMasuk.UseAccentColor = False
        Me.btnMasuk.UseVisualStyleBackColor = True
        '
        'showPass
        '
        Me.showPass.AutoSize = True
        Me.showPass.Depth = 0
        Me.showPass.Location = New System.Drawing.Point(20, 347)
        Me.showPass.Margin = New System.Windows.Forms.Padding(0)
        Me.showPass.MouseLocation = New System.Drawing.Point(-1, -1)
        Me.showPass.MouseState = MaterialSkin.MouseState.HOVER
        Me.showPass.Name = "showPass"
        Me.showPass.ReadOnly = False
        Me.showPass.Ripple = True
        Me.showPass.Size = New System.Drawing.Size(185, 37)
        Me.showPass.TabIndex = 4
        Me.showPass.Text = "Tampilkan Password"
        Me.showPass.UseVisualStyleBackColor = True
        '
        'MaterialLabel2
        '
        Me.MaterialLabel2.AutoSize = True
        Me.MaterialLabel2.Depth = 0
        Me.MaterialLabel2.Font = New System.Drawing.Font("Roboto", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel2.Location = New System.Drawing.Point(19, 269)
        Me.MaterialLabel2.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel2.Name = "MaterialLabel2"
        Me.MaterialLabel2.Size = New System.Drawing.Size(71, 19)
        Me.MaterialLabel2.TabIndex = 3
        Me.MaterialLabel2.Text = "Password"
        '
        'MaterialLabel1
        '
        Me.MaterialLabel1.AutoSize = True
        Me.MaterialLabel1.Depth = 0
        Me.MaterialLabel1.Font = New System.Drawing.Font("Roboto", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel1.Location = New System.Drawing.Point(20, 184)
        Me.MaterialLabel1.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel1.Name = "MaterialLabel1"
        Me.MaterialLabel1.Size = New System.Drawing.Size(72, 19)
        Me.MaterialLabel1.TabIndex = 2
        Me.MaterialLabel1.Text = "Username"
        '
        'txtUsername
        '
        Me.txtUsername.AnimateReadOnly = False
        Me.txtUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.txtUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtUsername.Depth = 0
        Me.txtUsername.Font = New System.Drawing.Font("Roboto", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.txtUsername.HideSelection = True
        Me.txtUsername.Hint = "Username"
        Me.txtUsername.LeadingIcon = Nothing
        Me.txtUsername.Location = New System.Drawing.Point(20, 208)
        Me.txtUsername.MaxLength = 32767
        Me.txtUsername.MouseState = MaterialSkin.MouseState.OUT
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtUsername.PrefixSuffixText = Nothing
        Me.txtUsername.ReadOnly = False
        Me.txtUsername.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUsername.SelectedText = ""
        Me.txtUsername.SelectionLength = 0
        Me.txtUsername.SelectionStart = 0
        Me.txtUsername.ShortcutsEnabled = True
        Me.txtUsername.Size = New System.Drawing.Size(346, 48)
        Me.txtUsername.TabIndex = 0
        Me.txtUsername.TabStop = False
        Me.txtUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtUsername.TrailingIcon = Nothing
        Me.txtUsername.UseSystemPasswordChar = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Chau Philomene One", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 160)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(419, 100)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Selamat Datang di Aplikasi " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SIDOMAR Berbasis Desktop"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(31, 64)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(200, 93)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 23
        Me.PictureBox2.TabStop = False
        '
        'MenuLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(915, 671)
        Me.Controls.Add(Me.Panel1)
        Me.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None
        Me.Name = "MenuLogin"
        Me.Padding = New System.Windows.Forms.Padding(3, 24, 3, 3)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MenuLogin"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MaterialCard2.ResumeLayout(False)
        Me.MaterialCard2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MaterialCard2 As MaterialSkin.Controls.MaterialCard
    Friend WithEvents txtPassword As MaterialSkin.Controls.MaterialTextBox
    Friend WithEvents MaterialLabel3 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents txtAddress As MaterialSkin.Controls.MaterialTextBox2
    Friend WithEvents MaterialLabel5 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialLabel4 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents btnMasuk As MaterialSkin.Controls.MaterialButton
    Friend WithEvents showPass As MaterialSkin.Controls.MaterialCheckbox
    Friend WithEvents MaterialLabel2 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialLabel1 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents txtUsername As MaterialSkin.Controls.MaterialTextBox2
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox2 As PictureBox
End Class
