using Microsoft.AspNetCore.Http;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Numerics;
using System.Text;

namespace ProNotes.AppLib.Tools
{
    /// dotnet add package SixLabors.ImageSharp
    /// dotnet add package SixLabors.ImageSharp.Drawing --prerelease
    public static class Captcha2
    {
        public static Color[] TextColor { get; set; } = new Color[] { Color.Blue, Color.Black, Color.Black, Color.Brown, Color.Gray, Color.Green };

        public static Color[] NoiseRateColor { get; set; } = new Color[] { Color.Gray };

        private static CaptchaResult GenerateCaptchaImage(string captchaCode, int width = 100, int height = 36)
        {
            AffineTransformBuilder getRotation(int w, int h)
            {
                Random random = new Random();
                var builder = new AffineTransformBuilder();
                var width = random.Next(10, w);
                var height = random.Next(10, h);
                var pointF = new PointF(width, height);
                var rotationDegrees = random.Next(5, 10);//0,5
                var result = builder.PrependRotationDegrees(rotationDegrees, pointF);
                return result;
            }

            float GenerateNextFloat(double min = -3.40282347E+38, double max = 3.40282347E+38)
            {
                Random random = new Random();
                double range = max - min;
                double sample = random.NextDouble();
                double scaled = sample * range + min;
                float result = (float)scaled;
                return result;
            }

            try
            {
                byte[] result;

                using (var imgText = new Image<Rgba32>(width, height))
                {
                    float position = 0;

                    Random random = new Random(DateTime.Now.Millisecond);

                    byte startWith = (byte)random.Next(5, 10);

                    imgText.Mutate(ctx => ctx.BackgroundColor(Color.Transparent));

                    Font font = SystemFonts.CreateFont("Arial", 24, FontStyle.Regular);//29

                    foreach (char c in captchaCode)
                    {
                        var location = new PointF(startWith + position, random.Next(6, 13));

                        imgText.Mutate(ctx => ctx.DrawText(c.ToString(), font, TextColor[random.Next(0, TextColor.Length)], location));

                        position += TextMeasurer.Measure(c.ToString(), new TextOptions(font)).Width;
                    }

                    AffineTransformBuilder rotation = getRotation(width, height);
                    imgText.Mutate(ctx => ctx.Transform(rotation));

                    ushort size = (ushort)TextMeasurer.Measure(captchaCode, new TextOptions(font)).Width;
                    var img = new Image<Rgba32>(size + 15, height + 5);
                    img.Mutate(ctx => ctx.BackgroundColor(Color.White));

                    Parallel.For(0, 2, i =>
                    {
                        int x0 = random.Next(0, random.Next(0, img.Width));
                        int y0 = random.Next(0, img.Height);
                        int x1 = random.Next(0, img.Width);
                        int y1 = random.Next(0, img.Height);
                        img.Mutate(ctx =>
                                ctx.DrawLines(TextColor[random.Next(0, TextColor.Length)], GenerateNextFloat(0.7f, 2.0f), new PointF[] { new PointF(x0, y0), new PointF(x1, y1) }));
                    });

                    img.Mutate(ctx => ctx.DrawImage(imgText, 0.80f));

                    Parallel.For(0, 250, i =>
                    {
                        int x0 = random.Next(0, img.Width);                        
                        int y0 = random.Next(0, img.Height);
                        img.Mutate(ctx =>
                            ctx.DrawLines(NoiseRateColor[random.Next(0, NoiseRateColor.Length)], GenerateNextFloat(0.5, 1.5), new PointF[] { new Vector2(x0, y0), new Vector2(x0, y0) }));
                    });

                    img.Mutate(x =>
                    {
                        x.Resize(width, height);
                    });

                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms, new PngEncoder());
                        result = ms.ToArray();
                    }
                }

                return new CaptchaResult { CaptchaCode = captchaCode, CaptchaByteData = result, Timestamp = DateTime.Now };

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private const string AllowedItems = "0123456789ABCDEF";

        private static string GenerateCaptchaCode(int characterCount = 5)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < characterCount; i++)
            {
                int index = RandomGenerator.Next(0, AllowedItems.Length);
                sb.Append(AllowedItems[index]);
            }

            return sb.ToString();
        }

        public static CaptchaResult GenerateCaptchaImage()
        {
            CaptchaResult cr = GenerateCaptchaImage(captchaCode: GenerateCaptchaCode());
            return cr;
        }
    }

    public class CaptchaResult
    {
        public string CaptchaCode { get; set; } = string.Empty;
        public byte[] CaptchaByteData { get; set; } = new byte[] { 0 };
        public string CaptchBase64Data => Convert.ToBase64String(CaptchaByteData);
        public DateTime Timestamp { get; set; }
    }
}