Imports OpenQA.Selenium
Imports System.IO
Imports System.Threading
Imports OpenQA.Selenium.Chrome

Public Class Scraper

    Private mdirRef As String
    Private mdirChrome As String
    Private mdirVPN As String
    Private mDriver As IWebDriver
    Private mURL As String
    Public Sub New()
        Initialise()
    End Sub
    Private Sub Initialise()
        mdirRef = "C:\Users\tintwork\Desktop\MA\may\June 2020\July\August\September\October\November2020\DECEMBER\21\Scrapers\Google_For_Selenium_78.0.3904.7000\Chrome\Application\"
        mdirChrome = mdirRef & "Chrome.exe"
        mdirVPN = mdirRef & "2.0.0_0.crx"
        mURL = "https://www.google.com/search?q=what+is+my+ip+address&oq=what+is+my+ip+address&aqs=chrome.0.69i59l2j0l5j69i60.5760j0j7&sourceid=chrome&ie=UTF-8"
    End Sub
    Public Sub Scraper()
        Initialise_Driver()
        For i = 0 To 21
            Utilities_Old.Rotate_VPN(i, mDriver)
            Go_To_URl(mURL)
            Thread.Sleep(2000)
        Next
    End Sub
    Private Sub Go_To_URl(ByVal url As String)
        mDriver.Navigate.GoToUrl(url)
    End Sub
    Private Sub Initialise_Driver()
        Dim options As New ChromeOptions
        options.BinaryLocation = mdirChrome
        options.AddExtension(mdirVPN)
        mDriver = New ChromeDriver(options)
        mDriver.Manage.Timeouts.PageLoad = New TimeSpan(0, 0, 60)
        mDriver.Manage.Timeouts.ImplicitWait = New TimeSpan(0, 0, 10)
    End Sub
End Class




