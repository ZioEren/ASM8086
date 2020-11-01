Public Class InfoWindow
    Public Shared stringToBeWritten As String = "Welcome to the tutorial."
    Private Sub InfoWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = stringToBeWritten
    End Sub
End Class