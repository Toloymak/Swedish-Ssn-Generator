module SSN.Generator.Tests

open System
open NUnit.Framework
open SSN.Generator.SwedishGenerator

[<SetUp>]
let Setup () =
    ()

[<TestCase("2000-01-01", Gender.Male)>]
[<TestCase("2000-01-01", Gender.Female)>]
let Test1 (birthDate : DateOnly, gender : Gender) =
    {
        BirthDate = birthDate
        Gender = gender
    }
    |> GenerateSSN
    |> printfn "Generated SSN: %A"
    