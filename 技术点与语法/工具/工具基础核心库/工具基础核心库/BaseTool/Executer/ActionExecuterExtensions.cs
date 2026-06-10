using System.Runtime.CompilerServices;
using 工具基础核心库.BaseTool.LogHlper;

namespace 工具基础核心库.BaseTool.Executer
{
    /// <summary>
    /// <see cref="ActionExecuter"/>的扩展
    /// </summary>
    public static class ActionExecuterExtensions
    {
        #region Pipe 管道

        /// <summary>
        /// 管道-添加一个<see cref="Action"/>到管道中
        /// </summary>
        /// <param name="executer">执行器实例</param>
        /// <param name="action">要添加的委托</param>
        /// <param name="actionName">调用时的参数名</param>
        /// <returns></returns>
        public static ActionExecuter Pipe(
            this ActionExecuter executer,
            Action action,
            [CallerArgumentExpression(nameof(action))] string actionName = null)
        {
            actionName ??= string.Empty;
            actionName = actionName.Replace("this.", string.Empty);

            Action newAction = () =>
            {
                LogHelper.Log($"步骤开始:[{actionName}]");
                action();
                LogHelper.Log($"步骤结束:[{actionName}]");
            };

            executer.StepList.Add(newAction);
            return executer;
        }

        /// <summary>
        /// 管道-添加一个<see cref="Action"/>到管道中
        /// </summary>
        /// <param name="executer">执行器实例</param>
        /// <param name="action">要添加的委托</param>
        /// <param name="actionName">调用时的参数名</param>
        /// <returns></returns>
        public static ActionExecuter PipeTry(
            this ActionExecuter executer,
            Action action,
            [CallerArgumentExpression(nameof(action))] string actionName = null)
        {
            actionName ??= string.Empty;
            actionName = actionName.Replace("this.", string.Empty);

            Action newAction = () =>
            {
                LogHelper.Log($"步骤开始:[{actionName}]");
                try
                {
                    action();
                }
                catch(Exception e)
                {
                    LogHelper.LogError($"步骤异常:[{actionName}]", e);
                    throw;
                }

                LogHelper.Log($"步骤结束:[{actionName}]");
            };

            executer.StepList.Add(newAction);
            return executer;
        }

        /// <summary>
        /// 管道-有条件的添加一个<see cref="Action"/>到管道中
        /// </summary>
        /// <param name="executer">执行器实例</param>
        /// <param name="predicate">条件判断</param>
        /// <param name="action1">当条件为true时添加的委托</param>
        /// <param name="action2">当条件为false时添加的委托</param>
        /// <returns></returns>
        public static ActionExecuter PipeIf(
            this ActionExecuter executer,
            bool predicate,
            Action action1,
            Action action2)
        {
            if(predicate)
            {
                executer.StepList.Add(action1);
            }
            else
            {
                executer.StepList.Add(action2);
            }
            return executer;
        }

        /// <summary>
        /// 管道-有条件的添加一个<see cref="Action"/>到管道中
        /// </summary>
        /// <param name="executer">执行器实例</param>
        /// <param name="predicate">条件判断委托</param>
        /// <param name="action1">当条件为true时添加的委托</param>
        /// <param name="action2">当条件为false时添加的委托</param>
        /// <returns></returns>
        public static ActionExecuter PipeIf(
            this ActionExecuter executer,
            Func<bool> predicate, Action action1,
            Action action2)
        {
            executer.PipeIf(predicate(), action1, action2);
            return executer;
        }

        #endregion Pipe 管道

        #region MayEndPipe 可能结束的管道

        /// <summary>
        /// 可能结束的管道节点,true时执行后续节点,false时结束执行
        /// </summary>
        /// <param name="executer">执行器实例</param>
        /// <param name="predicate">是否结束判断委托</param>
        /// <param name="onEnded">结束后执行的委托</param>
        public static ActionExecuter MayEndPipe(
            this ActionExecuter executer,
            Func<bool> predicate,
            Action onEnded)
        {
            executer.StepList.Add(() =>
            {
                bool isContinue = predicate();

                if(!isContinue)
                {
                    executer.IsEnd = true;
                    onEnded();//为true时执行后续
                }
            });
            return executer;
        }

        /// <summary>
        /// 可能结束的管道节点,true时执行后续节点,false时结束执行
        /// </summary>
        /// <param name="executer">执行器实例</param>
        /// <param name="isEnd">是否结束</param>
        public static ActionExecuter MayEndPipe(this ActionExecuter executer, bool isEnd)
        {
            return executer.MayEndPipe(
                predicate: () => isEnd,
                onEnded: ActionExecuter.NullAction);
        }

        /// <summary>
        /// 可能结束的管道节点,true时执行后续节点,false时结束执行
        /// </summary>
        /// <param name="executer">执行器实例</param>
        /// <param name="predicate">是否结束判断委托</param>
        public static ActionExecuter MayEndPipe(
            this ActionExecuter executer,
            Func<bool> predicate)
        {
            return executer.MayEndPipe(
                predicate: predicate,
                onEnded: ActionExecuter.NullAction);
        }

        #endregion MayEndPipe 可能结束的管道

        #region 其它

        /// <summary>
        /// 指定管道忽略之前的状态，结束执行
        /// </summary>
        /// <param name="executer"></param>
        /// <returns></returns>
        public static ActionExecuter End(
            this ActionExecuter executer)
        {
            executer.IsEnd = true;
            return executer;
        }

        /// <summary>
        /// 指定管道忽略之前的状态，继续执行
        /// </summary>
        /// <param name="executer"></param>
        /// <returns></returns>
        public static ActionExecuter Continue(
            this ActionExecuter executer)
        {
            executer.IsEnd = false;
            return executer;
        }

        #endregion 其它
    }
}