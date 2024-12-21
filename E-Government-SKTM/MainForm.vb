Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class MainForm
    Inherits Form

    ' Enumeration for message types
    Private Enum MessageType
        Info
        Warning
        [Error]
    End Enum

    ' List to store application data
    Private applicationData As New List(Of String)()

    ' Constant message
    Private Const AppTitle As String = "E-Government Application - Surat Keterangan Tidak Mampu"

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = AppTitle
    End Sub

    ' Event: Add Button Click
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim nik As String = txtNIK.Text.Trim()
        Dim name As String = txtName.Text.Trim()
        Dim address As String = txtAddress.Text.Trim()

        If String.IsNullOrEmpty(nik) OrElse String.IsNullOrEmpty(name) OrElse String.IsNullOrEmpty(address) Then
            ShowMessage("Please fill in all fields!", MessageType.Warning)
            Return
        End If

        Dim data As String = $"NIK: {nik}, Name: {name}, Address: {address}, Type: Surat Keterangan Tidak Mampu"
        applicationData.Add(data)

        ' Update ListBox
        lstApplications.Items.Add(data)

        ShowMessage("Application submitted successfully!", MessageType.Info)

        ' Clear input fields
        ClearInputs()
    End Sub

    ' Event: Clear Button Click
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearInputs()
    End Sub

    ' Event: Print Button Click
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If lstApplications.SelectedIndex = -1 Then
            ShowMessage("Please select an application to print!", MessageType.Warning)
            Return
        End If

        Dim selectedApplication As String = lstApplications.SelectedItem.ToString()

        Dim result As DialogResult = MessageBox.Show($"Do you want to print this application?\n\n{selectedApplication}",
            AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ShowMessage("Application printed successfully!", MessageType.Info)
        End If
    End Sub

    ' Event: Exit Button Click
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    ' Menu: File -> Exit
    Private Sub menuExit_Click(sender As Object, e As EventArgs) Handles menuExit.Click
        Application.Exit()
    End Sub

    ' Menu: Help -> About
    Private Sub menuAbout_Click(sender As Object, e As EventArgs) Handles menuAbout.Click
        MessageBox.Show("E-Government App - Surat Keterangan Tidak Mampu\nVersion 1.0\nDeveloped by [Your Name]", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Method to clear input fields
    Private Sub ClearInputs()
        txtNIK.Clear()
        txtName.Clear()
        txtAddress.Clear()
    End Sub

    ' Method to show a message box
    Private Sub ShowMessage(message As String, type As MessageType)
        Dim icon As MessageBoxIcon = MessageBoxIcon.Information

        If type = MessageType.Warning Then
            icon = MessageBoxIcon.Warning
        ElseIf type = MessageType.Error Then
            icon = MessageBoxIcon.Error
        End If

        MessageBox.Show(message, AppTitle, MessageBoxButtons.OK, icon)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles menuExit.Click
        Application.Exit()
    End Sub

    ' Event: Calculate Average Applications per Day
    Private Sub HitungSuratToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HitungSuratToolStripMenuItem.Click
        ' Mendapatkan jumlah aplikasi dari ListBox
        Dim totalApplications As Integer = lstApplications.Items.Count

        ' Menampilkan InputBox untuk meminta jumlah hari
        Dim inputDays As String = InputBox("Please enter the number of days:", "Enter Number of Days", "1")

        ' Validasi input jumlah hari
        Dim totalDays As Integer
        If Integer.TryParse(inputDays, totalDays) AndAlso totalDays > 0 Then
            ' Menghitung rata-rata aplikasi per hari
            Dim averageApplications As Double = totalApplications / totalDays ' Operator Aritmatika: Pembagian (/)

            ' Menampilkan hasil
            ShowMessage($"Average applications per day: {averageApplications:F2}", MessageType.Info)
        Else
            ' Jika input jumlah hari tidak valid
            ShowMessage("Please enter a valid number of days!", MessageType.Warning)
        End If
    End Sub
End Class
