using System;
using System.Text;
using System.Windows.Forms;

namespace FromatErroInfo
{
    public partial class Form_Main : Form
    {
        /// <summary>
        /// 默认换行符
        /// </summary>
        private const string DefaultPlaceholder = "[Enter]";

        public Form_Main()
        {
            this.InitializeComponent();

            StringBuilder showInfo = new StringBuilder();
            showInfo.Append("如何使用？");
            showInfo.Append("1.复制错误信息到这里面自动格式化");
            showInfo.Append("2.下次使用时再次复制到输入框即可");
            showInfo.Append("\r\n");
            showInfo.Append("推荐：双击标题栏全屏后使用");
            showInfo.Append("推荐2：如果你觉得好用，想经常用，可以配置环境变量，这样就可以使用win+搜索快捷访问了");
            showInfo.Append("\r\n");
            showInfo.Append("有任何疑问请提讨论或发邮件给我(lynn.x.wang@newegg.com)");
        }

        /// <summary>
        /// 值改变时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Erro_TextChanged(object sender, EventArgs e)
        {
            string soureTxt = this.Txt_Erro.Text;

            //找到冒号 \r\n \\r\\n  统一替换成[Enter]
            StringBuilder textA = new StringBuilder(soureTxt);

            //冒号的加两个回车-暂时不加了
            //textA.Replace(":", "[Colon]");
            //textA.Replace("：", "[ChinaColon]");

            //有-代表跨堆栈了 加个回车
            textA.Replace("--- End", "[Enter][Other Stack] End");

            //有Inner代表有内部异常了 加个回车
            textA.Replace("Inner", "[Enter]Inner");

            //at的换行处理
            this.ReplaceByAt(textA, Form_Main.DefaultPlaceholder);

            //换行符处理
            this.ReplaceByn(textA, Form_Main.DefaultPlaceholder);

            //还原处理
            StringBuilder textB = new StringBuilder(textA.ToString());
            textB.Replace(Form_Main.DefaultPlaceholder, "\r\n");
            //textB.Replace("[Bar]", "\r\n--- End");
            //textB.Replace("[Colon]", ":");
            //textB.Replace("[ChinaColon]", "：");

            this.Txt_Erro.Text = textB.ToString();
        }

        /// <summary>
        /// 鼠标按下时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Erro_MouseDown(object sender, MouseEventArgs e)
        {
            // this.Txt_Erro.SelectAll();
        }

        /// <summary>
        /// 处理换行符中是\r\n \n这类情况
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <returns></returns>
        private StringBuilder ReplaceByn(StringBuilder stringBuilder, string placeholder = Form_Main.DefaultPlaceholder)
        {
            stringBuilder.Replace("\r\n", placeholder);
            stringBuilder.Replace("\\r\\n", placeholder);
            stringBuilder.Replace("\\n", placeholder);
            stringBuilder.Replace("\n", placeholder);
            stringBuilder.Replace(@"\", "");//处理完后  把\都去掉

            return stringBuilder;
        }

        /// <summary>
        /// 处理at的情况
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="placeholder"></param>
        /// <returns></returns>
        private StringBuilder ReplaceByAt(StringBuilder stringBuilder, string placeholder = Form_Main.DefaultPlaceholder)
        {
            // stringBuilder.

            return stringBuilder;
        }
    }
}