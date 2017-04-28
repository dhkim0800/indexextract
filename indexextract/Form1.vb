﻿Public Class Form1
    Dim ppp As String
    Dim dev As String
    Dim rel As String
    Dim dl, rl As String
    Function Upcheck(server As String)
        Try
            Dim c As New DataSet
            c.ReadXml(server)
            ppp = c.Tables(0).Rows(0).Item(0)
            dev = c.Tables(0).Rows(0).Item(1)
            rel = c.Tables(0).Rows(0).Item(2)
            dl = c.Tables(0).Rows(0).Item(3)
            rl = c.Tables(0).Rows(0).Item(4)
        Catch ex As Exception
            Return 1
        End Try
        Return 0
    End Function




    Dim ale As Integer = 0
    Public fullt As String
    Dim abcd As Integer
    Public i As Integer
    Public a As Integer ' = 3
    Public hash As String
    Public b As Integer
    Public file As String
    Dim cmp As Integer
    Dim ncp As Integer
    Public tng As Boolean = False
    Public korean As Boolean
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button5.Enabled = True
        Label7.Text = "0"
        Label8.Text = "0"
        Label10.Text = "0"
        If korean Then
            Label4.Text = "추출중"
        Else
            Label4.Text = "Extracting"
        End If
        i = 0
        a = 3
        cmp = 0
        ale = 0
        ncp = 0
        Label4.Visible = True
        Label3.Visible = True
        Button6.Enabled = True
        Button2.Enabled = False
        Button1.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False

        Timer1.Enabled = True
        tng = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '언어설정은 한국인만 받아요
        If Not System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag = "ko-KR" Then
            Button4.Visible = False
            Button3.Visible = False
        End If
        Dim a As Integer = Screen.PrimaryScreen.Bounds.Width / 2
        Dim b As Integer = Screen.PrimaryScreen.Bounds.Height / 2
        Me.Top = b - Me.Height / 2
        Me.Left = a - Me.Width / 2
        'MsgBox(Me.BackgroundImage)
        OpenFileDialog1.InitialDirectory = "c:\users\" & Split(My.User.Name, "\")(1) & "\appdata\roaming\.minecraft\assets\indexes"
        '설정파일 처음 세팅
        If Not My.Computer.FileSystem.FileExists("c:\indexextract\lang.set") Then
            Try
                My.Computer.FileSystem.WriteAllText("c:\indexextract\lang.set", "lang=EN_US", False)
            Catch ex As System.IO.DirectoryNotFoundException
                My.Computer.FileSystem.CreateDirectory("c:\indexextract")
                My.Computer.FileSystem.WriteAllText("c:\indexextract\lang.set", "lang=EN_US", False)
            End Try
        End If
        '설정파일 로드
        '언어 설정파일
        Dim se As String
        se = My.Computer.FileSystem.ReadAllText("c:\indexextract\lang.set")
        Dim lang As String = ""
        'MsgBox(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) 앱데이터 폴더
        Try
            lang = Split(se, "lang=")(1)
        Catch ex As System.IndexOutOfRangeException
            Button3_Click(sender, e)
        End Try
        If lang = "EN_US" Then
            Button3_Click(sender, e)
        ElseIf lang = "KO_KR" Then
            Button4_Click(sender, e)
        Else
            My.Computer.FileSystem.DeleteFile("c:\indexextract\lang.set")
            Application.Restart()
        End If


        Dim vvr As String
        Upcheck("https://raw.githubusercontent.com/dhkim0800/indexextract/master/uc.xml")
        If Not ppp = "indexextract" Then
            MsgBox("Update server error")
            End
        End If
        If Replace(Me.Text, "...", "") = Me.Text Then
            vvr = rel
        Else
            vvr = dev
        End If
        If Not vvr = Me.Text Then
            Label13.Visible = True
            Button7.Visible = True
        End If

        Me.MaximizeBox = False

        If korean Then
            Label4.Text = "대기"
        Else
            Label4.Text = "Ready"
        End If


    End Sub
    Public cancel As Boolean = False
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If korean Then
            OpenFileDialog1.Title = "파일 찾아보기"
        Else
            OpenFileDialog1.Title = "Browse File"
        End If


        Form2.ShowDialog()
        If cancel Then
            Exit Sub
        End If
        Button1.Enabled = True
        fullt = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
        b = UBound(Split(fullt, "hash")) - 1
        Form2.Label3.Text = ""
        Form2.ComboBox1.Text = ""
        Form2.ComboBox1.SelectedItem = ""
        If korean Then
            Form2.Label3.Text = "파일 선택 안함."
        Else
            Form2.Label3.Text = "No file."
        End If
        Dim o As Integer
        o = UBound(Split(OpenFileDialog1.FileName, "\"))
        If My.Computer.FileSystem.DirectoryExists("indexextract\" & Split(Split(OpenFileDialog1.FileName, "\")(o), ".json")(0)) Then
            Dim y
            If korean Then
                y = MsgBox("추출하려고 하는 내용이 이미 존재하는것 같습니다." & "게임 업데이트로 인해서 내용이 변경될 수도 있습니다." & "삭제하고 진행하시겠습니까? (권장)", vbYesNo, "IndexExtract")
            Else
                y = MsgBox("Some extract results are already exists." & "When Minecraft is updated, these files may change." & "Do you want to delete and extract it again? (Recommended)", vbYesNo, "IndexExtract")
            End If
            If y = vbYes Then
                My.Computer.FileSystem.DeleteDirectory("indexextract\" & Split(Split(OpenFileDialog1.FileName, "\")(o), ".json")(0), FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Label1.Text = "After finish:"
        Label12.Text = "Lang :  ⓘ"
        CheckBox2.Text = "beep"

        Label4.Text = "Change language"
        Label3.Visible = True

        TextBox1.Font = New Font("Segoe UI", 9)
        Label4.Font = New Font("Segoe UI", 14.25)
        Button5.Font = New Font("Segoe UI", 18)
        Button2.Text = "Browse"
        Button5.Text = "Open folder"
        Button1.Text = "Start"
        Label2.Text = "Warning   : "
        korean = False
        My.Computer.FileSystem.WriteAllText("c:\indexextract\lang.set", "lang=EN_US", False)
        '툴팁 설정
        infott.SetToolTip(Button2, "100% free!! Use it right now!")
        infott.SetToolTip(Label12, "Only supported in some countries.")
        '상태 표시 설정
        Label9.Text = "Critical
(No file)"
        Label6.Text = "Info
(already exists)"
        Label11.Text = "Prefect"

        Label3.Visible = False
        Label4.Text = "Ready"

        Label13.Text = "An update available."
        Button7.Text = "Update now"
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Label1.Text = "작업 완료후:"
        Label12.Text = "언어 :  ⓘ"
        CheckBox2.Text = "알림음 재생"

        Label4.Text = "언어 변경"
        Label3.Visible = True

        TextBox1.Font = New Font("맑은 고딕", 9)
        Label4.Font = New Font("맑은 고딕", 14.25)
        Button5.Font = New Font("맑은 고딕", 18)
        Button2.Text = "찾아보기"
        Button1.Text = "시작"

        Button5.Text = "폴더 열기"
        Label2.Text = "경고         :"
        korean = True
        My.Computer.FileSystem.WriteAllText("c:\indexextract\lang.set", "lang=KO_KR", False)
        '툴팁 설정
        infott.SetToolTip(Button2, "100% 무료입니다!! 지금 바로 추출해보세요!")
        infott.SetToolTip(Label12, "한국 윈도우 사용자에게만 지원되는 기능입니다.")
        '상태 표시 설정
        Label9.Text = "오류
(파일 없음)"
        Label6.Text = "정보
(이미 추출됨)"
        Label11.Text = "완벽함"

        Label3.Visible = False
        Label4.Text = "대기"

        Label13.Text = "업데이트가 존재합니다."
        Button7.Text = "업데이트하기"
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        abcd = UBound(Split(OpenFileDialog1.FileName, "\"))
        i = i + 1
        Try
            file = Split(fullt, Chr(34))(a)
            hash = Split(fullt, Chr(34))(a + 4)
            a = a + 8
        Catch ex As System.IndexOutOfRangeException
            GoTo b
        End Try
        If My.Computer.FileSystem.FileExists("indexextract\" & Split(Split(OpenFileDialog1.FileName, "\")(abcd), ".json")(0) & "\" & file) Then
            ale = ale + 1
            Label7.Text = ale
            GoTo aale
        End If
        Try
            My.Computer.FileSystem.CopyFile("c:\users\" & Split(My.User.Name, "\")(1) & "\appdata\roaming\.minecraft\assets\objects\" & Microsoft.VisualBasic.Left(hash, 2) & "\" & hash, "indexextract\" & Split(Split(OpenFileDialog1.FileName, "\")(abcd), ".json")(0) & "\" & file, True)
        Catch ex As System.IO.FileNotFoundException
            ncp = ncp + 1
            Label8.Text = ncp
            GoTo aale
        End Try
        Label10.Text = cmp
        cmp = cmp + 1
aale:
        If korean Then
            Label4.Text = "추출중" & vbCrLf & Int(i / b * 100) & "%"
        Else
            Label4.Text = "Extracting" & vbCrLf & Int(i / b * 100) & "%"
        End If
        Exit Sub
b:
        '작업 완료후 코드
        TextBox1.Visible = True
        '버튼 활성화
        Button2.Enabled = True
        Button1.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        TextBox1.Text = vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
        Label3.Visible = False
        Dim iii As Integer
        For iii = 0 To 5
            TextBox1.Text = TextBox1.Text & vbCrLf
        Next
        If korean Then
            If ale > 0 Then
                TextBox1.Text = "[경고]이미 존재하는 파일이 " & ale & "개 있어서 건너뛰었습니다." & vbCrLf & "두 파일간에 차이점이 있을 수 있으니, 기존 폴더를 삭제하고 다시 작업하는 것을 권장합니다.(폴더 열기를 사용하십시오.)"
            End If
            If ncp > 0 Then
                TextBox1.Text = TextBox1.Text & vbCrLf & "[경고]마인크래프트 폴더상에 찾을 수 없는 파일들이 " & ncp & "개 있어서 건너뛰었습니다." & vbCrLf & "해당 버전의 마인크래프트를 실행한 뒤 다시 작업하는 것을 권장합니다."
            End If
            If Not cmp + ncp + ale = cmp Then
                TextBox1.Text = TextBox1.Text & vbCrLf & "[안내]모든 파일이 완벽하게 추출되지 않았습니다. 위 내용을 참고하세요."
            End If
        Else
            If ale > 0 Then
                TextBox1.Text = "[WARNING]" & ale & " files was already exists and I skipped them." & vbCrLf & "They may have a different, and we recommend to remove old directory and run this tool again.(Use 'Open' button.)"
            End If
            If ncp > 0 Then
                TextBox1.Text = TextBox1.Text & vbCrLf & "[WARNING]" & ncp & " files does not exist in Minecraft directory. I skipped them." & vbCrLf & "It is recommend that you execute the corresponding version of the Minecraft and then run this tool again."
            End If
            If Not cmp + ncp + ale = cmp Then
                TextBox1.Text = TextBox1.Text & vbCrLf & "[INFO]Some files were not extracted fully. Please refer to the above."
            End If
        End If
        Dim wn As Integer = 0
        If ale > 0 Then
            If CheckBox2.Checked Then
                For i = 0 To 4
                    Console.Beep()
                Next
                Threading.Thread.Sleep(500)
            End If
            wn = wn + 1
        End If
        If ncp > 0 Then
            If CheckBox2.Checked Then
                For i = 0 To 6
                    Console.Beep()
                Next
                Threading.Thread.Sleep(500)
            End If
            wn = wn + 1
        End If
        Label5.Text = "( " & wn & " )"

        Button6.Enabled = False
        TextBox1.ScrollBars = ScrollBars.Vertical
        Timer1.Enabled = False
        Button1.Enabled = False
        If CheckBox2.Checked Then

            Dim v As Integer
            For v = 1 To 10
                Console.Beep()
            Next
        End If
        ale = 0
        cmp = 0
        ncp = 0
        If korean Then
            Label4.Text = "대기"
        Else
            Label4.Text = "Ready"
        End If

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        '폴더 열기
        Try
            Process.Start(Application.StartupPath & "\indexextract\" & Split(Split(OpenFileDialog1.FileName, "\")(abcd), ".json")(0))
        Catch ex As System.ComponentModel.Win32Exception
            Process.Start(Application.StartupPath)
        End Try
    End Sub
    Private Sub Dotdotdot_Tick(sender As Object, e As EventArgs) Handles Dotdotdot.Tick
        Select Case Label3.Text
            Case "......."
                Label3.Text = ""
            Case ""
                Label3.Text = "."
            Case "."
                Label3.Text = ".."
            Case ".."
                Label3.Text = "..."
            Case "..."
                Label3.Text = "...."
            Case "...."
                Label3.Text = "....."
            Case "....."
                Label3.Text = "......"
            Case "......"
                Label3.Text = "......."
        End Select
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Button6.Text = "┃┃" Then
            Button6.Text = "▶"
            Dotdotdot.Enabled = False
            Label3.Text = "......."
            Timer1.Enabled = False
        ElseIf Button6.Text = "▶" Then
            Button6.Text = "┃┃"
            Dotdotdot.Enabled = True
            Timer1.Enabled = True
        End If
    End Sub

    Public cntt As Integer = 0
    Public acntt As Boolean = False

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim vvr As String
        If Replace(Me.Text, "...", "") = Me.Text Then
            vvr = rel
        Else
            vvr = dev
        End If
        If vvr = dev Then
            Process.Start(dl)
        ElseIf vvr = rel Then
            Process.Start(rl)
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
        If acntt Then
            MsgBox("Language option already enabled for '" & System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag & "' User", vbInformation)
            Exit Sub
        End If
        If cntt = 10 Then
            MsgBox("Language option enabled for '" & System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag & "' User", vbCritical)
            Button3.Visible = True
            Button4.Visible = True
            acntt = True
        Else
            cntt = cntt + 1
        End If
    End Sub
End Class
