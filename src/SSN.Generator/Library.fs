namespace SSN.Generator

open System
open Microsoft.FSharp.Core

type Gender =
    | Male = 0
    | Female = 1

type PersonInformation =
    { BirthDate: DateOnly
      Gender: Gender }

module SwedishGenerator =
    let private buildDatePart (birthDate: DateOnly) =
        birthDate.ToString("yyyyMMdd")
    let private randomNumberPart =
        Random().Next (10, 99)
        
    let private randomGenderPart (gender: Gender) =
        Random().Next 8 +
        match gender with
        | Gender.Male -> 1
        | Gender.Female -> 0
        | _ -> ArgumentOutOfRangeException() |> raise
        
    let private controlSum (number:string) =
        let mutable multiply = 2
        let mutable total = 0
        
        if number.Length <> 11 then raise (ArgumentException($"Number must be 9 digits long, but it was {number.Length} digits long."))
        
        let nineDigits = number.[2..]
        
        // Skip 2 first digits
        for symbol in nineDigits do
            let digit = symbol.ToString() |> Int32.Parse
            let multiplyResult = digit * multiply
            total <- total + multiplyResult / 10 + multiplyResult % 10
                
            multiply <- if multiply = 1 then 2 else 1
        
        let latDigitOfTotal =
            if total >= 10
            then total % 10
            else total
        
        let controlDigit = if latDigitOfTotal = 0 then 0 else 10 - latDigitOfTotal
        controlDigit
        
    let private buildDigitArray personInformation : string =
        let birthDatePart = buildDatePart personInformation.BirthDate
        let randomNumberPart = randomNumberPart.ToString()
        let randomGenderPart = (randomGenderPart personInformation.Gender).ToString()
        
        let ppid = $"{birthDatePart}{randomNumberPart}{randomGenderPart}"
        ppid + (controlSum ppid).ToString()
        
    let GenerateSSN personInformation : string =
        buildDigitArray personInformation
        
        
module DataGenerator =
    let private getFirstBirthDate age =
        -age-1
        |> DateTime.Today.AddYears
        |> DateOnly.FromDateTime
        
    let generateBirthDate age =
        Random().Next 364
        |> getFirstBirthDate(age).AddDays
        
    let randomGender =
        match Random().Next 1 with
        | 0 -> Gender.Male
        | 1 -> Gender.Female
        | _ -> ArgumentOutOfRangeException() |> raise
        
    let randomAge = Random().Next 120