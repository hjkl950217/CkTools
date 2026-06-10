using Confluent.Kafka;
using Newtonsoft.Json;

namespace 技术点验证.A42_连接Kafka;

[VerifcationType(VerificationTypeEnum.A42_连接Kafka)]
public class A42_连接Kafka : IVerification
{
    public void Start(string[]? args)
    {
        //https://www.cnblogs.com/meowv/p/13614516.html
    }
}

public interface IKafkaService
{
    /// <summary>
    /// 发送消息至指定主题
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <param name="topicName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task PublishAsync<TMessage>(string topicName, TMessage message) where TMessage : class;

    /// <summary>
    /// 从指定主题订阅消息
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <param name="topics"></param>
    /// <param name="messageFunc"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SubscribeAsync<TMessage>(
        IEnumerable<string> topics,
        Action<TMessage> messageFunc,
        CancellationToken cancellationToken) where TMessage : class;
}

public class KafkaService : IKafkaService
{
    private readonly string kafkaAddress;

    public KafkaService(string kafkaAddress)
    {
        this.kafkaAddress = kafkaAddress;
    }

    public async Task PublishAsync<TMessage>(
        string topicName,
        TMessage message)
        where TMessage : class
    {
        ProducerConfig? config = new ProducerConfig
        {
            BootstrapServers = this.kafkaAddress
        };
        using IProducer<string, string>? producer = new ProducerBuilder<string, string>(config).Build();
        await producer.ProduceAsync(topicName, new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = message.ToJsonExt()
        });
    }

    public async Task SubscribeAsync<TMessage>(
        IEnumerable<string> topics,
        Action<TMessage> messageFunc,
        CancellationToken cancellationToken)
        where TMessage : class
    {
        ArgumentNullException.ThrowIfNull(topics);
        ArgumentNullException.ThrowIfNull(messageFunc);

        #region 准备

        ConsumerConfig? config = new ConsumerConfig
        {
            BootstrapServers = this.kafkaAddress,
            GroupId = "crow-consumer",
            EnableAutoCommit = false,
            StatisticsIntervalMs = 5000,
            SessionTimeoutMs = 6000,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnablePartitionEof = true
        };
        //const int commitPeriod = 5;
        using IConsumer<Ignore, string>? consumer = new ConsumerBuilder<Ignore, string>(config)
            .SetErrorHandler((_, e) =>
            {
                Console.WriteLine($"Error: {e.Reason}");
            })
            .SetStatisticsHandler((_, json) =>
            {
                Console.WriteLine($" - {DateTime.Now:yyyy-MM-dd HH:mm:ss} > 消息监听中..");
            })
            .SetPartitionsAssignedHandler((c, partitions) =>
            {
                string partitionsStr = string.Join(", ", partitions);
                Console.WriteLine($" - 分配的 kafka 分区: {partitionsStr}");
            })
            .SetPartitionsRevokedHandler((c, partitions) =>
            {
                string partitionsStr = string.Join(", ", partitions);
                Console.WriteLine($" - 回收了 kafka 的分区: {partitionsStr}");
            })
            .Build();
        consumer.Subscribe(topics);

        #endregion 准备

        #region 处理

        try
        {
            while(true)
            {
                try
                {
                    #region 反序列化消息

                    ConsumeResult<Ignore, string>? consumeResult = consumer.Consume(cancellationToken);
                    Console.WriteLine($"消费信息[{consumeResult?.TopicPartitionOffset}]: '{consumeResult.Message?.Value}'.");
                    if(consumeResult.IsPartitionEOF)
                    {
                        Console.WriteLine($" - {DateTime.Now:yyyy-MM-dd HH:mm:ss} 已经到底了：{consumeResult.Topic}, 分区 {consumeResult.Partition}, 偏移 {consumeResult.Offset}.");
                        continue;
                    }
                    TMessage messageResult = null;
                    try
                    {
                        messageResult = JsonConvert.DeserializeObject<TMessage>(consumeResult.Message.Value);
                    }
                    catch(Exception ex)
                    {
                        string? errorMessage = $" - {DateTime.Now:yyyy-MM-dd HH:mm:ss}【Exception 消息反序列化失败，Value：{consumeResult.Message.Value}】 ：{ex.StackTrace?.ToString()}";
                        Console.WriteLine(errorMessage);
                        messageResult = null;
                    }

                    #endregion 反序列化消息

                    #region 执行委托以处理消息

                    if(messageResult != null/* && consumeResult.Offset % commitPeriod == 0*/)
                    {
                        messageFunc(messageResult);//执行传递进的委托处理消息
                        try
                        {
                            consumer.Commit(consumeResult);
                        }
                        catch(KafkaException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    #endregion 执行委托以处理消息
                }
                catch(ConsumeException e)
                {
                    Console.WriteLine($"消费错误: {e.Error.Reason}");
                }
            }
        }
        catch(OperationCanceledException)
        {
            Console.WriteLine("关闭消费者。");
            consumer.Close();
        }

        #endregion 处理

        await Task.CompletedTask;
    }
}