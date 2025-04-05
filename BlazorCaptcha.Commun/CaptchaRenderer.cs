using SkiaSharp;

namespace BlazorCaptcha;

/// <summary />
public static class CaptchaRenderer
{
    /// <summary />
    public static void Draw(SKCanvas canvas, int Height, int Width, string CaptchaWord)
    {
        Random RandomValue = new();
        List<Letter> Letters = new();

        // make sure the canvas is blank
        SKColor _bgColor = new SKColor((byte)RandomValue.Next(70, 100),
                               (byte)RandomValue.Next(60, 80),
                               (byte)RandomValue.Next(50, 90));

        canvas.Clear(_bgColor);

        var fontFamilies = new string[] { "Open Sans", "Courier", "Arial", "Times New Roman" };

        foreach (char c in CaptchaWord)
        {
            var letter = new Letter
            {
                Value = c.ToString(),
                Angle = RandomValue.Next(-15, 25),
                ForeColor = new SKColor((byte)RandomValue.Next(100, 256),
                                        (byte)RandomValue.Next(110, 256),
                                        (byte)RandomValue.Next(90, 256)),
                Family = fontFamilies[RandomValue.Next(0, fontFamilies.Length)],
            };

            Letters.Add(letter);
        }

        using SKPaint paint = new();
        float x = 10;

        foreach (Letter l in Letters)
        {
            using var font = new SKFont
            {
                Typeface = SKTypeface.FromFamilyName(l.Family),
                Size = RandomValue.Next(Height / 2, (Height / 2) + (Height / 4))
            };

            font.Edging = SKFontEdging.Antialias;
            paint.Color = l.ForeColor;
            paint.IsAntialias = true;

            float textWidth = font.GetGlyphWidths(l.Value, out SKRect[] bounds)[0];
            var y = (Height - bounds[0].Height);

            canvas.Save();
            canvas.RotateDegrees(l.Angle, x, y);
            canvas.DrawText(l.Value, x, y, SKTextAlign.Left, font, paint);
#if DEBUG
            //Draw red rectangle to debug:
            var y2 = GetNewY(x, y, bounds[0].Width, l.Angle);
            var paint1 = new SKPaint
            {
                TextSize = 64.0f,
                IsAntialias = true,
                Color = new SKColor(255, 0, 0),
                Style = SKPaintStyle.Stroke
            };
            canvas.DrawRect(bounds[0].Left + x, y2 + bounds[0].Top, bounds[0].Width, bounds[0].Height, paint1);
#endif
            canvas.Restore();

            x += textWidth + 10;
        }

        canvas.DrawLine(0, RandomValue.Next(0, Height), Width, RandomValue.Next(0, Height), paint);
        canvas.DrawLine(0, RandomValue.Next(0, Height), Width, RandomValue.Next(0, Height), paint);
        paint.Style = SKPaintStyle.Stroke;
        canvas.DrawOval(RandomValue.Next(-Width, Width), RandomValue.Next(-Height, Height), Width, Height, paint);
    }

#if DEBUG
    private static float GetNewY(float x1, float y1, float length, float angle)
    {
        angle *= (float)Math.PI / 180;

        return (float)(y1 + length * Math.Sin(angle));
    }
#endif
}
