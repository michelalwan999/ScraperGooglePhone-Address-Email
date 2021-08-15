Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim scr As New Google_Scraper
        scr.Scrape()

    End Sub
End Class
