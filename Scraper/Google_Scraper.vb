Imports OpenQA.Selenium
Imports System.IO
Imports System.Threading
Imports OpenQA.Selenium.Chrome
Public Class Google_Scraper

    Private mDir As String
    Private mDriver As IWebDriver
    Private mdirRef As String
    Private mdirChrome As String
    Private mdirVPN As String
    Private mFile_In As String
    Private mFile_Out As String
    Private mFail As String
    Private mURL_Base As String
    Private mCompany_Country As List(Of LinkedIn)
    Private mIP As List(Of Integer)
    Private mIP_Index As Integer
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        mCompany_Country = New List(Of LinkedIn)
        mdirRef = "C:\Users\TOSHIBA\Desktop\Software and Files\Expert 23\Task1\Google_For_Selenium_78.0.3904.7000\Chrome\Application\"
        mdirChrome = mdirRef & "Chrome.exe"
        mdirVPN = mdirRef & "2.0.0_0.crx"
        mDir = "C:\Users\TOSHIBA\Desktop\Software and Files\Expert 23\Task1\Google_Scraper\Email Scraper\Dir\"
        mFile_In = mDir & "Optimal.txt"
        mFile_Out = mDir & "S1.txt"
        mFail = mDir & "S1fail.txt"
        mURL_Base = "https://www.google.com/search?hl=en&ei=pcE_YLAlw_bmAsLPoMgG&q=shop+eight+hundred+mart&oq=shop+eight+hundred+mart&gs_lcp=Cgdnd3Mtd2l6EAMyCAghEBYQHRAeMggIIRAWEB0QHjIICCEQFhAdEB46CAguEJECEJMCOgUILhCRAjoFCAAQsQM6CAguELEDEIMBOgsILhCxAxDHARCvAToICAAQsQMQgwE6CAgAELEDEJECOgUILhCxAzoFCAAQkQI6CAguEMcBEK8BOgIILjoHCAAQsQMQQzoECAAQQzoCCAA6DQguEMcBEKMCEAoQkwI6BAgAEAo6BggAEBYQHjoICAAQFhAKEB46BAgAEA06CAgAEA0QBRAeOggIABAIEA0QHjoLCAAQyQMQCBANEB46BQghEKABOgYIABANEB46BwghEAoQoAFQ3u8RWJmdEmCQnhJoAHAAeACAAd4CiAGZK5IBBjItMjEuMpgBAKABAaoBB2d3cy13aXrAAQE&sclient=gws-wiz&ved=0ahUKEwjwi6DEzZTvAhVDu1kKHcInCGkQ4dUDCA0&uact=5"
        mIP = New List(Of Integer) From {3, 4, 12, 9, 0, 7, 11}
    End Sub
    Public Sub Scrape()
        Read_Ref_File()
        Initialise_Driver()
        Utilities.Rotate_VPN(3, True, mDriver)
        Dim counter = 0
        For i = 0 To 1740
            Debug.Print(i)
            Build_URL(mCompany_Country(i).Company & " " & mCompany_Country(i).Country)
            counter = counter + 1
            If counter = 10 Then
                counter = 0
                Utilities.Rotate_VPN(mIP(mIP_Index), False, mDriver)
                mIP_Index = mIP_Index + 1
                If mIP_Index = 7 Then
                    mIP_Index = 0
                End If
            End If
            Try
                Go_To_URl(mURL_Base)
                Scrape_Page(mCompany_Country(i))
            Catch ex As Exception
                Debug.Print("fail")
                My.Computer.FileSystem.WriteAllText(mFail, mCompany_Country(i).ToString, True)
            End Try

            Write_Line(mCompany_Country(i))
        Next
    End Sub

    Private Sub Build_URL(ByVal company As String)
        mURL_Base = "https://www.google.com/search?hl=en&ei=pcE_YLAlw_bmAsLPoMgG&q=shop+eight+hundred+mart&oq=shop+eight+hundred+mart&gs_lcp=Cgdnd3Mtd2l6EAMyCAghEBYQHRAeMggIIRAWEB0QHjIICCEQFhAdEB46CAguEJECEJMCOgUILhCRAjoFCAAQsQM6CAguELEDEIMBOgsILhCxAxDHARCvAToICAAQsQMQgwE6CAgAELEDEJECOgUILhCxAzoFCAAQkQI6CAguEMcBEK8BOgIILjoHCAAQsQMQQzoECAAQQzoCCAA6DQguEMcBEKMCEAoQkwI6BAgAEAo6BggAEBYQHjoICAAQFhAKEB46BAgAEA06CAgAEA0QBRAeOggIABAIEA0QHjoLCAAQyQMQCBANEB46BQghEKABOgYIABANEB46BwghEAoQoAFQ3u8RWJmdEmCQnhJoAHAAeACAAd4CiAGZK5IBBjItMjEuMpgBAKABAaoBB2d3cy13aXrAAQE&sclient=gws-wiz&ved=0ahUKEwjwi6DEzZTvAhVDu1kKHcInCGkQ4dUDCA0&uact=5"
        company = company.Trim
        company = company.Replace("/", "%2F")
        company = company.Replace(" ", "+")
        mURL_Base = mURL_Base.Replace("shop+eight+hundred+mart", company)
    End Sub

    Private Sub Write_Line(ByVal dt As LinkedIn)

        My.Computer.FileSystem.WriteAllText(mFile_Out, dt.ToString, True)

    End Sub
    Private Sub Scrape_Page(ByRef dt As LinkedIn)

        Try
            Scrape_Domain(dt)
            Try
                Scrape_Phone_Address(dt)
            Catch ex As Exception
            End Try
        Catch ex As Exception
            Debug.Print("fail")
            My.Computer.FileSystem.WriteAllText(mFail, dt.ToString, True)
        End Try



    End Sub
    Private Sub Map_To_DTO(ByRef dt As LinkedIn)

    End Sub
    Private Sub Scrape_Domain(ByRef dt As LinkedIn)

        Dim company = dt.Company.ToLower
        company = company.Replace(" ", "")
        Dim cards = mDriver.FindElements(By.CssSelector(".tF2Cxc"))
        For i = 0 To cards.Count - 1
            Dim url = cards(i).FindElement(By.TagName("a")).GetAttribute("href")
            Dim tempurl = url
            Clean_URL(tempurl)
            If company.Contains(tempurl) Then dt.Domain = url : Exit For
            Dim h3 = cards(i).FindElement(By.TagName("a")).FindElement(By.TagName("h3")).Text.ToLower
            h3 = h3.Replace(" ", "")
            If h3.Contains(company) Then dt.Domain = cards(i).FindElement(By.TagName("a")).GetAttribute("href") : Exit For
        Next

        Debug.Print(dt.Domain)

    End Sub
    Private Sub Scrape_Phone_Address(ByRef dt As LinkedIn)

        Dim card = mDriver.FindElement(By.CssSelector(".SALvLe.farUxc.mJ2Mod"))
        Dim elements = card.FindElements(By.CssSelector(".zloOqf.PZPZlf"))
        For i = 0 To elements.Count - 1

            Dim a = elements(i).FindElement(By.TagName("span")).FindElement(By.TagName("a")).Text
            If a = "Address" Then dt.Address = elements(i).Text.Replace("Address", "")
            If a = "Phone" Then dt.Phone_Number = elements(i).Text.Replace("Phone", "")
        Next

    End Sub
    Private Sub Scrape_Phone(ByRef dt As LinkedIn)

    End Sub
    Private Sub Clean_URL(ByRef url As String)

        url = url.Replace("https://www.", "https://")
        url = url.Replace("https://", "https://www.")

        Dim first = url.IndexOf(".") + 1
        url = url.Substring(first)
        url = StrReverse(url)
        Dim Second = url.LastIndexOf(".") + 1
        url = url.Substring(Second)
        url = StrReverse(url)

    End Sub
    Private Sub Read_Ref_File()

        Dim file As String = My.Computer.FileSystem.ReadAllText(mFile_In)
        Dim lines As String() = file.Split(vbNewLine)

        For Each line In lines
            Clean_Line(line)
            Dim dt As New LinkedIn
            Dim parts As String() = line.Split(vbTab)
            With dt
                .ID = parts(0)
                .Name_First = parts(1)
                .Name_Last = parts(2)
                .Title = parts(3)
                .Company = parts(4)
                .Education = parts(5)
                .City = parts(6)
                .State = parts(7)
                .Country = parts(8)
                .URL = parts(9)
            End With
            mCompany_Country.Add(dt)
        Next
    End Sub


    Private Sub Clean_Line(ByRef line As String)
        line = line.Replace(vbLf, "")
        line = line.Replace(vbNewLine, "")
        line = line.Replace(vbCr, "")
        line = line.Replace(vbCrLf, "")
        line = line.Replace("""", "")
    End Sub

    Private Sub Initialise_Driver()
        Dim options As New ChromeOptions
        options.BinaryLocation = mdirChrome
        options.AddExtension(mdirVPN)
        mDriver = New ChromeDriver(options)
        mDriver.Manage.Timeouts.PageLoad = New TimeSpan(0, 0, 60)
    End Sub
    Private Sub Go_To_URl(ByVal url As String)
        mDriver.Navigate.GoToUrl(url)
    End Sub

End Class
