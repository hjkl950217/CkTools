#nullable enable

using System.Diagnostics.CodeAnalysis;

namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B16_特性_在泛型和委托上标记_40)]
    public class B16_特性_AttributeTargets_80 : IVerification
    {
        public void Start(string[]? args)
        {
            Console.WriteLine("请直接看代码");
        }
    }

    public class TestAttributeInfo
    {
        [return: ReturnValue]
        public int TestGenericParameterMethod<[GenericParameter] T>([NotNull] T value)
        {
            //
            /*
             * NotNull：C# 8.0可用，仅会生成警告
             *
             * 条件：
             * 1.项目文件中有
             * <PropertyGroup>
             *  <TargetFramework>netcoreapp3.1</TargetFramework>
             *  <LangVersion>8.0</LangVersion>
             *  <Nullable>enable</Nullable>
             * </PropertyGroup>
             *
             * 2. 或者在CS文件最上面添加 #nullable enable
             *
             */

            return 0;
        }

        public class SampleEventArgs
        {
            public SampleEventArgs(string s)
            {
                this.Text = s;
            }

            public String Text { get; }
        }

        //如果打在非 delegate 上 会报错
        [Delegate]
        public delegate void SampleEventHandler(object sender, SampleEventArgs e);

        //如果打在非 Event 上 会报错
        [Event]
        public event SampleEventHandler? SampleEvent;

        protected virtual void Show_HowToUseEvent()
        {
            SampleEvent?.Invoke(this, new SampleEventArgs("Hello"));
        }
    }
}