using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MessagePack;
using MessagePack.Resolvers;

namespace 技术点验证
{
    public class MsgSerializer
    {
        public const string? SerializerName = "MsgSerializer";

        //防止Msg的序列化器会被多次调用
        private static bool IsSetup = false;

        /// <summary>
        /// 项目中配置的默认MsgPack配置
        /// </summary>
#pragma warning disable CS8618 // 不可为 null 的字段未初始化。请考虑声明为可以为 null。
        private static MessagePackSerializerOptions serializerOptions;
#pragma warning restore CS8618 // 不可为 null 的字段未初始化。请考虑声明为可以为 null。

        /// <summary>
        /// 注册Msg的序列化器-启动时调用一次即可
        /// </summary>
        public static void InitializeMsgPackSerializer()
        {
            if(MsgSerializer.IsSetup == true)
            { return; }

            //创建组合解析器
            IFormatterResolver resolver = CompositeResolver.Create(
                  //Builtin原语和标准类解析器。它包括原始（int，bool，string? ...）和可空，数组和列表。和一些额外的内置类型（Guid，Uri，BigInteger等......）。
                  BuiltinResolver.Instance,
                  //合成的解析器。它按以下顺序解析builtin -> attribute -> dynamic enum -> dynamic generic -> dynamic union -> dynamic object -> dynamic object fallback。这是MessagePackSerializer的默认值。
                  StandardResolver.Instance,
                  //所有类和结构的解析器。它不需要MessagePackObjectAttribute和序列化键作为字符串（与标记为[MessagePackObject（true）]相同）。
                  DynamicContractlessObjectResolver.Instance
             //枚举的解析器和可空的。它使用反射调用来解析第一次可以为空。
             // DynamicEnumAsstring?Resolver.Instance
             );
            //转换成Options并赋值到私有字段上面
            serializerOptions = MessagePackSerializerOptions.Standard.WithResolver(resolver);

            MsgSerializer.IsSetup = true;
        }

        public static byte[] Serialize<T>(T data)
        {
            //return MessagePackSerializer.Serialize<T>(data, MessagePack.Resolvers.ContractlessStandardResolver.Options);
            return MessagePackSerializer.Serialize<T>(data, MsgSerializer.serializerOptions);
        }

        public static T Deserialize<T>(byte[] data)
        {
            //  return MessagePackSerializer.Deserialize<T>(data, MessagePack.Resolvers.ContractlessStandardResolver.Options);
            return MessagePackSerializer.Deserialize<T>(data, MsgSerializer.serializerOptions);
        }

        public static byte[] SerializeDefault<T>(T obj)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            //序列化
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        public static T DeserializeDefault<T>(byte[] arrBytes) where T : class
        {
            using MemoryStream ms = new MemoryStream(arrBytes);
            IFormatter br = new BinaryFormatter();
            return (T)br.Deserialize(ms);
        }

        public static string SerializeJson<T>(T data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        public static T DeserializeJson<T>([NotNull] string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }
    }
}