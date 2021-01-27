Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Media.Animation
Module module2
    Public inipath As String = Environment.GetEnvironmentVariable("LocalAppData") + "\1.27\Settings.ini"
    <DllImport("kernel32")>
    Public Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
    End Function
    <DllImport("kernel32")>
    Public Function WritePrivateProfileString(ByVal section As String, ByVal val As String, ByVal filePath As String) As Long
    End Function

    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (
        ByVal lpApplicationName As String,
        ByVal lpKeyName As String,
        ByVal lpDefault As String,
        ByVal lpReturnedString As String,
        ByVal nSize As Integer,
        ByVal lpFileName As String) As Integer
    Public Function GetKeyValue(ByVal sectionName As String,
                                 ByVal keyName As String,
                                ByVal defaultText As String,
                                 ByVal filename As String) As String
        Dim Rvalue As Integer
        Dim BufferSize As Integer
        BufferSize = 255
        Dim keyValue As String
        keyValue = Space(BufferSize)
        Rvalue = GetPrivateProfileString(sectionName, keyName, "", keyValue, BufferSize, filename)
        If Rvalue = 0 Then
            keyValue = defaultText
        Else
            keyValue = GetIniValue(keyValue)
        End If
        Return keyValue
    End Function
    Public Function GetIniValue(ByVal msg As String) As String
        Dim PosChr0 As Integer
        PosChr0 = msg.IndexOf(Chr(0))
        If PosChr0 <> -1 Then msg = msg.Substring(0, PosChr0)
        Return msg
    End Function
    Public Function SetKeyValue(ByVal Section As String, ByVal Key As String, ByVal Value As String, ByVal iniFilePath As String) As Boolean
        Dim pat = Path.GetDirectoryName(iniFilePath)

        If Directory.Exists(pat) = False Then
            Directory.CreateDirectory(pat)
        End If

        If File.Exists(iniFilePath) = False Then
            File.Create(iniFilePath).Close()
        End If

        Dim OpStation As Long = WritePrivateProfileString(Section, Key, Value, iniFilePath)

        If OpStation = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function getsnapshotname()
        Return "HandyDraw_" + Now.ToString.Replace(":", "_").Replace(" ", "_").Replace("/", "_") + "_" + Now.Millisecond.ToString + ".png"
    End Function
    Public Function xiegang(p As String)
        Return IIf(p.Substring(p.Length - 1) = "\", "", "\")
    End Function
    Public Structure save_task
        Public a As Integer
        Public w As Integer
        Public h As Integer
        Public path As String
    End Structure
    Public save_queue As List(Of save_task)
    Public Function FindScaleTransform(ByVal hayStack As Transform) As ScaleTransform
        If TypeOf hayStack Is ScaleTransform Then
            Return CType(hayStack, ScaleTransform)
        End If

        If TypeOf hayStack Is TransformGroup Then
            Dim group As TransformGroup = TryCast(hayStack, TransformGroup)

            For Each child In group.Children

                If TypeOf child Is ScaleTransform Then
                    Return CType(child, ScaleTransform)
                End If
            Next
        End If

        Return Nothing
    End Function
    Public Function FindRotateTransform(ByVal hayStack As Transform) As RotateTransform
        If TypeOf hayStack Is RotateTransform Then
            Return CType(hayStack, RotateTransform)
        End If

        If TypeOf hayStack Is TransformGroup Then
            Dim group As TransformGroup = TryCast(hayStack, TransformGroup)

            For Each child In group.Children

                If TypeOf child Is RotateTransform Then
                    Return CType(child, RotateTransform)
                End If
            Next
        End If
        Return Nothing
    End Function
    Public Function CubicBezierDoubleAnimation(d As TimeSpan, s As Double, t As Double, Bezier As String) As DoubleAnimationUsingKeyFrames
        Dim dkf As DoubleKeyFrame = New LinearDoubleKeyFrame
        dkf.KeyTime = TimeSpan.FromSeconds(0.0)
        dkf.Value = s
        Dim sp As SplineDoubleKeyFrame = New SplineDoubleKeyFrame
        sp.KeyTime = d
        Dim p() As String = Bezier.Split(",")
        Dim controlPoint1 As Point = New Point(p(0), p(1))
        Dim controlPoint2 As Point = New Point(p(2), p(3))
        sp.KeySpline = New KeySpline With {
            .ControlPoint1 = controlPoint1,
            .ControlPoint2 = controlPoint2
        }
        sp.Value = t
        Dim da As DoubleAnimationUsingKeyFrames = New DoubleAnimationUsingKeyFrames
        da.KeyFrames.Add(dkf)
        da.KeyFrames.Add(sp)
        Return da
    End Function
    Public Function CubicBezierDoubleAnimation(st As TimeSpan, d As TimeSpan, s As Double, t As Double, Bezier As String) As DoubleAnimationUsingKeyFrames
        Dim dkf1 As DoubleKeyFrame = New LinearDoubleKeyFrame
        dkf1.KeyTime = TimeSpan.FromSeconds(0.0)
        dkf1.Value = s
        Dim dkf As DoubleKeyFrame = New LinearDoubleKeyFrame
        dkf.KeyTime = st
        dkf.Value = s
        Dim sp As SplineDoubleKeyFrame = New SplineDoubleKeyFrame
        sp.KeyTime = d
        Dim p() As String = Bezier.Split(",")
        Dim controlPoint1 As Point = New Point(p(0), p(1))
        Dim controlPoint2 As Point = New Point(p(2), p(3))
        sp.KeySpline = New KeySpline With {
            .ControlPoint1 = controlPoint1,
            .ControlPoint2 = controlPoint2
        }
        sp.Value = t
        Dim da As DoubleAnimationUsingKeyFrames = New DoubleAnimationUsingKeyFrames
        da.KeyFrames.Add(dkf1)
        da.KeyFrames.Add(dkf)
        da.KeyFrames.Add(sp)
        Return da
    End Function
End Module
