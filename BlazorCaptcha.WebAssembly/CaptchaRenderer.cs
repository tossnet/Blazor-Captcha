using SkiaSharp;

namespace BlazorCaptcha;

/// <summary>
/// Renders CAPTCHA images using SkiaSharp.
/// </summary>
public static class CaptchaRenderer
{
    /// <summary>
    /// Available font families for CAPTCHA text rendering.
    /// </summary>
    private static readonly string[] FontFamilies = ["Open Sans", "Courier", "Arial", "Times New Roman"];


    //    public static void Draw(SKCanvas canvas, int Height, int Width, string CaptchaWord)
    //    {
    //        Random RandomValue = new();
    //        List<Letter> Letters = new();

    //        // make sure the canvas is blank
    //        SKColor _bgColor = new SKColor((byte)RandomValue.Next(70, 100),
    //                               (byte)RandomValue.Next(60, 80),
    //                               (byte)RandomValue.Next(50, 90));

    //        canvas.Clear(_bgColor);

    //        var fontFamilies = new string[] { "Open Sans", "Courier", "Arial", "Times New Roman" };

    //        Letters.Clear();

    //        foreach (char c in CaptchaWord)
    //        {
    //            var letter = new Letter
    //            {
    //                Value = c.ToString(),
    //                Angle = RandomValue.Next(-15, 25),
    //                ForeColor = new SKColor((byte)RandomValue.Next(100, 256),
    //                                        (byte)RandomValue.Next(110, 256),
    //                                        (byte)RandomValue.Next(90, 256)),
    //                Family = fontFamilies[RandomValue.Next(0, fontFamilies.Length)],
    //            };

    //            Letters.Add(letter);
    //        }

    //        using SKPaint paint = new();
    //        float x = 10;

    //        foreach (Letter l in Letters)
    //        {
    //            using var font = new SKFont
    //            {
    //                Typeface = SKTypeface.FromFamilyName(l.Family),
    //                Size = RandomValue.Next(Height / 2, (Height / 2) + (Height / 4))
    //            };

    //            font.Edging = SKFontEdging.Antialias;
    //            paint.Color = l.ForeColor;
    //            paint.IsAntialias = true;

    //            float textWidth = font.GetGlyphWidths(l.Value, out SKRect[] bounds)[0];
    //            var y = (Height - bounds[0].Height);

    //            canvas.Save();
    //            canvas.RotateDegrees(l.Angle, x, y);
    //            canvas.DrawText(l.Value, x, y, SKTextAlign.Left, font, paint);
    //#if DEBUG
    //            //Draw red rectangle to debug:
    //            var y2 = GetNewY(x, y, bounds[0].Width, l.Angle);
    //            var paint1 = new SKPaint
    //            {
    //                TextSize = 64.0f,
    //                IsAntialias = true,
    //                Color = new SKColor(255, 0, 0),
    //                Style = SKPaintStyle.Stroke
    //            };
    //            canvas.DrawRect(bounds[0].Left + x, y2 + bounds[0].Top, bounds[0].Width, bounds[0].Height, paint1);
    //#endif
    //            canvas.Restore();

    //            x += textWidth + 10;
    //        }

    //        canvas.DrawLine(0, RandomValue.Next(0, Height), Width, RandomValue.Next(0, Height), paint);
    //        canvas.DrawLine(0, RandomValue.Next(0, Height), Width, RandomValue.Next(0, Height), paint);
    //        paint.Style = SKPaintStyle.Stroke;
    //        canvas.DrawOval(RandomValue.Next(-Width, Width), RandomValue.Next(-Height, Height), Width, Height, paint);
    //    }

    /// <summary>
    /// Draws a CAPTCHA image on the specified canvas.
    /// </summary>
    /// <param name="canvas">The SkiaSharp canvas to draw on.</param>
    /// <param name="height">The height of the CAPTCHA image.</param>
    /// <param name="width">The width of the CAPTCHA image.</param>
    /// <param name="captchaWord">The text to render as CAPTCHA.</param>
    public static void Draw(SKCanvas canvas, int height, int width, string captchaWord)
    {
        // Clear canvas with random dark background color
        SKColor bgColor = new(
            (byte)Random.Shared.Next(70, 100),
            (byte)Random.Shared.Next(60, 80),
            (byte)Random.Shared.Next(50, 90));

        canvas.Clear(bgColor);

        // Layer 1: Background distortion (BEFORE text)
        DrawBackgroundDistortion(canvas, height, width);

        using SKPaint paint = new() { IsAntialias = true };
        float x = 10;

        // Layer 2: Draw text
        foreach (char c in captchaWord)
        {
            int angle = Random.Shared.Next(-15, 25);
            SKColor foreColor = new(
                (byte)Random.Shared.Next(100, 256),
                (byte)Random.Shared.Next(110, 256),
                (byte)Random.Shared.Next(90, 256));
            string family = FontFamilies[Random.Shared.Next(FontFamilies.Length)];

            using var font = new SKFont
            {
                Typeface = SKTypeface.FromFamilyName(family),
                Size = Random.Shared.Next(height / 2, (height / 2) + (height / 4)),
                Edging = SKFontEdging.Antialias
            };

            paint.Color = foreColor;

            // SkiaSharp API requires string, not Span
            string letter = c.ToString();
            float textWidth = font.MeasureText(letter);
            font.GetGlyphWidths(letter, out SKRect[] bounds);
            float y = height - bounds[0].Height;

            canvas.Save();
            canvas.RotateDegrees(angle, x, y);
            canvas.DrawText(letter, x, y, SKTextAlign.Left, font, paint);

#if DEBUG
                DrawDebugRectangle(canvas, x, y, angle, bounds[0]);
#endif

            canvas.Restore();
            x += textWidth + 10;
        }

        // Layer 3: Foreground distortion (AFTER text) - lighter
        DrawForegroundDistortion(canvas, height, width);
    }

