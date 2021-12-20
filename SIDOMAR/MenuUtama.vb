Imports MaterialSkin

Public Class MenuUtama
    Dim metode As String = ""
    Dim stringID As String = "KAT"
    Dim proID As String = "PRO"
    Dim noInvo As String = "INV"
    Dim uID As String = "USERID"
    Dim idAkses As String
    Dim selectId As String
    Dim selectIdUser As String
    Dim userid As String

    Public idtoko As String
    Public idproduk As String
    Public idnota As String
    Public username As String = "username"

    Sub setRule()
        Dim hakakses = getValue("select hakakses from tbluser where dusername = '" & username & "'", "hakakses")
        Dim sql = "select menutag,flag from tblmenu where hakakses = '" & hakakses & "'"

        Dim dataMenu As DataTable = getData(sql)
        For Each row As DataRow In dataMenu.Rows
            Dim menuTag = row.Item("menutag")
            Dim checked = row.Item("flag")

            If menuTag = 1 Then
                tabDashboard.Visible = checked
            ElseIf menuTag = 2 Then
                tabProduk.Visible = checked
            ElseIf menuTag = 3 Then
                tabPenjualan.Visible = checked
            ElseIf menuTag = 4 Then
                tabGerai.Visible = checked
            ElseIf menuTag = 5 Then
                tabHakAkses.Visible = checked
            ElseIf menuTag = 6 Then
                tabUser.Visible = checked

            End If
        Next
    End Sub

    ''Untuk Menu Dashboard''
    Sub tampilkanDataLabaMasing2Gerai()
        Dim pilihBulan As String = cmbBulan.SelectedIndex

        dgvLabaPertoko.DataSource = getData("select distinct namagerai,(sum(totalharga) - sum(tbldetailtransaksi.jumlahbeli * tblproduk.prohargabeli)) as laba from tbldetailtransaksi 
        inner join tbltransaksi on tbldetailtransaksi.noinvoice = tbltransaksi.noinvoice
        inner join tblproduk on tbldetailtransaksi.proid = tblproduk.proid 
        inner join tbluser on tbltransaksi.userid = tbluser.userid 
        inner join tblgerai on tbluser.geraiid = tblgerai.geraiid 
        where extract (month from tbltransaksi.tgltransaksi) = " & pilihBulan & " + 1 and  tbluser.geraiid = tblgerai.geraiid and namagerai ilike '%" & txtCariLabaGerai.Text & "%'
        group by tbluser.geraiid,namagerai ")

        makeFillDG(dgvLabaPertoko)
    End Sub

    Sub detailDashboard()

        Dim jumLabaHariIni As String = "select 
       (
       (select sum(totalharga) from tbldetailtransaksi inner join tbltransaksi on tbltransaksi.noinvoice = tbldetailtransaksi.noinvoice 
       inner join tbluser on tbluser.userid = tbltransaksi.userid 
       inner join tblgerai on tblgerai.geraiid = tbluser.geraiid 
       where (tgltransaksi between '" & Now().ToString("yyyy-MM-dd") & " 00:00' and '" & Now().ToString("yyyy-MM-dd") & " 23:59') and tblgerai.geraiid = (select geraiid from tbluser where tbluser.dusername = '" & username & "' )) 
       - 
       (select sum(jumlahbeli*prohargabeli) from tbldetailtransaksi inner join tblproduk on tblproduk.proid = tbldetailtransaksi.proid 
       inner join tbltransaksi on tbltransaksi.noinvoice = tbldetailtransaksi.noinvoice 
       inner join tbluser on tbluser.userid = tbltransaksi.userid 
       inner join tblgerai on tblgerai.geraiid = tbluser.geraiid 
       where (tgltransaksi between '" & Now().ToString("yyyy-MM-dd") & " 00:00' and '" & Now().ToString("yyyy-MM-dd") & " 23:59') and tblgerai.geraiid = (select geraiid from tbluser where tbluser.dusername = '" & username & "'))
       ) as total"
        lblDetailLaba.Text = "Rp. " & numberFormat(getValue(jumLabaHariIni, "total"))


        Dim jumlahUserGerai As String = "select count (userid) as jumlahuser from tbluser 
        inner join tblgerai on tblgerai.geraiid = tbluser.geraiid 
        where tblgerai.geraiid = (select geraiid from tbluser where tbluser.dusername = '" & username & "' )"
        lblJumlahUserGerai.Text = getValue(jumlahUserGerai, "jumlahuser")

        Dim jumlahGeraiDashboard As String = "select count(tblgerai.geraiid) as jumgerai from tblgerai"
        lblJumGeraiDash.Text = getValue(jumlahGeraiDashboard, "jumgerai")

        Dim jumTransaksiDashboard As String = "select count(tbltransaksi.noinvoice) as totaltransaksi from tbltransaksi  
        inner join tbluser on tbluser.userid = tbltransaksi.userid 
        inner join tblgerai on tblgerai.geraiid = tbluser.geraiid 
        where  (tgltransaksi between '" & Now().ToString("yyyy-MM-dd") & " 00:00' and '" & Now().ToString("yyyy-MM-dd") & " 23:59') and tblgerai.geraiid = (select geraiid from tbluser where tbluser.dusername = '" & username & "' )"
        lblJumTransaksiDashboard.Text = getValue(jumTransaksiDashboard, "totaltransaksi")

        MaterialDivider1.BackColor = Color.FromArgb(255, 128, 128)
        MaterialDivider2.BackColor = Color.FromArgb(255, 192, 192)
        MaterialDivider3.BackColor = Color.FromArgb(192, 255, 255)
        MaterialDivider4.BackColor = Color.FromArgb(255, 224, 192)
    End Sub

    Sub tampilkanTopBuy()
        If cmbFilterTopBarang.SelectedIndex = 0 Then
            dgvTopTierBenda.DataSource = getData("select distinct pronama as barang,sum(jumlahbeli) as Total from tblproduk 
               inner join tbldetailtransaksi on tblproduk.proid = tbldetailtransaksi.proid
               inner join tbltransaksi on tbltransaksi.noinvoice = tbldetailtransaksi.noinvoice 
               inner join tbluser on tbluser.userid = tbltransaksi.userid 
               inner join tblgerai on tblgerai.geraiid = tbluser.geraiid 
               where tblgerai.geraiid = (select tbluser.geraiid from tbluser where dusername = '" & username & "') and pronama ilike '%" & txtCariTopBarang.Text & "%'
               group by pronama order by total desc ")

        ElseIf cmbFilterTopBarang.SelectedIndex = 1 Then
            dgvTopTierBenda.DataSource = getData("select distinct pronama as barang,sum(jumlahbeli) as Total from tblproduk 
               inner join tbldetailtransaksi on tblproduk.proid = tbldetailtransaksi.proid
               inner join tbltransaksi on tbltransaksi.noinvoice = tbldetailtransaksi.noinvoice 
               inner join tbluser on tbluser.userid = tbltransaksi.userid 
               inner join tblgerai on tblgerai.geraiid = tbluser.geraiid 
               where tblgerai.geraiid = (select tbluser.geraiid from tbluser where dusername = '" & username & "') and pronama ilike '%" & txtCariTopBarang.Text & "%'
               group by pronama order by total asc ")
        End If
        makeFillDG(dgvTopTierBenda)
    End Sub


    ''Untuk Menu Produk''
    Sub insertKategori()
        Dim katid As String = txtKatid.Text
        Dim katnama As String = txtKategori.Text

        exc("insert into tblkategori
                    (
	                  katid,kategori,katstatus
                    ) values 
                    (
                        '" & katid & "','" & katnama & "','1'
            )")
    End Sub

    Sub insertProduk()
        Dim proid As String = txtProid.Text
        Dim pronama As String = txtNamaProduk.Text
        Dim kategori As String = cmbPilihKategori.SelectedValue
        Dim prohargajual As String = txtHargaJualProduk.Text
        Dim prohargabeli As String = txtHargaBeliProduk.Text
        Dim prostok As String = txtJumProduk.Text

        exc("insert into tblproduk (proid,kategori,pronama,prostok,prohargabeli,prohargajual,promasuk) values 
        ('" & proid & "','" & kategori & "','" & pronama & "', '" & prostok & "', '" & prohargabeli & "', '" & prohargajual & "','" & prostok & "' ) ")
    End Sub

    Sub updateProduk()
        Dim proid As String = txtProid.Text
        Dim pronama As String = txtNamaProduk.Text
        Dim kategori As String = cmbPilihKategori.SelectedValue
        Dim prohargajual As String = txtHargaJualProduk.Text
        Dim prohargabeli As String = txtHargaBeliProduk.Text
        Dim prostok As String = txtJumProduk.Text

        exc("update tblproduk set 
        proid = '" & proid & "',kategori = '" & kategori & "',pronama = '" & pronama & "',prostok = prostok + '" & prostok & "' ,prohargabeli= '" & prohargabeli & "',prohargajual='" & prohargajual & "' ,promasuk ='" & prostok & "' 
        where proid = '" & proid & "'
        ")
    End Sub

    Sub getKategori()
        cmbPilihKategori.DataSource = getData("select kategori from tblkategori")
        cmbPilihKategori.DisplayMember = "kategori"
        cmbPilihKategori.ValueMember = "kategori"
    End Sub

    Sub tampilkanDataProduk()
        dgvProduk.DataSource = getData("select proid,pronama,kategori,prostok,prohargabeli,prohargajual 
        from qproduk where pronama ilike '%" & txtCariProduk.Text & "%' ")
        dgvProduk.Columns(0).Visible = False
        dgvProduk.Columns(1).HeaderText = "Nama Produk"
        dgvProduk.Columns(2).HeaderText = "Kategori"
        dgvProduk.Columns(3).HeaderText = "Stok"
        dgvProduk.Columns(4).HeaderText = "Harga Beli"
        dgvProduk.Columns(5).HeaderText = "Harga Jual"
        makeFillDG(dgvProduk)
    End Sub

    Private Sub btnSimpanKategori_Click(sender As Object, e As EventArgs) Handles btnSimpanKategori.Click
        If dialog("Apakah yakin menambah data kategori baru ?") Then
            insertKategori()
            dialogInfo("Tambah data sukses!")
            getKategori()
            txtKatid.Text = Now.ToString("'" & stringID & "'" & "yyyyMMddHHmmss")
            txtKategori.Text = ""
        Else
            dialogInfo("Tambah data dibatalkan!")
        End If
    End Sub

    Private Sub txtSimpanDataProduk_Click(sender As Object, e As EventArgs) Handles txtSimpanDataProduk.Click
        If metode = "tambah produk" Then
            If dialog("Apakah yakin menambah data produk baru ?") Then
                insertProduk()
                dialogInfo("Tambah data sukses!")
                tampilkanDataProduk()
                txtProdukId.Text = Now.ToString("'" & proID & "'" & "yyyyMMddHHmmss")
                txtNamaProduk.Text = ""
                txtHargaBeliProduk.Text = ""
                txtHargaJualProduk.Text = ""
                txtJumProduk.Text = ""
            Else
                dialogInfo("Tambah data dibatalkan!")
            End If
        ElseIf metode = "update produk" Then
            If dialog("Apakah yakin membuat perubahan data produk ?") Then
                updateProduk()
                dialogInfo("Update data sukses!")
                tampilkanDataProduk()
                txtProdukId.Text = Now.ToString("'" & proID & "'" & "yyyyMMddHHmmss")
                txtNamaProduk.Text = ""
                txtHargaBeliProduk.Text = ""
                txtHargaJualProduk.Text = ""
                txtJumProduk.Text = ""

            Else
                dialogInfo("Update data dibatalkan!")
            End If
        End If

    End Sub

    Private Sub txtBataSimpProduk_Click(sender As Object, e As EventArgs) Handles txtBataSimpProduk.Click
        lockFormTambahProduk()
        txtProNama.Text = ""
        txtProdukId.Text = ""
        txtHarga.Text = ""
        txtJumlahBeli.Text = ""
        txtTotalHarga.Text = ""
    End Sub

    Sub switchChange()
        If MaterialSwitch1.Checked = True Then
            txtHargaJualProduk.Enabled = True
            txtHargaBeliProduk.Enabled = True
        Else
            txtHargaJualProduk.Enabled = False
            txtHargaBeliProduk.Enabled = False
        End If
    End Sub
    Private Sub btnUbahDataProduk_Click(sender As Object, e As EventArgs) Handles btnUbahDataProduk.Click
        openFormTambahProduk()
        metode = "update produk"
        txtHargaJualProduk.Enabled = False
        txtHargaBeliProduk.Enabled = False
        cmbPilihKategori.Enabled = False
        MaterialSwitch1.Enabled = True
        switchChange()
    End Sub

    Private Sub MaterialSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles MaterialSwitch1.CheckedChanged
        switchChange()
    End Sub

    Private Sub btnHapusDataProduk_Click(sender As Object, e As EventArgs) Handles btnHapusDataProduk.Click
        If Not dgvProduk.SelectedCells.Count = 1 Then
            dialogError("Harap pilih tagihan yang akan dihapus")
            Return
        End If

        Dim proid = dgvProduk.Rows(dgvProduk.SelectedCells(0).RowIndex).Cells(0).Value
        If getCount($"select proid from tbldetailtransaksi where proid = '{proid}'") > 0 Then
            dialogError("Produk tidak dapat dihapus!")
        Else
            If dialog("Apakah anda yakin untuk menghapus data produk ini ?") Then
                exc($"DELETE FROM tblproduk WHERE proid ='{proid}'")
                dialogInfo("Produk berhasil dihapus !")
                tampilkanDataProduk()
            End If
        End If

    End Sub

    Private Sub dgvProduk_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProduk.CellClick
        If (e.RowIndex >= 0) Then
            idproduk = dgvProduk.Rows(e.RowIndex).Cells(0).Value

            txtProid.Text = dgvProduk.Rows(e.RowIndex).Cells(0).Value
            txtNamaProduk.Text = dgvProduk.Rows(e.RowIndex).Cells(1).Value
            cmbPilihKategori.SelectedValue = dgvProduk.Rows(e.RowIndex).Cells(2).Value
            txtJumProduk.Text = dgvProduk.Rows(e.RowIndex).Cells(3).Value
            txtHargaBeliProduk.Text = dgvProduk.Rows(e.RowIndex).Cells(4).Value
            txtHargaJualProduk.Text = dgvProduk.Rows(e.RowIndex).Cells(5).Value
        End If
    End Sub

    Private Sub MaterialFloatingActionButton_Click(sender As Object, e As EventArgs) Handles MaterialFloatingActionButton1.Click
        openFormTambahProduk()
        MaterialSwitch1.Enabled = False
        cmbPilihKategori.Enabled = True
        metode = "tambah produk"
        txtNamaProduk.Text = ""
        txtHargaBeliProduk.Text = ""
        txtHargaJualProduk.Text = ""
        txtJumProduk.Text = ""
    End Sub

    ''Untuk menu transaksi penjualan
    Sub tampilkanPergerakanTransaksi()
        dgvTransaksiBerjalan.DataSource = getData("select tbltransaksi.tgltransaksi,tbltransaksi.noinvoice,pronama,kategori,jumlahbeli,prohargajual,totalharga from tblgerai 
        inner join tbluser on tbluser.geraiid = tblgerai.geraiid 
        inner join tbltransaksi on tbltransaksi.userid = tbluser.userid 
        inner join tbldetailtransaksi on tbltransaksi.noinvoice = tbldetailtransaksi.noinvoice 
        inner join tblproduk on tbldetailtransaksi.proid = tblproduk.proid 
        where tbltransaksi.noinvoice ilike '%" & txtCariTransaksiBerjalan.Text & "%' and tblgerai.geraiid = (select geraiid from tbluser where dusername = '" & username & "')
        order by tgltransaksi desc")

        Debug.WriteLine("select tbltransaksi.tgltransaksi,tbltransaksi.noinvoice,pronama,kategori,jumlahbeli,prohargajual,totalharga from tblgerai 
        inner join tbluser on tbluser.geraiid = tblgerai.geraiid 
        inner join tbltransaksi on tbltransaksi.userid = tbluser.userid 
        inner join tbldetailtransaksi on tbltransaksi.noinvoice = tbldetailtransaksi.noinvoice 
        inner join tblproduk on tbldetailtransaksi.proid = tblproduk.proid 
        where tbltransaksi.noinvoice ilike '%" & txtCariTransaksiBerjalan.Text & "%' and tblgerai.geraiid = (select geraiid from tbluser where dusername = '" & username & "')
        order by tgltransaksi desc")

        makeFillDG(dgvTransaksiBerjalan)
    End Sub

    Private Sub btnSimpanDataTra_Click(sender As Object, e As EventArgs) Handles btnSimpanDataTra.Click
        Dim nomornota As String = txtNoNota1.Text
        Dim tanggalTransaksi As String = dtpTransaksi1.Value

        If dialog("Apakah Anda yakin untuk membuat transaksi ?") Then
            exc("insert into tbltransaksi (noinvoice,userid,tgltransaksi) values 
            ('" & nomornota & "', (select tbluser.userid from tbluser where dusername = '" & username & "') ,'" & tanggalTransaksi & "' ) ")

            dialogInfo("Buat transaksi sukses!")
            txtNoNota1.Text = Now.ToString("'" & noInvo & "'" & "yyyyMMddHHmmss")

            dgvDataTransaksiSementara.DataSource = Nothing
            dgvDataTransaksiSementara.Refresh()

        Else
            dialogInfo("Hapus akun batal !")
        End If
    End Sub

    Sub cariDataBelanja()
        dgvDataTransaksiSementara.DataSource = getData("select tbldetailtransaksi.noinvoice,tblproduk.pronama,tblproduk.prohargajual ,jumlahbeli,totalharga from 
        tbldetailtransaksi inner join tblproduk on tblproduk.proid = tbldetailtransaksi.proid 
        where noinvoice = (select noinvoice from tbltransaksi order by noinvoice desc limit 1) and tblproduk.pronama ilike  '%" & txtCariDataBelanja.Text & "%' ")

        dgvDataTransaksiSementara.Columns(0).HeaderText = "No Struk"
        dgvDataTransaksiSementara.Columns(1).HeaderText = "Nama Produk"
        dgvDataTransaksiSementara.Columns(2).HeaderText = "Harga Jual"
        dgvDataTransaksiSementara.Columns(3).HeaderText = "Jumlah Beli"
        dgvDataTransaksiSementara.Columns(4).HeaderText = "Total Harga"
    End Sub

    Private Sub btnSimpanDataTransaksi_Click(sender As Object, e As EventArgs) Handles btnSimpanDataTransaksi.Click
        Dim proId As String = txtProdukId.Text
        Dim jumlahBeli As String = txtJumlahBeli.Text
        Dim totalHarga As Double = toDouble(txtTotalHarga.Text)
        If dialog("Apakah yakin menambah data transaksi ?") Then
            exc("insert into tbldetailtransaksi (noinvoice,proid,jumlahbeli,totalharga) values 
            ((select tbltransaksi.noinvoice from tbltransaksi order by noinvoice desc limit 1),'" & proId & "','" & jumlahBeli & "', '" & totalHarga & "' ) ")
            dialogInfo("Tambah data sukses!")
            txtProNama.Text = ""
            txtProdukId.Text = ""
            txtHarga.Text = ""
            txtJumlahBeli.Text = ""
            txtTotalHarga.Text = ""

            cariDataBelanja()

            exc("update tblproduk set prostok = prostok - (select coalesce(sum(tbldetailtransaksi.jumlahbeli),0) from tbldetailtransaksi where tbldetailtransaksi.proid = tblproduk.proid)")
        Else
            dialogInfo("Tambah data dibatalkan!")
        End If
        tampilkanPergerakanTransaksi()
        detailDashboard()
    End Sub

    Private Sub btnSimpanDataDetailTra_Click(sender As Object, e As EventArgs)

        Dim proId As String = txtProdukId.Text
        Dim jumlahBeli As String = txtJumlahBeli.Text
        Dim totalHarga As Double = toDouble(txtTotalHarga.Text)

        If dialog("Apakah yakin menambah data transaksi ?") Then
            exc("insert into tbldetailtransaksi (noinvoice,proid,jumlahbeli,totalharga) values 
            (,'" & proId & "','" & jumlahBeli & "', '" & totalHarga & "' ) ")
            dialogInfo("Tambah data sukses!")

            txtProNama.Text = ""
            txtProdukId.Text = ""
            txtHarga.Text = ""
            txtJumlahBeli.Text = ""
            txtTotalHarga.Text = ""

            exc("update tblproduk set prostok = prostok - (select coalesce(sum(tbldetailtransaksi.jumlahbeli),0) from tbldetailtransaksi where tbldetailtransaksi.proid = tblproduk.proid)")
        Else
            dialogInfo("Tambah data dibatalkan!")
        End If
    End Sub

    Private Sub btnCariDataProduk_Click(sender As Object, e As EventArgs)
        MenuCari.menu = "Cari Produk"
        MenuCari.ShowDialog()
        MenuCari.Dispose()
    End Sub

    Sub totalHarga()
        Dim hargaBarang As Double = toDouble(txtHarga.Text)
        Dim jumlahBarang As Double = toDouble(txtJumlahBeli.Text)

        txtTotalHarga.Text = (hargaBarang * jumlahBarang).ToString
    End Sub

    Private Sub btnCariDataProduk_Click_1(sender As Object, e As EventArgs) Handles btnCariDataProduk.Click
        MenuCari.menu = "Cari Produk"
        MenuCari.ShowDialog()
        MenuCari.Dispose()

    End Sub


    ''Untuk menu menampilkan data gerai
    Sub tampilkanGerai()
        dgvLokasiGerai.DataSource = getData("select namagerai,alamat,notelp from tblgerai where namagerai ilike '%" & txtCariLokasi.Text & "%' ")
        dgvLokasiGerai.Columns(0).HeaderText = "Nama Gerai"
        dgvLokasiGerai.Columns(1).HeaderText = "Alamat"
        dgvLokasiGerai.Columns(2).HeaderText = "Nomor Telepon"
        makeFillDG(dgvLokasiGerai)
    End Sub


    ''Untuk menu hak akses
    Sub showComboBoxHakAkses()
        cmb_HakAkses.DataSource = getData("select hakakses from tblhakakses order by hakakses desc")
        cmb_HakAkses.DisplayMember = "hakakses"
        cmb_HakAkses.ValueMember = "hakakses"

    End Sub

    Sub showDataAkses()
        dgvDataHakses.DataSource = getData("select hakakses,hakdeskripsi from tblhakakses  where hakakses ilike '%" & txtCariHakAkses.Text & "%' order by hakakses asc")
        lblJumAkses.Text = "Jumlah Group : " & dgvDataHakses.Rows.Count
        makeFillDG(dgvDataHakses)
    End Sub

    Private Sub btnSimpanHakAkses_Click(sender As Object, e As EventArgs) Handles btnSimpanHakAkses.Click
        Dim levelHakAkses As String = txtLevelHakAkses.Text
        Dim levelDeskripsi As String = txtDeskHakAkses.Text

        If getCount("select hakakses from tblhakakses where hakakses = ' " & levelHakAkses & " '") = 0 Then
            If dialog("Yakin nambah data?") Then

                Dim baris As Integer = 1
                exc("insert into tblhakakses (hakakses,hakdeskripsi) values ('" & levelHakAkses & "' ,'" & levelDeskripsi & "' )")

                For Each menu As String In clb_HakAkses.Items
                    exc("insert into tblmenu(hakakses,menucaption,menutag,flag) values('" & levelHakAkses & "', '" & menu & "','" & baris.ToString & "',TRUE )")
                    baris += 1
                Next

            End If
        Else
            dialogError("data ganda!")
        End If

        showComboBoxHakAkses()
        showDataAkses()
        clb_HakAkses.Visible = False

    End Sub

    Private Sub btnTampilkanMenu_Click(sender As Object, e As EventArgs) Handles btnTampilkanMenu.Click
        clb_HakAkses.Visible = True

        Dim sql As String = "select * from tblmenu where hakakses = '" & cmb_HakAkses.SelectedValue & "'"
        Dim tblmenu As DataTable = getData(sql)


        For Each row As DataRow In tblmenu.Rows
            If row.Item("flag") Then
                clb_HakAkses.SetItemChecked(CInt(row.Item("menutag") - 1), True)
            Else
                clb_HakAkses.SetItemChecked(CInt(row.Item("menutag") - 1), False)
            End If
        Next
    End Sub

    Private Sub cmb_HakAkses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_HakAkses.SelectedIndexChanged
        clb_HakAkses.Visible = False
    End Sub

    Private Sub btnHapusAkses_Click(sender As Object, e As EventArgs) Handles btnHapusAkses.Click
        If String.IsNullOrEmpty(idAkses) Then
            dialogError("Harap pilih data terlebih dahulu !")
            Return
        Else
            If dialog("Apakah Anda yakin untuk menghapus group akses ?") Then
                If getCount("select hakakses from tbluser where hakakses = '" & selectId & "' ") > 0 Then
                    dialogError("Group hak akses tidak dapat dihapus !")
                    Return
                Else
                    exc("delete from tblhakakses where hakakses = '" & selectId & "' ")
                    exc("delete from tblmenu where hakakses = '" & selectId & "' ")
                    showDataAkses()
                    dialogInfo("Hapus hak akses berhasil")
                End If
            Else
                dialogInfo("Hapus batal !")
            End If
        End If
        showComboBoxHakAkses()
    End Sub

    Private Sub btnSimpanUpdateHakAkses_Click(sender As Object, e As EventArgs) Handles btnSimpanUpdateHakAkses.Click
        If clb_HakAkses.Visible = False Then
            dialogError("Pilih hak akses yang akan diupdate terlebih dahulu")
        Else

            Dim baris = 0

            For Each menu As String In clb_HakAkses.Items
                Dim checked = False
                Dim menuTag = baris + 1

                Dim hakakses As String = cmb_HakAkses.SelectedValue

                If clb_HakAkses.GetItemChecked(baris) Then
                    checked = True

                End If
                exc("update tblmenu set flag = '" & checked.ToString & "' where hakakses = '" & hakakses & "' and menutag = '" & menuTag & "' ")

                baris += 1

            Next
            dialogInfo("Update menu berhasil !")
            dialogInfo("Silahkan restart aplikasi untuk memperbarui menu !")
            showComboBoxHakAkses()

        End If
    End Sub

    ''Untuk menu user

    Sub lockFormUser()
        MaterialCard22.Enabled = False
    End Sub

    Sub showCmbBoxHakAkses()
        cmbGroupAkses.DataSource = getData("select hakakses from tblhakakses order by hakakses asc")
        cmbGroupAkses.DisplayMember = "hakakses"
        cmbGroupAkses.ValueMember = "hakakses"
    End Sub

    Sub tampilkanDataUser()
        dgvDataUser.DataSource = getData("select userid,dusername,dpassword,pemilik_akun,hakakses,tblgerai.geraiid from tbluser 
        inner join tblgerai on tblgerai.geraiid = tbluser.geraiid 
        where tblgerai.geraiid = (select tbluser.geraiid from tbluser where dusername = '" & username & "') and pemilik_akun ilike '%" & txtCariDataUser.Text & "%'")
        dgvDataUser.Columns(0).HeaderText = "ID User"
        dgvDataUser.Columns(1).HeaderText = "Username"
        dgvDataUser.Columns(2).HeaderText = "Password"
        dgvDataUser.Columns(3).HeaderText = "Pemilik Akun"
        dgvDataUser.Columns(4).HeaderText = "Hak Akses"
        dgvDataUser.Columns(5).HeaderText = "ID Gerai"

        makeFillDG(dgvDataUser)
        lblJumlahUser.Text = "Jumlah User : " & dgvDataUser.Rows.Count
    End Sub

    Private Sub btnSimpanAkun_Click(sender As Object, e As EventArgs) Handles btnSimpanAkun.Click
        If String.IsNullOrEmpty(txtNamaLengkap.Text) Then
            dialogError("Nama lengkap masih kosong !")
            Return
        ElseIf String.IsNullOrEmpty(txtUsername.Text) Then
            dialogError("Nama user user masih kosong !")
            Return
        ElseIf String.IsNullOrEmpty(txtPassword.Text) Then
            dialogError("Password masih kosong !")
            Return
        ElseIf String.IsNullOrEmpty(txtUlangiPassword.Text) Then
            dialogError("Harap ketikan ulang password Anda !")
            Return
        ElseIf txtPassword.Text <> txtUlangiPassword.Text Then
            dialogError("Password anda tidak sama !")
            Return
        Else
            Dim akses As String = cmbGroupAkses.SelectedValue

            Dim idUser As String = txtIDuser.Text
            Dim namaLengkap As String = txtNamaLengkap.Text
            Dim namaUser As String = txtUsername.Text
            Dim password As String = txtPassword.Text
            Dim ulangiPass As String = txtUlangiPassword.Text
            Dim geraiId As String = txtKodeGerai.Text

            If metode = "tambah user" Then
                If dialog("Apakah yakin membuat data user baru?") Then
                    If getCount("select dusername from tbluser where dusername = '" & namaUser & "' ") = 0 Then
                        exc("insert into tbluser 
                    (userid,dusername,dpassword,pemilik_akun,hakakses,geraiid) 
                values
                    (
                        '" & idUser & "',
                        '" & namaUser & "',
                        '" & password & "',
                        '" & namaLengkap & "',
                        '" & akses & "',
                        '" & geraiId & "'
                    ) 
                    ")

                        txtNamaLengkap.Text = ""
                        txtUsername.Text = ""
                        txtPassword.Text = ""
                        txtUlangiPassword.Text = ""

                        txtIDuser.Text = Now.ToString("'" & uID & "'" & "yyyyMMddHHmmss")

                        dialogInfo("Pembuatan user akun sukses !")
                    Else
                        dialogError("Username ada yang sama dengan user lain !")
                        Return

                    End If
                End If
            ElseIf metode = "update user" Then
                If dialog("Apakah yakin memperbarui data user ?") Then
                    exc("update tbluser set 
    `               dusername =  '" & namaUser & "', dpassword = '" & password & "', pemilik_akun = '" & namaLengkap & "',geraiid = '" & geraiId & "'
                    where userid = '" & idUser & "'
                    ")
                    txtNamaLengkap.Text = ""
                    txtUsername.Text = ""
                    txtPassword.Text = ""
                    txtUlangiPassword.Text = ""

                    txtIDuser.Text = Now.ToString("'" & uID & "'" & "yyyyMMddHHmmss")
                    dialogInfo("Pembaruan user akun sukses !")

                End If
            End If
        End If
        detailDashboard()
        tampilkanDataUser()

    End Sub

    Private Sub dgvDataUser_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDataUser.CellClick
        If (e.RowIndex >= 0) Then

            selectIdUser = dgvDataUser.Rows(e.RowIndex).Cells(0).Value

            txtIDuser.Text = dgvDataUser.Rows(e.RowIndex).Cells(0).Value
            txtUsername.Text = dgvDataUser.Rows(e.RowIndex).Cells(1).Value
            txtPassword.Text = dgvDataUser.Rows(e.RowIndex).Cells(2).Value
            txtUlangiPassword.Text = dgvDataUser.Rows(e.RowIndex).Cells(2).Value
            txtNamaLengkap.Text = dgvDataUser.Rows(e.RowIndex).Cells(3).Value
            cmbGroupAkses.SelectedValue = dgvDataUser.Rows(e.RowIndex).Cells(4).Value

            txtKodeGerai.Text = dgvDataUser.Rows(e.RowIndex).Cells(5).Value

            selectId = selectIdUser
        End If
    End Sub

    Private Sub btnHapusDataUser_Click(sender As Object, e As EventArgs) Handles btnHapusDataUser.Click
        If String.IsNullOrEmpty(selectId) Then
            dialogError("Harap pilih data User terlebih dahulu di tabel atas !")
            Return
        Else
            If getCount("select userid from tbltransaksi where userid =  '" & selectIdUser & "'") > 0 Then
                dialogError("User tidak dapat dihapus !")
            Else
                If dialog("Apakah Anda yakin untuk menghapus Akun Pengguna / User ?") Then
                    If exc("delete from tbluser where userid = '" & selectIdUser & "'") Then
                        dialogInfo("Hapus berhasil !")
                    Else
                        dialogError("Hapus gagal !")
                        Return
                    End If

                Else
                    dialogInfo("Hapus akun batal !")
                End If
                tampilkanDataUser()
            End If
        End If
    End Sub

    Private Sub dgvDataHakses_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDataHakses.CellClick
        If (e.RowIndex >= 0) Then
            selectId = dgvDataHakses.Rows(e.RowIndex).Cells(0).Value
            idAkses = selectId
        End If
    End Sub

    Private Sub btnCariPenempatanGerai_Click(sender As Object, e As EventArgs) Handles btnCariPenempatanGerai.Click
        MenuCari.menu = "Cari Penempatan Gerai"
        MenuCari.ShowDialog()
        MenuCari.Dispose()
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txtUlangiPassword.Password = False
            txtPassword.Password = False
        ElseIf CheckBox1.Checked = False Then
            txtUlangiPassword.Password = True
            txtPassword.Password = True
        End If
    End Sub


    Private Sub MenuUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.AddFormToManage(Me)

        SkinManager.Theme = MaterialSkinManager.Themes.LIGHT
        SkinManager.ColorScheme = New ColorScheme(Primary.Blue300, Primary.Blue400, Primary.BlueGrey500, Accent.LightBlue700, TextShade.BLACK)
        tampilkanGerai()
        tampilkanDataProduk()

        getKategori()
        showComboBoxHakAkses()
        showDataAkses()
        showCmbBoxHakAkses()
        setRule()
        tampilkanDataUser()
        detailDashboard()
        tampilkanTopBuy()
        tampilkanPergerakanTransaksi()


        tampilkanDataLabaMasing2Gerai()
        txtKatid.Text = Now.ToString("'" & stringID & "'" & "yyyyMMddHHmmss")
        txtProid.Text = Now.ToString("'" & proID & "'" & "yyyyMMddHHmmss")
        txtNoNota1.Text = Now.ToString("'" & noInvo & "'" & "yyyyMMddHHmmss")
        txtIDuser.Text = Now.ToString("'" & uID & "'" & "yyyyMMddHHmmss")

        clb_HakAkses.Visible = False
        cariDataBelanja()

        lockFormTambahProduk()
        lockFormUser()
    End Sub

    Private Sub txtCariLokasi_TextChanged(sender As Object, e As EventArgs) Handles txtCariLokasi.TextChanged
        tampilkanGerai()
    End Sub


    Private Sub txtHargaJualProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHargaJualProduk.KeyPress
        onlyNumber(e)
    End Sub

    Private Sub txtHargaBeliProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHargaBeliProduk.KeyPress
        onlyNumber(e)
    End Sub

    Private Sub txtJumProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumProduk.KeyPress
        onlyNumber(e)
    End Sub

    Sub lockFormTambahProduk()
        MaterialCard6.Enabled = False
    End Sub

    Sub openFormTambahProduk()
        MaterialCard6.Enabled = True
    End Sub


    Private Sub txtCariProduk_TextChanged(sender As Object, e As EventArgs) Handles txtCariProduk.TextChanged
        tampilkanDataProduk()
    End Sub

    Private Sub txtTotalHarga_TextChanged(sender As Object, e As EventArgs)
        totalHarga()
    End Sub

    Private Sub txtJumlahBeli_TextChanged(sender As Object, e As EventArgs)
        totalHarga()
    End Sub

    Private Sub txtCariHakAkses_TextChanged(sender As Object, e As EventArgs) Handles txtCariHakAkses.TextChanged
        showDataAkses()
    End Sub

    Private Sub txtCariDataUser_TextChanged(sender As Object, e As EventArgs) Handles txtCariDataUser.TextChanged
        tampilkanDataUser()
    End Sub

    Private Sub txtJumlahBeli_TextChanged_1(sender As Object, e As EventArgs) Handles txtJumlahBeli.TextChanged
        totalHarga()
    End Sub

    Private Sub txtTotalHarga_TextChanged_1(sender As Object, e As EventArgs) Handles txtTotalHarga.TextChanged
        totalHarga()
    End Sub

    Private Sub txtCariDataBelanja_TextChanged(sender As Object, e As EventArgs) Handles txtCariDataBelanja.TextChanged
        cariDataBelanja()
    End Sub

    Private Sub cmbGerai_SelectedIndexChanged(sender As Object, e As EventArgs)
        tampilkanDataLabaMasing2Gerai()
    End Sub

    Private Sub cmbBulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBulan.SelectedIndexChanged
        tampilkanDataLabaMasing2Gerai()
    End Sub

    Private Sub txtCariLabaGerai_TextChanged(sender As Object, e As EventArgs) Handles txtCariLabaGerai.TextChanged
        tampilkanDataLabaMasing2Gerai()
    End Sub

    Private Sub cmbFilterTopBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterTopBarang.SelectedIndexChanged
        tampilkanTopBuy()
    End Sub

    Private Sub txtCariTopBarang_TextChanged(sender As Object, e As EventArgs) Handles txtCariTopBarang.TextChanged
        tampilkanTopBuy()
    End Sub

    Private Sub MaterialFloatingActionButton2_Click(sender As Object, e As EventArgs) Handles MaterialFloatingActionButton2.Click
        MaterialCard22.Enabled = True
        metode = "tambah user"

        txtNamaLengkap.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtUlangiPassword.Text = ""

        txtIDuser.Text = Now.ToString("'" & uID & "'" & "yyyyMMddHHmmss")

    End Sub

    Private Sub MaterialButton9_Click(sender As Object, e As EventArgs) Handles MaterialButton9.Click
        lockFormUser()


        txtNamaLengkap.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtUlangiPassword.Text = ""

        txtIDuser.Text = Now.ToString("'" & uID & "'" & "yyyyMMddHHmmss")

    End Sub

    Private Sub btnUbahDataUser_Click(sender As Object, e As EventArgs) Handles btnUbahDataUser.Click
        MaterialCard22.Enabled = True
        metode = "update user"
        txtIDuser.Enabled = False
        cmbGroupAkses.Enabled = False

    End Sub

    Private Sub txtCariTransaksiBerjalan_TextChanged(sender As Object, e As EventArgs) Handles txtCariTransaksiBerjalan.TextChanged
        tampilkanPergerakanTransaksi()
    End Sub
End Class