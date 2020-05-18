Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

'Module Module1

'    Sub Main()

'        Dim myProcessReader As StreamReader = New StreamReader("C:\Users\sony\Documents\Visual Studio 2015\MyProjects\ReadJSONFile\ReadJSONFile\RoleBP.txt")   'open the file Values.txt
'        Dim line As String = ""
'        Dim position As Int16
'        Dim patternLeftStr As String
'        Dim patElementsStr As String
'        Dim trimedPattern As String
'        Dim trimedPatElementsStr As String
'        'Dim patElementsArray() As String

'        line = myProcessReader.ReadLine()
'        position = line.IndexOf(":")
'        patternLeftStr = Strings.Left(line, position)
'        trimedPattern = patternLeftStr.Trim("{").Trim("""")
'        patElementsStr = Strings.Right(line, Len(line) - position - 2).Trim("}")
'        trimedPatElementsStr = patElementsStr.Trim("[").Trim("]")

'        'This is not the right way to split a string as an array
'        Dim patElementsArray() As String = Split(trimedPatElementsStr, ", ")


'        Console.WriteLine(line)
'        Console.WriteLine(patternLeftStr)
'        Console.WriteLine(trimedPattern)
'        Console.WriteLine(patElementsStr)
'        Console.WriteLine(trimedPatElementsStr)

'        For index = 0 To patElementsArray.Length - 1
'            Console.WriteLine(patElementsArray(index))
'        Next

'        myProcessReader.Close()   'close the file RoleBP.txt

'        Console.ReadLine()

'    End Sub
'End Module


Module Module1

    Public Sub Main()

        'Dim myProcessReader As StreamReader = New StreamReader("C:\Users\sony\Documents\Visual Studio 2015\MyProjects\ReadJSONFile\ReadJSONFile\RoleBP.txt")
        'Dim line As String = myProcessReader.ReadLine()

        ''This pattern is able to identify a single word within ""
        ''Dim regExPattern As String = "\b\w+[-\s_]*\w*[-\s_]*\w*[-\s_]*\w*[-\s_]*\w*\b"

        'Dim regExPattern As String = "{'(?<PatternL1>\b\w+[-\s_]*\w*[-\s_]*\w*[-\s_]*\w*[-\s_]*\w*\b)':\s\[{'element_A':\s'(?<Ele1Task>\b\w+[-\s_]*\w*[-\s_]*\w*[-\s_]*\w*[-\s_]*\w*\b)'}\,\s{'element_B':\s'(?<Ele2Task>\b\w+[-\s_]*\w*[-\s_]*\w*[-\s_]*\w*[-\s_]*\w*\b)'}\]}"

        'Dim m As Match = Regex.Match(line, regExPattern)

        'If m.Success Then

        '    'Console.WriteLine("Lv1-Pattern: {0}, Element-1: {1}, Element-2: {2}", m.Groups("1"), m.Groups("2"), m.Groups("3"))
        '    Console.WriteLine(m.Value)
        '    Console.WriteLine(m.Groups("PatternL1"))
        '    Console.WriteLine(m.Groups("Ele1Task"))
        '    Console.WriteLine(m.Groups("Ele2Task"))

        'End If

        ''Console.WriteLine(line)
        ''Console.WriteLine(regExPattern)





        ''Dim regExPattern As String = "Strict"

        ''For Each m As Match In Regex.Matches(line, regExPattern)
        ''    Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index)
        ''Next



        'Dim myProcessReader As StreamReader = New StreamReader("C:\Users\sony\Documents\Visual Studio 2015\MyProjects\ReadJSONFile\ReadJSONFile\RoleBP.txt")
        'Dim line As String = myProcessReader.ReadLine()


        Dim s As String = File.ReadAllText("C:\Users\sony\Documents\Visual Studio 2015\MyProjects\ReadJSONFile\ReadJSONFile\json_sample.json")
        Dim p As JObject = JObject.Parse(s)
        Dim CtrlFlwPttn As New List(Of String)  ' Each item is a string, not a JToken

        Dim children As List(Of JToken) = p.Children().ToList ' p.Children(): transform JEnumerable's JTokens to Collection's Tokens  
        ' children(0) is a "JToken", not a "JProperty. 
        ' children(0).Name And children(0).Value Is invalid. 
        ' So children(0) should be transformed to a JProperty to call .Name And .Value
        Dim a As JProperty = children(0)

        Console.WriteLine("The Name is {0}", a.Name)  'Name is got!!!
        Console.WriteLine(a.Value)   ' a.Value is a JToken

        CtrlFlwPttn.Add(a.Name)

        Dim childTokens As List(Of JToken) = a.Value.ToList

        For Each childToken As JObject In childTokens
            Dim q As List(Of JToken) = childToken.Children().ToList
            Dim r As JProperty = q(0)
            CtrlFlwPttn.Add(r.Value)
        Next

        For Each listItem As String In CtrlFlwPttn
            Console.WriteLine(listItem)
        Next















        'Console.WriteLine(s)
        'Console.WriteLine(p.ToString())

        'For Each child As JProperty In children
        '    Console.WriteLine("The Name is {0}", child.Name)
        '    Console.WriteLine(child.Value)
        '    Console.WriteLine(children.IndexOf(child))
        'Next


        'Dim o1 As JArray = JArray.Parse(s)
        'Dim childArrayTokens As List(Of JToken) = parsedArray.Children().ToList

        'For Each childArrayToken As JToken In childArrayTokens
        '    Console.WriteLine(childArrayToken.ToString)
        'Next



        'Console.WriteLine(p.Name)
        'Console.WriteLine(p.Value)


        'Dim childArrayTokens As List(Of JToken) = o1.Children().ToList


        'For Each childArrayToken As JToken In o1.Children()
        '    Console.WriteLine(childArrayToken.ToString)
        'Next

        'myProcessReader.Close()
        Console.ReadLine()

    End Sub
End Module