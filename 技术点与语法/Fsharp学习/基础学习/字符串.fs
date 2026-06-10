module Fsharp学习.基础学习.字符串
open System

let showString=

    printfn "---演示---字符串获取值"
    let helloWorld="helloWorld"
    // let substring = helloWorld.Substring(0,6).ToString()
    let substring=helloWorld.[0..6]

    printfn "%s" substring