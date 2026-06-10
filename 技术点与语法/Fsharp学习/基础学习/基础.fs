module Fsharp学习.基础学习.基础

open System
open System.Linq;

///演示递归
let showBasic=

    let function1 x = x + 1
    let function2 x = x * 2
    let h = function1 >> function2

    printfn "---演示-函数组成"
    printfn "用>>符号"
    printfn "let function1 x = x + 1"
    printfn "let function2 x = x * 2"
    printfn "let h = function1 >> function2"

    let fun1 g a = g (g a) //f(g,a) = g(g(a))
    let fun2 g a = g(a |> g) //f(g,a) = g(g(a))

    printfn "---演示-高阶函数---"
    printfn "定义的函数为:f(g,a) = f(g(a))"
    printfn "普通写法:    let fun1 g a = g (g a)"
    printfn "管道式写法:  let fun2 g a = g a |> g   或   let fun2 g a = g(a |> g)"

    let add x y= x + y
    let add_T=fun x->fun y->x+y
    let one = 1
    let add1 = add 1

    printfn "---演示-柯里化---"
    printfn "准备的函数:          let add x y= x + y"
    printfn "也等价于:            let add_T=fun x->fun y->x+y"
    printfn "1的常量表达式:       let one =1"
    printfn ""
    printfn "柯里化：             let add1 = add 1  //返回 1+y的函数，通过这种方式让x记住了1 "
    printfn "等价于:              let add1 y = 1+y"

    let fun1 (x:int,y:int)= x+y
    let fun1=(fun (x:int,y:int) -> x*y)
    printfn "%d" (fun1(2,3))

    printfn "---演示-函数重新绑定---"
    printfn "原函数:           let fun1 (x:int,y:int)= x+y"
    printfn "新函数:           let fun1=(fun (x:int,y:int) -> x*y)"
    printfn "传递%d和%d运算结果:%d" 2 3 (fun1(2,3))

    let toUpperCase (s:string):string=s.ToUpper()
    let exclaim x=x+"!"
    let shout=toUpperCase>>exclaim

    printfn "---演示-组合-1--"
    printfn "函数1:           let toUpperCase (s:string):string=s.ToUpper()"
    printfn "函数2:           let exclaim x=x+\"!\""
    printfn "函数3:           let shout=toUpperCase>>exclaim"
    printfn "调用组合函数前： 'abc'"
    printfn "调用组合函数后： %s" (shout("abc"))
    printfn "也可以使用管道:  %s" ("abc"|>toUpperCase |>exclaim )

    let snakeCase (word:string)= word.ToLower().Replace("/\s+/ig","_")
    let toLowerCase (word:string)=word.ToLower()
    let replace (oldValue:string) (newValue:string) (source:string)=source.Replace(oldValue,newValue);
    let snakeCase2 = toLowerCase>>replace

    printfn "---演示-组合-1--"