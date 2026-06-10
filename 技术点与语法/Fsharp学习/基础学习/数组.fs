module Fsharp学习.基础学习.数组

open System

let showArray=
    printfn "---演示---数组"

    //数组是相同类型元素的固定大小的可变集合。它们支持元素的快速随机访问，并且比F＃列表快，因为它们只是连续的内存块
    let array1=[| |] //空数组
    let array2=[|"张三";"李四"|]// string类型的数组
    let array3=[|1..5|]//构建一个1到5的数组

    //构建一个只包括'张'的数组
    let array4=
        [| for word in array2 do
            if word.Contains("张") then
                word
        |]
    let evenNumbers =Array.init 5 (fun x->x*x) //这是一个由索引初始化的数组，包含从5个偶数。从0开始
    printfn "Array.init初始化数组：%A" evenNumbers
    printfn "Array.init初始化数组的长度：%d" evenNumbers.Length

    let evenNumbersSlise=evenNumbers.[0..2] //从evenNumbers切片。获取索引0到2的数据
    printfn "数组切片：%A" evenNumbersSlise

    array2.[0]<-"张三1" //使用左箭头分配符修改数组中的值
    printfn "修改数组内容后的数组:%A" array2

    let sumOfLengthsOfWords (array:string[]) =
        array
        |> Array.filter (fun x -> x.StartsWith "张")
        |> Array.sumBy (fun x -> x.Length)

    printfn "利用sumBy求和平方后的numberList：%A" (sumOfLengthsOfWords(array2))