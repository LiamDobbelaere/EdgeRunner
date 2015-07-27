Imports WiiMoteLib
Imports InTheHand.Net.Bluetooth
Imports InTheHand.Net.Sockets
Imports System.Speech.Synthesis

Public Class frmEdgeRunner
    Dim wiiMote As Wiimote
    Dim wiiBB As Wiimote
    Dim speaker As New SpeechSynthesizer
    Dim COG_X As Double
    Dim COG_XPREV As Double = -512
    Dim stick As Integer = 0
    Dim hasunpressed As Boolean = False
    Dim hasunpressed_space As Boolean = False
    Dim hasunpressed_shift As Boolean = False
    Dim hasunpressed_back As Boolean = False
    Dim hasunpressed_left As Boolean = False
    Dim hasunpressed_right As Boolean = False
    Dim hasunpressed_mouseright As Boolean = False
    Dim hasunpressed_wkey As Boolean = False
    Dim punchstick As Integer = 0
    Dim jumpTime As DateTime = DateTime.UtcNow

    Dim MotesManager As New WiimoteCollection

    Dim setCenterOffset As Boolean = False

    Dim naCorners As Single = 0.0F
    Dim oaTopLeft As Single = 0.0F
    Dim oaTopRight As Single = 0.0F
    Dim oaBottomLeft As Single = 0.0F
    Dim oaBottomRight As Single = 0.0F

    Sub Log(ByVal text As String, ByVal color As Color)
        speaker.SpeakAsyncCancelAll()
        rtbLog.SelectionColor = color
        rtbLog.AppendText(text + vbCrLf)
        rtbLog.Refresh()
        speaker.SpeakAsync(text.Replace("Wii", "Wee").Replace("Weemote", "Wiimote"))
    End Sub
    Private Sub btnWiiMote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWiiMote.Click
        Log("Attempting connection with Balance Board...", Color.Black)

        Dim wiiMotes As New WiimoteCollection
        Try
            Log("Attempting to pair Wii Balance Board, be sure to press the red sync button.", Color.Blue)
            Using btClient As New BluetoothClient

                Log("Removing existing Nintendo devices...", Color.Black)
                For Each btItem In btClient.DiscoverDevices(255, False, True, False)
                    If Not btItem.DeviceName.Contains("Nintendo") Then Continue For
                    BluetoothSecurity.RemoveDevice(btItem.DeviceAddress)
                    btItem.SetServiceState(BluetoothService.HumanInterfaceDevice, False)
                Next

                Dim btIgnored As Integer

                Log("Finding Wii devices...", Color.Black)
                For Each btDevice In btClient.DiscoverDevices(255, False, False, True)
                    If Not btDevice.DeviceName.Contains("Nintendo") Then
                        btIgnored += 1
                        Continue For
                    End If
                    Log("Found a device, adding..", Color.Green)
                    btDevice.SetServiceState(BluetoothService.HumanInterfaceDevice, True)
                Next

                Log("Waiting to connect, press OK to continue", Color.Blue)
                MsgBox("Press OK when Windows says the software is installed.")
                DoConnect()
            End Using
        Catch ex As Exception
            Log(ex.Message.ToString, Color.Red)
        End Try

    End Sub
    Sub DoConnect()
        Log("Connecting balance board...", Color.Black)
        Try
            'Dim wiiMotesBB As New WiimoteCollection
            'wiiMotesBB.FindAllWiimotes()
            MotesManager.FindAllWiimotes()
            For Each viiMote As Wiimote In MotesManager
                viiMote.Connect()
                viiMote.SetLEDs(True, False, False, False)
                If viiMote.WiimoteState.ExtensionType = ExtensionType.BalanceBoard Then
                    wiiBB = viiMote
                    Log("Balance board succesfully added.", Color.Black)
                    Log("Reported battery level: " + (viiMote.WiimoteState.BatteryRaw / 2).ToString, Color.Black)
                    pbxBalanceboard.BackColor = Color.Green
                Else
                    viiMote.SetLEDs(False, False, False, False)
                    viiMote.Disconnect()
                End If
            Next
        Catch ex As Exception
            Log(ex.Message.ToString, Color.Red)
        End Try

    End Sub

    Private Sub tmrUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUpdate.Tick
        Try
            pbrBB.Value = CInt(wiiBB.WiimoteState.BatteryRaw / 2)
            lblBBBP.Text = CInt(wiiBB.WiimoteState.BatteryRaw / 2).ToString + "%"

            COG_X = wiiBB.WiimoteState.BalanceBoardState.CenterOfGravity.X
            If COG_X >= -8 And COG_X <= 8 And stick = 0 Then
                Label1.Text = "Player is not running"
                Label1.ForeColor = Color.Red
                If hasunpressed = False Then
                    InputManager.Keyboard.KeyUp(Keys.Z)
                    hasunpressed = True
                End If
            Else
                Label1.Text = "Player is running"
                Label1.ForeColor = Color.Green
                InputManager.Keyboard.KeyDown(Keys.Z)
                stick += 1
                If stick = 8 Then
                    stick = 0
                End If
                hasunpressed = False
            End If

            InfoUpdate(wiiBB)
        Catch ex As Exception
        End Try

        Try
            pbrWM.Value = CInt(wiiMote.WiimoteState.BatteryRaw / 2)
            lblWMBP.Text = CInt(wiiMote.WiimoteState.BatteryRaw / 2).ToString + "%"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnectWiimote.Click
        Try
            Using btClient As New BluetoothClient
                Log("Finding Wii devices...", Color.Black)
                For Each btDevice In btClient.DiscoverDevices(255, False, False, True)
                    ' If Not btDevice.DeviceName.Contains("Nintendo") Then
                    '  Continue For
                    ' End If
                    Log("Found a device, adding..", Color.Green)
                    btDevice.SetServiceState(BluetoothService.HumanInterfaceDevice, True)
                Next

                Log("Waiting to connect, press OK to continue", Color.Blue)
                MsgBox("Press OK when Windows says the software is installed.")
            End Using
            Log("Connecting to WiiMote...", Color.Black)
            'Dim wiiMotes As New WiimoteCollection
            'wiiMotes.FindAllWiimotes()
            MotesManager.FindAllWiimotes()
            If MotesManager.Count = 0 Then
                Log("No WiiMote found, try reconnecting.", Color.Red)
                Exit Sub
            End If

            wiiMote = MotesManager(MotesManager.Count - 1)
            'If chkWiiFix.Checked = True Then
            'wiiMote = MotesManager(1)
            'Else
            'wiiMote = MotesManager(0)
            'End If

            wiiMote.Connect()
            Log("Paired WiiMote! Steps completed.", Color.Black)
            wiiMote.SetLEDs(True, False, True, False)
            rtbLog.Focus()
        Catch ex As Exception
            Log(ex.Message, Color.Red)
        End Try
    End Sub

    Sub InfoUpdate(ByVal wiiDevice As Wiimote)
        Dim rwWeight = wiiDevice.WiimoteState.BalanceBoardState.WeightKg
        Dim owWeight = If(rwWeight < 0.0F, 0.0F, rwWeight)
        Dim sendJump = False

        If owWeight < 1.0F Then
            If DateTime.UtcNow.Subtract(jumpTime).Seconds < 2 Then
                sendJump = True
            End If
        Else
            jumpTime = DateTime.UtcNow
        End If

        If sendJump Then
            InputManager.Keyboard.KeyDown(Keys.Space)
            hasunpressed_space = False
        Else
            If hasunpressed_space = False Then
                InputManager.Keyboard.KeyUp(Keys.Space)
                hasunpressed_space = True
            End If
        End If
    End Sub

    Private Sub tmrMouse_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrMouse.Tick
        Try
            Dim joyX, joyY As Integer
            joyX = CInt(wiiMote.WiimoteState.NunchukState.Joystick.X * 25)
            joyY = CInt(wiiMote.WiimoteState.NunchukState.Joystick.Y * 25)

            InputManager.Mouse.MoveRelative(joyX, -joyY)
        Catch ex As Exception
        End Try

        Try
            'S key
            If wiiMote.WiimoteState.ButtonState.Down Then
                InputManager.Keyboard.KeyDown(Keys.S)
                hasunpressed_back = False
            Else
                If hasunpressed_back = False Then
                    InputManager.Keyboard.KeyUp(Keys.S)
                    hasunpressed_back = True
                End If
            End If

            'Q key
            If wiiMote.WiimoteState.ButtonState.Left Then
                InputManager.Keyboard.KeyDown(Keys.Q)
                hasunpressed_left = False
            Else
                If hasunpressed_left = False Then
                    InputManager.Keyboard.KeyUp(Keys.Q)
                    hasunpressed_left = True
                End If
            End If

            'D key
            If wiiMote.WiimoteState.ButtonState.Right Then
                InputManager.Keyboard.KeyDown(Keys.D)
                hasunpressed_right = False
            Else
                If hasunpressed_right = False Then
                    InputManager.Keyboard.KeyUp(Keys.D)
                    hasunpressed_right = True
                End If
            End If

            'Right mouse key
            If wiiMote.WiimoteState.ButtonState.B Then
                InputManager.Mouse.PressButton(InputManager.Mouse.MouseKeys.Right, 1)
            End If

            'W key
            If wiiMote.WiimoteState.ButtonState.A Then
                InputManager.Keyboard.KeyDown(Keys.W)
                hasunpressed_wkey = False
            Else
                If hasunpressed_wkey = False Then
                    InputManager.Keyboard.KeyUp(Keys.W)
                    hasunpressed_wkey = True
                End If
            End If

            'Shift key
            If wiiMote.WiimoteState.NunchukState.Z Then
                InputManager.Keyboard.KeyDown(Keys.ShiftKey)
                hasunpressed_shift = False
            Else
                If hasunpressed_shift = False Then
                    InputManager.Keyboard.KeyUp(Keys.ShiftKey)
                    hasunpressed_shift = True
                End If
            End If

            'Punching
            If wiiMote.WiimoteState.NunchukState.AccelState.Values.Z <= -2 And punchstick <= 0 And chkNunchukPunch.Checked = True And wiiMote.WiimoteState.NunchukState.C Then
                InputManager.Mouse.PressButton(InputManager.Mouse.MouseKeys.Left, 1)
                punchstick = 30
            End If
            If punchstick > 0 Then
                punchstick -= 1
            End If
            Me.Text = punchstick
            Me.Refresh()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnWiiBBRecon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWiiBBRecon.Click
        DoConnect()
    End Sub

    Private Sub btnReconWiiM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReconWiiM.Click
        Dim wiiMotes As New WiimoteCollection
        wiiMotes.FindAllWiimotes()
        wiiMote = wiiMotes(0)
        wiiMote.Connect()
        Log("Paired WiiMote! Steps completed.", Color.Black)
        wiiMote.SetLEDs(True, False, True, False)
        rtbLog.Focus()
    End Sub

    Private Sub btnDiscall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiscall.Click
        Using btClient As New BluetoothClient
            Log("Removing Nintendo devices...", Color.Black)
            For Each btItem In btClient.DiscoverDevices(255, False, True, False)
                If Not btItem.DeviceName.Contains("Nintendo") Then Continue For
                BluetoothSecurity.RemoveDevice(btItem.DeviceAddress)
                btItem.SetServiceState(BluetoothService.HumanInterfaceDevice, False)
            Next
        End Using
        Log("Done..", Color.Black)
    End Sub
End Class
