namespace 语法验证与学习
{
    /// <summary>
    /// 长方形-结构体
    /// </summary>
    public struct Rectangle
    {
        /// <summary>
        /// 长
        /// </summary>
        public double Length { get; }

        /// <summary>
        /// 高
        /// </summary>
        public double Height { get; }

        public Rectangle(double length, double height)
        {
            this.Length = length;
            this.Height = height;
        }
    }
}