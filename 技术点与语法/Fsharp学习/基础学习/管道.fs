module Fsharp学习.基础学习.管道

open System

let showPipe=

    printfn "---演示-管道---"
    let isOdd x = x % 2 <> 0//判断是否是奇数
    let square x = x * x
    let addOne x = x + 1

    let numbers = [ 1; 2; 3; 4]

    //定义嵌套式函数
    let spuareOddValueAndAddOneNested values =
        let odds = List.filter isOdd values //过滤掉偶数
        let square = List.map square odds // 对剩余的偶数计算平方
        let result =List.map addOne square
        result

    printfn "原始数据:%A" numbers
    printfn "过滤偶数->平方->每个数加1:%A" (spuareOddValueAndAddOneNested(numbers))

    //下面是管道式
    //第一个是要调用List.filter 然后参数是isOdd这个函数
    //这样组合完后，得到一个新的函数
    let spuareOddValueAndAddOnePipeline values =
        values
        |> List.filter isOdd
        |> List.map square
        |> List.map addOne
    printfn "管道式 过滤偶数->平方->每个数加1:%A" (spuareOddValueAndAddOnePipeline(numbers))

    //下面是合并管道
    let spuareOddValueAndAddOneShortPipleLine values =
        values
        |> List.filter isOdd
        |> List.map (fun x ->  x |> square |> addOne)//给map传递的处理函数 是 合并管道后的处理函数

    printfn "合并管道式 过滤偶数->平方->每个数加1:%A" (spuareOddValueAndAddOneShortPipleLine(numbers))