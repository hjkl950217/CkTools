namespace 语法验证与学习
{
    public class Code2
    {
        public void Run()
        {
            Person[] persons = new Person[]
            {
                new Man(){Status=StatusEnum.CauseSuccess},
                new Woman(){Status=StatusEnum.CauseSuccess},
                new Man(){Status=StatusEnum.CauseFailure},
                new Woman(){Status=StatusEnum.CauseFailure},
                new Man(){Status=StatusEnum.InLove},
                new Woman(){Status=StatusEnum.InLove}
            };

            foreach(Person item in persons)
            {
                item.GetConclusion();
            }
        }
    }

    public abstract class Person
    {
        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum Status { get; set; }

        /// <summary>
        /// 得到结论或反应
        /// </summary>
        public abstract void GetConclusion();
    }

    public class Man : Person
    {
        public override void GetConclusion()
        {
            if(base.Status == StatusEnum.CauseSuccess)
            {
                Console.WriteLine("男人成功时,背后多半有一个伟大的女人");
            }
            else if(base.Status == StatusEnum.CauseFailure)
            {
                Console.WriteLine("男人失败时,闷头喝酒");
            }
            else if(base.Status == StatusEnum.InLove)
            {
                Console.WriteLine("男人恋爱时,凡事不懂也要装懂");
            }

            //以后不知道还有没有，所以这里是ealse if而不是else
        }
    }

    public class Woman : Person
    {
        public override void GetConclusion()
        {
            switch(base.Status)
            {
                case StatusEnum.CauseSuccess:
                    Console.WriteLine("女人成功时,背后大多有一个不成功的男人");
                    break;

                case StatusEnum.CauseFailure:
                    Console.WriteLine("女人失败时,眼泪汪汪");
                    break;

                case StatusEnum.InLove:
                    Console.WriteLine("女人恋爱时,遇事懂也装作不懂");
                    break;

                default:
                    break;
            }
        }
    }
}