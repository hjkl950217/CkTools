module Fsharp学习.基础学习.列表

open System

let showList=
     printfn "---演示---列表"

     //List是相同类型元素的有序，不可变的集合。它们是单链接列表，这意味着它们仅用于枚举，但是如果它们很大，则对于随机访问和串联来说是一个糟糕的选择。
     //这与其他流行语言中的列表不同，后者通常不使用单链接列表来表示列表

     let list1=[]
     let list2=[1;2;3]//用分号分割
     let list3=[
         1
         2
         3
     ]

     let numberList=[1..5] //定义1到1000的数组

     //List也可以通过计算生成。 这是包含一年中所有日期的列表。
     let format (date:DateTime) = date.ToString("yyyy-MM-dd")
     let dayList=
         [for month in 1 .. 12 do
             for day in 1 .. System.DateTime.DaysInMonth(2019,month) do
                 yield new System.DateTime(2019,month,day)|> format]
     printfn "通过循环生成的1年所有日期的集合:%A" dayList
     printfn "集合长度:%d" dayList.Length
     printfn "前5个元素%A" (dayList|> List.take 5)

     //计算可以包括条件。 这是一个包含元组的列表，元组是国际象棋棋盘上黑色正方形的坐标。
     let blackSquares =
         [ for i in 0 .. 7 do
               for j in 0 .. 7 do
                   if (i+j) % 2 = 1 then
                       yield (i, j) ]

     //利用map构建numberList平方后的集合
     let squaresOfNumberList=
         numberList
         |>List.map (fun x->x*x)

     printfn "利用map构建numberList平方后的集合：%A" squaresOfNumberList

     ///利用sumBy求和平方后的numberList
     let sumOfSquaresFromNumberList=
         numberList
         |> List.sumBy (fun x->x*x)

     printfn "利用sumBy求和平方后的numberList：%A" sumOfSquaresFromNumberList