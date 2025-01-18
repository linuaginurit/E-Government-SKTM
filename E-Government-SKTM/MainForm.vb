Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.IO
Imports System.Globalization

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
        Dim nik As String = txtNik.Text.Trim()
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
            ' Control and alignment for printing text
            PrintApplication(selectedApplication)
            ShowMessage("Application printed successfully!", MessageType.Info)
        End If
    End Sub

    ' Event: Exit Button Click
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    ' Method to clear input fields
    Private Sub ClearInputs()
        txtNik.Clear()
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

    ' Method: Print application details
    Private Sub PrintApplication(applicationData As String)
        ' Control alignment and format text for printing
        Dim printText As String = applicationData.PadRight(80)
        Dim printFilePath As String = Path.Combine(Application.StartupPath, "PrintedApplications.txt")

        Using writer As New StreamWriter(printFilePath, True)
            writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {printText}")
        End Using

        MessageBox.Show($"Application printed to: {printFilePath}", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Event: Calculate Average Applications per Day
    Private Sub HitungSuratToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HitungSuratToolStripMenuItem.Click
        Dim totalApplications As Integer = lstApplications.Items.Count
        Dim inputDays As String = InputBox("Please enter the number of days:", "Enter Number of Days", "1")

        Dim totalDays As Integer
        If Integer.TryParse(inputDays, totalDays) AndAlso totalDays > 0 Then
            Dim averageApplications As Double = totalApplications / totalDays
            ShowMessage($"Average applications per day: {averageApplications:F2}", MessageType.Info)
        Else
            ShowMessage("Please enter a valid number of days!", MessageType.Warning)
        End If
    End Sub

    ' Method: Save application data to file
    Private Sub SaveApplicationData()
        Dim saveFilePath As String = Path.Combine(Application.StartupPath, "ApplicationsBackup.txt")
        Using writer As New StreamWriter(saveFilePath)
            For Each data As String In applicationData
                writer.WriteLine(data)
            Next
        End Using
    End Sub

    ' Handling Exceptions
    Private Sub HandleError(ex As Exception)
        ShowMessage($"An error occurred: {ex.Message}", MessageType.Error)
    End Sub

    ' Example: Use Date, Time, and TimeSpan
    Private Sub DisplayCurrentDateAndTime()
        Dim currentDate As String = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture)
        ShowMessage($"Current Date and Time: {currentDate}", MessageType.Info)
    End Sub

    ' Demonstrating Loops
    Private Sub DisplayApplicationsUsingLoops()
        Dim dataSummary As String = String.Empty

        ' For Loop
        For i As Integer = 0 To applicationData.Count - 1
            dataSummary &= $"{i + 1}. {applicationData(i)}\n"
        Next

        ' While Loop
        Dim index As Integer = 0
        While index < applicationData.Count
            dataSummary &= $"(While) {index + 1}. {applicationData(index)}\n"
            index += 1
        End While

        ' Do Loop
        index = 0
        Do While index < applicationData.Count
            dataSummary &= $"(Do) {index + 1}. {applicationData(index)}\n"
            index += 1
        Loop

        MessageBox.Show(dataSummary, "Application Data Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Example: ByVal and ByRef
    Private Sub ModifyDataByRef(ByRef data As String)
        data = data.ToUpper()
    End Sub

    Private Function ModifyDataByVal(ByVal data As String) As String
        Return data.ToLower()
    End Function

End Class