    /// <summary>
    /// Draws background distortion elements (behind text).
    /// </summary>
    private static void DrawBackgroundDistortion(SKCanvas canvas, int height, int width)
    {
        // Draw 2-3 subtle wavy lines behind text
        int lineCount = Random.Shared.Next(2, 4);
        for (int i = 0; i < lineCount; i++)
        {
            DrawWavyLine(canvas, height, width, opacity: 180);
        }

        // Draw subtle noise dots
        DrawNoiseDots(canvas, height, width, dotCount: Random.Shared.Next(15, 25), maxRadius: 2);
    }

    /// <summary>
    /// Draws foreground distortion elements (in front of text) - lighter touch.
    /// </summary>
    private static void DrawForegroundDistortion(SKCanvas canvas, int height, int width)
    {
        // Draw 1-2 crossing lines on top
        int lineCount = Random.Shared.Next(1, 3);
        for (int i = 0; i < lineCount; i++)
        {
            DrawWavyLine(canvas, height, width, opacity: 200);
        }

        // Draw a subtle oval
        using var ovalPaint = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 1,
            Color = GetRandomColor(minBrightness: 100, opacity: 150)
        };

        canvas.DrawOval(
            Random.Shared.Next(0, width / 2),
            Random.Shared.Next(0, height / 2),
            Random.Shared.Next(width / 3, width / 2),
            Random.Shared.Next(height / 3, height / 2),
            ovalPaint);
    }

    /// <summary>
    /// Draws a wavy Bezier curve across the canvas.
    /// </summary>
    private static void DrawWavyLine(SKCanvas canvas, int height, int width, byte opacity)
    {
        using var linePaint = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = Random.Shared.Next(1, 3),
            Color = GetRandomColor(minBrightness: 100, opacity: opacity)
        };

        using var path = new SKPath();

        // Start point on left edge
        float startY = Random.Shared.Next(0, height);
        path.MoveTo(0, startY);

        // Create 2-3 control points for natural curve
        int segments = Random.Shared.Next(2, 4);
        float segmentWidth = width / (float)segments;

        for (int i = 0; i < segments; i++)
        {
            float cp1X = (i * segmentWidth) + (segmentWidth / 3);
            float cp1Y = Random.Shared.Next(0, height);
            float cp2X = (i * segmentWidth) + (2 * segmentWidth / 3);
            float cp2Y = Random.Shared.Next(0, height);
            float endX = (i + 1) * segmentWidth;
            float endY = Random.Shared.Next(0, height);

            path.CubicTo(cp1X, cp1Y, cp2X, cp2Y, endX, endY);
        }

        canvas.DrawPath(path, linePaint);
    }

    /// <summary>
    /// Draws random noise dots across the canvas.
    /// </summary>
    private static void DrawNoiseDots(SKCanvas canvas, int height, int width, int dotCount, int maxRadius)
    {
        using var dotPaint = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill
        };

        for (int i = 0; i < dotCount; i++)
        {
            dotPaint.Color = GetRandomColor(minBrightness: 60, opacity: 200);
            float x = Random.Shared.Next(0, width);
            float y = Random.Shared.Next(0, height);
            float radius = Random.Shared.Next(1, maxRadius + 1);
            canvas.DrawCircle(x, y, radius, dotPaint);
        }
    }

    /// <summary>
    /// Generates a random color with minimum brightness threshold and opacity.
    /// </summary>
    private static SKColor GetRandomColor(int minBrightness, byte opacity)
    {
        return new SKColor(
            (byte)Random.Shared.Next(minBrightness, 256),
            (byte)Random.Shared.Next(minBrightness, 256),
            (byte)Random.Shared.Next(minBrightness, 256),
            opacity);
    }

#if DEBUG
    /// <summary>
    /// Draws a debug rectangle around each letter for visual debugging.
    /// </summary>
    private static void DrawDebugRectangle(SKCanvas canvas, float x, float y, int angle, SKRect bounds)
    {
        float y2 = GetNewY(x, y, bounds.Width, angle);

        using var debugPaint = new SKPaint
        {
            TextSize = 64.0f,
            IsAntialias = true,
            Color = new SKColor(255, 0, 0),
            Style = SKPaintStyle.Stroke
        };

        canvas.DrawRect(bounds.Left + x, y2 + bounds.Top, bounds.Width, bounds.Height, debugPaint);
    }

    private static float GetNewY(float x1, float y1, float length, float angle)
    {
        angle *= (float)Math.PI / 180;
        return (float)(y1 + length * Math.Sin(angle));
    }
#endif
}
