using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace 技术点验证
{
    /*
  * 需要
  *
  *  Microsoft.Extensions.FileProviders
  *
  */

    [VerifcationType(VerificationTypeEnum.A09_读取文件并监控变化)]
    public class A09_读取文件并监控变化 : IVerification
    {
        public VerificationTypeEnum VerificationType =>
            VerificationTypeEnum.A09_读取文件并监控变化;

        private const string? testFile = "testA9.json";

        public void Start(string[]? args)
        {
            //设置一个目录映射
            //一个IFileProvider 可以看做是对一个目录的映射

            string? debugAddress = Directory.GetCurrentDirectory();
            string? root = Path.Combine(debugAddress, $"{{{nameof(A09_读取文件并监控变化)}}}");

            Console.WriteLine($"根目录: {root}\t 测试文件:{testFile}");

            //建立映射
            IFileProvider fileProvider = new PhysicalFileProvider(root);

            //先显示一波
            Stream fileStream = fileProvider.GetFileInfo(testFile).CreateReadStream();
            string? testData = ReadData(fileStream);
            Console.WriteLine($"原始数据:{testData}");
            Console.WriteLine();

            //注册改变时的事件
            ChangeToken.OnChange(
                () => fileProvider.Watch(testFile),
                () => ShowChange(fileProvider)
                );

            // ChangeToken.OnChange();
        }

        public static void ShowChange(IFileProvider fileProvider)
        {
            Stream fileStream = fileProvider.GetFileInfo(testFile).CreateReadStream();
            string? testData = ReadData(fileStream);
            Console.WriteLine($"改变后的数据:{testData}");
            Console.WriteLine();
        }

        public static string? ReadData(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string? result = reader.ReadToEnd();

            reader.Close();
            stream.Close();

            return result;
        }
    }
}