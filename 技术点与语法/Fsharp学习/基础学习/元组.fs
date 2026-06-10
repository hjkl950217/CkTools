module Fsharp学习.基础学习.元组

open System

let showTuple=

    printfn "---演示---循环+元组"
    let sampleNumbers = [ 0 .. 10 ]
    let sampleTableOfSquares = [ for i in 0 .. 10 -> (i, (i*i).ToString()) ]

    // The next line prints a list that includes tuples, using '%A' for generic printing.
    printfn "The table of squares from 0 to 99 is:\n%A" sampleTableOfSquares
    Console.WriteLine();

    printfn "---演示---元组"
    let tuple1 = (1, 2, 3)//一个简单int元组
    let swapElems (a, b) = (b, a)//一个元组转换函数
    printfn "交换 （1, 20）的结果是 %A" (swapElems (1,20))

    let tuple2=(1,"张三",3.1415)//一个比较复杂的元组
    Printf.printfn "tuple1:%A tuple2:%A" tuple1 tuple2

    //struct元组
    let sampleStructTuple=struct(1,2)

    let convertFromStructTuple (struct(a,b))=(a,b)//把一个结构体元组转换成普通元组
    let convertToStructTuple (a,b)=struct(a,b)

    printfn "struct tuple:%A convert to general tuple:%A"
        sampleStructTuple
        (sampleStructTuple |>convertFromStructTuple)