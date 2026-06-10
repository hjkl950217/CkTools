 //Learn more about F# at http://fsharp.org
 //微软的文档  https://docs.microsoft.com/en-us/dotnet/fsharp/tour

module Fsharp学习.main

open System
open Fsharp学习.基础学习.字符串
open Fsharp学习.基础学习.元组
open Fsharp学习.基础学习.管道
open Fsharp学习.基础学习.列表
open Fsharp学习.基础学习.数组
open Fsharp学习.基础学习.序列
open Fsharp学习.基础学习.递归
open Fsharp学习.基础学习.基础

open Fsharp学习.练习Demo.容器

let sign num=
    if num>0 then "positive"
    elif num <0 then "negative"
    else "zero"

let function1 num =
    if num>0 then "positive"
    elif num <0 then "negative"
    else "zero"

[<EntryPoint>]
let main argv=

    //showBasic //演示基础部分相关

    //showString //演示字符串相关
    //showTuple //演示元组相关
    //showPipe //演示管道相关
    //showList //演示列表相关
    //showArray //演示数组相关
    //showSequence //演示序列相关
    //showRecursiveFunctions //演示递归相关

    tupperwareContainer //演示容器

    Console.ReadLine() |>ignore;

    0 // return an integer exit code