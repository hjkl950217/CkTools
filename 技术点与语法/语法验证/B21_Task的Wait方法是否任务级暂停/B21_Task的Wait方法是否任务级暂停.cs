namespace 语法验证.B21_Task的Wait方法;

[VerifcationType(VerificationTypeEnum.B21_Task的Wait方法是否任务级暂停)]
public class B21_Task的Wait方法是否任务级暂停 : IVerification
{
    public static int MaxCount = 0;
    public static List<string> MsgList = new List<string>();

    public void Start(string[]? args)
    {
        LinkAction
          .Start()
          .Add(this.TaskTest)
          .BatchRun();
    }

    public void TaskTest()
    {
        TaskJob[] taskJobs = new TaskJob[]
        {
                new TaskJob(1),
                new TaskJob(2),
                new TaskJob(3),
                new TaskJob(4),
        };

        foreach(TaskJob job in taskJobs)
        {
            Task.Factory
                .StartNew(job.Run)
                .ConfigureAwait(false);
        }

        Task.Factory.StartNew(() =>
        {
            while(B21_Task的Wait方法是否任务级暂停.MaxCount < 20)
            {
                Console.WriteLine("等待中..");
                Task.Delay(1 * 1000).Wait(); //休眠
            }
            Console.WriteLine(B21_Task的Wait方法是否任务级暂停.MsgList.ToJsonExt());
        });
    }
}

public class TaskJob
{
    private readonly int index;

    public TaskJob(int index)
    {
        this.index = index;
    }

    public void Run()
    {
        while(B21_Task的Wait方法是否任务级暂停.MaxCount < 20)
        {
            B21_Task的Wait方法是否任务级暂停.MsgList.Add("-------------------------------------------------");
            B21_Task的Wait方法是否任务级暂停.MsgList.Add($"[{this.index}]:{DateTime.Now}");
            B21_Task的Wait方法是否任务级暂停.MsgList.Add("-------------------------------------------------");
            B21_Task的Wait方法是否任务级暂停.MaxCount++;

            Task.Delay(this.index * 1000).Wait(); //休眠
        }
    }
}