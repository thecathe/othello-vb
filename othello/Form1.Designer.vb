<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.gameBoard = New System.Windows.Forms.PictureBox()
        Me.p1Label = New System.Windows.Forms.Label()
        Me.p1scoreLabel = New System.Windows.Forms.Label()
        Me.p2Label = New System.Windows.Forms.Label()
        Me.p2scoreLabel = New System.Windows.Forms.Label()
        Me.restartButton = New System.Windows.Forms.Button()
        Me.indicator = New System.Windows.Forms.PictureBox()
        CType(Me.gameBoard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indicator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gameBoard
        '
        Me.gameBoard.Location = New System.Drawing.Point(94, 58)
        Me.gameBoard.Name = "gameBoard"
        Me.gameBoard.Size = New System.Drawing.Size(100, 50)
        Me.gameBoard.TabIndex = 0
        Me.gameBoard.TabStop = False
        '
        'p1Label
        '
        Me.p1Label.AutoSize = True
        Me.p1Label.Location = New System.Drawing.Point(55, 148)
        Me.p1Label.Name = "p1Label"
        Me.p1Label.Size = New System.Drawing.Size(48, 13)
        Me.p1Label.TabIndex = 1
        Me.p1Label.Text = "Player 1:"
        '
        'p1scoreLabel
        '
        Me.p1scoreLabel.AutoSize = True
        Me.p1scoreLabel.Location = New System.Drawing.Point(119, 148)
        Me.p1scoreLabel.Name = "p1scoreLabel"
        Me.p1scoreLabel.Size = New System.Drawing.Size(13, 13)
        Me.p1scoreLabel.TabIndex = 2
        Me.p1scoreLabel.Text = "0"
        '
        'p2Label
        '
        Me.p2Label.AutoSize = True
        Me.p2Label.Location = New System.Drawing.Point(149, 148)
        Me.p2Label.Name = "p2Label"
        Me.p2Label.Size = New System.Drawing.Size(48, 13)
        Me.p2Label.TabIndex = 3
        Me.p2Label.Text = "Player 2:"
        '
        'p2scoreLabel
        '
        Me.p2scoreLabel.AutoSize = True
        Me.p2scoreLabel.Location = New System.Drawing.Point(203, 148)
        Me.p2scoreLabel.Name = "p2scoreLabel"
        Me.p2scoreLabel.Size = New System.Drawing.Size(13, 13)
        Me.p2scoreLabel.TabIndex = 4
        Me.p2scoreLabel.Text = "0"
        '
        'restartButton
        '
        Me.restartButton.Location = New System.Drawing.Point(126, 220)
        Me.restartButton.Name = "restartButton"
        Me.restartButton.Size = New System.Drawing.Size(75, 23)
        Me.restartButton.TabIndex = 5
        Me.restartButton.Text = "new game"
        Me.restartButton.UseVisualStyleBackColor = True
        '
        'indicator
        '
        Me.indicator.Location = New System.Drawing.Point(41, 164)
        Me.indicator.Name = "indicator"
        Me.indicator.Size = New System.Drawing.Size(100, 50)
        Me.indicator.TabIndex = 6
        Me.indicator.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.indicator)
        Me.Controls.Add(Me.p1Label)
        Me.Controls.Add(Me.p1scoreLabel)
        Me.Controls.Add(Me.p2scoreLabel)
        Me.Controls.Add(Me.restartButton)
        Me.Controls.Add(Me.p2Label)
        Me.Controls.Add(Me.gameBoard)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.gameBoard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indicator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gameBoard As PictureBox
    Friend WithEvents p1Label As Label
    Friend WithEvents p1scoreLabel As Label
    Friend WithEvents p2Label As Label
    Friend WithEvents p2scoreLabel As Label
    Friend WithEvents restartButton As Button
    Friend WithEvents indicator As PictureBox
End Class
