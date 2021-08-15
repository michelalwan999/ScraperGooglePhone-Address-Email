Public Class LinkedIn
    Public ID As String
    Public Name_First As String
    Public Name_Last As String
    Public Title As String
    Public Company As String
    Public Education As String
    Public City As String
    Public State As String
    Public Country As String
    Public URL As String
    Public Domain As String
    Public Phone_Number As String
    Public Address As String
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        ID = String.Empty
        Name_First = String.Empty
        Name_Last = String.Empty
        Title = String.Empty
        Company = String.Empty
        Education = String.Empty
        City = String.Empty
        State = String.Empty
        Country = String.Empty
        URL = String.Empty
        Domain = "unspecified"
        Phone_Number = "unspecified"
        Address = "unspecified"
    End Sub
    Public Overrides Function ToString() As String
        Return ID & vbTab & Name_First & vbTab & Name_Last & vbTab &
               Title & vbTab & Company & vbTab & Education & vbTab &
               City & vbTab & State & vbTab & Country & vbTab & URL &
               vbTab & Domain & vbTab & Address & vbTab & Phone_Number & vbNewLine
    End Function
End Class
