module Fsharp学习.练习Demo.容器
//资料：https://llh911001.gitbooks.io/mostly-adequate-guide-chinese/content/ch8.html#%E2%80%9C%E7%BA%AF%E2%80%9D%E9%94%99%E8%AF%AF%E5%A4%84%E7%90%86

type MyContainer<'a when 'a :>System.Object  >(x:'a)=
    //let value=x

    //member this.Value
        //with get()=value
        //and set(value:'a)=this.Value<-value

    member val Value=x with get,set //自动属性

    ///静态方法-创建容器
    static member Create (x:'a):MyContainer<'a>=new MyContainer<'a>(x)

    ///
    member this.IsEmptyString():bool=this.Value.ToString().Equals("")
    member this.Map f =this.Value |>f |>MyContainer.Create
    member this.Map2 f=
        if this.IsEmptyString() then MyContainer.Create("")
        else this.Value |>f |>MyContainer.Create
    member this.MapToString() =this.Map (fun x->x.ToString())

///特百惠容器
let tupperwareContainer=

    let a=MyContainer.Create(5)
    let b=MyContainer.Create("abc")

    ///创建嵌套容器
    let containerNested=MyContainer.Create>>MyContainer.Create
    let c=containerNested "abc"

    let a1=a.Map(fun x->x+5)
    let b1=b.Map(fun x->"100").Map(fun t->System.Int32.Parse(t)+123)
    let c1=c.MapToString()
    //let c2=MyContainer.Map (fun x->x.ToString().Length) c

    printfn "tupperware-特百惠：特百惠是美国家居用品品牌，代表产品是塑料容器。"