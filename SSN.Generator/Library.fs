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
        
    let private controlSum number =
        let mutable multiply = 2
        let mutable total = 0
        for symbol in number do
            let digit = Int32.Parse symbol
            total <- digit * multiply
            multiply <- if multiply = 1 then 2 else 1
        ()
        
    let rec private SumDigit number =
        let sum =
            if number >= 10
                then number % 10 + SumDigit number / 10
                else number
                
        if sum >= 10
            then SumDigit sum
            else number
        
    let private buildDigitArray personInformation : string =
        let birthDatePart = buildDatePart personInformation.BirthDate
        let randomNumberPart = randomNumberPart.ToString()
        let randomGenderPart = (randomGenderPart personInformation.Gender).ToString()
        
        // TODO: Add control sum logic
        birthDatePart + randomNumberPart + randomGenderPart
        
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