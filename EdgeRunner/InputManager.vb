Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms

Namespace InputManager
        ''' <summary>
        ''' Provide methods to send keyboard input that also works in DirectX games.
        ''' </summary>
        Public Class Keyboard
            <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)> _
            Private Shared Function SendInput(ByVal cInputs As Integer, ByRef pInputs As INPUT, ByVal cbSize As Integer) As Integer
            End Function

            Private Structure INPUT
                Public dwType As UInteger
                Public mkhi As MOUSEKEYBDHARDWAREINPUT
            End Structure

            Private Structure KEYBDINPUT
                Public wVk As Short
                Public wScan As Short
                Public dwFlags As UInteger
                Public time As Integer
                Public dwExtraInfo As IntPtr
            End Structure

            Private Structure HARDWAREINPUT
                Public uMsg As Integer
                Public wParamL As Short
                Public wParamH As Short
            End Structure

            <StructLayout(LayoutKind.Explicit)> _
            Private Structure MOUSEKEYBDHARDWAREINPUT
                <FieldOffset(0)> _
                Public mi As MOUSEINPUT
                <FieldOffset(0)> _
                Public ki As KEYBDINPUT
                <FieldOffset(0)> _
                Public hi As HARDWAREINPUT
            End Structure

            Private Structure MOUSEINPUT
                Public dx As Integer
                Public dy As Integer
                Public mouseData As Integer
                Public dwFlags As UInteger
                Public time As Integer
                Public dwExtraInfo As IntPtr
            End Structure

            Const INPUT_MOUSE As UInt32 = 0
            Const INPUT_KEYBOARD As Integer = 1
            Const INPUT_HARDWARE As Integer = 2
            Const KEYEVENTF_EXTENDEDKEY As UInt32 = &H1
            Const KEYEVENTF_KEYUP As UInt32 = &H2
            Const KEYEVENTF_UNICODE As UInt32 = &H4
            Const KEYEVENTF_SCANCODE As UInt32 = &H8
            Const XBUTTON1 As UInt32 = &H1
            Const XBUTTON2 As UInt32 = &H2
            Const MOUSEEVENTF_MOVE As UInt32 = &H1
            Const MOUSEEVENTF_LEFTDOWN As UInt32 = &H2
            Const MOUSEEVENTF_LEFTUP As UInt32 = &H4
            Const MOUSEEVENTF_RIGHTDOWN As UInt32 = &H8
            Const MOUSEEVENTF_RIGHTUP As UInt32 = &H10
            Const MOUSEEVENTF_MIDDLEDOWN As UInt32 = &H20
            Const MOUSEEVENTF_MIDDLEUP As UInt32 = &H40
            Const MOUSEEVENTF_XDOWN As UInt32 = &H80
            Const MOUSEEVENTF_XUP As UInt32 = &H100
            Const MOUSEEVENTF_WHEEL As UInt32 = &H800
            Const MOUSEEVENTF_VIRTUALDESK As UInt32 = &H4000
            Const MOUSEEVENTF_ABSOLUTE As UInt32 = &H8000

            <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)> _
            Private Shared Function MapVirtualKey(ByVal uCode As UInt32, ByVal uMapType As MapVirtualKeyMapTypes) As UInt32
            End Function

            <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)> _
            Private Shared Function MapVirtualKeyEx(ByVal uCode As UInt32, ByVal uMapType As MapVirtualKeyMapTypes, ByVal dwhkl As IntPtr) As UInt32
            End Function

            <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)> _
            Private Shared Function GetKeyboardLayout(ByVal idThread As UInteger) As IntPtr
            End Function

            ''' <summary>The set of valid MapTypes used in MapVirtualKey
            ''' </summary>
            ''' <remarks></remarks>
            Public Enum MapVirtualKeyMapTypes As UInteger
                ''' <summary>uCode is a virtual-key code and is translated into a scan code.
                ''' If it is a virtual-key code that does not distinguish between left- and
                ''' right-hand keys, the left-hand scan code is returned.
                ''' If there is no translation, the function returns 0.
                ''' </summary>
                ''' <remarks></remarks>
                MAPVK_VK_TO_VSC = &H0

                ''' <summary>uCode is a scan code and is translated into a virtual-key code that
                ''' does not distinguish between left- and right-hand keys. If there is no
                ''' translation, the function returns 0.
                ''' </summary>
                ''' <remarks></remarks>
                MAPVK_VSC_TO_VK = &H1

                ''' <summary>uCode is a virtual-key code and is translated into an unshifted
                ''' character value in the low-order word of the return value. Dead keys (diacritics)
                ''' are indicated by setting the top bit of the return value. If there is no
                ''' translation, the function returns 0.
                ''' </summary>
                ''' <remarks></remarks>
                MAPVK_VK_TO_CHAR = &H2

                ''' <summary>Windows NT/2000/XP: uCode is a scan code and is translated into a
                ''' virtual-key code that distinguishes between left- and right-hand keys. If
                ''' there is no translation, the function returns 0.
                ''' </summary>
                ''' <remarks></remarks>
                MAPVK_VSC_TO_VK_EX = &H3

                ''' <summary>Not currently documented
                ''' </summary>
                ''' <remarks></remarks>
                MAPVK_VK_TO_VSC_EX = &H4
            End Enum
            '''Enum
            Private Shared Function GetScanKey(ByVal VKey As Keys) As ScanKey
                Dim ScanCode As UInteger = MapVirtualKey(CUInt(VKey), MapVirtualKeyMapTypes.MAPVK_VK_TO_VSC)
                Dim Extended As Boolean = (VKey = Keys.RMenu) OrElse (VKey = Keys.RControlKey) OrElse (VKey = Keys.Left) OrElse (VKey = Keys.Right) OrElse (VKey = Keys.Up) OrElse (VKey = Keys.Down) OrElse (VKey = Keys.Home) OrElse (VKey = Keys.Delete) OrElse (VKey = Keys.PageUp) OrElse (VKey = Keys.PageDown) OrElse (VKey = Keys.[End]) OrElse (VKey = Keys.Insert) OrElse (VKey = Keys.NumLock) OrElse (VKey = Keys.PrintScreen) OrElse (VKey = Keys.Divide)
                Return New ScanKey(ScanCode, Extended)
            End Function

            Private Structure ScanKey
                Public ScanCode As UInteger
                Public Extended As Boolean
                Public Sub New(ByVal sCode As UInteger, ByVal ex As [Boolean])
                    ' = false
                    ScanCode = sCode
                    Extended = ex
                End Sub
            End Structure

            ''' <summary>
            ''' Sends shortcut keys (key down and up) signals.
            ''' </summary>
            ''' <param name="kCode">The array of keys to send as a shortcut.</param>
            ''' <param name="Delay">The delay in milliseconds between the key down and up events.</param>
            ''' <remarks></remarks>
            Public Shared Sub ShortcutKeys(ByVal kCode As Keys(), ByVal Delay As Integer)
                '= 0
                Dim KeysPress As New KeyPressStruct(kCode, Delay)
                Dim t As New Thread(New ParameterizedThreadStart(AddressOf KeyPressThread))
                t.Start(KeysPress)
            End Sub

            ''' <summary>
            ''' Sends a key down signal.
            ''' </summary>
            ''' <param name="kCode">The virtual keycode to send.</param>
            ''' <remarks></remarks>
            Public Shared Sub KeyDown(ByVal kCode As Keys)
                Dim sKey As ScanKey = GetScanKey(kCode)
                Dim input As New INPUT()
                input.dwType = INPUT_KEYBOARD
                input.mkhi.ki = New KEYBDINPUT()
                input.mkhi.ki.wScan = CShort(sKey.ScanCode)
                input.mkhi.ki.dwExtraInfo = IntPtr.Zero
            input.mkhi.ki.dwFlags = KEYEVENTF_SCANCODE Or (If(sKey.Extended, KEYEVENTF_EXTENDEDKEY, CUInt(0)))
                Dim cbSize As Integer = Marshal.SizeOf(GetType(INPUT))
                SendInput(1, input, cbSize)
            End Sub

            ''' <summary>
            ''' Sends a key up signal.
            ''' </summary>
            ''' <param name="kCode">The virtual keycode to send.</param>
            ''' <remarks></remarks>
            Public Shared Sub KeyUp(ByVal kCode As Keys)
                Dim sKey As ScanKey = GetScanKey(kCode)
                Dim input As New INPUT()
                input.dwType = INPUT_KEYBOARD
                input.mkhi.ki = New KEYBDINPUT()
                input.mkhi.ki.wScan = CShort(sKey.ScanCode)
                input.mkhi.ki.dwExtraInfo = IntPtr.Zero
            input.mkhi.ki.dwFlags = KEYEVENTF_SCANCODE Or KEYEVENTF_KEYUP Or (If(sKey.Extended, KEYEVENTF_EXTENDEDKEY, CUInt(0)))
                Dim cbSize As Integer = Marshal.SizeOf(GetType(INPUT))
                SendInput(1, input, cbSize)
            End Sub

            ''' <summary>
            ''' Sends a key press signal (key down and up).
            ''' </summary>
            ''' <param name="kCode">The virtual keycode to send.</param>
            ''' <param name="Delay">The delay to set between the key down and up commands.</param>
            ''' <remarks></remarks>
            Public Shared Sub KeyPress(ByVal kCode As Keys, ByVal Delay As Integer)
                '= 0
                Dim SendKeys As Keys() = New Keys() {kCode}
                Dim KeysPress As New KeyPressStruct(SendKeys, Delay)
                Dim t As New Thread(New ParameterizedThreadStart(AddressOf KeyPressThread))
                t.Start(KeysPress)
            End Sub

            Private Shared Sub KeyPressThread(ByVal obj As Object)
                Dim KeysP As KeyPressStruct = CType(obj, KeyPressStruct)
                For Each k As Keys In KeysP.Keys
                    KeyDown(k)
                Next
                If KeysP.Delay > 0 Then
                    Thread.Sleep(KeysP.Delay)
                End If
                For Each k As Keys In KeysP.Keys
                    KeyUp(k)
                Next
            End Sub

            Private Structure KeyPressStruct
                Public Keys As Keys()
                Public Delay As Integer
                Public Sub New(ByVal KeysToPress As Keys(), ByVal DelayTime As Integer)
                    '= 0
                    Keys = KeysToPress
                    Delay = DelayTime
                End Sub
            End Structure
        End Class

        ''' <summary>
        ''' Provides methods to send keyboard input. The keys are being sent virtually and cannot be used with DirectX.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class VirtualKeyboard
            <DllImport("user32.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, EntryPoint:="keybd_event", ExactSpelling:=True, SetLastError:=True)> _
            Public Shared Function keybd_event(ByVal bVk As UInt32, ByVal bScan As UInt32, ByVal dwFlags As UInt32, ByVal dwExtraInfo As UInt32) As Boolean
            End Function

            Const KEYEVENTF_EXTENDEDKEY As UInt32 = &H1
            Const KEYEVENTF_KEYUP As UInt32 = &H2

            ''' <summary>
            ''' Sends shortcut keys (key down and up) signals.
            ''' </summary>
            ''' <param name="kCode">The array of keys to send as a shortcut.</param>
            ''' <param name="Delay">The delay in milliseconds between the key down and up events.</param>
            ''' <remarks></remarks>
            Public Shared Sub ShortcutKeys(ByVal kCode As Keys(), ByVal Delay As Integer)
                '= 0
                Dim KeyPress As New KeyPressStruct(kCode, Delay)
                Dim t As New Thread(New ParameterizedThreadStart(AddressOf KeyPressThread))
                t.Start(KeyPress)
            End Sub

            ''' <summary>
            ''' Sends a key down signal.
            ''' </summary>
            ''' <param name="kCode">The virtual keycode to send.</param>
            ''' <remarks></remarks>
            Public Shared Sub KeyDown(ByVal kCode As Keys)
                keybd_event(CType(kCode, UInt32), 0, 0, 0)
            End Sub

            ''' <summary>
            ''' Sends a key up signal.
            ''' </summary>
            ''' <param name="kCode">The virtual keycode to send.</param>
            ''' <remarks></remarks>
            Public Shared Sub KeyUp(ByVal kCode As Keys)
                keybd_event(CType(kCode, UInt32), 0, KEYEVENTF_KEYUP, 0)
            End Sub

            ''' <summary>
            ''' Sends a key press signal (key down and up).
            ''' </summary>
            ''' <param name="kCode">The virtual key code to send.</param>
            ''' <param name="Delay">The delay to set between the key down and up commands.</param>
            ''' <remarks></remarks>
            Public Shared Sub KeyPress(ByVal kCode As Keys, ByVal Delay As Integer)
                '= 0
                Dim SendKeys As Keys() = New Keys() {kCode}
                Dim KeyPress As New KeyPressStruct(SendKeys, Delay)
                Dim t As New Thread(New ParameterizedThreadStart(AddressOf KeyPressThread))
                t.Start(KeyPress)
            End Sub

            Public Shared Sub KeyPressThread(ByVal obj As Object)
                Dim KeysP As KeyPressStruct = CType(obj, KeyPressStruct)
                For Each k As Keys In KeysP.Keys
                    KeyDown(k)
                Next
                If KeysP.Delay > 0 Then
                    Thread.Sleep(KeysP.Delay)
                End If
                For Each k As Keys In KeysP.Keys
                    KeyUp(k)
                Next
            End Sub

            Private Structure KeyPressStruct
                Public Keys As Keys()
                Public Delay As Integer
                Public Sub New(ByVal KeysToPress As Keys(), ByVal DelayTime As Integer)
                    '= 0
                    Keys = KeysToPress
                    Delay = DelayTime
                End Sub
            End Structure
        End Class

        ''' <summary>
        ''' Provides methods to send mouse input that also works in DirectX games.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Mouse
            <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)> _
            Private Shared Function GetSystemMetrics(ByVal smIndex As Integer) As Integer
            End Function

            <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)> _
            Private Shared Function SendInput(ByVal cInputs As Integer, ByRef pInputs As INPUT, ByVal cbSize As Integer) As Integer
            End Function

            Private Const SM_SWAPBUTTON As Integer = 23

            Private Structure INPUT
                Public dwType As UInteger
                Public mkhi As MOUSEKEYBDHARDWAREINPUT
            End Structure

            Private Structure KEYBDINPUT
                Public wVk As Short
                Public wScan As Short
                Public dwFlags As Integer
                Public time As Integer
                Public dwExtraInfo As IntPtr
            End Structure

            Private Structure HARDWAREINPUT
                Public uMsg As Integer
                Public wParamL As Short
                Public wParamH As Short
            End Structure

            <StructLayout(LayoutKind.Explicit)> _
            Private Structure MOUSEKEYBDHARDWAREINPUT
                <FieldOffset(0)> _
                Public mi As MOUSEINPUT
                <FieldOffset(0)> _
                Public ki As KEYBDINPUT
                <FieldOffset(0)> _
                Public hi As HARDWAREINPUT
            End Structure

            Private Structure MOUSEINPUT
                Public dx As Integer
                Public dy As Integer
                Public mouseData As Integer
                Public dwFlags As UInteger
                Public time As Integer
                Public dwExtraInfo As IntPtr
            End Structure

            Const INPUT_MOUSE As UInt32 = 0
            Const INPUT_KEYBOARD As Integer = 1
            Const INPUT_HARDWARE As Integer = 2
            Const KEYEVENTF_EXTENDEDKEY As UInt32 = &H1
            Const KEYEVENTF_KEYUP As UInt32 = &H2
            Const KEYEVENTF_UNICODE As UInt32 = &H4
            Const KEYEVENTF_SCANCODE As UInt32 = &H8
            Const XBUTTON1 As UInt32 = &H1
            Const XBUTTON2 As UInt32 = &H2
            Const MOUSEEVENTF_MOVE As UInt32 = &H1
            Const MOUSEEVENTF_LEFTDOWN As UInt32 = &H2
            Const MOUSEEVENTF_LEFTUP As UInt32 = &H4
            Const MOUSEEVENTF_RIGHTDOWN As UInt32 = &H8
            Const MOUSEEVENTF_RIGHTUP As UInt32 = &H10
            Const MOUSEEVENTF_MIDDLEDOWN As UInt32 = &H20
            Const MOUSEEVENTF_MIDDLEUP As UInt32 = &H40
            Const MOUSEEVENTF_XDOWN As UInt32 = &H80
            Const MOUSEEVENTF_XUP As UInt32 = &H100
            Const MOUSEEVENTF_WHEEL As UInt32 = &H800
            Const MOUSEEVENTF_VIRTUALDESK As UInt32 = &H4000
            Const MOUSEEVENTF_ABSOLUTE As UInt32 = &H8000

            Public Enum MouseButtons
                LeftDown = &H2
                LeftUp = &H4
                RightDown = &H8
                RightUp = &H10
                MiddleDown = &H20
                MiddleUp = &H40
                Absolute = &H8000
                Wheel = &H800
            End Enum

            Public Enum MouseKeys
                Left = -1
                Right = -2
                Middle = -3
            End Enum

            Public Enum ScrollDirection
                Up = 120
                Down = -120
            End Enum

            ''' <summary>
            ''' Returns true if mouse buttons are swapped
            ''' </summary>
            ''' <value></value>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Shared ReadOnly Property IsLeftHanded() As Boolean
                Get
                    Try
                        Return (GetSystemMetrics(SM_SWAPBUTTON) = 1)
                    Catch generatedExceptionName As Exception
                        Return False
                    End Try
                End Get
            End Property

            ''' <summary>
            ''' Sends a mouse button signal. To send a scroll use the Scroll method.
            ''' </summary>
            ''' <param name="mButton">The button to send.</param>
            ''' <remarks></remarks>
            Public Shared Sub SendButton(ByVal mButton As MouseButtons)
                Dim input As New INPUT()
                input.dwType = INPUT_MOUSE
                input.mkhi.mi = New MOUSEINPUT()
                input.mkhi.mi.dwExtraInfo = IntPtr.Zero
                input.mkhi.mi.dwFlags = CUInt(mButton)
                input.mkhi.mi.dx = 0
                input.mkhi.mi.dy = 0
                Dim cbSize As Integer = Marshal.SizeOf(GetType(INPUT))
                SendInput(1, input, cbSize)
            End Sub

            ''' <summary>
            ''' Sends a mouse press signal (down and up).
            ''' </summary>
            ''' <param name="mKey">The key to press.</param>
            ''' <param name="Delay">The delay to set between the events.</param>
            ''' <remarks></remarks>
            Public Shared Sub PressButton(ByVal mKey As MouseKeys, ByVal Delay As Integer)
                '= 0
                ButtonDown(mKey)
                If Delay > 0 Then
                    System.Threading.Thread.Sleep(Delay)
                End If
                ButtonUp(mKey)
            End Sub

            ''' <summary>
            ''' Send a mouse button down signal.
            ''' </summary>
            ''' <param name="mKey">The mouse key to send as mouse button down.</param>
            ''' <remarks></remarks>
            Public Shared Sub ButtonDown(ByVal mKey As MouseKeys)
                Select Case mKey
                    Case MouseKeys.Left
                        SendButton(MouseButtons.LeftDown)
                        Return
                    Case MouseKeys.Right
                        SendButton(MouseButtons.RightDown)
                        Return
                    Case MouseKeys.Middle
                        SendButton(MouseButtons.MiddleDown)
                        Return
                End Select
            End Sub

            ''' <summary>
            ''' Send a mouse button up signal.
            ''' </summary>
            ''' <param name="mKey">The mouse key to send as mouse button up.</param>
            ''' <remarks></remarks>
            Public Shared Sub ButtonUp(ByVal mKey As MouseKeys)
                Select Case mKey
                    Case MouseKeys.Left
                        SendButton(MouseButtons.LeftUp)
                        Return
                    Case MouseKeys.Right
                        SendButton(MouseButtons.RightUp)
                        Return
                    Case MouseKeys.Middle
                        SendButton(MouseButtons.MiddleUp)
                        Return
                End Select
            End Sub

            ''' <summary>
            ''' Moves the mouse to a certain location on the screen.
            ''' </summary>
            ''' <param name="X">The x location to move the mouse.</param>
            ''' <param name="Y">The y location to move the mouse</param>
            ''' <remarks></remarks>
            Public Shared Sub Move(ByVal X As Integer, ByVal Y As Integer)
                Dim input As New INPUT()
                input.dwType = INPUT_MOUSE
                input.mkhi.mi = New MOUSEINPUT()
                input.mkhi.mi.dwExtraInfo = IntPtr.Zero
                input.mkhi.mi.dwFlags = MOUSEEVENTF_ABSOLUTE Or MOUSEEVENTF_MOVE
                input.mkhi.mi.dx = X * (65535 \ Screen.PrimaryScreen.Bounds.Width)
                input.mkhi.mi.dy = Y * (65535 \ Screen.PrimaryScreen.Bounds.Height)
                Dim cbSize As Integer = Marshal.SizeOf(GetType(INPUT))
                SendInput(1, input, cbSize)
            End Sub

            ''' <summary>
            ''' Moves the mouse to a location relative to the current one.
            ''' </summary>
            ''' <param name="X">The amount of pixels to move the mouse on the x axis.</param>
            ''' <param name="Y">The amount of pixels to move the mouse on the y axis.</param>
            ''' <remarks></remarks>
            Public Shared Sub MoveRelative(ByVal X As Integer, ByVal Y As Integer)
                Dim input As New INPUT()
                input.dwType = INPUT_MOUSE
                input.mkhi.mi = New MOUSEINPUT()
                input.mkhi.mi.dwExtraInfo = IntPtr.Zero
                input.mkhi.mi.dwFlags = MOUSEEVENTF_MOVE
                input.mkhi.mi.dx = X
                input.mkhi.mi.dy = Y
                Dim cbSize As Integer = Marshal.SizeOf(GetType(INPUT))
                SendInput(1, input, cbSize)
            End Sub

            ''' <summary>
            ''' Sends a scroll signal with a specific direction to scroll.
            ''' </summary>
            ''' <param name="Direction">The direction to scroll.</param>
            ''' <remarks></remarks>
            Public Shared Sub Scroll(ByVal Direction As ScrollDirection)
                Dim input As New INPUT()
                input.dwType = INPUT_MOUSE
                input.mkhi.mi = New MOUSEINPUT()
                input.mkhi.mi.dwExtraInfo = IntPtr.Zero
                input.mkhi.mi.dwFlags = CUInt(MouseButtons.Wheel)
                input.mkhi.mi.mouseData = CInt(Direction)
                input.mkhi.mi.dx = 0
                input.mkhi.mi.dy = 0
                Dim cbSize As Integer = Marshal.SizeOf(GetType(INPUT))
                SendInput(1, input, cbSize)
            End Sub
        End Class
    End Namespace