namespace 技术点验证.A44_Barrier同步基元的使用示例
{
    [VerifcationType(VerificationTypeEnum.A44_Barrier同步基元的使用示例)]
    public class A44_Barrier同步基元的使用示例 : IVerification
    {
        /*
         * 原文： https://www.freesion.com/article/874534225/
         * Barrier（屏障）是一种自定义的同步原语（synchronization primitive），它解决了多个线程（参与者）在多个阶段之间的并发和协调问题。
         * 1.多个参与者执行相同的几个阶段的操作
         * 2.在每一个阶段内，多个参与者并发执行
         * 3.一个屏障点代表一个阶段的结束
         * 4.一个参与者运行至屏障点时被阻塞，需要等待其他参与者都到达屏障点
         * 5.所有参与者都到达了屏障点，才可以进入下一个阶段
         *
         * 创建Barrier时，需要指定参与者数量。它允许动态的添加或者删除参与者。
         * Barrier适合多个线程执行多个阶段的操作。如果只有一个或两个阶段，可以考虑ContinueWhenAll。
         *
         * 缺点：
         * 1. barrier对象不知道具体的参与者有那些，只知道参与者数量
         * 2. 参与者不能知道在当前阶段自己是几个提交,因为提交后就会开始等待，只能获取当前未提交的数量(只能当预估，因为多线程环境下每时刻都有数据在变化)
         *
         */

        public void Start(string[]? args)
        {
            Barrier barrier = new Barrier(4, (b) =>
            {
                Console.WriteLine($"阶段 {b.CurrentPhaseNumber} 已完成");
                Console.WriteLine();
            });

            List<Task>? tasks = new List<Task>();
            for(int i = 0 ; i < 4 ; i++)
            {
                tasks.Add(Task.Factory.StartNew((index) =>
                {
                    int taskId = (int)index;

                    RunPhase(1, taskId);
                    barrier.SignalAndWait();

                    RunPhase(2, taskId);
                    barrier.SignalAndWait();

                    RunPhase(3, taskId);
                    barrier.SignalAndWait();

                    RunPhase(4, taskId);
                    barrier.SignalAndWait();
                }, i));
            }

            Task? finalTask = Task.Factory.ContinueWhenAll(tasks.ToArray(), (taskList) =>
            {
                Console.WriteLine("全部任务已经已完成");
                barrier.Dispose();
            });
            finalTask.Wait();

            Console.ReadLine();
        }

        private static void RunPhase(int phaseNo, int taskId)
        {
            Console.WriteLine($"阶段 {phaseNo}, 任务实例号: {taskId} 已完成");
        }
    }
}