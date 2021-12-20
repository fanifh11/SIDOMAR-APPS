Imports MaterialSkin

Public Class MenuLogin
    Private Sub MenuLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.AddFormToManage(Me)
        SkinManager.Theme = MaterialSkinManager.Themes.LIGHT

        SkinManager.ColorScheme = New ColorScheme(Primary.Blue400, Primary.Blue400, Primary.BlueGrey500, Accent.LightBlue700, TextShade.WHITE)

        Label1.Font = New Font("Chau Philomene One", 27, FontStyle.Regular)

    End Sub

    Private Sub btnMasuk_Click(sender As Object, e As EventArgs) Handles btnMasuk.Click
        setLocationDatabase(txtAddress.Text)
        Dim usernameTxt = txtUsername.Text
        Dim passwordTxt = txtPassword.Text


        If clearKoneksi() Then
            If getCount("select dusername,dpassword from tbluser where dusername = '" & usernameTxt & "' and dpassword = '" & passwordTxt & "' ") > 0 Then
                Me.Hide()

                MenuUtama.username = usernameTxt
                MenuUtama.setRule()
                MenuUtama.Show()

                txtAddress.Clear()
                txtUsername.Clear()
                txtPassword.Clear()

            Else
                dialogError("username atau passwrod tidak ditemukan")
            End If
        Else
            dialogError("address tidak ditemukan")
        End If
    End Sub

    Private Sub showPass_CheckedChanged(sender As Object, e As EventArgs) Handles showPass.CheckedChanged
        If showPass.Checked = True Then
            txtPassword.Password = False
        ElseIf showPass.Checked = False Then
            txtPassword.Password = True

        End If
    End Sub
End Class