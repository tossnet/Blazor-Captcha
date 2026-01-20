using Microsoft.AspNetCore.Components;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorCaptcha;

/// <summary />
public partial class Captcha : CaptchaComponentBase
{
    /// <summary />
#pragma warning disable BL0007
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
#pragma warning restore BL0007

    private string _captchaWord;
    private string img = null;

    /// <summary />
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Initialization();
    }

    /// <summary />
    private async Task OnRefreshInternal()
    {
        CaptchaWord = Tools.GetCaptchaWord(CharNumber);

        Initialization();

        await CaptchaWordChanged.InvokeAsync(CaptchaWord);
    }

    /// <summary />
    private void Initialization()
    {
        if (string.IsNullOrEmpty(CaptchaWord))
        {
            return;
        }

        int effectiveCaptchaLength = Math.Max(CharNumber, CharNumber);
        int dynamicWidth = Width + (effectiveCaptchaLength - CharNumber) * 10;
        int dynamicHeight = Height;
        using SKBitmap bitmap = new(dynamicWidth, dynamicHeight);
        using SKCanvas canvas = new(bitmap);

        CaptchaRenderer.Draw(canvas, Height, Width, CaptchaWord);

        using SKImage image = SKImage.FromBitmap(bitmap);
        using SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100);

        img = ConvStreamToBase64(data);
    }

    /// <summary />
    private static string ConvStreamToBase64(SKData Data)
    {
        MemoryStream ms = new();
        Data.SaveTo(ms);

        byte[] byteArray = ms.ToArray();
        string b64String = Convert.ToBase64String(byteArray);

        return $"data:image/jpg;base64,{b64String}";
    }
}
