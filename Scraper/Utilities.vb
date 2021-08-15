Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome


Public Class Utilities
    Private Function Binary_Search(ByVal country As String) As Integer

        Dim min As Integer = 1
        Dim mid As Integer
        Dim max As Integer = 110

        While min <= max

            mid = min + (max - min) / 2
            Dim url As String = country & mid.ToString
            'Go_To_URL(url)

            'If (mdriver.Url = url) Then min = mid + 1
            'If (mdriver.Url <> url) Then max = mid - 1

            If (max < min) Then Return max

        End While

        Return 0

    End Function

    Private Sub Shuffle_Links(ByVal inputDirectory As String, ByVal outputDirectory As String)

        Dim file As String = My.Computer.FileSystem.ReadAllText(inputDirectory)
        Dim murtToScrape As New List(Of String)
        Dim lines As String() = file.Split(vbCrLf)
        For i = 0 To lines.Count - 1

            lines(i) = lines(i).Replace(vbLf, "")

        Next

        Dim random As New List(Of Integer)
        random = Random_Generator(lines.Count - 1)


        Debug.Print("random   " & random.Count)
        Debug.Print("lines   " & lines.Count)
        My.Computer.FileSystem.WriteAllText(outputDirectory, lines(0) & vbNewLine, True)
        murtToScrape.Add(lines(0))
        For i = 0 To random.Count - 1
            murtToScrape.Add(random(i))
            If i = random.Count - 1 Then
                My.Computer.FileSystem.WriteAllText(outputDirectory, lines(random(i)), True)
            Else
                My.Computer.FileSystem.WriteAllText(outputDirectory, lines(random(i)) & vbNewLine, True)
            End If

        Next
    End Sub

    Protected Function Random_Generator(ByVal largestnumber As Integer) As List(Of Integer)
        Dim x, mix, temp As Integer
        Dim num As Integer = largestnumber    'This could be any number
        Dim randarray(num) As Integer
        Dim randnum As Random = New Random()

        'Assign sequential values to array
        For x = 1 To num
            randarray(x) = x
        Next x

        'Swap the array indexes randomly
        For x = num To 1 Step -1
            mix = randnum.Next(1, x)
            temp = randarray(mix)
            randarray(mix) = randarray(x)
            randarray(x) = temp
        Next x

        'Display all the rearranged array values in a listbox'
        Dim lstshuffle As New List(Of Integer)

        For x = 1 To num
            lstshuffle.Add(randarray(x))
        Next x

        Return lstshuffle

    End Function

    Private Sub From_File_To_List(ByVal file As String, ByRef ls As List(Of String))


        Dim raw As String = My.Computer.FileSystem.ReadAllText(file)
        Dim lines As String() = raw.Split(vbNewLine)

        For Each line In lines
            ls.Add(line)
        Next

    End Sub


    Public Shared Sub Rotate_VPN(ByVal countryIndex As Integer, ByVal login As Boolean, ByVal mDriver As ChromeDriver)
        Dim countryi As Integer = 0
        Dim countryj As Integer = 0
        Find_index_VPN(countryIndex, countryi, countryj)

        If login Then VPN_Login(mDriver) Else VPN_Home(mDriver)
        If (countryi > 8) Then VPN_Select_IP_Down(countryi, countryj, mDriver) Else VPN_Select_IP_Up(mDriver, countryIndex, countryi, countryj)
    End Sub

    Private Shared Sub ScrollDown_VPN(ByVal countries As IWebElement, ByVal count As Integer)
        ' For i = 0 To count
        'countries.SendKeys(Keys.ArrowDown)

        'Next
    End Sub
    Private Shared Sub Find_index_VPN(ByVal country As Integer, ByRef index As Integer, ByRef jindex As Integer)
        Dim countryref As New SortedDictionary(Of Integer, Integer) From {{0, 10},
                                             {1, 2}, {2, 1}, {3, 1}, {4, 1}, {5, 1},
                                              {6, 2}, {7, 1}, {8, 1}, {9, 1}, {10, 1}}
        Dim counter As Integer = 0
        For i = 0 To countryref.Count - 1
            For j = 1 To countryref(i)
                If (counter = country) Then
                    index = i
                    jindex = j
                    jindex = jindex - 1
                    Exit Sub
                End If
                counter = counter + 1
            Next
        Next

    End Sub

    Private Shared Sub VPN_Login(ByVal mDriver As IWebDriver)
        mDriver.Navigate.GoToUrl("chrome-extension://aojlhgbkmkahabcmcpifbolnoichfeep/login.html")




        Dim js2 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
        Dim subcats12 = mDriver.FindElements(By.CssSelector(".form-input"))


        Try

            subcats12(0).SendKeys("q11@usa.com")
        Catch ex As Exception
            Thread.Sleep(500)
            subcats12(0).SendKeys("q11@usa.com")
        End Try

        Dim js3 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
        Dim subcats13 = mDriver.FindElements(By.CssSelector(".form-input"))
        js3.ExecuteScript("arguments[0].setAttribute('value','Repostul8!')", subcats13(1))
        Thread.Sleep(500)


        Dim js4 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
        Dim subcats14 = mDriver.FindElement(By.CssSelector(".button.button-primary"))
        js4.ExecuteScript("arguments[0].click();", subcats14)
        Thread.Sleep(17000)

        Try
            Dim js5 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats15 = mDriver.FindElement(By.CssSelector(".location-toggle.open-location-option"))
            js5.ExecuteScript("arguments[0].click();", subcats15)
            Thread.Sleep(1000)
        Catch ex As Exception

            Thread.Sleep(5000)
            Dim js5 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats15 = mDriver.FindElement(By.CssSelector(".location-toggle.open-location-option"))
            js5.ExecuteScript("arguments[0].click();", subcats15)
            Thread.Sleep(5000)
        End Try




    End Sub
    Private Shared Sub VPN_Home(ByVal mDriver As IWebDriver)
        mDriver.Navigate.GoToUrl("chrome-extension://aojlhgbkmkahabcmcpifbolnoichfeep/home.html")
        Thread.Sleep(1000)


        Try
            Dim js5 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats13 = mDriver.FindElement(By.CssSelector(".location-toggle.open-location-option"))
            js5.ExecuteScript("arguments[0].click();", subcats13)
            Thread.Sleep(1000)
        Catch ex As Exception
            Thread.Sleep(5000)
            Dim js5 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats13 = mDriver.FindElement(By.CssSelector(".location-toggle.open-location-option"))
            js5.ExecuteScript("arguments[0].click();", subcats13)
            Thread.Sleep(5000)
        End Try



    End Sub

    Private Shared Sub VPN_Select_IP_Down(ByVal countryi As Integer,
                                 ByVal countryj As Integer,
                                 ByVal mDriver As IWebDriver)



        Try

            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))

            Dim js2 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats12 = Countries(0).FindElement(By.TagName("a"))
            js2.ExecuteScript("arguments[0].click();", subcats12)
            Thread.Sleep(1000)
        Catch ex As Exception
            Thread.Sleep(5000)
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))

            Dim js2 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats12 = Countries(0).FindElement(By.TagName("a"))
            js2.ExecuteScript("arguments[0].click();", subcats12)
            Thread.Sleep(4000)
        End Try


        Try
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim js As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats1 = Countries(0).FindElement(By.TagName("a"))
            js.ExecuteScript("arguments[0].click();", subcats1)
            ScrollDown_VPN(subcats1, 9)
            Thread.Sleep(1000)
            VPN_Select_IP(mDriver, countryi, countryj)
        Catch ex As Exception
            Thread.Sleep(5000)
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim js As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats1 = Countries(0).FindElement(By.TagName("a"))
            js.ExecuteScript("arguments[0].click();", subcats1)


            ScrollDown_VPN(subcats1, 9)
            Thread.Sleep(4000)
            VPN_Select_IP(mDriver, countryi, countryj)

        End Try


    End Sub
    Private Shared Sub VPN_Select_IP_Up(ByVal mDriver As IWebDriver,
                                  ByVal country As Integer,
                                  ByVal countryi As Integer,
                                  ByVal countryj As Integer)
        If country > 0 Then VPN_Scroll_Home(mDriver) Else VPN_Scroll_Login(mDriver, countryi)
        VPN_Select_IP(mDriver, countryi, countryj)

    End Sub

    Private Shared Sub VPN_Scroll_Home(ByVal mDriver As IWebDriver)


        Try
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim js As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats1 = Countries(0).FindElement(By.TagName("a"))
            js.ExecuteScript("arguments[0].click();", subcats1)
            Thread.Sleep(1000)
            Dim j2s As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
        Catch ex As Exception

            Thread.Sleep(5000)

            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim js As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats1 = Countries(0).FindElement(By.TagName("a"))
            js.ExecuteScript("arguments[0].click();", subcats1)
            Thread.Sleep(5000)
            Dim j2s As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)

        End Try



        Try
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim js4 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats2 = Countries(0).FindElement(By.TagName("a"))
            js4.ExecuteScript("arguments[0].click();", subcats2)
            Thread.Sleep(1000)
            ScrollDown_VPN(subcats2, 1)
        Catch ex As Exception

            Thread.Sleep(5000)
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim js4 As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats2 = Countries(0).FindElement(By.TagName("a"))
            js4.ExecuteScript("arguments[0].click();", subcats2)
            Thread.Sleep(5000)
            ScrollDown_VPN(subcats2, 1)
        End Try


    End Sub

    Private Shared Sub VPN_Scroll_Login(ByVal mDriver As IWebDriver, ByVal countryi As Integer)

        Try

            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim a = Countries(0).FindElement(By.TagName("a"))
            Dim js As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats = Countries(countryi).FindElement(By.TagName("a"))
            js.ExecuteScript("arguments[0].click();", subcats)

            Thread.Sleep(1000)
            ScrollDown_VPN(a, 1)
        Catch ex As Exception
            Thread.Sleep(5000)
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim a = Countries(0).FindElement(By.TagName("a"))
            Dim js As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats = Countries(countryi).FindElement(By.TagName("a"))
            js.ExecuteScript("arguments[0].click();", subcats)

            Thread.Sleep(4000)
            ScrollDown_VPN(a, 1)
        End Try

    End Sub

    Private Shared Sub VPN_Select_IP(ByVal mDriver As IWebDriver,
                              ByVal countryi As Integer,
                              ByVal countryj As Integer)

        Try
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim js As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats = Countries(countryi).FindElements(By.CssSelector(".location-info"))
            js.ExecuteScript("arguments[0].click();", subcats(countryj))
            Thread.Sleep(1000)
        Catch ex As Exception
            Thread.Sleep(4000)
            Dim Countries = mDriver.FindElements(By.CssSelector(".has-sub-menu"))
            Dim js As IJavaScriptExecutor = TryCast(mDriver, IJavaScriptExecutor)
            Dim subcats = Countries(countryi).FindElements(By.CssSelector(".location-info"))
            js.ExecuteScript("arguments[0].click();", subcats(countryj))
            Thread.Sleep(4000)
        End Try

    End Sub

End Class
