Imports System.IO
Imports System.Text.RegularExpressions
'Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Linq

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs)

        Dim window1 As New Window1
        Dim window2 As New Window2
        Dim window3 As New Window3
        Dim window4 As New Window4
        Dim window5 As New Window5
        Dim window6 As New Window6
        Dim window7 As New Window7


        Dim theLIST As List(Of JToken) = Module1.GetDataResults()

        window1.label_AnnounceAJob.Content = theLIST(0)

        window2.label_DateTime.Content = theLIST(1)
        window2.label_Venue.Content = theLIST(2)

        window3.label_ReferenceLetter.Content = theLIST(3)
        window3.label_ReferenceEvaluation.Content = theLIST(4)

        window4.label_InterviewReport.Content = theLIST(5)

        window5.label_ReferenceEvalutionReport.Content = theLIST(6)
        window5.label_InterviewReport.Content = theLIST(7)
        window5.label_personalInfo.Content = theLIST(8)
        window5.label_Approve.Content = theLIST(9)

        window6.label_ApprovalLetter.Content = theLIST(10)

        window7.label_RejectionLetter.Content = theLIST(11)




        window1.Show()
        window2.Show()
        window3.Show()
        window4.Show()
        window5.Show()
        window6.Show()
        window7.Show()

    End Sub





End Class
