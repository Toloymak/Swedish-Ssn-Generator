module SSN.Generator.Tests

open System
open NUnit.Framework
open SSN.Generator.SwedishGenerator

[<SetUp>]
let Setup () =
    ()

[<TestCase("2000-01-01", Gender.Male)>]
[<TestCase("2000-01-01", Gender.Female)>]
let ValidGeneratedNumber (birthDate : DateOnly, gender : Gender) =
    let ssn =
        {
        BirthDate = birthDate
        Gender = gender
        }
        |> GenerateSSN
    
    printfn $"Generated SSN: {ssn}"
    
    Assert.That(ssn.Length, Is.EqualTo(12))
    Assert.That(ssn.Substring(0, 8), Is.EqualTo(birthDate.ToString("yyyyMMdd")))