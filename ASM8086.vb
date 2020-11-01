Public Class ASM8086
    Dim inConfirm As Boolean = False
    Dim inConfirm1 As Boolean = False
    Dim downloader As New System.Net.WebClient
    Dim tutorialStep As Integer = 0
    Private Sub ASM8086_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.Network.IsAvailable Then
            Try
                Dim web As New System.Net.WebClient
                If Not web.DownloadString("https://mathplusplus.altervista.org/ASM8086/get-version.php") = "15.0" Then
                    If MessageBox.Show("A new version of this program is available. Do you wanna download it?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        SaveFileDialog1.DefaultExt = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                            downloader.DownloadFile("https://mathplusplus.altervista.org/ASM8086/ASM8086.exe", SaveFileDialog1.FileName)
                            Application.Exit()
                            Exit Sub
                        Else
                            Application.Exit()
                            Exit Sub
                        End If
                    Else
                        Application.Exit()
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
            End Try
        End If
        ListBox2.Items.Add("=================================")
        ListBox3.Items.Add("=================================")
        ListBox2.Items.Add("Part 1: Sequency (Base part)")
        ListBox2.Items.Add("Write string")
        ListBox2.Items.Add("Write char")
        ListBox2.Items.Add("Input number to variable (8 bit)")
        ListBox2.Items.Add("Input number to variable (16 bit)")
        ListBox2.Items.Add("Input char to variable (8 bit)")
        ListBox2.Items.Add("Input char to variable (16 bit)")
        ListBox2.Items.Add("Write constant (8 bit)")
        ListBox2.Items.Add("Write declared string")
        ListBox2.Items.Add("Write declared constant (8 bit)")
        ListBox2.Items.Add("Write constant (16 bit)")
        ListBox2.Items.Add("Write declared constant (16 bit)")
        ListBox2.Items.Add("Assembly instruction")
        ListBox3.Items.Add("Declare constant (8 bit)")
        ListBox3.Items.Add("Declare empty variable (8 bit)")
        ListBox3.Items.Add("Declare string")
        ListBox3.Items.Add("Declare constant (16 bit)")
        ListBox3.Items.Add("Declare empty variable (16 bit)")
        ListBox3.Items.Add("Declare array (8 bit)")
        ListBox3.Items.Add("Declare array (16 bit)")
        ListBox2.Items.Add("Set value to variable (8 bit)")
        ListBox2.Items.Add("Set value to variable (16 bit)")
        ListBox2.Items.Add("Increment variable")
        ListBox2.Items.Add("Decrement variable")
        ListBox2.Items.Add("Add to variable (8 bit)")
        ListBox2.Items.Add("Add to variable (16 bit)")
        ListBox2.Items.Add("Subtract to variable (8 bit)")
        ListBox2.Items.Add("Subtract to variable (16 bit)")
        ListBox2.Items.Add("Multiplicate to variable (8 bit)")
        ListBox2.Items.Add("Multiplicate to variable (16 bit)")
        ListBox2.Items.Add("Divide to variable (8 bit)")
        ListBox2.Items.Add("Divide to variable (16 bit)")
        ListBox2.Items.Add("Set variable to time hour (8 bit)")
        ListBox2.Items.Add("Set variable to time hour (16 bit)")
        ListBox2.Items.Add("Set variable to time minutes (8 bit)")
        ListBox2.Items.Add("Set variable to time minutes (16 bit)")
        ListBox2.Items.Add("Set variable to time seconds (8 bit)")
        ListBox2.Items.Add("Set variable to time seconds (16 bit)")
        ListBox2.Items.Add("Set variable to time hundreds (8 bit)")
        ListBox2.Items.Add("Set variable to time hundreds (16 bit)")
        ListBox2.Items.Add("Apply discount to variable (8 bit)")
        ListBox2.Items.Add("Apply discount to variable (16 bit)")
        ListBox2.Items.Add("End program")
        ListBox2.Items.Add("Generate overflow")
        ListBox2.Items.Add("Write current memory")
        ListBox2.Items.Add("Clear registers")
        ListBox2.Items.Add("Leave blank line")
        ListBox2.Items.Add("Write decimal number (8 bit)")
        ListBox2.Items.Add("Convert 8 bit variable to 16 bit variable")
        ListBox2.Items.Add("Convert 16 bit variable to 8 bit variable")
        ListBox2.Items.Add("=================================")
        ListBox3.Items.Add("=================================")
        ListBox2.Items.Add("Part 2: Selection (If part)")
        ListBox2.Items.Add("Start label")
        ListBox2.Items.Add("Jump to label")
        ListBox2.Items.Add("Compare two elements")
        ListBox2.Items.Add("Check if number is in range")
        ListBox2.Items.Add("=================================")
        ListBox2.Items.Add("Part 3: Iteration (Cycles part)")
        ListBox2.Items.Add("Cycle while")
        ListBox2.Items.Add("Cycle do while")
        ListBox2.Items.Add("Program pause")
        ListBox2.Items.Add("=================================")
        ListBox2.Items.Add("Part 4: Arrays (Arrays part)")
        ListBox2.Items.Add("Clear array")
        ListBox2.Items.Add("=================================")
        ListBox2.Items.Add("=================================")
        ListBox2.Items.Add("Part 5: Logical operations (Bitwise part)")
        ListBox2.Items.Add("Logical AND")
        ListBox2.Items.Add("Logical OR")
        ListBox2.Items.Add("Logical NOT")
        ListBox2.Items.Add("Right shift")
        ListBox2.Items.Add("Left shift")
        ListBox2.Items.Add("Jump to label if carry")
        ListBox2.Items.Add("Jump to label if not carry")
        ListBox2.Items.Add("Clear carry")
        ListBox2.Items.Add("Set carry")
        ListBox2.Items.Add("Rotate right")
        ListBox2.Items.Add("Rotate left")
        ListBox2.Items.Add("Rotate right with carry")
        ListBox2.Items.Add("Rotate left with carry")
        ListBox2.Items.Add("=================================")
        GenerateAssembly()
        If Not System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\ASM8086.txt") Then
            System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\ASM8086.txt", "")
            If LinkLabel1.Text = "Start tutorial" Then
                LinkLabel1.Text = "Stop tutorial"
                InfoWindow.stringToBeWritten = "Welcome to the tutorial."
                InfoWindow.Show()
                Timer1.Start()
            Else
                LinkLabel1.Text = "Start tutorial"
                Timer1.Stop()
                tutorialStep = 0
                InfoWindow.Close()
            End If
        End If
    End Sub
    Public Sub GenerateAssembly()
        Dim dataPart As String = ""
        Dim codePart As String = ""
        For i = 0 To ListBox1.Items.Count - 1
            Dim element As String = ListBox1.Items(i).ToString()
            If element.StartsWith("Write string ") Then
                element = element.Substring(13, element.Length - 13)
                Dim varName As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim thing As String = "  " + varName + " DB 13, 10, (""" + element + """), '$'"
                If dataPart = "" Then
                    dataPart = thing
                Else
                    dataPart += vbNewLine + thing
                End If
                thing = "  LEA DX, " + varName + vbNewLine + "  MOV AH, 09H" + vbNewLine + "  INT 21H"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Write declared string ") Then
                element = element.Substring(22, element.Length - 22)
                Dim thing As String = "  LEA DX, " + element + vbNewLine + "  MOV AH, 09H" + vbNewLine + "  INT 21H"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Write constant ") Then
                element = element.Substring(23, element.Length - 23)
                Dim thing As String = ""
                If Not element.EndsWith("D") And Not element.EndsWith("H") And Not element.EndsWith("B") Then
                    element += "D"
                End If
                Dim numericThing As String = translateNumber(element).ToString()
                If Int32.Parse(numericThing) > 255 Then
                    numericThing = "255"
                    element = "255D"
                End If
                If numericThing.Length = 1 Then
                    thing = "  MOV DL, " + element + vbNewLine + "  ADD DL, 48D" + vbNewLine + "  MOV AH, 02H" + vbNewLine + "  INT 21H"
                ElseIf numericThing.Length = 2 Then
                    thing = "  MOV AH, 00H" + vbNewLine +
                        "  MOV AL, " + element + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, q" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                ElseIf numericThing.Length = 3 Then
                    thing = "  MOV AH, 00H" + vbNewLine +
                        "  MOV AL, " + element + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r1, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV AL, r1" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                End If
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Write declared constant ") Then
                element = element.Substring(32, element.Length - 32)
                Dim splitter() As String = Split(element, " ")
                Dim varName As String = splitter(0)
                Dim declaredLength As Integer = Int32.Parse(splitter(1))
                Dim thing As String = ""
                If declaredLength = 1 Then
                    thing = "  MOV DL, " + varName + vbNewLine + "  ADD DL, 48D" + vbNewLine + "  MOV AH, 02H" + vbNewLine + "  INT 21H"
                ElseIf declaredLength = 2 Then
                    thing = "  MOV AH, 00H" + vbNewLine +
                        "  MOV AL, " + varName + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, q" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                ElseIf declaredLength = 3 Then
                    thing = "  MOV AH, 00H" + vbNewLine +
                        "  MOV AL, " + varName + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r1, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV AL, r1" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                End If
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Write constant ") Then
                element = element.Substring(24, element.Length - 24)
                Dim thing As String = ""
                Dim dataThing As String = ""
                Dim numericThing As String = translateNumber(element).ToString()
                If UShort.Parse(numericThing) > 65535 Then
                    numericThing = "65535"
                    element = "65535D"
                End If
                If numericThing.Length = 1 Then
                    thing = "  MOV DX, " + element + vbNewLine + "  ADD DX, 48D" + vbNewLine + "  MOV AH, 02H" + vbNewLine + "  INT 21H"
                ElseIf numericThing.Length = 2 Then
                    thing = "  MOV AX, " + element + vbNewLine +
                      "  MOV BL, 10D" + vbNewLine +
                      "  DIV BL" + vbNewLine +
                      "  MOV q, AL" + vbNewLine +
                      "  MOV r, AH" + vbNewLine +
                      "  MOV DL, q" + vbNewLine +
                      "  ADD DL, 48D" + vbNewLine +
                      "  MOV AH, 02H" + vbNewLine +
                      "  INT 21H" + vbNewLine +
                      "  MOV DL, r" + vbNewLine +
                      "  ADD DL, 48D" + vbNewLine +
                      "  MOV AH, 02H" + vbNewLine +
                      "  INT 21H"
                ElseIf numericThing.Length = 3 Then
                    thing = "  MOV AX, " + element + vbNewLine +
    "  MOV BL, 100D" + vbNewLine +
    "  DIV BL" + vbNewLine +
    "  MOV r, AH" + vbNewLine +
    "  MOV DL, AL" + vbNewLine +
    "  ADD DL, 48D" + vbNewLine +
    "  MOV AH, 02H" + vbNewLine +
    "  INT 21H" + vbNewLine +
    "  MOV AL, r" + vbNewLine +
    "  MOV BL, 10D" + vbNewLine +
    "  MOV AH, 00H" + vbNewLine +
    "  DIV BL" + vbNewLine +
    "  MOV r, AH" + vbNewLine +
    "  MOV DL, AL" + vbNewLine +
    "  ADD DL, 48D" + vbNewLine +
    "  MOV AH, 02H" + vbNewLine +
    "  INT 21H" + vbNewLine +
    "  MOV DL, r" + vbNewLine +
    "  ADD DL, 48D" + vbNewLine +
    "  MOV AH, 02H" + vbNewLine +
    "  INT 21H"
                ElseIf numericThing.Length = 4 Then
                    thing = "  MOV AX, " + element + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r1, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r1" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AL, r" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                ElseIf numericThing.Length = 5 Then
                    thing = "  MOV AX, " + element + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AL, q" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, q" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AL, r" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                End If
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Write declared constant ") Then
                element = element.Substring(33, element.Length - 33)
                Dim thing As String = ""
                Dim splitter() As String = Split(element)
                Dim varName As String = splitter(0)
                Dim numberLength As Integer = Int32.Parse(splitter(1))
                If numberLength < 1 Then
                    numberLength = 1
                End If
                If numberLength > 5 Then
                    numberLength = 5
                End If
                If numberLength = 1 Then
                    thing = "  MOV DX, " + varName + vbNewLine + "  ADD DX, 48D" + vbNewLine + "  MOV AH, 02H" + vbNewLine + "  INT 21H"
                ElseIf numberLength = 2 Then
                    thing = "  MOV AX, " + varName + vbNewLine +
                      "  MOV BL, 10D" + vbNewLine +
                      "  DIV BL" + vbNewLine +
                      "  MOV q, AL" + vbNewLine +
                      "  MOV r, AH" + vbNewLine +
                      "  MOV DL, q" + vbNewLine +
                      "  ADD DL, 48D" + vbNewLine +
                      "  MOV AH, 02H" + vbNewLine +
                      "  INT 21H" + vbNewLine +
                      "  MOV DL, r" + vbNewLine +
                      "  ADD DL, 48D" + vbNewLine +
                      "  MOV AH, 02H" + vbNewLine +
                      "  INT 21H"
                ElseIf numberLength = 3 Then
                    thing = "  MOV AX, " + varName + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AL, r" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                ElseIf numberLength = 4 Then
                    thing = "  MOV AX, " + varName + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r1, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r1" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AL, r" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                ElseIf numberLength = 5 Then
                    thing = "  MOV AX, " + varName + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 100D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AL, q" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, q" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV AL, r" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV DL, AL" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                End If
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Assembly instruction ") Then
                element = element.Substring(21, element.Length - 21)
                If codePart = "" Then
                    codePart = "  " + element
                Else
                    codePart += vbNewLine + "  " + element
                End If
            ElseIf element.StartsWith("(8 bit) Set value to variable ") Then
                element = element.Substring(30, element.Length - 30)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AH, " + varValue + vbNewLine + "  MOV " + varName + ", AH"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Set value to variable ") Then
                element = element.Substring(31, element.Length - 31)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AX, " + varValue + vbNewLine + "  MOV " + varName + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Add to variable ") Then
                element = element.Substring(24, element.Length - 24)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AH, " + varValue + vbNewLine + "  ADD " + varName + ", AH"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Add to variable ") Then
                element = element.Substring(25, element.Length - 25)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AX, " + varValue + vbNewLine + "  ADD " + varName + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Subtract to variable ") Then
                element = element.Substring(24, element.Length - 24)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AH, " + varValue + vbNewLine + "  SUB " + varName + ", AH"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Subtract to variable ") Then
                element = element.Substring(25, element.Length - 25)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AX, " + varValue + vbNewLine + "  SUB " + varName + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Divide to variable") Then
                element = element.Substring(26, element.Length - 26)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AL, " + varName + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  MOV BL, " + varValue + vbNewLine + "  DIV BL" + vbNewLine + "MOV " + varName + ", AL"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Divide to variable") Then
                element = element.Substring(27, element.Length - 27)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AX, " + varValue + vbNewLine + "  MOV BL, " + varValue + vbNewLine + "  DIV BL" + vbNewLine + "MOV " + varName + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Set variable to time hour") Then
                element = element.Substring(33, element.Length - 33)
                Dim thing As String = "  MOV AH, 2CH" + vbNewLine + "  INT 21H" + vbNewLine + "  MOV " + element + ", CH"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Set variable to time hour") Then
                element = element.Substring(34, element.Length - 34)
                Dim thing As String = "  MOV AH, 2CH" + vbNewLine + "  INT 21H" + vbNewLine + "MOV CL, 00H" + vbNewLine + "  MOV " + element + ", CX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Set variable to time minutes") Then
                element = element.Substring(36, element.Length - 36)
                Dim thing As String = "  MOV AH, 2CH" + vbNewLine + "  INT 21H" + vbNewLine + "  MOV " + element + ", CL"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Set variable to time minutes") Then
                element = element.Substring(37, element.Length - 37)
                Dim thing As String = "  MOV AH, 2CH" + vbNewLine + "  INT 21H" + vbNewLine + "MOV CH, 00H" + vbNewLine + "  MOV " + element + ", CX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Set variable to time seconds") Then
                element = element.Substring(36, element.Length - 36)
                Dim thing As String = "  MOV AH, 2CH" + vbNewLine + "  INT 21H" + vbNewLine + "  MOV " + element + ", DH"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Set variable to time seconds") Then
                element = element.Substring(37, element.Length - 37)
                Dim thing As String = "  MOV AH, 2CH" + vbNewLine + "  INT 21H" + vbNewLine + "MOV DL, 00H" + vbNewLine + "  MOV " + element + ", DX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Write char ") Then
                element = element.Substring(11, element.Length - 11)
                Dim numericThing As String = translateNumber(element)
                Dim thing As String = ""
                Dim charCode As Integer = 0
                If IsNumeric(numericThing) And Not numericThing = "0" Then
                    charCode = Int32.Parse(numericThing)
                Else
                    charCode = AscW(element)
                End If
                thing = "  MOV DL, " + charCode.ToString() + "D" + vbNewLine + "  MOV AH, 02H" + vbNewLine + "  INT 21H"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Generate overflow") Then
                Dim thing As String = "  MOV AX, 0" + vbNewLine + "  MOV CX, 0" + vbNewLine + "  DIV CX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Input number to variable") Then
                element = element.Substring(32, element.Length - 32)
                Dim splitter() As String = Split(element)
                Dim varName As String = splitter(1)
                Dim inputLength As Integer = Integer.Parse(splitter(2))
                If inputLength > 3 Then
                    inputLength = 3
                End If
                If inputLength < 0 Then
                    inputLength = 1
                End If
                Dim thing As String = ""
                If inputLength = 1 Then
                    thing = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV " + varName + ", AL"
                ElseIf inputLength = 2 Then
                    thing = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 10D" + vbNewLine + "  MUL BL" + vbNewLine + "  MOV " + varName + ", AL" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  ADD " + varName + ", AL"
                ElseIf inputLength = 3 Then
                    thing = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 100D" + vbNewLine + "  MUL BL" + vbNewLine + "  MOV " + varName + ", AL" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 10D" + vbNewLine + "  MUL BL" + vbNewLine + "  ADD " + varName + ", AL" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  ADD " + varName + ", AL"
                End If
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Input number to variable") Then
                element = element.Substring(33, element.Length - 33)
                Dim splitter() As String = Split(element)
                Dim varName As String = splitter(1)
                Dim inputLength As Integer = Integer.Parse(splitter(2))
                If inputLength > 3 Then
                    inputLength = 3
                End If
                If inputLength < 0 Then
                    inputLength = 1
                End If
                Dim thing As String = ""
                If inputLength = 1 Then
                    thing = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  MOV " + varName + ", AX"
                ElseIf inputLength = 2 Then
                    thing = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 10D" + vbNewLine + "  MUL BL" + vbNewLine + "  MOV " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  ADD " + varName + ", AX"
                ElseIf inputLength = 3 Then
                    thing = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 100D" + vbNewLine + "  MUL BL" + vbNewLine + "  MOV " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 10D" + vbNewLine + "  MUL BL" + vbNewLine + "  ADD " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  ADD " + varName + ", AX"
                ElseIf inputLength = 4 Then
                    thing = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 1000D" + vbNewLine + "  MUL BL" + vbNewLine + "  MOV " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 100D" + vbNewLine + "  MUL BL" + vbNewLine + "  ADD " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 10D" + vbNewLine + "  MUL BL" + vbNewLine + "  ADD " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  ADD " + varName + ", AX"
                ElseIf inputLength = 5 Then
                    thing = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 10000D" + vbNewLine + "  MUL BL" + vbNewLine + "  MOV " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 1000D" + vbNewLine + "  MUL BL" + vbNewLine + "  ADD " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 100D" + vbNewLine + "  MUL BL" + vbNewLine + "  ADD " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV BL, 10D" + vbNewLine + "  MUL BL" + "  ADD " + varName + ", AX" + vbNewLine + "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  SUB AL, 48D" + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  ADD " + varName + ", AX"
                End If
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Multiplicate to variable") Then
                element = element.Substring(33, element.Length - 33)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AL, " + varName + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  MOV BL, " + varValue + vbNewLine + "  MUL BL" + vbNewLine + "  MOV " + varName + ", AL"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Multiplicate to variable") Then
                element = element.Substring(34, element.Length - 34)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  MOV AX, " + varValue + vbNewLine + "  MOV BL, " + varValue + vbNewLine + "  MUL BL" + vbNewLine + "MOV " + varName + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Write current memory") Then
                Dim codeThing As String = "  LEA DX, dump" + vbNewLine + "  MOV AH, 09H" + vbNewLine + "  INT 21H"
                If codePart = "" Then
                    codePart = codeThing
                Else
                    codePart += vbNewLine + codeThing
                End If
            ElseIf element.StartsWith("Clear registers") Then
                Dim thing As String = "  MOV AH, 00H" + vbNewLine +
                    "  MOV AL, 00H" + vbNewLine +
                    "  MOV BH, 00H" + vbNewLine +
                    "  MOV BL, 00H" + vbNewLine +
                    "  MOV CH, 00H" + vbNewLine +
                    "  MOV CL, 00H" + vbNewLine +
                    "  MOV DH, 00H" + vbNewLine +
                    "  MOV DL, 00H"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Input char to variable ") Then
                element = element.Substring(31, element.Length - 31)
                Dim thing As String = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  MOV " + element + ", AL"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Input char to variable ") Then
                element = element.Substring(32, element.Length - 32)
                Dim thing As String = "  MOV AH, 01H" + vbNewLine + "  INT 21H" + vbNewLine + "  MOV " + element + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Leave blank line") Then
                Dim thing As String = "  LEA DX, blank" + vbNewLine + "  MOV AH, 09H" + vbNewLine + "  INT 21H"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Write decimal number ") Then
                element = element.Substring(29, element.Length - 29)
                Dim splitter() As String = Split(element)
                Dim firstOperand As String = splitter(0)
                Dim secondOperand As String = splitter(1)
                Dim aproximation As Integer = Int32.Parse(splitter(2))
                Dim numberLength As Integer = Int32.Parse(splitter(3))
                Dim thing As String = ""
                If aproximation < 1 Then
                    aproximation = 1
                End If
                If aproximation > 2 Then
                    aproximation = 2
                End If
                If numberLength > 3 Then
                    numberLength = 3
                End If
                If numberLength < 1 Then
                    numberLength = 1
                End If
                thing = "  MOV AL, " + firstOperand.ToString() + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, " + secondOperand.ToString() + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV q, AL" + vbNewLine +
                        "  MOV r, AH" + vbNewLine +
                        "  MOV AL, r" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  MUL BL" + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, " + secondOperand.ToString() + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r, AL"
                If aproximation = 2 Then
                    thing += vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  MOV BL, 10D" + vbNewLine +
                        "  MUL BL" + vbNewLine +
                        "  MOV BL, " + secondOperand.ToString() + vbNewLine +
                        "  MOV AH, 00H" + vbNewLine +
                        "  DIV BL" + vbNewLine +
                        "  MOV r1, AL"
                End If
                If numberLength = 1 Then
                    thing += vbNewLine + "  MOV DL, q" + vbNewLine +
                            "  ADD DL, 48D" + vbNewLine +
                            "  MOV AH, 02H" + vbNewLine +
                            "  INT 21H"
                ElseIf numberLength = 2 Then
                    thing += vbNewLine + "  MOV AL, q" + vbNewLine +
                            "  MOV AH, 00H" + vbNewLine +
                            "  MOV BL, 10D" + vbNewLine +
                            "  DIV BL" + vbNewLine +
                            "  MOV q, AH" + vbNewLine +
                            "  MOV DL, AL" + vbNewLine +
                            "  ADD DL, 48D" + vbNewLine +
                            "  MOV AH, 02H" + vbNewLine +
                            "  INT 21H" + vbNewLine +
                            "  MOV DL, q" + vbNewLine +
                            "  ADD DL, 48D" + vbNewLine +
                            "  MOV AH, 02H" + vbNewLine +
                            "  INT 21H"
                ElseIf numberLength = 3 Then
                    thing += vbNewLine + "  MOV AL, q" + vbNewLine +
                            "  MOV AH, 00H" + vbNewLine +
                            "  MOV BL, 100D" + vbNewLine +
                            "  DIV BL" + vbNewLine +
                            "  MOV q, AH" + vbNewLine +
                            "  MOV DL, AL" + vbNewLine +
                            "  ADD DL, 48D" + vbNewLine +
                            "  MOV AH, 02H" + vbNewLine +
                            "  INT 21H" + vbNewLine +
                            "  MOV AL, q" + vbNewLine +
                            "  MOV BL, 10D" + vbNewLine +
                            "  MOV AH, 00H" + vbNewLine +
                            "  DIV BL" + vbNewLine +
                            "  MOV q, AH" + vbNewLine +
                            "  MOV DL, AL" + vbNewLine +
                            "  ADD DL, 48D" + vbNewLine +
                            "  MOV AH, 02H" + vbNewLine +
                            "  INT 21H" + vbNewLine +
                            "  MOV DL, q" + vbNewLine +
                            "  ADD DL, 48D" + vbNewLine +
                            "  MOV AH, 02H" + vbNewLine +
                            "  INT 21H"
                End If
                thing += vbNewLine + "  MOV DL, 46D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H" + vbNewLine +
                        "  MOV DL, r" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                If aproximation = 2 Then
                    thing += vbNewLine + "  MOV DL, r1" + vbNewLine +
                        "  ADD DL, 48D" + vbNewLine +
                        "  MOV AH, 02H" + vbNewLine +
                        "  INT 21H"
                End If
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Convert 8 bit variable to 16 bit variable ") Then
                element = element.Substring(42, element.Length - 42)
                Dim splitter() As String = Split(element)
                Dim firstVar As String = splitter(0)
                Dim secondVar As String = splitter(1)
                Dim thing As String = "  MOV AL, " + firstVar + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  MOV " + secondVar + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Convert 16 bit variable to 8 bit variable ") Then
                element = element.Substring(42, element.Length - 42)
                Dim splitter() As String = Split(element)
                Dim firstVar As String = splitter(0)
                Dim secondVar As String = splitter(1)
                Dim thing As String = "  MOV AX, " + firstVar + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  MOV " + secondVar + ", AL"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Start label ") Then
                element = element.Substring(12, element.Length - 12)
                Dim thing As String = "  " + element + ":"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Jump to label ") Then
                element = element.Substring(14, element.Length - 14)
                Dim thing As String = "  JMP " + element
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Compare two elements ") Then
                element = element.Substring(21, element.Length - 21)
                Dim splitter() As String = Split(element)
                Dim firstOperand As String = splitter(0)
                Dim secondOperand As String = splitter(1)
                Dim comparingOperand As String = splitter(2)
                Dim labelToJump As String = splitter(3)
                Dim jumpOperand As String = ""
                If comparingOperand = "==" Then
                    jumpOperand = "JE"
                ElseIf comparingOperand = ">" Then
                    jumpOperand = "JG"
                ElseIf comparingOperand = ">=" Then
                    jumpOperand = "JGE"
                ElseIf comparingOperand = "<" Then
                    jumpOperand = "JL"
                ElseIf comparingOperand = "<=" Then
                    jumpOperand = "JLE"
                ElseIf comparingOperand = "!=" Then
                    jumpOperand = "JNE"
                End If
                Dim thing As String = "  CMP " + firstOperand + ", " + secondOperand + vbNewLine +
                            "  " + jumpOperand + " " + labelToJump
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("End program") Then
                Dim thing As String = "  MOV AH, 4CH" + vbNewLine + "  INT 21H"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Apply discount to variable ") Then
                element = element.Substring(35, element.Length - 35)
                Dim splitter() As String = Split(element)
                Dim varName As String = splitter(0)
                Dim discountValue As String = splitter(1)
                Dim destionationVariable As String = splitter(2)
                Dim thing As String = "  MOV AL, " + varName + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  MOV BL, " + discountValue + vbNewLine + "  MUL BL" + vbNewLine + "  MOV BL, 100D" + vbNewLine + "  DIV BL" + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  MOV r, AL" + vbNewLine + "  MOV AL, " + varName + vbNewLine + "  SUB AL, r" + vbNewLine + "  MOV " + destionationVariable + ", AL"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Apply discount to variable ") Then
                element = element.Substring(36, element.Length - 36)
                Dim splitter() As String = Split(element)
                Dim varName As String = splitter(0)
                Dim discountValue As String = splitter(1)
                Dim destionationVariable As String = splitter(2)
                Dim thing As String = "  MOV AX, " + varName + vbNewLine + "  MOV BL, " + discountValue + vbNewLine + "  MUL BL" + vbNewLine + "  MOV BL, 100D" + vbNewLine + "  DIV BL" + vbNewLine + "  MOV c, AX" + vbNewLine + "  MOV AX, " + varName + vbNewLine + "  SUB AX, c" + vbNewLine + "  MOV " + destionationVariable + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Check if number is in range ") Then
                element = element.Substring(28, element.Length - 28)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim minRange As String = lol(1)
                Dim maxRange As String = lol(2)
                Dim labelToJump As String = lol(3)
                Dim max1 As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim false1 As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim max2 As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim thing As String = "  MOV AL, " + varName + vbNewLine + "  CMP AL, " + minRange + vbNewLine + "  JGE " + max1 + vbNewLine + "  JMP " + false1 + vbNewLine + "  " + max1 + ": CMP AL, " + maxRange + vbNewLine + "  JLE " + labelToJump + vbNewLine + "  JMP " + max2 + vbNewLine + "  " + max2 + ": "
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Cycle while ") Then
                element = element.Substring(12, element.Length - 12)
                Dim lol() As String = Split(element, " ")
                Dim subject As String = lol(0)
                Dim times As String = lol(1)
                Dim labelCycle As String = lol(2)
                Dim labelEndCycle As String = lol(3)
                Dim check As String = "cycle" + i.ToString()
                Dim thing As String = "  MOV " + subject + ", 00H" + vbNewLine + "  JMP " + check + vbNewLine + "  " + check + ": " + vbNewLine +
                    "  CMP " + subject + ", " + times + vbNewLine + "  JGE " + labelEndCycle + vbNewLine + "  JMP " + labelCycle + vbNewLine +
                    "  " + labelCycle + ": " + vbNewLine + "  INC " + subject
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Increment variable ") Then
                element = element.Substring(19, element.Length - 19)
                Dim thing As String = "  INC " + element
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Decrement variable ") Then
                element = element.Substring(19, element.Length - 19)
                Dim thing As String = "  DEC " + element
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Cycle do while ") Then
                element = element.Substring(15, element.Length - 15)
                Dim lol() As String = Split(element, " ")
                Dim subject As String = lol(0)
                Dim times As String = lol(1)
                Dim labelCycle As String = lol(2)
                Dim labelEndCycle As String = lol(3)
                Dim check As String = "cycle" + i.ToString()
                Dim thing As String = "  MOV " + subject + ", 00H" + vbNewLine + "  DEC " + subject + vbNewLine + "  JMP " + labelCycle + vbNewLine + "  " + check + ": " + vbNewLine +
                    "  CMP " + subject + ", " + times + vbNewLine + "  JGE " + labelEndCycle + vbNewLine + "  JMP " + labelCycle + vbNewLine +
                    "  " + labelCycle + ": " + vbNewLine + "  INC " + subject
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Set variable to time hundreds ") Then
                element = element.Substring(38, element.Length - 38)
                Dim thing As String = "  MOV AH, 02CH" + vbNewLine + "  INT 21H" + vbNewLine + "  MOV " + element + ", DL"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Set variable to time hundreds ") Then
                element = element.Substring(39, element.Length - 39)
                Dim thing As String = "  MOV AH, 02CH" + vbNewLine + "  INT 21H" + vbNewLine + "  MOV AL, " + element + vbNewLine + "  MOV AH, 00H" + vbNewLine + "  MOV " + element + ", AX"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Program pause ") Then
                element = element.Substring(14, element.Length - 14)
                Dim label1 As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim label2 As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim label3 As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim thing As String = "  MOV CX, 00H" + vbNewLine + "  JMP " + label1 + vbNewLine + "  " + label1 + ":" + vbNewLine + "  CMP CX, " + element + vbNewLine + "  JGE " + label3 + vbNewLine + "  JMP " + label2 + vbNewLine + "  " + label2 + ":" + vbNewLine + "  JMP " + label1 + vbNewLine + "  " + label3 + ":"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Clear array ") Then
                element = element.Substring(12, element.Length - 12)
                Dim lol() As String = Split(element, " ")
                Dim arrayName As String = lol(0)
                Dim arraySize As String = lol(1)
                Dim labelRepeat As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim labelEnd As String = RandomUtils.RandomNormalString(16).ToUpper()
                Dim thing As String = "  MOV DI, 00H" + vbNewLine + "  LEA BX, " + arrayName + vbNewLine + "  " + labelRepeat + ": " + vbNewLine + "  CMP DI, " + arraySize + vbNewLine +
                    "  JE " + labelEnd + vbNewLine + "  MOV CH, 00H" + vbNewLine + "  MOV [BX + DI], CH" + vbNewLine + "  INC DI" + vbNewLine + "  JMP " + labelRepeat + vbNewLine + "  " + labelEnd + ": "
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Carry jump ") Then
                element = element.Substring(11, element.Length - 11)
                Dim thing As String = "  JC " + element
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Not carry jump ") Then
                element = element.Substring(15, element.Length - 15)
                Dim thing As String = "  JNC " + element
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Logical AND ") Then
                element = element.Substring(12, element.Length - 11)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  AND " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Logical NOT ") Then
                element = element.Substring(12, element.Length - 12)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  NOT " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Logical OR ") Then
                element = element.Substring(11, element.Length - 11)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  AND " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Right shift ") Then
                element = element.Substring(12, element.Length - 12)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  SHR " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Left shift ") Then
                element = element.Substring(11, element.Length - 11)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  SHL " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Clear carry") Then
                Dim thing As String = "  CLC"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Set carry") Then
                Dim thing As String = "  STC"
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Rotate left with carry ") Then
                element = element.Substring(23, element.Length - 23)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  RCL " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Rotate right with carry ") Then
                element = element.Substring(24, element.Length - 24)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  RCR " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Rotate left ") Then
                element = element.Substring(12, element.Length - 12)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  ROL " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("Rotate right ") Then
                element = element.Substring(13, element.Length - 13)
                Dim lol() As String = Split(element, " ")
                Dim targetReg As String = lol(0)
                Dim injectVal As String = lol(1)
                Dim thing As String = "  ROR " + targetReg + ", " + injectVal
                If codePart = "" Then
                    codePart = thing
                Else
                    codePart += vbNewLine + thing
                End If
            End If
        Next
        For i = 0 To ListBox4.Items.Count - 1
            Dim element As String = ListBox4.Items(i).ToString()
            If element.StartsWith("(8 bit) Declare constant ") Then
                element = element.Substring(25, element.Length - 25)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  " + varName + " DB " + varValue
                If dataPart = "" Then
                    dataPart = thing
                Else
                    dataPart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Declare empty variable ") Then
                element = element.Substring(31, element.Length - 31)
                If element.Contains(" ") Then
                    Dim elements() As String = Split(element)
                    For Each elemen As String In elements
                        Dim thing As String = "  " + elemen + " DB (?)"
                        If dataPart = "" Then
                            dataPart = thing
                        Else
                            dataPart += vbNewLine + thing
                        End If
                    Next
                Else
                    Dim thing As String = "  " + element + " DB (?)"
                    If dataPart = "" Then
                        dataPart = thing
                    Else
                        dataPart += vbNewLine + thing
                    End If
                End If
            ElseIf element.StartsWith("Declare string ") Then
                element = element.Substring(15, element.Length - 15)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue = element.Replace(varName + " ", "")
                Dim thing As String = "  " + varName + " DB 13, 10, (""" + varValue + """), '$'"
                If dataPart = "" Then
                    dataPart = thing
                Else
                    dataPart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Declare empty variable ") Then
                element = element.Substring(32, element.Length - 32)
                If element.Contains(" ") Then
                    Dim elements() As String = Split(element)
                    For Each elemen As String In elements
                        Dim thing As String = "  " + elemen + " DW (?)"
                        If dataPart = "" Then
                            dataPart = thing
                        Else
                            dataPart += vbNewLine + thing
                        End If
                    Next
                Else
                    Dim thing As String = "  " + element + " DW (?)"
                    If dataPart = "" Then
                        dataPart = thing
                    Else
                        dataPart += vbNewLine + thing
                    End If
                End If
            ElseIf element.StartsWith("(16 bit) Declare constant ") Then
                element = element.Substring(26, element.Length - 26)
                Dim lol() As String = Split(element, " ")
                Dim varName As String = lol(0)
                Dim varValue As String = lol(1)
                Dim thing As String = "  " + varName + " DW " + varValue
                If dataPart = "" Then
                    dataPart = thing
                Else
                    dataPart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(8 bit) Declare array ") Then
                element = element.Substring(22, element.Length - 22)
                Dim lol() As String = Split(element, " ")
                Dim arrayName As String = lol(0)
                Dim arraySize As String = lol(1)
                Dim arrayInitial As String = lol(2)
                Dim thing As String = "  " + arrayName + " DB " + arraySize + " DUP (" + arrayInitial + ")"
                If dataPart = "" Then
                    dataPart = thing
                Else
                    dataPart += vbNewLine + thing
                End If
            ElseIf element.StartsWith("(16 bit) Declare array ") Then
                element = element.Substring(23, element.Length - 23)
                Dim lol() As String = Split(element, " ")
                Dim arrayName As String = lol(0)
                Dim arraySize As String = lol(1)
                Dim arrayInitial As String = lol(2)
                Dim thing As String = "  " + arrayName + " DW " + arraySize + " DUP (" + arrayInitial + ")"
                If dataPart = "" Then
                    dataPart = thing
                Else
                    dataPart += vbNewLine + thing
                End If
            End If
        Next
        If dataPart = "" Then
            dataPart = "  q DB (?)" + vbNewLine + "  r DB (?)" + vbNewLine + "  dump DB 13, 10, ("""")" + vbNewLine + "  r1 DB (?)" + vbNewLine + "  blank DB 13, 10, (""""), '$'" + vbNewLine + "  c DW (?)"
        Else
            dataPart += vbNewLine + "  q DB (?)" + vbNewLine + "  r DB (?)" + vbNewLine + "  dump DB 13, 10, ("""")" + vbNewLine + "  r1 DB (?)" + vbNewLine + "  blank DB 13, 10, (""""), '$'" + vbNewLine + "  c DW (?)"
        End If
        Dim newCode As String = ".MODEL SMALL" + vbNewLine + ".STACK 100H" + vbNewLine + ".DATA" + vbNewLine + dataPart + vbNewLine + ".CODE" + vbNewLine + "  MOV AX, @DATA" + vbNewLine + "  MOV DS, AX" + vbNewLine + codePart + vbNewLine + "  MOV AH, 4CH" + vbNewLine + "  INT 21H" + vbNewLine + "END"
        If Not TextBox2.Text.Replace(" ", "") = "" Then
            newCode = "TITLE " + TextBox2.Text + vbNewLine + newCode
        End If
        RichTextBox1.Text = newCode
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not ListBox2.SelectedItem = "" And Not ListBox2.SelectedItem = Nothing Then
            Dim instruction As String = ListBox2.SelectedItem.ToString().ToLower()
            If inConfirm Then
                If instruction.Equals("write string") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Write string " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("write declared string") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Write declared string " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("write constant (8 bit)") Then
                    If Not TextBox1.Text = "" And IsNumeric(translateNumber(TextBox1.Text)) Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Write constant " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("write declared constant (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And IsNumeric(TextBox5.Text) Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Write declared constant " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("assembly instruction") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Assembly instruction " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("write constant (16 bit)") Then
                    If Not TextBox1.Text = "" And IsNumeric(translateNumber(TextBox1.Text)) Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Write constant " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("write declared constant (16 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And IsNumeric(TextBox5.Text) Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Write declared constant " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("set value to variable (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Set value to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("set value to variable (16 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Set value to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("add to variable (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Add to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("add to variable (16 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Add to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("subtract to variable (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Subtract to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("subtract to variable (16 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Subtract to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("divide to variable (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Divide to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("divide to variable (16 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Divide to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("set variable to time hour (8 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Set variable to time hour " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("set variable to time hour (16 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Set variable to time hour " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("set variable to time minutes (8 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Set variable to time minutes " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("set variable to time minutes (16 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Set variable to time minutes " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("set variable to time seconds (8 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Set variable to time seconds " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("set variable to time seconds (16 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Set variable to time seconds " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("write char") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Write char " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("input number to variable (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And IsNumeric(TextBox5.Text) Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Input number to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("input number to variable (16 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And IsNumeric(TextBox5.Text) Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Input number to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("multiplicate to variable (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Multiplicate to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("multiplicate to variable (16 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Multiplicate to variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                        TextBox5.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("input char to variable (8 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Input char to variable " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("input char to variable (16 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Input char to variable " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("write decimal number (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And Not TextBox6.Text = "" And Not TextBox7.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Write decimal number " + TextBox1.Text + " " + TextBox5.Text + " " + TextBox6.Text + " " + TextBox7.Text)
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox6.Visible = False
                        Label6.Visible = False
                        TextBox7.Visible = False
                        Label7.Visible = False
                    End If
                ElseIf instruction.Equals("convert 8 bit variable to 16 bit variable") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Convert 8 bit variable to 16 bit variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        Label1.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("convert 16 bit variable to 8 bit variable") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Convert 16 bit variable to 8 bit variable " + TextBox1.Text + " " + TextBox5.Text)
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        Label1.Visible = False
                        Label5.Visible = False
                    End If
                ElseIf instruction.Equals("start label") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Start label " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("jump to label") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Jump to label " + TextBox1.Text)
                        TextBox1.Visible = False
                        Label1.Visible = False
                    End If
                ElseIf instruction.Equals("compare two elements") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And Not TextBox7.Text = "" And Not ComboBox1.SelectedItem.ToString() = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Compare two elements " + TextBox1.Text + " " + TextBox5.Text + " " + ComboBox1.SelectedItem.ToString() + " " + TextBox7.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        Label6.Visible = False
                        Label7.Visible = False
                        ComboBox1.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        TextBox7.Visible = False
                    End If
                ElseIf instruction.Equals("apply discount to variable (8 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And Not TextBox6.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Apply discount to variable " + TextBox1.Text + " " + TextBox5.Text + " " + TextBox6.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        Label6.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                    End If
                ElseIf instruction.Equals("apply discount to variable (16 bit)") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And Not TextBox6.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Apply discount to variable " + TextBox1.Text + " " + TextBox5.Text + " " + TextBox6.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        Label6.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                    End If
                ElseIf instruction.Equals("check if number is in range") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And Not TextBox6.Text = "" And Not TextBox7.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Check if number is in range " + TextBox1.Text + " " + TextBox5.Text + " " + TextBox6.Text + " " + TextBox7.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        Label6.Visible = False
                        Label7.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                        TextBox7.Visible = False
                    End If
                ElseIf instruction.Equals("cycle while") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And Not TextBox6.Text = "" And Not TextBox7.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Cycle while " + TextBox1.Text + " " + TextBox5.Text + " " + TextBox6.Text + " " + TextBox7.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        Label6.Visible = False
                        Label7.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                        TextBox7.Visible = False
                    End If
                ElseIf instruction.Equals("increment variable") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Increment variable " + TextBox1.Text)
                        Label1.Visible = False
                        TextBox1.Visible = False
                    End If
                ElseIf instruction.Equals("decrement variable") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Decrement variable " + TextBox1.Text)
                        Label1.Visible = False
                        TextBox1.Visible = False
                    End If
                ElseIf instruction.Equals("cycle do while") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" And Not TextBox6.Text = "" And Not TextBox7.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Cycle do while " + TextBox1.Text + " " + TextBox5.Text + " " + TextBox6.Text + " " + TextBox7.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        Label6.Visible = False
                        Label7.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                        TextBox7.Visible = False
                    End If
                ElseIf instruction.Equals("set variable to time hundreds (8 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(8 bit) Set variable to time hundreds " + TextBox1.Text)
                        Label1.Visible = False
                        TextBox1.Visible = False
                    End If
                ElseIf instruction.Equals("set variable to time hundreds (16 bit)") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("(16 bit) Set variable to time hundreds " + TextBox1.Text)
                        Label1.Visible = False
                        TextBox1.Visible = False
                    End If
                ElseIf instruction.Equals("program pause") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Program pause " + TextBox1.Text)
                        Label1.Visible = False
                        TextBox1.Visible = False
                    End If
                ElseIf instruction.Equals("clear array") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Clear array " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("logical and") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Logical AND " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("logical or") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Logical OR " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("logical not") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Logical NOT " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("right shift") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Right shift " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("left shift") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Left shift " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("jump to label if carry") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Carry jump " + TextBox1.Text)
                        Label1.Visible = False
                        TextBox1.Visible = False
                    End If
                ElseIf instruction.Equals("jump to label if not carry") Then
                    If Not TextBox1.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Not carry jump " + TextBox1.Text)
                        Label1.Visible = False
                        TextBox1.Visible = False
                    End If
                ElseIf instruction.Equals("rotate left") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Rotate left " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("rotate right") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Rotate right " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("rotate left with carry ") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Rotate left with carry " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                ElseIf instruction.Equals("rotate right with carry") Then
                    If Not TextBox1.Text = "" And Not TextBox5.Text = "" Then
                        inConfirm = False
                        ListBox1.Items.Add("Rotate right with carry " + TextBox1.Text + " " + TextBox5.Text)
                        Label1.Visible = False
                        Label5.Visible = False
                        TextBox1.Visible = False
                        TextBox5.Visible = False
                    End If
                End If
            Else
                inConfirm = True
                If instruction.Equals("write string") Then
                    Label1.Text = "String to be written: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("write declared string") Then
                    Label1.Text = "Name of the string: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("write constant (8 bit)") Then
                    Label1.Text = "Value of the constant: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("write declared constant (8 bit)") Then
                    Label1.Text = "Name of the constant: "
                    Label5.Text = "Output length: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("assembly instruction") Then
                    Label1.Text = "Assembly instruction: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("write constant (16 bit)") Then
                    Label1.Text = "Value of the constant: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("write declared constant (16 bit)") Then
                    Label1.Text = "Name of the constant: "
                    Label5.Text = "Output length: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("set value to variable (8 bit)") Then
                    Label1.Text = "Name of the variable: "
                    Label5.Text = "New value/other variable: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("set value to variable (16 bit)") Then
                    Label1.Text = "Name of the variable: "
                    Label5.Text = "New value/other variable: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("add to variable (8 bit)") Then
                    Label1.Text = "Name of the variable: "
                    Label5.Text = "New value/other variable: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("add to variable (16 bit)") Then
                    Label1.Text = "Name of the variable: "
                    Label5.Text = "New value/other variable: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("subtract to variable (8 bit)") Then
                    Label1.Text = "Name of the variable: "
                    Label5.Text = "New value/other variable: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("subtract to variable (16 bit)") Then
                    Label1.Text = "Name of the variable: "
                    Label5.Text = "New value/other variable: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("divide to variable (8 bit)") Then
                    Label1.Text = "Name of the variable: "
                    Label5.Text = "New value/other variable: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("divide to variable (16 bit)") Then
                    Label1.Text = "Name of the variable: "
                    Label5.Text = "New value/other variable: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("set variable to time hour (8 bit)") Then
                    Label1.Text = "Name of the variable: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("set variable to time hour (16 bit)") Then
                    Label1.Text = "Name of the variable: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("set variable to time minutes (8 bit)") Then
                    Label1.Text = "Name of the variable: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("set variable to time minutes (16 bit)") Then
                    Label1.Text = "Name of the variable: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("set variable to time seconds (8 bit)") Then
                    Label1.Text = "Name of the variable: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("set variable to time seconds (16 bit)") Then
                    Label1.Text = "Name of the variable: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("write char") Then
                    Label1.Text = "Char code/character: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("generate overflow") Then
                    inConfirm = False
                    ListBox1.Items.Add("Generate overflow")
                ElseIf instruction.Equals("input number to variable (8 bit)") Then
                    Label1.Text = "Variable name: "
                    TextBox1.Text = ""
                    Label5.Text = "Input length: "
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("input number to variable (16 bit)") Then
                    Label1.Text = "Variable name: "
                    TextBox1.Text = ""
                    Label5.Text = "Input length: "
                    TextBox5.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("multiplicate to variable (8 bit)") Then
                    Label1.Text = "Variable name: "
                    Label5.Text = "New variable/other value: "
                    TextBox5.Text = ""
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("multiplicate to variable (16 bit)") Then
                    Label1.Text = "Variable name: "
                    Label5.Text = "New variable/other value: "
                    TextBox5.Text = ""
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("write current memory") Then
                    inConfirm = False
                    ListBox1.Items.Add("Write current memory")
                ElseIf instruction.Equals("clear registers") Then
                    inConfirm = False
                    ListBox1.Items.Add("Clear registers")
                ElseIf instruction.Equals("input char to variable (8 bit)") Then
                    Label1.Text = "Variable name: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("input char to variable (16 bit)") Then
                    Label1.Text = "Variable name: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("leave blank line") Then
                    inConfirm = False
                    ListBox1.Items.Add("Leave blank line")
                ElseIf instruction.Equals("write decimal number (8 bit)") Then
                    Label1.Text = "First operand: "
                    Label5.Text = "Second operand: "
                    Label6.Text = "Aproximation places: "
                    Label7.Text = "Number length: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    TextBox6.Text = ""
                    TextBox7.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                    Label5.Visible = True
                    TextBox5.Visible = True
                    Label6.Visible = True
                    TextBox6.Visible = True
                    Label7.Visible = True
                    TextBox7.Visible = True
                ElseIf instruction.Equals("convert 8 bit variable to 16 bit variable") Then
                    Label1.Text = "8 bit variable name: "
                    Label5.Text = "16 bit variable name: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    Label5.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("convert 16 bit variable to 8 bit variable") Then
                    Label1.Text = "16 bit variable name: "
                    Label5.Text = "8 bit variable name: "
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    Label1.Visible = True
                    Label5.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                ElseIf instruction.Equals("start label") Or instruction.Equals("jump to label") Then
                    Label1.Text = "Name of the label: "
                    TextBox1.Text = ""
                    Label1.Visible = True
                    TextBox1.Visible = True
                ElseIf instruction.Equals("compare two elements") Then
                    Label1.Text = "First operand: "
                    Label5.Text = "Second operand: "
                    Label6.Text = "Comparing operand: "
                    Label7.Text = "Label to jump if true: "
                    Label1.Visible = True
                    Label5.Visible = True
                    Label6.Visible = True
                    Label7.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                    TextBox7.Visible = True
                    ComboBox1.Visible = True
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    TextBox7.Text = ""
                    ComboBox1.SelectedItem = ""
                    ComboBox1.Items.Clear()
                    ComboBox1.Items.Add("==")
                    ComboBox1.Items.Add("!=")
                    ComboBox1.Items.Add(">")
                    ComboBox1.Items.Add(">=")
                    ComboBox1.Items.Add("<")
                    ComboBox1.Items.Add("<=")
                ElseIf instruction.Equals("end program") Then
                    inConfirm = False
                    ListBox1.Items.Add("End program")
                ElseIf instruction.Equals("apply discount to variable (8 bit)") Or instruction.Equals("apply discount to variable (16 bit)") Then
                    Label1.Text = "Variable to apply discount: "
                    Label5.Text = "Discount value: "
                    Label6.Text = "Destionation variable: "
                    Label1.Visible = True
                    Label5.Visible = True
                    Label6.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                    TextBox6.Visible = True
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    TextBox6.Text = ""
                ElseIf instruction.Equals("check if number is in range") Then
                    Label1.Text = "Variable name: "
                    Label5.Text = "Min range: "
                    Label6.Text = "Max range: "
                    Label7.Text = "Label to jump if true: "
                    Label1.Visible = True
                    Label5.Visible = True
                    Label6.Visible = True
                    Label7.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                    TextBox6.Visible = True
                    TextBox7.Visible = True
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    TextBox6.Text = ""
                    TextBox7.Text = ""
                ElseIf instruction.Equals("cycle while") Or instruction.Equals("cycle do while") Then
                    Label1.Text = "Subject inc var/reg: "
                    Label5.Text = "Times to repeat: "
                    Label6.Text = "Cycle label: "
                    Label7.Text = "End cycle label: "
                    Label1.Visible = True
                    Label5.Visible = True
                    Label6.Visible = True
                    Label7.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                    TextBox6.Visible = True
                    TextBox7.Visible = True
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                    TextBox6.Text = ""
                    TextBox7.Text = ""
                ElseIf instruction.Equals("increment variable") Then
                    Label1.Text = "Variable to increment: "
                    Label1.Visible = True
                    TextBox1.Visible = True
                    TextBox1.Text = ""
                ElseIf instruction.Equals("decrement variable") Then
                    Label1.Text = "Variable to decrement: "
                    Label1.Visible = True
                    TextBox1.Visible = True
                    TextBox1.Text = ""
                ElseIf instruction.Equals("set variable to time hundreds (8 bit)") Then
                    Label1.Text = "Variable name: "
                    Label1.Visible = True
                    TextBox1.Visible = True
                    TextBox1.Text = ""
                ElseIf instruction.Equals("set variable to time hundreds (16 bit)") Then
                    Label1.Text = "Variable name: "
                    Label1.Visible = True
                    TextBox1.Visible = True
                    TextBox1.Text = ""
                ElseIf instruction.Equals("program pause") Then
                    Label1.Text = "Number of cycles: "
                    Label1.Visible = True
                    TextBox1.Visible = True
                    TextBox1.Text = ""
                ElseIf instruction.Equals("clear array") Then
                    Label1.Text = "Array name: "
                    Label5.Text = "Array size: "
                    Label1.Visible = True
                    Label5.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                ElseIf instruction.Equals("logical and") Or instruction.Equals("logical or") Or instruction.Equals("logical not") Then
                    Label1.Text = "Target register/variable: "
                    Label5.Text = "Logical value to inject: "
                    Label1.Visible = True
                    Label5.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                ElseIf instruction.Equals("right shift") Or instruction.Equals("left shift") Then
                    Label1.Text = "Target register/variable: "
                    Label5.Text = "Positions to shift: "
                    Label1.Visible = True
                    Label5.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                ElseIf instruction.Equals("jump to label if carry") Or instruction.Equals("jump to label if not carry") Then
                    Label1.Text = "Target label to jump in: "
                    Label1.Visible = True
                    TextBox1.Visible = True
                    TextBox1.Text = ""
                ElseIf instruction.Equals("clear carry") Then
                    inConfirm = False
                    ListBox1.Items.Add("Clear carry")
                ElseIf instruction.Equals("set carry") Then
                    inConfirm = False
                    ListBox1.Items.Add("Set carry")
                ElseIf instruction.Equals("rotate right") Or instruction.Equals("rotate left") Or instruction.Equals("rotate right with carry") Or instruction.Equals("rotate left with carry") Then
                    Label1.Text = "Target register/variable: "
                    Label5.Text = "Positions to rotate: "
                    Label1.Visible = True
                    Label5.Visible = True
                    TextBox1.Visible = True
                    TextBox5.Visible = True
                    TextBox1.Text = ""
                    TextBox5.Text = ""
                End If
                TextBox1.Select()
            End If
        End If
        GenerateAssembly()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If inConfirm Then
            inConfirm = False
            TextBox1.Visible = False
            Label1.Visible = False
            TextBox5.Visible = False
            Label5.Visible = False
            ComboBox1.Visible = False
            Label6.Visible = False
            Label7.Visible = False
            TextBox6.Visible = False
            TextBox7.Visible = False
        Else
            If Not ListBox1.SelectedItem = "" And Not ListBox1.SelectedItem = Nothing Then
                ListBox1.Items.Remove(ListBox1.Items(ListBox1.SelectedIndex))
            End If
        End If
        GenerateAssembly()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Not ListBox3.SelectedItem = "" And Not ListBox3.SelectedItem = Nothing Then
            Dim instruction As String = ListBox3.SelectedItem.ToString().ToLower()
            If inConfirm1 Then
                If instruction.Equals("declare constant (8 bit)") Then
                    If Not TextBox3.Text = "" And Not TextBox4.Text = "" And IsNumeric(translateNumber(TextBox3.Text)) Then
                        inConfirm1 = False
                        ListBox4.Items.Add("(8 bit) Declare constant " + TextBox4.Text + " " + TextBox3.Text)
                        TextBox3.Visible = False
                        Label3.Visible = False
                        Label4.Visible = False
                        TextBox4.Visible = False
                    End If
                ElseIf instruction.Equals("declare empty variable (8 bit)") Then
                    If Not TextBox3.Text = "" Then
                        inConfirm1 = False
                        ListBox4.Items.Add("(8 bit) Declare empty variable " + TextBox3.Text)
                        TextBox3.Visible = False
                        Label3.Visible = False
                    End If
                ElseIf instruction.Equals("declare string") Then
                    If Not TextBox3.Text = "" And Not TextBox4.Text = "" Then
                        inConfirm1 = False
                        ListBox4.Items.Add("Declare string " + TextBox3.Text + " " + TextBox4.Text)
                        TextBox3.Visible = False
                        Label3.Visible = False
                        Label4.Visible = False
                        TextBox4.Visible = False
                    End If
                ElseIf instruction.Equals("declare constant (16 bit)") Then
                    If Not TextBox3.Text = "" And Not TextBox4.Text = "" And IsNumeric(translateNumber(TextBox3.Text)) Then
                        inConfirm1 = False
                        ListBox4.Items.Add("(16 bit) Declare constant " + TextBox4.Text + " " + TextBox3.Text)
                        TextBox3.Visible = False
                        Label3.Visible = False
                        Label4.Visible = False
                        TextBox4.Visible = False
                    End If
                ElseIf instruction.Equals("declare empty variable (16 bit)") Then
                    If Not TextBox3.Text = "" Then
                        inConfirm1 = False
                        ListBox4.Items.Add("(16 bit) Declare empty variable " + TextBox3.Text)
                        TextBox3.Visible = False
                        Label3.Visible = False
                    End If
                ElseIf instruction.Equals("declare array (8 bit)") Then
                    If Not TextBox3.Text = "" And Not TextBox4.Text = "" And Not TextBox8.Text = "" Then
                        inConfirm1 = False
                        ListBox4.Items.Add("(8 bit) Declare array " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox8.Text)
                        TextBox3.Visible = False
                        TextBox4.Visible = False
                        TextBox8.Visible = False
                        Label3.Visible = False
                        Label4.Visible = False
                        Label8.Visible = False
                    End If
                End If
            ElseIf instruction.Equals("declare array (16 bit)") Then
                If Not TextBox3.Text = "" And Not TextBox4.Text = "" And Not TextBox8.Text = "" Then
                    inConfirm1 = False
                    ListBox4.Items.Add("(16 bit) Declare array " + TextBox3.Text + " " + TextBox4.Text + " " + TextBox8.Text)
                    TextBox3.Visible = False
                    TextBox4.Visible = False
                    TextBox8.Visible = False
                    Label3.Visible = False
                    Label4.Visible = False
                    Label8.Visible = False
                End If
            Else
                inConfirm1 = True
                If instruction.Equals("declare constant (8 bit)") Then
                    Label3.Text = "Value of the constant: "
                    Label4.Text = "Name of the constant: "
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    Label3.Visible = True
                    Label4.Visible = True
                    TextBox3.Visible = True
                    TextBox4.Visible = True
                ElseIf instruction.Equals("declare empty variable (8 bit)") Then
                    Label3.Text = "Name of the variable: "
                    TextBox3.Text = ""
                    Label3.Visible = True
                    TextBox3.Visible = True
                ElseIf instruction.Equals("declare string") Then
                    Label3.Text = "Name of the variable: "
                    Label4.Text = "Value of the variable: "
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    Label3.Visible = True
                    Label4.Visible = True
                    TextBox3.Visible = True
                    TextBox4.Visible = True
                ElseIf instruction.Equals("declare constant (16 bit)") Then
                    Label3.Text = "Value of the constant: "
                    Label4.Text = "Name of the constant: "
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    Label3.Visible = True
                    Label4.Visible = True
                    TextBox3.Visible = True
                    TextBox4.Visible = True
                ElseIf instruction.Equals("declare empty variable (16 bit)") Then
                    Label3.Text = "Name of the variable: "
                    TextBox3.Text = ""
                    Label3.Visible = True
                    TextBox3.Visible = True
                ElseIf instruction.Equals("declare array (8 bit)") Or instruction.Equals("declare array (16 bit)") Then
                    Label3.Text = "Name of the array: "
                    Label4.Text = "Size of the array: "
                    Label8.Text = "Initial value: "
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox8.Text = ""
                    TextBox3.Visible = True
                    TextBox4.Visible = True
                    TextBox8.Visible = True
                    Label3.Visible = True
                    Label4.Visible = True
                    Label8.Visible = True
                End If
                TextBox3.Select()
            End If
        End If
        GenerateAssembly()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If inConfirm1 Then
            inConfirm1 = False
            TextBox3.Visible = False
            Label3.Visible = False
            TextBox4.Visible = False
            Label4.Visible = False
            TextBox8.Visible = False
            Label8.Visible = False
        Else
            If Not ListBox4.SelectedItem = "" And Not ListBox4.SelectedItem = Nothing Then
                ListBox4.Items.Remove(ListBox4.Items(ListBox4.SelectedIndex))
            End If
        End If
        GenerateAssembly()
    End Sub
    Public Function translateNumber(ByVal numberToTranslate As String) As UShort
        numberToTranslate = replaceStartingZeros(numberToTranslate)
        Dim numberTranslated As UShort = 0
        numberToTranslate = numberToTranslate.ToUpper()
        numberToTranslate = numberToTranslate.Replace(" ", "")
        If numberToTranslate.EndsWith("D") Then
            numberToTranslate = numberToTranslate.Substring(0, numberToTranslate.Length - 1)
            numberToTranslate = replaceStartingZeros(numberToTranslate)
            numberTranslated = UShort.Parse(numberToTranslate)
        ElseIf numberToTranslate.EndsWith("B") Then
            numberToTranslate = numberToTranslate.Substring(0, numberToTranslate.Length - 1)
            numberToTranslate = replaceStartingZeros(numberToTranslate)
            numberTranslated = convertBinaryToUShort(numberToTranslate)
        ElseIf numberToTranslate.EndsWith("H") Then
            numberToTranslate = numberToTranslate.Substring(0, numberToTranslate.Length - 1)
            numberToTranslate = replaceStartingZeros(numberToTranslate)
            numberTranslated = convertHexadecimalToUShort(numberToTranslate)
        End If
        Return numberTranslated
    End Function
    Public Function replaceStartingZeros(ByVal numberToCorrect As String) As String
        Dim toContinue As Boolean = False
        If numberToCorrect.StartsWith("0") Then
            toContinue = True
            Dim newString As String = ""
            For Each c As Char In numberToCorrect.ToCharArray()
                Dim s As String = c.ToString()
                If toContinue Then
                    If s <> "0" Then
                        newString += s
                        toContinue = False
                    End If
                Else
                    newString += s
                End If
            Next
        End If
        Return numberToCorrect
    End Function
    Public Function convertBinaryToUShort(ByVal binaryNumber As String) As UShort
        Dim convertedNumber As UShort = CUShort(System.Convert.ToInt32(binaryNumber, 2))
        Return convertedNumber
    End Function
    Public Function convertHexadecimalToUShort(ByVal hexadecimalNumber As String) As UShort
        Dim convertedNumber As UShort = CUShort(System.Convert.ToInt32(hexadecimalNumber, 16))
        Return convertedNumber
    End Function
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Not ListBox1.SelectedItem = "" And Not ListBox1.SelectedIndex <= 0 Then
            Dim currentIndex As Integer = ListBox1.SelectedIndex
            Dim newIndex As Integer = currentIndex - 1
            Dim items() As String = New String(ListBox1.Items.Count) {}
            For i = 0 To ListBox1.Items.Count - 1
                items(i) = ListBox1.Items(i)
            Next
            items(currentIndex) = ListBox1.Items(newIndex)
            items(newIndex) = ListBox1.Items(currentIndex)
            ListBox1.Items.Clear()
            For Each item As String In items
                Try
                    ListBox1.Items.Add(item)
                Catch ex As Exception
                End Try
            Next
        End If
        GenerateAssembly()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Not ListBox1.SelectedItem = "" And Not ListBox1.SelectedIndex < 0 And Not ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then
            Dim currentIndex As Integer = ListBox1.SelectedIndex
            Dim newIndex As Integer = currentIndex + 1
            Dim items() As String = New String(ListBox1.Items.Count) {}
            For i = 0 To ListBox1.Items.Count - 1
                items(i) = ListBox1.Items(i)
            Next
            items(currentIndex) = ListBox1.Items(newIndex)
            items(newIndex) = ListBox1.Items(currentIndex)
            ListBox1.Items.Clear()
            For Each item As String In items
                Try
                    ListBox1.Items.Add(item)
                Catch ex As Exception
                End Try
            Next
            ListBox1.SelectedItem = currentIndex
        End If
        GenerateAssembly()
    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyValue = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub
    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyValue = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub
    Private Sub TextBox6_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox6.KeyDown
        If e.KeyValue = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub
    Private Sub TextBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyValue = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub
    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyValue = Keys.Enter Then
            Button4.PerformClick()
        End If
    End Sub
    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyValue = Keys.Enter Then
            Button4.PerformClick()
        End If
    End Sub
    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        If e.KeyValue = Keys.Back Or e.KeyValue = Keys.Cancel Then
            Button2.PerformClick()
        ElseIf e.KeyValue = Keys.Up Or e.KeyValue = Keys.W Then
            Button6.PerformClick()
        ElseIf e.KeyValue = Keys.Down Or e.KeyValue = Keys.S Then
            Button5.PerformClick()
        End If
    End Sub
    Private Sub ListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyDown
        If e.KeyValue = Keys.Back Or e.KeyValue = Keys.Cancel Then
            Button3.PerformClick()
        ElseIf e.KeyValue = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If LinkLabel1.Text = "Start tutorial" Then
            LinkLabel1.Text = "Stop tutorial"
            InfoWindow.stringToBeWritten = "Welcome to the tutorial."
            InfoWindow.Show()
            Timer1.Start()
        Else
            LinkLabel1.Text = "Start tutorial"
            Timer1.Stop()
            tutorialStep = 0
            InfoWindow.Close()
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If tutorialStep = 0 Then
            tutorialStep = 1
            InfoWindow.stringToBeWritten = "Every information that you need will be explained now, so please be patient and pay attention."
        ElseIf tutorialStep = 1 Then
            tutorialStep = 2
            InfoWindow.stringToBeWritten = "This program is created to advantage you to write your easy Assembly 8086 code to learn what are you really doing with the machine."
        ElseIf tutorialStep = 2 Then
            tutorialStep = 3
            TabControl1.SelectedIndex = 0
            InfoWindow.stringToBeWritten = "In this page, you are free to add every instruction you want to your program. In this page, you add every thing that goes after the .CODE"
        ElseIf tutorialStep = 3 Then
            tutorialStep = 4
            TabControl1.SelectedIndex = 0
            InfoWindow.stringToBeWritten = "For every instruction (excepts something that will be explained) you need to add operands to make it working."
        ElseIf tutorialStep = 4 Then
            tutorialStep = 5
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 1
            InfoWindow.stringToBeWritten = "Let's go with the instructions. With this instruction, you can write a string, for example: Hello World! - So you will need a string operand."
        ElseIf tutorialStep = 5 Then
            tutorialStep = 6
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 2
            InfoWindow.stringToBeWritten = "With this instruction, you can write a char. Insert the ASCII code of the char or the variable with the ASCII code (46D, 12H, 10111B, '.' etc...)"
        ElseIf tutorialStep = 6 Then
            tutorialStep = 7
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 3
            InfoWindow.stringToBeWritten = "With this, you can insert in input a number with specified variable that will contain it and the length of the inserted number (1-3)"
        ElseIf tutorialStep = 7 Then
            tutorialStep = 8
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 3
            InfoWindow.stringToBeWritten = "Ooh, calm back. Are you not thinking that how you will use these 4 arrows? I will explain to you how."
        ElseIf tutorialStep = 8 Then
            tutorialStep = 9
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 3
            InfoWindow.stringToBeWritten = "The first arrow has 2 functions: when you select an instruction to add in the first list, operands will be requested."
            Button1.PerformClick()
        ElseIf tutorialStep = 9 Then
            tutorialStep = 10
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 3
            TextBox1.Text = "x"
            TextBox5.Text = "2"
            InfoWindow.stringToBeWritten = "Nextly, when you re-click it, you will add the instruction to the second list with the inserted operands."
            Button1.PerformClick()
        ElseIf tutorialStep = 10 Then
            tutorialStep = 11
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 3
            ListBox1.SelectedIndex = 0
            InfoWindow.stringToBeWritten = "You can see that the instruction has been added. But how can I remove it? Easy, you need to click the third button (<)."
            Button2.PerformClick()
        ElseIf tutorialStep = 11 Then
            tutorialStep = 12
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 3
            InfoWindow.stringToBeWritten = "The second button is needed if you want to move the selected instruction up of an element. Same for the fourth button, it will go down."
        ElseIf tutorialStep = 12 Then
            tutorialStep = 13
            TabControl1.SelectedIndex = 0
            ListBox2.SelectedIndex = 3
            InfoWindow.stringToBeWritten = "I will not explain to you how other instructions work because you will know it by the name. Go to see the variables now."
        ElseIf tutorialStep = 13 Then
            tutorialStep = 14
            TabControl1.SelectedIndex = 1
            ListBox3.SelectedIndex = 1
            InfoWindow.stringToBeWritten = "Welcome to the variables page. Here you can add every variable needed for your program, 8 and 16 bit. Let's see."
        ElseIf tutorialStep = 14 Then
            tutorialStep = 15
            TabControl1.SelectedIndex = 1
            ListBox3.SelectedIndex = 1
            InfoWindow.stringToBeWritten = "This is needed to declare a 8 bit constant (lower or equal to 255) with the name you want and you will see that it will be added."
        ElseIf tutorialStep = 15 Then
            tutorialStep = 16
            TabControl1.SelectedIndex = 1
            ListBox3.SelectedIndex = 2
            InfoWindow.stringToBeWritten = "Remember that now you work on the data segment, so after .DATA. This is needed to add an empty variable (?)."
        ElseIf tutorialStep = 16 Then
            tutorialStep = 17
            TabControl1.SelectedIndex = 1
            ListBox3.SelectedIndex = 2
            InfoWindow.stringToBeWritten = "I think that you know how to use the other types of declarations, so we will go to the final part."
        ElseIf tutorialStep = 17 Then
            tutorialStep = 18
            TabControl1.SelectedIndex = 2
            InfoWindow.stringToBeWritten = "This is the page where you can see your generated Assembly code with the instructions and the variables that you added to your program."
        ElseIf tutorialStep = 18 Then
            tutorialStep = 19
            TabControl1.SelectedIndex = 2
            InfoWindow.stringToBeWritten = "I hope that you enjoyed this tutorial to use the program. Have a good luck using it and do not tell to anyone ;)"
        Else
            Timer1.Stop()
            InfoWindow.stringToBeWritten = ""
            InfoWindow.Close()
            LinkLabel1.Text = "Start tutorial"
            TabControl1.SelectedIndex = 0
        End If
    End Sub
    Private Sub ListBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox3.KeyDown
        If e.KeyValue = Keys.Back Or e.KeyValue = Keys.Cancel Then
            Button3.PerformClick()
        ElseIf e.KeyValue = Keys.Enter Then
            Button4.PerformClick()
        End If
    End Sub
    Private Sub ListBox2_DoubleClick(sender As Object, e As EventArgs) Handles ListBox2.DoubleClick
        Button1.PerformClick()
    End Sub
    Private Sub ListBox3_DoubleClick(sender As Object, e As EventArgs) Handles ListBox3.DoubleClick
        Button4.PerformClick()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.DefaultExt = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Try
                Dim fileData As String = System.IO.File.ReadAllText(OpenFileDialog1.FileName)
                ListBox1.Items.Clear()
                ListBox4.Items.Clear()
                Dim codePart As String = ""
                Dim dataPart As String = ""
                Dim part1() As String = Split(fileData, "BEGIN CODE PART")
                Dim part2() As String = Split(part1(1), "END CODE PART")
                codePart = part2(0)
                Dim part3() As String = Split(fileData, "BEGIN DATA PART")
                Dim part4() As String = Split(part3(1), "END DATA PART")
                dataPart = part4(0)
                For Each codeLine As String In Split(codePart, vbNewLine)
                    If Not codeLine.Replace(" ", "") = "" Then
                        ListBox1.Items.Add(codeLine)
                    End If

                Next
                For Each dataLine As String In Split(dataPart, vbNewLine)
                    If Not dataLine.Replace(" ", "") = "" Then
                        ListBox4.Items.Add(dataLine)
                    End If
                Next
                GenerateAssembly()
            Catch ex As Exception
                MessageBox.Show("Invalid data file!")
                ListBox1.Items.Clear()
                ListBox4.Items.Clear()
            End Try
        End If
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        SaveFileDialog2.FileName = ""
        If SaveFileDialog2.ShowDialog() = DialogResult.OK Then
            Dim fileData As String = "BEGIN CODE PART"
            For Each codeLine As String In ListBox1.Items
                fileData += vbNewLine + codeLine
            Next
            fileData += vbNewLine + "END CODE PART" + vbNewLine + "BEGIN DATA PART"
            For Each dataLine As String In ListBox4.Items
                fileData += vbNewLine + dataLine
            Next
            fileData += vbNewLine + "END DATA PART"
            System.IO.File.WriteAllText(SaveFileDialog2.FileName, fileData)
        End If
    End Sub
End Class