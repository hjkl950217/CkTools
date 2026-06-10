module Fsharp学习.基础学习.类型

open Fsharp学习.基础学习.类型模块

let showType=

    printfn "---演示-类型---"

    //  实例化类型
    let contact1 =
        {
            Name = "Alf"
            Phone = "(206) 555-0157"
            Verified = false
        }

    //也可以写成一行
    let contact2= { Name="张三" ; Phone="123456789"; Verified=true  }

    //复制contact1的值，只有phone是自己写的，其它是复制
    let contactCopy=
        { contact1 with
            Phone="1234454458"
        }

    //可区分联合

    printfn "ass"