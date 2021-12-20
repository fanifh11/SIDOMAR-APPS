Imports System.Data.OleDb
Imports System.Net.Mail
Imports System.Text
Imports System.Security.Cryptography
Imports Npgsql
Imports System.IO

Module Modul
    'ITBrainPOSTheToko25122020ONLINE
    Public kodeaplikasi As String = "KoperasiSP" '25122020  khusus taufiq tambahi ini
    Public versi As String = "1.0.0"
    Public fullver As Boolean = False
    Dim release As Boolean = True
    Public pro As Boolean = True

    Public ext As String = "mdb"
    Public toko As String = "UD. Taufik"
    Public alamat As String = "Buduran"
    Public no As String = "0888"
    ' Dim kodeaplikasi As String = "ITBrainPOSTheToko"
    Dim Conn As NpgsqlConnection
    Dim CMD As NpgsqlCommand
    Dim row As NpgsqlDataReader
    Dim DA As NpgsqlDataAdapter
    Dim DT As DataTable
    Dim QUERY As String


    Sub setLogo(pb As PictureBox)
        Dim productImageByte As Byte() = Nothing
        Dim sql = "SELECT pg_typeof(""logo"") from tblidentitas limit 1;"
        'If getValue(sql, "pg_typeof").ToString = "25" Then
        '    exc("ALTER TABLE tblidentitas DROP COLUMN logo;
        '            ALTER TABLE tblidentitas ADD COLUMN logo bytea;")
        'End If

        Dim gambar As Byte()
        Try
            gambar = getValue("select logo from tblidentitas where idkoperasi=1", "logo")
        Catch ex As Exception

        End Try

        productImageByte = (gambar)
        Dim productImageStream As MemoryStream
        If Not IsNothing(productImageByte) Then
            productImageStream = New System.IO.MemoryStream(productImageByte)
        Else
            Dim pgFileStream As FileStream = New FileStream(Application.StartupPath & "\koperasi.png", FileMode.Open, FileAccess.Read)
            Dim pgReader As BinaryReader = New BinaryReader(New BufferedStream(pgFileStream))
            gambar = pgReader.ReadBytes(Convert.ToInt32(pgFileStream.Length))
            productImageStream = New System.IO.MemoryStream(gambar)
        End If
        pb.Image = Image.FromStream(productImageStream)
    End Sub

    Function uploadIamge(file As String)
        koneksi()
        Dim pgFileStream As FileStream = New FileStream(file, FileMode.Open, FileAccess.Read)
        Dim pgReader As BinaryReader = New BinaryReader(New BufferedStream(pgFileStream))
        Dim ImgByteA As Byte() = pgReader.ReadBytes(Convert.ToInt32(pgFileStream.Length))


        Try
            Dim sql As String = "UPDATE tblidentitas set logo = @Image where idkoperasi =1"
            CMD = New NpgsqlCommand(sql, Conn)
            Dim param As NpgsqlParameter = CMD.CreateParameter
            param.ParameterName = "@Image"
            param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea
            param.Value = ImgByteA
            CMD.Parameters.Add(param)

            CMD.ExecuteNonQuery()

            Return True
        Catch ex As Exception

            Return False
        Finally
            closeKoneksi()
        End Try


    End Function



    'fungsi untuk write file .ini
    Public Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
    Alias "WritePrivateProfileStringW" (ByVal lpSection As String, ByVal lpParamName As String,
    ByVal lpParamVal As String, ByVal lpFileName As String) As Int32

    'procedure untuk write .ini
    Public Sub writeini(ByVal iniFilename As String, ByVal section As String, ByVal ParamName As String, ByVal ParamVal As String)
        'menanggil fungsi WritePrivateProfilString untuk write file .ini
        Dim result As Integer = WritePrivateProfileString(section, ParamName, ParamVal, iniFilename)
    End Sub
    'function untuk read file .ini
    Public Declare Unicode Function GetPrivateProfileString Lib "kernel32" _
    Alias "GetPrivateProfileStringW" (ByVal lpSection As String, ByVal lpParamName As String,
    ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Int32,
    ByVal lpFilename As String) As Int32

    'function untuk read file .ini
    Public Function readini(ByVal iniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamDefault As String) As String
        Dim ParamVal As String = Space$(1024)
        Dim LenParamVal As Long = GetPrivateProfileString(Section, ParamName, ParamDefault, ParamVal, Len(ParamVal), iniFileName)
        'mengembalikan nilai yang sudah didapatkan
        readini = Strings.Left(ParamVal, LenParamVal)
    End Function

    Function RandString(n As Long) As String
        'Assumes that Randomize has been invoked by caller
        Dim i As Long, j As Long, m As Long, s As String, pool As String
        pool = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
        m = Len(pool)
        For i = 1 To n
            j = 1 + Int(m * Rnd())
            s = s & Mid(pool, j, 1)
        Next i
        RandString = s
    End Function

    Function getLocationDatabase() As String

        Dim default_database = "127.0.0.1"
        '    Dim folder As String = Application.StartupPath & "\data\setup.ini"
        Dim folder As String = kodeaplikasi + "posgre"

        Dim key = "DBFOLDER"
        Dim value = default_database
        Dim reg As String = regRead(folder, key)
        If String.IsNullOrEmpty(reg) Then
            regEdit(folder, key, value)
        End If
        reg = regRead(folder, key)
        Return reg
    End Function

    Sub setLocationDatabase(dir As String)
        Dim folder As String = kodeaplikasi + "posgre"
        regEdit(folder, "DBFOLDER", dir)

    End Sub

    Function getCPUNumber() As String
        Dim wmi As Object = GetObject("WinMgmts:")

        ' Get the "base boards" (mother boards).
        Dim serial_numbers As String = ""
        Dim mother_boards As Object =
        wmi.InstancesOf("Win32_BaseBoard")
        For Each board As Object In mother_boards
            serial_numbers &= ", " & board.SerialNumber
        Next board

        If serial_numbers.Length > 0 Then serial_numbers =
        serial_numbers.Substring(2)
        serial_numbers = serial_numbers.Replace("/", "")
        serial_numbers = serial_numbers.Trim
        If serial_numbers.Length > 16 Then

            serial_numbers = serial_numbers.Substring(0, 16)
        ElseIf serial_numbers.Length < 16 Then

            serial_numbers = serial_numbers & RandString(16 - serial_numbers.Length)
        End If



        If serial_numbers.Length < 16 Then
            serial_numbers = RandString(16)
        End If
        Return seperatorString(serial_numbers, "-", 4).ToUpper()
    End Function

    Function seperatorString(word As String, seperator As String, seplimit As Integer) As String
        Dim output As String = ""
        For index As Integer = 0 To word.Length - 1
            output = output & word(index).ToString.ToUpper
            If (index + 1) Mod seplimit = 0 And Not index = word.Length - 1 Then
                output = output & seperator
            End If
        Next
        Return output
    End Function

    Function hashString(ByVal inputString) As String
        Dim sha1 As SHA1 = SHA1Managed.Create()
        Dim sha256 As SHA256 = SHA256Managed.Create()
        Dim md5s As MD5 = MD5.Create
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString + kodeaplikasi + "ONLINE")
        Dim hash As Byte() = sha1.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return stringBuilder.ToString().Substring(0, 16)
    End Function

    Function hash256(ByVal inputString) As String
        Dim sha256 As SHA256 = SHA256Managed.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
        Dim hash As Byte() = sha256.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()
        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return stringBuilder.ToString
    End Function


    Function hashStringWithoutCut(ByVal inputString) As String
        Dim sha1 As SHA1 = SHA1Managed.Create()

        Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)
        Dim hash As Byte() = sha1.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return stringBuilder.ToString()
    End Function

    Function getLogo() As String
        Dim gambar As String = getValue("select idkoperasi, namatoko, alamatoko, notoko, caption1, caption2, caption3, printstruk, logo, kota from tblidentitas where idkoperasi=1", "logo")
        If String.IsNullOrWhiteSpace(gambar) Then
            gambar = Application.StartupPath & "\itb.png"
        ElseIf Not System.IO.File.Exists(gambar) Then
            gambar = Application.StartupPath & "\itb.png"
        End If
        Return gambar
    End Function

    Sub regEdit(subReg As String, key As String, value As String)

        My.Computer.Registry.CurrentUser.CreateSubKey(subReg)
        ' Change MyTestKeyValue to This is a test value. 
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\" & subReg,
          key, value)
    End Sub
    Function regRead(subReg As String, key As String) As String

        My.Computer.Registry.CurrentUser.CreateSubKey(subReg)
        ' Change MyTestKeyValue to This is a test value. 
        Return My.Computer.Registry.GetValue("HKEY_CURRENT_USER\" & subReg, key, "")
    End Function

    Function limit(table As String) As Boolean
        If Not pro Then
            Dim tables As DataTable = checkLisence()
            If Not IsNothing(tables) Then
                If seperatorString(hashString(tables.Rows(0).Item("data")), "-", 4) = tables.Rows(0).Item("value") Then
                    'Form1.setnama()
                    pro = True
                End If
            End If
        End If
        If pro Then
            Return True
        ElseIf getCount("select * from " & table) > 5 Then
            dialogError("Aplikasi Melampui batas inputan, silahkan aktivasi aplikasi ini")

            'Aktivasi.ShowDialog()
            'Aktivasi.Dispose()
            Return False
        Else
            Return True
        End If
    End Function


    Function getRegProduk() As String
        Dim reg As String = ""
        Dim tables As DataTable = checkLisence()
        If Not IsNothing(tables) Then
            reg = tables.Rows(0).Item("data")
        End If
        Return reg
    End Function

    Function getLisence() As String
        Dim folder As String = hashString(kodeaplikasi + "ONLINE")

        Dim key1 As String = regRead("27" & folder, "$$$")
        Dim key2 As String = regRead(folder & "12", "#$#")

        Dim key3 As String = regRead("20" & folder, "&&&")
        Dim key4 As String = regRead(folder & "01", "*&*")

        Return key1 & "-" & key2 & "-" & key3 & "-" & key4
    End Function


    Function checkLisence()
        Try
            If IsNothing(Conn) Then

            End If
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            DT = New DataTable
            DA = New NpgsqlDataAdapter("SELECT data,value FROM ""private"".""tbltoko""", Conn)
            DA.Fill(DT)
            Return DT
        Catch ex As Exception
            Return Nothing
        Finally
            Conn.Close()
            DA.Dispose()
            DT.Dispose()
        End Try
    End Function

    Function setLicense(isi As String())
        Dim folder As String = hashString(kodeaplikasi + "ONLINE")
        regEdit("27" & folder, "$$$", isi(0))
        regEdit(folder & "12", "#$#", isi(1))
        regEdit("20" & folder, "&&&", isi(2))
        regEdit(folder & "01", "*&*", isi(3))
    End Function


    Sub copyFile(oldDir As String, newDir As String, Optional pesan As String = "Operasi Berhasil dilakukan")
        If System.IO.File.Exists(oldDir) = True Then
            Try
                System.IO.File.Copy(oldDir, newDir, True)
            Catch ex As Exception
                dialogError("Tutup file terlebih dahulu")
            End Try


            dialogInfo(pesan)
        Else
            MessageBox.Show("Galat", "File tidak ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Sub writeDebug(ByVal x As String)
        Dim path As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim FILE_NAME As String = path & "\Solusi.txt"

        If System.IO.File.Exists(FILE_NAME) = False Then
            System.IO.File.Create(FILE_NAME).Dispose()

            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine("Untuk mengatasi permasalahan ini silahkan install atau download plugin tambahan dari Microsoft Access yang telah tersedia.")
            objWriter.WriteLine("Link : https://www.microsoft.com/en-us/download/confirmation.aspx?id=13255")
            objWriter.Close()
        End If

        Try
            System.Diagnostics.Process.Start("notepad.exe", FILE_NAME)
        Catch ex As Exception

        End Try

    End Sub

    Function clearKoneksi() As Boolean
        Try
            Dim location As String = getLocationDatabase()
            Dim Query32 = "Server=" & location & ";Port=5433;Database=DBSIDOMAR;username=postgres;password=fanixer123"
            Conn = New NpgsqlConnection(Query32)
            If Conn.State = Conn.State.Closed Then
                Conn.Open()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub koneksi()

        Dim location As String = getLocationDatabase()


        Dim Query32 = "Server=" & location & ";Port=5433;Database=DBSIDOMAR;username=postgres;password=fanixer123"
        QUERY = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & location & ";Jet OLEDB:Database Password=itbrain1milyar;"
        Try
            If IsNothing(Conn) Then
                Conn = New NpgsqlConnection(Query32)

            End If
            If Conn.State = Conn.State.Closed Then
                Conn.Open()
            End If

        Catch ex As Exception


        End Try

    End Sub

    Sub closeKoneksi()
        Conn.Close()
        'CMD.Dispose()
    End Sub

    Function getData(sql As String)
        koneksi()
        Try
            DT = New DataTable
            DA = New NpgsqlDataAdapter(sql, Conn)
            ' CMD = New NpgsqlCommand(sql, Conn)
            'DA.SelectCommand = CMD

            DA.Fill(DT)
            Return DT
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Return New DataTable
        Finally
            closeKoneksi()
            DT.Dispose()
            DA.Dispose()
        End Try
    End Function

    Function getCount(sql As String)
        koneksi()
        Try
            DA = New NpgsqlDataAdapter
            DT = New DataTable
            CMD = New NpgsqlCommand(sql, Conn)
            DA.SelectCommand = CMD
            DA.Fill(DT)
            Return DT.Rows.Count

        Catch ex As Exception

            Return 0
        Finally
            closeKoneksi()
        End Try
    End Function

    Function exc(sql As String)
        koneksi()
        Try

            CMD = New NpgsqlCommand(sql, Conn)
            CMD.ExecuteNonQuery()

            Return sql

        Catch ex As Exception
            Debug.WriteLine(sql)
            MsgBox(ex.Message)
            Return False
        Finally
            closeKoneksi()
        End Try
    End Function

    Function excQuerry(sql As String)
        koneksi()
        Try

            CMD = New NpgsqlCommand(sql, Conn)
            CMD.ExecuteReader()

            Return True

        Catch ex As Exception
            Debug.WriteLine(sql)
            MsgBox(ex.Message)
            Return False
        Finally
            closeKoneksi()
        End Try
    End Function

    Function getValue(sql As String, col As String)
        koneksi()

        Try

            CMD = New NpgsqlCommand(sql, Conn)
            row = CMD.ExecuteReader
            row.Read()
            If IsDBNull(row.Item(col)) Then
                Return ""
            Else

                Return row.Item(col)
            End If


        Catch ex As Exception

            Return ""
        Finally
            closeKoneksi()
        End Try
    End Function

    Function getRow(sql As String)
        koneksi()
        Try

            CMD = New NpgsqlCommand(sql, Conn)
            row = CMD.ExecuteReader
            row.Read()
            If IsDBNull(row) Then
                Return Nothing
            Else

                Return row
            End If


        Catch ex As Exception
            Return ""
        Finally
            closeKoneksi()
        End Try
    End Function


    Sub clearForm(gb As GroupBox)
        For Each ct As Control In gb.Controls
            If TypeOf ct Is TextBox Then
                CType(ct, TextBox).Text = ""
            End If

            If TypeOf ct Is PictureBox Then
                CType(ct, PictureBox).ImageLocation = Nothing
            End If

            If TypeOf ct Is CheckBox Then
                CType(ct, CheckBox).Checked = False
            End If

            If TypeOf ct Is ComboBox Then
                Try
                    CType(ct, ComboBox).SelectedIndex = 0
                Catch ex As Exception

                End Try

            End If
        Next
    End Sub

    Function unnumberFormat(te As String) As String

        Try
            te = te.Replace(".", "")
            te = te.Replace(",", "")
        Catch ex As Exception

        End Try
        Return te
    End Function

    Function toDouble(te As String) As Double

        Dim uang As Double
        Try
            uang = Double.Parse(te)
        Catch ex As Exception
            uang = 0
        End Try

        Return uang
    End Function

    Function numberFormat(te As String)
        Dim uang As Double
        Try
            uang = Double.Parse(te)
        Catch ex As Exception
            uang = 0
        End Try
        Dim hasil As String = uang.ToString("N1")
        If Len(hasil) > 2 Then
            Return hasil.Substring(0, hasil.Count - 2)
        Else
            Return hasil
        End If

    End Function

    Function numberFor(te As String)
        Dim uang As Double
        Try
            uang = Double.Parse(te)
        Catch ex As Exception
            uang = 0
        End Try
        Return uang.ToString("C2")
    End Function
    Function adaKosong(gb As GroupBox)
        Dim status As Boolean = False
        For Each ct As Control In gb.Controls
            If TypeOf ct Is TextBox Then
                If CType(ct, TextBox).Text = "" Then
                    status = True
                    Exit For
                End If
            End If

            If TypeOf ct Is PictureBox Then
                If CType(ct, PictureBox).ImageLocation = Nothing Then
                    status = True
                    Exit For
                End If
            End If


        Next
        Return status
    End Function

    Sub onlyNumber(e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True

            End If


        End If
    End Sub



    Sub onlyNumberWithComma(e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) = 44 Then


            ElseIf Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True

            End If


        End If
    End Sub

    Function kunci(t As String, inter As Integer)
        Dim c As Char
        Dim result As String

        For i As Integer = 0 To Len(t) - 1
            c = t.Substring(i, 1)
            result += Chr(Asc(c) + inter)
        Next


        Return result
    End Function

    Function dialog(txt As String)
        Dim ds As DialogResult = MessageBox.Show(txt, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If ds = DialogResult.Yes Then
            Return True
        Else
            Return False
        End If
    End Function


    Function setImage(img As PictureBox, url As String)
        If System.IO.File.Exists(url) Then
            img.Image = Image.FromFile(url)
            img.ImageLocation = url
        Else

            img.Image = Nothing
            img.ImageLocation = ""
        End If
    End Function


    Function isEmail(email As String)
        Try
            Dim mail = New MailAddress(email)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Sub makeFillDG(DG As DataGridView)
        For a As Integer = 0 To DG.Columns.Count - 1
            DG.Columns(a).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
    End Sub
    Sub styliseDG(DG As DataGridView)
        DG.EnableHeadersVisualStyles = False

        DG.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDarkDark
        DG.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.Control
        DG.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DG.RowsDefaultCellStyle.ForeColor = Color.Black
        DG.RowsDefaultCellStyle.BackColor = Color.White
        DG.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 9.75F, FontStyle.Bold)
        DG.RowHeadersVisible = False
    End Sub

    Function dekripsi(txt As String)
        Return kunci(txt, -7)
    End Function

    Function enkripsi(txt As String)
        Return kunci(txt, 7)
    End Function

    Function generateReceipt() As String
        Dim hariini As String = Date.Now().ToString("yyyy/MM/dd")
        Dim format = "00000000"

        Dim idjual As String = getValue("select  fakturjual from tbljual where tgljual > ('" & hariini & " 00:00:00') order by fakturjual desc limit 1", "fakturjual")


        If getCount("select  fakturjual from tbljual where tgljual > ('" & hariini & " 00:00:00') order by fakturjual desc limit 1") = 0 Then
            idjual = "0"
        End If

        Dim faktur = format.Substring(0, format.Length - (Integer.Parse(idjual) + 1).ToString.Length) & (Integer.Parse(idjual) + 1).ToString
        Return faktur


    End Function

    Function importExcelFromURL(url As String, Optional sheetname As String = "Sheet1")
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim DtSet As System.Data.DataSet
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter


        Try
            If ext = "mdb" Then
                MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & url & "';Mode=Read;" & "Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""")
            Else
                MyConnection = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & url & "';Mode=Read;" & "Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;""")
            End If


            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & sheetname & "$]", MyConnection)

            DtSet = New System.Data.DataSet
            Dim dt As New DataTable
            MyCommand.Fill(dt)
            Return dt

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Return New DataTable
        Finally
            MyConnection.Close()
        End Try

    End Function



    Sub dialogError(message As String)
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Sub dialogInfo(message As String)
        MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Function countTable(dt As DataTable) As Integer
        Try
            Return dt.Rows.Count
        Catch ex As Exception

            Return 0

        End Try
    End Function
    Function firstTable(dt As DataTable, col As String)
        Try
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(col)
            Else
                Return ""
            End If

        Catch ex As Exception

            Return ""

        End Try
    End Function
End Module
