Public Class Form1

    Dim board(7, 7) As String
    Dim valid(7, 7) As Boolean
    Dim canMove As Boolean = True
    Dim blackTurn As Integer = True

    Dim multi As Integer = 100

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Size = New Point((8 * multi) + 60, (8 * multi) + 100)
        Me.Location = New Point((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2), (My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2))
        init()
    End Sub

    Private Sub gameBoard_Paint(sender As Object, e As PaintEventArgs) Handles gameBoard.Paint
        Dim g As Graphics = e.Graphics

        Dim bBrush As SolidBrush = Brushes.DarkOliveGreen
        For ix = 0 To 7
            For iy = 0 To 7
                g.FillRectangle(bBrush, New Rectangle(New Point(multi * ix, multi * iy), New Size(multi, multi)))
                If bBrush.Color = Color.DarkOliveGreen Then
                    bBrush = Brushes.ForestGreen
                Else
                    bBrush = Brushes.DarkOliveGreen
                End If
            Next
            If bBrush.Color = Color.DarkOliveGreen Then
                bBrush = Brushes.ForestGreen
            Else
                bBrush = Brushes.DarkOliveGreen
            End If
        Next

        Dim p1score As Integer = 0
        Dim p2score As Integer = 0
        Dim cBrush As SolidBrush = Brushes.GhostWhite
        For ix = 0 To 7
            For iy = 0 To 7
                Select Case board(ix, iy)
                    Case "b"
                        cBrush = Brushes.Black
                        g.FillEllipse(cBrush, New Rectangle(New Point((multi * ix) + multi * 0.125, (multi * iy) + multi * 0.125), New Size(multi * 0.75, multi * 0.75)))
                        p1score += 1
                    Case "w"
                        cBrush = Brushes.GhostWhite
                        g.FillEllipse(cBrush, New Rectangle(New Point((multi * ix) + multi * 0.125, (multi * iy) + multi * 0.125), New Size(multi * 0.75, multi * 0.75)))
                        p2score += 1
                End Select
            Next
        Next

        p1scoreLabel.Text = p1score
        p2scoreLabel.Text = p2score

        findPossibleMoves(g)

        If p1score + p2score = 64 Or p1score = 0 Or p2score = 0 Then
            If p1score > p2score Then
                MsgBox("Winner : Player1")
            Else
                MsgBox("Winner : Player2")
            End If
        End If

        canMove = False
        For ix = 0 To 7
            For iy = 0 To 7
                If valid(ix, iy) Then
                    canMove = True
                End If
            Next
        Next

    End Sub

    Private Sub gameBoard_MouseDown(sender As Object, e As MouseEventArgs) Handles gameBoard.MouseDown
        Dim p As String 'player
        Dim t As String 'opponent
        Dim posX As Integer = pointToInteger(e.X)
        Dim posY As Integer = pointToInteger(e.Y)
        Dim hBrush As SolidBrush = Brushes.LightSkyBlue

        Debug.WriteLine("x: " + posX.ToString + " y: " + posY.ToString)

        If valid(posX, posY) And canMove Then

            If blackTurn Then
                p = "b"
                t = "w"
                indicator.BackColor = Color.GhostWhite
            Else
                p = "w"
                t = "b"
                indicator.BackColor = Color.Black
            End If

            board(posX, posY) = p

            Debug.WriteLine("==== " & p & " flipping " & t & " ====")

            If posY > 1 Then
                If board(posX, posY - 1) = t Then 'check north for opponent
                    If recursiveFlip("n", p, t, posX, posY) Then
                        board(posX, posY) = p
                    End If
                End If
            End If
            If posX > 1 And posY > 1 Then
                If board(posX - 1, posY - 1) = t Then 'check northwest for opponent
                    If recursiveFlip("nw", p, t, posX, posY) Then
                        board(posX, posY) = p
                    End If
                End If
            End If
            If posX > 1 Then
                If board(posX - 1, posY) = t Then 'check west for opponent
                    If recursiveFlip("w", p, t, posX, posY) Then
                        board(posX, posY) = p
                    End If
                End If
            End If
            If posX > 1 And posY < 7 Then
                If board(posX - 1, posY + 1) = t Then 'check southwest for opponent
                    If recursiveFlip("sw", p, t, posX, posY) Then
                        board(posX, posY) = p
                    End If
                End If
            End If
            If posY < 7 Then
                If board(posX, posY + 1) = t Then 'check south for opponent
                    If recursiveFlip("s", p, t, posX, posY) Then
                        board(posX, posY) = p
                    End If
                End If
            End If
            If posX < 7 And posY < 7 Then
                If board(posX + 1, posY + 1) = t Then 'check southeast for opponent
                    If recursiveFlip("se", p, t, posX, posY) Then
                        board(posX, posY) = p
                    End If
                End If
            End If
            If posX < 7 Then
                If board(posX + 1, posY) = t Then 'check east for opponent
                    If recursiveFlip("e", p, t, posX, posY) Then
                        board(posX, posY) = p
                    End If
                End If
            End If
            If posX < 7 And posY > 1 Then
                If board(posX + 1, posY - 1) = t Then 'check northeast for opponent
                    If recursiveFlip("ne", p, t, posX, posY) Then
                        board(posX, posY) = p
                    End If
                End If
            End If

            If blackTurn Then
                blackTurn = False
            Else
                blackTurn = True
            End If

            gameBoard.Invalidate()

            'cleans up. only newly found valid can be placed
            For posX = 0 To 7
                For posY = 0 To 7
                    valid(posX, posY) = False
                Next
            Next
        End If

    End Sub

    Public Sub findPossibleMoves(g As Graphics)
        Dim p As String 'player
        Dim t As String 'opponent
        Dim hBrush As SolidBrush = Brushes.LightSkyBlue

        If blackTurn Then
            p = "b"
            t = "w"
        Else
            p = "w"
            t = "b"
        End If

        Debug.WriteLine("==== " & p & " finding " & t & " ====")

        'checks for target in near area, and then recursively searches in that direction
        'to check whether they can be taken
        For posX = 0 To 7
            For posY = 0 To 7
                If board(posX, posY) = p Then 'if player found
                    If posY > 1 Then
                        If board(posX, posY - 1) = t Then 'check north for opponent
                            Debug.WriteLine("found " & t & " @ " & posX & " " & posY & "heading n")
                            Dim r As New Point
                            r = recursiveFind("n", p, t, posX, posY)
                            If r.X > -1 Then
                                g.FillEllipse(hBrush, New Rectangle(New Point((multi * 0.4) + r.X * 100, (multi * 0.4) + r.Y * 100), New Size(20, 20)))
                            End If
                        End If
                    End If
                    If posX > 1 And posY > 1 Then
                    If board(posX - 1, posY - 1) = t Then 'check northwest for opponent
                        Debug.WriteLine("found " & t & " @ " & posX & " " & posY & " heading nw")
                        Dim r As New Point
                        r = recursiveFind("nw", p, t, posX, posY)
                        If r.X > -1 Then
                            g.FillEllipse(hBrush, New Rectangle(New Point((multi * 0.4) + r.X * 100, (multi * 0.4) + r.Y * 100), New Size(20, 20)))
                        End If
                    End If
                End If
                If posX > 1 Then
                    If board(posX - 1, posY) = t Then 'check west for opponent
                        Debug.WriteLine("found " & t & " @ " & posX & " " & posY & " heading w")
                        Dim r As New Point
                        r = recursiveFind("w", p, t, posX, posY)
                        If r.X > -1 Then
                            g.FillEllipse(hBrush, New Rectangle(New Point((multi * 0.4) + r.X * 100, (multi * 0.4) + r.Y * 100), New Size(20, 20)))
                        End If
                    End If
                End If
                If posX > 1 And posY < 7 Then
                    If board(posX - 1, posY + 1) = t Then 'check southwest for opponent
                        Debug.WriteLine("found " & t & " @ " & posX & " " & posY & " heading sw")
                        Dim r As New Point
                        r = recursiveFind("sw", p, t, posX, posY)
                        If r.X > -1 Then
                            g.FillEllipse(hBrush, New Rectangle(New Point((multi * 0.4) + r.X * 100, (multi * 0.4) + r.Y * 100), New Size(20, 20)))
                        End If
                    End If
                End If
                If posY < 7 Then
                    If board(posX, posY + 1) = t Then 'check south for opponent
                        Debug.WriteLine("found " & t & " @ " & posX & " " & posY & " heading s")
                        Dim r As New Point
                        r = recursiveFind("s", p, t, posX, posY)
                        If r.X > -1 Then
                            g.FillEllipse(hBrush, New Rectangle(New Point((multi * 0.4) + r.X * 100, (multi * 0.4) + r.Y * 100), New Size(20, 20)))
                        End If
                    End If
                End If
                If posX < 7 And posY < 7 Then
                    If board(posX + 1, posY + 1) = t Then 'check southeast for opponent
                        Debug.WriteLine("found " & t & " @ " & posX & " " & posY & " heading se")
                        Dim r As New Point
                        r = recursiveFind("se", p, t, posX, posY)
                        If r.X > -1 Then
                            g.FillEllipse(hBrush, New Rectangle(New Point((multi * 0.4) + r.X * 100, (multi * 0.4) + r.Y * 100), New Size(20, 20)))
                        End If
                    End If
                End If
                If posX < 7 Then
                    If board(posX + 1, posY) = t Then 'check east for opponent
                        Debug.WriteLine("found " & t & " @ " & posX & " " & posY & " heading e")
                        Dim r As New Point
                        r = recursiveFind("e", p, t, posX, posY)
                        If r.X > -1 Then
                            g.FillEllipse(hBrush, New Rectangle(New Point((multi * 0.4) + r.X * 100, (multi * 0.4) + r.Y * 100), New Size(20, 20)))
                        End If
                    End If
                End If
                    If posX < 7 And posY > 1 Then
                        If board(posX + 1, posY - 1) = t Then 'check northeast for opponent
                            Debug.WriteLine("found " & t & " @ " & posX & " " & posY & " heading ne")
                            Dim r As New Point
                            r = recursiveFind("ne", p, t, posX, posY)
                            If r.X > -1 Then
                                g.FillEllipse(hBrush, New Rectangle(New Point((multi * 0.4) + r.X * 100, (multi * 0.4) + r.Y * 100), New Size(20, 20)))
                            End If
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    Public Function recursiveFind(direction As String, p As String, t As String, posX As Integer, posY As Integer)
        Select Case direction
            Case "n"
                posY -= 1
            Case "nw"
                posX -= 1
                posY -= 1
            Case "w"
                posX -= 1
            Case "sw"
                posX -= 1
                posY += 1
            Case "s"
                posY += 1
            Case "se"
                posX += 1
                posY += 1
            Case "e"
                posX += 1
            Case "ne"
                posX += 1
                posY -= 1
        End Select
        If posX > -1 And posX < 8 And posY > -1 And posY < 8 Then
            Select Case board(posX, posY)
                Case p 'if player, quit
                    Debug.WriteLine("failed " & board(posX, posY) & " @ " & posX & "," & posY)
                    Debug.WriteLine("----------")
                    Return New Point(-1, -1)
                Case t 'continue search
                    Debug.WriteLine("searching " & board(posX, posY) & " @ " & posX & "," & posY)
                    Return recursiveFind(direction, p, t, posX, posY)
                Case "o" 'empty, valid option
                    Debug.WriteLine("finished " & board(posX, posY) & " @ " & posX & "," & posY)
                    Debug.WriteLine("----------")
                    valid(posX, posY) = True
                    Return New Point(posX, posY)
            End Select
        End If
        Return New Point(-1, -1)
    End Function

    Public Function recursiveFlip(direction As String, p As String, t As String, posX As Integer, posY As Integer)
        Select Case direction
            Case "n"
                posY -= 1
            Case "nw"
                posX -= 1
                posY -= 1
            Case "w"
                posX -= 1
            Case "sw"
                posX -= 1
                posY += 1
            Case "s"
                posY += 1
            Case "se"
                posX += 1
                posY += 1
            Case "e"
                posX += 1
            Case "ne"
                posX += 1
                posY -= 1
        End Select

        If posX > 8 And direction = "ne" Or posX > 8 And direction = "e" Or posX > 8 And direction = "se" Or posY > 8 And direction = "se" Or posY > 8 And direction = "s" Or posY > 8 And direction = "sw" Or posX < 0 And direction = "nw" Or posX < 0 And direction = "w" Or posX < 0 And direction = "sw" Or posY < 0 And direction = "ne" Or posY < 0 And direction = "n" Or posY < 0 And direction = "nw" Then
            Return False
        End If

        If posX > 7 Or posY > 7 Or posX < 0 Or posY < 0 Then
            Return False
        End If

        Select Case board(posX, posY)
            Case p 'if player, flip
                Debug.WriteLine("finished " & board(posX, posY) & " @ " & posX & "," & posY)
                Debug.WriteLine("----------")
                Return True
            Case t 'continue search
                Debug.WriteLine("searching " & board(posX, posY) & " @ " & posX & "," & posY)
                If recursiveFlip(direction, p, t, posX, posY) Then
                    board(posX, posY) = p
                    Debug.WriteLine("* flipped " & posX & posY & " to " & board(posX, posY))
                    Return True
                End If
            Case "o" 'empty, quit
                Debug.WriteLine("failed " & board(posX, posY) & " @ " & posX & "," & posY)
                Debug.WriteLine("----------")
                Return False
        End Select
        Return False
    End Function

    Public Function pointToInteger(int As Integer)

        If 0 < (int / multi) And (int / multi) < 1 Then
            Return 0
        ElseIf 1 < (int / multi) And (int / multi) < 2 Then
            Return 1
        ElseIf 2 < (int / multi) And (int / multi) < 3 Then
            Return 2
        ElseIf 3 < (int / multi) And (int / multi) < 4 Then
            Return 3
        ElseIf 4 < (int / multi) And (int / multi) < 5 Then
            Return 4
        ElseIf 5 < (int / multi) And (int / multi) < 6 Then
            Return 5
        ElseIf 6 < (int / multi) And (int / multi) < 7 Then
            Return 6
        ElseIf 7 < (int / multi) And (int / multi) < 8 Then
            Return 7
        End If

        Return -1
    End Function

    Private Sub restartButton_Click(sender As Object, e As EventArgs) Handles restartButton.Click
        init()
    End Sub

    Public Sub init()
        Debug.WriteLine("========================")
        Debug.WriteLine("------- new game -------")

        gameBoard.Size = New Point(multi * 8, multi * 8)
        gameBoard.Location = New Point(20, 15)

        indicator.Location = New Point(0, 0)
        indicator.Size = New Point((multi * 8) + 60, (multi * 8) + 30)
        indicator.BackColor = Color.Black
        indicator.SendToBack()

        p1Label.Location = New Point(multi * 2, (multi * 8.1) + (p1Label.Height * 1.5))
        p1scoreLabel.Location = New Point(multi * 3, (multi * 8.1) + (p1scoreLabel.Height * 1.5))
        p2Label.Location = New Point(multi * 5.5, (multi * 8.1) + (p2Label.Height * 1.5))
        p2scoreLabel.Location = New Point(multi * 6.5, (multi * 8.1) + (p2scoreLabel.Height * 1.5))

        p1Label.BringToFront()
        p1scoreLabel.BringToFront()
        p2Label.BringToFront()
        p2scoreLabel.BringToFront()

        restartButton.Location = New Point((multi * 4) - restartButton.Size.Width / 4, (multi * 8.1) + (restartButton.Height / 2))

        'load board
        For ix = 0 To 7
            For iy = 0 To 7
                If (ix = 3 And iy = 3) Or (ix = 4 And iy = 4) Then
                    board(ix, iy) = "b"
                ElseIf (ix = 3 And iy = 4) Or (ix = 4 And iy = 3) Then
                    board(ix, iy) = "w"
                Else
                    board(ix, iy) = "o"
                End If
            Next
        Next

        gameBoard.Invalidate()
    End Sub

End Class
