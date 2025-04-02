using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlazorCaptcha;

public class Captcha : ComponentBase
{
    [Parameter]
    public int Width { get; set; } = 170;

    [Parameter]
    public int Height { get; set; } = 40;

    [Parameter]
    public int CharNumber { get; set; } = 5;

    [Parameter]
    public EventCallback<MouseEventArgs> OnRefresh { get; set; }

    private string _captchaWord;
    [Parameter]
    public string CaptchaWord
    {
        get => _captchaWord;
        set
        {
            if (_captchaWord != value)
            {
                _captchaWord = value;
                Initialization();
            }
        }
    }

    [Parameter]
    public EventCallback<string> CaptchaWordChanged { get; set; }

    private async Task OnRefreshInternal()
    {
        CaptchaWord = Tools.GetCaptchaWord(CharNumber);
        Initialization();
        await CaptchaWordChanged.InvokeAsync(CaptchaWord);
    }

    private Random RandomValue { get; set; } = new Random();
    private List<Letter> Letters;
    private SKColor _bgColor;
    private string img = string.Empty;

    public Captcha()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (string.IsNullOrEmpty(CaptchaWord))
        {
            return;
        }

        _bgColor = new SKColor((byte)RandomValue.Next(70, 100), 
                               (byte)RandomValue.Next(60, 80), 
                               (byte)RandomValue.Next(50, 90));

        var fontFamilies = new string[] { "Open Sans", "Courier", "Arial", "Times New Roman" };

        Letters = new List<Letter>();

        if (!string.IsNullOrEmpty(CaptchaWord))
        {
            foreach (char c in CaptchaWord)
            {
                var letter = new Letter
                {
                    Value = c.ToString(),
                    Angle = RandomValue.Next(-15, 25),
                    ForeColor = new SKColor((byte)RandomValue.Next(100,256), 
                                            (byte)RandomValue.Next(110,256), 
                                            (byte)RandomValue.Next(90,256)),
                    Family = fontFamilies[RandomValue.Next(0, fontFamilies.Length)],
                };

                Letters.Add(letter);
            }

            int effectiveCaptchaLength = Math.Max(CharNumber, CharNumber);
            int dynamicWidth = Width + (effectiveCaptchaLength - CharNumber) * 10;
            int dynamicHeight = Height;
            using SKBitmap bitmap = new(dynamicWidth, dynamicHeight);
            using SKCanvas canvas = new(bitmap);
            canvas.Clear(_bgColor);

            using (SKPaint paint = new())
            {
                float x = 10;

                foreach (Letter l in Letters)
                {
                    var typeface = SKTypeface.FromFamilyName(l.Family);
                    using var font = new SKFont(typeface, RandomValue.Next(Height / 2, (Height / 2) + (Height / 4)));

                    font.Edging = SKFontEdging.Antialias;
                    paint.Color = l.ForeColor;
                    paint.IsAntialias = true;

                    float textWidth = font.GetGlyphWidths(l.Value, out SKRect[] bounds)[0];
                    //var y = RandomValue.Next(26, 35);
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

            //SKImageInfo imageInfo = new(Width, Height);
            //using (var surface = SKSurface.Create(imageInfo))
            //{
            //    var canvas = surface.Canvas;
            //    canvas.Clear(_bgColor);

            //    using (SKPaint paint = new())
            //    {
            //        float x = 10;

            //        foreach (Letter l in Letters)
            //        {
            //            // Configuration des propriétés liées à SKFont
            //            var typeface = SKTypeface.FromFamilyName(l.Family);

            //            using (var font = new SKFont(typeface, RandomValue.Next(Height / 2, (Height / 2) + (Height / 4))))
            //            {
            //                font.Edging = SKFontEdging.Antialias;
            //                //font.Typeface.FontWeight = SKFontStyleWeight.Bold;
                            
            //                // Configuration des propriétés de SKPaint
            //                paint.Color = l.ForeColor;
            //                paint.IsAntialias = true;
            //                //paint.TextAlign = SKTextAlign.Left;

            //                float width = font.GetGlyphWidths(l.Value, out SKRect[] bounds)[0];

            //                float textWidth = width;
            //                var y = (Height - bounds[0].Height);

            //                canvas.Save();

            //                canvas.RotateDegrees(l.Angle, x, y);
            //                canvas.DrawText(l.Value, x, y, font, paint);

            //                // Draw red rectangle to debug :
            //                //var y2 = GetNewY(x, y, bounds[0].Width, l.Angle);
            //                //var paint1 = new SKPaint
            //                //{
            //                //    TextSize = 64.0f,
            //                //    IsAntialias = true,
            //                //    Color = new SKColor(255, 0, 0),
            //                //    Style = SKPaintStyle.Stroke
            //                //};
            //                //canvas.DrawRect(bounds[0].Left + x, y2 + bounds[0].Top, bounds[0].Width, bounds[0].Height, paint1);

            //                canvas.Restore();

            //                x += textWidth + 10;
            //            }
            //        }

            //        canvas.DrawLine(0, RandomValue.Next(0, Height), Width, RandomValue.Next(0, Height), paint);
            //        canvas.DrawLine(0, RandomValue.Next(0, Height), Width, RandomValue.Next(0, Height), paint);
            //        paint.Style = SKPaintStyle.Stroke;
            //        canvas.DrawOval(RandomValue.Next(-Width, Width), RandomValue.Next(-Height, Height), Width, Height, paint);
            //    }

            using SKImage image = SKImage.FromBitmap(bitmap);
            using SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100);

            img = ConvStreamToBase64(data);
        }
    }

    private static string ConvStreamToBase64(SKData Data)
    {
        MemoryStream ms = new();
        Data.SaveTo(ms);

        byte[] byteArray = ms.ToArray();
        string b64String = Convert.ToBase64String(byteArray);

        return $"data:image/jpg;base64,{b64String}";
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (RandomValue == null || string.IsNullOrEmpty(CaptchaWord)) return;

        var seq = 0;
        builder.OpenElement(++seq, "div");
        builder.AddAttribute(++seq, "class", "divCaptach");
        {
            builder.OpenElement(++seq, "img");
            builder.AddAttribute(++seq, "src", img);
            builder.CloseElement(); 

            builder.OpenElement(++seq, "button");
            {
                builder.AddAttribute(++seq, "class", "btn-refresh");
                builder.AddAttribute(++seq, "type", "button");
                builder.AddAttribute(++seq, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, () => OnRefreshInternal()));
            }
            builder.CloseElement(); 
        }
        builder.CloseElement();

        base.BuildRenderTree(builder);
    }

#if DEBUG
    private static float GetNewY( float x1, float y1, float length, float angle)
    {
        angle *= (float) Math.PI / 180;

        //var x2 = x1 + length * Math.Cos(angle);
        return (float)(y1 + length * Math.Sin(angle));
    }
#endif
}
