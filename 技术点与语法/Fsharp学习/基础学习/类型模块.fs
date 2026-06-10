module Fsharp学习.基础学习.类型模块

///纪录类型-联系卡
type ContactCard =
    {
        Name     : string
        Phone    : string
        Verified : bool
    }

//扑克牌的花色
type Suit =
     ///红桃
     | Hearts
     ///梅花
     | Clubs
     ///方块
     | Diamonds
     ///黑桃
     | Spades

//扑克牌的等级
type Rank =
       /// 代表 从 2 到 10
       | Value of int
       ///A
       | Ace
       ///K
       | King
       ///Q
       | Queen
       ///J
       | Jack