Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Module Module1

    Public Sub Main()

        Dim importFile As String = File.ReadAllText("C:\Users\sony\Documents\Visual Studio 2015\MyProjects\ReadJSONFile\ReadJSONFile\json_sample.json")
        Dim patternDetails As Dictionary(Of String, JToken) = IdentifyTopLevelPattern_1(importFile)



        '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        For Each patternElement As KeyValuePair(Of String, JToken) In patternDetails
            If patternElement.Key IsNot "Pattern_Name" Then  ' Exlude a special Key-Value pair: Pattern_Name: Strict-order Sequential

                Dim trans_patternElement As List(Of JToken) = patternElement.Value.Children().ToList
                Dim real_patternElement As JProperty = trans_patternElement(0)
                ' Till now, available_patternElement.Name is eg. Task
                ' and available_patternElement.value can be a task or a ctrlFlwPttn. It cannot be an attribute.

                Console.WriteLine("{0}  &  {1}", real_patternElement.Name, real_patternElement.Value)

                If real_patternElement.Name = "task" Then
                    'Deal with real_patternElement.Value, that is a multi-property object

                    Dim valueObject As JObject = real_patternElement.Value
                    Console.WriteLine("Uer Role is:   {0}", valueObject("userRole"))
                    ' User Role of the task is got by valueObject("userRole")!!!!!!!!!


                Else
                    'Deal with real_patternElement.Value, that is a multi-element array

                End If

            End If
        Next
        '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


        For Each dicItem As KeyValuePair(Of String, JToken) In patternDetails
            Console.WriteLine(dicItem.Key & " - " & dicItem.Value.ToString)
        Next

        Console.ReadLine()

    End Sub






    Public Function IdentifyTopLevelPattern_1(ByVal jsonstring As String) As Dictionary(Of String, JToken)
        ' For patterns without branches, like Strict-order Sequetial
        ' !!!!! The input must be a JSON object! (embraced by {})

        Dim parsedJSON As JObject = JObject.Parse(jsonstring)
        Dim CtrlFlwPttn As New Dictionary(Of String, JToken)()

        Dim childrenTokens As List(Of JToken) = parsedJSON.Children().ToList ' p.Children(): transform JEnumerable's JTokens to Collection's Tokens  
        ' children(0) is a "JToken", not a "JProperty. 
        ' children(0).Name And children(0).Value Is invalid. 
        ' So children(0) should be transformed to a JProperty to call .Name And .Value
        Dim a As JProperty = childrenTokens(0)

        CtrlFlwPttn.Add("Pattern_Name", a.Name) 'Pattern Name is got!!!  a.Value is a JToken

        Dim childTokens As List(Of JToken) = a.Value.ToList

        For Each childToken As JObject In childTokens
            Dim a1 As List(Of JToken) = childToken.Children().ToList
            Dim a2 As JProperty = a1(0)
            CtrlFlwPttn.Add(a2.Name, a2.Value)
        Next

        Return CtrlFlwPttn

    End Function


    Public Function IdentifyTopLevelPattern_2(ByVal jsonstring As String) As Dictionary(Of String, JToken)
        ' For patterns without branches, like Parallel-A
        ' !!!!! The input must be a JSON object! (embraced by {})

        Dim CtrlFlwPttn As New Dictionary(Of String, JToken)()
        Dim parsedJSON As JObject = JObject.Parse(jsonstring)

        Dim childrenTokens As List(Of JToken) = parsedJSON.Children().ToList
        Dim a As JProperty = childrenTokens(0)
        CtrlFlwPttn.Add("Pattern_Name", a.Name) 'Pattern Name is got!!!  a.Value is a JToken

        Dim branchTokens As List(Of JToken) = a.Value.ToList ' Each branch is a JToken
        For Each branchToken As JObject In branchTokens

            Dim branch As List(Of JToken) = branchToken.Children().ToList
            Dim branch_Prop As JProperty = branch(0)
            ' branch_Prop.Name is got!!!
            Dim branchEleTokens As List(Of JToken) = branch_Prop.Value.ToList
            For Each branchEleToken As JObject In branchEleTokens

                Dim branchEle As List(Of JToken) = branchEleToken.Children().ToList
                Dim branchEle_Prop As JProperty = branchEle(0)
                ' branchEle_Prop.Name    branchEle_Prop.Value are got!!!

                Dim doubleValueKey As String = branch_Prop.Name & ":" & branchEle_Prop.Name
                CtrlFlwPttn.Add(doubleValueKey, branchEle_Prop.Value)

            Next
        Next

        Return CtrlFlwPttn

    End Function


End Module