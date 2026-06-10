module Fsharp学习.基础学习.序列

open System

let showSequence=

    //序列是元素的**逻辑序列**，所有元素都是相同的类型。
    //这些是比列表和数组更通用的类型，能够作为您“查看”任何逻辑系列元素的方式。
    //它们也很突出，因为它们可以是**惰性**的，这意味着只有在需要时才可以计算元素。

    printfn "---演示---序列"
    let seq1 = Seq.empty //空序列
    let seq2 = seq {  //包含值的序列
        yield "张三";
        yield "李四";  //yield 也可以不写。不写的时候需要所有的都不写
    }

    printfn "输出序列的值:%A" seq2

    let seq3 =seq {1..10}//定义1到10的数组
    let seq4=seq { for word in seq2 do
                    if word.Contains("张") then
                        yield word
    }//用for循环定义一个序列

    printfn "通过for循环定义的序列:%A" seq4

    let seq5 =Seq.init 11 (fun n->n+1)
    printfn "通过Seq.init定义的序列:%A" seq5

    let rnd=System.Random().NextDouble
    let rndFloat32:float32=float32 (rnd()) //类型强转

    let rec randomWalk n:seq<float32>=seq {
        yield n
        yield! randomWalk(n + (float32 (rnd())) - 0.5f)  //yield! 表示后面跟的是一个序列 不是单值
        }
    let rec randomWalk n=seq {
        yield n
        yield! randomWalk(n + rnd() - 0.5)
        }

    let firstValuesOfRandomWalk (count:int)=
        randomWalk 5.0
        |> Seq.truncate count
        |> Seq.toList

    let printfnCount (count:int)=  printfn "序列中前%d项:%A"  count (firstValuesOfRandomWalk(count))
    10|>printfnCount