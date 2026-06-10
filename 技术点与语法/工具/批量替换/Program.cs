using System;

namespace BatchReplace
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Console.WriteLine("请输入要替换的源字符,按回车结束：");
            //string sourceStr = Console.ReadLine();
            //Console.WriteLine("请输入要替换的目标字符,按回车结束：");
            //string trgetStr = Console.ReadLine();

            string sourceStr = "Mkpl.Item";
            string trgetStr = "{{cookiecutter.namespace_name}}";

            Console.WriteLine("运行开始");
            BatchReplaceHelp replaceHelp = new BatchReplaceHelp();

            replaceHelp.StartWork(sourceStr, trgetStr);
            System.Collections.Generic.List<FileData> tt = replaceHelp.FileList;

            foreach(FileData item in tt)
            {
                Console.WriteLine($"{item.FullName}\t{item.Content.Length}");
            }

            Console.WriteLine($"共处理{tt.Count}个文件");
            Console.WriteLine("运行结束");
            Console.ReadLine();
        }
    }
}