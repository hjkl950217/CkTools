namespace 工具基础核心库.BaseTool.Executer
{
    /// <summary>
    /// 方法执行器
    /// </summary>
    public sealed class ActionExecuter
    {
        public List<Action> StepList { get; internal set; }
        public static Action NullAction = () => { };

        /// <summary>
        /// 是否结束管道,true时执行后续节点,false时结束执行
        /// </summary>
        public bool IsEnd { get; internal set; }

        /// <summary>
        /// 初始化<see cref="ActionExecuter"/>
        /// </summary>
        private ActionExecuter()
        {
            this.StepList = new List<Action>();
            this.IsEnd = false;
        }

        /// <summary>
        /// 初始化<see cref="ActionExecuter"/>
        /// </summary>
        public static ActionExecuter Init()
        {
            return new ActionExecuter();
        }

        /// <summary>
        /// 执行
        /// </summary>
        public void Execute()
        {
            //执行中间步骤
            foreach(Action action in this.StepList)
            {
                action();
                if(this.IsEnd == true)
                {
                    return;
                }
            }
        }
    }
}