namespace 技术点验证.A40_任意长度2进制操作
{
    [VerifcationType(VerificationTypeEnum.A40_任意长度2进制操作)]
    public class A40_任意长度2进制操作 : IVerification
    {
        public void Start(string[]? args)
        {
            bool[]? boolArray = BitHelper.ToBoolArray(3UL);
            ulong ulongValue = BitHelper.ToUlong(boolArray);
            long longValue = BitHelper.ToLong(boolArray);
        }
    }

    public static class BitHelper
    {
        public const int LongLength = 64;

        private static Dictionary<int, ulong> bitMap;

        /// <summary>
        /// 记录2进制中对应长度全为1的值
        /// <para>1=1</para>
        /// <para>2=3</para>
        /// <para>3=7</para>
        /// </summary>
        public static Dictionary<int, ulong> BitMap
        {
            get
            {
                if(bitMap == null)
                {
                    bitMap = new Dictionary<int, ulong>(BitHelper.LongLength);
                    for(int i = 1 ; i < BitHelper.LongLength + 1 ; i++)
                    {
                        bitMap[i] = ulong.MaxValue >> (BitHelper.LongLength - i);
                    }
                    return bitMap;
                }
                else
                {
                    return bitMap;
                }
            }
        }

        /// <summary>
        /// 将<paramref name="source"/>中的每一个2进制位转换为<see cref="bool"/>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool[] ToBoolArray(ulong source)
        {
            bool[] result = new bool[BitHelper.LongLength];

            for(int i = 0 ; i < result.Length ; i++)
            {
                result[i] = (source | BitHelper.BitMap[i + 1]) == source;
            }
            return result;
        }

        public static ulong ToUlong(bool[] source)
        {
            if(source.Length != BitHelper.LongLength)
            {
                throw new ArgumentException("必须为64个长度");
            }

            ulong result = ulong.MinValue;
            for(int i = 0 ; i < BitHelper.LongLength ; i++)
            {
                result = source[i]
                    ? (result | BitHelper.BitMap[i + 1])
                    : result;
            }

            return result;
        }

        public static long ToLong(bool[] source)
        {
            return (long)BitHelper.ToUlong(source);
        }
    }

    public static class StringEx
    {
        //bin 2进制
        //oct 8进制
        //dec 10进制
        //hex 16进制

        public static string ToBinString(this ushort source)
        {
            return Convert.ToString(source, 2);
        }

        public static string ToBinString(this ulong source)
        {
            return Convert.ToString((long)source, 2);
        }
    }
}