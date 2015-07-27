<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEdgeRunner
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEdgeRunner))
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.rtbLog = New System.Windows.Forms.RichTextBox
        Me.btnWiiMote = New System.Windows.Forms.Button
        Me.pbrBB = New System.Windows.Forms.ProgressBar
        Me.lblBBBP = New System.Windows.Forms.Label
        Me.pbrWM = New System.Windows.Forms.ProgressBar
        Me.lblWMBP = New System.Windows.Forms.Label
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.btnConnectWiimote = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.pbxWiimote = New System.Windows.Forms.PictureBox
        Me.pbxBalanceboard = New System.Windows.Forms.PictureBox
        Me.tmrMouse = New System.Windows.Forms.Timer(Me.components)
        Me.btnWiiBBRecon = New System.Windows.Forms.Button
        Me.btnReconWiiM = New System.Windows.Forms.Button
        Me.chkNunchukPunch = New System.Windows.Forms.CheckBox
        Me.btnDiscall = New System.Windows.Forms.Button
        Me.chkWiiFix = New System.Windows.Forms.CheckBox
        CType(Me.pbxWiimote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxBalanceboard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(556, 347)
        Me.Splitter1.TabIndex = 0
        Me.Splitter1.TabStop = False
        '
        'rtbLog
        '
        Me.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbLog.Location = New System.Drawing.Point(556, 0)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.ReadOnly = True
        Me.rtbLog.Size = New System.Drawing.Size(269, 347)
        Me.rtbLog.TabIndex = 1
        Me.rtbLog.Text = ""
        '
        'btnWiiMote
        '
        Me.btnWiiMote.Location = New System.Drawing.Point(12, 278)
        Me.btnWiiMote.Name = "btnWiiMote"
        Me.btnWiiMote.Size = New System.Drawing.Size(266, 29)
        Me.btnWiiMote.TabIndex = 2
        Me.btnWiiMote.Text = "Connect Balance board"
        Me.btnWiiMote.UseVisualStyleBackColor = True
        '
        'pbrBB
        '
        Me.pbrBB.Location = New System.Drawing.Point(12, 63)
        Me.pbrBB.Name = "pbrBB"
        Me.pbrBB.Size = New System.Drawing.Size(222, 23)
        Me.pbrBB.TabIndex = 5
        '
        'lblBBBP
        '
        Me.lblBBBP.AutoSize = True
        Me.lblBBBP.Location = New System.Drawing.Point(241, 69)
        Me.lblBBBP.Name = "lblBBBP"
        Me.lblBBBP.Size = New System.Drawing.Size(18, 13)
        Me.lblBBBP.TabIndex = 6
        Me.lblBBBP.Text = "-%"
        '
        'pbrWM
        '
        Me.pbrWM.Location = New System.Drawing.Point(284, 63)
        Me.pbrWM.Name = "pbrWM"
        Me.pbrWM.Size = New System.Drawing.Size(222, 23)
        Me.pbrWM.TabIndex = 7
        '
        'lblWMBP
        '
        Me.lblWMBP.AutoSize = True
        Me.lblWMBP.Location = New System.Drawing.Point(512, 69)
        Me.lblWMBP.Name = "lblWMBP"
        Me.lblWMBP.Size = New System.Drawing.Size(18, 13)
        Me.lblWMBP.TabIndex = 8
        Me.lblWMBP.Text = "-%"
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Enabled = True
        '
        'btnConnectWiimote
        '
        Me.btnConnectWiimote.Location = New System.Drawing.Point(284, 278)
        Me.btnConnectWiimote.Name = "btnConnectWiimote"
        Me.btnConnectWiimote.Size = New System.Drawing.Size(257, 29)
        Me.btnConnectWiimote.TabIndex = 9
        Me.btnConnectWiimote.Text = "Connect Wiimote"
        Me.btnConnectWiimote.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "IDLE"
        '
        'pbxWiimote
        '
        Me.pbxWiimote.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pbxWiimote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbxWiimote.Image = Global.EdgeRunner.My.Resources.Resources.wiimote
        Me.pbxWiimote.Location = New System.Drawing.Point(284, 92)
        Me.pbxWiimote.Name = "pbxWiimote"
        Me.pbxWiimote.Size = New System.Drawing.Size(257, 180)
        Me.pbxWiimote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbxWiimote.TabIndex = 4
        Me.pbxWiimote.TabStop = False
        '
        'pbxBalanceboard
        '
        Me.pbxBalanceboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pbxBalanceboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbxBalanceboard.Image = Global.EdgeRunner.My.Resources.Resources.Wii_Balance_Board_transparent
        Me.pbxBalanceboard.Location = New System.Drawing.Point(12, 92)
        Me.pbxBalanceboard.Name = "pbxBalanceboard"
        Me.pbxBalanceboard.Size = New System.Drawing.Size(266, 180)
        Me.pbxBalanceboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbxBalanceboard.TabIndex = 3
        Me.pbxBalanceboard.TabStop = False
        '
        'tmrMouse
        '
        Me.tmrMouse.Enabled = True
        Me.tmrMouse.Interval = 1
        '
        'btnWiiBBRecon
        '
        Me.btnWiiBBRecon.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnWiiBBRecon.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWiiBBRecon.Location = New System.Drawing.Point(12, 313)
        Me.btnWiiBBRecon.Name = "btnWiiBBRecon"
        Me.btnWiiBBRecon.Size = New System.Drawing.Size(266, 29)
        Me.btnWiiBBRecon.TabIndex = 11
        Me.btnWiiBBRecon.Text = "Reconnect"
        Me.btnWiiBBRecon.UseVisualStyleBackColor = False
        '
        'btnReconWiiM
        '
        Me.btnReconWiiM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnReconWiiM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReconWiiM.Location = New System.Drawing.Point(284, 313)
        Me.btnReconWiiM.Name = "btnReconWiiM"
        Me.btnReconWiiM.Size = New System.Drawing.Size(257, 29)
        Me.btnReconWiiM.TabIndex = 12
        Me.btnReconWiiM.Text = "Reconnect"
        Me.btnReconWiiM.UseVisualStyleBackColor = False
        '
        'chkNunchukPunch
        '
        Me.chkNunchukPunch.AutoSize = True
        Me.chkNunchukPunch.Location = New System.Drawing.Point(284, 40)
        Me.chkNunchukPunch.Name = "chkNunchukPunch"
        Me.chkNunchukPunch.Size = New System.Drawing.Size(154, 17)
        Me.chkNunchukPunch.TabIndex = 13
        Me.chkNunchukPunch.Text = "Nunchuk punch = left click"
        Me.chkNunchukPunch.UseVisualStyleBackColor = True
        '
        'btnDiscall
        '
        Me.btnDiscall.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDiscall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDiscall.Location = New System.Drawing.Point(12, 12)
        Me.btnDiscall.Name = "btnDiscall"
        Me.btnDiscall.Size = New System.Drawing.Size(266, 29)
        Me.btnDiscall.TabIndex = 14
        Me.btnDiscall.Text = "Disconnect all"
        Me.btnDiscall.UseVisualStyleBackColor = False
        '
        'chkWiiFix
        '
        Me.chkWiiFix.AutoSize = True
        Me.chkWiiFix.Location = New System.Drawing.Point(284, 19)
        Me.chkWiiFix.Name = "chkWiiFix"
        Me.chkWiiFix.Size = New System.Drawing.Size(178, 17)
        Me.chkWiiFix.TabIndex = 15
        Me.chkWiiFix.Text = "Potential WiiMote connection fix"
        Me.chkWiiFix.UseVisualStyleBackColor = True
        '
        'frmEdgeRunner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 347)
        Me.Controls.Add(Me.chkWiiFix)
        Me.Controls.Add(Me.btnDiscall)
        Me.Controls.Add(Me.chkNunchukPunch)
        Me.Controls.Add(Me.btnReconWiiM)
        Me.Controls.Add(Me.btnWiiBBRecon)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnConnectWiimote)
        Me.Controls.Add(Me.lblWMBP)
        Me.Controls.Add(Me.pbrWM)
        Me.Controls.Add(Me.lblBBBP)
        Me.Controls.Add(Me.pbrBB)
        Me.Controls.Add(Me.pbxWiimote)
        Me.Controls.Add(Me.pbxBalanceboard)
        Me.Controls.Add(Me.btnWiiMote)
        Me.Controls.Add(Me.rtbLog)
        Me.Controls.Add(Me.Splitter1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEdgeRunner"
        Me.Text = "WiiRun - 0.7.1 alpha"
        CType(Me.pbxWiimote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxBalanceboard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents rtbLog As System.Windows.Forms.RichTextBox
    Friend WithEvents btnWiiMote As System.Windows.Forms.Button
    Friend WithEvents pbxBalanceboard As System.Windows.Forms.PictureBox
    Friend WithEvents pbxWiimote As System.Windows.Forms.PictureBox
    Friend WithEvents pbrBB As System.Windows.Forms.ProgressBar
    Friend WithEvents lblBBBP As System.Windows.Forms.Label
    Friend WithEvents pbrWM As System.Windows.Forms.ProgressBar
    Friend WithEvents lblWMBP As System.Windows.Forms.Label
    Friend WithEvents tmrUpdate As System.Windows.Forms.Timer
    Friend WithEvents btnConnectWiimote As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmrMouse As System.Windows.Forms.Timer
    Friend WithEvents btnWiiBBRecon As System.Windows.Forms.Button
    Friend WithEvents btnReconWiiM As System.Windows.Forms.Button
    Friend WithEvents chkNunchukPunch As System.Windows.Forms.CheckBox
    Friend WithEvents btnDiscall As System.Windows.Forms.Button
    Friend WithEvents chkWiiFix As System.Windows.Forms.CheckBox

End Class
