using System.Data;
using System.Text;
using OfficeOpenXml;

namespace 从json转换DND法术表
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DataTable dt = ReadFile();

            StringBuilder stringBuilder = new();

            foreach(DataRow row in dt.Rows)
            {
                //   stringBuilder.AppendLine($"public static readonly string {row["职业"]} = \"{row["技能"]}\";");
                stringBuilder.AppendLine($"\"{row["职业"]}\",\"{row["环位"]}\",\"{row["技能"]}\"");
            }

            string aaaa = stringBuilder.ToString();
        }

        public static DataTable ReadFile()
        {
            // 1. 加载Excel文件
            ExcelPackage package = new(new FileInfo(@"E:\个人\learningTest\技术点与语法\工具\从json转换DND法术表\5e不全书源码\速查\法术速查\5E万法大全.xlsx"));

            // 2. 获取指定工作表
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet4"];

            // 3. 准备DataTable
            DataTable dataTable = new();
            dataTable.Columns.Add("职业", typeof(string));
            dataTable.Columns.Add("环位", typeof(string));
            dataTable.Columns.Add("技能", typeof(string));

            // 4. 按单元格读取数据
            for(int row = 1 ; row <= worksheet.Dimension.Rows ; row++)
            {
                string? jobName = worksheet.Cells[row, 1].Value?.ToString();//第一列 职业
                string tempRing = string.Empty;

                for(int col = 1 ; col <= worksheet.Dimension.Columns ; col++)
                {
                    // 获取单元格的值
                    string? cellValue = worksheet.Cells[row, col].Value?.ToString();
                    if(cellValue == null || string.IsNullOrEmpty(cellValue))
                    {
                        continue;
                    }

                    if(cellValue.Contains('环'))
                    {
                        tempRing = cellValue;
                        continue;
                    }
                    if(tempRing != null && !string.IsNullOrEmpty(tempRing))
                    {
                        DataRow tempRow = dataTable.NewRow();
                        tempRow["职业"] = jobName;
                        tempRow["环位"] = tempRing;
                        tempRow["技能"] = cellValue;
                        dataTable.Rows.Add(tempRow);
                        continue;
                    }
                }
            }

            // 4. 关闭Excel文件
            package.Dispose();

            return dataTable;
        }
    }
}