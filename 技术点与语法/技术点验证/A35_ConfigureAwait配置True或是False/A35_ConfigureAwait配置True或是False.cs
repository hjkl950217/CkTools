using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.ConfiguredTaskAwaitable;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A35_ConfigureAwait配置True或是False)]
    public class A35_ConfigureAwait配置True或是False : IVerification
    {
        public void Start(string[]? args)
        {
            System.Console.WriteLine("直接看代码即可");

            TaskAwaiter a = new Task(() => { }).GetAwaiter();
            ConfiguredTaskAwaiter b = new Task(() => { }).ConfigureAwait(false).GetAwaiter();

            TaskAwaiter a1 = new Task(() => { }).ContinueWith(t => { }).GetAwaiter();

            TaskScheduler taskScheduler = TaskScheduler.Current;

            TaskAwaiter b1 = new Task(() => { })
                .ContinueWith(t => { }, TaskScheduler.FromCurrentSynchronizationContext())
                .GetAwaiter();
        }

        //public Task Test(Task task, Task nextTask)
        //{
        //    SynchronizationContext? currentSyncContext = SynchronizationContext.Current;

        //    return task.ContinueWith(() =>
        //    {
        //        if (currentSyncContext == null)
        //        {
        //            return nextTask;
        //        }
        //        else
        //        {
        //            currentSyncContext.Post(() => nextTask, null);
        //        }
        //    }, TaskScheduler.Current);
        //}
    }
}