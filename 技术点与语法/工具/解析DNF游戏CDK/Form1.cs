using System.Text;

namespace FormGetCDK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            this.InitializeComponent();
        }

        private static readonly string[] separator = new string[] { "\t", " " };

        #region 解析按钮-点击事件

        private void button1_Click(object sender, EventArgs e)
        {
            #region 读取

            string[] contentList = this.textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            contentList = contentList.Where(t => !string.IsNullOrEmpty(t)).ToArray();

            #endregion 读取

            #region 解析

            List<string> resultList = [];
            foreach(string line in contentList)
            {
                IEnumerable<string> lineStrs = line
                    .Split(separator, StringSplitOptions.None)
                    .Where(t => !string.IsNullOrEmpty(t))
                    .Select(t => t.Trim())
                    .Skip(1);//拆开后第1个是名称，第2个才是CDK

                foreach(var lineStr in lineStrs)
                {
                    if(char.IsAscii(lineStr[0]))
                    {
                        resultList.Add(lineStr);
                        break;
                    }
                }
            }

            #endregion 解析

            this.textBox2.Text = string.Join("\r\n", resultList);
        }

        #endregion 解析按钮-点击事件

        #region 排序CDK按钮-点击事件

        private void button2_Click(object sender, EventArgs e)
        {
            #region 获取文件地址

            string[] fileNames = Array.Empty<string>();
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Multiselect = true;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileNames = openFileDialog.FileNames;
                // 这里可以处理选中的文件
            }

            if(fileNames == null || fileNames.Length == 0)
            {
                return;
            }

            #endregion 获取文件地址

            // 读取目录下所有的txt文件，按行读取
            List<string> lines = ReadLinesFromTxtFiles(fileNames);

            // 将处理好的List写到给定的目录下，文件名以时间戳，写成一个txt文件，按行写入
            string? exePath = Path.GetDirectoryName(fileNames[0]);
            var sortCdkResult = WriteLinesToTxtFile(lines, exePath);

            // 显示提示信息
            this.textBox2.Text = $"排序完成，文件地址:{Environment.NewLine}{Path.GetFullPath(sortCdkResult)}";
        }

        private static List<string> ReadLinesFromTxtFiles(string[] fileNames)
        {
            List<string> lines = new(128);
            foreach(var fileName in fileNames)
            {
                foreach(var fileLine in File.ReadLines(fileName, Encoding.GetEncoding("GB18030")))
                {
                    if(string.IsNullOrEmpty(fileLine)
                        || fileLine.Contains("状态"))
                    {
                        continue;
                    }

                    lines.Add(fileLine.Trim());
                }
            }

            lines.Sort();// 排序
            return lines.Distinct().ToList();
        }

        private static string WriteLinesToTxtFile(List<string> lines, string? directoryPath)
        {
            //判断地址是否存在
            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            //写入文件
            string fileName = $"刮刮乐排序_{DateTime.Now:yyyyMMddHHmmss}.txt";
            string filePath = Path.Combine(directoryPath, fileName);
            File.WriteAllLines(filePath, lines, Encoding.GetEncoding("GB18030"));
            return filePath;
        }

        #endregion 排序CDK按钮-点击事件
    }
}