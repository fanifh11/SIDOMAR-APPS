Public Class MenuCari
    Public menu As String = ""


    Private Sub MenuCari_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData()
    End Sub

    Sub tampilkanData()
        If menu = "Cari Gerai" Then
            dgvCari.DataSource = getData("select geraiid,namagerai,alamat,notelp from tblgerai where namagerai ilike '%" & txtCari.Text & "%'")
            makeFillDG(dgvCari)
        ElseIf menu = "Cari Produk" Then
            dgvCari.DataSource = getData("select proid,pronama,prohargajual from tblproduk where pronama ilike '%" & txtCari.Text & "%'")
            makeFillDG(dgvCari)
        ElseIf menu = "Cari Data Transaksi" Then
            dgvCari.DataSource = getData("select noinvoice,tgltransaksi from tbltransaksi where noinvoice ilike '%" & txtCari.Text & "%' ")
            makeFillDG(dgvCari)
        ElseIf menu = "Cari Penempatan Gerai" Then
            dgvCari.DataSource = getData("select geraiid,namagerai,alamat from tblgerai where namagerai ilike '%" & txtCari.Text & "%' ")
            makeFillDG(dgvCari)
        End If


    End Sub

    Private Sub dgvCari_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCari.CellClick
        If e.RowIndex >= 0 Then

            If menu = "Cari Produk" Then
                MenuUtama.idproduk = dgvCari.Rows(e.RowIndex).Cells(0).Value

                MenuUtama.txtProdukId.Text = dgvCari.Rows(e.RowIndex).Cells(0).Value
                MenuUtama.txtProNama.Text = dgvCari.Rows(e.RowIndex).Cells(1).Value
                MenuUtama.txtHarga.Text = dgvCari.Rows(e.RowIndex).Cells(2).Value

            ElseIf menu = "Cari Penempatan Gerai" Then
                MenuUtama.idtoko = dgvCari.Rows(e.RowIndex).Cells(0).Value
                MenuUtama.txtKodeGerai.Text = dgvCari.Rows(e.RowIndex).Cells(0).Value

            End If


        End If

        Me.Close()

    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        tampilkanData()
    End Sub
End Class