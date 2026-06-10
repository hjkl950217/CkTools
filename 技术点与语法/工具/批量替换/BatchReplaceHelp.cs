// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchReplaceHelp.cs" company="Newegg" Author="lw47">
//   Copyright (c) 2018 Newegg.inc. All rights reserved.
// </copyright>
// <summary>
//   BatchReplaceHelp created at  5/2/2018 1:44:47 PM
// </summary>
//<Description>
//
//</Description>
// --------------------------------------------------------------------------------------------------------------------
//向星辰大海前进！

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BatchReplace
{
    /// <summary>
    /// The class of BatchReplaceHelp.
    /// </summary>
    public class BatchReplaceHelp
    {
        public string SourceAddress { get; set; }

        public string TragetAddress { get; set; }

        public List<FileData> FileList { get; set; }

        public void StartWork(string sourceStr, string trgetStr)
        {
            string basePath = System.Environment.CurrentDirectory;
            this.SourceAddress = basePath + @"\TestSource";
            this.TragetAddress = basePath + @"\NewFile";

            //获取文件名
            this.FileList = this.GetFileName(this.SourceAddress);

            //处理
            this.ProcessFile(sourceStr, trgetStr);

            //生成
            this.WriteFile();
        }

        public List<FileData> GetFileName(string direAddress)
        {
            List<FileData> fileNameList = new List<FileData>();

            DirectoryInfo dir = new DirectoryInfo(direAddress);

            List<FileData> result = dir
                .GetFiles("*.*", SearchOption.AllDirectories)
                .Select(t => new FileData()
                {
                    FullName = t.FullName,
                    Content = File.ReadAllText(t.FullName, Encoding.Default)
                })
                .ToList();

            fileNameList.AddRange(result);
            return fileNameList;
        }

        public List<FileData> ProcessFile(string sourceStr, string trgetStr)
        {
            //替换内容中的字符
            foreach(FileData item in this.FileList)
            {
                item.Content = item.Content.Replace(sourceStr, trgetStr);
            }

            //替换文件名
            foreach(FileData item in this.FileList)
            {
                item.FullName = item.FullName.Replace(this.SourceAddress, this.TragetAddress);
                item.FullName = item.FullName.Replace(sourceStr, trgetStr);
            }

            return this.FileList;
        }

        public void WriteFile()
        {
            foreach(FileData item in this.FileList)
            {
                FileInfo fi = new FileInfo(item.FullName);
                DirectoryInfo di = fi.Directory;
                if(!di.Exists)
                    di.Create();

                //创建文件流
                FileStream fs = new FileStream(item.FullName, FileMode.Create);

                //创建写入器
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                //以流的方式写入
                sw.Write(item.Content);

                //关闭写入器
                sw.Close();

                //关闭文件流
                fs.Close();
            }
        }
    }
}