using System.Text;
using 工具基础核心库.BaseTool.Executer;
using 工具基础核心库.BaseTool.LogHlper;

namespace 工具基础核心库.AppCommand
{
    public abstract class BaseRunner
    {
        public static UTF8Encoding UTF8 = new(false);

        protected readonly ActionExecuter actionExecuter;

        protected BaseRunner()
        {
            this.actionExecuter = ActionExecuter.Init();
        }

        public void Run()
        {
            LogHelper.Log($"========任务开始:[{this.GetType().Name}]========");
            this.InternalRun();
            LogHelper.Log($"========任务完成:[{this.GetType().Name}]========");
        }

        protected abstract void InternalRun();
    }
}