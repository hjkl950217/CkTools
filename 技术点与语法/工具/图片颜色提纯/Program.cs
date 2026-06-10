using System.Drawing;
using System.Drawing.Imaging;

namespace PixelGroupingExample
{
    public class Program
    {
        public static string sourcePath = @"C:\Users\gt\Desktop\logo.png";

        private static void Main(string[] args)
        {
            // 1. 读取一个png图片文件
            string inputImagePath = sourcePath;
            Bitmap originalImage = new(inputImagePath);

            // 2. 将其中的像素处理
            Bitmap modifiedImage = B_ModifyPixels(originalImage);

            // 3. 将修改后的文件保存到硬盘上
            string outputImagePath = C_BuildNewFileName(sourcePath);
            modifiedImage.Save(outputImagePath, ImageFormat.Png);

            Console.WriteLine("处理完毕: " + outputImagePath);
            Console.ReadLine();
        }

        // 读取
        private static Bitmap A_GroupPixels(string filePath)
        {
            Bitmap image = new(filePath);

            return new Bitmap(image.Width, image.Height);
        }

        // 计算
        private static Bitmap B_ModifyPixels(Bitmap image)
        {
            Dictionary<byte, int> colorCount = [];
            Bitmap newBitmap = new(1296, 1296);

            for(int x = 0 ; x < image.Width ; x++)
            {
                for(int y = 0 ; y < image.Height ; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    //颜色debug代码
                    if(colorCount.ContainsKey(pixelColor.R))
                    {
                        colorCount[pixelColor.R] += 1;
                    }
                    else
                    {
                        colorCount[pixelColor.R] = 0;
                    }

                    //判断处理
                    Color colorPoint = Color.Empty;
                    if(pixelColor.R > 200 && pixelColor.R < 204)
                    {
                        colorPoint = Color.FromArgb(pixelColor.A, 201, 163, 85);//深棕色
                    }
                    else
                    {
                        colorPoint = Color.FromArgb(pixelColor.A, 250, 247, 242);//淡黄色
                    }

                    image.SetPixel(x, y, colorPoint);
                    newBitmap.SetPixel(x, y, colorPoint);
                }
            }

            for(int x = 0 ; x < newBitmap.Width ; x++)
            {
                for(int y = 0 ; y < newBitmap.Height ; y++)
                {
                    Color pixelColor = newBitmap.GetPixel(x, y);

                    //判断处理
                    Color colorPoint = Color.Empty;
                    if(pixelColor.R == 0)
                    {
                        colorPoint = Color.FromArgb(pixelColor.A, 250, 247, 242);//淡黄色
                    }
                    else
                    {
                        colorPoint = pixelColor;
                    }

                    newBitmap.SetPixel(x, y, colorPoint);
                }
            }

            return newBitmap;
        }

        // 构造新文件名
        private static string C_BuildNewFileName(string sourcePath)
        {
            string directory = Path.GetDirectoryName(sourcePath);
            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string extension = Path.GetExtension(sourcePath);

            fileName = $"{fileName}_{DateTime.Now:HHmmssfff}";

            return Path.Combine(directory, $"{fileName}{extension}");
        }
    }
}